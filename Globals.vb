Module Globals

    Public m_Objects As New Objects

    Public Enum Tools
        Pointer
        State
    End Enum

    Public Const CircleDiam As Integer = 60 'actual diameter - 1. Must always be an even number.
    Public Const Radius As Integer = CInt(CircleDiam / 2)

    Public Function SmallCircleDiam() As Integer
        Dim smallDiam As Integer = Math.Floor(CircleDiam * 0.15)
        Return CInt(IIf(smallDiam Mod 2 = 0, smallDiam, smallDiam + 1)) ' Must always be an even number.
    End Function

    Public Function SmallCircleRadius() As Integer
        Return Math.Floor(SmallCircleDiam() / 2)
    End Function

End Module
