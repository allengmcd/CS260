Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient



Partial Class customerLogin
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("mainMenu.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim i As String = TextBox2.Text
        Dim t As String = TextBox1.Text

        Dim conn As New OleDb.OleDbConnection(
     "Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
     Server.MapPath("~/Access/Bookstore.accdb"))


        Dim sql As String = "SELECT * FROM customers WHERE @name = customerName AND @id = customerID"

        conn.Open()
        'Dim sql As String = "SELECT * FROM books"
        Dim cmd As New OleDb.OleDbCommand(sql, conn)
        cmd.Parameters.AddWithValue("@name", t)
        cmd.Parameters.AddWithValue("@id", i)
        Dim dbread = cmd.ExecuteReader()
        searchResult.DataSource = dbread
        searchResult.DataBind()
        dbread.Close()
        conn.Close()


        If (searchResult.Items.Count = 1) Then
            Response.Redirect("customerMenu.aspx?id=" + i + "&name=" + t)

        Else
            Label4.Text = "Invalid Username or password"
        End If




    End Sub
End Class
