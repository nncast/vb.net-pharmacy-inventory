Public Class DriverForm
    Public adding As Boolean = False
    Public updating As Boolean = False
    Public driverid As Integer = Nothing

    Private Sub DriverForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()

        fill()

        btnnew.Enabled = True
        btnsave.Enabled = False
        pnlinput.Enabled = False
    End Sub

    Public Sub fill()
        Dim searchText As String = txtsearch.Text.Trim()

        Dim query As String = "SELECT id, name, license FROM driver"
        If searchText <> "" Then
            query &= " WHERE id LIKE @s OR name LIKE @s OR license LIKE @s"
        End If
        query &= " ORDER BY name ASC"

        GetQuery(query, "driver", P("@s", "%" & searchText & "%"))
        driverlist.Items.Clear()

        For i = 0 To ds.Tables("driver").Rows.Count - 1
            Dim item = driverlist.Items.Add(ds.Tables("driver").Rows(i).Item("id").ToString())
            With item.SubItems
                .Add(ds.Tables("driver").Rows(i).Item("name").ToString())
                .Add(ds.Tables("driver").Rows(i).Item("license").ToString())
            End With
        Next
    End Sub

    Public Sub clearfields()
        txtname.Clear()
        txtlicense.Clear()
        driverid = Nothing
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
        If driverid = Nothing Then
            MsgBox("Select a driver to update.", MsgBoxStyle.Information, "No Selection")
            Exit Sub
        End If

        enablebuttons()
        updating = True
        pnlinput.Enabled = True
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If txtname.Text.Trim = "" Or txtlicense.Text.Trim = "" Then
            MsgBox("All fields are required!", MsgBoxStyle.Information, "Validation Error")
            Exit Sub
        End If

        If adding Then
            If MsgBox("Are you sure you want to add a new driver?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
                If SetQuery("INSERT INTO driver (name, license) VALUES (@name, @license)", P("@name", txtname.Text.Trim()), P("@license", txtlicense.Text.Trim())) Then
                    fill()
                    disablebuttons()
                    clearfields()
                    adding = False
                    MsgBox("Driver added successfully!", MsgBoxStyle.Information, "Success")
                End If
            End If
        ElseIf updating Then
            If MsgBox("Are you sure you want to update this driver?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
                If SetQuery("UPDATE driver SET name = @name, license = @license WHERE id = @id", P("@name", txtname.Text.Trim()), P("@license", txtlicense.Text.Trim()), P("@id", driverid)) Then
                    fill()
                    disablebuttons()
                    clearfields()
                    updating = False
                    MsgBox("Driver updated successfully!", MsgBoxStyle.Information, "Success")
                End If
            End If
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If driverid = Nothing Then
            MsgBox("Select a driver to delete.", MsgBoxStyle.Information, "No Selection")
            Exit Sub
        End If

        ' The foreign keys cascade, so deleting a driver would silently remove their deliveries.
        Dim assigned As Integer = CInt(GetValue("SELECT COUNT(*) FROM order_driver WHERE driverid = @id", P("@id", driverid)))
        If assigned > 0 Then
            MsgBox("This driver is assigned to " & assigned & " delivery(ies) and can't be deleted. Reassign or delete those deliveries first.", MsgBoxStyle.Exclamation, "Driver In Use")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to delete this driver?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Delete") = MsgBoxResult.Yes Then
            If SetQuery("DELETE FROM driver WHERE id = @id", P("@id", driverid)) Then
                fill()
                clearfields()
                disablebuttons()
                MsgBox("Driver deleted successfully!", MsgBoxStyle.Information, "Success")
            End If
        End If
    End Sub

    Private Sub driverlist_DoubleClick(sender As Object, e As EventArgs) Handles driverlist.DoubleClick
        If adding Or updating Or driverlist.SelectedItems.Count = 0 Then Exit Sub

        driverid = CInt(driverlist.SelectedItems(0).SubItems(0).Text)
        txtname.Text = driverlist.SelectedItems(0).SubItems(1).Text
        txtlicense.Text = driverlist.SelectedItems(0).SubItems(2).Text

        btnupdate.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        If adding Then
            If MsgBox("Are you sure you want to cancel adding new driver information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Cancel") = MsgBoxResult.Yes Then
                adding = False
            Else
                Exit Sub
            End If
        ElseIf updating Then
            If MsgBox("Are you sure you want to cancel updating driver information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Cancel") = MsgBoxResult.Yes Then
                updating = False
            Else
                Exit Sub
            End If
        End If

        disablebuttons()
        clearfields()
        pnlinput.Enabled = False
        driverid = Nothing
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        fill()
    End Sub

    Private Sub txtsearch_GotFocus(sender As Object, e As EventArgs) Handles txtsearch.GotFocus
        HandleFocus(shapesearch, True)
    End Sub

    Private Sub txtsearch_LostFocus(sender As Object, e As EventArgs) Handles txtsearch.LostFocus
        HandleFocus(shapesearch, False)
    End Sub

    Private Sub txtname_GotFocus(sender As Object, e As EventArgs) Handles txtname.GotFocus
        HandleFocus(shapename, True)
    End Sub

    Private Sub txtname_LostFocus(sender As Object, e As EventArgs) Handles txtname.LostFocus
        HandleFocus(shapename, False)
    End Sub

    Private Sub txtlicense_GotFocus(sender As Object, e As EventArgs) Handles txtlicense.GotFocus
        HandleFocus(shapelicense, True)
    End Sub

    Private Sub txtlicense_LostFocus(sender As Object, e As EventArgs) Handles txtlicense.LostFocus
        HandleFocus(shapelicense, False)
    End Sub
End Class
