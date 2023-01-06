Imports System.IO
Imports jp.co.systembase.report.renderer.gdi.imageloader

Public Class RenderingEnv
    Public Graphics As Graphics
    Public Printer As Printer
    Public InDesigner As Boolean = False

    Private _IsMonospacedDefaultFont As Boolean = False
    Private _IsMonospacedFontMap As New Dictionary(Of String, Boolean)

    Public Sub New(g As Graphics, printer As Printer)
        Graphics = g
        Me.Printer = printer
        Using _g = Graphics.FromImage(New Bitmap(1, 1))
            _IsMonospacedDefaultFont = _IsMonospacedFont(_g, printer.Setting.DefaultFont)
            For Each f As String In printer.Setting.FontMap.Keys
                _IsMonospacedFontMap.Add(f, _IsMonospacedFont(_g, printer.Setting.FontMap(f)))
            Next
        End Using
    End Sub

    Public Function GetImage(reportDesign As ReportDesign, desc As Hashtable, key As String) As Image
        If Printer.ImageCache Is Nothing Then
            Return Nothing
        End If
        If Not Printer.ImageCache.ContainsKey(desc) OrElse Not Printer.ImageCache(desc).ContainsKey(key) Then
            Me.createImageCache(reportDesign, desc, key)
        End If
        Return Printer.ImageCache(desc)(key)
    End Function

    Private Sub createImageCache(reportDesign As ReportDesign, desc As Hashtable, key As String)
        If Not Printer.ImageCache.ContainsKey(desc) Then
            Printer.ImageCache.Add(desc, New Dictionary(Of String, Image))
        End If
        If Printer.ImageCache(desc).ContainsKey(key) Then
            Printer.ImageCache(desc).Remove(key)
        End If
        If desc.ContainsKey(key) Then
            Dim imgb As Byte() = reportDesign.GetImage(desc, key)
            If imgb IsNot Nothing Then
                Printer.ImageCache(desc).Add(key, Image.FromStream(New MemoryStream(imgb)))
            Else
                Printer.ImageCache(desc).Add(key, Nothing)
            End If
        Else
            Printer.ImageCache(desc).Add(key, Nothing)
        End If
    End Sub

    Private Function _IsMonospacedFont(g As Graphics, f As String) As Boolean
        Dim font As New Font(f, 1)
        Return ReportUtil.RoundDown(g.MeasureString("i", font).Width, -3) = ReportUtil.RoundDown(g.MeasureString("W", font).Width, -3)
    End Function

    Public Function IsMonospacedFont(fontName As String) As Boolean
        If _IsMonospacedFontMap.ContainsKey(fontName) Then
            Return _IsMonospacedFontMap(fontName)
        Else
            Return _IsMonospacedDefaultFont
        End If
    End Function

End Class
