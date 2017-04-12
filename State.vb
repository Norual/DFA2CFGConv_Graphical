Public Class State
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

    Private m_Objects As New Objects

    Friend Event MouseEntered(ByVal sender As Object)
    Friend Event MouseLeave()
    Friend Event Refresh(ByVal sender As Object)

    Friend WriteOnly Property CircleImage As Bitmap
        Set(value As Bitmap)
            m_bmpCircle = value
        End Set
    End Property

    Friend Property Name As String
        Get
            Return m_strName
        End Get
        Set(value As String)
            m_strName = value
        End Set
    End Property

    Friend Sub ToolSelected(ByVal tool As Tools)
        m_ToolSelected = tool
    End Sub

    Friend Sub StateFocused(ByVal sender As Object)
        m_strFocusedState = CType(sender, State).Name
    End Sub

    Friend Sub StateUnFocused()
        m_strFocusedState = ""
    End Sub

    Friend Sub MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        If m_strFocusedState = "" Or m_strFocusedState = Me.Name Then
            Select Case m_ToolSelected
                Case Tools.Pointer

                    Dim pbx As PictureBox = CType(sender, PictureBox)
                    If Not m_bMouseDown Then
                        Dim pLocation As New Point(m_pLocation.X + Math.Floor(m_bmpCircle.Width / 2), m_pLocation.Y + Math.Floor(m_bmpCircle.Height / 2))
                        Dim a As Integer = Math.Abs(pLocation.X - e.Location.X)
                        Dim b As Integer = Math.Abs(pLocation.Y - e.Location.Y)
                        Dim hypotenuse As Integer = Math.Sqrt((a ^ 2) + (b ^ 2))
                        If hypotenuse < Radius + 1 And Not m_bMouseEntered Then
                            RaiseEvent MouseEntered(Me)
                            m_pbxClone = BackImage(pbx)
                            HighlightState(pbx, m_pLocation)
                            m_bMouseEntered = True
                        ElseIf hypotenuse > Radius + 4 And m_bMouseEntered Then
                            pbx.Image = m_pbxClone
                            m_bMouseEntered = False
                            RaiseEvent MouseLeave()
                        End If
                    ElseIf m_bMouseDown Then
                        pbx.Image = Nothing
                        MoveState(pbx, e.Location)
                    End If

                Case Tools.State

            End Select
        End If
    End Sub

    Friend Sub MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        Select Case m_ToolSelected
            Case Tools.Pointer
                If m_bMouseEntered Then
                    If Not m_bMouseDown Then
                        Dim pbx As PictureBox = CType(sender, PictureBox)
                        m_pDiff = New Point(e.Location.X - m_pLocation.X, e.Location.Y - m_pLocation.Y)
                        RaiseEvent Refresh(Me)
                        m_pbxClone = BackImage(pbx)
                        MoveState(pbx, e.Location)
                        m_bMouseDown = True
                    End If
                End If
            Case Tools.State

        End Select
    End Sub

    Friend Sub MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        Select Case m_ToolSelected
            Case Tools.Pointer
                If m_bMouseEntered Then
                    If m_bMouseDown Then
                        Dim pbx As PictureBox = CType(sender, PictureBox)
                        RaiseEvent Refresh(Nothing)
                        m_pbxClone = BackImage(pbx)
                        m_bMouseDown = False
                    End If
                End If
            Case Tools.State

        End Select
    End Sub

    Friend Sub DrawImage(ByVal sender As State, ByVal pbx As PictureBox)
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

    Public Sub New(ByVal strName As String, ByVal pbx As PictureBox, ByVal p As Point)
        m_strName = strName
        m_bmpCircle = m_Objects.Circle
        DrawState(pbx, p)
        m_bmpHighlightedCircle = m_Objects.Circle_Highlight
    End Sub
End Class
