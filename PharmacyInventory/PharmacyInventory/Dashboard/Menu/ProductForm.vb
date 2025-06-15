Public Class ProductForm
    Public adding As Boolean = False
    Public updating As Boolean = False
    Public productid As Integer = Nothing

    Private Sub ProductForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect("localhost", "dbpharmacy", "3306", "root", "")
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
        Dim query As String = "SELECT p.productid, p.productname, c.categoryname, p.stock, p.price " &
                              "FROM product p LEFT JOIN category c ON p.categoryid = c.id "

        ' Add search filter
        If search <> "" Then
            query &= "WHERE p.productid LIKE '%" & search & "%' " &
                     "OR p.productname LIKE '%" & search & "%' " &
                     "OR c.categoryname LIKE '%" & search & "%' " &
                     "OR p.price LIKE '%" & search & "%' " &
                     "OR p.stock LIKE '%" & search & "%' "
        End If

        query &= "ORDER BY p.productname ASC"

        GetQuery(query, "product")
        productlist.Items.Clear()

        For Each row As DataRow In ds.Tables("product").Rows
            With productlist.Items.Add(row("productid").ToString())
                .SubItems.Add(row("productname").ToString())
                .SubItems.Add(row("categoryname").ToString())
                .SubItems.Add(row("stock").ToString())
                .SubItems.Add(row("price").ToString())
            End With
        Next
    End Sub


    Public Sub LoadCategories()
        GetQuery("SELECT id, categoryname FROM category ORDER BY categoryname ASC", "category")
        cbocategory.DisplayMember = "categoryname"
        cbocategory.ValueMember = "id"
        cbocategory.DataSource = ds.Tables("category")
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
        If txtproductname.Text.Trim = "" Or cbocategory.SelectedIndex = -1 Or txtstock.Text.Trim = "" Or txtprice.Text.Trim = "" Then
            MsgBox("All fields are required!", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        If adding Then
            SetQuery("INSERT INTO product (productname, categoryid, stock, price) VALUES ('" & txtproductname.Text.Trim() & "', " & cbocategory.SelectedValue & ", " & txtstock.Text.Trim() & ", " & txtprice.Text.Trim() & ")")
            MsgBox("Product added successfully!", MsgBoxStyle.Information, "Success")
        ElseIf updating Then
            SetQuery("UPDATE product SET productname = '" & txtproductname.Text.Trim() & "', categoryid = " & cbocategory.SelectedValue & ", stock = " & txtstock.Text.Trim() & ", price = " & txtprice.Text.Trim() & " WHERE productid = " & productid)
            MsgBox("Product updated successfully!", MsgBoxStyle.Information, "Success")
        End If

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

        If MsgBox("Are you sure you want to delete this product?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Delete") = MsgBoxResult.Yes Then
            SetQuery("DELETE FROM product WHERE productid = " & productid)
            MsgBox("Product deleted successfully!", MsgBoxStyle.Information, "Success")
            fill()
            clearfields()
        End If
    End Sub

    Private Sub productlist_DoubleClick(sender As Object, e As EventArgs) Handles productlist.DoubleClick
        productid = CInt(productlist.FocusedItem.SubItems(0).Text)
        txtproductname.Text = productlist.FocusedItem.SubItems(1).Text
        txtstock.Text = productlist.FocusedItem.SubItems(3).Text
        txtprice.Text = productlist.FocusedItem.SubItems(4).Text

        Dim categoryName As String = productlist.FocusedItem.SubItems(2).Text
        LoadCategories()
        Dim foundRow As DataRow() = ds.Tables("category").Select("categoryname = '" & categoryName & "'")
        If foundRow.Length > 0 Then
            cbocategory.SelectedValue = foundRow(0).Item("id")
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
