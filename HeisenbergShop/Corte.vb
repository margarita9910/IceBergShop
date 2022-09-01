Imports MySql.Data.MySqlClient

Public Class Corte
    Dim conn As New Conexion
    Dim da, da1, da2 As MySqlDataAdapter
    Dim ds, ds1, ds2 As DataSet
    Dim orden As Boolean = False
    Dim id As Integer
    Dim fecha As String = DateTime.Now.ToShortDateString()

    Private Sub Corte_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblnombre.Text = My.Settings.nombre
        Cargar(fecha)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If txtRecibo.Text = "" Then
                MessageBox.Show("Necesitas introducir una cantidad para continuar", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            End If
            lblCambio.Text = Decimal.Subtract(Decimal.Parse(lblTotal.Text.Replace(".", ",")), (Decimal.Parse(txtRecibo.Text.Replace(".", ",").Replace(".0", ".00")))).ToString.Replace(",", ".")
            If Decimal.Parse(lblCambio.Text.Replace(".", ",")) < 0.00 Then
                lblCambio.ForeColor = Color.DarkRed
                orden = False
            Else
                txtRecibo.ForeColor = Color.Black
                lblCambio.ForeColor = Color.White
                orden = True
            End If
        Catch ex As Exception
            txtRecibo.ForeColor = Color.DarkRed
            orden = True
        End Try
    End Sub

    Private Sub btnCorte_Click(sender As Object, e As EventArgs) Handles btnCorte.Click
        If orden And txtRecibo.Text <> "" Then
            conn.RegistrarVenta(-1, Decimal.Parse(txtRecibo.Text), My.Settings.usuario)
            MessageBox.Show("El retiro de efectivo de la caja se realizó en esta fecha: " + DateTime.Now, "Retiro de caja exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Finalize()
        Else
            MessageBox.Show("No es posible retirar más dinero de lo que hay en caja", "Verifica el monto a retirar", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub txtRecibo_TextChanged(sender As Object, e As EventArgs) Handles txtRecibo.TextChanged

    End Sub

    Private Sub txtRecibo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRecibo.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar = "." And Not txtRecibo.Text.IndexOf(".") Then
            e.Handled = True
        ElseIf e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub Cargar(ByVal fecha As String)
        da2 = conn.ReporteDiario(fecha, My.Settings.sucursal)
        ds2 = New DataSet()
        da2.Fill(ds2)
        Dim index As Integer = 0
        Dim suma As Decimal = 0.0
        Dim total As Decimal = 0.0
        Dim corte As Decimal = 0.0

        For Each fila As DataRow In ds2.Tables(0).Rows()

            If fila(2) = -1 Then
                suma = suma - fila(4)
                corte = corte + fila(4)
            Else
                suma = suma + fila(4)
                total = total + fila(4)
            End If
            index = index + 1
        Next
        lblTotal.Text = suma.ToString.Replace(",", ".")
        lblCambio.Text = lblTotal.Text
    End Sub
    Private Sub lblMinimize_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblMinimize.LinkClicked
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub lblClose_Click(sender As Object, e As EventArgs) Handles lblClose.Click
        Me.Finalize()
    End Sub
End Class