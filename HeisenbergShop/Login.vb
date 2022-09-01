Imports HeisenbergShop.Conexion
Imports MySql.Data.MySqlClient

Public Class LoginForm

    Dim valor As Boolean = False
    Dim moux As Integer
    Dim mouy As Integer
    Dim conexion As Boolean
    Dim conn As New Conexion
    Dim panel As New Panel

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        startLogin()
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

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If conn.Comprobar() <> ConnectionState.Open Then
            Me.Finalize()
        End If
        txtUser.Select()

        'Try
        'If conn.Comprobar() = ConnectionState.Open Then
        'Me.lblStatus.Text = "Se ha establecido conexión con la base de datos en: " + My.Resources.host
        'conexion = True
        'End If
        'If conn.Comprobar() = ConnectionState.Closed Then
        'Me.lblStatus.Text = "No se ha podido conectar a la base de datos."
        'conexion = False
        'End If
        'Catch ex As Exception
        'BD.Show()
        'End Try


    End Sub

    Private Sub startLogin()
        If txtUser.Text IsNot "" And txtPass IsNot "" Then

            Dim cmd As MySqlDataReader = conn.Login(txtUser.Text, txtPass.Text)

            Dim tipo As String
            Dim id As String
            Dim nombre As String
            Dim sucursal As String
            While cmd.Read()
                id = cmd.GetString(0)
                tipo = cmd.GetString(1)
                nombre = cmd.GetString(2)
                sucursal = cmd.GetString(3)
            End While
            cmd.Close()
            My.Settings.usuario = id
            My.Settings.tipo = tipo
            My.Settings.nombre = nombre
            My.Settings.sucursal = sucursal
            If id = "" Then
                MessageBox.Show("Este usuario no existe en nuestra base de datos", "Usuario no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                panel.Show()
                Me.Finalize()
            End If
        Else
            MessageBox.Show("Requerimos que ambos campos estén llenos", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

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

    Private Sub lblClose_Click(sender As Object, e As EventArgs) Handles lblClose.Click
        Me.Finalize()
    End Sub

    Private Sub LoginForm_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
    End Sub

    Private Sub lblMinimize_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblMinimize.LinkClicked
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class
