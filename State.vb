Public Class State
    Private WithEvents m_diag As Diagram
    Private WithEvents m_PBox As PictureBox

    Private m_bmpImage As Bitmap
    Private m_pbxClone As Bitmap
    Private m_bmpCircle As Bitmap
    Private m_bmpHighlightedCircle As Bitmap

    Private m_ToolSelected As Tools = Tools.State

    Private m_strName As String
    Private m_strFocusedState As String

    Private m_bMouseEntered As Boolean = False
    Private m_bMouseDown As Boolean = False

    Private m_pLocation As Point
    Private m_pDiff As Point

    Private m_CollectionOfSmallCircles As New Dictionary(Of String, AnchorPoint)

    Friend Event eMouseEntered(ByVal sender As String, ByVal pbx As PictureBox)
    Friend Event eMouseLeave()
    Friend Event eRefresh(ByVal sender As Object)
    Friend Event eMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
    Friend Event eMouseUp(ByVal sender As PictureBox, ByVal p As Point)

    Friend Property Name As String
        Get
            Return m_strName
        End Get
        Set(value As String)
            m_strName = value
        End Set
    End Property


    Private Sub Diag_StateFocused(ByVal sender As Object) Handles m_diag.StateFocused
        m_strFocusedState = sender
    End Sub

    Private Sub Diag_StateUnFocused() Handles m_diag.StateUnFocused
        m_strFocusedState = ""
    End Sub

    Private Sub Diag_SelectedToolChanged(ByVal tool As Tools) Handles m_diag.SelectedToolChanged
        m_ToolSelected = tool
    End Sub

    Private Sub PBox_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles m_PBox.MouseMove
        If m_strFocusedState = "" Or m_strFocusedState = Me.Name Then
            Select Case m_ToolSelected
                Case Tools.Pointer

                    Dim pbx As PictureBox = CType(sender, PictureBox)

                    RaiseEvent eMouseMove(sender, e)

                    If Not m_bMouseDown Then
                        Dim pLocation As New Point(m_pLocation.X + Math.Floor(m_bmpCircle.Width / 2), m_pLocation.Y + Math.Floor(m_bmpCircle.Height / 2))
                        Dim a As Integer = Math.Abs(pLocation.X - e.Location.X)
                        Dim b As Integer = Math.Abs(pLocation.Y - e.Location.Y)
                        Dim hypotenuse As Integer = Math.Sqrt((a ^ 2) + (b ^ 2))
                        If hypotenuse < Radius + 1 And Not m_bMouseEntered Then
                            m_pbxClone = BackImage(pbx)
                            RaiseEvent eMouseEntered(Name, pbx)
                            m_bMouseEntered = True
                        ElseIf hypotenuse > Radius + 4 And m_bMouseEntered Then
                            pbx.Image = m_pbxClone
                            m_bMouseEntered = False
                            RaiseEvent eMouseLeave()
                        End If
                    ElseIf m_bMouseDown Then
                        pbx.Image = Nothing
                        MoveState(pbx, e.Location)
                    End If

                Case Tools.State

            End Select
        End If
    End Sub

    Private Sub PBox_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles m_PBox.MouseDown
        Select Case m_ToolSelected
            Case Tools.Pointer
                If m_bMouseEntered Then
                    If Not m_bMouseDown Then
                        Dim pbx As PictureBox = CType(sender, PictureBox)
                        m_pDiff = New Point(e.Location.X - m_pLocation.X, e.Location.Y - m_pLocation.Y)
                        RaiseEvent eRefresh(Me)
                        m_pbxClone = BackImage(pbx)
                        MoveState(pbx, e.Location)
                        m_bMouseDown = True
                    End If
                End If
            Case Tools.State

        End Select
    End Sub

    Private Sub PBox_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles m_PBox.MouseUp
        Select Case m_ToolSelected
            Case Tools.Pointer
                If m_bMouseEntered Then
                    If m_bMouseDown Then
                        Dim pbx As PictureBox = CType(sender, PictureBox)
                        RaiseEvent eRefresh(Nothing)
                        m_pbxClone = BackImage(pbx)
                        UpdateSmallCirclesLocation()
                        RaiseEvent eMouseEntered(Name, pbx)
                        m_bMouseDown = False
                    End If
                End If
            Case Tools.State

        End Select
    End Sub

    Private Sub Diag_RefreshImage(ByVal sender As State, ByVal pbx As PictureBox) Handles m_diag.RefreshImage
        If sender Is Nothing OrElse sender.Name <> Name Then
            Dim bmp As Bitmap = BackImage(pbx)
            Dim g As Graphics = Graphics.FromImage(bmp)
            g.DrawImage(m_bmpImage, 0, 0)
            pbx.Image = bmp.Clone
            bmp.Dispose()
            g.Dispose()
        End If
    End Sub

    Private Sub MoveState(ByVal pbx As PictureBox, ByVal p As Point)
        Dim bmp As Bitmap = BackImage(pbx, True)
        Dim g As Graphics = Graphics.FromImage(bmp)
        m_pLocation = New Point(p.X - m_pDiff.X, p.Y - m_pDiff.Y)
        g.DrawImage(m_bmpCircle, m_pLocation)
        m_bmpImage = bmp.Clone
        g.DrawImage(m_pbxClone, 0, 0)
        g.DrawImage(m_bmpImage, 0, 0)
        pbx.Image = bmp.Clone
        bmp.Dispose()
        g.Dispose()
    End Sub

    Private Sub HighlightState(ByVal pbx As PictureBox, ByVal p As Point)
        Dim bmp As Bitmap = BackImage(pbx)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.DrawImage(m_bmpHighlightedCircle, m_pLocation)
        pbx.Image = bmp.Clone
        bmp.Dispose()
        g.Dispose()
    End Sub

    Private Sub DrawState(ByVal pbx As PictureBox, ByVal p As Point)
        Dim bmp As New Bitmap(pbx.Width, pbx.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        m_pLocation = New Point(p.X - Math.Floor(m_bmpCircle.Width / 2), p.Y - Math.Floor(m_bmpCircle.Height / 2))
        g.DrawImage(m_bmpCircle, m_pLocation)
        m_bmpImage = bmp.Clone
        bmp.Dispose()
        g.Dispose()
    End Sub

    Private Function BackImage(ByVal pbx As PictureBox, Optional bForceCreateNew As Boolean = False) As Bitmap
        If pbx.Image Is Nothing Or bForceCreateNew Then
            Return New Bitmap(pbx.Width, pbx.Height)
        Else
            Return pbx.Image.Clone
        End If
    End Function

    Private Sub AddSmallCircles(ByVal pbx As PictureBox)
        Dim center As New Point(m_pLocation.X + Math.Floor(m_bmpCircle.Width / 2), m_pLocation.Y + Math.Floor(m_bmpCircle.Height / 2))

        m_CollectionOfSmallCircles.Add("Anc1", New AnchorPoint("Anc1", pbx, New Point(center.X - Radius, center.Y)))
        m_CollectionOfSmallCircles.Add("Anc3", New AnchorPoint("Anc3", pbx, New Point(center.X, center.Y + Radius)))
        m_CollectionOfSmallCircles.Add("Anc5", New AnchorPoint("Anc5", pbx, New Point(center.X + Radius, center.Y)))
        m_CollectionOfSmallCircles.Add("Anc7", New AnchorPoint("Anc7", pbx, New Point(center.X, center.Y - Radius)))

        Dim loc45deg As New Point(Math.Floor(Math.Cos(Math.PI / 4) * Radius), Math.Floor(Math.Sin(Math.PI / 4) * Radius))

        m_CollectionOfSmallCircles.Add("Anc2", New AnchorPoint("Anc2", pbx, New Point(center.X - loc45deg.X, center.Y - loc45deg.Y)))
        m_CollectionOfSmallCircles.Add("Anc4", New AnchorPoint("Anc4", pbx, New Point(center.X - loc45deg.X, center.Y + loc45deg.Y)))
        m_CollectionOfSmallCircles.Add("Anc6", New AnchorPoint("Anc6", pbx, New Point(center.X + loc45deg.X, center.Y + loc45deg.Y)))
        m_CollectionOfSmallCircles.Add("Anc8", New AnchorPoint("Anc8", pbx, New Point(center.X + loc45deg.X, center.Y - loc45deg.Y)))

        For Each anc As AnchorPoint In m_CollectionOfSmallCircles.Values
            AddHandler Me.eMouseEntered, AddressOf anc.MouseEnteredState
            AddHandler Me.eMouseLeave, AddressOf anc.MouseLeftState
            AddHandler Me.eMouseMove, AddressOf anc.MouseMove
        Next
    End Sub

    Private Sub UpdateSmallCirclesLocation()
        Dim origLoc As New Point(m_pLocation.X + Math.Floor(m_bmpCircle.Width / 2), m_pLocation.Y + Math.Floor(m_bmpCircle.Height / 2))

        m_CollectionOfSmallCircles("Anc1").Location = New Point(origLoc.X - Radius, origLoc.Y)
        m_CollectionOfSmallCircles("Anc3").Location = New Point(origLoc.X, origLoc.Y + Radius)
        m_CollectionOfSmallCircles("Anc5").Location = New Point(origLoc.X + Radius, origLoc.Y)
        m_CollectionOfSmallCircles("Anc7").Location = New Point(origLoc.X, origLoc.Y - Radius)

        Dim loc45deg As New Point(Math.Floor(Math.Cos(Math.PI / 4) * Radius), Math.Floor(Math.Sin(Math.PI / 4) * Radius))

        m_CollectionOfSmallCircles("Anc2").Location = New Point(origLoc.X - loc45deg.X, origLoc.Y - loc45deg.Y)
        m_CollectionOfSmallCircles("Anc4").Location = New Point(origLoc.X - loc45deg.X, origLoc.Y + loc45deg.Y)
        m_CollectionOfSmallCircles("Anc6").Location = New Point(origLoc.X + loc45deg.X, origLoc.Y + loc45deg.Y)
        m_CollectionOfSmallCircles("Anc8").Location = New Point(origLoc.X + loc45deg.X, origLoc.Y - loc45deg.Y)
    End Sub

    Public Sub New(ByVal strName As String, ByVal diag As Diagram, ByVal p As Point)
        m_diag = diag
        m_PBox = diag.PBox
        m_strName = strName
        m_bmpCircle = m_Objects.Circle
        DrawState(diag.PBox, p)
        m_bmpHighlightedCircle = m_Objects.Circle_Highlight
        AddSmallCircles(diag.PBox)
    End Sub

End Class
