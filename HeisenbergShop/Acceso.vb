
Public Class Acceso
    Dim conn As New Conexion
    Dim usuarios As New Usuarios
    Dim productos As New Precios
    Dim reportes As New Reportes
    Dim valor As Boolean = False
    Dim moux As Integer
    Dim mouy As Integer
    Dim corte As New Corte

    Private Sub Acceso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtUser.Select()
    End Sub

    Private Sub startLogin()
        If txtUser.Text IsNot "" And txtPass IsNot "" Then
            Dim id As String = conn.Verificar(txtUser.Text, txtPass.Text, My.Settings.tipo)
            If id = My.Settings.usuario Then
                Me.Hide()
                If My.Settings.destino = "usuarios" Then
                    usuarios.Show()
                End If
                If My.Settings.destino = "productos" Then
                    Precios.Show()
                End If
                If My.Settings.destino = "reportes" Then
                    reportes.Show()
                End If
                If My.Settings.destino = "corte" Then
                    corte.Show()
                End If
            ElseIf id = "" Then
                MessageBox.Show("Este usuario no existe o no está autorizado. Vuelve a iniciar sesión desde el Login principal.", "Error de credenciales", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ElseIf id <> My.Settings.usuario Then
                MessageBox.Show("Estás accediendo desde la cuenta de otro administrador. Cierra sesión desde el panel principal y vuelve a ingresar desde tu cuenta.", "Administrador no autorizado.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("Requerimos que ambos campos estén llenos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub lblClose_Click(sender As Object, e As EventArgs) Handles lblClose.Click
        Me.Hide()
    End Sub

    Private Sub txtUser_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtUser.KeyDown
        If e.KeyCode = Keys.Enter Then
            startLogin()
        End If
    End Sub

    Private Sub txtPass_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPass.KeyDown
        If e.KeyCode = Keys.Enter Then
            startLogin()
        End If
    End Sub

    Private Sub Acceso_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        valor = True
        moux = Cursor.Position.X - Me.Left
        mouy = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Acceso_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If valor = True Then
            Me.Top = Cursor.Position.Y - mouy
            Me.Left = Cursor.Position.X - moux
        End If
    End Sub

    Private Sub Acceso_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        valor = False
    End Sub

    Private Sub lblMinimize_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblMinimize.LinkClicked
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnAcceder_Click(sender As Object, e As EventArgs) Handles btnAcceder.Click
        startLogin()
    End Sub
End Class