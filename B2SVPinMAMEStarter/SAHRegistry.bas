Attribute VB_Name = "SAHRegistry"
' *****************************************************************************************************************
' NAME
' SAHRegistry.bas
'
' DESCRIPTION
' functions for manipulating your registry
' *****************************************************************************************************************
Option Explicit
Option Base 0

Public Const gcsREGPATH = "Software\B2S\VPinMAME"
Public Const gcsHKEY = &H80000001 ' HKEY_CURRENT_USER
Public Const HKEY_CLASSES_ROOT = &H80000000
Public Const HKEY_CURRENT_USER = &H80000001
Public Const HKEY_LOCAL_MACHINE = &H80000002
Public Const HKEY_USERS = &H80000003
Public Const KEY_ALL_ACCESS = &H3F
Public Const KEY_READ = &H19
Public Const REG_CREATED_NEW_KEY = &H1
Public Const REG_OPENED_EXISTING_KEY = &H2
Public Const ERR_NOERROR = 0&
Public Const REG_SZ = (1)
Public Const REG_BINARY = (3)

Declare Function RegQueryBinValueEx Lib "advapi32.dll" Alias "RegQueryValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal lpReserved As Long, lpType As Long, lpData As Any, lpcbData As Long) As Long         ' Note that if you declare the lpData parameter as String, you must pass it By Value.
Declare Function RegDeleteKey Lib "advapi32.dll" Alias "RegDeleteKeyA" (ByVal hKey As Long, ByVal lpSubKey As String) As Long
Declare Function RegDeleteValue Lib "advapi32.dll" Alias "RegDeleteValueA" (ByVal hKey As Long, ByVal lpValueName As String) As Long
Declare Function SAHRegEnumValue Lib "sahfunc.dll" (ByVal hKey As Long, ByVal nIndex As Long, sRegKey As String, sValues As String) As Long
Declare Function RegEnumKey Lib "advapi32.dll" Alias "RegEnumKeyA" (ByVal hKey As Long, ByVal dwIndex As Long, ByVal lpName As String, ByVal cbName As Long) As Long
Declare Function RegEnumValue Lib "advapi32.dll" Alias "RegEnumValueA" (ByVal hKey As Long, ByVal dwIndex As Long, ByVal lpValueName As String, lpcbValueName As Long, ByVal lpReserved As Long, lpType As Long, lpData As Byte, lpcbData As Long) As Long
Declare Function RegSetValueEx Lib "advapi32.dll" Alias "RegSetValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal Reserved As Long, ByVal dwType As Long, ByVal szData As String, ByVal cbData As Long) As Long
Declare Function RegSetBinValueEx Lib "advapi32.dll" Alias "RegSetValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal Reserved As Long, ByVal dwType As Long, szData As Any, ByVal cbData As Long) As Long
Declare Function RegCloseKey Lib "advapi32.dll" (ByVal hKey As Long) As Long                                                                                                                    ' API calls
Declare Function RegCreateKey Lib "advapi32.dll" Alias "RegCreateKeyA" (ByVal hKey As Long, ByVal lpSubKey As String, phkResult As Long) As Long
Declare Function RegOpenKeyEx Lib "advapi32.dll" Alias "RegOpenKeyExA" (ByVal hKey As Long, ByVal lpSubKey As String, ByVal ulOptions As Long, ByVal samDesired As Long, phkResult As Long) As Long
Declare Function RegQueryValueEx Lib "advapi32" Alias "RegQueryValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal lpReserved As Long, ByRef lpType As Long, ByVal szData As String, ByRef lpcbData As Long) As Long

