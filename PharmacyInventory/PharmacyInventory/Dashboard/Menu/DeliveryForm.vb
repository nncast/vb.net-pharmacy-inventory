Public Class DeliveryForm
    Public deliveryid As Integer = 0   ' customer_delivery.id of the selected history row
    Public orderid As Integer = 0      ' order picked from "Orders to Deliver"

    Private Sub DeliveryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        disableButtons()
        pnlinput.Enabled = False

        fillDrivers()
    End Sub

    Public Sub fill()
        fillDeliveryHistory()
        fillOrdersToDeliver()
    End Sub

    Public Sub fillOrdersToDeliver()
        GetQuery("SELECT o.id, c.name, o.orderdate FROM orders o " &
                 "INNER JOIN customer c ON o.customerid = c.id " &
                 "LEFT JOIN order_driver od ON o.id = od.orderid " &
                 "WHERE od.id IS NULL", "orders")

        lvOrdersToDeliver.Items.Clear()
        For Each row As DataRow In ds.Tables("orders").Rows
            Dim item = lvOrdersToDeliver.Items.Add(row("id").ToString())
            item.SubItems.Add(row("name").ToString())
            item.SubItems.Add(row("orderdate").ToString())
        Next
    End Sub

    Public Sub fillDrivers()
        GetQuery("SELECT id, name FROM driver", "driver")
        Dim dtDriver As DataTable = ds.Tables("driver").Copy()
        cmbDriver.DataSource = dtDriver
        cmbDriver.DisplayMember = "name"
        cmbDriver.ValueMember = "id"
        cmbDriver.SelectedIndex = -1
    End Sub

    Public Sub fillDeliveryHistory()
        Dim search As String = txtsearch.Text.Trim()
        Dim query As String = "SELECT cd.id, c.name AS customer_name, d.id AS driver_id, d.name AS driver_name, d.license, cd.status, cd.dateassigned " &
                              "FROM customer_delivery cd " &
                              "INNER JOIN customer c ON cd.customerid = c.id " &
                              "LEFT JOIN order_driver od ON cd.deliveryid = od.id " &
                              "LEFT JOIN driver d ON od.driverid = d.id"
        If search <> "" Then
            query &= " WHERE cd.id LIKE @s OR c.name LIKE @s OR d.name LIKE @s OR cd.status LIKE @s"
        End If

        GetQuery(query, "delivery", P("@s", "%" & search & "%"))

        lvDeliveryHistory.Items.Clear()
        For i = 0 To ds.Tables("delivery").Rows.Count - 1
            Dim row = ds.Tables("delivery").Rows(i)
            Dim item = lvDeliveryHistory.Items.Add(row("id").ToString())
            item.SubItems.Add(row("customer_name").ToString())
            item.SubItems.Add(If(IsDBNull(row("driver_name")), "N/A", row("driver_name").ToString()))
            item.SubItems.Add(If(IsDBNull(row("license")), "N/A", row("license").ToString())) ' License
            item.SubItems.Add(row("status").ToString())                     ' Status
            item.SubItems.Add(row("dateassigned").ToString())               ' Delivery Date
            item.Tag = If(IsDBNull(row("driver_id")), 0, row("driver_id"))  ' Store Driver ID in Tag
        Next
    End Sub

    Public Sub clearFields()
        cmbDriver.SelectedIndex = -1
        cmbStatus.SelectedIndex = -1
        txtorderid.Clear()
        orderid = 0
        deliveryid = 0
        disableButtons()
        pnlinput.Enabled = False
    End Sub

    Public Sub enableButtons()
        btnassign.Enabled = True
        btncancel.Enabled = True
        btnsave.Enabled = True
    End Sub

    Public Sub disableButtons()
        btnassign.Enabled = False
        btncancel.Enabled = False
        btnsave.Enabled = False
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        enableButtons()
        pnlinput.Enabled = True
    End Sub

    ' Puts back the stock of every stock-out recorded for a delivery and removes those records.
    Private Sub ReverseStockout(customerDeliveryId As Integer)
        GetQuery("SELECT sod.productid, sod.quantity FROM stockout so INNER JOIN stockout_details sod ON so.id = sod.stockoutid WHERE so.customer_delivery_id = @cd",
                 "reverse_stockout", P("@cd", customerDeliveryId))
        For Each row As DataRow In ds.Tables("reverse_stockout").Rows
            Execute("UPDATE product SET stock = stock + @q WHERE productid = @p", P("@q", CInt(row("quantity"))), P("@p", CInt(row("productid"))))
        Next
        Execute("DELETE FROM stockout WHERE customer_delivery_id = @cd", P("@cd", customerDeliveryId))
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If deliveryid = 0 Or cmbStatus.SelectedIndex = -1 Then
            MsgBox("Select a delivery and a status.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        If cmbDriver.SelectedIndex = -1 OrElse cmbDriver.SelectedValue Is Nothing Then
            MsgBox("Select a driver.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        GetQuery("SELECT status, orderid, deliveryid FROM customer_delivery WHERE id = @id", "current_delivery", P("@id", deliveryid))
        If ds.Tables("current_delivery").Rows.Count = 0 Then
            MsgBox("This delivery no longer exists.", MsgBoxStyle.Exclamation, "Not Found")
            fill()
            clearFields()
            Exit Sub
        End If

        Dim current As DataRow = ds.Tables("current_delivery").Rows(0)
        Dim oldStatus As String = current("status").ToString()
        Dim newStatus As String = cmbStatus.SelectedItem.ToString()
        Dim orderDriverId As Integer = CInt(current("deliveryid"))
        Dim deliveredOrderId As Integer = CInt(current("orderid"))
        Dim stockMessage As String = ""

        Try
            BeginTransaction()

            Execute("UPDATE customer_delivery SET status = @s WHERE id = @id", P("@s", newStatus), P("@id", deliveryid))
            ' customer_delivery.deliveryid points at the order_driver row; its id is not the delivery's id.
            Execute("UPDATE order_driver SET driverid = @d WHERE id = @od", P("@d", cmbDriver.SelectedValue), P("@od", orderDriverId))

            If newStatus = "Delivered" AndAlso oldStatus <> "Delivered" Then
                ' Stock only leaves once, when the delivery first becomes Delivered.
                GetQuery("SELECT od.productid, od.quantity, p.stock, p.productname FROM orderdetails od INNER JOIN product p ON od.productid = p.productid WHERE od.orderid = @o",
                         "orderdetails", P("@o", deliveredOrderId))

                For Each row As DataRow In ds.Tables("orderdetails").Rows
                    If CInt(row("quantity")) > CInt(row("stock")) Then
                        Throw New InvalidOperationException("Not enough stock for " & row("productname").ToString() & " (needs " & row("quantity").ToString() & ", has " & row("stock").ToString() & ").")
                    End If
                Next

                Execute("INSERT INTO stockout (customer_delivery_id, transactiondate) VALUES (@cd, NOW())", P("@cd", deliveryid))
                Dim stockoutId As Integer = GetLastInsertedID()

                For Each row As DataRow In ds.Tables("orderdetails").Rows
                    Dim productId As Integer = CInt(row("productid"))
                    Dim quantity As Integer = CInt(row("quantity"))
                    Execute("INSERT INTO stockout_details (stockoutid, productid, quantity) VALUES (@so, @p, @q)", P("@so", stockoutId), P("@p", productId), P("@q", quantity))
                    Execute("UPDATE product SET stock = stock - @q WHERE productid = @p", P("@q", quantity), P("@p", productId))
                Next
                stockMessage = "Stockout recorded successfully for delivered order!"

            ElseIf oldStatus = "Delivered" AndAlso newStatus <> "Delivered" Then
                ReverseStockout(deliveryid)
                stockMessage = "The delivery is no longer Delivered, so its stock was returned to inventory."
            End If

            CommitTransaction()
        Catch ex As Exception
            RollbackTransaction()
            MsgBox("Error updating delivery status or driver: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try

        If stockMessage <> "" Then MsgBox(stockMessage, MsgBoxStyle.Information, "Stock Updated")
        MsgBox("Delivery status updated successfully!", MsgBoxStyle.Information, "Success")
        clearFields()
        fillDeliveryHistory()
    End Sub

    Private Sub lvOrdersToDeliver_DoubleClick(sender As Object, e As EventArgs) Handles lvOrdersToDeliver.DoubleClick
        If lvOrdersToDeliver.SelectedItems.Count = 0 Then Exit Sub

        clearFields()
        orderid = CInt(lvOrdersToDeliver.SelectedItems(0).SubItems(0).Text)
        txtorderid.Text = orderid.ToString()
        pnlinput.Enabled = True
        enableButtons()
    End Sub

    Private Sub lvDeliveryHistory_DoubleClick(sender As Object, e As EventArgs) Handles lvDeliveryHistory.DoubleClick
        If lvDeliveryHistory.SelectedItems.Count = 0 Then Exit Sub

        clearFields()
        Dim item As ListViewItem = lvDeliveryHistory.SelectedItems(0)
        deliveryid = CInt(item.SubItems(0).Text)
        txtorderid.Text = deliveryid.ToString()

        Dim driverId = CInt(item.Tag)
        Dim status = item.SubItems(4).Text

        cmbDriver.SelectedValue = driverId
        cmbStatus.SelectedItem = status

        enableButtons()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearFields()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        fillDeliveryHistory()
    End Sub

    Private Sub txtorderid_GotFocus(sender As Object, e As EventArgs) Handles txtorderid.GotFocus
        HandleFocus(shapeid, True)
    End Sub
    Private Sub txtorderid_LostFocus(sender As Object, e As EventArgs) Handles txtorderid.LostFocus
        HandleFocus(shapeid, False)
    End Sub
    Private Sub cmbDriver_GotFocus(sender As Object, e As EventArgs) Handles cmbDriver.GotFocus
        HandleFocus(shapedriver, True)
    End Sub
    Private Sub cmbDriver_LostFocus(sender As Object, e As EventArgs) Handles cmbDriver.LostFocus
        HandleFocus(shapedriver, False)
    End Sub
    Private Sub cmbStatus_GotFocus(sender As Object, e As EventArgs) Handles cmbStatus.GotFocus
        HandleFocus(shapestatus, True)
    End Sub
    Private Sub cmbStatus_LostFocus(sender As Object, e As EventArgs) Handles cmbStatus.LostFocus
        HandleFocus(shapestatus, False)
    End Sub

    Private Sub txtsearch_GotFocus(sender As Object, e As EventArgs) Handles txtsearch.GotFocus
        HandleFocus(shapesearch, True)
    End Sub
    Private Sub txtsearch_LostFocus(sender As Object, e As EventArgs) Handles txtsearch.LostFocus
        HandleFocus(shapesearch, False)
    End Sub

    Private Sub btnassign_Click(sender As Object, e As EventArgs) Handles btnassign.Click
        If orderid = 0 Or cmbDriver.SelectedIndex = -1 Then
            MsgBox("Please select an order and a driver before assigning.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        If CInt(GetValue("SELECT COUNT(*) FROM order_driver WHERE orderid = @o", P("@o", orderid))) > 0 Then
            MsgBox("This order already has a driver assigned.", MsgBoxStyle.Information, "Already Assigned")
            fillOrdersToDeliver()
            clearFields()
            Exit Sub
        End If

        Try
            BeginTransaction()
            Execute("INSERT INTO order_driver (orderid, driverid) VALUES (@o, @d)", P("@o", orderid), P("@d", cmbDriver.SelectedValue))
            Dim orderDriverId As Integer = GetLastInsertedID()
            Execute("INSERT INTO customer_delivery (customerid, orderid, deliveryid, status) " &
                    "VALUES ((SELECT customerid FROM orders WHERE id = @o), @o, @od, 'Pending')", P("@o", orderid), P("@od", orderDriverId))
            CommitTransaction()
        Catch ex As Exception
            RollbackTransaction()
            MsgBox("Error assigning driver: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try

        MsgBox("Driver assigned successfully!", MsgBoxStyle.Information, "Success")

        fillOrdersToDeliver()
        fillDeliveryHistory()
        clearFields()
    End Sub


    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If deliveryid = 0 Then
            MsgBox("Please select a delivery to delete.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        GetQuery("SELECT status, deliveryid FROM customer_delivery WHERE id = @id", "delete_delivery", P("@id", deliveryid))
        If ds.Tables("delete_delivery").Rows.Count = 0 Then
            fill()
            clearFields()
            Exit Sub
        End If

        Dim status As String = ds.Tables("delete_delivery").Rows(0)("status").ToString()
        Dim orderDriverId As Integer = CInt(ds.Tables("delete_delivery").Rows(0)("deliveryid"))
        Dim question As String = "Are you sure you want to delete this delivery?"
        If status = "Delivered" Then question &= vbCrLf & "It was already delivered, so its stock will be returned to inventory."

        If MsgBox(question, MsgBoxStyle.YesNo, "Confirm Deletion") = MsgBoxResult.No Then Exit Sub

        Try
            BeginTransaction()
            If status = "Delivered" Then ReverseStockout(deliveryid)
            Execute("DELETE FROM customer_delivery WHERE id = @id", P("@id", deliveryid))
            ' Remove the driver assignment of *this* delivery's order (looked up, not taken from the textbox).
            Execute("DELETE FROM order_driver WHERE id = @od", P("@od", orderDriverId))
            CommitTransaction()
        Catch ex As Exception
            RollbackTransaction()
            MsgBox("Error deleting delivery: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try

        MsgBox("Delivery deleted successfully! Order is now available for assignment again.", MsgBoxStyle.Information, "Success")

        fillDeliveryHistory()
        fillOrdersToDeliver()
        clearFields()
    End Sub

End Class
