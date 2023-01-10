Imports jp.co.systembase.NPOI.SS.UserModel
Imports System.Text

Imports ZXing
Imports ZXing.Common
Imports ZXing.QrCode
Imports ZXing.QrCode.Internal

Imports jp.co.systembase.barcode
Imports jp.co.systembase.report.component
Imports jp.co.systembase.report.renderer.xlsx.component
Imports SkiaSharp
Imports System.Reflection

Namespace elementrenderer
    Public Class BarcodeRenderer
        Implements IElementRenderer

        Protected Const MARGIN_X = 2.0F
        Protected Const MARGIN_Y = 2.0F
        Protected Const SCALE = 10.0F

        Shared Sub New()
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
        End Sub

        Public Overridable Sub Collect(
          renderer As XlsxRenderer,
          reportDesign As ReportDesign,
          region As Region,
          design As ElementDesign,
          data As Object) Implements IElementRenderer.Collect
            Dim _region As Region = region.ToPointScale(reportDesign)
            Dim code As String = _GetCode(reportDesign, design, data)
            Dim shape As New Shape
            shape.Region = _region
            shape.Renderer = New BarcodeShapeRenderer(design, code)
            renderer.CurrentPage.Shapes.Add(shape)
        End Sub

        Protected Overridable Function _GetCode(reportDesign As ReportDesign, design As ElementDesign, data As Object) As String
            Return RenderUtil.Format(reportDesign, design.Child("formatter"), data)
        End Function

        Public Class BarcodeShapeRenderer
            Implements IShapeRenderer
            Public Code As String
            Public Design As ElementDesign

            Public Sub New(design As ElementDesign, code As String)
                Me.Code = code
                Me.Design = design
            End Sub

            Public Overridable Sub Render(page As Page, shape As Shape) Implements IShapeRenderer.Render
                If Code Is Nothing Then
                    Exit Sub
                End If
                If shape.Region.GetWidth = 0 OrElse shape.Region.GetHeight = 0 Then
                    Exit Sub
                End If
                Using bmp As New SKBitmap(shape.Region.GetWidth * SCALE, shape.Region.GetHeight * SCALE)
                    _RenderBarcode(bmp, _CreateBarcodeShape(bmp.Width, bmp.Height))
                    Dim p As IDrawing = page.Renderer.Sheet.CreateDrawingPatriarch
                    Dim index As Integer = page.Renderer.Workbook.AddPicture(
                        SKImage.FromBitmap(bmp).Encode(SKEncodedImageFormat.Png, 100).ToArray(),
                        NPOI.SS.UserModel.PictureType.PNG)
                    p.CreatePicture(shape.GetXSSFClientAnchor(page.TopRow), index)
                End Using
            End Sub

            Protected Overridable Function _CreateBarcodeShape(width As Single, height As Single) As barcode.Barcode.Shape
                Const MX = MARGIN_X * SCALE
                Const MY = MARGIN_Y * SCALE
                Select Case Design.Get("barcode_type")
                    Case "ean8"
                        Dim barcode As New Ean8
                        If Design.Get("without_text") Then
                            barcode.WithText = False
                        End If
                        Return barcode.CreateShape(MX, MY, width - MX * 2, height - MY * 2, Code)
                    Case "code39"
                        Dim barcode As New Code39
                        If Design.Get("without_text") Then
                            barcode.WithText = False
                        End If
                        If Design.Get("generate_checksum") Then
                            barcode.GenerateCheckSum = True
                        End If
                        Return barcode.CreateShape(MX, MY, width - MX * 2, height - MY * 2, Code)
                    Case "codabar"
                        Dim barcode As New Codabar
                        If Design.Get("without_text") Then
                            barcode.WithText = False
                        End If
                        If Design.Get("generate_checksum") Then
                            barcode.GenerateCheckSum = True
                        End If
                        Dim startCode As String = "A"
                        Dim stopCode As String = "A"
                        If Not Design.IsNull("codabar_startstop_code") Then
                            Dim ss As String = Design.Get("codabar_startstop_code")
                            If ss.Length = 1 Then
                                startCode = ss
                                stopCode = ss
                            ElseIf ss.Length > 1 Then
                                startCode = ss(0)
                                stopCode = ss(1)
                            End If
                        End If
                        If Design.Get("codabar_startstop_show") Then
                            barcode.WithStartStopText = True
                        End If
                        Return barcode.CreateShape(MX, MY, width - MX * 2, height - MY * 2, startCode & Code & stopCode)
                    Case "itf"
                        Dim barcode As New Itf
                        If Design.Get("without_text") Then
                            barcode.WithText = False
                        End If
                        If Design.Get("generate_checksum") Then
                            barcode.GenerateCheckSum = True
                        End If
                        Return barcode.CreateShape(MX, MY, width - MX * 2, height - MY * 2, Code)
                    Case "code128"
                        Dim barcode As New Code128
                        If Design.Get("without_text") Then
                            barcode.WithText = False
                        End If
                        Return barcode.CreateShape(MX, MY, width - MX * 2, height - MY * 2, Code)
                    Case "gs1_128"
                        Dim barcode As New Gs1_128
                        If Design.Get("without_text") Then
                            barcode.WithText = False
                        End If
                        If Design.Get("gs1_conveni") Then
                            barcode.ConveniFormat = True
                        End If
                        Return barcode.CreateShape(MX, MY, width - MX * 2, height - MY * 2, Code)
                    Case "yubin"
                        Dim barcode As New Yubin
                        Return barcode.CreateShape(MX, MY, width - MX * 2, height - MY * 2, Code)
                    Case "qrcode"
                        Dim w As New QRCodeWriter
                        Dim h As New Dictionary(Of EncodeHintType, Object)
                        If Not Design.IsNull("qr_charset") Then
                            h.Add(EncodeHintType.CHARACTER_SET, Design.Get("qr_charset"))
                        Else
                            h.Add(EncodeHintType.CHARACTER_SET, "SJIS")
                        End If
                        If Not Design.IsNull("qr_correction_level") Then
                            Select Case Design.Get("qr_correction_level")
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
                        bm = w.encode(Code, BarcodeFormat.QR_CODE, 0, 0, h)
                        Dim mw As Single = width / bm.Width
                        Dim mh As Single = height / bm.Height
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
                        If Design.Get("without_text") Then
                            barcode.WithText = False
                        End If
                        Return barcode.CreateShape(MX, MY, width - MX * 2, height - MY * 2, Code)
                End Select
            End Function

            Protected Overridable Sub _RenderBarcode(bmp As SKBitmap, shape As barcode.Barcode.Shape)
                If shape Is Nothing Then
                    Exit Sub
                End If
                Using canvas As New SKCanvas(bmp)
                    With Nothing
                        Dim paint = New SKPaint With {
                              .Color = SKColors.Black,
                              .Style = SKPaintStyle.Fill
                            }
                        For Each b In shape.Bars
                            canvas.DrawRect(b.X, b.Y, b.W, b.H, paint)
                        Next
                    End With
                    If shape.Texts.Count Then
                        Dim paint As New SKPaint With {
                          .TextSize = shape.FontSize,
                          .Color = SKColors.Black,
                          .Style = SKPaintStyle.Fill,
                          .IsAntialias = True,
                          .Typeface = SKTypeface.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("jp.co.systembase.report.renderer.xlsx.NotoSans-Regular.ttf"))
                        }
                        For Each t In shape.Texts
                            Dim x As Single = t.X
                            If t.W > 0 Then
                                x += (t.W - paint.MeasureText(t.Text)) / 2
                            Else
                                x += shape.FontSize * 0.25
                            End If
                            canvas.DrawText(t.Text, x, t.Y + shape.FontSize, paint)
                        Next
                    End If
                End Using
            End Sub

        End Class

    End Class
End Namespace