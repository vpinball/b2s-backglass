#Disable Warning BC42016, BC42017, BC42018, BC42019, BC42032, BC42021
Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection
Imports System.ComponentModel.Composition
Imports System.ComponentModel.Composition.Hosting
Imports System.ComponentModel
Imports B2SServerPluginInterface

Public Class PluginList
    Inherits BindingList(Of Plugin)

    Sub PluginInit(TableName As String, RomName As String)
        For Each P As Plugin In Me
            P.PluginInit(TableName, RomName)
        Next
    End Sub
    Sub PluginFinish()
        For Each P As Plugin In Me
            P.PluginFinish()
        Next
    End Sub

    Sub PinMameRun()
        For Each P As Plugin In Me
            P.PinMameRun()
        Next
    End Sub
    Sub PinMamePause()
        For Each P As Plugin In Me
            P.PinMamePause()
        Next
    End Sub
    Sub PinMameContinue()
        For Each P As Plugin In Me
            P.PinMameContinue()
        Next
    End Sub
    Sub PinMameStop()
        For Each P As Plugin In Me
            P.PinMameStop()
        Next
    End Sub

    ''' <summary>
    ''' Is called when new data on a single table element (Lamp, Switch, Solenoid, Mech, GI) is available.
    ''' Forwards the data to all plugins
    ''' </summary>
    ''' <param name="TableElementTypeChar">Type of the table element.</param>
    ''' <param name="Number">The number of the table element.</param>
    ''' <param name="Value">The value of the table element.</param>
    Sub DataReceive(TableElementTypeChar As Char, Number As Integer, Value As Integer)
        For Each P As Plugin In Me
            P.DataReceive(TableElementTypeChar, Number, Value)
        Next
    End Sub


    ''' <summary>
    ''' Is called when new data on a table element (Lamp, Switch, Solenoid, Mech, GI) is available.
    ''' </summary>
    ''' <param name="TableElementTypeChar">Type of the table element.</param>
    ''' <param name="Data">The data received from Pinmame.</param>
    Public Sub DataReceive(TableElementTypeChar As Char, Data As Object)
        If TableElementTypeChar = "D" Then                                   ' *** Unsure Why Logic Below Doesn't Work for "D"igits, but this does work
            If Not Data Is Nothing Then
                For i As Integer = 0 To Data.GetUpperBound(0)
                    For Each P As Plugin In Me
                        If P.Status = PluginStatusEnum.Active Then
                            P.DataReceive(TableElementTypeChar, CInt(Data(i, 0)), CInt(Data(i, 2)))
                        End If
                    Next
                Next
            End If
        Else                                                                 ' *** Everything Except "D"igits
            If Count > 0 AndAlso Not Data Is Nothing Then
                Dim DataArray As Object(,) = DirectCast(Data, Object(,))
                If Not DataArray Is Nothing AndAlso DataArray.GetType().IsArray Then

                    Dim IntDataArray(DataArray.GetLength(0), 2) As Integer
                    For T = 0 To DataArray.GetLength(0) - 1
                        IntDataArray(T, 0) = Convert.ToInt32(DataArray(T, 0))
                        IntDataArray(T, 1) = Convert.ToInt32(DataArray(T, 1))
                    Next

                    For Each P As Plugin In Me
                        If P.Status = PluginStatusEnum.Active Then
                            For T As Integer = 0 To IntDataArray.GetLength(0) - 1
                                P.DataReceive(TableElementTypeChar, IntDataArray(T, 0), IntDataArray(T, 1))
                            Next
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Adds the specified direct plugins.
    ''' </summary>
    ''' <param name="DirectPlugins">The direct plugins.</param>
    Overloads Sub Add(Optional DirectPlugins As IEnumerable(Of IDirectPlugin) = Nothing)
        If Not DirectPlugins Is Nothing Then
            For Each P As IDirectPlugin In DirectPlugins
                Add(New Plugin(P))
            Next
        End If
    End Sub

    <ImportMany(GetType(IDirectPlugin))>
    Private ImportedPlugins As IEnumerable(Of IDirectPlugin)
    ''' <summary>
    ''' Loads the plugins in the specified directory.
    ''' </summary>
    ''' <param name="Directory">The directory containing the plugins.</param>
    Private Sub LoadDirectoryPlugins(Directory As DirectoryInfo)

        ImportedPlugins = New List(Of IDirectPlugin)
        Try
            Dim Catalog As New DirectoryCatalog(Directory.FullName)
            Dim Container As New CompositionContainer(Catalog)

            Container.ComposeParts(Me)
        Catch ex As Exception
            Server.errorlog.WriteLogEntry("LoadDirectoryPlugins: Failed loading plugin (" & Directory.FullName & ") Exception: " & ex.Message)
        End Try
        If Not ImportedPlugins Is Nothing Then
            Add(ImportedPlugins)
        End If
        ImportedPlugins = Nothing
    End Sub

    ''' <summary>
    ''' Loads the plugins from the plugins directory.<br/>
    ''' The plugins directory must be a sub-directory of the directory containing the B2SServer dll. This directory can have any number of subdirectories containing plugins.<br/>
    ''' Plugins in directories having a name start with "-" are not loaded.<br/>
    ''' Plugins must be class libraries implementing the IDirectPlugin interface found in the B2SServerPluginInterface.dll and exporting this type for composition (see MEF docu for more info).
    ''' </summary>
    Sub LoadPlugins()
        Server.errorlog.WriteLogEntry("PluginList.LoadPlugins")

        Dim LoadedDirectories As New List(Of String)
        'Clear the PluginList
        Me.Clear()

        Dim AssemblyDirectory As New DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))

        Try
            'loop through all directories named plugin or plugins (case insensitive)
            Dim PluginDirectory As DirectoryInfo = Nothing
            Dim String64on64BitProcess As String = If(Environment.Is64BitProcess, "64", "")
            B2SSettings.PluginsFilePath = AssemblyDirectory.FullName

            For Each PluginDirectory In AssemblyDirectory.GetDirectories()

                If PluginDirectory.Name.ToLower = "plugin" + String64on64BitProcess OrElse PluginDirectory.Name.ToLower = "plugins" + String64on64BitProcess Then
                    Server.errorlog.WriteLogEntry("PluginList.LoadPlugins Directory: " & PluginDirectory.Name)
                    'Loop through all subdirectory directories and load plugins
                    For Each PluginSubDirectory As DirectoryInfo In PluginDirectory.GetDirectories()
                        If Not PluginSubDirectory.Name.StartsWith("-") Then
                            'Skip if directory have already been loaded
                            If Not LoadedDirectories.Contains(PluginSubDirectory.FullName.ToLower()) Then
                                Server.errorlog.WriteLogEntry("PluginList.LoadPlugins.LoadDirectory: " & PluginSubDirectory.Name)
                                LoadDirectoryPlugins(PluginSubDirectory)
                                LoadedDirectories.Add(PluginSubDirectory.FullName.ToLower())
                            End If
                        End If
                    Next

                    'Check plugin directory for shortcuts (.lnk files) and load plugins if shortcut points to a directory
                    'Dim Shell As New IWshRuntimeLibrary.WshShell()
                    For Each LnkFile As FileInfo In PluginDirectory.GetFiles("*.lnk")
                        If Not LnkFile.Name.StartsWith("-") Then
                            'Dim Shortcut As IWshRuntimeLibrary.WshShortcut = Shell.CreateShortcut(LnkFile.FullName)
                            'If Directory.Exists(Shortcut.TargetPath) Then
                            '    'It's a folder path
                            '    Dim LnkDirectory As DirectoryInfo = New DirectoryInfo(Shortcut.TargetPath)
                            '    'Skip if directory have already been loaded
                            '    If Not LoadedDirectories.Contains(LnkDirectory.FullName.ToLower()) Then
                            '        LoadDirectoryPlugins(LnkDirectory)
                            '        LoadedDirectories.Add(LnkDirectory.FullName.ToLower())
                            '    End If
                            'ElseIf File.Exists(Shortcut.TargetPath) Then
                            '    'It's a file path
                            '    'Do nothing
                            'End If
                            Dim WScriptShell As Object
                            Dim Shortcut As Object
                            Try
                                WScriptShell = CreateObject("WScript.Shell")
                                Shortcut = WScriptShell.CreateShortcut(LnkFile.FullName)
                                If Directory.Exists(Shortcut.TargetPath) Then
                                    'It's a folder path
                                    Dim LnkDirectory As New DirectoryInfo(Shortcut.TargetPath)
                                    'Skip if directory has already been loaded
                                    If Not LoadedDirectories.Contains(LnkDirectory.FullName.ToLower()) Then
                                        Server.errorlog.WriteLogEntry("PluginList.LoadPlugins.LoadShortcut: " & LnkDirectory.Name)
                                        LoadDirectoryPlugins(LnkDirectory)
                                        LoadedDirectories.Add(LnkDirectory.FullName.ToLower())
                                    End If
                                ElseIf File.Exists(Shortcut.TargetPath) Then
                                    'It's a file path
                                    'Do nothing
                                End If
                            Catch ex As Exception
                                Server.errorlog.WriteLogEntry("PluginList.LoadPlugins.LoadShortcut Exception: " & ex.Message)
                            End Try

                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            Server.errorlog.WriteLogEntry("PluginList.LoadPlugins Exception: " & ex.Message)
        End Try
        Server.errorlog.WriteLogEntry("PluginList.LoadPlugins END")
    End Sub

    Public Sub New(Optional LoadPlugins As Boolean = False)
        If LoadPlugins Then
            Me.LoadPlugins()
        End If
    End Sub

End Class
