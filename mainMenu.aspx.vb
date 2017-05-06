
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient


Partial Class mainMenu
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("adminLogin.aspx")
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("customerLogin.aspx")
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Response.Redirect("createCustomer.aspx")
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim s As Integer = 1

        If s = 1 Then
            Dim conn As New OleDb.OleDbConnection(
         "Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
         Server.MapPath("~/Access/Bookstore.accdb"))
            Dim sql As String = "DELETE * FROM bought_by"
            Dim cmd As New OleDb.OleDbCommand(sql, conn)
            conn.Open()
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            conn.Close()
        End If


        If s = 1 Then
            Dim conn As New OleDb.OleDbConnection(
         "Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
         Server.MapPath("~/Access/Bookstore.accdb"))
            Dim sql As String = "DELETE * FROM books"
            Dim cmd As New OleDb.OleDbCommand(sql, conn)
            conn.Open()
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            conn.Close()
        End If


        If s = 1 Then
            Dim conn As New OleDb.OleDbConnection(
         "Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
         Server.MapPath("~/Access/Bookstore.accdb"))
            Dim sql As String = "DELETE * FROM customers"
            Dim cmd As New OleDb.OleDbCommand(sql, conn)
            conn.Open()
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            conn.Close()
        End If
    End Sub
End Class
