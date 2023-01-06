Imports System.Drawing

Namespace imageloader
    Public Class XlsxImageLoader
        Implements IXlsxImageLoader

        Public ImageMap As ImageMap

        Private pool As New Dictionary(Of Byte(), Integer)

        Public Sub New()
            Me.New(New ImageMap)
        End Sub

        Public Sub New(imageMap As ImageMap)
            Me.ImageMap = imageMap
        End Sub

        Public Overridable Function GetImage(param As Object) As Byte() Implements IXlsxImageLoader.GetImage
            Return Me.ImageMap(param)
        End Function

    End Class
End Namespace