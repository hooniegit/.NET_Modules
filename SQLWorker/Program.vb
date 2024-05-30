Imports System.Data.OleDb
Imports System.Data

Public Class SQLWorker

    ' OleDbConnection
    Public sqlCon As System.Data.OleDb.OleDbConnection

    ' Connect to SQL Server
    Sub SqlConnect(serverip As String, database As String, loginid As String, loginpw As String)
        ' Create Connection
        Dim conString As String = "Provider=SQLOLEDB.1;Persist Security Info=True;Data Source=" + serverip + ";Initial Catalog=" + database + ";User ID=" + loginid + ";Password=" + loginpw 
        sqlCon = New System.Data.OleDb.OleDbConnection(conString)
        
        ' State Check
        If sqlCon.State <> 0 Then
            sqlCon.Close 
        End If

        ' Open Connection
        sqlCon.Open()
    End Sub

    ' Execute Query (except SELECT)
    Function ExecuteQuery(query As String) As Integer
        Dim sqlCmd As New System.Data.OleDb.OleDbCommand(query, sqlCon)
        Dim result As Integer
        result = sqlCmd.ExecuteNonQuery()

        Return result
    End Function

    ' Fetchall SELECT Query
    Function FetchallQuery(query As String) As DataTable
        Dim sqlCmd As New System.Data.OleDb.OleDbCommand(query, sqlCon)
        Dim da As New System.Data.OleDb.OleDbDataAdapter(sqlCmd)
        Dim result As New dataTable()

        da.Fill(result)
        Return result
    End Function

    ' Close Connection
    Function CloseConnection()
        If sqlCon IsNot Nothing AndAlso sqlCon.State <> ConnectionState.Closed Then
            sqlCon.Close()
        End If
    End Function

End Class

Module Program
    Sub Main(args As String())

        ' Create SQL Server Connection
        Dim sqlWorker As New SQLWorker()
        sqlWorker.SqlConnect("localhost,1433", "ctc_config", "sa", "!@34qwer")

        Dim SelectQuery as String = "SELECT * FROM ctc_dssource"
        Dim dt As DataTable = sqlWorker.FetchallQuery(SelectQuery)

        For Each row As DataRow In dt.Rows
            For Each col As DataColumn In dt.Columns
                Console.Write(row(col) & vbTab)
            Next
            Console.WriteLine()
        Next
    End Sub
End Module
