Public Class Diagram
    Private WithEvents DisplayArea As PictureBox

    Private m_listOfStates As New List(Of State)

    Private m_bmpClone As Bitmap
    Private m_bmpCircle As Bitmap

    Private m_Tool As Tools = Tools.Pointer

    Private m_bDA_MouseDown As Boolean = False

    Friend Event SelectedToolChanged(ByVal tool As Tools)
    Friend Event StateFocused(ByVal s As Object)
    Friend Event StateUnFocused()
    Friend Event RefreshImage(ByVal sender As State, ByVal s As PictureBox)

    Public Sub New(ByVal pbox As PictureBox)
        DisplayArea = pbox
        m_bmpCircle = m_Objects.Circle
        m_bmpClone = GetCurrentDisplayImage()
    End Sub

    Friend Property SelectedTool As Tools
        Get
            Return m_Tool
        End Get
        Set(value As Tools)
            m_Tool = value
            Select Case m_Tool
                Case Tools.Pointer
                    DisplayArea.Image = m_bmpClone.Clone
                Case Tools.State

            End Select

            RaiseEvent SelectedToolChanged(m_Tool)
        End Set
    End Property

    Friend Property PBox As PictureBox
        Get
            Return DisplayArea
        End Get
        Set(value As PictureBox)
            DisplayArea = value
        End Set
    End Property

    Private Sub State_MouseEntered(ByVal sender As String, ByVal pbx As PictureBox)
        RaiseEvent StateFocused(sender)
    End Sub

    Private Sub State_MouseLeave()
        RaiseEvent StateUnFocused()
    End Sub

    Private Sub RefreshDisplay(ByVal sender As State)
        DisplayArea.Image = Nothing
        RaiseEvent RefreshImage(sender, DisplayArea)
    End Sub

    Private Function GetCurrentDisplayImage() As Bitmap
        If DisplayArea.Image Is Nothing Then
            Return New Bitmap(DisplayArea.Width, DisplayArea.Height)
        Else
            Return DisplayArea.Image.Clone
        End If
    End Function

    Private Sub CirclePointer(ByVal p As Point)
        Dim bmp As New Bitmap(DisplayArea.Width, DisplayArea.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.DrawImage(m_bmpClone, 0, 0)
        g.DrawImage(m_bmpCircle, CInt(p.X - (m_bmpCircle.Width / 2)), CInt(p.Y - (m_bmpCircle.Height / 2)))
        g.Dispose()
        DisplayArea.Image = bmp.Clone
        bmp.Dispose()
    End Sub

    Private Sub CreateState(ByVal p As Point)
        Dim m_intNUmOfStates As Integer = m_listOfStates.Count
        Dim st As New State("State" & m_intNUmOfStates, Me, p)
        m_listOfStates.Add(st)

        AddHandler m_listOfStates(m_intNUmOfStates).eMouseLeave, AddressOf State_MouseLeave
        AddHandler m_listOfStates(m_intNUmOfStates).eMouseEntered, AddressOf State_MouseEntered
        AddHandler m_listOfStates(m_intNUmOfStates).eRefresh, AddressOf RefreshDisplay

        DisplayArea.Image = Nothing
        RaiseEvent RefreshImage(Nothing, DisplayArea)
    End Sub

    Private Sub DisplayArea_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles DisplayArea.MouseDown
        m_bDA_MouseDown = True
        Select Case m_Tool
            Case Tools.Pointer
            Case Tools.State
                CreateState(e.Location)
        End Select
    End Sub

    Private Sub DisplayArea_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles DisplayArea.MouseUp
        m_bDA_MouseDown = False
        Select Case m_Tool
            Case Tools.Pointer
            Case Tools.State
        End Select

        m_bmpClone = GetCurrentDisplayImage()
    End Sub

    Private Sub DisplayArea_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles DisplayArea.MouseMove
        Select Case m_Tool
            Case Tools.Pointer
            Case Tools.State
                If Not m_bDA_MouseDown Then
                    CirclePointer(e.Location)
                End If
        End Select
    End Sub
End Class
