Public Class Objects
    Private m_bmpHighlightedCircle As Bitmap
    Private m_bmpCircle As Bitmap
    Private m_bmpCircle_Small As Bitmap
    Private m_bmpCircle_Small_Transparent As Bitmap
    Private m_pntDimension As New Point(81, 81) 'x and y must always be odd

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
        Dim bmpHalfWidth As Integer = Math.Floor(m_bmpCircle_Small_Transparent.Width / 2)
        Dim bmpHalfHeight As Integer = Math.Floor(m_bmpCircle_Small_Transparent.Height / 2)

        g.TranslateTransform(Math.Floor(m_pntDimension.X / 2), Math.Floor(m_pntDimension.Y / 2))

        g.DrawImage(m_bmpCircle_Small_Transparent, -bmpHalfWidth, -Radius - bmpHalfHeight)
        g.DrawImage(m_bmpCircle_Small_Transparent, -Radius - bmpHalfWidth, -bmpHalfHeight)
        g.DrawImage(m_bmpCircle_Small_Transparent, -bmpHalfWidth, Radius - bmpHalfHeight)
        g.DrawImage(m_bmpCircle_Small_Transparent, Radius - bmpHalfWidth, -bmpHalfHeight)

        g.RotateTransform(45)

        g.DrawImage(m_bmpCircle_Small_Transparent, -bmpHalfWidth, -Radius - bmpHalfHeight)
        g.DrawImage(m_bmpCircle_Small_Transparent, -Radius - bmpHalfWidth, -bmpHalfHeight)
        g.DrawImage(m_bmpCircle_Small_Transparent, -bmpHalfWidth, Radius - bmpHalfHeight)
        g.DrawImage(m_bmpCircle_Small_Transparent, Radius - bmpHalfWidth, -bmpHalfHeight)

        g.Dispose()

    End Sub

    Private Sub DrawCircle()
        m_bmpCircle = New Bitmap(m_pntDimension.X, m_pntDimension.Y)
        Dim g As Graphics = Graphics.FromImage(m_bmpCircle)
        g.TranslateTransform(Math.Floor(m_pntDimension.X / 2), Math.Floor(m_pntDimension.Y / 2))
        g.FillEllipse(Brushes.Yellow, -Radius, -Radius, CircleDiam, CircleDiam)
        g.DrawEllipse(New Pen(Brushes.Black, 3), -Radius, -Radius, CircleDiam, CircleDiam)
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
        Dim cDiam As Integer = CInt(CircleDiam * 0.15)
        cDiam = CInt(IIf(cDiam Mod 2 = 0, cDiam, cDiam + 1))
        Dim bmpSize As Integer = cDiam + 3

        m_bmpCircle_Small_Transparent = New Bitmap(bmpSize, bmpSize)

        Dim g As Graphics = Graphics.FromImage(m_bmpCircle_Small_Transparent)
        Dim cRadius As Integer = Math.Floor(cDiam / 2)

        g.TranslateTransform(Math.Floor(m_bmpCircle_Small_Transparent.Width / 2), Math.Floor(m_bmpCircle_Small_Transparent.Height / 2))
        g.FillEllipse(New SolidBrush(Color.FromArgb(128, Color.Red)), -cRadius, -cRadius, cDiam, cDiam)
        g.DrawEllipse(New Pen(Color.FromArgb(128, Color.Black), 1), -cRadius, -cRadius, cDiam, cDiam)
        g.Dispose()
    End Sub

    Public Sub New()
        DrawCircle()
        SmallCircle_Transparent()
        DrawHighlightedCircle()
    End Sub
End Class
