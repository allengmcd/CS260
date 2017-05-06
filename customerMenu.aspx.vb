Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Class customerMenu
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim id As String = Server.HtmlEncode(Request.QueryString("id")).ToString
        Dim name As String = Server.HtmlEncode(Request.QueryString("name")).ToString
        Dim ii As Integer = 1

        Label1.Text = "Welcome" + name


        If ii = 1 Then


            ' Label1.Text = "Books written by " & a
            Dim conn As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
           Server.MapPath("~/Access/Bookstore.accdb"))
            conn.Open()
            Dim sql As String = "SELECT * FROM customers WHERE customerID = @id"

            Dim cmd As New OleDb.OleDbCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", id)
            Dim dbread = cmd.ExecuteReader()

            GridView1.DataSource = dbread
            GridView1.DataBind()
            dbread.Close()
            conn.Close()
        End If


        If ii = 1 Then


            ' Label1.Text = "Books written by " & a
            Dim conn As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
           Server.MapPath("~/Access/Bookstore.accdb"))
            conn.Open()
            Dim sql As String = "SELECT * FROM books WHERE ISBN IN (SELECT ISBN FROM bought_by WHERE customerID = @id)"

            Dim cmd As New OleDb.OleDbCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", id)
            Dim dbread = cmd.ExecuteReader()

            GridView2.DataSource = dbread
            GridView2.DataBind()
            dbread.Close()
            conn.Close()
        End If



    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim id As String = Server.HtmlEncode(Request.QueryString("id")).ToString
        Dim name As String = Server.HtmlEncode(Request.QueryString("name")).ToString

        Response.Redirect("searchBooks.aspx?id=" + id + "&name=" + name)
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("customerLogin.aspx")
    End Sub
End Class
