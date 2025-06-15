Public Class DeliveryForm
    Public deliveryid As Integer = 0
    Public orderid As Integer = 0

    Private Sub DeliveryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect("localhost", "dbpharmacy", "3306", "root", "")
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

        GetQuery("SELECT cd.id, c.name AS customer_name, d.id AS driver_id, d.name AS driver_name, d.license, cd.status, cd.dateassigned " &
                 "FROM customer_delivery cd " &
                 "INNER JOIN customer c ON cd.customerid = c.id " &
                 "LEFT JOIN order_driver od ON cd.deliveryid = od.id " &
                 "LEFT JOIN driver d ON od.driverid = d.id", "delivery")

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

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If deliveryid = 0 Or cmbStatus.SelectedIndex = -1 Then
            MsgBox("Select a delivery and a status.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        Dim status = cmbStatus.SelectedItem.ToString()

        Try
            SetQuery("UPDATE customer_delivery SET status = '" & status & "' WHERE id = " & txtorderid.Text)
            SetQuery("UPDATE order_driver SET driverid = '" & cmbDriver.SelectedValue & "' WHERE id = " & txtorderid.Text)

            If status = "Delivered" Then
                SetQuery("INSERT INTO stockout (customer_delivery_id, transactiondate) VALUES (" & _
                         txtorderid.Text & ", NOW())")

                Dim stockoutId As Integer = GetLastInsertedID()

                GetQuery("SELECT od.productid, od.quantity " & _
                         "FROM orderdetails od " & _
                         "INNER JOIN customer_delivery cd ON cd.orderid = od.orderid " & _
                         "WHERE cd.id = " & txtorderid.Text, "orderdetails")

                For Each row As DataRow In ds.Tables("orderdetails").Rows
                    Dim productId As Integer = CInt(row("productid"))
                    Dim quantity As Integer = CInt(row("quantity"))

                    SetQuery("INSERT INTO stockout_details (stockoutid, productid, quantity) " & _
                             "VALUES (" & stockoutId & ", " & productId & ", " & quantity & ")")

                    SetQuery("UPDATE product SET stock = stock - " & quantity & " WHERE productid = " & productId)
                Next

                MsgBox("Stockout recorded successfully for delivered order!", MsgBoxStyle.Information, "Stockout Success")
            End If

            MsgBox("Delivery status updated successfully!", MsgBoxStyle.Information, "Success")
            clearFields()
        Catch ex As Exception
            MsgBox("Error updating delivery status or driver: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
        fillDeliveryHistory()
    End Sub

    Private Sub lvOrdersToDeliver_DoubleClick(sender As Object, e As EventArgs) Handles lvOrdersToDeliver.DoubleClick
        clearFields()
        orderid = CInt(lvOrdersToDeliver.FocusedItem.SubItems(0).Text)
        txtorderid.Text = orderid.ToString()
        pnlinput.Enabled = True
        enableButtons()
    End Sub

    Private Sub lvDeliveryHistory_DoubleClick(sender As Object, e As EventArgs) Handles lvDeliveryHistory.DoubleClick
        clearFields()
        deliveryid = CInt(lvDeliveryHistory.FocusedItem.SubItems(0).Text)
        txtorderid.Text = deliveryid.ToString()

        Dim driverId = CInt(lvDeliveryHistory.FocusedItem.Tag)
        Dim status = lvDeliveryHistory.FocusedItem.SubItems(4).Text

        cmbDriver.SelectedValue = driverId
        cmbStatus.SelectedItem = status

        enableButtons()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearFields()
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

        Try
            SetQuery("INSERT INTO order_driver (orderid, driverid) VALUES (" & orderid & ", " & cmbDriver.SelectedValue & ")")

            SetQuery("INSERT INTO customer_delivery (customerid, orderid, deliveryid, status) " & _
                     "VALUES ((SELECT customerid FROM orders WHERE id = " & orderid & "), " & _
                     orderid & ", (SELECT id FROM order_driver WHERE orderid = " & orderid & "), 'Pending')")

            MsgBox("Driver assigned successfully!", MsgBoxStyle.Information, "Success")

            fillOrdersToDeliver()
            fillDeliveryHistory()
            clearFields()

        Catch ex As Exception
            MsgBox("Error assigning driver: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If deliveryid = 0 Then
            MsgBox("Please select a delivery to delete.", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        Dim confirmation = MsgBox("Are you sure you want to delete this delivery?", MsgBoxStyle.YesNo, "Confirm Deletion")
        If confirmation = MsgBoxResult.No Then Exit Sub

        Try
            Dim orderIdToRestore As Integer = CInt(txtorderid.Text)

            SetQuery("DELETE FROM order_driver WHERE orderid = " & orderIdToRestore)

            SetQuery("DELETE FROM customer_delivery WHERE id = " & deliveryid)

            MsgBox("Delivery deleted successfully! Order is now available for assignment again.", MsgBoxStyle.Information, "Success")

            fillDeliveryHistory()
            fillOrdersToDeliver()
            clearFields()

        Catch ex As Exception
            MsgBox("Error deleting delivery: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

End Class