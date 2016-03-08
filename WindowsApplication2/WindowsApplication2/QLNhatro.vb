Imports System.Data
Imports System.Data.SqlClient
Public Class QLNhatro
    Protected SqlCon As SqlConnection
    Protected SqlCom As SqlCommand
    Protected SqlDa As SqlDataAdapter
    Protected ConnectionString As String = "Data Source=DESKTOP-J18I3G0;Initial Catalog=QLNhaTro;Integrated Security=True"
    'Purpose: Class constructor.
    Public Sub New()
        'Initialize the class' members.
        SqlCon = New SqlConnection
        ' Set connection string of the SqlConnection object
        SqlCon.ConnectionString = ConnectionString
    End Sub

    ' Purpose: Opens a database connection
    ' if SQL connection open successful then return true
    ' else occur exception return false
    Public Function OpenConnection() As Boolean
        Try ' open connection
            If SqlCon.State <> ConnectionState.Open Then
                SqlCon.ConnectionString = ConnectionString
                SqlCon.Open()
                Return True
            Else
                Return False
            End If
        Catch ex As SqlException
            MessageBox.Show("Can not connect to database, please choose a database!", "Connect Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    ' Purpose: Close a database connection. 
    ' if SQL connection close successful then return true else
    ' occur exception return false
    Protected Function CloseConnection() As Boolean
        Try ' Close connection
            If SqlCon.State <> ConnectionState.Closed Then
                SqlCon.Close()
                Return True
            Else
                Return False
            End If
        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Close Database")
            Return False
        End Try
    End Function

    'Hàm thực thi các câu lệnh INSERT, UPDATE, DELETE
    Public Function ExecuteNoneQuery(ByVal v_sSqlString As String, Optional ByVal ShowErrorMessage As Boolean = False) As Boolean
        If OpenConnection() Then
            Try
                'sets the SqlConnection used by this SqlCom of the SqlCommand
                SqlCom = New SqlCommand(v_sSqlString, SqlCon)
                ' Executes a SQL statement against the connection and returns
                ' the number of rows affected.
                SqlCom.ExecuteNonQuery()
                Return True
            Catch ex As SqlException
                If ShowErrorMessage Then
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "Execute!")
                End If

                Return False
            Finally
                ' Close database connection.
                CloseConnection()
                SqlCom.Dispose()
            End Try
        End If
    End Function

    'Hàm thực thi câu lệnh SELECT 
    Public Function GetDataTable(ByVal v_sSqlString As String) As DataTable
        Dim sTableName As String = String.Empty
        Try
            sTableName = v_sSqlString.Substring(14)
            sTableName = sTableName.Substring(0, sTableName.IndexOf(" ") + 1)
        Catch ex As Exception
        End Try

        Dim dtable As New DataTable(sTableName)
        SqlDa = New SqlDataAdapter(v_sSqlString, SqlCon)
        If OpenConnection() Then
            Try
                ' Configures the schema to match that in the data source based on 
                ' the specified System.Data.SchemaType
                'SqlDa.FillSchema(dtable, SchemaType.Source)
                SqlDa.Fill(dtable)
            Catch ex As SqlException
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Get Datatable")
            Finally
                ' Close database connection.
                CloseConnection()
                SqlDa.Dispose()
            End Try
        End If
        Return dtable
    End Function

    Protected Overloads Sub Dispose()
        CloseConnection()
        GC.SuppressFinalize(Me)
    End Sub
End Class

