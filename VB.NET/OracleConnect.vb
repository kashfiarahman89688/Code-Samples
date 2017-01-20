Imports System.IO
Imports System.Globalization
Imports System.Security.Cryptography
Imports System.ComponentModel.Component
Imports System.Data.OracleClient.OracleCommand
Imports System.Data.OracleClient.OracleCommandBuilder
Imports System.Data.OracleClient.OracleDataAdapter



Module OracleConnect

    Public Sql As String
    Public tst As String
    Public ReportPath As String
    Public ReportFilename As String
    Public ReportCaption As String
    Public ReportDate As String
    Public BranchCode As String
    Public BranchName As String
    Public RefDate As String
    Public ShowRefDate As String
    Public info As String
    Public conn As New ADODB.Connection
    Public ConnectionString As New ADODB.Connection
    Public param As New ADODB.Recordset
    Public cl_type As String = "0"

    Public cmd As New ADODB.Command
    Public rssql As New ADODB.Recordset
    'Public rs1Clone As New ADODB.Recordset
    'Public rs1Temp As New ADODB.Recordset
    'Public rsTmp As New ADODB.Recordset
    'Public sqlStr As String
    Public currentUser As String
    'Public cl_type As String
    'Public servername As String
    Public userLevel As String

    Public Const ReportDSN As String = "DSN= BASIC_CL;uid=BASIC_CL;pwd=basiccl"
    Public Function connStr()
        Return "Driver={Microsoft ODBC for Oracle};" & _
            "Server=HODB_161;" & _
            "Uid=BASIC_CL;" & _
            "Pwd=basiccl"
        '---database user name and password "PIMSConnector"
    End Function
    Public Function OpenConn() As Boolean
        Try
            If conn.State = 1 Then conn.Close()
            conn.Open(connStr())
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return False
        End Try

        Return True
    End Function
    Public Function SetReportPath() As Boolean
        Try
            If rssql.State = 1 Then
                rssql.Close()
            End If
            rssql.Open("select * from cl_reportpath", conn, ADODB.CursorTypeEnum.adOpenKeyset)
            ReportPath = rssql.Fields("REPORT_PATH").Value.ToString()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return False
        End Try

        Return True
    End Function
   

End Module
