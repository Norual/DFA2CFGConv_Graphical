Public Class Form1

    Private m_ToolSelected As Tools

#Region "Bitmaps Declaration"
    Private m_bmpOfPBX As Bitmap
    Private m_bmpClone1 As Bitmap
    Private m_bmpCircle As Bitmap
#End Region

    Private m_CollectionOfStates As New Dictionary(Of Integer, State)

#Region "Events Declaration"
    Private Event ToolSelected(ByVal tool As Tools)
    Private Event StateFocused(ByVal s As Object)
    Private Event StateUnFocused()
    Private Event RefreshImage(ByVal sender As State, ByVal s As PictureBox)
#End Region

    Private m_intNumOfStates As Integer

    Private m_bMouseDown As Boolean = False

#Region "Private Sub"
    Private Sub CirclePointer(ByVal p As Point)
        Dim bmpClone As Bitmap = BackImage(True)
        Dim g As Graphics = Graphics.FromImage(bmpClone)
        g.DrawImage(m_bmpClone1, 0, 0)
        g.DrawImage(m_bmpCircle, CInt(p.X - (m_bmpCircle.Width / 2)), CInt(p.Y - (m_bmpCircle.Height / 2)))
        g.Dispose()
        pbxDisplay.Image = bmpClone.Clone
        bmpClone.Dispose()
    End Sub

    Private Sub State_MouseEntered(ByVal sender As String, ByVal pbx As PictureBox)
        RaiseEvent StateFocused(sender)
    End Sub

    Private Sub State_MouseLeave()
        RaiseEvent StateUnFocused()
    End Sub

    Private Sub RefreshDisplay(ByVal sender As State)
        pbxDisplay.Image = Nothing
        RaiseEvent RefreshImage(sender, pbxDisplay)
    End Sub

    Private Sub CreateState(ByVal p As Point)
        Dim st As New State("State" & m_intNumOfStates, pbxDisplay, p)
        m_CollectionOfStates.Add(m_intNumOfStates, st)

        AddHandler pbxDisplay.MouseMove, AddressOf m_CollectionOfStates(m_intNumOfStates).MouseMove
        AddHandler ToolSelected, AddressOf m_CollectionOfStates(m_intNumOfStates).ToolSelected
        AddHandler pbxDisplay.MouseDown, AddressOf m_CollectionOfStates(m_intNumOfStates).MouseDown
        AddHandler pbxDisplay.MouseUp, AddressOf m_CollectionOfStates(m_intNumOfStates).MouseUp
        AddHandler Me.StateFocused, AddressOf m_CollectionOfStates(m_intNumOfStates).StateFocused
        AddHandler Me.StateUnFocused, AddressOf m_CollectionOfStates(m_intNumOfStates).StateUnFocused
        AddHandler m_CollectionOfStates(m_intNumOfStates).eMouseLeave, AddressOf State_MouseLeave
        AddHandler m_CollectionOfStates(m_intNumOfStates).eMouseEntered, AddressOf State_MouseEntered
        AddHandler RefreshImage, AddressOf m_CollectionOfStates(m_intNumOfStates).DrawImage
        AddHandler m_CollectionOfStates(m_intNumOfStates).eRefresh, AddressOf RefreshDisplay

        pbxDisplay.Image = Nothing
        RaiseEvent RefreshImage(Nothing, pbxDisplay)
        m_intNumOfStates += 1
    End Sub

#End Region

#Region "Private Function"
    Private Function BackImage(Optional ByVal bForceCreateNew As Boolean = False) As Bitmap
        If pbxDisplay.Image Is Nothing Or bForceCreateNew Then
            Return New Bitmap(pbxDisplay.Width, pbxDisplay.Height)
        Else
            Return pbxDisplay.Image.Clone
        End If
    End Function
#End Region

#Region "Form Events"
    Private Sub pbxDisplay_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles pbxDisplay.MouseMove
        Select Case m_ToolSelected
            Case Tools.Pointer
            Case Tools.State
                If Not m_bMouseDown Then
                    CirclePointer(e.Location)
                End If
        End Select
    End Sub

    Private Sub pbxDisplay_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles pbxDisplay.MouseDown
        m_bMouseDown = True
        Select Case m_ToolSelected
            Case Tools.Pointer
            Case Tools.State
                CreateState(e.Location)
        End Select
    End Sub

    Private Sub pbxDisplay_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles pbxDisplay.MouseUp
        m_bMouseDown = False
        Select Case m_ToolSelected
            Case Tools.Pointer
            Case Tools.State
        End Select

        m_bmpClone1 = BackImage()
    End Sub

    Private Sub btnClick(sender As System.Object, e As System.EventArgs) Handles btnPointer.Click, btnState.Click
        Dim btn As Button = CType(sender, Button)

        Select Case btn.Name
            Case "btnPointer"
                m_ToolSelected = Tools.Pointer
                pbxDisplay.Image = m_bmpClone1.Clone
            Case "btnState"
                m_ToolSelected = Tools.State
        End Select

        RaiseEvent ToolSelected(m_ToolSelected)
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        m_ToolSelected = Tools.Pointer
        m_bmpCircle = m_Objects.Circle
        m_bmpClone1 = BackImage()
    End Sub
#End Region


End Class
