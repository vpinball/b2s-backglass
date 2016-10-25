Imports System

Public Class Processes

    Private Declare Function EnumWindows Lib "user32.dll" (ByVal lpEnumFunc As EnumWindowsProc, ByVal lParam As Int32) As Int32
    Private Declare Function IsWindowVisible Lib "user32.dll" (ByVal hwnd As IntPtr) As Boolean
    Private Delegate Function EnumWindowsProc(ByVal hwnd As IntPtr, ByVal lParam As Int32) As Boolean
    Private Declare Function GetWindowText Lib "user32.dll" Alias "GetWindowTextA" (ByVal hwnd As IntPtr, ByVal lpString As String, ByVal cch As Int32) As Int32
    Private Declare Function GetWindowTextLength Lib "user32.dll" Alias "GetWindowTextLengthA" (ByVal hwnd As IntPtr) As Int32
    Private Declare Function GetWindowLong Lib "user32.dll" Alias "GetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Int32) As Int32
    Private Declare Function GetParent Lib "user32.dll" (ByVal intptr As IntPtr) As IntPtr

    Private windowlist As List(Of ProcInfo)
    Private _tablename As String = String.Empty
    Private _tablecount As Integer = 0
    Private _tablehandle As IntPtr = 0
    Private _backglassformhandle As IntPtr = 0
    Private _ishyperpinrunning As Boolean = False
    
    Private Const GWL_HWNDPARENT As Int32 = -8

    Private Class ProcInfo
        Public Name As String
        Public Handle As IntPtr

        Public Sub New(ByVal _name As String, ByVal _handle As IntPtr)
            Name = _name
            Handle = _handle
        End Sub
    End Class

    Public Sub New()
        RefreshWindowList()
        For Each proc As ProcInfo In windowlist
            If Not String.IsNullOrEmpty(proc.Name) Then
                If proc.Name.StartsWith("Visual Pinball - ", StringComparison.CurrentCultureIgnoreCase) Then
                    If String.IsNullOrEmpty(_tablename) Then
                        _tablename = proc.Name.Substring(17)
                        If _tablename.StartsWith("[") AndAlso Not _tablename.EndsWith("]") Then
                            Dim i As Integer = _tablename.Length - 1
                            Dim found As Boolean = False
                            Do While i > 0
                                If _tablename.Substring(i, 1) = "]" Then
                                    found = True
                                    Exit Do
                                End If
                                i -= 1
                            Loop
                            If found Then
                                _tablename = _tablename.Substring(0, i + 1)
                            End If
                        End If
                        If _tablename.StartsWith("[") Then _tablename = _tablename.Substring(1)
                        If _tablename.EndsWith("]") Then _tablename = _tablename.Substring(0, _tablename.Length - 1)
                        If _tablename.EndsWith("*") Then _tablename = _tablename.Substring(0, _tablename.Length - 1)
                        If _tablename.EndsWith(".vpt") Then _tablename = _tablename.Substring(0, _tablename.Length - 4)
                        If _tablename.EndsWith("*") Then _tablename = _tablename.Substring(0, _tablename.Length - 1)
                    End If
                    _tablecount += 1
                End If
                If proc.Name.StartsWith("Visual Pinball Player", StringComparison.CurrentCultureIgnoreCase) Then
                    _tablehandle = proc.Handle
                End If
                If proc.Name.Equals("Form1") Then
                    _backglassformhandle = proc.Handle
                End If
                If proc.Name.Equals("HyperPin", StringComparison.CurrentCultureIgnoreCase) Then
                    _ishyperpinrunning = True
                End If
            End If
        Next
    End Sub

    Public Sub KillProcess(ByVal processname As String)

        Dim proc As Process() = Process.GetProcesses

        For i As Integer = proc.GetUpperBound(0) To 0 Step -1
            If proc(i).ProcessName = processname Then
                proc(i).Kill()
            End If
        Next

    End Sub

    Public ReadOnly Property TableName() As String
        Get
            Return _tablename
        End Get
    End Property
    Public ReadOnly Property TableCount() As Integer
        Get
            Return _tablecount
        End Get
    End Property
    Public ReadOnly Property TableHandle() As IntPtr
        Get
            Return _tablehandle
        End Get
    End Property
    Public ReadOnly Property BackglassFormHandle() As IntPtr
        Get
            Return _backglassformhandle
        End Get
    End Property
    Public ReadOnly Property IsHyperpinRunning() As Boolean
        Get
            Return _ishyperpinrunning
        End Get
    End Property

    Private Function EnumWinProc(ByVal hwnd As IntPtr, ByVal lParam As Int32) As Boolean
        If IsWindowVisible(hwnd) AndAlso GetParent(hwnd) = IntPtr.Zero AndAlso GetWindowLong(hwnd, GWL_HWNDPARENT) = 0 Then
            Dim str As String = String.Empty.PadLeft(GetWindowTextLength(hwnd) + 1)
            GetWindowText(hwnd, str, str.Length)
            If Not String.IsNullOrEmpty(str.Substring(0, str.Length - 1)) Then windowlist.Add(New ProcInfo(str.Substring(0, str.Length - 1), hwnd))
        End If
        EnumWinProc = True
    End Function
    Private Sub RefreshWindowList()
        windowlist = New List(Of ProcInfo)
        EnumWindows(AddressOf EnumWinProc, CInt(True))
    End Sub

End Class
