Public Class OrderForm
    Public orderid As Integer = Nothing
    Public adding As Boolean = False
    Public updating As Boolean = False

    Private Sub OrderForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect("localhost", "dbpharmacy", "3306", "root", "")

        fillOrderHistory()
        disablebuttons()
        pnlinput.Enabled = False
    End Sub

    Private dtCustomers As New DataTable()
    Private dtProducts As New DataTable()

    Private Sub fillCustomers()
        GetQuery("SELECT id, name FROM customer", "customer")
        dtCustomers = ds.Tables("customer").Copy()
        cmbcustomer.DataSource = dtCustomers
        cmbcustomer.DisplayMember = "name"
        cmbcustomer.ValueMember = "id"
    End Sub

    Private Sub fillProducts()
        GetQuery("SELECT productid, productname, price, stock FROM product", "product")
        dtProducts = ds.Tables("product").Copy()
        cmbproduct.DataSource = dtProducts
        cmbproduct.DisplayMember = "productname"
        cmbproduct.ValueMember = "productid"
    End Sub

    Public Sub fillOrderHistory()
        Dim search As String = txtSearch.Text.Trim()
        Dim query As String = "SELECT o.id, c.name, o.orderdate " & _
                              "FROM orders o LEFT JOIN customer c ON o.customerid = c.id "

        If search <> "" Then
            query &= "WHERE o.id LIKE '%" & search & "%' " & _
                     "OR c.name LIKE '%" & search & "%' " & _
                     "OR o.orderdate LIKE '%" & search & "%' "
        End If

        query &= "ORDER BY o.orderdate DESC"

        GetQuery(query, "orders")
        lvOrderHistory.Items.Clear()

        For Each row As DataRow In ds.Tables("orders").Rows
            With lvOrderHistory.Items.Add(row("id").ToString())
                .SubItems.Add(row("name").ToString())
                .SubItems.Add(CDate(row("orderdate")).ToString())
            End With
        Next
    End Sub

    Public Sub clearfields()
        cmbcustomer.SelectedIndex = -1
        cmbproduct.SelectedIndex = -1
        numquantity.Value = 0
        lvcart.Items.Clear()
        orderid = Nothing
        adding = False
        updating = False
    End Sub

    Public Sub enablebuttons()
        btnnew.Enabled = 0
        btnupdate.Enabled = 0
        btndelete.Enabled = 0
        btncancel.Enabled = 1
        btnsave.Enabled = 1
    End Sub

    Public Sub disablebuttons()
        btnnew.Enabled = 1
        btnupdate.Enabled = 1
        btndelete.Enabled = 1
        btncancel.Enabled = 1
        btnsave.Enabled = 0
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        lbltotal.Text = "00.00"
        enablebuttons()
        clearfields()
        adding = True
        pnlinput.Enabled = True
        fillCustomers()
        fillProducts()
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        enablebuttons()
        updating = True
        pnlinput.Enabled = True
    End Sub

    Private Sub btnAddToCart_Click(sender As Object, e As EventArgs) Handles btnaddtocart.Click
        If cmbcustomer.SelectedIndex = -1 Then
            MsgBox("Please select a customer.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        If cmbproduct.SelectedIndex = -1 Then
            MsgBox("Please select a product.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        If numquantity.Value <= 0 Then
            MsgBox("Please enter a quantity greater than zero.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        Dim selectedProduct = cmbproduct.SelectedItem
        Dim productName = selectedProduct("productname").ToString()
        Dim productId = CInt(selectedProduct("productid"))
        Dim price = CDec(selectedProduct("price"))
        Dim quantity = CInt(numquantity.Text.Trim())
        Dim total = price * quantity

        If MsgBox("Are you sure you want to add this item to the cart?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
            Dim item = lvcart.Items.Add(productName)
            item.SubItems.Add(quantity.ToString())
            item.SubItems.Add(price.ToString("F2"))
            item.SubItems.Add(total.ToString("F2"))
            item.SubItems.Add(productId.ToString())

            UpdateTotal()
        End If
    End Sub

    Private Sub UpdateTotal()
        Dim totalPrice As Decimal = 0
        For Each item As ListViewItem In lvcart.Items
            totalPrice += CDec(item.SubItems(3).Text)
        Next
        lbltotal.Text = totalPrice.ToString("F2")
    End Sub


    Private Sub btnRemoveFromCart_Click(sender As Object, e As EventArgs) Handles btnremovefromcart.Click
        If lvCart.SelectedItems.Count = 0 Then
            MsgBox("Please select an item to remove.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to remove this item from the cart?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
            Dim productId As Integer = CInt(lvCart.SelectedItems(0).SubItems(4).Text)
            If updating Then
                SetQuery("DELETE FROM orderdetails WHERE orderid = " & orderid & " AND productid = " & productId)
            End If
            lvCart.Items.Remove(lvCart.SelectedItems(0))
            UpdateTotal()
        End If
    End Sub



    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If lvcart.Items.Count = 0 Then
            MsgBox("Your cart is empty. Please add items before saving the order.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        Dim customerId = CInt(cmbcustomer.SelectedValue)

        If adding Then
            If MsgBox("Are you sure you want to add a new order?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
                SetQuery("INSERT INTO orders (customerid) VALUES (" & customerId & ")")
                GetQuery("SELECT LAST_INSERT_ID() AS last_id", "last_id")
                orderid = CInt(ds.Tables("last_id").Rows(0).Item("last_id"))

                For Each item As ListViewItem In lvCart.Items
                    Dim productId = CInt(item.SubItems(4).Text)
                    Dim quantity = CInt(item.SubItems(1).Text)
                    Dim price = CDec(item.SubItems(2).Text)
                    SetQuery("INSERT INTO orderdetails (orderid, productid, quantity, price) VALUES (" & orderid & ", " & productId & ", " & quantity & ", " & price & ")")
                Next

                MsgBox("Order added successfully!", MsgBoxStyle.Information, "Success")
            End If

        ElseIf updating Then
            If MsgBox("Are you sure you want to update this order?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then

                SetQuery("UPDATE orders SET customerid = " & customerId & " WHERE id = " & orderid)

                GetQuery("SELECT productid FROM orderdetails WHERE orderid = " & orderid, "existingItems")
                Dim existingItems As New List(Of Integer)
                For Each row As DataRow In ds.Tables("existingItems").Rows
                    existingItems.Add(CInt(row("productid")))
                Next

                For Each item As ListViewItem In lvcart.Items
                    Dim productId As Integer = CInt(item.SubItems(4).Text)
                    Dim quantity As Integer = CInt(item.SubItems(1).Text)
                    Dim price As Decimal = CDec(item.SubItems(2).Text)

                    If existingItems.Contains(productId) Then
                        SetQuery("UPDATE orderdetails SET quantity = " & quantity & ", price = " & price &
                                 " WHERE orderid = " & orderid & " AND productid = " & productId)
                        existingItems.Remove(productId)
                    Else
                        SetQuery("INSERT INTO orderdetails (orderid, productid, quantity, price) VALUES (" &
                                  orderid & ", " & productId & ", " & quantity & ", " & price & ")")
                    End If
                Next

                For Each productId As Integer In existingItems
                    SetQuery("DELETE FROM orderdetails WHERE orderid = " & orderid & " AND productid = " & productId)
                Next

                MsgBox("Order updated successfully!", MsgBoxStyle.Information, "Success")
            End If
        End If

        lbltotal.Text = "00.00"
        fillOrderHistory()
        disablebuttons()
        clearfields()
        pnlinput.Enabled = False
        updating = False
        adding = False
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Dim message As String

        If adding Then
            message = "Are you sure you want to cancel adding this order? All unsaved changes will be lost."
        ElseIf updating Then
            message = "Are you sure you want to cancel updating this order? All unsaved changes will be lost."
        Else
            message = "Are you sure you want to cancel? All unsaved changes will be lost."
        End If

        If MsgBox(message, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Cancel") = MsgBoxResult.Yes Then
            clearfields()
            disablebuttons()
            pnlinput.Enabled = False
            updating = False
            adding = False
            lbltotal.Text = "00.00"
        End If
    End Sub

    

    Private Sub lvOrderHistory_DoubleClick(sender As Object, e As EventArgs) Handles lvOrderHistory.DoubleClick
        orderid = CInt(lvOrderHistory.FocusedItem.SubItems(0).Text)

        GetQuery("SELECT o.id, o.customerid, od.productid, od.quantity, od.price, p.productname, p.productid " &
                 "FROM orders o " &
                 "INNER JOIN orderdetails od ON o.id = od.orderid " &
                 "INNER JOIN product p ON od.productid = p.productid " &
                 "WHERE o.id = " & orderid, "orderdetails")

        Dim customerid As String = ds.Tables("orderdetails").Rows(0).Item("customerid").ToString()
        Dim productid As String = ds.Tables("orderdetails").Rows(0).Item("productid").ToString()
        Dim quantity As Integer = CInt(ds.Tables("orderdetails").Rows(0).Item("quantity"))

        lvcart.Items.Clear()
        Dim totalPrice As Decimal = 0

        For i = 0 To ds.Tables("orderdetails").Rows.Count - 1
            Dim quantityItem = CInt(ds.Tables("orderdetails").Rows(i).Item("quantity"))
            Dim priceItem = CDec(ds.Tables("orderdetails").Rows(i).Item("price"))
            Dim itemTotal = quantityItem * priceItem
            totalPrice += itemTotal

            Dim item = lvcart.Items.Add(ds.Tables("orderdetails").Rows(i).Item("productname").ToString())
            item.SubItems.Add(quantityItem.ToString())
            item.SubItems.Add(priceItem.ToString("F2"))
            item.SubItems.Add(itemTotal.ToString("F2"))
            item.SubItems.Add(ds.Tables("orderdetails").Rows(i).Item("productid").ToString())

        Next

        fillCustomers()
        fillProducts()

        cmbcustomer.SelectedValue = customerid
        cmbproduct.SelectedValue = productid

        lbltotal.Text = totalPrice.ToString("F2")


        numquantity.Value = quantity

        btnupdate.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub cmbproduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbproduct.SelectedIndexChanged
        If cmbproduct.SelectedIndex >= 0 Then
            Dim selectedProduct = cmbproduct.SelectedItem
            Dim stock = CInt(selectedProduct("stock"))
            numquantity.Maximum = stock
        Else
            numquantity.Maximum = 0
        End If
    End Sub
    Private Sub cmbcustomer_GotFocus(sender As Object, e As EventArgs) Handles cmbcustomer.GotFocus
        HandleFocus(shapecustomer, True)
    End Sub

    Private Sub cmbcustomer_LostFocus(sender As Object, e As EventArgs) Handles cmbcustomer.LostFocus
        HandleFocus(shapecustomer, False)
    End Sub

    Private Sub cmbproduct_GotFocus(sender As Object, e As EventArgs) Handles cmbproduct.GotFocus
        HandleFocus(shapeproduct, True)
    End Sub

    Private Sub cmbproduct_LostFocus(sender As Object, e As EventArgs) Handles cmbproduct.LostFocus
        HandleFocus(shapeproduct, False)
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        fillOrderHistory()
    End Sub

    Private Sub txtsearch_GotFocus(sender As Object, e As EventArgs) Handles txtsearch.GotFocus
        HandleFocus(shapesearch, True)
    End Sub

    Private Sub txtsearch_LostFocus(sender As Object, e As EventArgs) Handles txtsearch.LostFocus
        HandleFocus(shapesearch, False)
    End Sub
    Private Sub numquantity_GotFocus(sender As Object, e As EventArgs) Handles numquantity.GotFocus
        HandleFocus(shapequantity, True)
    End Sub

    Private Sub numquantity_LostFocus(sender As Object, e As EventArgs) Handles numquantity.LostFocus
        HandleFocus(shapequantity, False)
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If orderid = Nothing Then
            MsgBox("Please select an order to delete.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        Dim confirmation As DialogResult = MsgBox("Are you sure you want to delete this order?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Deletion")

        If confirmation = DialogResult.Yes Then
            Try
                SetQuery("DELETE FROM orderdetails WHERE orderid = " & orderid)

                SetQuery("DELETE FROM orders WHERE id = " & orderid)

                MsgBox("Order deleted successfully!", MsgBoxStyle.Information, "Success")

                fillOrderHistory()
                clearfields()
                disablebuttons()
                pnlinput.Enabled = False
                orderid = Nothing
            Catch ex As Exception
                MsgBox("An error occurred while deleting the order: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub
End Class
