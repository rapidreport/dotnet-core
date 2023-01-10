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

        Public Overridable Sub Render(
          renderer As PdfRenderer,
          reportDesign As ReportDesign,
          region As Region,
          design As ElementDesign,
          data As Object) Implements IElementRenderer.Render
            Dim code = _GetCode(reportDesign, design, data)
            If code Is Nothing Then
                Exit Sub
            End If
            Try
                Dim _region = region.ToPointScale(reportDesign)
                Dim image = _GetImage(renderer, _region, _CreateBarcodeShape(_region, design, code))
                If image IsNot Nothing Then
                    If Not Report.Compatibility._5_13_PdfBarcode Then
                        image.ScaleAbsolute(_region.GetWidth - 4, _region.GetHeight - 4)
                        image.SetAbsolutePosition(renderer.Trans.X(_region.Left + 2), renderer.Trans.Y(_region.Bottom - 2))
                    Else
                        image.ScaleAbsolute(_region.GetWidth - 2, _region.GetHeight - 2)
                        image.SetAbsolutePosition(renderer.Trans.X(_region.Left + 1), renderer.Trans.Y(_region.Bottom + 1))
                    End If
                    renderer.Writer.DirectContent.AddImage(image)
                End If
            Catch ex As Exception
            End Try
        End Sub

        Protected Overridable Function _GetCode(reportDesign As ReportDesign, design As ElementDesign, data As Object) As String
            Return RenderUtil.Format(reportDesign, design.Child("formatter"), data)
        End Function

        Protected Overridable Function _CreateBarcodeShape(region As Region, design As ElementDesign, code As String) As barcode.Barcode.Shape
            Select Case design.Get("barcode_type")
                Case "ean8"
                    Dim barcode As New Ean8
                    If design.Get("without_text") Then
                        barcode.WithText = False
                    End If
                    Return barcode.CreateShape(0, 0, region.GetWidth, region.GetHeight, code)
                Case "code39"
                    Dim barcode As New Code39
                    If design.Get("without_text") Then
                        barcode.WithText = False
                    End If
                    If design.Get("generate_checksum") Then
                        barcode.GenerateCheckSum = True
                    End If
                    Return barcode.CreateShape(0, 0, region.GetWidth, region.GetHeight, code)
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
                    Return barcode.CreateShape(0, 0, region.GetWidth, region.GetHeight, startCode & code & stopCode)
                Case "itf"
                    Dim barcode As New Itf
                    If design.Get("without_text") Then
                        barcode.WithText = False
                    End If
                    If design.Get("generate_checksum") Then
                        barcode.GenerateCheckSum = True
                    End If
                    Return barcode.CreateShape(0, 0, region.GetWidth, region.GetHeight, code)
                Case "code128"
                    Dim barcode As New Code128
                    If design.Get("without_text") Then
                        barcode.WithText = False
                    End If
                    Return barcode.CreateShape(0, 0, region.GetWidth, region.GetHeight, code)
                Case "gs1_128"
                    Dim barcode As New Gs1_128
                    If design.Get("without_text") Then
                        barcode.WithText = False
                    End If
                    If design.Get("gs1_conveni") Then
                        barcode.ConveniFormat = True
                    End If
                    Return barcode.CreateShape(0, 0, region.GetWidth, region.GetHeight, code)
                Case "yubin"
                    Dim barcode As New Yubin
                    Return barcode.CreateShape(0, 0, region.GetWidth, region.GetHeight, code)
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
                    Dim bm = w.encode(code, BarcodeFormat.QR_CODE, 0, 0, h)
                    Dim mw As Single = region.GetWidth / bm.Width
                    Dim mh As Single = region.GetHeight / bm.Height
                    For y As Integer = 0 To bm.Height - 1
                        For x As Integer = 0 To bm.Width - 1
                            If bm(x, y) Then
                                ret.Bars.Add(New barcode.Barcode.Shape.Bar(mw * x, mh * y, mw, mh))
                            End If
                        Next
                    Next
                    Return ret
                Case Else
                    Dim barcode As New Ean13
                    If design.Get("without_text") Then
                        barcode.WithText = False
                    End If
                    Return barcode.CreateShape(0, 0, region.GetWidth, region.GetHeight, code)
            End Select
        End Function

        Protected Overridable Function _GetImage(renderer As PdfRenderer, region As Region, shape As barcode.Barcode.Shape) As Image
            If shape Is Nothing Then
                Return Nothing
            End If
            Dim tmp = renderer.Writer.DirectContent.CreateTemplate(region.GetWidth, region.GetHeight)
            tmp.SetColorFill(Color.BLACK)
            For Each bar In shape.Bars
                tmp.Rectangle(bar.X, region.GetHeight - bar.Y, bar.W, -bar.H)
            Next
            tmp.Fill()
            If shape.Texts.Count > 0 Then
                tmp.BeginText()
                Dim f = BaseFont.CreateFont("Helvetica", "winansi", False)
                tmp.SetFontAndSize(f, shape.FontSize)
                For Each t In shape.Texts
                    If t.W > 0 Then
                        tmp.SetTextMatrix(t.X + (t.W - f.GetWidthPoint(t.Text, shape.FontSize)) / 2, region.GetHeight - t.Y - shape.FontSize)
                    Else
                        tmp.SetTextMatrix(t.X + shape.FontSize * 0.2, region.GetHeight - t.Y - shape.FontSize)
                    End If
                    tmp.ShowText(t.Text)
                Next
                tmp.EndText()
            End If
            Return Image.GetInstance(tmp)
        End Function

    End Class

End Namespace