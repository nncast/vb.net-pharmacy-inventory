Public Class CategoryForm
    Public adding As Boolean = False
    Public updating As Boolean = False
    Public categoryid As Integer = Nothing

    Private Sub CategoryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect("localhost", "dbpharmacy", "3306", "root", "")

        fill()
        btnnew.Enabled = True
        btnsave.Enabled = False
        pnlinput.Enabled = False
    End Sub

    Public Sub fill()
        Dim search As String = txtsearch.Text.Trim()
        Dim query As String = "SELECT id, categoryname FROM category"
        If search <> "" Then query &= " WHERE id LIKE '%" & search & "%' OR categoryname LIKE '%" & search & "%'"

        GetQuery(query, "category")
        categorylist.Items.Clear()

        For Each row As DataRow In ds.Tables("category").Rows
            With categorylist.Items.Add(row("id").ToString())
                .SubItems.Add(row("categoryname").ToString())
            End With
        Next
    End Sub

    Public Sub clearfields()
        txtcategoryname.Clear()
        categoryid = Nothing
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
        enablebuttons()
        clearfields()
        adding = True
        pnlinput.Enabled = True
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        If categoryid = Nothing Then
            MsgBox("Select a category to update.", MsgBoxStyle.Information, "No Selection")
            Exit Sub
        End If

        enablebuttons()
        updating = True
        pnlinput.Enabled = True
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If txtcategoryname.Text.Trim = "" Then
            MsgBox("Category name is required!", MsgBoxStyle.Critical, "Validation Error")
            Exit Sub
        End If

        If adding Then
            If MsgBox("Are you sure you want to add a new category?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
                SetQuery("INSERT INTO category (categoryname) VALUES ('" & txtcategoryname.Text.Trim() & "')")
                fill()
                clearfields()
                disablebuttons()
                pnlinput.Enabled = False
                adding = False
                MsgBox("Category added successfully!", MsgBoxStyle.Information, "Success")
            End If
        ElseIf updating Then
            If MsgBox("Are you sure you want to update this category?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
                SetQuery("UPDATE category SET categoryname = '" & txtcategoryname.Text.Trim() & "' WHERE id = " & categoryid)
                fill()
                clearfields()
                disablebuttons()
                pnlinput.Enabled = False
                updating = False
                MsgBox("Category updated successfully!", MsgBoxStyle.Information, "Success")
            End If
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If categoryid = Nothing Then
            MsgBox("Select a category to delete.", MsgBoxStyle.Information, "No Selection")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to delete this category?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm Delete") = MsgBoxResult.Yes Then
            SetQuery("DELETE FROM category WHERE id = " & categoryid)
            fill()
            clearfields()
            MsgBox("Category deleted successfully!", MsgBoxStyle.Information, "Success")
        End If
    End Sub

    Private Sub categorylist_DoubleClick(sender As Object, e As EventArgs) Handles categorylist.DoubleClick
        categoryid = CInt(categorylist.FocusedItem.SubItems(0).Text)
        txtcategoryname.Text = categorylist.FocusedItem.SubItems(1).Text

        btnupdate.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        If adding Then
            If MsgBox("Are you sure you want to cancel adding new category information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Cancel") = MsgBoxResult.Yes Then
                adding = False
            Else
                Exit Sub
            End If
        ElseIf updating Then
            If MsgBox("Are you sure you want to cancel updating category information?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Cancel") = MsgBoxResult.Yes Then
                updating = False
            Else
                Exit Sub
            End If
        End If

        disablebuttons()
        clearfields()
        pnlinput.Enabled = False
        categoryid = Nothing
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        fill()
    End Sub

    Private Sub txtcategoryname_GotFocus(sender As Object, e As EventArgs) Handles txtcategoryname.GotFocus
        HandleFocus(shapecategory, True)
    End Sub

    Private Sub txtcategoryname_LostFocus(sender As Object, e As EventArgs) Handles txtcategoryname.LostFocus
        HandleFocus(shapecategory, False)
    End Sub

    Private Sub txtsearch_GotFocus(sender As Object, e As EventArgs) Handles txtsearch.GotFocus
        HandleFocus(shapesearch, True)
    End Sub

    Private Sub txtsearch_LostFocus(sender As Object, e As EventArgs) Handles txtsearch.LostFocus
        HandleFocus(shapesearch, False)
    End Sub

End Class
