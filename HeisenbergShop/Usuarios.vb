Imports MySql.Data.MySqlClient

Public Class Usuarios
    Dim valor As Boolean = False
    Dim moux As Integer
    Dim mouy As Integer
    Dim conn As New Conexion
    Dim da, da1 As MySqlDataAdapter
    Dim ds, ds1 As DataSet
    Dim edicion As Boolean = False
    Dim id As Integer
    Private Sub lblClose_Click(sender As Object, e As EventArgs) Handles lblClose.Click
        Me.Close()
    End Sub

    Private Sub lblMinimize_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblMinimize.LinkClicked
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Usuarios_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        valor = True
        moux = Cursor.Position.X - Me.Left
        mouy = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Usuarios_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If valor = True Then
            Me.Top = Cursor.Position.Y - mouy
            Me.Left = Cursor.Position.X - moux
        End If
    End Sub

    Private Sub Usuarios_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        valor = False
    End Sub
    Private Sub CargarTabla()
        dgvUsuarios.Rows.Clear()
        da = conn.VerUsuarios()
        ds = New DataSet()
        da.Fill(ds)

        For Each fila As DataRow In ds.Tables(0).Rows()
            dgvUsuarios.Rows.Add(fila(0), fila(1), fila(2), fila(3), fila(4), fila(5))
        Next
    End Sub
    Private Sub Usuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarTabla()

        da1 = conn.VerSucursales()
        ds1 = New DataSet()
        da1.Fill(ds1)
        For Each fila As DataRow In ds1.Tables(0).Rows()
            txtSucursal.Items.Add(fila(1))

        Next

    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If txtNombre.Text <> "" And txtPass.Text <> "" And txtSucursal.Text <> "" And txtUsername.Text <> "" And txtAcceso.Text <> "" Then
            Dim acc As String
            If txtAcceso.Text = "Cajero" Then
                acc = "C"
            End If
            If txtAcceso.Text = "Administrador" Then
                acc = "A"
            End If
            If edicion = False Then
                conn.AgregarUsuario(txtNombre.Text, acc, txtPass.Text, txtUsername.Text, txtSucursal.SelectedIndex + 1)
                CargarTabla()
                limpiar()
            Else
                Try
                    conn.EditarUsuario(id, txtNombre.Text, acc, txtPass.Text, txtUsername.Text, txtSucursal.SelectedIndex + 1)
                    CargarTabla()
                    limpiar()
                    edicion = False
                Catch ex As Exception
                    MessageBox.Show("El usuario ya no existe en la base de datos, probablemente eliminaste su perfil anteriormente", "Usuario no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try

            End If
        Else
            MessageBox.Show("Requerimos que todos los campos estén llenos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub limpiar()
        txtNombre.Text = ""
        txtPass.Text = ""
        txtUsername.Text = ""
        txtSucursal.Text = ""
        txtAcceso.Text = ""
        edicion = False
        gpbEdicion.Text = "Agregar"
        btnOk.Text = "Agregar"
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        limpiar()
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Dim strInputString As String = "1234567890abcdefghijklmnopqrstuvwxyz"

        Dim intLength As Integer = Len(strInputString)

        Dim intNameLength As Integer = 10

        Randomize()

        Dim strName As String = ""

        For intStep = 1 To intNameLength
            Dim intRnd As Integer = Int((intLength * Rnd()) + 1)

            strName = strName & Mid(strInputString, intRnd, 1)
        Next

        txtPass.Text = strName
    End Sub

    Private Sub dgvUsuarios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsuarios.CellClick
        If e.ColumnIndex = 6 Then
            id = dgvUsuarios.Rows(e.RowIndex).Cells(0).Value
            txtNombre.Text = dgvUsuarios.Rows(e.RowIndex).Cells(1).Value
            txtUsername.Text = dgvUsuarios.Rows(e.RowIndex).Cells(2).Value
            txtPass.Text = dgvUsuarios.Rows(e.RowIndex).Cells(3).Value
            If dgvUsuarios.Rows(e.RowIndex).Cells(5).Value.ToString = "C" Then
                txtAcceso.Text = "Cajero"
            Else
                txtAcceso.Text = "Administrador"
            End If

            txtSucursal.Text = dgvUsuarios.Rows(e.RowIndex).Cells(4).Value
            gpbEdicion.Text = "Editar"
            btnOk.Text = "Guardar cambios"
            edicion = True
        End If
        If e.ColumnIndex = 7 Then
            Dim result As DialogResult = MessageBox.Show("¿Deseas eliminar permanentemente al usuario?",
                              "Eliminar usuario",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                Try
                    conn.EliminarUsuario(dgvUsuarios.Rows(e.RowIndex).Cells(0).Value)
                    CargarTabla()
                Catch ex As Exception
                    MessageBox.Show("El usuario ya no existe en la base de datos, probablemente eliminaste su perfil anteriormente", "Usuario no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End If


        End If
    End Sub
End Class