Public Class StockInForm
    Public adding As Boolean = False
    Public updating As Boolean = False
    Public stockinid As Integer = Nothing

    Private Sub StockInForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect("localhost", "dbpharmacy", "3306", "root", "")
        fillSuppliers()
        fillProducts()
        fillstockinhistory()
        btnnew.Enabled = True
        btnsave.Enabled = False
        pnlinput.Enabled = False
    End Sub

    Public Sub fillstockinhistory()
        Dim search As String = txtsearch.Text.Trim()
        Dim query As String = "SELECT s.id, sup.name AS supplier, sup.address, p.productname, sd.quantity, s.transactiondate " & _
                              "FROM stockin s " & _
                              "JOIN supplier sup ON s.supplierid = sup.id " & _
                              "JOIN stockin_details sd ON s.id = sd.stockinid " & _
                              "JOIN product p ON sd.productid = p.productid "

        If search <> "" Then
            query &= "WHERE s.id LIKE '%" & search & "%' " & _
                     "OR sup.name LIKE '%" & search & "%' " & _
                     "OR sup.address LIKE '%" & search & "%' " & _
                     "OR p.productname LIKE '%" & search & "%' " & _
                     "OR sd.quantity LIKE '%" & search & "%' " & _
                     "OR s.transactiondate LIKE '%" & search & "%' "
        End If

        query &= "ORDER BY s.transactiondate DESC"

        GetQuery(query, "stockin_history")
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


    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If cmbsupplier.SelectedIndex = -1 Or cmbProduct.SelectedIndex = -1 Or numquantity.Text = "" Then
            MsgBox("Please complete all fields.", MsgBoxStyle.Information, "Missing Information")
            Exit Sub
        End If

        Dim supplierId = cmbsupplier.SelectedValue
        Dim productId = cmbProduct.SelectedValue
        Dim quantity = CInt(numquantity.Text)

        If adding Then
            If MsgBox("Are you sure you want to add this stock-in record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
                Try
                    SetQuery("INSERT INTO stockin (supplierid) VALUES ('" & supplierId & "')")
                    Dim stockInId = cmd.LastInsertedId

                    SetQuery("INSERT INTO stockin_details (stockinid, productid, quantity) VALUES ('" & stockInId & "', '" & productId & "', '" & quantity & "')")

                    SetQuery("UPDATE product SET stock = stock + " & quantity & " WHERE productid = " & productId)

                    fillstockinhistory()
                    disablebuttons()
                    clearfields()
                    MsgBox("Stock-In Record Added Successfully!", MsgBoxStyle.Information, "Success")
                    adding = False
                    pnlinput.Enabled = False
                Catch ex As Exception
                    MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
                End Try
            End If

        ElseIf updating Then
            If MsgBox("Are you sure you want to update this stock-in record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Update") = MsgBoxResult.Yes Then
                Try
                    GetQuery("SELECT quantity FROM stockin_details WHERE stockinid = " & stockinid & " AND productid = " & productId, "current_quantity")
                    Dim currentQuantity As Integer = CInt(ds.Tables("current_quantity").Rows(0)("quantity"))

                    SetQuery("UPDATE stockin SET supplierid = " & supplierId & " WHERE id = " & stockinid)

                    SetQuery("UPDATE stockin_details SET quantity = " & quantity & " WHERE stockinid = " & stockinid & " AND productid = " & productId)

                    Dim quantityDiff As Integer = quantity - currentQuantity
                    SetQuery("UPDATE product SET stock = stock + " & quantityDiff & " WHERE productid = " & productId)

                    fillstockinhistory()
                    disablebuttons()
                    clearfields()
                    MsgBox("Stock-In Record Updated Successfully!", MsgBoxStyle.Information, "Success")
                    updating = False
                    pnlinput.Enabled = False
                Catch ex As Exception
                    MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
                End Try
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
                MsgBox("Updating operation cancelled.", MsgBoxStyle.Information, "Cancelled")
            End If

        Else
            clearfields()
            disablebuttons()
            pnlinput.Enabled = False
            updating = False
            adding = False
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

        If MsgBox("Are you sure you want to delete this stock-in record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Delete") = MsgBoxResult.Yes Then
            SetQuery("DELETE FROM stockin_details WHERE stockinid = " & stockinid)
            SetQuery("DELETE FROM stockin WHERE id = " & stockinid)
            fillstockinhistory()
            clearfields()
            MsgBox("Stock-In Record Deleted Successfully!", MsgBoxStyle.Information, "Success")
        End If
    End Sub


    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        enablebuttons()
        updating = True
        pnlinput.Enabled = True
    End Sub

    Private Sub liststockin_DoubleClick(sender As Object, e As EventArgs) Handles liststockin.DoubleClick
        If liststockin.SelectedItems.Count > 0 Then
            stockinid = CInt(liststockin.FocusedItem.SubItems(0).Text)
            txtid.Text = stockinid

            GetQuery("SELECT s.id, s.supplierid, sd.productid, sd.quantity " & _
                     "FROM stockin s " & _
                     "JOIN stockin_details sd ON s.id = sd.stockinid " & _
                     "WHERE s.id = " & stockinid, "stockin_details")

            cmbsupplier.SelectedValue = ds.Tables("stockin_details").Rows(0).Item("supplierid").ToString()
            cmbProduct.SelectedValue = ds.Tables("stockin_details").Rows(0).Item("productid").ToString()
            numquantity.Text = ds.Tables("stockin_details").Rows(0).Item("quantity").ToString()

            btnupdate.Enabled = True
            btndelete.Enabled = True
        End If
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
        adding = True
        pnlinput.Enabled = True
    End Sub
End Class
