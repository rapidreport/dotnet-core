﻿Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports jp.co.systembase.report
Imports jp.co.systembase.report.component

Namespace elementrenderer

    Public Class FieldRenderer
        Implements IElementRenderer

        Public Sub Render(
          renderer As PdfRenderer,
          reportDesign As ReportDesign,
          region As Region,
          design As ElementDesign,
          data As Object) Implements IElementRenderer.Render
            If Not design.IsNull("rect") Then
                renderer.Setting.GetElementRenderer("rect").Render(
                  renderer,
                  reportDesign,
                  region,
                  design.Child("rect"),
                  Nothing)
            End If
            Dim fd As ElementDesign = design.Child("formatter")
            Dim text As String = RenderUtil.Format(reportDesign, design.Child("formatter"), data)
            If text Is Nothing Then
                Exit Sub
            End If
            Dim _region As Region = region
            If Not design.IsNull("margin") Then
                Dim m As ElementDesign = design.Child("margin")
                _region = New Region(region, m.Get("left"), m.Get("top"), m.Get("right"), m.Get("bottom"))
            End If
            If renderer.Setting.ReplaceBackslashToYen Then
                text = text.Replace("\", ChrW(&HA5))
            End If
            Dim pdfText = _GetPdfText(renderer, reportDesign, _region, design, text)
            pdfText.Initialize(renderer, reportDesign, _region, design, text)
            pdfText.Draw()
        End Sub

        Protected Overridable Function _GetPdfText(renderer As PdfRenderer, reportDesign As ReportDesign, region As Region, design As ElementDesign, text As String) As PdfText
            Return New PdfText()
        End Function

    End Class

End Namespace
