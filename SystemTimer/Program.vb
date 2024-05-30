Imports System
Imports System.Threading

Module SystemTimer
    ' Variable Settings
    Private Timer As Timer
    Private IntervalInSeconds As Integer
    Private ElapsedTime As Integer
    Private Callback As TimerCallback

    ' Event Setting
    Public Event TimerElapsed()

    ' Start Timer
    Public Sub StartTimer(duration As Integer, seconds As Integer)
        IntervalInSeconds = seconds
        duration = duration * 1000
        ElapsedTime = 0

        ' Map Timer CallBack
        If Timer Is Nothing Then
            Callback = New TimerCallback(AddressOf Timer_Tick)
            Timer = New Timer(Callback, Nothing, duration, duration)
        Else
            Timer.Change(duration, duration)
        End If
    End Sub

    ' Stop Timer
    Public Sub StopTimer()
        If Timer IsNot Nothing Then
            Timer.Change(Timeout.Infinite, Timeout.Infinite)
        End If
    End Sub

    ' Timer CallBack
    Private Sub Timer_Tick(state As Object)
        ' Set Timer Functions Here..
        ElapsedTime += 1
        If ElapsedTime >= IntervalInSeconds Then
            StopTimer()
            RaiseEvent TimerElapsed()
        End If
    End Sub
End Module


Module Program

    Sub Main(args As String())
        ' Start Timer
        SystemTimer.StartTimer(2, 10)

        ' Sample Task - TEST
        For cnt As Integer = 1 To 1000000
            Console.WriteLine("Hello, World!")
        Next
    End Sub
    
End Module
