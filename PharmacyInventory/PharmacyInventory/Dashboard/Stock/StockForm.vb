Public Class StockForm

    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles Panel1.Click
        Dashboard.switchPanel(StockInForm)
        Dashboard.lbltitle.Text = "Dashboard / Stock / Stock In"
    End Sub

    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        Dashboard.switchPanel(StockoutForm)
        Dashboard.lbltitle.Text = "Dashboard / Stock / Stock Out"
    End Sub

    Private Sub RectangleShape1_Click(sender As Object, e As EventArgs) Handles RectangleShape1.Click
        Dashboard.switchPanel(StockInForm)
        Dashboard.lbltitle.Text = "Dashboard / Stock / Stock In"
    End Sub

    Private Sub RectangleShape2_Click(sender As Object, e As EventArgs) Handles RectangleShape2.Click
        Dashboard.switchPanel(StockoutForm)
        Dashboard.lbltitle.Text = "Dashboard / Stock / Stock Out"
        StockoutForm.fillstockouthistory()
    End Sub

End Class