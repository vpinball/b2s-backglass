Imports System.Windows.Forms
Imports System.Drawing

Public Class PluginWindow

    Sub New(Plugins As PluginList)

        InitializeComponent()
        Me.Plugins = Plugins
    End Sub

    Private _Plugins As PluginList = New PluginList()
    Public Property Plugins() As PluginList
        Get
            Return _Plugins
        End Get
        Private Set(ByVal value As PluginList)
            _Plugins = value
            Me.PluginDataGrid.DataSource = value
            Me.PluginDataGrid.Refresh()
        End Set
    End Property

    Private Sub ExceptionContextMenu_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ExceptionContextMenu.ItemClicked
        Select Case e.ClickedItem.Name
            Case "ShowExceptionDetails"
                MsgBox(Plugins(Me.PluginDataGrid.CurrentCell.RowIndex).PluginException.ToString(), MsgBoxStyle.Exclamation + MsgBoxStyle.MsgBoxSetForeground + MsgBoxStyle.OkOnly, "Exception details for plugin " & Plugins(Me.PluginDataGrid.CurrentCell.RowIndex).Name)
            Case "DiscardException"
                Plugins(Me.PluginDataGrid.CurrentCell.RowIndex).PluginException = Nothing
            Case "DiscardExceptionAndActivate"
                Plugins(Me.PluginDataGrid.CurrentCell.RowIndex).PluginException = Nothing
                Plugins(Me.PluginDataGrid.CurrentCell.RowIndex).Status = PluginStatusEnum.Active
        End Select


    End Sub

    Private Sub PluginDataGrid_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles PluginDataGrid.CellDoubleClick
        If e.RowIndex >= 0 AndAlso e.RowIndex <= Plugins.Count Then
            Plugins(e.RowIndex).PluginShowFrontEnd()
        End If
    End Sub



    Private Sub PluginDataGrid_CellMouseEnter(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles PluginDataGrid.CellMouseEnter
        If e.ColumnIndex >= 0 AndAlso e.ColumnIndex < Me.PluginDataGrid.Columns.Count Then
            Me.StatusLabel.Text = Me.PluginDataGrid.Columns(e.ColumnIndex).ToolTipText
        End If
    End Sub

    Private Sub PluginDataGrid_CellMouseLeave(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles PluginDataGrid.CellMouseLeave
        Me.StatusLabel.Text = "Doubleclick a entry to show the frontend of the plugin"
    End Sub



    Private Sub StatusContextMenu_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles StatusContextMenu.ItemClicked
        Select Case e.ClickedItem.Name
            Case "ActivatePlugin"
                Plugins(Me.PluginDataGrid.CurrentCell.RowIndex).Status = PluginStatusEnum.Active
            Case "DisablePlugin"
                Plugins(Me.PluginDataGrid.CurrentCell.RowIndex).Status = PluginStatusEnum.Disabled
        End Select

    End Sub
End Class