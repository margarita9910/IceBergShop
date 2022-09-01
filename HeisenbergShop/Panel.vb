Imports HeisenbergShop.Conexion
Imports MySql.Data.MySqlClient
Public Class Panel

    Dim valor As Boolean = False
    Dim moux As Integer
    Dim mouy As Integer
    Dim conexion As Boolean
    Dim conn As New Conexion
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim status As Boolean = False

    Private Sub lblClose_Click(sender As Object, e As EventArgs) Handles lblClose.Click
        close()

    End Sub

    Private Sub lblMinimize_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblMinimize.LinkClicked
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Panel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        da = conn.CargarPrecios()
        ds = New DataSet()
        da.Fill(ds)
        dgvTemporal.DataSource = ds.Tables(0).DefaultView
        lblTurno.Text = My.Settings.turno
        If My.Settings.tipo = "C" Then
            btnUsuarios.Visible = False
            btnPrecios.Visible = False
            btnCaja.Visible = False
            btnReporte.Visible = False
        End If
    End Sub

    Private Sub Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        valor = True
        moux = Cursor.Position.X - Me.Left
        mouy = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If valor = True Then
            Me.Top = Cursor.Position.Y - mouy
            Me.Left = Cursor.Position.X - moux
        End If
    End Sub

    Private Sub Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        valor = False
    End Sub

    'Repartir precios 
    Private Function repartir(ByVal producto As Integer) As Decimal
        Dim contador As Integer = 0
        For j As Integer = 0 To dgvTemporal.RowCount - 1
            If CStr(dgvTemporal.Rows(j).Cells(0).Value) = producto.ToString Then
                contador = j
            End If
        Next
        Dim btn As New DataGridViewButtonColumn()
        btn.HeaderText = "Click Data"
        btn.Name = "btn"
        btn.Text = "-"
        btn.UseColumnTextForButtonValue = True
        If tablaticket.RowCount = 0 Then
            tablaticket.Rows.Add(1, dgvTemporal.Rows(contador).Cells(1).Value.ToString, "$" + dgvTemporal.Rows(contador).Cells(2).Value.ToString)
        Else
            Dim contador2 = -1
            For o As Integer = 0 To tablaticket.RowCount - 1
                If dgvTemporal.Rows(contador).Cells(1).Value.ToString = tablaticket.Rows(o).Cells(1).Value.ToString Then
                    contador2 = o
                End If
            Next
            If contador2 > -1 Then
                tablaticket.Rows(contador2).Cells(0).Value = tablaticket.Rows(contador2).Cells(0).Value + 1
                tablaticket.Rows(contador2).Cells(2).Value = "$" + (dgvTemporal.Rows(contador).Cells(2).Value * tablaticket.Rows(contador2).Cells(0).Value).ToString
            Else
                tablaticket.Rows.Add(1, dgvTemporal.Rows(contador).Cells(1).Value.ToString, "$" + dgvTemporal.Rows(contador).Cells(2).Value.ToString)
            End If
        End If
        sumar()
    End Function

    Private Sub sumar()
        Dim total As Decimal = 0.00
        For i As Integer = 0 To tablaticket.RowCount - 1
            Dim precio As String = ""
            For p As Integer = 0 To tablaticket.Rows(i).Cells(2).Value.ToString.Length - 1
                If tablaticket.Rows(i).Cells(2).Value.ToString(p) <> "$" Then
                    precio = precio + tablaticket.Rows(i).Cells(2).Value.ToString(p)
                End If
            Next
            total = total + Decimal.Parse(precio)
        Next
        lblTotal.Text = total.ToString.Replace(",", ".")
    End Sub


    Private Sub tablaticket_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles tablaticket.CellClick
        If e.ColumnIndex = 3 Then
            If tablaticket.Rows(e.RowIndex).Cells(0).Value > 1 Then
                tablaticket.Rows(e.RowIndex).Cells(0).Value = tablaticket.Rows(e.RowIndex).Cells(0).Value - 1
                tablaticket.Rows(e.RowIndex).Cells(2).Value = "$" + (dgvTemporal.Rows(e.RowIndex).Cells(2).Value * tablaticket.Rows(e.RowIndex).Cells(0).Value).ToString

            Else
                tablaticket.Rows.RemoveAt(e.RowIndex)
            End If

            sumar()
        End If
        If e.ColumnIndex = 4 Then
            tablaticket.Rows.RemoveAt(e.RowIndex)
            sumar()
        End If

    End Sub

    'Nieve Sencilla
    Private Sub btnNSencilla_Click(sender As Object, e As EventArgs) Handles btnNSencilla.Click
        repartir(1)
    End Sub

    Private Sub btnNDoble_Click(sender As Object, e As EventArgs) Handles btnNDoble.Click
        repartir(2)
    End Sub

    Private Sub btnNTriple_Click(sender As Object, e As EventArgs) Handles btnNTriple.Click
        repartir(3)
    End Sub

    Private Sub btnN12_Click(sender As Object, e As EventArgs) Handles btnN12.Click
        repartir(4)
    End Sub

    Private Sub btnN1_Click(sender As Object, e As EventArgs) Handles btnN1.Click
        repartir(5)
    End Sub

    Private Sub btnN5_Click(sender As Object, e As EventArgs) Handles btnN5.Click
        repartir(6)
    End Sub

    Private Sub btnNImporte_Click(sender As Object, e As EventArgs) Handles btnNImporte.Click
        repartir(22)
    End Sub

    Private Sub btnPFrut_Click(sender As Object, e As EventArgs) Handles btnPFrut.Click
        repartir(7)
    End Sub

    Private Sub btnPCrema_Click(sender As Object, e As EventArgs) Handles btnPCrema.Click
        repartir(8)
    End Sub

    Private Sub btnPEspecial_Click(sender As Object, e As EventArgs) Handles btnPEspecial.Click
        repartir(9)
    End Sub

    Private Sub btnFChico_Click(sender As Object, e As EventArgs) Handles btnFChico.Click
        repartir(10)
    End Sub

    Private Sub btnFGrande_Click(sender As Object, e As EventArgs) Handles btnFGrande.Click
        repartir(11)
    End Sub

    Private Sub btnCChica_Click(sender As Object, e As EventArgs) Handles btnCChica.Click
        repartir(12)
    End Sub

    Private Sub btnCGrande_Click(sender As Object, e As EventArgs) Handles btnCGrande.Click
        repartir(13)
    End Sub

    Private Sub btnCExtra_Click(sender As Object, e As EventArgs) Handles btnCExtra.Click
        repartir(14)
    End Sub

    Private Sub btnAChica_Click(sender As Object, e As EventArgs) Handles btnAChica.Click
        repartir(15)
    End Sub

    Private Sub btnAGrande_Click(sender As Object, e As EventArgs) Handles btnAGrande.Click
        repartir(16)
    End Sub

    Private Sub btnIChica_Click(sender As Object, e As EventArgs) Handles btnIChica.Click
        repartir(17)
    End Sub

    Private Sub btnIGrande_Click(sender As Object, e As EventArgs) Handles btnIGrande.Click
        repartir(18)
    End Sub

    Private Sub btnSSolos_Click(sender As Object, e As EventArgs) Handles btnSSolos.Click
        repartir(19)
    End Sub

    Private Sub btnSNormal_Click(sender As Object, e As EventArgs) Handles btnSNormal.Click
        repartir(20)
    End Sub

    Private Sub btnSExtra_Click(sender As Object, e As EventArgs) Handles btnSExtra.Click
        repartir(21)
    End Sub

    Private Sub Cambio()
        Try

            lblCambio.Text = Decimal.Subtract((Decimal.Parse(txtRecibo.Text.Replace(".", ",").Replace(".0", ".00"))), Decimal.Parse(lblTotal.Text.Replace(".", ","))).ToString.Replace(",", ".")
            If Decimal.Parse(lblCambio.Text.Replace(".", ",")) < 0.00 Then

                lblCambio.ForeColor = Color.DarkRed
                status = False

            Else

                txtRecibo.ForeColor = Color.White
                lblCambio.ForeColor = Color.White
                status = True

            End If
        Catch ex As Exception
            status = False
            txtRecibo.ForeColor = Color.DarkRed
        End Try
    End Sub
    Private Sub txtRecibo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRecibo.KeyPress

        If e.KeyChar = Chr(13) And txtRecibo.Text IsNot "" Then
            Cambio()
        End If

        If txtRecibo.Text = "" Then
            lblCambio.Text = "0.00"
        End If

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



    Private Sub Panel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

        If e.KeyChar.ToString.ToUpper = "Q" Then
            repartir(1)
        End If
        If e.KeyChar.ToString.ToUpper = "W" Then
            repartir(2)
        End If
        If e.KeyChar.ToString.ToUpper = "E" Then
            repartir(3)
        End If
        If e.KeyChar.ToString.ToUpper = "R" Then
            repartir(4)
        End If
        If e.KeyChar.ToString.ToUpper = "T" Then
            repartir(5)
        End If
        If e.KeyChar.ToString.ToUpper = "Y" Then
            repartir(6)
        End If
        If e.KeyChar.ToString.ToUpper = "U" Then
            repartir(22)
        End If
        If e.KeyChar.ToString.ToUpper = "I" Then
            repartir(7)
        End If
        If e.KeyChar.ToString.ToUpper = "O" Then
            repartir(8)
        End If
        If e.KeyChar.ToString.ToUpper = "P" Then
            repartir(9)
        End If

        If e.KeyChar.ToString.ToUpper = "A" Then
            repartir(10)
        End If
        If e.KeyChar.ToString.ToUpper = "S" Then
            repartir(11)
        End If
        If e.KeyChar.ToString.ToUpper = "D" Then
            repartir(12)
        End If
        If e.KeyChar.ToString.ToUpper = "F" Then
            repartir(13)
        End If
        If e.KeyChar.ToString.ToUpper = "G" Then
            repartir(14)
        End If
        If e.KeyChar.ToString.ToUpper = "H" Then
            repartir(15)
        End If
        If e.KeyChar.ToString.ToUpper = "J" Then
            repartir(16)
        End If
        If e.KeyChar.ToString.ToUpper = "K" Then
            repartir(17)
        End If
        If e.KeyChar.ToString.ToUpper = "L" Then
            repartir(18)
        End If
        If e.KeyChar.ToString.ToUpper = "Ñ" Then
            repartir(19)
        End If

        If e.KeyChar.ToString.ToUpper = "Z" Then
            repartir(20)
        End If
        If e.KeyChar.ToString.ToUpper = "X" Then
            repartir(21)
        End If
        If e.KeyChar.ToString.ToUpper = "C" Then
            repartir(23)
        End If
        If e.KeyChar.ToString.ToUpper = "V" Then
            repartir(24)
        End If
        If e.KeyChar.ToString.ToUpper = "B" Then
            repartir(25)
        End If


        If Char.IsSeparator(e.KeyChar) Then
            txtRecibo.Select()
        End If

    End Sub

    Private Sub txtRecibo_TextChanged(sender As Object, e As EventArgs) Handles txtRecibo.TextChanged

    End Sub

    Private Sub btnFSolas_Click(sender As Object, e As EventArgs) Handles btnFSolas.Click
        repartir(23)
    End Sub

    Private Sub btnFDobles_Click(sender As Object, e As EventArgs) Handles btnFDobles.Click
        repartir(24)
    End Sub

    Private Sub btnFTriples_Click(sender As Object, e As EventArgs) Handles btnFTriples.Click
        repartir(25)
    End Sub

    Private Sub impresion()
        Dim Ticket1 As New clsFactura
        Dim cmd As MySqlDataReader = conn.RetornarSucursal(My.Settings.sucursal)

        Dim nombre As String
        Dim direccion As String
        While cmd.Read()
            nombre = cmd.GetString(1)
            direccion = cmd.GetString(2)
        End While
        cmd.Close()

        Ticket1.TextoCentro(nombre)
        Ticket1.TextoCentro(direccion)
        Ticket1.TextoCentro("**********************************")

        Ticket1.TextoIzquierda("")
        Ticket1.TextoCentro("Turno")
        Ticket1.TextoIzquierda(My.Settings.turno)
        Ticket1.TextoIzquierda("Fecha:" + DateTime.Now.ToShortDateString() + " Hora:" + DateTime.Now.ToShortTimeString())
        Ticket1.TextoIzquierda("Le Atendio: " + My.Settings.nombre)
        Ticket1.TextoIzquierda("")
        clsFactura.LineasGuion()

        clsFactura.EncabezadoVenta()
        clsFactura.LineasGuion()
        For Each r In tablaticket.Rows
            Ticket1.AgregaArticulo(r.Cells(1).Value.ToString(), Double.Parse(r.Cells(0).Value.ToString().Replace("$", "").Replace(",", ".")), Integer.Parse(Double.Parse(r.Cells(2).Value.ToString().Replace("$", "")) / Double.Parse(r.Cells(0).Value.ToString().Replace("$", ""))), Double.Parse(r.Cells(2).Value.ToString.Replace("$", "").Replace(",", ".")))
        Next

        clsFactura.LineasGuion()
        Ticket1.TextoIzquierda(" ")
        Ticket1.AgregaTotales("Total", Double.Parse(lblTotal.Text))
        Ticket1.TextoIzquierda(" ")
        Ticket1.AgregaTotales("Efectivo Entregado:", Double.Parse(txtRecibo.Text))
        Ticket1.AgregaTotales("Efectivo Devuelto:", Double.Parse(lblCambio.Text))


        Ticket1.TextoIzquierda(" ")
        Ticket1.TextoCentro("**********************************")
        Ticket1.TextoCentro("*     Gracias por preferirnos    *")

        Ticket1.TextoCentro("**********************************")
        Ticket1.TextoIzquierda(" ")
        Dim impresora As String = "Microsoft XPS Document Writer"
        Ticket1.ImprimirTiket(impresora)

        MessageBox.Show("Gracias por preferirnos")

        Me.close()
    End Sub
    Private Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click

        Cambio()

        If tablaticket.RowCount > 0 And status Then
            Dim impr As Boolean = False

            Try
                impresion()
            Catch ex As Exception
                If MessageBox.Show("No se ha detectado una impresora térmica. ¿Desea realizar la venta?", "Impresora no encontrada", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = DialogResult.Yes Then
                    impr = True
                End If
            End Try
            If impr Then

                conn.RegistrarVenta(My.Settings.turno, lblTotal.Text.Replace(".", ","), My.Settings.usuario)
                My.Settings.turno = My.Settings.turno + 1
                If My.Settings.turno > 99 Then
                    My.Settings.turno = 1
                End If
                lblTurno.Text = My.Settings.turno
                tablaticket.Rows.Clear()
                lblTotal.Text = "0.00"
                txtRecibo.Text = ""
                lblCambio.Text = "0.00"

            End If
        Else
            MessageBox.Show("Verifica el estado de la venta (existencia de artículos o cambio correcto)", "La venta no está completa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        tablaticket.Rows.Clear()
        lblTotal.Text = "0.00"
        txtRecibo.Text = ""
        lblCambio.Text = "0.00"
    End Sub

    Private Sub btnUsuarios_Click(sender As Object, e As EventArgs) Handles btnUsuarios.Click
        Dim acceso As New Acceso
        acceso.Show()
        My.Settings.destino = "usuarios"
    End Sub

    Private Sub btnPrecios_Click(sender As Object, e As EventArgs) Handles btnPrecios.Click
        Dim acceso As New Acceso
        acceso.Show()
        My.Settings.destino = "productos"
    End Sub
    Private Sub close()
        If MessageBox.Show("Estas a punto de cerrar sesión. Haz clic en sí para confirmar.", "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
            My.Settings.usuario = ""
            My.Settings.tipo = ""
            My.Settings.turno = 1
            MessageBox.Show("¡Gracias por tu empeño, " + My.Settings.nombre + "!", "Hasta pronto")
            My.Settings.nombre = ""
            Dim login As New LoginForm
            login.Show()
            Me.Finalize()
        End If
    End Sub
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        close()
    End Sub

    Private Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        Dim acceso As New Acceso
        acceso.Show()
        My.Settings.destino = "reportes"
    End Sub

    Private Sub btnCaja_Click(sender As Object, e As EventArgs) Handles btnCaja.Click
        Dim acceso As New Acceso
        acceso.Show()
        My.Settings.destino = "corte"
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Drawing.Printing.PrintPageEventArgs)

    End Sub
End Class