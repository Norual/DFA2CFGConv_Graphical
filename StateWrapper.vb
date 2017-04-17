Public Class StateWrapper
#Region "Old Code"
    'Private WithEvents m_PBox As PictureBox
    'Private m_Diag As Diagram

    'Private m_Tool As Diagram.Tools = Diagram.Tools.State

    'Private m_listOfStates As New List(Of State)

    'Private m_bMouseEntered As Boolean = False
    'Private m_bMouseDown As Boolean = False

    'Private m_pDiff As Point

    'Private m_pBoxClone As Bitmap

    'Private m_intSelectedIndex As Integer

    'Private m_SelectedState As State

    'Public Sub New(ByVal diag As Diagram)
    '    m_Diag = diag
    '    m_PBox = diag.PBox
    'End Sub

    'Friend Property Tool As Diagram.Tools
    '    Get
    '        Return m_Tool
    '    End Get
    '    Set(value As Diagram.Tools)
    '        m_Tool = value
    '    End Set
    'End Property

    'Public Sub AddState(ByVal Name As String, ByVal location As Point)
    '    Dim listCount As Integer = m_listOfStates.Count
    '    m_listOfStates.Add(New State(Name & listCount, m_Diag, location))
    'End Sub

    'Private Sub ReDraw()
    '    m_PBox.Image = Nothing
    '    For Each st As State In m_listOfStates
    '        If st.Name = m_SelectedState.Name Then Continue For
    '        st.ReDraw()
    '    Next
    'End Sub

    'Private Function GetSelectedState(ByVal location As Point) As State
    '    For Each st In m_listOfStates
    '        If m_SelectedState Is Nothing OrElse m_SelectedState.Name = st.Name Then
    '            Dim bmpCircle As Bitmap = G_Objects.Circle
    '            Dim pLocation As New Point(st.Location.X + Math.Floor(bmpCircle.Width / 2), st.Location.Y + Math.Floor(bmpCircle.Height / 2))
    '            Dim a As Integer = Math.Abs(pLocation.X - location.X)
    '            Dim b As Integer = Math.Abs(pLocation.Y - location.Y)
    '            Dim hypotenuse As Integer = Math.Sqrt((a ^ 2) + (b ^ 2))
    '            If hypotenuse < Radius + 1 And Not m_bMouseEntered Then
    '                st.DrawOnMouseEntered(Nothing, m_Tool)
    '                m_bMouseEntered = True
    '                Return st
    '            ElseIf hypotenuse > Radius + 5 And m_bMouseEntered Then
    '                m_bMouseEntered = False
    '                st.DrawOnMouseLeave(Nothing, m_Tool)
    '                Return Nothing
    '            End If
    '        End If
    '    Next

    '    Return m_SelectedState
    'End Function

    'Private Sub Prioritize(ByVal state As State)
    '    Dim states() As State
    '    ReDim states(m_listOfStates.Count)

    '    m_listOfStates.CopyTo(states)
    '    m_listOfStates.Clear()

    '    For Each st As State In states
    '        If st Is Nothing Then Exit For
    '        If st.Name = state.Name Then Continue For
    '        m_listOfStates.Add(st)
    '    Next

    '    m_listOfStates.Add(state)
    'End Sub

    'Private Sub PBox_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles m_PBox.MouseMove
    '    Select Case m_Tool
    '        Case Diagram.Tools.Pointer
    '            If Not m_bMouseDown Then
    '                m_SelectedState = GetSelectedState(e.Location)

    '                If Not m_SelectedState Is Nothing Then
    '                    m_SelectedState.RaiseMouseMoveEvent(e)
    '                End If
    '            ElseIf m_bMouseDown Then
    '                m_PBox.Image = Nothing
    '                m_SelectedState.DrawOnMouseMove(e, m_Tool)
    '            End If

    '        Case Diagram.Tools.State

    '    End Select
    'End Sub

    'Private Sub PBox_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles m_PBox.MouseDown
    '    Select Case m_Tool
    '        Case Diagram.Tools.Pointer
    '            If m_bMouseEntered Then
    '                If Not m_bMouseDown Then
    '                    ReDraw()
    '                    m_SelectedState.DrawOnMouseDown(e, m_Tool)
    '                    m_bMouseDown = True
    '                End If
    '            End If
    '        Case Diagram.Tools.State

    '    End Select
    'End Sub

    'Private Sub PBox_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles m_PBox.MouseUp
    '    Select Case m_Tool
    '        Case Diagram.Tools.Pointer
    '            If m_bMouseEntered Then
    '                If m_bMouseDown Then
    '                    ReDraw()
    '                    m_SelectedState.DrawOnMouseUp(e, Tool)
    '                    m_bMouseDown = False
    '                End If
    '            End If
    '        Case Diagram.Tools.State

    '    End Select
    'End Sub

    'Private Function GetCurrentDisplayImage(Optional bForceCreateNew As Boolean = False) As Bitmap
    '    If m_PBox.Image Is Nothing Or bForceCreateNew Then
    '        Return New Bitmap(m_PBox.Width, m_PBox.Height)
    '    Else
    '        Return m_PBox.Image.Clone
    '    End If
    'End Function
