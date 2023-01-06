Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports ZXing
Imports ZXing.Common
Imports ZXing.QrCode
Imports ZXing.QrCode.Internal

Imports jp.co.systembase.report.component
Imports jp.co.systembase.barcode

Namespace elementrenderer

    Public Class BarcodeRenderer
        Implements IElementRenderer

        Protected Const MARGIN_X = 2.0F
        Protected Const MARGIN_Y = 2.0F
        Protected Const SCALE = 10.0F

        Public Sub Render(
          renderer As PdfRenderer,
          reportDesign As ReportDesign,
          region As Region,
          design As ElementDesign,
          data As Object) Implements IElementRenderer.Render
            Dim code As String = _GetCode(reportDesign, design, data)
            If code Is Nothing Then
                Exit Sub
            End If
            Try
                _RenderBarcode(renderer, _CreateBarcodeShape(design, _GetRegion(reportDesign, region), code))
            Catch ex As Exception
            End Try
        End Sub

        Protected Overridable Function _GetCode(reportDesign As ReportDesign, design As ElementDesign, data As Object) As String
            Return RenderUtil.Format(reportDesign, design.Child("formatter"), data)
        End Function

        Protected Overridable Function _GetRegion(reportDesign As ReportDesign, region As Region) As Region
            Return New Region(region.ToPointScale(reportDesign), MARGIN_X, MARGIN_Y, MARGIN_X, MARGIN_Y)
        End Function

        Protected Overridable Function _CreateBarcodeShape(design As ElementDesign, region As Region, code As String) As barcode.Barcode.Shape
            Select Case design.Get("barcode_type")
                Case "ean8"
                    Dim barcode As New Ean8
                    If design.Get("without_text") Then
                        barcode.WithText = False
                    End If
                    Return barcode.CreateShape(region.Left, region.Top, region.GetWidth, region.GetHeight, code)
                Case "code39"
                    Dim barcode As New Code39
                    If design.Get("without_text") Then
                        barcode.WithText = False
                    End If
                    If design.Get("generate_checksum") Then
                        barcode.GenerateCheckSum = True
                    End If
                    Return barcode.CreateShape(region.Left, region.Top, region.GetWidth, region.GetHeight, code)
                Case "codabar"
                    Dim barcode As New Codabar
                    If design.Get("without_text") Then
                        barcode.WithText = False
                    End If
                    If design.Get("generate_checksum") Then
                        barcode.GenerateCheckSum = True
                    End If
                    Dim startCode As String = "A"
                    Dim stopCode As String = "A"
                    If Not design.IsNull("codabar_startstop_code") Then
                        Dim ss As String = design.Get("codabar_startstop_code")
                        If ss.Length = 1 Then
                            startCode = ss
                            stopCode = ss
                        ElseIf ss.Length > 1 Then
                            startCode = ss(0)
                            stopCode = ss(1)
                        End If
                    End If
                    If design.Get("codabar_startstop_show") Then
                        barcode.WithStartStopText = True
                    End If
                    Return barcode.CreateShape(region.Left, region.Top, region.GetWidth, region.GetHeight, startCode & code & stopCode)
                Case "itf"
                    Dim barcode As New Itf
                    If design.Get("without_text") Then
                        barcode.WithText = False
                    End If
                    If design.Get("generate_checksum") Then
                        barcode.GenerateCheckSum = True
                    End If
                    Return barcode.CreateShape(region.Left, region.Top, region.GetWidth, region.GetHeight, code)
                Case "code128"
                    Dim barcode As New Code128
                    If design.Get("without_text") Then
                        barcode.WithText = False
                    End If
                    Return barcode.CreateShape(region.Left, region.Top, region.GetWidth, region.GetHeight, code)
                Case "gs1_128"
                    Dim barcode As New Gs1_128
                    If design.Get("without_text") Then
                        barcode.WithText = False
                    End If
                    If design.Get("gs1_conveni") Then
                        barcode.ConveniFormat = True
                    End If
                    Return barcode.CreateShape(region.Left, region.Top, region.GetWidth, region.GetHeight, code)
                Case "yubin"
                    Dim barcode As New Yubin
                    Return barcode.CreateShape(region.Left, region.Top, region.GetWidth, region.GetHeight, code)
                Case "qrcode"
                    Dim w As New QRCodeWriter
                    Dim h As New Dictionary(Of EncodeHintType, Object)
                    If Not design.IsNull("qr_charset") Then
                        h.Add(EncodeHintType.CHARACTER_SET, design.Get("qr_charset"))
                    Else
                        h.Add(EncodeHintType.CHARACTER_SET, "SJIS")
                    End If
                    If Not design.IsNull("qr_correction_level") Then
                        Select Case design.Get("qr_correction_level")
                            Case "L"
                                h.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L)
                            Case "Q"
                                h.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.Q)
                            Case "H"
                                h.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H)
                            Case Else
                                h.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.M)
                        End Select
                    Else
                        h.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.M)
                    End If
                    h.Add(EncodeHintType.DISABLE_ECI, True)
                    Dim ret As New barcode.Barcode.Shape
                    Dim bm As BitMatrix
                    bm = w.encode(code, BarcodeFormat.QR_CODE, 0, 0, h)
                    Dim mw As Single = region.GetWidth / bm.Width
                    Dim mh As Single = region.GetHeight / bm.Height
                    For y As Integer = 0 To bm.Height - 1
                        For x As Integer = 0 To bm.Width - 1
                            If bm(x, y) Then
                                ret.Bars.Add(New barcode.Barcode.Shape.Bar(region.Left + mw * x, region.Top + mh * y, mw, mh))
                            End If
                        Next
                    Next
                    Return ret
                Case Else
                    Dim barcode As New Ean13
                    If design.Get("without_text") Then
                        barcode.WithText = False
                    End If
                    Return barcode.CreateShape(region.Left, region.Top, region.GetWidth, region.GetHeight, code)
            End Select
        End Function

        Protected Overridable Sub _RenderBarcode(renderer As PdfRenderer, shape As barcode.Barcode.Shape)
            If shape Is Nothing Then
                Exit Sub
            End If
            Dim cb = renderer.Writer.DirectContent
            cb.SaveState()
            Try
                cb.SetColorFill(Color.BLACK)
                For Each bar In shape.Bars
                    cb.Rectangle(renderer.Trans.X(bar.X), renderer.Trans.Y(bar.Y), bar.W, -bar.H)
                Next
                cb.Fill()
                If shape.Texts.Count > 0 Then
                    cb.BeginText()
                    Dim f = BaseFont.CreateFont("Helvetica", "winansi", False)
                    cb.SetFontAndSize(f, shape.FontSize)
                    For Each t In shape.Texts
                        If t.W > 0 Then
                            cb.SetTextMatrix(renderer.Trans.X(t.X + (t.W - f.GetWidthPoint(t.Text, shape.FontSize)) / 2), renderer.Trans.Y(t.Y + shape.FontSize))
                        Else
                            cb.SetTextMatrix(renderer.Trans.X(t.X + shape.FontSize * 0.2), renderer.Trans.Y(t.Y + shape.FontSize))
                        End If
                        cb.ShowText(t.Text)
                    Next
                    cb.EndText()
                End If
            Finally
                cb.RestoreState()
            End Try
        End Sub

    End Class

End Namespace