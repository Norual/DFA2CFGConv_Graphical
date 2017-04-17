Public Class State
#Region "Old Code"
    'Private WithEvents m_diag As Diagram
    'Private WithEvents m_PBox As PictureBox

    'Private Shared m_pBoxClone As Bitmap

    'Private m_bmpImage As Bitmap
    'Private m_bmpCircle As Bitmap
    'Private m_bmpHighlightedCircle As Bitmap

    'Private m_ToolSelected As Diagram.Tools = Diagram.Tools.State

    'Private m_strName As String

    'Private m_bMouseEntered As Boolean = False
    'Private m_bMouseDown As Boolean = False

    'Private m_pLocation As Point
    'Private m_pDiff As Point

    'Private m_CollectionOfAnchorPoints As New Dictionary(Of String, AnchorPoint)

    'Friend Event MouseEntered()
    'Friend Event MouseLeave()
    'Friend Event MouseMove(ByVal e As MouseEventArgs)
    'Friend Event MouseUp(ByVal sender As PictureBox, ByVal p As Point)

    'Public Property Name As String
    '    Get
    '        Return m_strName
    '    End Get
    '    Set(value As String)
    '        m_strName = value
    '    End Set
    'End Property

    'Public ReadOnly Property Location As Point
    '    Get
    '        Return m_pLocation
    '    End Get
    'End Property

    'Public Property PBox As PictureBox
    '    Get
    '        Return m_PBox
    '    End Get
    '    Set(value As PictureBox)
    '        m_PBox = value
    '    End Set
    'End Property

    'Public Sub ReDraw()
    '    Dim bmp As Bitmap = GetCurrentDisplayImage()
    '    Dim g As Graphics = Graphics.FromImage(bmp)
    '    g.DrawImage(m_bmpImage, 0, 0)
    '    m_PBox.Image = bmp.Clone
    '    bmp.Dispose()
    '    g.Dispose()
    'End Sub

    'Public Sub RaiseMouseMoveEvent(ByVal e As MouseEventArgs)
    '    RaiseEvent MouseMove(e)
    'End Sub

    'Public Sub DrawOnMouseMove(ByVal e As MouseEventArgs, ByVal tool As Diagram.Tools)
    '    Select Case tool
    '        Case Diagram.Tools.Pointer
    '            MoveState(e.Location)
    '        Case Diagram.Tools.State
    '    End Select
    'End Sub

    'Public Sub DrawOnMouseEntered(ByVal e As MouseEventArgs, ByVal tool As Diagram.Tools)
    '    Select Case tool
    '        Case Diagram.Tools.Pointer
    '            m_pBoxClone = GetCurrentDisplayImage()
    '            RaiseEvent MouseEntered()
    '        Case Diagram.Tools.State
    '    End Select
    'End Sub

    'Public Sub DrawOnMouseLeave(ByVal e As MouseEventArgs, ByVal tool As Diagram.Tools)
    '    Select Case tool
    '        Case Diagram.Tools.Pointer
    '            m_PBox.Image = m_pBoxClone.Clone
    '            RaiseEvent MouseLeave()
    '        Case Diagram.Tools.State
    '    End Select
    'End Sub

    'Public Sub DrawOnMouseDown(ByVal e As MouseEventArgs, ByVal tool As Diagram.Tools)
    '    Select Case tool
    '        Case Diagram.Tools.Pointer
    '            m_pDiff = New Point(e.Location.X - m_pLocation.X, e.Location.Y - m_pLocation.Y)
    '            ' m_pBoxClone = GetCurrentDisplayImage()
    '            MoveState(e.Location)
    '        Case Diagram.Tools.State

    '    End Select
    'End Sub

    'Public Sub DrawOnMouseUp(ByVal e As MouseEventArgs, ByVal tool As Diagram.Tools)
    '    Select Case tool
    '        Case Diagram.Tools.Pointer
    '            'm_pBoxClone = GetCurrentDisplayImage()
    '            MoveState(e.Location)
    '            UpdateAnchorPointsLocation()
    '            'DrawOnMouseEntered(e, tool)
    '        Case Diagram.Tools.State

    '    End Select
    'End Sub

    'Private Sub MoveState(ByVal p As Point)
    '    Dim bmp As Bitmap = GetCurrentDisplayImage(True)
    '    Dim g As Graphics = Graphics.FromImage(bmp)
    '    m_pLocation = New Point(p.X - m_pDiff.X, p.Y - m_pDiff.Y)
    '    g.DrawImage(m_bmpCircle, m_pLocation)
    '    m_bmpImage = bmp.Clone
    '    g.DrawImage(m_pBoxClone, 0, 0)
    '    g.DrawImage(m_bmpImage, 0, 0)
    '    m_PBox.Image = bmp.Clone
    '    bmp.Dispose()
    '    g.Dispose()
    'End Sub

    'Private Sub HighlightState(ByVal pbx As PictureBox, ByVal p As Point)
    '    Dim bmp As Bitmap = GetCurrentDisplayImage()
    '    Dim g As Graphics = Graphics.FromImage(bmp)
    '    g.DrawImage(m_bmpHighlightedCircle, m_pLocation)
    '    pbx.Image = bmp.Clone
    '    bmp.Dispose()
    '    g.Dispose()
    'End Sub

    'Private Sub DrawState(ByVal pbx As PictureBox, ByVal p As Point)
    '    Dim bmp As New Bitmap(pbx.Width, pbx.Height)
    '    Dim g As Graphics = Graphics.FromImage(bmp)
    '    m_pLocation = New Point(p.X - Math.Floor(m_bmpCircle.Width / 2), p.Y - Math.Floor(m_bmpCircle.Height / 2))
    '    g.DrawImage(m_bmpCircle, m_pLocation)
    '    m_bmpImage = bmp.Clone
    '    bmp.Dispose()
    '    g.Dispose()
    'End Sub

    'Private Function GetCurrentDisplayImage(Optional bForceCreateNew As Boolean = False) As Bitmap
    '    If m_PBox.Image Is Nothing Or bForceCreateNew Then
    '        Return New Bitmap(m_PBox.Width, m_PBox.Height)
    '    Else
    '        Return m_PBox.Image.Clone
    '    End If
    'End Function

    'Private Sub AddAnchcorPoints(ByVal pbx As PictureBox)
    '    Dim center As New Point(m_pLocation.X + Math.Floor(m_bmpCircle.Width / 2), m_pLocation.Y + Math.Floor(m_bmpCircle.Height / 2))

    '    m_CollectionOfAnchorPoints.Add("Anc1", New AnchorPoint("Anc1", Me, New Point(center.X - Radius, center.Y)))
    '    m_CollectionOfAnchorPoints.Add("Anc3", New AnchorPoint("Anc3", Me, New Point(center.X, center.Y + Radius)))
    '    m_CollectionOfAnchorPoints.Add("Anc5", New AnchorPoint("Anc5", Me, New Point(center.X + Radius, center.Y)))
    '    m_CollectionOfAnchorPoints.Add("Anc7", New AnchorPoint("Anc7", Me, New Point(center.X, center.Y - Radius)))

    '    Dim loc45deg As New Point(Math.Floor(Math.Cos(Math.PI / 4) * Radius), Math.Floor(Math.Sin(Math.PI / 4) * Radius))

    '    m_CollectionOfAnchorPoints.Add("Anc2", New AnchorPoint("Anc2", Me, New Point(center.X - loc45deg.X, center.Y - loc45deg.Y)))
    '    m_CollectionOfAnchorPoints.Add("Anc4", New AnchorPoint("Anc4", Me, New Point(center.X - loc45deg.X, center.Y + loc45deg.Y)))
    '    m_CollectionOfAnchorPoints.Add("Anc6", New AnchorPoint("Anc6", Me, New Point(center.X + loc45deg.X, center.Y + loc45deg.Y)))
    '    m_CollectionOfAnchorPoints.Add("Anc8", New AnchorPoint("Anc8", Me, New Point(center.X + loc45deg.X, center.Y - loc45deg.Y)))

    '    ' For Each anc As AnchorPoint In m_CollectionOfSmallCircles.Values
    '    'AddHandler Me.MouseEntered, AddressOf anc.MouseEnteredState
    '    'AddHandler Me.MouseLeave, AddressOf anc.MouseLeftState
    '    'AddHandler Me.MouseMove, AddressOf anc.MouseMove
    '    ' Next
    'End Sub

    'Private Sub UpdateAnchorPointsLocation()
    '    Dim origLoc As New Point(m_pLocation.X + Math.Floor(m_bmpCircle.Width / 2), m_pLocation.Y + Math.Floor(m_bmpCircle.Height / 2))

    '    m_CollectionOfAnchorPoints("Anc1").Location = New Point(origLoc.X - Radius, origLoc.Y)
    '    m_CollectionOfAnchorPoints("Anc3").Location = New Point(origLoc.X, origLoc.Y + Radius)
    '    m_CollectionOfAnchorPoints("Anc5").Location = New Point(origLoc.X + Radius, origLoc.Y)
    '    m_CollectionOfAnchorPoints("Anc7").Location = New Point(origLoc.X, origLoc.Y - Radius)

    '    Dim loc45deg As New Point(Math.Floor(Math.Cos(Math.PI / 4) * Radius), Math.Floor(Math.Sin(Math.PI / 4) * Radius))

    '    m_CollectionOfAnchorPoints("Anc2").Location = New Point(origLoc.X - loc45deg.X, origLoc.Y - loc45deg.Y)
    '    m_CollectionOfAnchorPoints("Anc4").Location = New Point(origLoc.X - loc45deg.X, origLoc.Y + loc45deg.Y)
    '    m_CollectionOfAnchorPoints("Anc6").Location = New Point(origLoc.X + loc45deg.X, origLoc.Y + loc45deg.Y)
    '    m_CollectionOfAnchorPoints("Anc8").Location = New Point(origLoc.X + loc45deg.X, origLoc.Y - loc45deg.Y)
    'End Sub

    'Public Sub New(ByVal strName As String, ByVal diag As Diagram, ByVal p As Point)
    '    m_diag = diag
    '    m_PBox = diag.PBox
    '    m_strName = strName
    '    m_bmpCircle = G_Objects.Circle
    '    DrawState(diag.PBox, p)
    '    m_bmpHighlightedCircle = G_Objects.Circle_Highlight
    '    AddAnchcorPoints(diag.PBox)
    'End Sub
