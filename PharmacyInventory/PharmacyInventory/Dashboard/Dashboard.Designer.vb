<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dashboard))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbldriver = New System.Windows.Forms.Label()
        Me.lbldelivery = New System.Windows.Forms.Label()
        Me.lblorder = New System.Windows.Forms.Label()
        Me.lblstock = New System.Windows.Forms.Label()
        Me.lblsupplier = New System.Windows.Forms.Label()
        Me.lblcustomer = New System.Windows.Forms.Label()
        Me.lblproduct = New System.Windows.Forms.Label()
        Me.lblcategory = New System.Windows.Forms.Label()
        Me.lblhome = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbltitle = New System.Windows.Forms.Label()
        Me.btnback = New System.Windows.Forms.Button()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.pnlhome = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlhome.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lbldriver)
        Me.Panel1.Controls.Add(Me.lbldelivery)
        Me.Panel1.Controls.Add(Me.lblorder)
        Me.Panel1.Controls.Add(Me.lblstock)
        Me.Panel1.Controls.Add(Me.lblsupplier)
        Me.Panel1.Controls.Add(Me.lblcustomer)
        Me.Panel1.Controls.Add(Me.lblproduct)
        Me.Panel1.Controls.Add(Me.lblcategory)
        Me.Panel1.Controls.Add(Me.lblhome)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(308, 844)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Candara", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.DarkGray
        Me.Label1.Location = New System.Drawing.Point(4, 731)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(304, 51)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Log Out"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbldriver
        '
        Me.lbldriver.Font = New System.Drawing.Font("Candara", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldriver.ForeColor = System.Drawing.Color.DarkGray
        Me.lbldriver.Location = New System.Drawing.Point(0, 410)
        Me.lbldriver.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbldriver.Name = "lbldriver"
        Me.lbldriver.Size = New System.Drawing.Size(308, 51)
        Me.lbldriver.TabIndex = 10
        Me.lbldriver.Text = "Driver"
        Me.lbldriver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbldelivery
        '
        Me.lbldelivery.Font = New System.Drawing.Font("Candara", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldelivery.ForeColor = System.Drawing.Color.DarkGray
        Me.lbldelivery.Location = New System.Drawing.Point(0, 565)
        Me.lbldelivery.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbldelivery.Name = "lbldelivery"
        Me.lbldelivery.Size = New System.Drawing.Size(308, 51)
        Me.lbldelivery.TabIndex = 9
        Me.lbldelivery.Text = "Delivery"
        Me.lbldelivery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblorder
        '
        Me.lblorder.Font = New System.Drawing.Font("Candara", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblorder.ForeColor = System.Drawing.Color.DarkGray
        Me.lblorder.Location = New System.Drawing.Point(0, 514)
        Me.lblorder.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblorder.Name = "lblorder"
        Me.lblorder.Size = New System.Drawing.Size(308, 51)
        Me.lblorder.TabIndex = 8
        Me.lblorder.Text = "Order"
        Me.lblorder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblstock
        '
        Me.lblstock.Font = New System.Drawing.Font("Candara", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstock.ForeColor = System.Drawing.Color.DarkGray
        Me.lblstock.Location = New System.Drawing.Point(0, 461)
        Me.lblstock.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblstock.Name = "lblstock"
        Me.lblstock.Size = New System.Drawing.Size(308, 51)
        Me.lblstock.TabIndex = 7
        Me.lblstock.Text = "Stock"
        Me.lblstock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblsupplier
        '
        Me.lblsupplier.Font = New System.Drawing.Font("Candara", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsupplier.ForeColor = System.Drawing.Color.DarkGray
        Me.lblsupplier.Location = New System.Drawing.Point(0, 308)
        Me.lblsupplier.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblsupplier.Name = "lblsupplier"
        Me.lblsupplier.Size = New System.Drawing.Size(308, 51)
        Me.lblsupplier.TabIndex = 5
        Me.lblsupplier.Text = "Supplier"
        Me.lblsupplier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblcustomer
        '
        Me.lblcustomer.Font = New System.Drawing.Font("Candara", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcustomer.ForeColor = System.Drawing.Color.DarkGray
        Me.lblcustomer.Location = New System.Drawing.Point(0, 359)
        Me.lblcustomer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblcustomer.Name = "lblcustomer"
        Me.lblcustomer.Size = New System.Drawing.Size(308, 51)
        Me.lblcustomer.TabIndex = 6
        Me.lblcustomer.Text = "Customer"
        Me.lblcustomer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblproduct
        '
        Me.lblproduct.Font = New System.Drawing.Font("Candara", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblproduct.ForeColor = System.Drawing.Color.DarkGray
        Me.lblproduct.Location = New System.Drawing.Point(0, 257)
        Me.lblproduct.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblproduct.Name = "lblproduct"
        Me.lblproduct.Size = New System.Drawing.Size(308, 51)
        Me.lblproduct.TabIndex = 3
        Me.lblproduct.Text = "Product"
        Me.lblproduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblcategory
        '
        Me.lblcategory.Font = New System.Drawing.Font("Candara", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcategory.ForeColor = System.Drawing.Color.DarkGray
        Me.lblcategory.Location = New System.Drawing.Point(0, 206)
        Me.lblcategory.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblcategory.Name = "lblcategory"
        Me.lblcategory.Size = New System.Drawing.Size(308, 51)
        Me.lblcategory.TabIndex = 4
        Me.lblcategory.Text = "Category"
        Me.lblcategory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblhome
        '
        Me.lblhome.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.lblhome.Font = New System.Drawing.Font("Candara", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblhome.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.lblhome.Location = New System.Drawing.Point(0, 155)
        Me.lblhome.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblhome.Name = "lblhome"
        Me.lblhome.Size = New System.Drawing.Size(308, 51)
        Me.lblhome.TabIndex = 2
        Me.lblhome.Text = "Home"
        Me.lblhome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(24, 34)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(60, 62)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(-37, -19)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(432, 171)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Location = New System.Drawing.Point(350, 70)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1125, 740)
        Me.Panel2.TabIndex = 1
        '
        'lbltitle
        '
        Me.lbltitle.AutoSize = True
        Me.lbltitle.Font = New System.Drawing.Font("Candara", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.lbltitle.Location = New System.Drawing.Point(328, 26)
        Me.lbltitle.Name = "lbltitle"
        Me.lbltitle.Size = New System.Drawing.Size(145, 35)
        Me.lbltitle.TabIndex = 2
        Me.lbltitle.Text = "Dashboard"
        '
        'btnback
        '
        Me.btnback.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.btnback.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.btnback.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.btnback.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.btnback.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnback.Font = New System.Drawing.Font("Candara", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnback.ForeColor = System.Drawing.Color.DimGray
        Me.btnback.Location = New System.Drawing.Point(5, 6)
        Me.btnback.Name = "btnback"
        Me.btnback.Size = New System.Drawing.Size(131, 49)
        Me.btnback.TabIndex = 11
        Me.btnback.Text = "         Home"
        Me.btnback.UseVisualStyleBackColor = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(17, 14)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(41, 35)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox3.TabIndex = 12
        Me.PictureBox3.TabStop = False
        '
        'pnlhome
        '
        Me.pnlhome.Controls.Add(Me.PictureBox3)
        Me.pnlhome.Controls.Add(Me.btnback)
        Me.pnlhome.Location = New System.Drawing.Point(1313, 12)
        Me.pnlhome.Name = "pnlhome"
        Me.pnlhome.Size = New System.Drawing.Size(148, 64)
        Me.pnlhome.TabIndex = 13
        Me.pnlhome.Visible = False
        '
        'Dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1517, 836)
        Me.Controls.Add(Me.lbltitle)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlhome)
        Me.Font = New System.Drawing.Font("Candara", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Dashboard"
        Me.Text = "Dashboard"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlhome.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblhome As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblproduct As System.Windows.Forms.Label
    Friend WithEvents lblcategory As System.Windows.Forms.Label
    Friend WithEvents lblstock As System.Windows.Forms.Label
    Friend WithEvents lblsupplier As System.Windows.Forms.Label
    Friend WithEvents lblcustomer As System.Windows.Forms.Label
    Friend WithEvents lbldriver As System.Windows.Forms.Label
    Friend WithEvents lbldelivery As System.Windows.Forms.Label
    Friend WithEvents lblorder As System.Windows.Forms.Label
    Friend WithEvents lbltitle As System.Windows.Forms.Label
    Friend WithEvents btnback As System.Windows.Forms.Button
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents pnlhome As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
