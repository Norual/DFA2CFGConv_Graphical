Public Class Form1
    Private diag As Diagram

    Private Sub btnClick(sender As System.Object, e As System.EventArgs) Handles btnPointer.Click, btnState.Click
        Dim btn As Button = CType(sender, Button)

        Select Case btn.Name
            Case "btnPointer"
                diag.SelectedTool = Diagram.Tools.Pointer
            Case "btnState"
                diag.SelectedTool = Diagram.Tools.State
        End Select

    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        diag = New Diagram(pbxDisplay)
    End Sub


End Class
