Public Class Dashboard
    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        switchPanel(HomeForm)
    End Sub

    Public Sub switchPanel(frm As Form)
        Panel2.Controls.Clear()
        frm.TopLevel = False
        Panel2.Controls.Add(frm)
        frm.Dock = DockStyle.Fill
        frm.Show()
    End Sub

    Private Sub lblhome_Click(sender As Object, e As EventArgs) Handles lblhome.Click
        labelclicked(lblhome)
        lbltitle.Text = "Dashboard"
        pnlhome.Visible = False
        switchPanel(HomeForm)
        HomeForm.fill()
    End Sub

    Private Sub lblproduct_Click(sender As Object, e As EventArgs) Handles lblproduct.Click
        labelclicked(lblproduct)
        lbltitle.Text = "Dashboard / Product"
        pnlhome.Visible = True
        switchPanel(ProductForm)
        ProductForm.fill()
    End Sub

    Private Sub lblcategory_Click(sender As Object, e As EventArgs) Handles lblcategory.Click
        labelclicked(lblcategory)
        pnlhome.Visible = True
        lbltitle.Text = "Dashboard / Category"
        switchPanel(CategoryForm)
        CategoryForm.fill()
    End Sub

    Private Sub lblsupplier_Click(sender As Object, e As EventArgs) Handles lblsupplier.Click
        labelclicked(lblsupplier)
        switchPanel(SupplierForm)
        pnlhome.Visible = True
        lbltitle.Text = "Dashboard / Supplier"
        SupplierForm.fill()
    End Sub

    Private Sub lblcustomer_Click(sender As Object, e As EventArgs) Handles lblcustomer.Click
        labelclicked(lblcustomer)
        switchPanel(CustomerForm)
        pnlhome.Visible = True
        lbltitle.Text = "Dashboard / Supplier"
        CustomerForm.fill()
    End Sub

    Private Sub lblstock_Click(sender As Object, e As EventArgs) Handles lblstock.Click
        labelclicked(lblstock)
        switchPanel(StockForm)
        pnlhome.Visible = True
        lbltitle.Text = "Dashboard / Stock"
    End Sub

    Private Sub lblorder_Click(sender As Object, e As EventArgs) Handles lblorder.Click
        labelclicked(lblorder)
        switchPanel(OrderForm)
        lbltitle.Text = "Dashboard / Order"
        pnlhome.Visible = True
        OrderForm.fillOrderHistory()
    End Sub

    Private Sub lbldelivery_Click(sender As Object, e As EventArgs) Handles lbldelivery.Click
        labelclicked(lbldelivery)
        switchPanel(DeliveryForm)
        lbltitle.Text = "Dashboard / Delivery"
        pnlhome.Visible = True
        DeliveryForm.fill()
        DeliveryForm.fillDrivers()
        DeliveryForm.fillOrdersToDeliver()
    End Sub

    Private Sub lbldriver_Click(sender As Object, e As EventArgs) Handles lbldriver.Click
        labelclicked(lbldriver)
        switchPanel(DriverForm)
        lbltitle.Text = "Dashboard / Driver"""
        pnlhome.Visible = True
        DriverForm.fill()
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        labelclicked(lblhome)
        switchPanel(HomeForm)
        HomeForm.fill()
        pnlhome.Visible = False
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        If MsgBox("Are you sure you want to log out?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Application.Exit()
        Else
            Exit Sub
        End If
    End Sub

    
End Class