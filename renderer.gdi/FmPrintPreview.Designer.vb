﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FmPrintPreview
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FmPrintPreview))
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.PrintPreviewSearch = New jp.co.systembase.report.renderer.gdi.PrintPreviewSearch()
        Me.PrintPreviewSearchPanel = New jp.co.systembase.report.renderer.gdi.PrintPreviewSearchPanel()
        Me.PrintPreviewZoom = New jp.co.systembase.report.renderer.gdi.PrintPreviewZoom()
        Me.PrintPreviewPage = New jp.co.systembase.report.renderer.gdi.PrintPreviewPage()
        Me.PrintPreview = New jp.co.systembase.report.renderer.gdi.PrintPreview()
        Me.PrintPreviewMultiPage = New jp.co.systembase.report.renderer.gdi.PrintPreviewMultiPage()
        Me.SuspendLayout()
        '
        'BtnPrint
        '
        Me.BtnPrint.Location = New System.Drawing.Point(12, 6)
        Me.BtnPrint.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(70, 40)
        Me.BtnPrint.TabIndex = 0
        Me.BtnPrint.Text = "印刷..."
        Me.BtnPrint.UseVisualStyleBackColor = True
        '
        'BtnClose
        '
        Me.BtnClose.Location = New System.Drawing.Point(772, 6)
        Me.BtnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(70, 40)
        Me.BtnClose.TabIndex = 6
        Me.BtnClose.Text = "閉じる"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'PrintPreviewSearch
        '
        Me.PrintPreviewSearch.Location = New System.Drawing.Point(726, 6)
        Me.PrintPreviewSearch.Margin = New System.Windows.Forms.Padding(5)
        Me.PrintPreviewSearch.Name = "PrintPreviewSearch"
        Me.PrintPreviewSearch.Size = New System.Drawing.Size(40, 40)
        Me.PrintPreviewSearch.TabIndex = 4
        '
        'PrintPreviewSearchPanel
        '
        Me.PrintPreviewSearchPanel.Location = New System.Drawing.Point(632, 54)
        Me.PrintPreviewSearchPanel.Margin = New System.Windows.Forms.Padding(5)
        Me.PrintPreviewSearchPanel.Name = "PrintPreviewSearchPanel"
        Me.PrintPreviewSearchPanel.Size = New System.Drawing.Size(210, 25)
        Me.PrintPreviewSearchPanel.TabIndex = 5
        Me.PrintPreviewSearchPanel.Visible = False
        '
        'PrintPreviewZoom
        '
        Me.PrintPreviewZoom.Location = New System.Drawing.Point(449, 6)
        Me.PrintPreviewZoom.Margin = New System.Windows.Forms.Padding(5)
        Me.PrintPreviewZoom.Name = "PrintPreviewZoom"
        Me.PrintPreviewZoom.PrintPreview = Nothing
        Me.PrintPreviewZoom.Size = New System.Drawing.Size(270, 40)
        Me.PrintPreviewZoom.TabIndex = 3
        '
        'PrintPreviewPage
        '
        Me.PrintPreviewPage.Location = New System.Drawing.Point(89, 6)
        Me.PrintPreviewPage.Margin = New System.Windows.Forms.Padding(5)
        Me.PrintPreviewPage.Name = "PrintPreviewPage"
        Me.PrintPreviewPage.PrintPreview = Nothing
        Me.PrintPreviewPage.Size = New System.Drawing.Size(307, 40)
        Me.PrintPreviewPage.TabIndex = 1
        '
        'PrintPreview
        '
        Me.PrintPreview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PrintPreview.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.PrintPreview.Location = New System.Drawing.Point(12, 54)
        Me.PrintPreview.Margin = New System.Windows.Forms.Padding(4)
        Me.PrintPreview.Name = "PrintPreview"
        Me.PrintPreview.Size = New System.Drawing.Size(921, 657)
        Me.PrintPreview.TabIndex = 7
        Me.PrintPreview.TabStop = False
        '
        'PrintPreviewMultiPage
        '
        Me.PrintPreviewMultiPage.Location = New System.Drawing.Point(402, 6)
        Me.PrintPreviewMultiPage.Margin = New System.Windows.Forms.Padding(5)
        Me.PrintPreviewMultiPage.Name = "PrintPreviewMultiPage"
        Me.PrintPreviewMultiPage.PrintPreview = Nothing
        Me.PrintPreviewMultiPage.Size = New System.Drawing.Size(40, 40)
        Me.PrintPreviewMultiPage.TabIndex = 2
        '
        'FmPrintPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(944, 721)
        Me.Controls.Add(Me.PrintPreviewMultiPage)
        Me.Controls.Add(Me.PrintPreviewSearch)
        Me.Controls.Add(Me.PrintPreviewSearchPanel)
        Me.Controls.Add(Me.PrintPreviewZoom)
        Me.Controls.Add(Me.PrintPreviewPage)
        Me.Controls.Add(Me.PrintPreview)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.BtnPrint)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimumSize = New System.Drawing.Size(814, 240)
        Me.Name = "FmPrintPreview"
        Me.Text = "プレビュー"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents BtnPrint As System.Windows.Forms.Button
    Public WithEvents BtnClose As System.Windows.Forms.Button
    Public WithEvents PrintPreview As systembase.report.renderer.gdi.PrintPreview
    Public WithEvents PrintPreviewPage As systembase.report.renderer.gdi.PrintPreviewPage
    Public WithEvents PrintPreviewZoom As systembase.report.renderer.gdi.PrintPreviewZoom
    Friend WithEvents PrintPreviewSearchPanel As jp.co.systembase.report.renderer.gdi.PrintPreviewSearchPanel
    Friend WithEvents PrintPreviewSearch As jp.co.systembase.report.renderer.gdi.PrintPreviewSearch
    Friend WithEvents PrintPreviewMultiPage As PrintPreviewMultiPage
End Class
