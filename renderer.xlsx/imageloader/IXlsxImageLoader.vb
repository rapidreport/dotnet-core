Imports System.Drawing

Namespace imageloader
    Public Interface IXlsxImageLoader
        Function GetImage(param As Object) As Byte()
    End Interface
End Namespace