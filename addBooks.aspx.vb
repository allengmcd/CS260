Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient




Partial Class addBooks
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("adminMenu.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim conn As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
 Server.MapPath("~/Access/Bookstore.accdb"))
        Dim sql As String = "INSERT INTO books( ISBN, title, price ) VALUES( @isbn, @title, @price )"
        Dim cmd As New OleDb.OleDbCommand(sql, conn)

        cmd.Parameters.AddWithValue("@isbn", TextBox1.Text)
        cmd.Parameters.AddWithValue("@title", TextBox2.Text)
        cmd.Parameters.AddWithValue("@price", TextBox3.Text)
        conn.Open()
        cmd.ExecuteNonQuery()
        cmd.Dispose()
        conn.Close()


        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        Label5.Text = "Inserted Successfully"
    End Sub
End Class
