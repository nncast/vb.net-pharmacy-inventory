Public Class SupplierForm
    Public adding As Boolean = False
    Public updating As Boolean = False
    Public supplierid As Integer = Nothing

    Private Sub SupplierForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        btnnew.Enabled = True
        btnsave.Enabled = False
        pnlinput.Enabled = False

        fill()
    End Sub

    Public Sub fill()
        Dim search As String = txtsearch.Text.Trim()
        Dim query As String = "SELECT id, name, contact, address FROM supplier"

        If search <> "" Then
            query &= " WHERE id LIKE @s OR name LIKE @s OR contact LIKE @s OR address LIKE @s"
        End If

        query &= " ORDER BY name ASC"
        GetQuery(query, "supplier", P("@s", "%" & search & "%"))

        supplierlist.Items.Clear()

        For i = 0 To ds.Tables("supplier").Rows.Count - 1
            Dim item = supplierlist.Items.Add(ds.Tables("supplier").Rows(i).Item("id").ToString())
            With item.SubItems
                .Add(ds.Tables("supplier").Rows(i).Item("name").ToString())
                .Add(ds.Tables("supplier").Rows(i).Item("contact").ToString())
                .Add(ds.Tables("supplier").Rows(i).Item("address").ToString())
            End With
        Next
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        fill()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        enablebuttons()
        clearfields()
        adding = True
        pnlinput.Enabled = True
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        If supplierid = Nothing Then
            MsgBox("Select a supplier to update.", MsgBoxStyle.Information, "No Selection")
            Exit Sub
        End If

        enablebuttons()
        updating = True
        pnlinput.Enabled = True
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If txtsuppliername.Text.Trim = "" Or txtcontact.Text.Trim = "" Or txtaddress.Text.Trim = "" Then
            MsgBox("All fields are required!", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        If adding Then
            If MsgBox("Are you sure you want to add a new supplier?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
                If SetQuery("INSERT INTO supplier (name, contact, address) VALUES (@name, @contact, @address)",
                            P("@name", txtsuppliername.Text.Trim()), P("@contact", txtcontact.Text.Trim()), P("@address", txtaddress.Text.Trim())) Then
                    MsgBox("Supplier added successfully!", MsgBoxStyle.Information, "Success")
                    adding = False
                    refreshForm()
                End If
            End If

        ElseIf updating Then
            If MsgBox("Are you sure you want to update this supplier?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
                If SetQuery("UPDATE supplier SET name = @name, contact = @contact, address = @address WHERE id = @id",
                            P("@name", txtsuppliername.Text.Trim()), P("@contact", txtcontact.Text.Trim()), P("@address", txtaddress.Text.Trim()), P("@id", supplierid)) Then
                    MsgBox("Supplier updated successfully!", MsgBoxStyle.Information, "Success")
                    updating = False
                    refreshForm()
                End If
            End If
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If supplierid = Nothing Then
            MsgBox("Select a supplier to delete.", MsgBoxStyle.Information, "No Selection")
            Exit Sub
        End If

        ' The foreign keys cascade, so deleting a supplier would silently delete its stock-in history.
        Dim stockIns As Integer = CInt(GetValue("SELECT COUNT(*) FROM stockin WHERE supplierid = @id", P("@id", supplierid)))
        If stockIns > 0 Then
            MsgBox("This supplier has " & stockIns & " stock-in record(s) and can't be deleted.", MsgBoxStyle.Exclamation, "Supplier In Use")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to delete this supplier?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Delete") = MsgBoxResult.Yes Then
            If SetQuery("DELETE FROM supplier WHERE id = @id", P("@id", supplierid)) Then
                MsgBox("Supplier deleted successfully!", MsgBoxStyle.Information, "Success")
                refreshForm()
            End If
        End If
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Dim action As String = If(adding, "adding", If(updating, "updating", ""))
        If action <> "" AndAlso MsgBox("Are you sure you want to cancel " & action & " supplier information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Cancel") = MsgBoxResult.No Then
            Exit Sub
        End If
        adding = False
        updating = False
        refreshForm()
    End Sub

    Private Sub supplierlist_DoubleClick(sender As Object, e As EventArgs) Handles supplierlist.DoubleClick
        If adding Or updating Or supplierlist.SelectedItems.Count = 0 Then Exit Sub

        supplierid = CInt(supplierlist.SelectedItems(0).SubItems(0).Text)
        txtsuppliername.Text = supplierlist.SelectedItems(0).SubItems(1).Text
        txtcontact.Text = supplierlist.SelectedItems(0).SubItems(2).Text
        txtaddress.Text = supplierlist.SelectedItems(0).SubItems(3).Text

        btnupdate.Enabled = True
        btndelete.Enabled = True
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

    Public Sub clearfields()
        txtsuppliername.Clear()
        txtcontact.Clear()
        txtaddress.Clear()
        supplierid = Nothing
    End Sub

    Public Sub refreshForm()
        disablebuttons()
        clearfields()
        fill()
    End Sub

    Private Sub txtsuppliername_GotFocus(sender As Object, e As EventArgs) Handles txtsuppliername.GotFocus
        HandleFocus(shapesupplier, True)
    End Sub

    Private Sub txtsuppliername_LostFocus(sender As Object, e As EventArgs) Handles txtsuppliername.LostFocus
        HandleFocus(shapesupplier, False)
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
