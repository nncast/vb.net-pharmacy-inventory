Public Class StockInForm
    Public adding As Boolean = False
    Public updating As Boolean = False
    Public stockinid As Integer = Nothing

    Private Sub StockInForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        ' The designer left the default limit of 100 units per stock-in.
        numquantity.Maximum = 1000000
        fillSuppliers()
        fillProducts()
        fillstockinhistory()
        btnnew.Enabled = True
        btnsave.Enabled = False
        pnlinput.Enabled = False
    End Sub

    Public Sub fillstockinhistory()
        Dim search As String = txtsearch.Text.Trim()
        Dim query As String = "SELECT s.id, sup.name AS supplier, sup.address, p.productname, sd.quantity, s.transactiondate " &
                              "FROM stockin s " &
                              "JOIN supplier sup ON s.supplierid = sup.id " &
                              "JOIN stockin_details sd ON s.id = sd.stockinid " &
                              "JOIN product p ON sd.productid = p.productid "

        If search <> "" Then
            query &= "WHERE s.id LIKE @s OR sup.name LIKE @s OR sup.address LIKE @s OR p.productname LIKE @s OR sd.quantity LIKE @s OR s.transactiondate LIKE @s "
        End If

        query &= "ORDER BY s.transactiondate DESC"

        GetQuery(query, "stockin_history", P("@s", "%" & search & "%"))
        liststockin.Items.Clear()

        For Each row As DataRow In ds.Tables("stockin_history").Rows
            Dim item = liststockin.Items.Add(row("id").ToString())
            With item.SubItems
                .Add(row("supplier").ToString())
                .Add(row("address").ToString())
                .Add(row("productname").ToString())
                .Add(row("quantity").ToString())
                .Add(Convert.ToDateTime(row("transactiondate")).ToString("yyyy-MM-dd HH:mm:ss"))
            End With
        Next
    End Sub


    Public Sub fillSuppliers()
        GetQuery("SELECT id, name FROM supplier ORDER BY name ASC", "supplier")
        Dim dtSupplier As DataTable = ds.Tables("supplier").Copy()
        cmbsupplier.DataSource = dtSupplier
        cmbsupplier.DisplayMember = "name"
        cmbsupplier.ValueMember = "id"
        cmbsupplier.SelectedIndex = -1
    End Sub


    Public Sub fillProducts()
        GetQuery("SELECT productid, productname FROM product ORDER BY productname ASC", "product")
        Dim dtProduct As DataTable = ds.Tables("product").Copy()
        cmbProduct.DataSource = dtProduct
        cmbProduct.DisplayMember = "productname"
        cmbProduct.ValueMember = "productid"
        cmbProduct.SelectedIndex = -1
    End Sub

    ' Current stock of a product (call inside the transaction so the value is current).
    Private Function StockOf(productId As Integer) As Integer
        Return CInt(GetValue("SELECT stock FROM product WHERE productid = @p", P("@p", productId)))
    End Function

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If cmbsupplier.SelectedIndex = -1 Or cmbProduct.SelectedIndex = -1 Then
            MsgBox("Please complete all fields.", MsgBoxStyle.Information, "Missing Information")
            Exit Sub
        End If

        If numquantity.Value <= 0 Then
            MsgBox("Quantity must be greater than zero.", MsgBoxStyle.Information, "Missing Information")
            Exit Sub
        End If

        Dim supplierId As Integer = CInt(cmbsupplier.SelectedValue)
        Dim productId As Integer = CInt(cmbProduct.SelectedValue)
        Dim quantity As Integer = CInt(numquantity.Value)

        If adding Then
            If MsgBox("Are you sure you want to add this stock-in record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
                Try
                    BeginTransaction()
                    Execute("INSERT INTO stockin (supplierid) VALUES (@s)", P("@s", supplierId))
                    Dim stockInId As Integer = GetLastInsertedID()
                    Execute("INSERT INTO stockin_details (stockinid, productid, quantity) VALUES (@si, @p, @q)", P("@si", stockInId), P("@p", productId), P("@q", quantity))
                    Execute("UPDATE product SET stock = stock + @q WHERE productid = @p", P("@q", quantity), P("@p", productId))
                    CommitTransaction()
                Catch ex As Exception
                    RollbackTransaction()
                    MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End Try

                fillstockinhistory()
                disablebuttons()
                clearfields()
                MsgBox("Stock-In Record Added Successfully!", MsgBoxStyle.Information, "Success")
                adding = False
                pnlinput.Enabled = False
            End If

        ElseIf updating Then
            If MsgBox("Are you sure you want to update this stock-in record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Update") = MsgBoxResult.Yes Then
                Try
                    BeginTransaction()
                    GetQuery("SELECT productid, quantity FROM stockin_details WHERE stockinid = @si", "current_detail", P("@si", stockinid))
                    If ds.Tables("current_detail").Rows.Count = 0 Then Throw New InvalidOperationException("This stock-in record no longer exists.")

                    Dim oldProductId As Integer = CInt(ds.Tables("current_detail").Rows(0)("productid"))
                    Dim oldQuantity As Integer = CInt(ds.Tables("current_detail").Rows(0)("quantity"))

                    ' Undo the old quantity, then add the new one (possibly to a different product).
                    If StockOf(oldProductId) - oldQuantity + If(oldProductId = productId, quantity, 0) < 0 Then
                        Throw New InvalidOperationException("Some of the stock from this record has already been used, so it can't be reduced that much.")
                    End If

                    Execute("UPDATE product SET stock = stock - @q WHERE productid = @p", P("@q", oldQuantity), P("@p", oldProductId))
                    Execute("UPDATE product SET stock = stock + @q WHERE productid = @p", P("@q", quantity), P("@p", productId))
                    Execute("UPDATE stockin SET supplierid = @s WHERE id = @si", P("@s", supplierId), P("@si", stockinid))
                    Execute("UPDATE stockin_details SET productid = @p, quantity = @q WHERE stockinid = @si", P("@p", productId), P("@q", quantity), P("@si", stockinid))
                    CommitTransaction()
                Catch ex As Exception
                    RollbackTransaction()
                    MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End Try

                fillstockinhistory()
                disablebuttons()
                clearfields()
                MsgBox("Stock-In Record Updated Successfully!", MsgBoxStyle.Information, "Success")
                updating = False
                stockinid = Nothing
                pnlinput.Enabled = False
            End If
        End If

    End Sub


    Private Sub txtsearch_GotFocus(sender As Object, e As EventArgs) Handles txtsearch.GotFocus
        HandleFocus(shapesearch, True)
    End Sub

    Private Sub txtsearch_LostFocus(sender As Object, e As EventArgs) Handles txtsearch.LostFocus
        HandleFocus(shapesearch, False)
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        fillstockinhistory()
    End Sub

    Private Sub cmbProduct_GotFocus(sender As Object, e As EventArgs) Handles cmbProduct.GotFocus
        HandleFocus(shapeproduct, True)
    End Sub

    Private Sub cmbProduct_LostFocus(sender As Object, e As EventArgs) Handles cmbProduct.LostFocus
        HandleFocus(shapeproduct, False)
    End Sub

    Private Sub cmbsupplier_GotFocus(sender As Object, e As EventArgs) Handles cmbsupplier.GotFocus
        HandleFocus(shapesupplier, True)
    End Sub

    Private Sub cmbsupplier_LostFocus(sender As Object, e As EventArgs) Handles cmbsupplier.LostFocus
        HandleFocus(shapesupplier, False)
    End Sub

    Private Sub numquantity_GotFocus(sender As Object, e As EventArgs) Handles numquantity.GotFocus
        HandleFocus(shapequantity, True)
    End Sub

    Private Sub numquantity_LostFocus(sender As Object, e As EventArgs) Handles numquantity.LostFocus
        HandleFocus(shapequantity, False)
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        If adding Then
            If MsgBox("Are you sure you want to cancel adding a new stock-in record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Cancel") = MsgBoxResult.Yes Then
                clearfields()
                disablebuttons()
                pnlinput.Enabled = False
                adding = False
                MsgBox("Adding operation cancelled.", MsgBoxStyle.Information, "Cancelled")
            End If

        ElseIf updating Then
            If MsgBox("Are you sure you want to cancel updating the stock-in record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Cancel") = MsgBoxResult.Yes Then
                clearfields()
                disablebuttons()
                pnlinput.Enabled = False
                updating = False
                stockinid = Nothing
                MsgBox("Updating operation cancelled.", MsgBoxStyle.Information, "Cancelled")
            End If

        Else
            clearfields()
            disablebuttons()
            pnlinput.Enabled = False
            updating = False
            adding = False
            stockinid = Nothing
        End If
    End Sub

    Public Sub clearfields()
        cmbProduct.SelectedIndex = -1
        cmbsupplier.SelectedIndex = -1
        numquantity.Value = 0
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If stockinid = Nothing Then
            MsgBox("Please select a stock-in record to delete.", MsgBoxStyle.Information, "No Selection")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to delete this stock-in record? Its quantity will be removed from the product's stock.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Delete") = MsgBoxResult.Yes Then
            Try
                BeginTransaction()
                GetQuery("SELECT productid, quantity FROM stockin_details WHERE stockinid = @si", "delete_detail", P("@si", stockinid))
                For Each row As DataRow In ds.Tables("delete_detail").Rows
                    Dim productId As Integer = CInt(row("productid"))
                    Dim quantity As Integer = CInt(row("quantity"))
                    If StockOf(productId) < quantity Then
                        Throw New InvalidOperationException("Some of this stock has already been used, so the record can't be deleted.")
                    End If
                    Execute("UPDATE product SET stock = stock - @q WHERE productid = @p", P("@q", quantity), P("@p", productId))
                Next
                Execute("DELETE FROM stockin_details WHERE stockinid = @si", P("@si", stockinid))
                Execute("DELETE FROM stockin WHERE id = @si", P("@si", stockinid))
                CommitTransaction()
            Catch ex As Exception
                RollbackTransaction()
                MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
                Exit Sub
            End Try

            fillstockinhistory()
            clearfields()
            stockinid = Nothing
            MsgBox("Stock-In Record Deleted Successfully!", MsgBoxStyle.Information, "Success")
        End If
    End Sub


    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        If stockinid = Nothing Then
            MsgBox("Please select a stock-in record to update.", MsgBoxStyle.Information, "No Selection")
            Exit Sub
        End If

        enablebuttons()
        updating = True
        pnlinput.Enabled = True
    End Sub

    Private Sub liststockin_DoubleClick(sender As Object, e As EventArgs) Handles liststockin.DoubleClick
        If adding Or updating Or liststockin.SelectedItems.Count = 0 Then Exit Sub

        stockinid = CInt(liststockin.SelectedItems(0).SubItems(0).Text)
        txtid.Text = stockinid

        GetQuery("SELECT s.id, s.supplierid, sd.productid, sd.quantity " &
                 "FROM stockin s " &
                 "JOIN stockin_details sd ON s.id = sd.stockinid " &
                 "WHERE s.id = @si", "stockin_details", P("@si", stockinid))
        If ds.Tables("stockin_details").Rows.Count = 0 Then Exit Sub

        cmbsupplier.SelectedValue = ds.Tables("stockin_details").Rows(0).Item("supplierid")
        cmbProduct.SelectedValue = ds.Tables("stockin_details").Rows(0).Item("productid")
        numquantity.Value = CInt(ds.Tables("stockin_details").Rows(0).Item("quantity"))

        btnupdate.Enabled = True
        btndelete.Enabled = True
    End Sub



    Public Sub disablebuttons()
        btnnew.Enabled = 1
        btnupdate.Enabled = 1
        btndelete.Enabled = 1
        btncancel.Enabled = 1
        btnsave.Enabled = 0
    End Sub

    Public Sub enablebuttons()
        btnnew.Enabled = 0
        btnupdate.Enabled = 0
        btndelete.Enabled = 0
        btncancel.Enabled = 1
        btnsave.Enabled = 1
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        enablebuttons()
        clearfields()
        stockinid = Nothing
        adding = True
        pnlinput.Enabled = True
    End Sub
End Class
