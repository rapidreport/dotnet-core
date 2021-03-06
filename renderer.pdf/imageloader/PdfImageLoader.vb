﻿Imports iTextSharp.text
Imports jp.co.systembase.report.renderer

Namespace imageloader

    Public Class PdfImageLoader
        Implements IPdfImageLoader

        Public ImageMap As PdfImageMap

        Public Sub New()
            Me.New(New PdfImageMap)
        End Sub

        Public Sub New(imageMap As PdfImageMap)
            Me.ImageMap = imageMap
        End Sub

        Public Sub New(imageMap As ImageMap)
            Me.ImageMap = New PdfImageMap
            For Each key As Object In imageMap.Keys
                Me.ImageMap.Add(key, Image.GetInstance(imageMap(key), imageMap(key).RawFormat))
            Next
        End Sub

        Public Function GetImage(param As Object) As Image Implements IPdfImageLoader.GetImage
            Return Me.ImageMap(param)
        End Function

    End Class

End Namespace
