Imports MySql.Data.MySqlClient

Public Class Precios

    Dim conn As New Conexion
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim ids As Integer
    Dim valor As Boolean = False
    Dim moux As Integer
    Dim mouy As Integer

    Private Sub Precios_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        valor = True
        moux = Cursor.Position.X - Me.Left
        mouy = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Precios_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If valor = True Then
            Me.Top = Cursor.Position.Y - mouy
            Me.Left = Cursor.Position.X - moux
        End If
    End Sub

    Private Sub Precios_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        valor = False
    End Sub

    Private Sub Precios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        refrescar()
    End Sub

    Private Sub refrescar()
        dgvProductos.Rows.Clear()
        da = conn.CargarPrecios()
        ds = New DataSet()
        da.Fill(ds)

        For Each fila As DataRow In ds.Tables(0).Rows()
            dgvProductos.Rows.Add(fila(0), fila(1), fila(2))
        Next
    End Sub

    Private Sub guardar()
        If txtPrecio.Text <> "" Then
            Dim result As DialogResult = MessageBox.Show("¿Desea conservar los cambios?", "Confirmar edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                conn.actualizarPrecio(Decimal.Parse(txtPrecio.Text.Replace(".0", ".00").Replace(".", ",")), ids)
                MessageBox.Show("Cambios efectuados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
                refrescar()
                limpiar()
            End If
        Else
            MessageBox.Show("No ha introducido un nuevo precio al producto.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub dgvProductos_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellClick

        If e.ColumnIndex = 3 Then
            txtPrecio.Enabled = True
            ids = dgvProductos.Rows(e.RowIndex).Cells(0).Value
            lblPrecio.Text = dgvProductos.Rows(e.RowIndex).Cells(2).Value.ToString.Replace(",", ".")
            If ids >= 1 And ids <= 6 Then
                pctBoxHelados.Image = My.Resources.helado__1_
            End If
            If ids >= 7 And ids <= 9 Then
                pctBoxHelados.Image = My.Resources.paleta_de_hielo
            End If
            If ids >= 10 And ids <= 11 Then
                pctBoxHelados.Image = My.Resources.frappuccino
            End If
            If ids >= 12 And ids <= 14 Then
                pctBoxHelados.Image = My.Resources.yogurt_congelado
            End If
            If ids >= 15 And ids <= 16 Then
                pctBoxHelados.Image = My.Resources.jugo_de_fresa
            End If
            If ids >= 17 And ids <= 18 Then
                pctBoxHelados.Image = My.Resources.helado__2_
            End If
            If ids >= 19 And ids <= 21 Then
                pctBoxHelados.Image = My.Resources.nachos__1_
            End If
            If ids >= 23 And ids <= 25 Then
                pctBoxHelados.Image = My.Resources.fresa
            End If
            If ids = 22 Then
                pctBoxHelados.Image = My.Resources.helado__1_
            End If

        End If

    End Sub

    Private Sub txtPrecio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrecio.KeyPress
        If e.KeyChar = Chr(13) And txtPrecio.Text IsNot "" Then
            guardar()
        End If

        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar = "." And Not txtPrecio.Text.IndexOf(".") Then
            e.Handled = True
        ElseIf e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        guardar()
    End Sub
    Private Sub limpiar()
        txtPrecio.Text = ""
        lblPrecio.Text = "0.00"
        ids = 0
        txtPrecio.Enabled = False
        pctBoxHelados.Image = My.Resources.heladoprecio
    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        limpiar()
    End Sub

    Private Sub lblMinimize_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblMinimize.LinkClicked
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub lblClose_Click(sender As Object, e As EventArgs) Handles lblClose.Click
        Me.Hide()
    End Sub
End Class