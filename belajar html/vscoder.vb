Imports MySql.Data.MySqlClient
Public Class Form1
    Dim strkon As String = "server=localhost;uid=root;database=karyawan_tambah"
    Dim kon As New MySqlConnection(strkon)
    Dim perintah As New MySqlCommand
    Dim mda As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim cek As MySqlDataReader

    Private Sub txtid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Enter
                kon.Open()
                perintah.Connection = kon
                perintah.CommandType = CommandType.Text
                perintah.CommandText = "select * from karyawan where idkaryawan='" & txtid.Text & "'"
                cek = perintah.ExecuteReader
                cek.Read()
                MsgBox("Data sudah ada...!", MsgBoxStyle.Information, "Pesan")
                If cek.HasRows Then
                    dtlahir.Value = cek.Item("tanggallahir")
                    dtmasuk.Value = cek.Item("tanggalmasuk")
                    txtnama.Text = cek.Item("namakaryawan")
                    txttempatlahir.Text = cek.Item("tempatlahir")
                    txtnohp.Text = cek.Item("nohp")
                    txtalamat.Text = cek.Item("alamat")

                    cmdsimpan.Enabled = False
                End If
                kon.Close()
                ' tidakaktif()
                cmdtambah.Enabled = True
        End Select
    End Sub

    Private Sub cmdtambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdtambah.Click
        aktif()
        txtid.Focus()
        cmdtambah.Enabled = False
    End Sub

    Private Sub cmdsimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsimpan.Click
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "insert into karyawan values " & _
            " ('" & txtid.Text & "', '" & txtnama.Text & "', '" & txttempatlahir.Text & "', " & _
            " '" & Format(dtlahir.Value, "yyyy-MM-dd") & "', '" & txtnohp.Text & "', '" & txtalamat.Text & "', " & _
            " '" & Format(dtmasuk.Value, "yyyy-MM-dd") & "')"
        perintah.ExecuteNonQuery()
        kon.Close()
        MsgBox("Data berhasil disimpan", MsgBoxStyle.Information, "Pesan")
        tampil()
        bersih()
    End Sub

    Private Sub cmdbatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdbatal.Click
        tidakaktif()
        cmdbatal.Enabled = True
        bersih()
    End Sub

    Private Sub cmdupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdupdate.Click
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "update karyawan set tanggalmasuk='" & Format(dtmasuk.Value, "yyyy-MM-dd") & "', " & _
        " namakaryawan='" & txtnama.Text & "', tempatlahir='" & txttempatlahir.Text & "', " & _
        " nohp='" & txtnohp.Text & "', alamat='" & txtalamat.Text & "' where idkaryawan='" & txtid.Text & "'"
        perintah.ExecuteNonQuery()
        kon.Close()
        tampil()
        bersih()
        tidakaktif()
    End Sub

    Private Sub cmdhapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdhapus.Click
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "delete from karyawan where idkaryawan='" &
        txtid.Text & "'"
        perintah.ExecuteNonQuery()
        kon.Close()
        tampil()
        bersih()
    End Sub


    Private Sub dg_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellClick
        Dim i As Integer
        i = dg.CurrentRow.Index
        txtid.Text = dg.Rows.Item(i).Cells(0).Value
        txtnama.Text = dg.Rows.Item(i).Cells(1).Value
        txttempatlahir.Text = dg.Rows.Item(i).Cells(2).Value
        dtlahir.Value = dg.Rows.Item(i).Cells(3).Value
        txtnohp.Text = dg.Rows.Item(i).Cells(4).Value
        txtalamat.Text = dg.Rows.Item(i).Cells(5).Value
        dtmasuk.Value = dg.Rows.Item(i).Cells(6).Value
    End Sub

    Sub tidakaktif()
        txtid.Enabled = False
        txtnama.Enabled = False
        txttempatlahir.Enabled = False
        dtlahir.Enabled = False
        txtnohp.Enabled = False
        txtalamat.Enabled = False
        dtmasuk.Enabled = False

        txtid.BackColor = Color.Gray
        txtnama.BackColor = Color.Gray
        txttempatlahir.BackColor = Color.Gray
        txtnohp.BackColor = Color.Gray
        txtalamat.BackColor = Color.Gray

        cmdhapus.Enabled = False
        cmdsimpan.Enabled = False
        cmdupdate.Enabled = False
        cmdbatal.Enabled = False
    End Sub


    Sub aktif()
        txtid.Enabled = True
        txtnama.Enabled = True
        txttempatlahir.Enabled = True
        dtlahir.Enabled = True
        txtnohp.Enabled = True
        txtalamat.Enabled = True
        dtmasuk.Enabled = True

        txtid.BackColor = Color.White
        txtnama.BackColor = Color.White
        txttempatlahir.BackColor = Color.White
        txtnohp.BackColor = Color.White
        txtalamat.BackColor = Color.White

        cmdhapus.Enabled = True
        cmdsimpan.Enabled = True
        cmdupdate.Enabled = True
        cmdbatal.Enabled = True
    End Sub


    Sub bersih()
        txtid.Text = ""
        txtnama.Text = ""
        txttempatlahir.Text = ""
        dtlahir.Text = ""
        txtnohp.Text = ""
        txtalamat.Text = ""
        dtmasuk.Text = ""
    End Sub


    Sub tampil()
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "select * from karyawan"
        mda.SelectCommand = perintah
        ds.Tables.Clear()
        mda.Fill(ds, "karyawan")
        dg.DataSource = ds.Tables("karyawan")
        kon.Close()
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tidakaktif()
        bersih()
        tampil()
    End Sub
End Class