<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlinput = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.numquantity = New System.Windows.Forms.NumericUpDown()
        Me.ShapeContainer5 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.shapequantity = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbproduct = New System.Windows.Forms.ComboBox()
        Me.ShapeContainer4 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.shapeproduct = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbcustomer = New System.Windows.Forms.ComboBox()
        Me.ShapeContainer3 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.shapecustomer = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.btnremovefromcart = New System.Windows.Forms.Button()
        Me.btnaddtocart = New System.Windows.Forms.Button()
        Me.ShapeContainer2 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape2 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.lvcart = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btndelete = New System.Windows.Forms.Button()
        Me.btncancel = New System.Windows.Forms.Button()
        Me.btnsave = New System.Windows.Forms.Button()
        Me.btnupdate = New System.Windows.Forms.Button()
        Me.btnnew = New System.Windows.Forms.Button()
        Me.txtsearch = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lvOrderHistory = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ShapeContainer6 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.shapesearch = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.RectangleShape5 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lbltotal = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.pnlinput.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.numquantity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlinput
        '
        Me.pnlinput.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlinput.Controls.Add(Me.Panel5)
        Me.pnlinput.Controls.Add(Me.Panel4)
        Me.pnlinput.Controls.Add(Me.Panel3)
        Me.pnlinput.Controls.Add(Me.btnremovefromcart)
        Me.pnlinput.Controls.Add(Me.btnaddtocart)
        Me.pnlinput.Controls.Add(Me.ShapeContainer2)
        Me.pnlinput.Location = New System.Drawing.Point(94, 56)
        Me.pnlinput.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlinput.Name = "pnlinput"
        Me.pnlinput.Size = New System.Drawing.Size(595, 283)
        Me.pnlinput.TabIndex = 0
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.numquantity)
        Me.Panel5.Controls.Add(Me.ShapeContainer5)
        Me.Panel5.Location = New System.Drawing.Point(35, 119)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(192, 65)
        Me.Panel5.TabIndex = 23
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.ForeColor = System.Drawing.Color.DimGray
        Me.Label3.Location = New System.Drawing.Point(22, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 24)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Quantity"
        '
        'numquantity
        '
        Me.numquantity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.numquantity.Location = New System.Drawing.Point(32, 20)
        Me.numquantity.Name = "numquantity"
        Me.numquantity.Size = New System.Drawing.Size(132, 28)
        Me.numquantity.TabIndex = 11
        '
        'ShapeContainer5
        '
        Me.ShapeContainer5.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer5.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer5.Name = "ShapeContainer5"
        Me.ShapeContainer5.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.shapequantity})
        Me.ShapeContainer5.Size = New System.Drawing.Size(192, 65)
        Me.ShapeContainer5.TabIndex = 3
        Me.ShapeContainer5.TabStop = False
        '
        'shapequantity
        '
        Me.shapequantity.BackColor = System.Drawing.Color.White
        Me.shapequantity.BorderColor = System.Drawing.Color.Black
        Me.shapequantity.CornerRadius = 10
        Me.shapequantity.FillColor = System.Drawing.Color.White
        Me.shapequantity.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.shapequantity.Location = New System.Drawing.Point(8, 10)
        Me.shapequantity.Name = "shapequantity"
        Me.shapequantity.Size = New System.Drawing.Size(166, 43)
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.cmbproduct)
        Me.Panel4.Controls.Add(Me.ShapeContainer4)
        Me.Panel4.Location = New System.Drawing.Point(323, 38)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(237, 65)
        Me.Panel4.TabIndex = 22
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.ForeColor = System.Drawing.Color.DimGray
        Me.Label2.Location = New System.Drawing.Point(31, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 24)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Product:"
        '
        'cmbproduct
        '
        Me.cmbproduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbproduct.FormattingEnabled = True
        Me.cmbproduct.Location = New System.Drawing.Point(26, 20)
        Me.cmbproduct.Name = "cmbproduct"
        Me.cmbproduct.Size = New System.Drawing.Size(188, 32)
        Me.cmbproduct.TabIndex = 2
        '
        'ShapeContainer4
        '
        Me.ShapeContainer4.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer4.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer4.Name = "ShapeContainer4"
        Me.ShapeContainer4.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.shapeproduct})
        Me.ShapeContainer4.Size = New System.Drawing.Size(237, 65)
        Me.ShapeContainer4.TabIndex = 3
        Me.ShapeContainer4.TabStop = False
        '
        'shapeproduct
        '
        Me.shapeproduct.BackColor = System.Drawing.Color.White
        Me.shapeproduct.BorderColor = System.Drawing.Color.Black
        Me.shapeproduct.CornerRadius = 10
        Me.shapeproduct.FillColor = System.Drawing.Color.White
        Me.shapeproduct.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.shapeproduct.Location = New System.Drawing.Point(8, 10)
        Me.shapeproduct.Name = "shapeproduct"
        Me.shapeproduct.Size = New System.Drawing.Size(219, 43)
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.cmbcustomer)
        Me.Panel3.Controls.Add(Me.ShapeContainer3)
        Me.Panel3.Location = New System.Drawing.Point(35, 38)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(273, 65)
        Me.Panel3.TabIndex = 21
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.ForeColor = System.Drawing.Color.DimGray
        Me.Label1.Location = New System.Drawing.Point(22, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 24)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Customer Name:"
        '
        'cmbcustomer
        '
        Me.cmbcustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbcustomer.FormattingEnabled = True
        Me.cmbcustomer.Items.AddRange(New Object() {"aaa", "bb"})
        Me.cmbcustomer.Location = New System.Drawing.Point(31, 19)
        Me.cmbcustomer.Name = "cmbcustomer"
        Me.cmbcustomer.Size = New System.Drawing.Size(222, 32)
        Me.cmbcustomer.TabIndex = 0
        '
        'ShapeContainer3
        '
        Me.ShapeContainer3.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer3.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer3.Name = "ShapeContainer3"
        Me.ShapeContainer3.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.shapecustomer})
        Me.ShapeContainer3.Size = New System.Drawing.Size(273, 65)
        Me.ShapeContainer3.TabIndex = 3
        Me.ShapeContainer3.TabStop = False
        '
        'shapecustomer
        '
        Me.shapecustomer.BackColor = System.Drawing.Color.White
        Me.shapecustomer.BorderColor = System.Drawing.Color.Black
        Me.shapecustomer.CornerRadius = 10
        Me.shapecustomer.FillColor = System.Drawing.Color.White
        Me.shapecustomer.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.shapecustomer.Location = New System.Drawing.Point(8, 10)
        Me.shapecustomer.Name = "shapecustomer"
        Me.shapecustomer.Size = New System.Drawing.Size(257, 43)
        '
        'btnremovefromcart
        '
        Me.btnremovefromcart.BackColor = System.Drawing.Color.White
        Me.btnremovefromcart.Font = New System.Drawing.Font("Candara", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnremovefromcart.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnremovefromcart.Location = New System.Drawing.Point(441, 117)
        Me.btnremovefromcart.Name = "btnremovefromcart"
        Me.btnremovefromcart.Size = New System.Drawing.Size(119, 49)
        Me.btnremovefromcart.TabIndex = 18
        Me.btnremovefromcart.Text = "Remove"
        Me.btnremovefromcart.UseVisualStyleBackColor = False
        '
        'btnaddtocart
        '
        Me.btnaddtocart.BackColor = System.Drawing.Color.White
        Me.btnaddtocart.Font = New System.Drawing.Font("Candara", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnaddtocart.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnaddtocart.Location = New System.Drawing.Point(317, 117)
        Me.btnaddtocart.Name = "btnaddtocart"
        Me.btnaddtocart.Size = New System.Drawing.Size(119, 49)
        Me.btnaddtocart.TabIndex = 10
        Me.btnaddtocart.Text = "Add"
        Me.btnaddtocart.UseVisualStyleBackColor = False
        '
        'ShapeContainer2
        '
        Me.ShapeContainer2.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer2.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer2.Name = "ShapeContainer2"
        Me.ShapeContainer2.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape2})
        Me.ShapeContainer2.Size = New System.Drawing.Size(595, 283)
        Me.ShapeContainer2.TabIndex = 20
        Me.ShapeContainer2.TabStop = False
        '
        'RectangleShape2
        '
        Me.RectangleShape2.CornerRadius = 10
        Me.RectangleShape2.FillColor = System.Drawing.Color.White
        Me.RectangleShape2.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.RectangleShape2.Location = New System.Drawing.Point(3, 6)
        Me.RectangleShape2.Name = "RectangleShape2"
        Me.RectangleShape2.Size = New System.Drawing.Size(587, 275)
        '
        'lvcart
        '
        Me.lvcart.BackColor = System.Drawing.Color.White
        Me.lvcart.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvcart.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4, Me.ColumnHeader7, Me.ColumnHeader3, Me.ColumnHeader6, Me.ColumnHeader8})
        Me.lvcart.FullRowSelect = True
        Me.lvcart.GridLines = True
        Me.lvcart.Location = New System.Drawing.Point(11, 38)
        Me.lvcart.Name = "lvcart"
        Me.lvcart.Size = New System.Drawing.Size(333, 210)
        Me.lvcart.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvcart.TabIndex = 16
        Me.lvcart.UseCompatibleStateImageBehavior = False
        Me.lvcart.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Product Name"
        Me.ColumnHeader4.Width = 130
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Quantity"
        Me.ColumnHeader7.Width = 78
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Price"
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Total"
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Width = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.Panel2.Controls.Add(Me.btndelete)
        Me.Panel2.Controls.Add(Me.btncancel)
        Me.Panel2.Controls.Add(Me.btnsave)
        Me.Panel2.Controls.Add(Me.btnupdate)
        Me.Panel2.Controls.Add(Me.btnnew)
        Me.Panel2.Controls.Add(Me.txtsearch)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.lvOrderHistory)
        Me.Panel2.Controls.Add(Me.ShapeContainer6)
        Me.Panel2.Location = New System.Drawing.Point(94, 347)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(967, 335)
        Me.Panel2.TabIndex = 1
        '
        'btndelete
        '
        Me.btndelete.BackColor = System.Drawing.Color.White
        Me.btndelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btndelete.Location = New System.Drawing.Point(445, 2)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(119, 49)
        Me.btndelete.TabIndex = 20
        Me.btndelete.Text = "Delete"
        Me.btndelete.UseVisualStyleBackColor = False
        '
        'btncancel
        '
        Me.btncancel.BackColor = System.Drawing.Color.White
        Me.btncancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btncancel.Location = New System.Drawing.Point(567, 2)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(119, 49)
        Me.btncancel.TabIndex = 13
        Me.btncancel.Text = "Cancel"
        Me.btncancel.UseVisualStyleBackColor = False
        '
        'btnsave
        '
        Me.btnsave.BackColor = System.Drawing.Color.White
        Me.btnsave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnsave.Location = New System.Drawing.Point(689, 2)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(119, 49)
        Me.btnsave.TabIndex = 14
        Me.btnsave.Text = "Save"
        Me.btnsave.UseVisualStyleBackColor = False
        '
        'btnupdate
        '
        Me.btnupdate.BackColor = System.Drawing.Color.White
        Me.btnupdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnupdate.Location = New System.Drawing.Point(323, 2)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(119, 49)
        Me.btnupdate.TabIndex = 11
        Me.btnupdate.Text = "Update"
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'btnnew
        '
        Me.btnnew.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnnew.Font = New System.Drawing.Font("Candara", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.ForeColor = System.Drawing.Color.White
        Me.btnnew.Location = New System.Drawing.Point(811, 2)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(153, 49)
        Me.btnnew.TabIndex = 21
        Me.btnnew.Text = "+   New"
        Me.btnnew.UseVisualStyleBackColor = False
        '
        'txtsearch
        '
        Me.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearch.Location = New System.Drawing.Point(102, 18)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(165, 25)
        Me.txtsearch.TabIndex = 24
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(11, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 24)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Search : "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Candara", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(18, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(222, 29)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "List of Order History"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lvOrderHistory
        '
        Me.lvOrderHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvOrderHistory.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader5})
        Me.lvOrderHistory.FullRowSelect = True
        Me.lvOrderHistory.GridLines = True
        Me.lvOrderHistory.Location = New System.Drawing.Point(14, 90)
        Me.lvOrderHistory.Name = "lvOrderHistory"
        Me.lvOrderHistory.Size = New System.Drawing.Size(940, 220)
        Me.lvOrderHistory.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvOrderHistory.TabIndex = 9
        Me.lvOrderHistory.UseCompatibleStateImageBehavior = False
        Me.lvOrderHistory.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "ID"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Customer"
        Me.ColumnHeader2.Width = 500
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Date Submitted"
        Me.ColumnHeader5.Width = 380
        '
        'ShapeContainer6
        '
        Me.ShapeContainer6.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer6.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer6.Name = "ShapeContainer6"
        Me.ShapeContainer6.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.shapesearch, Me.RectangleShape5})
        Me.ShapeContainer6.Size = New System.Drawing.Size(967, 335)
        Me.ShapeContainer6.TabIndex = 21
        Me.ShapeContainer6.TabStop = False
        '
        'shapesearch
        '
        Me.shapesearch.BackColor = System.Drawing.Color.White
        Me.shapesearch.CornerRadius = 10
        Me.shapesearch.FillColor = System.Drawing.Color.White
        Me.shapesearch.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.shapesearch.Location = New System.Drawing.Point(87, 12)
        Me.shapesearch.Name = "shapesearch"
        Me.shapesearch.Size = New System.Drawing.Size(187, 35)
        '
        'RectangleShape5
        '
        Me.RectangleShape5.CornerRadius = 10
        Me.RectangleShape5.FillColor = System.Drawing.Color.White
        Me.RectangleShape5.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.RectangleShape5.Location = New System.Drawing.Point(5, 55)
        Me.RectangleShape5.Name = "RectangleShape5"
        Me.RectangleShape5.Size = New System.Drawing.Size(956, 274)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lbltotal)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.lvcart)
        Me.Panel1.Controls.Add(Me.ShapeContainer1)
        Me.Panel1.Location = New System.Drawing.Point(708, 56)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(350, 283)
        Me.Panel1.TabIndex = 22
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Font = New System.Drawing.Font("Candara", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(156, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 29)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Cart"
        '
        'lbltotal
        '
        Me.lbltotal.AutoSize = True
        Me.lbltotal.BackColor = System.Drawing.Color.White
        Me.lbltotal.Location = New System.Drawing.Point(281, 251)
        Me.lbltotal.Name = "lbltotal"
        Me.lbltotal.Size = New System.Drawing.Size(59, 24)
        Me.lbltotal.TabIndex = 19
        Me.lbltotal.Text = "00.00"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("Candara", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(18, 251)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 24)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Total Cost"
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(350, 283)
        Me.ShapeContainer1.TabIndex = 17
        Me.ShapeContainer1.TabStop = False
        '
        'RectangleShape1
        '
        Me.RectangleShape1.CornerRadius = 10
        Me.RectangleShape1.FillColor = System.Drawing.Color.White
        Me.RectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.RectangleShape1.Location = New System.Drawing.Point(4, 3)
        Me.RectangleShape1.Name = "RectangleShape1"
        Me.RectangleShape1.Size = New System.Drawing.Size(341, 277)
        '
        'OrderForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1125, 740)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlinput)
        Me.Font = New System.Drawing.Font("Candara", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "OrderForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OrdersForm"
        Me.pnlinput.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.numquantity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlinput As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnsave As System.Windows.Forms.Button
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents btnaddtocart As System.Windows.Forms.Button
    Friend WithEvents lvOrderHistory As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmbcustomer As System.Windows.Forms.ComboBox
    Friend WithEvents cmbproduct As System.Windows.Forms.ComboBox
    Friend WithEvents lvcart As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnremovefromcart As System.Windows.Forms.Button
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Friend WithEvents numquantity As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnnew As System.Windows.Forms.Button
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lbltotal As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ShapeContainer2 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape2 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents ShapeContainer5 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents shapequantity As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents ShapeContainer4 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents shapeproduct As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents ShapeContainer3 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents shapecustomer As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents ShapeContainer6 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape5 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtsearch As System.Windows.Forms.TextBox
    Friend WithEvents shapesearch As Microsoft.VisualBasic.PowerPacks.RectangleShape
End Class
