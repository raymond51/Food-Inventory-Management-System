<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRM_Sub_InsertFood
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
        Me.txtbox_Foodname = New System.Windows.Forms.TextBox()
        Me.btn_SAVE = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.NumericUpDownSTOCK = New System.Windows.Forms.NumericUpDown()
        Me.DateTimePickerExpiry = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RBY = New System.Windows.Forms.RadioButton()
        Me.RBN = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBoxFoodType = New System.Windows.Forms.ComboBox()
        CType(Me.NumericUpDownSTOCK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtbox_Foodname
        '
        Me.txtbox_Foodname.Location = New System.Drawing.Point(85, 30)
        Me.txtbox_Foodname.Name = "txtbox_Foodname"
        Me.txtbox_Foodname.Size = New System.Drawing.Size(151, 20)
        Me.txtbox_Foodname.TabIndex = 0
        '
        'btn_SAVE
        '
        Me.btn_SAVE.Location = New System.Drawing.Point(97, 247)
        Me.btn_SAVE.Name = "btn_SAVE"
        Me.btn_SAVE.Size = New System.Drawing.Size(95, 23)
        Me.btn_SAVE.TabIndex = 1
        Me.btn_SAVE.Text = "SAVE"
        Me.btn_SAVE.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "FoodName"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Stock"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Expiry"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 192)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Freezable?"
        '
        'NumericUpDownSTOCK
        '
        Me.NumericUpDownSTOCK.Location = New System.Drawing.Point(85, 73)
        Me.NumericUpDownSTOCK.Name = "NumericUpDownSTOCK"
        Me.NumericUpDownSTOCK.Size = New System.Drawing.Size(151, 20)
        Me.NumericUpDownSTOCK.TabIndex = 9
        '
        'DateTimePickerExpiry
        '
        Me.DateTimePickerExpiry.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePickerExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerExpiry.Location = New System.Drawing.Point(85, 109)
        Me.DateTimePickerExpiry.Name = "DateTimePickerExpiry"
        Me.DateTimePickerExpiry.Size = New System.Drawing.Size(151, 20)
        Me.DateTimePickerExpiry.TabIndex = 23
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RBY)
        Me.GroupBox1.Controls.Add(Me.RBN)
        Me.GroupBox1.Location = New System.Drawing.Point(85, 192)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(171, 49)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Option"
        '
        'RBY
        '
        Me.RBY.AutoSize = True
        Me.RBY.Location = New System.Drawing.Point(40, 19)
        Me.RBY.Name = "RBY"
        Me.RBY.Size = New System.Drawing.Size(32, 17)
        Me.RBY.TabIndex = 25
        Me.RBY.TabStop = True
        Me.RBY.Text = "Y"
        Me.RBY.UseVisualStyleBackColor = True
        '
        'RBN
        '
        Me.RBN.AutoSize = True
        Me.RBN.Location = New System.Drawing.Point(95, 19)
        Me.RBN.Name = "RBN"
        Me.RBN.Size = New System.Drawing.Size(33, 17)
        Me.RBN.TabIndex = 26
        Me.RBN.TabStop = True
        Me.RBN.Text = "N"
        Me.RBN.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 155)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "FoodType"
        '
        'ComboBoxFoodType
        '
        Me.ComboBoxFoodType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxFoodType.FormattingEnabled = True
        Me.ComboBoxFoodType.Items.AddRange(New Object() {"Dairy", "Vegetable", "Protein", "Fruit"})
        Me.ComboBoxFoodType.Location = New System.Drawing.Point(85, 155)
        Me.ComboBoxFoodType.Name = "ComboBoxFoodType"
        Me.ComboBoxFoodType.Size = New System.Drawing.Size(151, 21)
        Me.ComboBoxFoodType.TabIndex = 27
        '
        'FRM_Sub_InsertFood
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(291, 282)
        Me.Controls.Add(Me.ComboBoxFoodType)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DateTimePickerExpiry)
        Me.Controls.Add(Me.NumericUpDownSTOCK)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_SAVE)
        Me.Controls.Add(Me.txtbox_Foodname)
        Me.Name = "FRM_Sub_InsertFood"
        Me.Text = "InsertFood"
        CType(Me.NumericUpDownSTOCK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtbox_Foodname As System.Windows.Forms.TextBox
    Friend WithEvents btn_SAVE As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownSTOCK As System.Windows.Forms.NumericUpDown
    Friend WithEvents DateTimePickerExpiry As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RBY As System.Windows.Forms.RadioButton
    Friend WithEvents RBN As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxFoodType As System.Windows.Forms.ComboBox
End Class
