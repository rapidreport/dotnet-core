Imports System.IO
Imports jp.co.systembase.report.renderer

Namespace imageloader

    Public Class GdiImageLoader
        Implements IGdiImageLoader

        Public ImageMap As GdiImageMap

        Public Sub New()
            Me.New(New GdiImageMap)
        End Sub

        Public Sub New(imageMap As GdiImageMap)
            Me.ImageMap = imageMap
        End Sub

        Public Sub New(imageMap As ImageMap)
            Me.ImageMap = New GdiImageMap
            For Each key In imageMap.Keys
                Me.ImageMap.Add(key, Image.FromStream(New MemoryStream(imageMap(key))))
            Next
        End Sub

        Public Function GetImage(param As Object) As Image Implements IGdiImageLoader.GetImage
            Return ImageMap(param)
        End Function

    End Class

End Namespace
