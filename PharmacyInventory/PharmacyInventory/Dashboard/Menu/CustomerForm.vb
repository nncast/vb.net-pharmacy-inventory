Public Class CustomerForm
    Public adding As Boolean = False
    Public updating As Boolean = False
    Public customerid As Integer = Nothing

    Private Sub CustomerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()

        fill()

        btnnew.Enabled = True
        btnsave.Enabled = False
        pnlinput.Enabled = False
    End Sub

    Public Sub fill()
        Dim search As String = txtsearch.Text.Trim()
        Dim query As String = "SELECT id, name, contact, address FROM customer"
        If search <> "" Then
            query &= " WHERE id LIKE @s OR name LIKE @s OR contact LIKE @s OR address LIKE @s"
        End If
        query &= " ORDER BY name ASC"

        GetQuery(query, "customer", P("@s", "%" & search & "%"))
        customerlist.Items.Clear()


        For i = 0 To ds.Tables("customer").Rows.Count - 1
            Dim item = customerlist.Items.Add(ds.Tables("customer").Rows(i).Item("id").ToString())
            With item.SubItems
                .Add(ds.Tables("customer").Rows(i).Item("name").ToString())
                .Add(ds.Tables("customer").Rows(i).Item("contact").ToString())
                .Add(ds.Tables("customer").Rows(i).Item("address").ToString())
            End With
        Next
    End Sub

    Public Sub clearfields()
        txtcustomername.Clear()
        txtcontact.Clear()
        txtaddress.Clear()
        customerid = Nothing
    End Sub

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

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        enablebuttons()
        clearfields()
        adding = True
        pnlinput.Enabled = True
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        If customerid = Nothing Then
            MsgBox("Select a customer to update.", MsgBoxStyle.Information, "No Selection")
            Exit Sub
        End If

        enablebuttons()
        updating = True
        pnlinput.Enabled = True
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If txtcustomername.Text.Trim = "" Or txtcontact.Text.Trim = "" Or txtaddress.Text.Trim = "" Then
            MsgBox("All fields are required!", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        If adding Then
            If MsgBox("Are you sure you want to add a new customer?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
                If SetQuery("INSERT INTO customer (name, contact, address) VALUES (@name, @contact, @address)",
                            P("@name", txtcustomername.Text.Trim()), P("@contact", txtcontact.Text.Trim()), P("@address", txtaddress.Text.Trim())) Then
                    MsgBox("Customer added successfully!", MsgBoxStyle.Information, "Success")
                    adding = False
                    refreshForm()
                End If
            End If

        ElseIf updating Then
            If MsgBox("Are you sure you want to update this customer?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
                If SetQuery("UPDATE customer SET name = @name, contact = @contact, address = @address WHERE id = @id",
                            P("@name", txtcustomername.Text.Trim()), P("@contact", txtcontact.Text.Trim()), P("@address", txtaddress.Text.Trim()), P("@id", customerid)) Then
                    MsgBox("Customer updated successfully!", MsgBoxStyle.Information, "Success")
                    updating = False
                    refreshForm()
                End If
            End If
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If customerid = Nothing Then
            MsgBox("Select a customer to delete.", MsgBoxStyle.Information, "No Selection")
            Exit Sub
        End If

        ' The foreign keys cascade, so deleting a customer would silently delete their orders and deliveries.
        Dim orders As Integer = CInt(GetValue("SELECT COUNT(*) FROM orders WHERE customerid = @id", P("@id", customerid)))
        If orders > 0 Then
            MsgBox("This customer has " & orders & " order(s) and can't be deleted.", MsgBoxStyle.Exclamation, "Customer In Use")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to delete this customer?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Delete") = MsgBoxResult.Yes Then
            If SetQuery("DELETE FROM customer WHERE id = @id", P("@id", customerid)) Then
                MsgBox("Customer deleted successfully!", MsgBoxStyle.Information, "Success")
                refreshForm()
            End If
        End If
    End Sub

    Private Sub customerlist_DoubleClick(sender As Object, e As EventArgs) Handles customerlist.DoubleClick
        If adding Or updating Or customerlist.SelectedItems.Count = 0 Then Exit Sub

        customerid = CInt(customerlist.SelectedItems(0).SubItems(0).Text)
        txtcustomername.Text = customerlist.SelectedItems(0).SubItems(1).Text
        txtcontact.Text = customerlist.SelectedItems(0).SubItems(2).Text
        txtaddress.Text = customerlist.SelectedItems(0).SubItems(3).Text

        btnupdate.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        If adding Then
            If MsgBox("Are you sure you want to cancel adding new customer information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Cancel") = MsgBoxResult.Yes Then
                adding = False
            Else
                Exit Sub
            End If
        ElseIf updating Then
            If MsgBox("Are you sure you want to cancel updating customer information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Cancel") = MsgBoxResult.Yes Then
                updating = False
            Else
                Exit Sub
            End If
        End If

        disablebuttons()
        clearfields()
        pnlinput.Enabled = False
        customerid = Nothing
    End Sub

    Public Sub refreshForm()
        disablebuttons()
        clearfields()
        fill()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        fill()
    End Sub

    Private Sub txtcustomername_GotFocus(sender As Object, e As EventArgs) Handles txtcustomername.GotFocus
        HandleFocus(shapecustomer, True)
    End Sub

    Private Sub txtcustomername_LostFocus(sender As Object, e As EventArgs) Handles txtcustomername.LostFocus
        HandleFocus(shapecustomer, False)
    End Sub

    Private Sub txtcontactk_GotFocus(sender As Object, e As EventArgs) Handles txtcontact.GotFocus
        HandleFocus(shapecontact, True)
    End Sub

    Private Sub txtcontact_LostFocus(sender As Object, e As EventArgs) Handles txtcontact.LostFocus
        HandleFocus(shapecontact, False)
    End Sub

    Private Sub txtaddress_GotFocus(sender As Object, e As EventArgs) Handles txtaddress.GotFocus
        HandleFocus(shapeaddress, True)
    End Sub

    Private Sub txtaddress_LostFocus(sender As Object, e As EventArgs) Handles txtaddress.LostFocus
        HandleFocus(shapeaddress, False)
    End Sub

    Private Sub txtsearch_GotFocus(sender As Object, e As EventArgs) Handles txtsearch.GotFocus
        HandleFocus(shapesearch, True)
    End Sub

    Private Sub txtsearch_LostFocus(sender As Object, e As EventArgs) Handles txtsearch.LostFocus
        HandleFocus(shapesearch, False)
    End Sub
End Class
