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

    ' stockout_details.stock_after (added in v0.1.3) keeps each product's stock right
    ' after a stock-out, so the history shows the levels at that time. Databases
    ' created before it get the column the first time it is needed.
    Private stockAfterReady As Boolean = False
    Public Sub EnsureStockAfterColumn()
        If stockAfterReady Then Return
        If CInt(GetValue("SELECT COUNT(*) FROM information_schema.columns WHERE table_schema = DATABASE() AND table_name = 'stockout_details' AND column_name = 'stock_after'")) = 0 Then
            If Not SetQuery("ALTER TABLE stockout_details ADD COLUMN stock_after INT NULL") Then Return
        End If
        stockAfterReady = True
    End Sub

End Module