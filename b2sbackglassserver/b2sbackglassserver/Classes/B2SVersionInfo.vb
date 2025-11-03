' Add VersionInfo class

Public Class B2SVersionInfo
    Public Const B2S_VERSION_MAJOR = "2"
    Public Const B2S_VERSION_MINOR = "1"
    Public Const B2S_VERSION_REVISION = "5"
    Public Const B2S_VERSION_BUILD = "999"
    Public Const B2S_VERSION_HASH = "nonset"
    ' 2.5.0
    Public Const B2S_VERSION_STRING = B2S_VERSION_MAJOR & "." & B2S_VERSION_MINOR & "." & B2S_VERSION_REVISION
    ' 2.5.0.999
    Public Const B2S_BUILD_STRING = B2S_VERSION_MAJOR & "." & B2S_VERSION_MINOR & "." &
                                    B2S_VERSION_REVISION & "." & B2S_VERSION_BUILD
    ' 2.5.0.999-nonset Git hash
    Public Const B2S_BUILD_STRING_HASH = B2S_VERSION_MAJOR & "." & B2S_VERSION_MINOR & "." &
                                         B2S_VERSION_REVISION & "." & B2S_VERSION_BUILD & "-" & B2S_VERSION_HASH
End Class