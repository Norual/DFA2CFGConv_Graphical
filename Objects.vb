Public Class Objects
    Private m_bmpHighlightedCircle As Bitmap
    Private m_bmpCircle As Bitmap
    Private m_bmpCircle_Small As Bitmap
    Private m_bmpCircle_Small_Transparent As Bitmap
    Private m_bmpSize As New Size(81, 81) 'x and y must always be an odd number


    Public ReadOnly Property Circle_Highlight As Bitmap
        Get
            Return m_bmpHighlightedCircle.Clone
        End Get
    End Property

    Public ReadOnly Property Circle As Bitmap
        Get
            Return m_bmpCircle.Clone
        End Get
    End Property

    Public ReadOnly Property SmallCircle As Bitmap
        Get
            Return m_bmpCircle_Small.Clone
        End Get
    End Property

    Public ReadOnly Property SmallCircle_Transparent As Bitmap
        Get
            Return m_bmpCircle_Small_Transparent.Clone
        End Get
    End Property

    Private Sub DrawHighlightedCircle()

        m_bmpHighlightedCircle = New Bitmap(m_bmpSize.Width, m_bmpSize.Height)
        Dim g As Graphics = Graphics.FromImage(m_bmpHighlightedCircle)
        Dim bmpHalfWidth As Integer = Math.Floor(m_bmpCircle_Small_Transparent.Width / 2)
        Dim bmpHalfHeight As Integer = Math.Floor(m_bmpCircle_Small_Transparent.Height / 2)

        g.TranslateTransform(Math.Floor(m_bmpSize.Width / 2), Math.Floor(m_bmpSize.Height / 2))

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
        m_bmpCircle = New Bitmap(m_bmpSize.Width, m_bmpSize.Height)
        Dim g As Graphics = Graphics.FromImage(m_bmpCircle)
        g.TranslateTransform(Math.Floor(m_bmpSize.Width / 2), Math.Floor(m_bmpSize.Height / 2))
        g.FillEllipse(Brushes.Yellow, -Radius, -Radius, CircleDiam, CircleDiam)
        g.DrawEllipse(New Pen(Brushes.Black, 3), -Radius, -Radius, CircleDiam, CircleDiam)
        g.Dispose()
    End Sub

    Private Sub DrawSmallCircle()
        Dim cDiam As Integer = SmallCircleDiam()
        Dim cRadius As Integer = SmallCircleRadius()
        Dim bmpSize As Integer = cDiam + 3

        m_bmpCircle_Small = New Bitmap(bmpSize, bmpSize)

        Dim g As Graphics = Graphics.FromImage(m_bmpCircle_Small)

        g.TranslateTransform(Math.Floor(m_bmpCircle_Small.Width / 2), Math.Floor(m_bmpCircle_Small.Height / 2))
        g.FillEllipse(Brushes.YellowGreen, -cRadius, -cRadius, cDiam, cDiam)
        g.DrawEllipse(New Pen(Brushes.Black, 1), -cRadius, -cRadius, cDiam, cDiam)
        g.Dispose()
    End Sub

    Private Sub DrawSmallCircle_Transparent()
        Dim cDiam As Integer = SmallCircleDiam()
        Dim cRadius As Integer = SmallCircleRadius()
        Dim bmpSize As Integer = cDiam + 3

        m_bmpCircle_Small_Transparent = New Bitmap(bmpSize, bmpSize)

        Dim g As Graphics = Graphics.FromImage(m_bmpCircle_Small_Transparent)

        g.TranslateTransform(Math.Floor(m_bmpCircle_Small_Transparent.Width / 2), Math.Floor(m_bmpCircle_Small_Transparent.Height / 2))
        g.FillEllipse(New SolidBrush(Color.FromArgb(128, Color.Red)), -cRadius, -cRadius, cDiam, cDiam)
        g.DrawEllipse(New Pen(Color.FromArgb(128, Color.Black), 1), -cRadius, -cRadius, cDiam, cDiam)
        g.Dispose()
    End Sub

    Public Sub New()
        DrawCircle()
        DrawSmallCircle()
        DrawSmallCircle_Transparent()
        DrawHighlightedCircle()
    End Sub
End Class
