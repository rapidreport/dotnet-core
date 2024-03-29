﻿Imports iTextSharp.text.pdf

Imports jp.co.systembase.report.component
Imports jp.co.systembase.report.textformatter
Imports jp.co.systembase.report.renderer
Imports jp.co.systembase.report.expression
Imports jp.co.systembase.report

Namespace elementrenderer

    Public Class TextRenderer
        Implements IElementRenderer

        Public Overridable Sub Render(
          renderer As PdfRenderer,
          reportDesign As ReportDesign,
          region As Region,
          design As ElementDesign,
          data As Object) Implements IElementRenderer.Render
            _RenderRect(renderer, reportDesign, region, design)
            Dim text = _GetText(reportDesign, design, data)
            If text Is Nothing Then
                Return
            End If
            If renderer.Setting.ReplaceBackslashToYen Then
                text = text.Replace("\", ChrW(&HA5))
            End If
            Dim pdfText = _GetPdfText()
            pdfText.Initialize(renderer, reportDesign, _GetRegion(reportDesign, region, design), design, text)
            pdfText.Draw()
        End Sub

        Protected Overridable Sub _RenderRect(renderer As PdfRenderer, reportDesign As ReportDesign, region As Region, design As ElementDesign)
            If Not design.IsNull("rect") Then
                renderer.Setting.GetElementRenderer("rect").Render(
                  renderer,
                  reportDesign,
                  region,
                  design.Child("rect"),
                  Nothing)
            End If
        End Sub

        Protected Overridable Function _GetText(reportDesign As ReportDesign, design As ElementDesign, data As Object) As String
            Dim ret As String = design.Get("text")
            If data IsNot Nothing Then
                Dim textProcessor As New EmbeddedTextProcessor
                ret = textProcessor.EmbedData(reportDesign, design.Child("formatter"), ret, data)
            End If
            Return ret
        End Function

        Protected Overridable Function _GetRegion(reportDesign As ReportDesign, region As Region, design As ElementDesign) As Region
            Dim _region = region
            If Not design.IsNull("margin") Then
                Dim m = design.Child("margin")
                _region = New Region(region, m.Get("left"), m.Get("top"), m.Get("right"), m.Get("bottom"))
            End If
            Return _region.ToPointScale(reportDesign)
        End Function

        Protected Overridable Function _GetPdfText() As PdfText
            Return New PdfText()
        End Function

    End Class


End Namespace