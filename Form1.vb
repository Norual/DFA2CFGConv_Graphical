Public Class Form1
    Private diag As Diagram

    Private Sub btnClick(sender As System.Object, e As System.EventArgs) Handles btnPointer.Click, btnState.Click
        Dim btn As Button = CType(sender, Button)

        Select Case btn.Name
            Case "btnPointer"
                diag.SelectedTool  = Tools.Pointer
            Case "btnState"
                diag.SelectedTool = Tools.State
        End Select

    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        diag = New Diagram(pbxDisplay)
    End Sub


End Class
