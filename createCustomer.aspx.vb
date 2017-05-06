Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient



Partial Class createCustomer
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("mainMenu.aspx")
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim conn As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
 Server.MapPath("~/Access/Bookstore.accdb"))
        Dim sql As String = "INSERT INTO customers( CustomerName, amount ) VALUES( @name, 0 )"
        Dim cmd As New OleDb.OleDbCommand(sql, conn)

        cmd.Parameters.AddWithValue("@name", TextBox1.Text)
        
        conn.Open()
        cmd.ExecuteNonQuery()
        cmd.Dispose()
        conn.Close()


        Label3.Text = "Account Successfully Created"
        TextBox1.Text = ""
    End Sub
End Class
