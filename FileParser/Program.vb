Imports System.IO
Imports System.Data
Imports System.Text

Public Module FileParser
    
    ' Create DataTable Using Text File
    Function ReadTextFile(filePath As String) As DataTable
        Dim table As New DataTable()

        Using reader As New StreamReader(filePath)
            ' Insert Column Name (with First Row)
            Dim line As String = reader.ReadLine()
            Dim headers As String() = line.Split(","c)
            For Each header As String In headers
                table.Columns.Add(header)
            Next

            ' Insert Datas
            While Not reader.EndOfStream
                Dim data As String() = reader.ReadLine().Split(","c)
                table.Rows.Add(data)
            End While
        End Using

        Return table
    End Function

    ' Create DataTable Using CSV File
    Function ReadCsvFile(filePath As String) As DataTable
        Dim table As New DataTable()

        Using reader As New StreamReader(filePath)
            ' Insert Column Name (with First Row)
            Dim headers As String() = reader.ReadLine().Split(","c)
            For Each header As String In headers
                table.Columns.Add(header)
            Next

            ' Insert Datas
            While Not reader.EndOfStream
                Dim data As String() = reader.ReadLine().Split(","c)
                table.Rows.Add(data)
            End While
        End Using

        Return table
    End Function

End Module

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello World!")
    End Sub
End Module
