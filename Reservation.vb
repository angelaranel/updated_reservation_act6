Imports Microsoft.Office.Interop
Imports MySql.Data.MySqlClient

Public Class Reservation

    Public currentdate As DateTime = DateTime.Now
    Public xlsPath As String = System.IO.Directory.GetCurrentDirectory & "\dataXls\template\"
    Public xlsFiles As String = System.IO.Directory.GetCurrentDirectory & "\dataXls\template"
    Dim connection As New MySqlConnection("server=localhost;port=3306;username=joana;password=joana0741;database=reservation")

    Public Function convertToLetters(ByVal number As Integer) As String
        number -= 1
        Dim result As String = String.Empty

        If (26 > number) Then
            result = Chr(number + 65)
        Else
            Dim column As Integer

            Do
                column = number Mod 26
                number = (number \ 26) - 1
                result = Chr(column + 65) + result
            Loop Until (number < 0)
        End If

        Return result

    End Function
    Public Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
    Public Sub importToExcel(ByVal mydg As DataGridView, ByVal templatefilename As String)
        Dim xlsApp As New Excel.Application
        Dim xlsWB As Excel.Workbook
        Dim xlsSheet As Excel.Worksheet

        xlsApp.Visible = False
        xlsWB = xlsApp.Workbooks.Open(xlsPath & templatefilename)

        xlsSheet = xlsWB.Worksheets(1)
        'xlsCell = xlsSheet.Range("A1")
        'xlsSheet.Cells(3, 1) = strfilter
        Dim x, y As Integer
        For x = 0 To mydg.RowCount - 1
            For y = 0 To mydg.ColumnCount - 1
                xlsSheet.Cells(x + 5, y + 1) = mydg.Rows(x).Cells(y).Value
            Next
        Next
        With xlsSheet.Range(convertToLetters(1) & 5, convertToLetters(mydg.ColumnCount) & x + 4)
            .Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous
        End With

        templatefilename = templatefilename.Replace(".xlsx", "")
        templatefilename = templatefilename.Replace(".xls", "")
        Dim myfilename As String = templatefilename & " " & currentdate.ToString("mm-dd-yy hh-mm-ss") & ".xlsx"
        MsgBox(myfilename)
        xlsApp.ActiveWindow.View = Excel.XlWindowView.xlPageLayoutView
        xlsApp.ActiveWindow.DisplayGridlines = False
        xlsWB.SaveAs(xlsFiles & myfilename)
        xlsApp.Quit()
        releaseObject(xlsApp)
        releaseObject(xlsWB)
        releaseObject(xlsSheet)
        System.Diagnostics.Process.Start("excel.exe", """" & xlsFiles & myfilename & """")
    End Sub
    Private Sub Reservation_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        myconn.ConnectionString = "server=localhost; user id=root;port=3306;password=joana0741;database=reservation_database"

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        myconn.Open()

        mycmd.Connection = myconn

        With mycmd

            .CommandText = "INSERT INTO reservation values (@reserve_num, @date_start, @date_end, @total_day, @venue_reserve, @venue_id, @event_id, @cus_id);"

            .CommandType = CommandType.Text
            .Parameters.AddWithValue("@reserve_num", numtxt.Text)
            .Parameters.AddWithValue("@date_start", starttxt.Text)
            .Parameters.AddWithValue("@date_end", endtxt.Text)
            .Parameters.AddWithValue("@total_day", daytxt.Text)
            .Parameters.AddWithValue("@venue_reserve", venue_reservetxt.Text)
            .Parameters.AddWithValue("@venue_id", venue_idtxt.Text)
            .Parameters.AddWithValue("@event_id", event_idtxt.Text)
            .Parameters.AddWithValue("@cus_id", cus_idtxt.Text)

        End With
        mycmd.ExecuteNonQuery()
        MsgBox("Data insert Successfully!")
        myconn.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        myconn.Open()

        mycmd.Connection = myconn

        With mycmd

            .CommandText = "UPDATE reservation SET date_start = @date_start, date_end = @date_end, total_day = @total_day, venue_reserve = @venue_reserve, venue_id = @venue_id, event_id =@event_id, cus_id = @cus_id where reserve_num = @reserve_num"

            .CommandType = CommandType.Text
            .Parameters.AddWithValue("@reserve_num", numtxt.Text)
            .Parameters.AddWithValue("@date_start", starttxt.Text)
            .Parameters.AddWithValue("@date_end", endtxt.Text)
            .Parameters.AddWithValue("@total_day", daytxt.Text)
            .Parameters.AddWithValue("@venue_reserve", venue_reservetxt.Text)
            .Parameters.AddWithValue("@venue_id", venue_idtxt.Text)
            .Parameters.AddWithValue("@event_id", event_idtxt.Text)
            .Parameters.AddWithValue("@cus_id", cus_idtxt.Text)


        End With

        mycmd.ExecuteNonQuery()
        MsgBox("Data Update Successfully!")
        myconn.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        myconn.Open()

        mycmd.Connection = myconn

        With mycmd

            .CommandText = "DELETE FROM reservation where reserve_num = @reserve_num"


            .CommandType = CommandType.Text
            .Parameters.AddWithValue("@reserve_num", numtxt.Text)
            .Parameters.AddWithValue("@date_start", starttxt.Text)
            .Parameters.AddWithValue("@date_end", endtxt.Text)
            .Parameters.AddWithValue("@total_day", daytxt.Text)
            .Parameters.AddWithValue("@venue_reserve", venue_reservetxt.Text)
            .Parameters.AddWithValue("@venue_id", venue_idtxt.Text)
            .Parameters.AddWithValue("@event_id", event_idtxt.Text)
            .Parameters.AddWithValue("@cus_id", cus_idtxt.Text)


        End With

        mycmd.ExecuteNonQuery()
        MsgBox("Data Delete Successfully!")
        myconn.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim SDA As New MySqlDataAdapter
        Dim dbdata As New DataTable
        Dim bSource As New BindingSource

        Try
            myconn.Open()
            sql = "Select * FROM reservation"
            mycmd = New MySqlCommand(sql, myconn)
            SDA.SelectCommand = mycmd
            SDA.Fill(dbdata)
            bSource.DataSource = dbdata
            reservationgrid.DataSource = bSource
            SDA.Update(dbdata)


            myconn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            myconn.Dispose()
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Call importToExcel(Me.reservationgrid, "samplereport.xlsx")
    End Sub
End Class