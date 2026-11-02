Public Class StockForm

    ' The card and its outline open the same screen, and both reload it.
    Private Sub StockIn_Click(sender As Object, e As EventArgs) Handles Panel1.Click, RectangleShape1.Click
        Dashboard.switchPanel(StockInForm)
        Dashboard.lbltitle.Text = "Dashboard / Stock / Stock In"
        StockInForm.fill()
    End Sub

    Private Sub StockOut_Click(sender As Object, e As EventArgs) Handles Panel2.Click, RectangleShape2.Click
        Dashboard.switchPanel(StockoutForm)
        Dashboard.lbltitle.Text = "Dashboard / Stock / Stock Out"
        StockoutForm.fillstockouthistory()
    End Sub

End Class