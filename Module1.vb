Imports MySql.Data.MySqlClient
Module UniversalVariables

    Public myconn As MySqlConnection = New MySqlConnection
    Public dataset As New DataSet
    Public mycmd As MySqlCommand = New MySqlCommand
    Public myreader As MySqlDataReader
    Public sql As String

    Public Sub connect()

        mycmd.CommandText = sql
        mycmd.CommandType = CommandType.Text
        mycmd.Connection = myconn
        myconn.Open()
        myreader = mycmd.ExecuteReader

    End Sub


End Module