Public Function SetReg(ByVal sRegKey As String, _
                       ByVal sValue As String, _
                       Optional ByVal bHKeyLocalMachine As Boolean = False) As Boolean
    Dim nRegKey As Long, sTmp As String, nRet As Long
    If sValue = "" Then sValue = Chr(0)
    sTmp = gcsREGPATH
    nRet = RegCreateKey(IIf(bHKeyLocalMachine, HKEY_LOCAL_MACHINE, gcsHKEY), sTmp, nRegKey)
    If nRet <> ERR_NOERROR Then
        Call MsgBox("Probleme bei Schreibzugriff auf Registry!", vbCritical, "Registry Error " + nRet)
    End If
    nRet = RegSetValueEx(nRegKey, sRegKey, 0, REG_SZ, sValue, Len(sValue))
    If nRet <> ERR_NOERROR Then
        Call MsgBox("Probleme beim Schreiben in die Registry!" + vbCrLf + Err.Description, vbCritical, "Registry Error " + CStr(nRet))
    End If
    Call RegCloseKey(nRegKey)
    ' maybe update local machine key
    If Not bHKeyLocalMachine Then
        nRet = RegOpenKeyEx(HKEY_LOCAL_MACHINE, sTmp, 0, KEY_ALL_ACCESS, nRegKey)
        If nRet = ERR_NOERROR Then
            nRet = RegSetValueEx(nRegKey, sRegKey, 0, REG_SZ, sValue, Len(sValue))
            Call RegCloseKey(nRegKey)
        End If
    End If
    SetReg = True
End Function

Public Function GetRegX(ByVal sRegKey As String, _
                        Optional ByVal sDefault As String = "", _
                        Optional ByVal bHKeyLocalMachine As Boolean = False) As String
    Dim nRegKey As Long, sTmp As String, sPath As String, nRet As Long, nLen As Long
    Dim oByte() As Byte, bGetOut As Boolean
    sTmp = Space(1000)
    sPath = gcsREGPATH
    Do While True
        'nRet = RegOpenKeyEx(IIf(bHKeyLocalMachine, HKEY_LOCAL_MACHINE, gcsHKEY), sPath, 0, KEY_ALL_ACCESS, nRegKey)
        nRet = RegOpenKeyEx(IIf(bHKeyLocalMachine, HKEY_LOCAL_MACHINE, gcsHKEY), sPath, 0, KEY_READ, nRegKey) ' (TP) 2004-12-10
        If nRet = ERR_NOERROR Then
            nLen = Len(sTmp)
            nRet = RegQueryValueEx(nRegKey, sRegKey, 0, REG_SZ, sTmp, nLen)
            If nRet <> ERR_NOERROR Then
                sTmp = Space(nLen)
                nRet = RegQueryValueEx(nRegKey, sRegKey, 0, REG_SZ, sTmp, nLen)
                If nRet <> ERR_NOERROR Then
                    sTmp = sDefault
                Else
                    bGetOut = True
                End If
            Else
                bGetOut = True
            End If
        Else
            sTmp = sDefault
        End If
        Call RegCloseKey(nRegKey)
        If Not bHKeyLocalMachine Or bGetOut Then Exit Do
        bHKeyLocalMachine = False
    Loop
    sTmp = RTrim(sTmp)
    GetRegX = KillNullTermX(sTmp)
End Function

Public Function DelReg(Optional ByVal sRegKey As String = "", _
                       Optional ByVal bHKeyLocalMachine As Boolean = False) As Boolean
    Dim nRegKey As Long, sTmp As String, nRet As Long
    sTmp = gcsREGPATH
    nRet = RegOpenKeyEx(IIf(bHKeyLocalMachine, HKEY_LOCAL_MACHINE, gcsHKEY), sTmp, 0, KEY_ALL_ACCESS, nRegKey)
    If sRegKey = "" Then
        nRet = RegDeleteKey(nRegKey, sRegKey)
    Else
        nRet = RegDeleteValue(nRegKey, sRegKey)
    End If
    Call RegCloseKey(nRegKey)
    If bHKeyLocalMachine Then
        nRet = RegOpenKeyEx(gcsHKEY, sTmp, 0, KEY_ALL_ACCESS, nRegKey)
        If sRegKey = "" Then
            nRet = RegDeleteKey(nRegKey, sRegKey)
        Else
            nRet = RegDeleteValue(nRegKey, sRegKey)
        End If
        Call RegCloseKey(nRegKey)
    End If
    DelReg = True
End Function

Private Function KillNullTermX(ByVal sText As String) As String
    Dim nPos As Long
    nPos = InStr(1, sText, Chr(0))
    If nPos > 0 Then
        sText = Left(sText, nPos - 1)
    End If
    KillNullTermX = sText
End Function
