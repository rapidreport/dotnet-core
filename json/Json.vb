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
        _writeHash(data, writer)
    End Sub

    Public Sub WriteArray(data As ArrayList, writer As TextWriter)
        Using w As New JsonTextWriter(writer)
            WriteArray(data, w)
        End Using
    End Sub

    Public Sub WriteArray(data As ArrayList, writer As JsonWriter)
        _writeArray(data, writer)
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
            Return _readHash(reader)
        Else
            Return Nothing
        End If
    End Function

    Public Function ReadArray(reader As TextReader) As ArrayList
        Return ReadArray(New JsonTextReader(reader))
    End Function

    Public Function ReadArray(reader As JsonReader) As ArrayList
        If reader.Read Then
            Return _readArray(reader)
        Else
            Return Nothing
        End If
    End Function

    Private Sub _writeNode(data As Object, writer As JsonWriter)
        If TypeOf data Is Hashtable Then
            _writeHash(data, writer)
        ElseIf TypeOf data Is ArrayList Then
            _writeArray(data, writer)
        ElseIf data Is Nothing Then
            writer.WriteNull()
        Else
            writer.WriteValue(data)
        End If
    End Sub

    Private Sub _writeHash(data As Hashtable, writer As JsonWriter)
        writer.WriteStartObject()
        For Each k As String In data.Keys
            writer.WritePropertyName(k)
            _writeNode(data(k), writer)
        Next
        writer.WriteEndObject()
    End Sub

    Private Sub _writeArray(data As ArrayList, writer As JsonWriter)
        writer.WriteStartArray()
        For Each v As Object In data
            _writeNode(v, writer)
        Next
        writer.WriteEndArray()
    End Sub

    Private Function _readNode(reader As JsonReader) As Object
        Select Case reader.TokenType
            Case JsonToken.StartArray
                Return _readArray(reader)
            Case JsonToken.StartObject
                Return _readHash(reader)
            Case Else
                Return reader.Value
        End Select
    End Function

    Private Function _readArray(reader As JsonReader) As ArrayList
        Dim ret As New ArrayList
        Do While reader.Read
            If reader.TokenType = JsonToken.EndArray Then
                Return ret
            End If
            ret.Add(_readNode(reader))
        Loop
        Return ret
    End Function

    Private Function _readHash(reader As JsonReader) As Hashtable
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
            ret.Add(key, _readNode(reader))
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
        Await _writeHashAsync(data, writer)
    End Function

    Public Async Function WriteArrayAsync(data As ArrayList, writer As TextWriter) As Task
        Using w As New JsonTextWriter(writer)
            Await WriteArrayAsync(data, w)
        End Using
    End Function

    Public Async Function WriteArrayAsync(data As ArrayList, writer As JsonWriter) As Task
        Await _writeArrayAsync(data, writer)
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

    Public Async Function ReadArrayAsync(reader As TextReader) As Task(Of ArrayList)
        Return Await ReadArrayAsync(New JsonTextReader(reader))
    End Function

    Public Async Function ReadArrayAsync(reader As JsonReader) As Task(Of ArrayList)
        If Await reader.ReadAsync Then
            Return Await _readArrayAsync(reader)
        Else
            Return Nothing
        End If
    End Function

    Private Async Function _writeNodeAsync(data As Object, writer As JsonWriter) As Task
        If TypeOf data Is Hashtable Then
            Await _writeHashAsync(data, writer)
        ElseIf TypeOf data Is ArrayList Then
            Await _writeArrayAsync(data, writer)
        ElseIf data Is Nothing Then
            Await writer.WriteNullAsync()
        Else
            Await writer.WriteValueAsync(data)
        End If
    End Function

    Private Async Function _writeHashAsync(data As Hashtable, writer As JsonWriter) As Task
        Await writer.WriteStartObjectAsync()
        For Each k As String In data.Keys
            Await writer.WritePropertyNameAsync(k)
            Await _writeNodeAsync(data(k), writer)
        Next
        Await writer.WriteEndObjectAsync()
    End Function

    Private Async Function _writeArrayAsync(data As ArrayList, writer As JsonWriter) As Task
        Await writer.WriteStartArrayAsync()
        For Each v As Object In data
            Await _writeNodeAsync(v, writer)
        Next
        Await writer.WriteEndArrayAsync()
    End Function

    Private Async Function _readNodeAsync(reader As JsonReader) As Task(Of Object)
        Select Case reader.TokenType
            Case JsonToken.StartArray
                Return Await _readArrayAsync(reader)
            Case JsonToken.StartObject
                Return Await readHashAsync(reader)
            Case Else
                Return reader.Value
        End Select
    End Function

    Private Async Function _readArrayAsync(reader As JsonReader) As Task(Of ArrayList)
        Dim ret As New ArrayList
        Do While Await reader.ReadAsync
            If reader.TokenType = JsonToken.EndArray Then
                Return ret
            End If
            ret.Add(Await _readNodeAsync(reader))
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
            ret.Add(key, Await _readNodeAsync(reader))
        Loop
        Return ret
    End Function


End Module