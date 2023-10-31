Imports Newtonsoft.Json
Imports System.IO

Public Module Json

    Public Sub Write(data As Hashtable, path As String)
        Using w As New StreamWriter(path)
            Write(data, w)
        End Using
    End Sub

    Public Sub Write(data As Hashtable, writer As TextWriter)
        Using w As New JsonTextWriter(writer)
            Write(data, w)
        End Using
    End Sub

    Public Sub Write(data As Hashtable, writer As JsonWriter)
        writeHash(data, writer)
    End Sub

    Public Function Read(path As String) As Hashtable
        Using reader As New StreamReader(path)
            Return Read(reader)
        End Using
    End Function

    Public Function Read(reader As TextReader) As Hashtable
        Return Read(New JsonTextReader(reader))
    End Function

    Public Function Read(reader As JsonReader) As Hashtable
        If reader.Read Then
            Return readHash(reader)
        Else
            Return Nothing
        End If
    End Function

    Private Sub writeNode(data As Object, writer As JsonWriter)
        If TypeOf data Is Hashtable Then
            writeHash(data, writer)
        ElseIf TypeOf data Is ArrayList Then
            writeArray(data, writer)
        ElseIf data Is Nothing Then
            writer.WriteNull()
        Else
            writer.WriteValue(data)
        End If
    End Sub

    Private Sub writeHash(data As Hashtable, writer As JsonWriter)
        writer.WriteStartObject()
        For Each k As String In data.Keys
            writer.WritePropertyName(k)
            writeNode(data(k), writer)
        Next
        writer.WriteEndObject()
    End Sub

    Private Sub writeArray(data As ArrayList, writer As JsonWriter)
        writer.WriteStartArray()
        For Each v As Object In data
            writeNode(v, writer)
        Next
        writer.WriteEndArray()
    End Sub

    Private Function readNode(reader As JsonReader) As Object
        Select Case reader.TokenType
            Case JsonToken.StartArray
                Return readArray(reader)
            Case JsonToken.StartObject
                Return readHash(reader)
            Case Else
                Return reader.Value
        End Select
    End Function

    Private Function readArray(reader As JsonReader) As ArrayList
        Dim ret As New ArrayList
        Do While reader.Read
            If reader.TokenType = JsonToken.EndArray Then
                Return ret
            End If
            ret.Add(readNode(reader))
        Loop
        Return ret
    End Function

    Private Function readHash(reader As JsonReader) As Hashtable
        Dim ret As New Hashtable
        Do While reader.Read
            Dim key As Object = Nothing
            If reader.TokenType = JsonToken.EndObject Then
                Return ret
            End If
            If reader.TokenType = JsonToken.PropertyName Then
                key = reader.Value
                reader.Read()
            End If
            ret.Add(key, readNode(reader))
        Loop
        Return ret
    End Function

    Public Async Function WriteAsync(data As Hashtable, path As String) As Task
        Using w As New StreamWriter(path)
            Await WriteAsync(data, w)
        End Using
    End Function

    Public Async Function WriteAsync(data As Hashtable, writer As TextWriter) As Task
        Using w As New JsonTextWriter(writer)
            Await WriteAsync(data, w)
        End Using
    End Function

    Public Async Function WriteAsync(data As Hashtable, writer As JsonWriter) As Task
        Await writeHashAsync(data, writer)
    End Function

    Public Async Function ReadAsync(path As String) As Task(Of Hashtable)
        Using reader As New StreamReader(path)
            Return Await ReadAsync(reader)
        End Using
    End Function

    Public Async Function ReadAsync(reader As TextReader) As Task(Of Hashtable)
        Return Await ReadAsync(New JsonTextReader(reader))
    End Function

    Public Async Function ReadAsync(reader As JsonReader) As Task(Of Hashtable)
        If Await reader.ReadAsync Then
            Return Await readHashAsync(reader)
        Else
            Return Nothing
        End If
    End Function

    Private Async Function writeNodeAsync(data As Object, writer As JsonWriter) As Task
        If TypeOf data Is Hashtable Then
            Await writeHashAsync(data, writer)
        ElseIf TypeOf data Is ArrayList Then
            Await writeArrayAsync(data, writer)
        ElseIf data Is Nothing Then
            Await writer.WriteNullAsync()
        Else
            Await writer.WriteValueAsync(data)
        End If
    End Function

    Private Async Function writeHashAsync(data As Hashtable, writer As JsonWriter) As Task
        Await writer.WriteStartObjectAsync()
        For Each k As String In data.Keys
            Await writer.WritePropertyNameAsync(k)
            Await writeNodeAsync(data(k), writer)
        Next
        Await writer.WriteEndObjectAsync()
    End Function

    Private Async Function writeArrayAsync(data As ArrayList, writer As JsonWriter) As Task
        Await writer.WriteStartArrayAsync()
        For Each v As Object In data
            Await writeNodeAsync(v, writer)
        Next
        Await writer.WriteEndArrayAsync()
    End Function

    Private Async Function readNodeAsync(reader As JsonReader) As Task(Of Object)
        Select Case reader.TokenType
            Case JsonToken.StartArray
                Return Await readArrayAsync(reader)
            Case JsonToken.StartObject
                Return Await readHashAsync(reader)
            Case Else
                Return reader.Value
        End Select
    End Function

    Private Async Function readArrayAsync(reader As JsonReader) As Task(Of ArrayList)
        Dim ret As New ArrayList
        Do While Await reader.ReadAsync
            If reader.TokenType = JsonToken.EndArray Then
                Return ret
            End If
            ret.Add(Await readNodeAsync(reader))
        Loop
        Return ret
    End Function

    Private Async Function readHashAsync(reader As JsonReader) As Task(Of Hashtable)
        Dim ret As New Hashtable
        Do While Await reader.ReadAsync
            Dim key As Object = Nothing
            If reader.TokenType = JsonToken.EndObject Then
                Return ret
            End If
            If reader.TokenType = JsonToken.PropertyName Then
                key = reader.Value
                Await reader.ReadAsync()
            End If
            ret.Add(key, Await readNodeAsync(reader))
        Loop
        Return ret
    End Function


End Module