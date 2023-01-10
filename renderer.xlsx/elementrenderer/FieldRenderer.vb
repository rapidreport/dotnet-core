Imports jp.co.systembase.report.component
Imports jp.co.systembase.report.renderer.xlsx.component

Namespace elementrenderer
    Public Class FieldRenderer
        Implements IElementRenderer

        Public Overridable Sub Collect(
          renderer As XlsxRenderer,
          reportDesign As ReportDesign,
          region As Region,
          design As ElementDesign,
          data As Object) Implements IElementRenderer.Collect
            _RenderRect(renderer, reportDesign, region, design)
            Dim _region As Region = region.ToPointScale(reportDesign)
            If _region.GetWidth <= 0 Or _region.GetHeight <= 0 Then
                Exit Sub
            End If
            renderer.CurrentPage.Fields.Add(_GetField(reportDesign, region, design, data))
        End Sub

        Protected Overridable Sub _RenderRect(renderer As XlsxRenderer, reportDesign As ReportDesign, region As Region, design As ElementDesign)
            If Not design.IsNull("rect") Then
                renderer.Setting.GetElementRenderer("rect").Collect(
                  renderer,
                  reportDesign,
                  region,
                  design.Child("rect"),
                  Nothing)
            End If
        End Sub

        Protected Overridable Function _GetText(reportDesign As ReportDesign, design As ElementDesign, data As Object) As String
            Return RenderUtil.Format(reportDesign, design.Child("formatter"), data)
        End Function

        Protected Overridable Function _GetField(reportDesign As ReportDesign, region As Region, design As ElementDesign, data As Object) As Field
            Dim ret As New Field
            ret.Region = region
            ret.Style = New FieldStyle(New TextDesign(reportDesign, design))
            If data IsNot Nothing Then
                If ret.Style.TextDesign.XlsFormat IsNot Nothing Then
                    ret.Data = data
                Else
                    ret.Data = _GetText(reportDesign, design, data)
                End If
            End If
            Return ret
        End Function

    End Class
End Namespace