<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PluginWindow
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PluginWindow))
        Me.StatusContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ActivatePlugin = New System.Windows.Forms.ToolStripMenuItem()
        Me.DisablePlugin = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExceptionContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowExceptionDetails = New System.Windows.Forms.ToolStripMenuItem()
        Me.DiscardException = New System.Windows.Forms.ToolStripMenuItem()
        Me.DiscardExceptionAndActivate = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PluginDataGrid = New System.Windows.Forms.DataGridView()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PluginExceptionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PluginListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.StatusContextMenu.SuspendLayout()
        Me.ExceptionContextMenu.SuspendLayout()
        CType(Me.PluginDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PluginListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusContextMenu
        '
        Me.StatusContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ActivatePlugin, Me.DisablePlugin})
        Me.StatusContextMenu.Name = "StatusContectMenu"
        Me.StatusContextMenu.Size = New System.Drawing.Size(118, 48)
        '
        'ActivatePlugin
        '
        Me.ActivatePlugin.Image = CType(resources.GetObject("ActivatePlugin.Image"), System.Drawing.Image)
        Me.ActivatePlugin.Name = "ActivatePlugin"
        Me.ActivatePlugin.Size = New System.Drawing.Size(117, 22)
        Me.ActivatePlugin.Text = "Activate"
        '
        'DisablePlugin
        '
        Me.DisablePlugin.Image = CType(resources.GetObject("DisablePlugin.Image"), System.Drawing.Image)
        Me.DisablePlugin.Name = "DisablePlugin"
        Me.DisablePlugin.Size = New System.Drawing.Size(117, 22)
        Me.DisablePlugin.Text = "Disable"
        '
        'ExceptionContextMenu
        '
        Me.ExceptionContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowExceptionDetails, Me.DiscardException, Me.DiscardExceptionAndActivate})
        Me.ExceptionContextMenu.Name = "ExceptionContextMenu"
        Me.ExceptionContextMenu.Size = New System.Drawing.Size(227, 70)
        '
        'ShowExceptionDetails
        '
        Me.ShowExceptionDetails.Name = "ShowExceptionDetails"
        Me.ShowExceptionDetails.Size = New System.Drawing.Size(226, 22)
        Me.ShowExceptionDetails.Text = "Show Exception Details"
        '
        'DiscardException
        '
        Me.DiscardException.Name = "DiscardException"
        Me.DiscardException.Size = New System.Drawing.Size(226, 22)
        Me.DiscardException.Text = "Discard Exception"
        '
        'DiscardExceptionAndActivate
        '
        Me.DiscardExceptionAndActivate.Name = "DiscardExceptionAndActivate"
        Me.DiscardExceptionAndActivate.Size = New System.Drawing.Size(226, 22)
        Me.DiscardExceptionAndActivate.Text = "Discard Exception && Activate"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 50
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn2.MinimumWidth = 50
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 62
        '
        'PluginDataGrid
        '
        Me.PluginDataGrid.AllowUserToAddRows = False
        Me.PluginDataGrid.AllowUserToDeleteRows = False
        Me.PluginDataGrid.AllowUserToOrderColumns = True
        Me.PluginDataGrid.AutoGenerateColumns = False
        Me.PluginDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.PluginDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PluginDataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameDataGridViewTextBoxColumn, Me.Status, Me.PluginExceptionDataGridViewTextBoxColumn})
        Me.PluginDataGrid.DataSource = Me.PluginListBindingSource
        Me.PluginDataGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PluginDataGrid.Location = New System.Drawing.Point(0, 0)
        Me.PluginDataGrid.Name = "PluginDataGrid"
        Me.PluginDataGrid.Size = New System.Drawing.Size(838, 504)
        Me.PluginDataGrid.TabIndex = 2
        '
        'Status
        '
        Me.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Status.ContextMenuStrip = Me.StatusContextMenu
        Me.Status.DataPropertyName = "StatusText"
        Me.Status.HeaderText = "Status"
        Me.Status.MinimumWidth = 50
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.ToolTipText = "Status of the plugin. Rightclick to change the status."
        Me.Status.Width = 62
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 482)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(838, 22)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'StatusLabel
        '
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(406, 17)
        Me.StatusLabel.Text = "B2S Server Plugins - Double click a entry to show the frontend of the plugin "
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'NameDataGridViewTextBoxColumn
        '
        Me.NameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.NameDataGridViewTextBoxColumn.ContextMenuStrip = Me.StatusContextMenu
        Me.NameDataGridViewTextBoxColumn.DataPropertyName = "Name"
        Me.NameDataGridViewTextBoxColumn.DividerWidth = 1
        Me.NameDataGridViewTextBoxColumn.HeaderText = "Name"
        Me.NameDataGridViewTextBoxColumn.MinimumWidth = 100
        Me.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn"
        Me.NameDataGridViewTextBoxColumn.ReadOnly = True
        Me.NameDataGridViewTextBoxColumn.ToolTipText = "Well, it is the name of the plugin."
        Me.NameDataGridViewTextBoxColumn.Width = 200
        '
        'PluginExceptionDataGridViewTextBoxColumn
        '
        Me.PluginExceptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.PluginExceptionDataGridViewTextBoxColumn.ContextMenuStrip = Me.ExceptionContextMenu
        Me.PluginExceptionDataGridViewTextBoxColumn.DataPropertyName = "PluginException"
        Me.PluginExceptionDataGridViewTextBoxColumn.HeaderText = "Last Exception"
        Me.PluginExceptionDataGridViewTextBoxColumn.MinimumWidth = 200
        Me.PluginExceptionDataGridViewTextBoxColumn.Name = "PluginExceptionDataGridViewTextBoxColumn"
        Me.PluginExceptionDataGridViewTextBoxColumn.ReadOnly = True
        Me.PluginExceptionDataGridViewTextBoxColumn.ToolTipText = "Last exception thrown by the plugin. Rightclick for options."
        '
        'PluginListBindingSource
        '
        Me.PluginListBindingSource.DataSource = GetType(B2S.PluginList)
        '
        'PluginWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(838, 504)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.PluginDataGrid)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PluginWindow"
        Me.Text = "B2S Server Plugins"
        Me.StatusContextMenu.ResumeLayout(False)
        Me.ExceptionContextMenu.ResumeLayout(False)
        CType(Me.PluginDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.PluginListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PluginListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ExceptionContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowExceptionDetails As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DiscardException As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DiscardExceptionAndActivate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StatusContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ActivatePlugin As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisablePlugin As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PluginDataGrid As System.Windows.Forms.DataGridView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PluginExceptionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
