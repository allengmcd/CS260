
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Collections
Imports Microsoft.VisualBasic



Partial Class searchBooks
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
       

        Dim coo As String = ""
        Dim Str As String = TextBox1.Text
        Str = LTrim(RTrim(Str))
        If Str = "" Then
            coo = "title LIKE '%%'"

        Else
            Dim strarr() As String
            Dim len = 0
            strarr = Str.Split(" "c)
            For Each s As String In strarr
                If len <> strarr.Length - 1 And s <> "" Then
                    coo = coo + " title LIKE '%" + s + "%' or "
                ElseIf s = "" Then

                Else
                    coo = coo + " title LIKE '%" + s + "%'"

                End If
                len = len + 1
            Next


        End If

        Dim high As Integer = 0
        Dim low As Integer = 0

        If (RadioButtonList1.SelectedValue.ToString = "10") Then
            high = 10
            low = 0
        ElseIf (RadioButtonList1.SelectedValue.ToString = "20") Then
            high = 20
            low = 10
        ElseIf (RadioButtonList1.SelectedValue.ToString = "30") Then
            high = 30
            low = 20
        ElseIf (RadioButtonList1.SelectedValue.ToString = "40") Then
            high = 40
            low = 30
        ElseIf (RadioButtonList1.SelectedValue.ToString = "50") Then
            high = 2147483647
            low = 40
        ElseIf (RadioButtonList1.SelectedValue.ToString = "0") Then
            high = 2147483647
            low = 0
        End If

        Dim jj As Integer = 4
        Dim id As String = Server.HtmlEncode(Request.QueryString("id")).ToString
        Dim name As String = Server.HtmlEncode(Request.QueryString("name")).ToString
        If jj = 4 Then
            Dim conn As New OleDb.OleDbConnection(
     "Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
     Server.MapPath("~/Access/Bookstore.accdb"))



            conn.Open()
            Dim sql As String = "SELECT * FROM books WHERE " + coo + "AND price > @low AND  price  <= @high;"
            'Dim sql As String = "SELECT title, count(ISBN) FROM books s LEFT OUTER JOIN bought_by a ON s.ISBN=a.ISBN GROUP BY title ORDER BY count(ISBN) DESC, title"
            'Dim sql As String = "SELECT ISBN, COUNT(ISBN) AS tot FROM bought_by GROUP BY ISBN ORDER BY tot DESC"
            'Dim sql As String = "SELECT * FROM books"
            Dim cmd As New OleDb.OleDbCommand(sql, conn)
            cmd.Parameters.AddWithValue("@low", low)
            cmd.Parameters.AddWithValue("@high", high)
            searchResult.DataValueField = "ISBN"
            searchResult.DataTextField = "title"

            Dim dbread = cmd.ExecuteReader()
            searchResult.DataSource = dbread
            searchResult.DataBind()
            dbread.Close()
            conn.Close()
        End If
        Dim cool As String = ""
        For i As Integer = 0 To searchResult.Items.Count - 1
            If (i = 0) Then
                cool = "WHERE ISBN = '" + searchResult.Items(i).Value + "'"

            Else

                cool = cool + " OR ISBN = '" + searchResult.Items(i).Value + "'"
            End If

        Next
        If jj = 4 Then
            Dim conn As New OleDb.OleDbConnection(
     "Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
     Server.MapPath("~/Access/Bookstore.accdb"))
            conn.Open()
            'Dim sql As String = "SELECT * FROM books WHERE " + coo + "AND price > @low AND  price  <= @high"
            'Dim sql As String = "SELECT ISBN, count(ISBN) AS total FROM bought_by WHERE ISBN = 84935758837412  GROUP BY ISBN ORDER BY total DESC"
            'Dim sql As String = "SELECT ISBN, COUNT(ISBN) AS tot FROM bought_by GROUP BY ISBN ORDER BY tot DESC"
            Dim sql As String = "SELECT ISBN, count(ISBN) FROM bought_by " + cool + " GROUP BY ISBN ORDER BY count(ISBN) DESC"
            Dim cmd As New OleDb.OleDbCommand(sql, conn)
            
            Dim dbread = cmd.ExecuteReader()
            rank.DataSource = dbread
            rank.DataBind()
            dbread.Close()
            conn.Close()
        End If

        Dim val As New ArrayList()
        Dim text As New ArrayList()

        For j As Integer = 0 To searchResult.Items.Count - 1
            val.Add(searchResult.Items(j).Value.ToString())
            text.Add(searchResult.Items(j).Text.ToString())

        Next

        Dim CheckedListBox1 As New CheckBoxList
        For i As Integer = 0 To rank.Rows.Count - 1
            For j As Integer = i To searchResult.Items.Count - 1
                If (rank.Rows(i).Cells(0).Text.ToString() = searchResult.Items(j).Value.ToString()) Then
                    Dim tempVal As String = searchResult.Items(j).Value.ToString()
                    Dim tempText As String = searchResult.Items(j).Text.ToString()
                    Label1.Text = Label1.Text + ": " + rank.Rows(i).Cells(0).Text.ToString() + " = " + searchResult.Items(j).Value.ToString()
                    searchResult.Items(j).Value = searchResult.Items(i).Value.ToString()
                    searchResult.Items(j).Text = searchResult.Items(i).Text.ToString()

                    searchResult.Items(j).Value = tempVal
                    searchResult.Items(j).Text = tempText
                    CheckedListBox1.Items.Add(tempText)

                   


                End If
            Next
        Next


        CheckedListBox1.Width = 300

        CheckedListBox1.Height = 400




        'For i As Integer = 0 To rank.Items.Count - 1
        'For j As Integer = i To rank.Items.Count - 1
        'If (Convert.ToInt16(rank.Items(i).Value) < Convert.ToInt16(rank.Items(j).Value)) Then
        'Label1.Text = Label1.Text + " " + searchResult.Items(i).Text
        ' If (jj = 2) Then
        'Dim tempVal As String = rank.Items(i).Value.ToString()
        'Dim tempText As String = rank.Items(i).Value.ToString()

        'rank.Items(i).Value = rank.Items(j).Value
        'rank.Items(i).Text = rank.Items(i).Text
        'rank.Items(j).Value = tempVal
        'rank.Items(j).Text = tempText
        'End If
        'Next
        'Next

        If (jj = 2) Then
            ' For i As Integer = 0 To rank.Items.Count - 1
            'For j As Integer = i To rank.Items.Count - 1
            'If (rank.Items(i).Text.ToString() = searchResult.Items(j).Value.ToString()) Then
            ' Dim tempVal As String = searchResult.Items(i).Value.ToString()
            ' Dim tempText As String = searchResult.Items(i).Value.ToString()

            'searchResult.Items(i).Value = rank.Items(j).Value
            'searchResult.Items(i).Text = rank.Items(i).Text
            'searchResult.Items(j).Value = tempVal
            'searchResult.Items(j).Text = tempText
            ' End If
            'Next
            'Next
        End If


        For i As Integer = 0 To searchResult.Items.Count - 1
            Dim hyp = New HyperLink
            hyp.Text = searchResult.Items(i).Text
            hyp.NavigateUrl = "custBookInfo.aspx?id=" + id + "&name=" + name + "&isbn=" + searchResult.Items(i).Value
            'searchResult.Items(i).Add(String.Format("<a href=\bookInfo.aspx?id=" + searchResult.Items(i).Value + "></a>"))
            pnlTest.Controls.Add(hyp)
            pnlTest.Controls.Add(New LiteralControl("<br/>"))

        Next


    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label3.Text = "Selected Books Purchased"
        Dim id As String = Server.HtmlEncode(Request.QueryString("id")).ToString
        For i As Integer = 0 To searchResult.Items.Count - 1

            Dim d As Integer = 1
            If (searchResult.Items(i).Selected) Then


                Dim isbn As String = searchResult.Items(i).Value
                Dim conn As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
    Server.MapPath("~/Access/Bookstore.accdb"))
                Dim sql As String = "INSERT INTO bought_by( ISBN, customerID ) VALUES( @isbn, @id )"
                Dim cmd As New OleDb.OleDbCommand(sql, conn)

                cmd.Parameters.AddWithValue("@isbn", isbn)
                cmd.Parameters.AddWithValue("@title", id)

                conn.Open()
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
            End If

            If (searchResult.Items(i).Selected) Then


                Dim isbn As String = searchResult.Items(i).Value
                Dim conn As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
    Server.MapPath("~/Access/Bookstore.accdb"))
                Dim sql As String = "SELECT price FROM books WHERE ISBN = @isbn"
                Dim cmd As New OleDb.OleDbCommand(sql, conn)

                cmd.Parameters.AddWithValue("@isbn", isbn)

                conn.Open()
                'cmd.ExecuteNonQuery()

                ListBox1.DataValueField = "price"


                Dim dbread = cmd.ExecuteReader()
                ListBox1.DataSource = dbread
                ListBox1.DataBind()
                dbread.Close()
                cmd.Dispose()
                conn.Close()
            End If



            If (searchResult.Items(i).Selected) Then
                Dim price As Integer = ListBox1.Items(0).Value
                Dim isbn As String = searchResult.Items(i).Value
                Dim conn As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" +
    Server.MapPath("~/Access/Bookstore.accdb"))
                Dim sql As String = "UPDATE customers SET amount = (amount + @tot) WHERE customerID = @id"
                Dim cmd As New OleDb.OleDbCommand(sql, conn)

                cmd.Parameters.AddWithValue("@tot", price)
                cmd.Parameters.AddWithValue("@id", id)

                conn.Open()
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
            End If


        Next



    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim id As String = Server.HtmlEncode(Request.QueryString("id")).ToString
        Dim name As String = Server.HtmlEncode(Request.QueryString("name")).ToString

        Response.Redirect("customerMenu.aspx?id=" + id + "&name=" + name)
    End Sub
End Class
