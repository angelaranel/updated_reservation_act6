Imports System.Net
Imports MySql.Data.MySqlClient

Public Class LoginForm1

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See https://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub LoginForm1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        myconn.ConnectionString = "server=localhost; user id=root;port=3306;password=joana0741;database=reservation_database"

    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        sql = "SELECT * FROM users WHERE username = '" & usernametxt.Text & "' AND  password = '" & passwordtxt.Text & "' "
        connect()

        If myreader.Read Then

            MsgBox("Welcome Admin! You Login Successfully!")
            Me.Hide()
            Form1.Show()

        Else

            MsgBox("Invalid Username or Password")

        End If

        myconn.Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then

            passwordtxt.UseSystemPasswordChar = False

        Else

            passwordtxt.UseSystemPasswordChar = True

        End If
    End Sub
End Class
