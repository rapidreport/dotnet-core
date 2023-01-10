Imports jp.co.systembase.report.component
Imports jp.co.systembase.report.expression

Namespace elementrenderer

    Public Class TextRenderer
        Implements IElementRenderer

        Public Overridable Sub Render(
          env As RenderingEnv,
          reportDesign As ReportDesign,
          region As Region,
          design As ElementDesign,
          data As Object) Implements IElementRenderer.Render
            _RenderRect(env, reportDesign, region, design)
            Dim text = _GetText(env, reportDesign, design, data)
            If text Is Nothing Then
                Return
            End If
            Dim gdiText = _GetGdiText()
            gdiText.Initialize(env, reportDesign, _GetRegion(reportDesign, region, design), design, text)
            gdiText.Draw()
        End Sub

        Protected Overridable Sub _RenderRect(env As RenderingEnv, reportDesign As ReportDesign, region As Region, design As ElementDesign)
            If Not design.IsNull("rect") Then
                env.Printer.Setting.GetElementRenderer("rect").Render(
                    env,
                    reportDesign,
                    region,
                    design.Child("rect"),
                    Nothing)
            End If
        End Sub

        Protected Overridable Function _GetText(env As RenderingEnv, reportDesign As ReportDesign, design As ElementDesign, data As Object) As String
            Dim ret = design.Get("text")
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

        Protected Overridable Function _GetGdiText() As GdiText
            Return New GdiText()
        End Function

    End Class

End Namespace
