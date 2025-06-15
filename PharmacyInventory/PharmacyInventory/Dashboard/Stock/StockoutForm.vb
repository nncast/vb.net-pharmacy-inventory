Public Class StockoutForm
    Private Sub StockoutForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect("localhost", "dbpharmacy", "3306", "root", "")
        fillstockouthistory()
    End Sub

    Public Sub fillstockouthistory()
        Dim search As String = txtsearch.Text.Trim()
        Dim query As String = "SELECT " & _
                              "so.id AS stockout_id, " & _
                              "c.name AS customer_name, " & _
                              "c.address AS customer_address, " & _
                              "d.name AS driver_name, " & _
                              "p.productname AS product, " & _
                              "sod.quantity AS order_quantity, " & _
                              "(sod.quantity + p.stock) AS stock_before, " & _
                              "p.stock AS stock_after, " & _
                              "so.transactiondate AS date_stockout " & _
                              "FROM stockout so " & _
                              "INNER JOIN customer_delivery cd ON so.customer_delivery_id = cd.id " & _
                              "INNER JOIN customer c ON cd.customerid = c.id " & _
                              "INNER JOIN order_driver od ON cd.deliveryid = od.id " & _
                              "INNER JOIN driver d ON od.driverid = d.id " & _
                              "INNER JOIN stockout_details sod ON so.id = sod.stockoutid " & _
                              "INNER JOIN product p ON sod.productid = p.productid " & _
                              "WHERE cd.status = 'Delivered' "

        If search <> "" Then
            query &= "AND (so.id LIKE '%" & search & "%' " & _
                     "OR c.name LIKE '%" & search & "%' " & _
                     "OR c.address LIKE '%" & search & "%' " & _
                     "OR d.name LIKE '%" & search & "%' " & _
                     "OR p.productname LIKE '%" & search & "%' " & _
                     "OR sod.quantity LIKE '%" & search & "%' " & _
                     "OR so.transactiondate LIKE '%" & search & "%') "
        End If

        query &= "ORDER BY so.transactiondate DESC"

        GetQuery(query, "stockout_history")
        lvStockoutHistory.Items.Clear()

        If ds.Tables("stockout_history").Rows.Count > 0 Then
            For Each row As DataRow In ds.Tables("stockout_history").Rows
                Dim item = lvStockoutHistory.Items.Add(row("stockout_id").ToString())
                With item.SubItems
                    .Add(row("customer_name").ToString())
                    .Add(row("customer_address").ToString())
                    .Add(row("driver_name").ToString())
                    .Add(row("product").ToString())
                    .Add(row("order_quantity").ToString())
                    .Add(row("stock_before").ToString())
                    .Add(row("stock_after").ToString())
                    .Add(Convert.ToDateTime(row("date_stockout")).ToString("yyyy-MM-dd HH:mm:ss"))
                End With
            Next
        End If
    End Sub

    Private Sub txtsearch_GotFocus(sender As Object, e As EventArgs) Handles txtsearch.GotFocus
        HandleFocus(shapesearch, True)
    End Sub

    Private Sub txtsearch_LostFocus(sender As Object, e As EventArgs) Handles txtsearch.LostFocus
        HandleFocus(shapesearch, False)
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        fillstockouthistory()
    End Sub

End Class
