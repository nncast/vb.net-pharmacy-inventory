Public Class HomeForm

    Private Sub HomeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect("localhost", "dbpharmacy", "3306", "root", "")
        fill()
    End Sub

    Public Sub fill()
        LoadTotalStock()
        LoadTotalOrders()
        LoadPendingDeliveries()
        LoadLowStockProducts()
        LoadRecentOrders()
        LoadRecentDeliveries()
    End Sub

    Private Sub LoadTotalStock()
        GetQuery("SELECT SUM(stock) AS total FROM product", "product")
        lbltotalstock.Text = If(ds.Tables("product").Rows.Count > 0 AndAlso Not IsDBNull(ds.Tables("product").Rows(0).Item("total")), ds.Tables("product").Rows(0).Item("total").ToString(), "0")
    End Sub

    Private Sub LoadTotalOrders()
        GetQuery("SELECT COUNT(id) AS total FROM orders", "orders")
        lbltotalorders.Text = If(ds.Tables("orders").Rows.Count > 0, ds.Tables("orders").Rows(0).Item("total").ToString(), "0")
    End Sub

    Private Sub LoadPendingDeliveries()
        GetQuery("SELECT COUNT(id) AS total FROM customer_delivery WHERE status = 'Pending'", "customer_delivery")
        lblpendingdeliveries.Text = If(ds.Tables("customer_delivery").Rows.Count > 0, ds.Tables("customer_delivery").Rows(0).Item("total").ToString(), "0")
    End Sub

    Private Sub LoadLowStockProducts()
        Try
            GetQuery("SELECT productname, stock, price FROM product WHERE stock <= 5 ORDER BY stock ASC", "product")
            lowstocklist.Items.Clear()

            If ds.Tables("product").Rows.Count = 0 Then
                Dim item = lowstocklist.Items.Add("")
                With item.SubItems
                    .Add("(No low stock)")
                    .Add("")
                    .Add("")
                End With
            Else
                For i As Integer = 0 To ds.Tables("product").Rows.Count - 1
                    Dim item = lowstocklist.Items.Add(ds.Tables("product").Rows(i).Item("productname").ToString()) ' Product Name as first column
                    With item.SubItems
                        .Add(ds.Tables("product").Rows(i).Item("stock").ToString())
                        .Add(ds.Tables("product").Rows(i).Item("price").ToString())
                    End With
                Next
            End If
        Catch ex As Exception
            MsgBox("Error loading low stock products: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub LoadRecentOrders()
        GetQuery("SELECT o.id, c.name, o.orderdate, SUM(od.quantity * od.price) AS total FROM orders o INNER JOIN customer c ON o.customerid = c.id INNER JOIN orderdetails od ON o.id = od.orderid GROUP BY o.id ORDER BY o.orderdate DESC LIMIT 5", "orders")
        recentorders.Items.Clear()

        For i As Integer = 0 To ds.Tables("orders").Rows.Count - 1
            Dim item = recentorders.Items.Add((i + 1).ToString())
            With item.SubItems
                .Add(ds.Tables("orders").Rows(i).Item("id").ToString())
                .Add(ds.Tables("orders").Rows(i).Item("name").ToString())
                .Add(ds.Tables("orders").Rows(i).Item("orderdate").ToString())
                .Add(ds.Tables("orders").Rows(i).Item("total").ToString())
            End With
        Next
    End Sub

    Private Sub LoadRecentDeliveries()
        GetQuery("SELECT cd.id, c.name AS customername, cd.status FROM customer_delivery cd INNER JOIN customer c ON cd.customerid = c.id ORDER BY cd.dateassigned DESC LIMIT 5", "customer_delivery")

        recentdeliveries.Items.Clear()

        For i As Integer = 0 To ds.Tables("customer_delivery").Rows.Count - 1
            Dim item = recentdeliveries.Items.Add(ds.Tables("customer_delivery").Rows(i).Item("id").ToString())
            With item.SubItems
                .Add(ds.Tables("customer_delivery").Rows(i).Item("customername").ToString())
                .Add(ds.Tables("customer_delivery").Rows(i).Item("status").ToString())
            End With
        Next
    End Sub

    Private Sub btnproducts_Click(sender As Object, e As EventArgs) Handles btnproducts.Click
        Dashboard.switchPanel(ProductForm)
        ProductForm.fill()
        Dashboard.pnlhome.Visible = True
    End Sub

    Private Sub btnstockin_Click(sender As Object, e As EventArgs) Handles btnstockin.Click
        Dashboard.switchPanel(StockInForm)
        Dashboard.lbltitle.Text = "Dashboard / Stock / Stock In"
        StockInForm.fillstockinhistory()
        Dashboard.pnlhome.Visible = True
    End Sub

    Private Sub btnstockout_Click(sender As Object, e As EventArgs) Handles btnstockout.Click
        Dashboard.switchPanel(StockoutForm)
        Dashboard.lbltitle.Text = "Dashboard / Stock / Stock Out"
        StockoutForm.fillstockouthistory()
        Dashboard.pnlhome.Visible = True
    End Sub

    Private Sub btnorders_Click(sender As Object, e As EventArgs) Handles btnorders.Click
        Dashboard.switchPanel(OrderForm)
        Dashboard.pnlhome.Visible = True
    End Sub

    Private Sub btndeliveries_Click(sender As Object, e As EventArgs) Handles btndeliveries.Click
        Dashboard.switchPanel(DeliveryForm)
        Dashboard.pnlhome.Visible = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btncategory.Click
        Dashboard.switchPanel(CategoryForm)
        Dashboard.pnlhome.Visible = True
    End Sub
End Class
