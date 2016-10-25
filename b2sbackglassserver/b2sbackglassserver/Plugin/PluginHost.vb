Imports System.IO
Imports System.Reflection
Imports System.ComponentModel.Composition
Imports System.ComponentModel.Composition.Hosting
Imports System.Windows.Forms

''' <summary>
''' Hosts and handles plugins for the B2S Server.
''' </summary>
Public Class PluginHost

    Private _Plugins As PluginList
    ''' <summary>
    ''' Gets the list of loaded plugins.
    ''' </summary>
    ''' <value>
    ''' The loaded plugins.
    ''' </value>
    Public Property Plugins() As PluginList
        Get
            Return _Plugins
        End Get
        Private Set(ByVal value As PluginList)
            _Plugins = value
        End Set
    End Property


    ''' <summary>
    ''' Initializes the plugin.<br/>
    ''' This is the first call to a plugin after the system start up.
    ''' </summary>
    ''' <param name="TableFilename">The table filename.</param>
    ''' <param name="RomName">Name of the rom.</param>
    Public Sub PluginInit(TableFilename As String, RomName As String)
        Plugins.PluginInit(TableFilename, RomName)
    End Sub
    ''' <summary>
    ''' Finishes the plugin.<br/>
    ''' This is the last call to a plugin before the system is shut down.
    ''' </summary>
    Public Sub PluginFinish()
        Plugins.PluginFinish()
    End Sub

    ''' <summary>
    ''' Gets called when the Run method of Pinmame is called.
    ''' </summary>
    Public Sub PinMameRun()
        Plugins.PinMameRun()
    End Sub
    ''' <summary>
    ''' Gets called when the property Pause of Pinmame is set to true.
    ''' </summary>
    Public Sub PinMamePause()
        Plugins.PinMamePause()
    End Sub
    ''' <summary>
    ''' Gets called when the property Pause of Pinmame is set to false.
    ''' </summary>
    Public Sub PinMameContinue()
        Plugins.PinMameContinue()
    End Sub
    ''' <summary>
    ''' Gets called when the Stop method of Pinmame is called.
    ''' </summary>
    Public Sub PinMameStop()
        Plugins.PinMameStop()
    End Sub

    ''' <summary>
    ''' Is called when new data on a table element (Lamp, Switch, Solenoid, Mech, GI) is available.
    ''' </summary>
    ''' <param name="TableElementTypeChar">Type of the table element.</param>
    ''' <param name="Number">The number of the table element.</param>
    ''' <param name="Value">The value of the table element.</param>
    Public Sub DataReceive(TableElementTypeChar As Char, Number As Integer, Value As Integer)
        Plugins.DataReceive(TableElementTypeChar, Number, Value)
    End Sub

    ''' <summary>
    ''' Is called when new data on a table element (Lamp, Switch, Solenoid, Mech, GI) is available.
    ''' </summary>
    ''' <param name="TableElementTypeChar">Type of the table element.</param>
    ''' <param name="Data">The data received from Pinmame.</param>
    Public Sub DataReceive(TableElementTypeChar As Char, Data As Object)
        Plugins.DataReceive(TableElementTypeChar, Data)
    End Sub


    Private PluginWindow As PluginWindow = Nothing

    ''' <summary>
    ''' Shows the plugin window.
    ''' </summary>
    Sub ShowPluginWindow(Optional ParentForm As Form = Nothing, Optional ParentSettings As System.Drawing.Rectangle = Nothing)

        For Each F As Form In Application.OpenForms
            If TypeOf F Is PluginWindow Then
                F.BringToFront()
                F.Focus()
                Return
            End If
        Next

        Dim PW As PluginWindow = New PluginWindow(Plugins)
        If ParentSettings = Nothing Then
            PW.StartPosition = FormStartPosition.CenterParent
        End If

        If ParentForm IsNot Nothing Then
            PW.Show(ParentForm)
        ElseIf ParentSettings <> Nothing Then
            PW.Show()
            Dim x As Integer = CInt(ParentSettings.X + ParentSettings.Width / 2 - PW.Width / 2)
            Dim y As Integer = CInt(ParentSettings.Y + ParentSettings.Height / 2 - PW.Height / 2)
            PW.Location = New System.Drawing.Point(x, y)
        Else
            PW.Show()
        End If
        PW.BringToFront()
        PW.Focus()
        PW.PluginDataGrid.Focus()
        PW.TopMost = True
        PW.Select()
        PW.PluginDataGrid.Select()
    End Sub

    ''' <summary>
    ''' Closes the plugin window.
    ''' </summary>
    Sub ClosePluginWindow()
        For Each F As Form In Application.OpenForms
            If TypeOf F Is PluginWindow Then
                F.Close()
                Return
            End If
        Next
    End Sub


    ''' <summary>
    ''' Initializes a new instance of the <see cref="PluginHost"/> class.
    ''' </summary>
    ''' <param name="LoadPlugins">if set to <c>true</c> the plugins are loaded after the class is instanciated.</param>
    Public Sub New(Optional LoadPlugins As Boolean = False)
        Plugins = New PluginList(LoadPlugins)
    End Sub


End Class
