Imports System.Reflection
Imports B2SServerPluginInterface
Imports System.ComponentModel

Public Enum PluginStatusEnum
    Active
    Disabled
    DisabledDueToException
End Enum


Public Class Plugin
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged


    Private Sub NotifyPropertyChanged(ByVal info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Public ReadOnly Property Name() As String
        Get
            Try
                If Not DirectPlugin Is Nothing Then
                    Return DirectPlugin.Name
                End If
            Catch ex As Exception
                HandleError(ex)
            End Try
            Return "<Name cant be queried>"
        End Get

    End Property



    Private _DirectPlugin As IDirectPlugin
    Public Property DirectPlugin() As IDirectPlugin
        Get
            Return _DirectPlugin
        End Get
        Set(ByVal value As IDirectPlugin)
            _DirectPlugin = value
        End Set
    End Property

    Private _Status As PluginStatusEnum = PluginStatusEnum.Active
    Public Property Status() As PluginStatusEnum
        Get
            Return _Status
        End Get
        Set(ByVal value As PluginStatusEnum)
            _Status = value
            NotifyPropertyChanged("Status")
        End Set
    End Property

    ReadOnly Property HasFrontendInterface() As Boolean
        Get
            Return TypeOf (DirectPlugin) Is IDirectPluginFrontend
        End Get
    End Property

    ReadOnly Property HasPinMameInterface() As Boolean
        Get
            Return TypeOf (DirectPlugin) Is IDirectPluginPinMame
        End Get
    End Property


    ReadOnly Property StatusText() As String
        Get
            Select Case Status
                Case PluginStatusEnum.Active
                    Return "Active"
                Case PluginStatusEnum.Disabled
                    Return "Disabled"
                Case PluginStatusEnum.DisabledDueToException
                    Return "Disabled due to exception"
                Case Else
                    Return "<unknown status>"
            End Select

        End Get
    End Property




    Private _PluginException As Exception
    Public Property PluginException() As Exception
        Get
            Return _PluginException
        End Get
        Set(ByVal value As Exception)
            _PluginException = value
            NotifyPropertyChanged("PluginException")
        End Set
    End Property


    Private Sub HandleError(ex As Exception)
        PluginException = ex
        Status = PluginStatusEnum.DisabledDueToException
        Try
            DirectPlugin.PluginFinish()
        Catch
        End Try
    End Sub

    Public Sub PluginShowFrontEnd()
        If HasFrontendInterface Then
            Try
                CType(DirectPlugin, IDirectPluginFrontend).PluginShowFrontend()
            Catch ex As Exception
                PluginException = ex
            End Try
        End If
    End Sub


    Public Sub PluginInit(TableFilename As String, RomName As String)
        If Status = PluginStatusEnum.Active AndAlso Not DirectPlugin Is Nothing Then
            Try
                DirectPlugin.PluginInit(TableFilename, RomName)
            Catch ex As Exception
                HandleError(ex)
            End Try
        End If
    End Sub
    Public Sub PluginFinish()
        If Status = PluginStatusEnum.Active AndAlso Not DirectPlugin Is Nothing Then
            Try
                DirectPlugin.PluginFinish()
            Catch ex As Exception
                HandleError(ex)
            End Try
        End If

    End Sub


    Public Sub PinMameRun()
        If HasPinMameInterface AndAlso Status = PluginStatusEnum.Active AndAlso Not DirectPlugin Is Nothing Then
            Try
                CType(DirectPlugin, IDirectPluginPinMame).PinMameRun()
            Catch ex As Exception
                HandleError(ex)
            End Try
        End If
    End Sub
    Public Sub PinMamePause()
        If HasPinMameInterface AndAlso Status = PluginStatusEnum.Active AndAlso Not DirectPlugin Is Nothing Then
            Try
                CType(DirectPlugin, IDirectPluginPinMame).PinMamePause()
            Catch ex As Exception
                HandleError(ex)
            End Try
        End If
    End Sub
    Public Sub PinMameContinue()
        If HasPinMameInterface AndAlso Status = PluginStatusEnum.Active AndAlso Not DirectPlugin Is Nothing Then
            Try
                CType(DirectPlugin, IDirectPluginPinMame).PinMameContinue()
            Catch ex As Exception
                HandleError(ex)
            End Try
        End If

    End Sub
    Public Sub PinMameStop()
        If HasPinMameInterface AndAlso Status = PluginStatusEnum.Active AndAlso Not DirectPlugin Is Nothing Then
            Try
                CType(DirectPlugin, IDirectPluginPinMame).PinMameStop()
            Catch ex As Exception
                HandleError(ex)
            End Try
        End If

    End Sub

    Public Sub DataReceive(TableElementType As Char, Number As Integer, Value As Integer)
        If Status = PluginStatusEnum.Active AndAlso Not DirectPlugin Is Nothing Then
            Try
                DirectPlugin.DataReceive(TableElementType, Number, Value)
            Catch ex As Exception
                HandleError(ex)
            End Try
        End If

    End Sub


    Public Sub New(Optional DirectPlugin As IDirectPlugin = Nothing)
        Me.DirectPlugin = DirectPlugin
    End Sub



End Class
