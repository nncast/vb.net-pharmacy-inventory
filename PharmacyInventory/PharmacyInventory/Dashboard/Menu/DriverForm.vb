Public Class DriverForm
    Public adding As Boolean = False
    Public updating As Boolean = False
    Public driverid As Integer = Nothing

    Private Sub DriverForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect("localhost", "dbpharmacy", "3306", "root", "")

        fill()

        btnnew.Enabled = True
        btnsave.Enabled = False
        pnlinput.Enabled = False
    End Sub

    Public Sub fill()
        Dim searchText As String = txtsearch.Text.Trim()

        Dim query As String = "SELECT id, name, license FROM driver"
        If searchText <> "" Then
            query &= " WHERE id LIKE '%" & searchText & "%' OR name LIKE '%" & searchText & "%' OR license LIKE '%" & searchText & "%'"
        End If
        query &= " ORDER BY name ASC"

        GetQuery(query, "driver")
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
                SetQuery("INSERT INTO driver (name, license) VALUES ('" & txtname.Text.Trim() & "', '" & txtlicense.Text.Trim() & "')")
                fill()
                disablebuttons()
                clearfields()
                adding = False
                MsgBox("Driver added successfully!", MsgBoxStyle.Information, "Success")
            End If
        ElseIf updating Then
            If MsgBox("Are you sure you want to update this driver?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
                SetQuery("UPDATE driver SET name = '" & txtname.Text.Trim() & "', license = '" & txtlicense.Text.Trim() & "' WHERE id = " & driverid)
                fill()
                disablebuttons()
                clearfields()
                updating = False
                MsgBox("Driver updated successfully!", MsgBoxStyle.Information, "Success")
            End If
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If driverid = Nothing Then
            MsgBox("Select a driver to delete.", MsgBoxStyle.Information, "No Selection")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to delete this driver?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Delete") = MsgBoxResult.Yes Then
            SetQuery("DELETE FROM driver WHERE id = " & driverid)
            fill()
            clearfields()
            MsgBox("Driver deleted successfully!", MsgBoxStyle.Information, "Success")
        End If
    End Sub

    Private Sub driverlist_DoubleClick(sender As Object, e As EventArgs) Handles driverlist.DoubleClick
        driverid = CInt(driverlist.FocusedItem.SubItems(0).Text)
        txtname.Text = driverlist.FocusedItem.SubItems(1).Text
        txtlicense.Text = driverlist.FocusedItem.SubItems(2).Text

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
