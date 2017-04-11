<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.pbxDisplay = New System.Windows.Forms.PictureBox()
        Me.btnPointer = New System.Windows.Forms.Button()
        Me.btnState = New System.Windows.Forms.Button()
        CType(Me.pbxDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbxDisplay
        '
        Me.pbxDisplay.Location = New System.Drawing.Point(2, 40)
        Me.pbxDisplay.Name = "pbxDisplay"
        Me.pbxDisplay.Size = New System.Drawing.Size(346, 244)
        Me.pbxDisplay.TabIndex = 0
        Me.pbxDisplay.TabStop = False
        '
        'btnPointer
        '
        Me.btnPointer.Location = New System.Drawing.Point(2, 11)
        Me.btnPointer.Name = "btnPointer"
        Me.btnPointer.Size = New System.Drawing.Size(75, 23)
        Me.btnPointer.TabIndex = 1
        Me.btnPointer.Text = "Button1"
        Me.btnPointer.UseVisualStyleBackColor = True
        '
        'btnState
        '
        Me.btnState.Location = New System.Drawing.Point(83, 11)
        Me.btnState.Name = "btnState"
        Me.btnState.Size = New System.Drawing.Size(75, 23)
        Me.btnState.TabIndex = 2
        Me.btnState.Text = "Button2"
        Me.btnState.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(349, 310)
        Me.Controls.Add(Me.btnState)
        Me.Controls.Add(Me.btnPointer)
        Me.Controls.Add(Me.pbxDisplay)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.pbxDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pbxDisplay As System.Windows.Forms.PictureBox
    Friend WithEvents btnPointer As System.Windows.Forms.Button
    Friend WithEvents btnState As System.Windows.Forms.Button

End Class
