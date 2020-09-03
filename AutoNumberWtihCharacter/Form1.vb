Imports System.Data.SqlClient

Public Class Form1
    Dim con_str As String = "Data source=HACKER50CC\SQLEXPRESS;Initial catalog=test_db;Integrated Security=true"
    Dim con As New SqlConnection()
    Dim cmd As New SqlCommand
    Dim adapter As New SqlDataAdapter

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getData()
        AutoID()
    End Sub


    Sub getData()
        Using con = New SqlConnection(con_str)
            Try
                con.Open()
                Dim sql As String = "SELECT * FROM tbl_sale"
                cmd = New SqlCommand(sql, con)
                adapter = New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                adapter.Fill(ds)
                dgvSale.DataSource = ds.Tables(0)

            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub


    Sub AutoID()
        Using con = New SqlConnection(con_str)
            Try
                con.Open()
                Dim sql As String = "SELECT TOP 1 id,sale_id FROM tbl_sale ORDER BY id DESC"
                cmd = New SqlCommand(sql, con)
                adapter = New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                adapter.Fill(ds)

                If (ds.Tables(0).Rows.Count > 0) Then

                    Dim tmp_id = ds.Tables(0).Rows(0)("sale_id").ToString().Substring(3, 7)
                    Dim new_id As Integer = CInt(tmp_id) + 1
                    txtSaleID.Text = "INV" & new_id.ToString("0000000")

                Else
                    txtSaleID.Text = "INV0000001"
                End If



            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Using con = New SqlConnection(con_str)
            Try
                con.Open()
                Dim sql As String = "INSERT INTO tbl_sale (sale_id,item) VALUES (@sale_id,@item)"
                cmd = New SqlCommand(sql, con)
                cmd.Parameters.AddWithValue("sale_id", txtSaleID.Text)
                cmd.Parameters.AddWithValue("item", "Item " & Now)
                cmd.ExecuteNonQuery()


                getData()
                AutoID()

            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub
End Class
