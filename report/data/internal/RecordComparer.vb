﻿Namespace data.internal
    Public Class RecordComparer
        Implements IComparer(Of Integer)

        Private _DataSource As IReportDataSource
        Private _Keys As List(Of String)
        Private _Logger As IReportLogger

        Public Sub New(dataSource As IReportDataSource, keys As List(Of String), logger As IReportLogger)
            Me._DataSource = dataSource
            Me._Keys = keys
            Me._Logger = logger
        End Sub

        Public Function Compare(x As Integer, y As Integer) As Integer Implements IComparer(Of Integer).Compare
            For Each k As String In Me._Keys
                Dim _x As Object = Nothing
                Dim _y As Object = Nothing
                Dim _k As String = k
                Dim order As Integer = 1
                If _k.StartsWith("^") Then
                    _k = _k.Substring(1)
                    order = -1
                End If
                Try
                    _x = Me._DataSource.Get(x, _k)
                    _y = Me._DataSource.Get(y, _k)
                Catch ex As UnknownFieldException
                    Me._Logger.UnknownFieldError(ex)
                End Try
                If _x Is Nothing AndAlso _y Is Nothing Then
                    Continue For
                End If
                If _x Is Nothing Then
                    Return -1 * order
                End If
                If _y Is Nothing Then
                    Return 1 * order
                End If
                If _x < _y Then
                    Return -1 * order
                End If
                If _x > _y Then
                    Return 1 * order
                End If
            Next
            Return Math.Sign(x - y)
        End Function

    End Class

End Namespace