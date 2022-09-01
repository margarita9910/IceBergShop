Imports MySql.Data.MySqlClient
Public Class BD
    Dim conn As New Conexion
    Dim valor As Boolean = False
    Dim moux As Integer
    Dim mouy As Integer

    Private Sub btnComprobar_Click(sender As Object, e As EventArgs) Handles btnComprobar.Click
        Try
            My.Settings.host = txthost.Text
            My.Settings.user = txtuser.Text
            My.Settings.pass = txtpass.Text
            If conn.Comprobar() = ConnectionState.Open Then
                MessageBox.Show("Se ha establecido una conexión a la base de datos.", "Conexión exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Finalize()
                LoginForm.Show()
            End If
        Catch ex As Exception
            MessageBox.Show("No se ha podido conectar a la base de datos.", "Conexión fallida", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoginForm_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        valor = True
        moux = Cursor.Position.X - Me.Left
        mouy = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub LoginForm_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If valor = True Then
            Me.Top = Cursor.Position.Y - mouy
            Me.Left = Cursor.Position.X - moux
        End If
    End Sub

    Private Sub LoginForm_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        valor = False
    End Sub

    Private Sub BD_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If conn.Comprobar() = ConnectionState.Open Then
            LoginForm.Show()
            Me.Finalize()
        End If
    End Sub

    Private Sub lblClose_Click(sender As Object, e As EventArgs) Handles lblClose.Click
        Me.Finalize()
    End Sub

    Private Sub lblMinimize_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblMinimize.LinkClicked
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class