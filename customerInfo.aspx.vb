Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Class customerInfo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim id As String = Server.HtmlEncode(Request.QueryString("id")).ToString

        Dim ii As Integer = 1

        Label1.Text = "Customer Info:"


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

            searchResult.DataSource = dbread
            searchResult.DataBind()
            dbread.Close()
            conn.Close()
        End If

    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("viewCustomers.aspx")
    End Sub
End Class
