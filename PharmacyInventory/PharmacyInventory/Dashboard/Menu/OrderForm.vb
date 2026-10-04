Public Class OrderForm
    Public orderid As Integer = Nothing
    Public adding As Boolean = False
    Public updating As Boolean = False

    Private Sub OrderForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()

        fillOrderHistory()
        disablebuttons()
        pnlinput.Enabled = False
    End Sub

    Private dtCustomers As New DataTable()
    Private dtProducts As New DataTable()

    Private Sub fillCustomers()
        GetQuery("SELECT id, name FROM customer ORDER BY name", "customer")
        dtCustomers = ds.Tables("customer").Copy()
        cmbcustomer.DataSource = dtCustomers
        cmbcustomer.DisplayMember = "name"
        cmbcustomer.ValueMember = "id"
    End Sub

    Private Sub fillProducts()
        GetQuery("SELECT productid, productname, price, stock FROM product ORDER BY productname", "product")
        dtProducts = ds.Tables("product").Copy()
        cmbproduct.DataSource = dtProducts
        cmbproduct.DisplayMember = "productname"
        cmbproduct.ValueMember = "productid"
    End Sub

    Public Sub fillOrderHistory()
        Dim search As String = txtSearch.Text.Trim()
        Dim query As String = "SELECT o.id, c.name, o.orderdate " &
                              "FROM orders o LEFT JOIN customer c ON o.customerid = c.id "

        If search <> "" Then
            query &= "WHERE o.id LIKE @s OR c.name LIKE @s OR o.orderdate LIKE @s "
        End If

        query &= "ORDER BY o.orderdate DESC"

        GetQuery(query, "orders", P("@s", "%" & search & "%"))
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

    ' Stock leaves the shelf when an order is delivered, so delivered orders can't be changed.
    Private Function IsDelivered(id As Integer) As Boolean
        Return CInt(GetValue("SELECT COUNT(*) FROM customer_delivery WHERE orderid = @o AND status = 'Delivered'", P("@o", id))) > 0
    End Function

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        lbltotal.Text = "00.00"
        enablebuttons()
        clearfields()
        adding = True
        pnlinput.Enabled = True
        fillCustomers()
        fillProducts()
        cmbcustomer.SelectedIndex = -1
        cmbproduct.SelectedIndex = -1
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        If orderid = Nothing Then
            MsgBox("Please select an order to update.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        If IsDelivered(orderid) Then
            MsgBox("This order has already been delivered, so its items can't be changed.", MsgBoxStyle.Exclamation, "Order Delivered")
            Exit Sub
        End If

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
        Dim stock = CInt(selectedProduct("stock"))
        Dim quantity = CInt(numquantity.Value)

        ' Adding a product that is already in the cart increases that line instead of duplicating it.
        Dim existing As ListViewItem = Nothing
        For Each item As ListViewItem In lvcart.Items
            If CInt(item.SubItems(4).Text) = productId Then existing = item
        Next

        Dim newQuantity As Integer = quantity + If(existing Is Nothing, 0, CInt(existing.SubItems(1).Text))
        If newQuantity > stock Then
            MsgBox("Only " & stock & " in stock for " & productName & ".", MsgBoxStyle.Information, "Not Enough Stock")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to add this item to the cart?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
            If existing IsNot Nothing Then
                Dim existingPrice As Decimal = CDec(existing.SubItems(2).Text)
                existing.SubItems(1).Text = newQuantity.ToString()
                existing.SubItems(3).Text = (existingPrice * newQuantity).ToString("F2")
            Else
                Dim item = lvcart.Items.Add(productName)
                item.SubItems.Add(quantity.ToString())
                item.SubItems.Add(price.ToString("F2"))
                item.SubItems.Add((price * quantity).ToString("F2"))
                item.SubItems.Add(productId.ToString())
            End If

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

        ' Only the cart changes here; the order itself is updated when it is saved,
        ' so Cancel still discards the removal.
        If MsgBox("Are you sure you want to remove this item from the cart?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
            lvCart.Items.Remove(lvCart.SelectedItems(0))
            UpdateTotal()
        End If
    End Sub



    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If Not (adding Or updating) Then Exit Sub

        If lvcart.Items.Count = 0 Then
            MsgBox("Your cart is empty. Please add items before saving the order.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        If cmbcustomer.SelectedIndex = -1 OrElse cmbcustomer.SelectedValue Is Nothing Then
            MsgBox("Please select a customer.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        Dim customerId = CInt(cmbcustomer.SelectedValue)
        Dim question As String = If(adding, "Are you sure you want to add a new order?", "Are you sure you want to update this order?")
        If MsgBox(question, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") <> MsgBoxResult.Yes Then Exit Sub

        Dim wasAdding As Boolean = adding
        Try
            BeginTransaction()
            If adding Then
                Execute("INSERT INTO orders (customerid) VALUES (@c)", P("@c", customerId))
                orderid = GetLastInsertedID()
            Else
                Execute("UPDATE orders SET customerid = @c WHERE id = @o", P("@c", customerId), P("@o", orderid))
                Execute("DELETE FROM orderdetails WHERE orderid = @o", P("@o", orderid))
            End If

            For Each item As ListViewItem In lvcart.Items
                Execute("INSERT INTO orderdetails (orderid, productid, quantity, price) VALUES (@o, @p, @q, @price)",
                        P("@o", orderid), P("@p", CInt(item.SubItems(4).Text)), P("@q", CInt(item.SubItems(1).Text)), P("@price", CDec(item.SubItems(2).Text)))
            Next
            CommitTransaction()
        Catch ex As Exception
            RollbackTransaction()
            MsgBox("Could not save the order: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try

        MsgBox(If(wasAdding, "Order added successfully!", "Order updated successfully!"), MsgBoxStyle.Information, "Success")

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
        If adding Or updating Or lvOrderHistory.SelectedItems.Count = 0 Then Exit Sub

        orderid = CInt(lvOrderHistory.SelectedItems(0).SubItems(0).Text)

        GetQuery("SELECT o.id, o.customerid, od.productid, od.quantity, od.price, p.productname " &
                 "FROM orders o " &
                 "INNER JOIN orderdetails od ON o.id = od.orderid " &
                 "INNER JOIN product p ON od.productid = p.productid " &
                 "WHERE o.id = @o", "orderdetails", P("@o", orderid))

        fillCustomers()
        fillProducts()
        lvcart.Items.Clear()

        If ds.Tables("orderdetails").Rows.Count = 0 Then
            ' An order without items: still show its customer so it can be fixed or deleted.
            cmbcustomer.SelectedValue = GetValue("SELECT customerid FROM orders WHERE id = @o", P("@o", orderid))
            lbltotal.Text = "00.00"
        Else
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

            cmbcustomer.SelectedValue = ds.Tables("orderdetails").Rows(0).Item("customerid")
            cmbproduct.SelectedValue = ds.Tables("orderdetails").Rows(0).Item("productid")
            lbltotal.Text = totalPrice.ToString("F2")
            numquantity.Value = Math.Min(numquantity.Maximum, CInt(ds.Tables("orderdetails").Rows(0).Item("quantity")))
        End If

        btnupdate.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub cmbproduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbproduct.SelectedIndexChanged
        If cmbproduct.SelectedIndex >= 0 Then
            Dim selectedProduct = cmbproduct.SelectedItem
            Dim stock = CInt(selectedProduct("stock"))
            numquantity.Maximum = Math.Max(0, stock)
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

        If IsDelivered(orderid) Then
            MsgBox("This order has already been delivered. Delete its delivery first (that returns the stock), then delete the order.", MsgBoxStyle.Exclamation, "Order Delivered")
            Exit Sub
        End If

        Dim question As String = "Are you sure you want to delete this order?"
        If CInt(GetValue("SELECT COUNT(*) FROM customer_delivery WHERE orderid = @o", P("@o", orderid))) > 0 Then
            question &= vbCrLf & "Its pending delivery will also be removed."
        End If

        If MsgBox(question, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Deletion") = MsgBoxResult.Yes Then
            Try
                BeginTransaction()
                Execute("DELETE FROM orderdetails WHERE orderid = @o", P("@o", orderid))
                ' order_driver and customer_delivery rows are removed by ON DELETE CASCADE.
                Execute("DELETE FROM orders WHERE id = @o", P("@o", orderid))
                CommitTransaction()
            Catch ex As Exception
                RollbackTransaction()
                MsgBox("An error occurred while deleting the order: " & ex.Message, MsgBoxStyle.Critical, "Error")
                Exit Sub
            End Try

            MsgBox("Order deleted successfully!", MsgBoxStyle.Information, "Success")

            fillOrderHistory()
            clearfields()
            disablebuttons()
            pnlinput.Enabled = False
            orderid = Nothing
        End If
    End Sub
End Class
