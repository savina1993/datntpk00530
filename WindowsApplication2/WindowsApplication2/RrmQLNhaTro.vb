Imports System.Data.SqlClient
Imports System.Data
Public Class RrmQLNhaTro
    Dim sqlConnect As New QLNhatro
    Private Sub RrmQLNhaTro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadQLNhaTro()
    End Sub

    Private Sub LoadQLNhaTro()
        lwquanlinhatro.Items.Clear()
        Dim dt As DataTable = sqlConnect.GetDataTable("Select * from Danhsachthuetro")

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim item As New ListViewItem(lwquanlinhatro.Items.Count + 1)
            item.SubItems.Add(dt.Rows(i).ItemArray(1))
            item.SubItems.Add(dt.Rows(i).ItemArray(2))
            item.SubItems.Add(dt.Rows(i).ItemArray(3))
            item.SubItems.Add(dt.Rows(i).ItemArray(4))
            item.SubItems.Add(dt.Rows(i).ItemArray(5))
            item.SubItems.Add(dt.Rows(i).ItemArray(6))
            lwquanlinhatro.Items.Add(item)
        Next

    End Sub
    Private Sub BtThem_Click(sender As Object, e As EventArgs) Handles BtThem.Click
        
        sqlConnect.ExecuteNoneQuery("Insert into Danhsachthuetro(sophongtro,tennguoithuetro,GioiTinh,cmnd,sdt,DiaChi) values('" + txtsophongtro.Text + "',N'" + txtnguoithuetro.Text + "','" + txtgioitinh.Text + "','" + txtcmnd.Text + "',N'" + txtsdt.Text + "',N'" + txtdiachi.Text + "')")
        LoadQLNhaTro()

    End Sub

    Private Sub BtThoat_Click(sender As Object, e As EventArgs) Handles BtThoat.Click
        Application.Exit()
    End Sub

    Private Sub BtSua_Click(sender As Object, e As EventArgs) Handles BtSua.Click
      
        For i As Integer = lwquanlinhatro.SelectedIndices.Count - 1 To 0 Step -1
            Dim viTriCanSua As Integer = lwquanlinhatro.SelectedIndices(i)
            Dim tennguoiCanSua As String = lwquanlinhatro.Items(viTriCanSua).SubItems(1).Text
            sqlConnect.ExecuteNoneQuery("Update Danhsachthuetro set sophongtro='" + txtsophongtro.Text + "',tennguoithuetro=N'" + txtnguoithuetro.Text + "',GioiTinh='" + txtgioitinh.Text + "',cmnd=N'" + txtcmnd.Text + "',sdt='" + txtsdt.Text + "',diachi=N'" + txtdiachi.Text + "' where sophongtro=" + tennguoiCanSua)
        Next
        LoadQLNhaTro()

    End Sub

    Private Sub BtXoa_Click(sender As Object, e As EventArgs) Handles BtXoa.Click
        For i As Integer = lwquanlinhatro.SelectedIndices.Count - 1 To 0 Step -1
            Dim viTriCanXoa As Integer = lwquanlinhatro.SelectedIndices(i)
            Dim tenngnuoiCanXoa As String = lwquanlinhatro.Items(viTriCanXoa).SubItems(1).Text
            sqlConnect.ExecuteNoneQuery("Delete from danhsachthuetro where sophongtro =" + tenngnuoiCanXoa)
        Next
        LoadQLNhaTro()
    End Sub

   
    Private Sub lwquanlinhatro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lwquanlinhatro.SelectedIndexChanged
        For Each ds As ListViewItem In lwquanlinhatro.SelectedItems
            txtsophongtro.Text = ds.SubItems(1).Text
            txtnguoithuetro.Text = ds.SubItems(2).Text
            txtgioitinh.Text = ds.SubItems(3).Text
            txtcmnd.Text = ds.SubItems(4).Text
            txtsdt.Text = ds.SubItems(5).Text
            txtdiachi.Text = ds.SubItems(6).Text

        Next
    End Sub
End Class

