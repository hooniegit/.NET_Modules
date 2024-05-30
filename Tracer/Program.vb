Imports System
Imports System.IO
Imports System.Text

Public Class Tracer

    ' Initialization
    Private tracer As System.Diagnostics.TraceSource
    Public Sub New(name As String)
        tracer = New System.Diagnostics.TraceSource(name)
    End Sub

    ' Dict : Set Trace Level
    Private traceLevels As New Dictionary(Of String, TraceEventType)() From {
        {"debug", SourceLevels.Verbose},
        {"information", SourceLevels.Information},
        {"warning", SourceLevels.Warning},
        {"error", SourceLevels.Error},
        {"critical", SourceLevels.Critical}
    }

    ' Set Trace Level
    Sub SetLevel(levelString As String)
        Dim userDefinedLevel As SourceLevels = traceLevels(levelString.ToLower())
        tracer.Switch.Level = userDefinedLevel
    End Sub

    ' Add Text Listner
    Sub AddTextListener(dir As String)
        ' Check Dir
        If Not Directory.Exists(dir) Then
            Directory.CreateDirectory(dir)
        End If

        ' Create & Add Listner
        Dim filePath As String = Path.Combine(dir, DateTime.Now.ToString("yyyy-MM-dd_HH-mm") & ".log")
        Dim fileWriter As New StreamWriter(filePath, True, Encoding.UTF8)
        Dim textListener As New System.Diagnostics.TextWriterTraceListener(fileWriter)
        tracer.Listeners.Add(textListener)
    End Sub

    ' Add Console Listner
    Sub AddConsoleListener()
        Dim consoleListener As New System.Diagnostics.ConsoleTraceListener()
        tracer.Listeners.Add(consoleListener)
    End Sub

    ' Clear Listner
    Sub ClearListeners()
        tracer.Listeners.Clear()
    End Sub

    ' Trace
    Sub Trace(message As String, level As String)
        Dim eventType As TraceEventType = traceLevels(level.ToLower())
        tracer.TraceEvent(eventType, 0, DateTime.Now.ToString("HH:mm:ss - ") & level & " - " & message)
        tracer.Flush()
    End Sub

End Class

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello World!")
    End Sub
End Module