#End Region
#Region "New Code"

    Private WithEvents C_PBox As PictureBox
    Private C_bmpCircle As Bitmap
    Private C_Center As Point
    Private C_pntImgLoc As Point
    Private C_strName As String

    Public Sub New(ByVal pBox As PictureBox)
        C_PBox = pBox
        C_bmpCircle = G_Objects.Circle 
    End Sub

    Public Property Image As Bitmap
        Get
            Return C_bmpCircle.Clone
        End Get
        Set(value As Bitmap)
            C_bmpCircle = value.Clone
        End Set
    End Property

    Public Property Name As String
        Get
            Return C_strName
        End Get
        Set(value As String)
            C_strName = value
        End Set
    End Property

    Public ReadOnly Property Location As Point
        Get
            Return C_pntImgLoc
        End Get
    End Property

    Public ReadOnly Property Center As Point
        Get
            Return C_Center
        End Get
    End Property

    Public Sub ReDraw()
        Dim bmp As Bitmap = GetCurrentImage()
        Dim g As Graphics = Graphics.FromImage(bmp)

        g.DrawImage(C_bmpCircle, C_pntImgLoc)
        C_PBox.Image = bmp.Clone
        g.Dispose()
        bmp.Dispose()
    End Sub

    Public Sub Draw(ByVal location As Point)
        Dim bmp As Bitmap = GetCurrentImage()
        Dim g As Graphics = Graphics.FromImage(bmp)
        C_Center = New Point(location.X, location.Y)
        C_pntImgLoc = New Point(C_Center.X - Math.Floor(C_bmpCircle.Width / 2), C_Center.Y - Math.Floor(C_bmpCircle.Height / 2))

        g.DrawImage(C_bmpCircle, C_pntImgLoc)
        C_PBox.Image = bmp.Clone
        g.Dispose()
        bmp.Dispose()
    End Sub

    Public Function MouseEntered(ByVal location As Point) As Boolean
        Dim xDiff As Integer = Math.Abs(C_Center.X - location.X)
        Dim yDiff As Integer = Math.Abs(C_Center.Y - location.Y)
        Dim hypotenuse As Integer = Math.Floor(Math.Sqrt((xDiff ^ 2) + (yDiff ^ 2)))
        If hypotenuse < Radius Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function GetCurrentImage() As Bitmap
        If C_PBox.Image Is Nothing Then
            Return New Bitmap(C_PBox.Width, C_PBox.Height)
        Else
            Return C_PBox.Image.Clone
        End If
    End Function

#End Region
End Class