#End Region
    Private WithEvents C_PBox As PictureBox

    Private C_listOfStates As New List(Of State)

    Private C_bEnabled As Boolean = False
    Private C_bMouseDown As Boolean = False

    Private C_stateCurrent As State

    Private C_bmpBackImage As Bitmap

    Private C_pntDiff As Point

    Public Sub New(ByVal pBox As PictureBox)
        C_PBox = pBox
    End Sub

    Public Property Enabled As Boolean
        Get
            Return C_bEnabled
        End Get
        Set(value As Boolean)
            C_bEnabled = value
        End Set
    End Property

    Public Sub AddState(ByVal name As String, ByVal location As Point)
        Dim st As New State(C_PBox)
        st.Draw(location)
        st.Name = name & C_listOfStates.Count
        C_listOfStates.Add(st)
    End Sub

    Private Sub PBox_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles C_PBox.MouseMove
        If C_bEnabled Then
            If Not C_bMouseDown Then
                C_stateCurrent = State_MouseEntered(e.Location)
            Else
                MoveState(New Point(e.Location.X + C_pntDiff.X, e.Location.Y + C_pntDiff.Y))
            End If
        End If
    End Sub

    Private Sub PBox_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles C_PBox.MouseDown
        If C_bEnabled Then
            If Not C_stateCurrent Is Nothing Then
                If Not C_bMouseDown Then
                    CreateBackImage()
                    C_bmpBackImage = GetCurrentImage()
                    C_bMouseDown = True
                    C_pntDiff = New Point(C_stateCurrent.Center.X - e.Location.X, C_stateCurrent.Center.Y - e.Location.Y)
                    C_stateCurrent.ReDraw()
                End If
            End If
        End If
    End Sub

    Private Sub PBox_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles C_PBox.MouseUp
        If C_bEnabled Then
            If Not C_stateCurrent Is Nothing Then
                If C_bMouseDown Then
                    C_bMouseDown = False
                End If
            End If
        End If
    End Sub

    Private Function State_MouseEntered(ByVal location As Point) As State
        For Each st As State In C_listOfStates
            If C_stateCurrent Is Nothing OrElse C_stateCurrent.Name = st.Name Then
                If st.MouseEntered(location) Then
                    Return st
                End If
            End If
        Next
        Return Nothing
    End Function

    Private Sub CreateBackImage()
        C_PBox.Image = Nothing
        For Each st As State In C_listOfStates
            If Not C_stateCurrent.Name = st.Name Then
                st.ReDraw()
            End If
        Next
    End Sub

    Private Function GetCurrentImage() As Bitmap
        If C_PBox.Image Is Nothing Then
            Return New Bitmap(C_PBox.Width, C_PBox.Height)
        Else
            Return C_PBox.Image.Clone
        End If
    End Function

    Private Sub MoveState(ByVal location As Point)
        C_PBox.Image = C_bmpBackImage.Clone
        C_stateCurrent.Draw(location)
    End Sub
End Class
