Public Class Objects
    Private m_bmpHighlightedCircle As Bitmap
    Private m_bmpCircle As Bitmap
    Private m_bmpCircle_Small As Bitmap
    Private m_bmpCircle_Small_Transparent As Bitmap
    Private m_pntDimension As New Point(82, 82)

    Public ReadOnly Property Circle_Highlight As Bitmap
        Get
            Return m_bmpHighlightedCircle
        End Get
    End Property

    Public ReadOnly Property Circle As Bitmap
        Get
            Return m_bmpCircle
        End Get
    End Property

    Private Sub DrawHighlightedCircle()
        m_bmpHighlightedCircle = New Bitmap(m_pntDimension.X, m_pntDimension.Y)
        Dim g As Graphics = Graphics.FromImage(m_bmpHighlightedCircle)
        Dim DrawE_dim As Integer = CInt(CircleDiam) + 1
        Dim FillE_dim As Integer = CInt(CircleDiam)
        Dim DrawE_Location As Integer = CInt(m_pntDimension.X / 2) - CInt(CircleDiam / 2) - 1
        Dim FillE_Location As Integer = CInt(m_pntDimension.X / 2) - CInt(CircleDiam / 2)
        g.FillEllipse(Brushes.Green, FillE_Location, FillE_Location, FillE_dim, FillE_dim)
        g.DrawEllipse(New Pen(Brushes.Black, 3), DrawE_Location, DrawE_Location, DrawE_dim, DrawE_dim)
        g.Dispose()
    End Sub

    Private Sub DrawHighlightedCircle2()
        m_bmpHighlightedCircle = New Bitmap(m_pntDimension.X, m_pntDimension.Y)
        Dim g As Graphics = Graphics.FromImage(m_bmpHighlightedCircle)
        Dim DrawE_dim As Integer = CInt(CircleDiam) + 1
        Dim FillE_dim As Integer = CInt(CircleDiam)
        Dim DrawE_Location As Integer = CInt(m_pntDimension.X / 2) - CInt(CircleDiam / 2) - 1
        Dim FillE_Location As Integer = CInt(m_pntDimension.X / 2) - CInt(CircleDiam / 2)
        Dim dimSmallCircle As New Point(m_bmpCircle_Small_Transparent.Width, m_bmpCircle_Small_Transparent.Height)

        g.DrawImage(m_bmpCircle_Small_Transparent, FillE_Location - CInt(dimSmallCircle.X / 2), FillE_Location + CInt(CircleDiam / 2) - CInt(dimSmallCircle.Y / 2))
        g.DrawImage(m_bmpCircle_Small_Transparent, FillE_Location + CInt(CircleDiam / 2) - CInt(dimSmallCircle.X / 2), FillE_Location - CInt(dimSmallCircle.Y / 2))
        g.DrawImage(m_bmpCircle_Small_Transparent, FillE_Location + CircleDiam - CInt(dimSmallCircle.X / 2), FillE_Location + CInt(CircleDiam / 2) - CInt(dimSmallCircle.Y / 2))
        g.DrawImage(m_bmpCircle_Small_Transparent, FillE_Location + CInt(CircleDiam / 2) - CInt(dimSmallCircle.X / 2), FillE_Location + CircleDiam - CInt(dimSmallCircle.Y / 2))

        g.Dispose()

    End Sub

    Private Sub DrawCircle()
        m_bmpCircle = New Bitmap(m_pntDimension.X, m_pntDimension.Y)
        Dim g As Graphics = Graphics.FromImage(m_bmpCircle)
        Dim DrawE_dim As Integer = CInt(CircleDiam) + 1
        Dim FillE_dim As Integer = CInt(CircleDiam)
        Dim DrawE_Location As Integer = CInt(m_pntDimension.X / 2) - CInt(CircleDiam / 2) - 1
        Dim FillE_Location As Integer = CInt(m_pntDimension.X / 2) - CInt(CircleDiam / 2)

        g.FillEllipse(Brushes.Yellow, FillE_Location, FillE_Location, FillE_dim, FillE_dim)
        g.DrawEllipse(New Pen(Brushes.Black, 3), DrawE_Location, DrawE_Location, DrawE_dim, DrawE_dim)
        g.DrawRectangle(Pens.Black, 0, 0, m_pntDimension.X, m_pntDimension.Y)
        g.Dispose()
    End Sub

    Private Sub SmallCircle()
        m_bmpCircle_Small = New Bitmap(CInt(m_pntDimension.X * 0.23), CInt(m_pntDimension.Y * 0.23))
        Dim g As Graphics = Graphics.FromImage(m_bmpCircle_Small)
        Dim DrawE_dim As Integer = CInt(m_pntDimension.X * 0.2) - 3
        Dim FillE_dim As Integer = CInt(m_pntDimension.X * 0.2) - 5
        Dim DrawE_Location As Integer = 1
        Dim FillE_Location As Integer = 2

        g.FillEllipse(Brushes.Yellow, FillE_Location, FillE_Location, FillE_dim, FillE_dim)
        g.DrawEllipse(New Pen(Brushes.Black, 3), DrawE_Location, DrawE_Location, DrawE_dim, DrawE_dim)
        g.Dispose()
    End Sub

    Private Sub SmallCircle_Transparent()
        m_bmpCircle_Small_Transparent = New Bitmap(CInt(CircleDiam * 0.2), CInt(CircleDiam * 0.2))
        Dim g As Graphics = Graphics.FromImage(m_bmpCircle_Small_Transparent)
        Dim DrawE_dim As Integer = CInt(CircleDiam * 0.2) - 3
        Dim FillE_dim As Integer = CInt(CircleDiam * 0.2) - 5
        Dim DrawE_Location As Integer = 1
        Dim FillE_Location As Integer = 2

        g.FillEllipse(New SolidBrush(Color.FromArgb(128, Color.Red)), FillE_Location, FillE_Location, FillE_dim, FillE_dim)
        g.DrawEllipse(New Pen(Color.FromArgb(128, Color.Black), 2), DrawE_Location, DrawE_Location, DrawE_dim, DrawE_dim)
        g.Dispose()
    End Sub

    Public Sub New()
        DrawCircle()
        SmallCircle_Transparent()
        DrawHighlightedCircle2()
    End Sub
End Class
