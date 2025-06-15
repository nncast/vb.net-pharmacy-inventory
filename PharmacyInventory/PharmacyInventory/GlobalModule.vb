Module GlobalModule

    Public Sub labelclicked(lbl As Label)
        Dim labels As Label() = {Dashboard.lblhome, Dashboard.lblcategory,
                             Dashboard.lblproduct, Dashboard.lblcustomer, Dashboard.lblsupplier,
                             Dashboard.lblstock, Dashboard.lblorder, Dashboard.lbldelivery,
                                 Dashboard.lbldriver}

        For Each label In labels
            label.BackColor = Color.White
            label.ForeColor = Color.DarkGray
        Next

        lbl.BackColor = Color.FromArgb(244, 245, 250)
        lbl.ForeColor = Color.FromArgb(5, 102, 187)
    End Sub

    Public Sub HandleFocus(shape As PowerPacks.RectangleShape, isFocused As Boolean)
        If isFocused Then
            shape.BorderColor = Color.FromArgb(5, 102, 187)
        Else
            shape.BorderColor = Color.Black
        End If
    End Sub

End Module