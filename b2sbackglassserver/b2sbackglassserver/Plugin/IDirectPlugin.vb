

''' <summary>
''' Interface for plugins for the B2S Server.<br/>
''' All plugins must implement this interface to be recognized by the B2S Server. 
''' In addition they also have to export the class implementing the interface using the following attribute  [Export(typeof(B2S.IDirectPlugin))] in c# or   &lt;Export(GetType(B2S.IDirectPlugin))&gt; for VB.net.
''' Dont forget to add a reference to the B2S Server and to import the System.ComponentModel.Composition namespace.<br/>
''' Be sure to handle all exceptions of your plugin, since the B2S Server will deactivate plugings throwing unhandled exceptions.<br/>
''' Please refer to the docu on the DirectOutput framework for more info on plugins.
''' </summary>
Public Interface IDirectPlugin

    ''' <summary>
    ''' This method initalizes the plugin.<br/>
    ''' It is the first plugin method beeing called after the plugin has been instanciated.
    ''' </summary>
    ''' <param name="TableFilename">The filename of the table.</param>
    ''' <param name="RomName">Name of the rom.</param>
    Sub PluginInit(TableFilename As String, RomName As String)

    ''' <summary>
    ''' Finishes the plugin.<br/>
    ''' This is the last method called, before a plugin is discared. This method is also called, after a undhandled exception has occured in a plugin.
    ''' </summary>
    Sub PluginFinish()


    ''' <summary>
    ''' This method is called, when the Run method of PinMame gets called.
    ''' </summary>
    Sub PinMameRun()

    ''' <summary>
    ''' This method is called, when the property Pause of Pinmame gets set to true.
    ''' </summary>
    Sub PinMamePause()

    ''' <summary>
    ''' This method is called, when the property Pause of Pinmame gets set to false.
    ''' </summary>
    Sub PinMameContinue()

    ''' <summary>
    ''' This method is called, when the Stop method of Pinmame is called.
    ''' </summary>
    Sub PinMameStop()

    ''' <summary>
    ''' This method is called, when new data from Pinmame becomes available.
    ''' </summary>
    ''' <param name="TableElementTypeChar">Char representing the table element type (S=Solenoid, W=Switch, L=Lamp, M=Mech, G=GI).</param>
    ''' <param name="Number">The number of the table element.</param>
    ''' <param name="Value">The value of the table element.</param>
    Sub PinMameDataReceive(TableElementTypeChar As Char, Number As Integer, Value As Integer)

    ''' <summary>
    ''' Gets the name of the plugin.
    ''' </summary>
    ''' <value>
    ''' The name of the plugin.
    ''' </value>
    ReadOnly Property Name As String



End Interface
