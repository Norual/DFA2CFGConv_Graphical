﻿Public Class AnchorPoint
    Private m_strName As String

    Private m_bmpSmallCircle As Bitmap
    Private m_bmpSmallCircle_Transparent As Bitmap
    Private m_bmpImage As Bitmap
    Private m_bmpImage_Trans As Bitmap
    Private m_bmpPbxClone As Bitmap

    Private m_pLocation As Point
    Private m_pCenter As Point

    Private m_bEnteredState As Boolean = False
    Private m_bMouseEntered As Boolean = False


    Friend Property Name As String
        Get
            Return m_strName
        End Get
        Set(value As String)
            m_strName = value
        End Set
    End Property

    Friend Property Location As Point
        Get
            Return m_pLocation
        End Get
        Set(value As Point)
            Dim bmpSize As Size = m_bmpSmallCircle.Size
            m_pCenter = New Point(value.X, value.Y)
            m_pLocation = New Point(value.X - CInt(Math.Floor(bmpSize.Width / 2)), value.Y - CInt(Math.Floor(bmpSize.Height / 2)))
        End Set
    End Property

    Friend Sub MouseEnteredState(ByVal sender As String, ByVal Pbx As PictureBox)
        m_bEnteredState = True
        ShowTransparentCircle(Pbx)
    End Sub

    Friend Sub MouseLeftState()
        m_bEnteredState = False
    End Sub

    Friend Sub MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        If m_bEnteredState Then
            Dim a As Integer = Math.Abs(m_pCenter.X - e.Location.X)
            Dim b As Integer = Math.Abs(m_pCenter.Y - e.Location.Y)
            Dim hypotenuse As Integer = Math.Sqrt((a ^ 2) + (b ^ 2))
            Dim radius As Integer = SmallCircleRadius()
            Dim pbx As PictureBox = CType(sender, PictureBox)
            If hypotenuse < radius And Not m_bMouseEntered Then
                m_bmpPbxClone = pbx.Image.Clone
                ShowCircle(pbx)
                m_bMouseEntered = True
            ElseIf hypotenuse > radius And m_bMouseEntered Then
                pbx.Image = m_bmpPbxClone.Clone
                m_bMouseEntered = False
            End If
        End If
    End Sub

    Friend Sub MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)

    End Sub

    Friend Sub MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)

    End Sub

    Friend Sub MouseEntered()

    End Sub

    Friend Sub MouseLeave()

    End Sub

    Private Sub ShowCircle(ByVal pbx As PictureBox)
        Dim bmp As Bitmap = pbx.Image.Clone
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.DrawImage(m_bmpSmallCircle, m_pLocation)
        pbx.Image = bmp.Clone
        g.Dispose()
        bmp.Dispose()
    End Sub

    Private Sub ShowTransparentCircle(ByVal pbx As PictureBox)
        Dim bmp As Bitmap = pbx.Image.Clone
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.DrawImage(m_bmpSmallCircle_Transparent, m_pLocation)
        pbx.Image = bmp.Clone
        g.Dispose()
        bmp.Dispose()
    End Sub

    Private Sub DrawPoint(ByVal pbx As PictureBox, ByVal p As Point)
        Dim bmp As New Bitmap(pbx.Width, pbx.Height)
        Dim bmp2 As New Bitmap(pbx.Width, pbx.Height)
        Dim bmpSize As Size = m_bmpSmallCircle.Size
        m_pCenter = New Point(p.X, p.Y)
        m_pLocation = New Point(p.X - CInt(Math.Floor(bmpSize.Width / 2)), p.Y - CInt(Math.Floor(bmpSize.Height / 2)))

        Dim g As Graphics = Graphics.FromImage(bmp)
        g.DrawImage(m_bmpSmallCircle, m_pLocation)
        m_bmpImage = bmp.Clone

        g = Graphics.FromImage(bmp2)
        g.DrawImage(m_bmpSmallCircle_Transparent, m_pLocation)
        m_bmpImage_Trans = bmp2.Clone

        g.Dispose()
        bmp.Dispose()
        bmp2.Dispose()
    End Sub

    Public Sub New(ByVal strName As String, ByVal pbx As PictureBox, ByVal p As Point)
        m_strName = strName
        m_bmpSmallCircle = m_Objects.SmallCircle
        m_bmpSmallCircle_Transparent = m_Objects.SmallCircle_Transparent
        DrawPoint(pbx, p)
    End Sub

End Class