Public Class StateWrapper
    Private WithEvents m_PBox As PictureBox
    Private m_Diag As Diagram

    Private m_Tool As Diagram.Tools = Diagram.Tools.State

    Private m_listOfStates As New List(Of State)

    Private m_bMouseEntered As Boolean = False
    Private m_bMouseDown As Boolean = False

    Private m_pDiff As Point

    Private m_pBoxClone As Bitmap

    Private m_intSelectedIndex As Integer

    Private m_SelectedState As State

    Public Sub New(ByVal diag As Diagram)
        m_Diag = diag
        m_PBox = diag.PBox
    End Sub

    Friend Property Tool As Diagram.Tools
        Get
            Return m_Tool
        End Get
        Set(value As Diagram.Tools)
            m_Tool = value
        End Set
    End Property

    Public Sub AddState(ByVal Name As String, ByVal location As Point)
        Dim listCount As Integer = m_listOfStates.Count
        m_listOfStates.Add(New State(Name & listCount, m_Diag, location))
    End Sub

    Private Sub ReDraw()
        m_PBox.Image = Nothing
        For Each st As State In m_listOfStates
            If st.Name = m_SelectedState.Name Then Continue For
            st.ReDraw()
        Next
    End Sub

    Private Function GetSelectedState(ByVal location As Point) As State
        For Each st In m_listOfStates
            If m_SelectedState Is Nothing OrElse m_SelectedState.Name = st.Name Then
                Dim bmpCircle As Bitmap = G_Objects.Circle
                Dim pLocation As New Point(st.Location.X + Math.Floor(bmpCircle.Width / 2), st.Location.Y + Math.Floor(bmpCircle.Height / 2))
                Dim a As Integer = Math.Abs(pLocation.X - location.X)
                Dim b As Integer = Math.Abs(pLocation.Y - location.Y)
                Dim hypotenuse As Integer = Math.Sqrt((a ^ 2) + (b ^ 2))
                If hypotenuse < Radius + 1 And Not m_bMouseEntered Then
                    st.DrawOnMouseEntered(Nothing, m_Tool)
                    m_bMouseEntered = True
                    Return st
                ElseIf hypotenuse > Radius + 5 And m_bMouseEntered Then
                    m_bMouseEntered = False
                    st.DrawOnMouseLeave(Nothing, m_Tool)
                    Return Nothing
                End If
            End If
        Next

        Return m_SelectedState
    End Function

    Private Sub PBox_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles m_PBox.MouseMove
        Select Case m_Tool
            Case Diagram.Tools.Pointer
                If Not m_bMouseDown Then
                    m_SelectedState = GetSelectedState(e.Location)
                    If Not m_SelectedState Is Nothing Then
                        m_SelectedState.RaiseMouseMoveEvent(e)
                    End If
                ElseIf m_bMouseDown Then
                    m_PBox.Image = Nothing
                    m_SelectedState.DrawOnMouseMove(e, m_Tool)
                End If

            Case Diagram.Tools.State

        End Select
        ' End If
    End Sub

    Private Sub PBox_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles m_PBox.MouseDown
        Select Case m_Tool
            Case Diagram.Tools.Pointer
                If m_bMouseEntered Then
                    If Not m_bMouseDown Then
                        ReDraw()
                        m_SelectedState.DrawOnMouseDown(e, m_Tool)
                        m_bMouseDown = True
                    End If
                End If
            Case Diagram.Tools.State

        End Select
    End Sub

    Private Sub PBox_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles m_PBox.MouseUp
        Select Case m_Tool
            Case Diagram.Tools.Pointer
                If m_bMouseEntered Then
                    If m_bMouseDown Then
                        ReDraw()
                        m_SelectedState.DrawOnMouseUp(e, Tool)
                        m_bMouseDown = False
                    End If
                End If
            Case Diagram.Tools.State

        End Select
    End Sub

    Private Function GetCurrentDisplayImage(Optional bForceCreateNew As Boolean = False) As Bitmap
        If m_PBox.Image Is Nothing Or bForceCreateNew Then
            Return New Bitmap(m_PBox.Width, m_PBox.Height)
        Else
            Return m_PBox.Image.Clone
        End If
    End Function
End Class
