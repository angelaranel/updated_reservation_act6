Imports System.CodeDom
Imports System.Data.OleDb
Imports System.Diagnostics.Eventing.Reader
Imports System.IO
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class DataLoading
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = openFileDialog1.FileName
            Dim dt As New DataTable()
            Using sr As New StreamReader(filePath)
                Dim headers = sr.ReadLine().Split(","c)
                For Each header In headers
                    dt.Columns.Add(header)
                Next
                While Not sr.EndOfStream
                    Dim rows = sr.ReadLine().Split(","c)
                    dt.Rows.Add(rows)
                End While
            End Using
            csvdatagrid.DataSource = dt
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim OpenFileDialog2 As New OpenFileDialog()
        OpenFileDialog2.Filter = "Batch Files (*.bat)|*.bat|All Files (*.*)|*.*"
        OpenFileDialog2.FilterIndex = 1
        OpenFileDialog2.RestoreDirectory = True

        If OpenFileDialog2.ShowDialog() = DialogResult.OK Then
            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Filter = "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                Dim process As New Process()
                process.StartInfo.FileName = OpenFileDialog2.FileName
                process.StartInfo.Arguments = saveFileDialog1.FileName
                process.StartInfo.CreateNoWindow = True
                process.Start()
                process.WaitForExit()

                If process.ExitCode = 0 Then
                    MessageBox.Show("Backup created successfully.")
                Else
                    MessageBox.Show("Error creating backup.")
                End If
            End If
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub csvdatagrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles csvdatagrid.CellContentClick

    End Sub


End Class