Public Class ProductForm
    Public adding As Boolean = False
    Public updating As Boolean = False
    Public productid As Integer = Nothing

    Private Sub ProductForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        ' The designer left the default limit of 100, which silently cut larger stock counts to 100 on save.
        txtstock.Maximum = 1000000
        btnnew.Enabled = True
        btnsave.Enabled = False
        pnlinput.Enabled = False
        fill()
        LoadCategories()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        fill()
    End Sub

    Public Sub fill()
        Dim search As String = txtsearch.Text.Trim()
        Dim query As String = "SELECT p.productid, p.productname, p.categoryid, c.categoryname, p.stock, p.price " &
                              "FROM product p LEFT JOIN category c ON p.categoryid = c.id "

        ' Add search filter
        If search <> "" Then
            query &= "WHERE p.productid LIKE @s OR p.productname LIKE @s OR c.categoryname LIKE @s OR p.price LIKE @s OR p.stock LIKE @s "
        End If

        query &= "ORDER BY p.productname ASC"

        GetQuery(query, "product", P("@s", "%" & search & "%"))
        productlist.Items.Clear()

        For Each row As DataRow In ds.Tables("product").Rows
            With productlist.Items.Add(row("productid").ToString())
                .SubItems.Add(row("productname").ToString())
                .SubItems.Add(row("categoryname").ToString())
                .SubItems.Add(row("stock").ToString())
                .SubItems.Add(row("price").ToString())
                .Tag = row("categoryid")
            End With
        Next
    End Sub


    Public Sub LoadCategories()
        GetQuery("SELECT id, categoryname FROM category ORDER BY categoryname ASC", "category")
        cbocategory.DisplayMember = "categoryname"
        cbocategory.ValueMember = "id"
        cbocategory.DataSource = ds.Tables("category").Copy()
        cbocategory.SelectedIndex = -1
    End Sub

    ' Enable buttons for saving/canceling
    Public Sub enablebuttons()
        btnnew.Enabled = False
        btnupdate.Enabled = False
        btndelete.Enabled = False
        btncancel.Enabled = True
        btnsave.Enabled = True
    End Sub

    Public Sub disablebuttons()
        btnnew.Enabled = True
        btnupdate.Enabled = False
        btndelete.Enabled = False
        btncancel.Enabled = False
        btnsave.Enabled = False
        pnlinput.Enabled = False
    End Sub

    Public Sub clearfields()
        txtproductname.Clear()
        cbocategory.SelectedIndex = -1
        txtstock.Value = 0
        txtprice.Clear()
        productid = Nothing
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        enablebuttons()
        clearfields()
        adding = True
        pnlinput.Enabled = True
        LoadCategories()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim price As Decimal

        If txtproductname.Text.Trim = "" Or cbocategory.SelectedIndex = -1 Or txtprice.Text.Trim = "" Then
            MsgBox("All fields are required!", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        If Not Decimal.TryParse(txtprice.Text.Trim(), price) OrElse price < 0 Then
            MsgBox("Price must be a valid amount (0 or more).", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        Dim saved As Boolean = False
        If adding Then
            saved = SetQuery("INSERT INTO product (productname, categoryid, stock, price) VALUES (@name, @cat, @stock, @price)",
                             P("@name", txtproductname.Text.Trim()), P("@cat", cbocategory.SelectedValue), P("@stock", CInt(txtstock.Value)), P("@price", price))
            If saved Then MsgBox("Product added successfully!", MsgBoxStyle.Information, "Success")
        ElseIf updating Then
            saved = SetQuery("UPDATE product SET productname = @name, categoryid = @cat, stock = @stock, price = @price WHERE productid = @id",
                             P("@name", txtproductname.Text.Trim()), P("@cat", cbocategory.SelectedValue), P("@stock", CInt(txtstock.Value)), P("@price", price), P("@id", productid))
            If saved Then MsgBox("Product updated successfully!", MsgBoxStyle.Information, "Success")
        End If

        If Not saved Then Exit Sub

        fill()
        disablebuttons()
        clearfields()
        adding = False
        updating = False
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        If productid = Nothing Then
            MsgBox("Select a product to update.", MsgBoxStyle.Information, "No Selection")
            Exit Sub
        End If

        enablebuttons()
        updating = True
        pnlinput.Enabled = True
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If productid = Nothing Then
            MsgBox("Select a product to delete.", MsgBoxStyle.Information, "No Selection")
            Exit Sub
        End If

        ' The foreign keys cascade, so deleting a product would erase it from past orders and stock history.
        Dim used As Integer = CInt(GetValue("SELECT (SELECT COUNT(*) FROM orderdetails WHERE productid = @id) + (SELECT COUNT(*) FROM stockin_details WHERE productid = @id) + (SELECT COUNT(*) FROM stockout_details WHERE productid = @id)",
                                            P("@id", productid)))
        If used > 0 Then
            MsgBox("This product appears in orders or stock records and can't be deleted.", MsgBoxStyle.Exclamation, "Product In Use")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to delete this product?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Delete") = MsgBoxResult.Yes Then
            If SetQuery("DELETE FROM product WHERE productid = @id", P("@id", productid)) Then
                MsgBox("Product deleted successfully!", MsgBoxStyle.Information, "Success")
                fill()
                clearfields()
                disablebuttons()
            End If
        End If
    End Sub

    Private Sub productlist_DoubleClick(sender As Object, e As EventArgs) Handles productlist.DoubleClick
        If adding Or updating Or productlist.SelectedItems.Count = 0 Then Exit Sub

        Dim item As ListViewItem = productlist.SelectedItems(0)
        productid = CInt(item.SubItems(0).Text)
        txtproductname.Text = item.SubItems(1).Text
        Dim stock As Decimal
        Decimal.TryParse(item.SubItems(3).Text, stock)
        txtstock.Value = Math.Max(0D, Math.Min(stock, txtstock.Maximum))
        txtprice.Text = item.SubItems(4).Text

        LoadCategories()
        If item.Tag IsNot Nothing AndAlso Not IsDBNull(item.Tag) Then
            cbocategory.SelectedValue = item.Tag
        Else
            cbocategory.SelectedIndex = -1
        End If

        btnupdate.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        If adding Then
            If MsgBox("Are you sure you want to cancel adding new product information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Cancel") = MsgBoxResult.Yes Then
                adding = False
            Else
                Exit Sub
            End If
        ElseIf updating Then
            If MsgBox("Are you sure you want to cancel updating product information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Cancel") = MsgBoxResult.Yes Then
                updating = False
            Else
                Exit Sub
            End If
        End If

        disablebuttons()
        clearfields()
        pnlinput.Enabled = False
        productid = Nothing
    End Sub

    Private Sub txtproductname_GotFocus(sender As Object, e As EventArgs) Handles txtproductname.GotFocus
        HandleFocus(shapeproduct, True)
    End Sub

    Private Sub txtproductname_LostFocus(sender As Object, e As EventArgs) Handles txtproductname.LostFocus
        HandleFocus(shapeproduct, False)
    End Sub

    Private Sub cbocategory_GotFocus(sender As Object, e As EventArgs) Handles cbocategory.GotFocus
        HandleFocus(shapecategory, True)
    End Sub

    Private Sub cbocategory_LostFocus(sender As Object, e As EventArgs) Handles cbocategory.LostFocus
        HandleFocus(shapecategory, False)
    End Sub

    Private Sub txtstock_GotFocus(sender As Object, e As EventArgs) Handles txtstock.GotFocus
        HandleFocus(shapestock, True)
    End Sub

    Private Sub txtstock_LostFocus(sender As Object, e As EventArgs) Handles txtstock.LostFocus
        HandleFocus(shapestock, False)
    End Sub

    Private Sub txtprice_GotFocus(sender As Object, e As EventArgs) Handles txtprice.GotFocus
        HandleFocus(shapeprice, True)
    End Sub

    Private Sub txtprice_LostFocus(sender As Object, e As EventArgs) Handles txtprice.LostFocus
        HandleFocus(shapeprice, False)
    End Sub

    Private Sub txtsearch_GotFocus(sender As Object, e As EventArgs) Handles txtsearch.GotFocus
        HandleFocus(shapesearch, True)
    End Sub

    Private Sub txtsearch_LostFocus(sender As Object, e As EventArgs) Handles txtsearch.LostFocus
        HandleFocus(shapesearch, False)
    End Sub
End Class
