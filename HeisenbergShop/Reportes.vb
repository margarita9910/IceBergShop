Imports MySql.Data.MySqlClient
Imports iText.Kernel.Pdf
Imports iText.Layout
Imports iText.Layout.Element
Imports iText.Layout.Properties
Imports System.IO
Imports iText.Kernel.Pdf.Canvas.Draw
Imports iText.Kernel.Colors
Imports iText.Kernel.Font
Imports Microsoft.Office.Core
Imports GemBox.Pdf

Public Class Reportes
    Dim valor As Boolean = False
    Dim moux As Integer
    Dim mouy As Integer
    Dim conn As New Conexion
    Dim da, da1, da2 As MySqlDataAdapter
    Dim ds, ds1, ds2 As DataSet
    Dim edicion As Boolean = False
    Dim id As Integer
    Dim fecha As String = DateTime.Now.ToShortDateString()

    Private Sub Reportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        da = conn.VerUsuarios(My.Settings.sucursal)
        ds = New DataSet()
        da.Fill(ds)
        For Each fila As DataRow In ds.Tables(0).Rows()
            'txtCajero.Items.Add(fila(1))
        Next
        lblNext.Visible = False
        btnNext.Enabled = False
        DateTimePicker1.Value = DateTime.Now()
        DateTimePicker1.MaxDate = DateTime.Now()

        da1 = conn.VerSucursales()
        ds1 = New DataSet()
        da1.Fill(ds1)
        For Each fila As DataRow In ds1.Tables(0).Rows()
            txtSucursal.Items.Add(fila(1))
            If fila(0) = My.Settings.sucursal Then
                txtSucursal.Text = fila(1)
            End If
        Next
        CargarTabla(DateTime.Now.ToShortDateString())
    End Sub

    Private Sub lblClose_Click(sender As Object, e As EventArgs) Handles lblClose.Click
        Me.Finalize()
    End Sub

    Private Sub lblMinimize_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblMinimize.LinkClicked
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        cambiarFecha(1)
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        cambiarFecha(-1)
    End Sub

    Private Sub cambiarFecha(ByVal resta As Integer)
        fecha = DateTime.Parse(fecha).AddDays(resta).ToShortDateString()
        CargarTabla(fecha)
        If fecha = DateTime.Now.ToShortDateString() Then
            lblNext.Visible = False
            btnNext.Visible = False
            lblBack.Text = "Ayer"
            lblMid.Text = "Hoy"
        Else
            lblNext.Visible = True
            btnNext.Enabled = True
            btnNext.Visible = True
            If fecha = DateTime.Now.AddDays(-1).ToShortDateString() Then
                lblBack.Text = DateTime.Parse(fecha).AddDays(-1).ToString("dd/MM")
                lblMid.Text = "Ayer"
                lblNext.Text = "Hoy"
            Else
                If fecha = DateTime.Now.AddDays(-2).ToShortDateString() Then
                    lblBack.Text = DateTime.Parse(fecha).AddDays(-1).ToString("dd/MM")
                    lblMid.Text = DateTime.Parse(fecha).ToString("dd/MM")
                    lblNext.Text = "Ayer"
                Else
                    lblBack.Text = DateTime.Parse(fecha).AddDays(-1).ToString("dd/MM")
                    lblMid.Text = DateTime.Parse(fecha).ToString("dd/MM")
                    lblNext.Text = DateTime.Parse(fecha).AddDays(1).ToString("dd/MM")
                End If
            End If
        End If

    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Dim exApp As New Microsoft.Office.Interop.Excel.Application
        Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
        Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet


        exLibro = exApp.Workbooks.Add
        exHoja = exLibro.Worksheets.Add()


        Dim NCol As Integer = dgvReporte.ColumnCount
        Dim NRow As Integer = dgvReporte.RowCount
        For i As Integer = 1 To NCol
            exHoja.Cells.Item(1, i) = dgvReporte.Columns(i - 1).HeaderText.ToString
        Next

        For Fila As Integer = 0 To NRow - 1
            For Col As Integer = 0 To NCol - 1
                exHoja.Cells.Item(Fila + 2, Col + 1) = dgvReporte.Rows(Fila).Cells(Col).Value
            Next
        Next



        exHoja.Rows.Item(1).Font.Bold = 1
        exHoja.Rows.Item(1).HorizontalAlignment = 3
        exHoja.Columns.AutoFit()


        exApp.Application.Visible = True

        exHoja = Nothing
        exLibro = Nothing
        exApp = Nothing
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        generarPDF(0)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        generarPDF(1)

    End Sub

    Private Sub generarPDF(ByVal Accion As Boolean)
        Dim SaveFileDialog1 As New SaveFileDialog
        SaveFileDialog1.Filter = "Archivo PDF (*.pdf*)|*.pdf"
        SaveFileDialog1.FileName = "IceBerg_Reporte_" + fecha.Replace("/", "_")
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim cmd As MySqlDataReader = conn.RetornarSucursal(My.Settings.sucursal)

            Dim nombre As String
            Dim direccion As String
            While cmd.Read()
                nombre = cmd.GetString(1)
                direccion = cmd.GetString(2)
            End While

            cmd.Close()
            Dim writer As New PdfWriter(SaveFileDialog1.FileName)
            Dim pdf As New iText.Kernel.Pdf.PdfDocument(writer)
            Dim Document As New Document(pdf)
            Dim header As New Paragraph(nombre)
            Dim dire As New Paragraph(direccion)
            Dim totaldeventas As New Paragraph("Total de ventas: $ " + lblTotal.Text)
            Dim cortecaja As New Paragraph("Retiros de efectivo: $ " + lblCorte.Text)
            Dim moneyfinal As New Paragraph("Final en caja: $ " + lblMoney.Text)

            Dim subheader As New Paragraph("Reporte del día " + fecha)

            Dim table As New Table(4, False)
            table.SetHorizontalAlignment(HorizontalAlignment.CENTER)
            Dim h1 As New Cell(1, 1)
            h1.SetBackgroundColor(ColorConstants.DARK_GRAY)
            h1.SetTextAlignment(TextAlignment.CENTER)
            h1.SetFontColor(ColorConstants.WHITE)
            h1.Add(New Paragraph("Hora"))
            table.AddCell(h1)
            Dim h2 As New Cell(1, 1)
            h2.SetBackgroundColor(ColorConstants.DARK_GRAY)
            h2.SetTextAlignment(TextAlignment.CENTER)
            h2.SetFontColor(ColorConstants.WHITE)
            h2.Add(New Paragraph("Turno"))
            table.AddCell(h2)
            Dim h3 As New Cell(1, 1)
            h3.SetBackgroundColor(ColorConstants.DARK_GRAY)
            h3.SetTextAlignment(TextAlignment.CENTER)
            h3.SetFontColor(ColorConstants.WHITE)
            h3.Add(New Paragraph("Responsable"))
            h3.SetWidth(300)
            table.AddCell(h3)
            Dim h4 As New Cell(1, 1)
            h4.SetBackgroundColor(ColorConstants.DARK_GRAY)
            h4.SetTextAlignment(TextAlignment.CENTER)
            h4.SetFontColor(ColorConstants.WHITE)
            h4.Add(New Paragraph("Total"))
            table.AddCell(h4)

            For Each row As DataRow In ds2.Tables(0).Rows()
                Dim colord As Color
                Dim newcell2 As New Cell(1, 1)


                If row(2).ToString = "-1" Then
                    colord = ColorConstants.LIGHT_GRAY
                    newcell2.Add(New Paragraph("Retiro"))
                Else
                    colord = ColorConstants.WHITE
                    newcell2.Add(New Paragraph(row(2).ToString))
                End If
                Dim newcell1 As New Cell(1, 1)
                newcell1.SetBackgroundColor(colord)
                newcell1.SetTextAlignment(TextAlignment.CENTER)
                newcell1.Add(New Paragraph(row(1).ToString))
                newcell1.SetFontSize(8)
                table.AddCell(newcell1)

                newcell2.SetBackgroundColor(colord)
                newcell2.SetTextAlignment(TextAlignment.CENTER)
                newcell2.SetFontSize(8)
                table.AddCell(newcell2)
                Dim newcell3 As New Cell(1, 1)
                newcell3.SetBackgroundColor(colord)
                newcell3.SetTextAlignment(TextAlignment.LEFT)
                newcell3.SetFontSize(8)
                newcell3.Add(New Paragraph(row(3).ToString))
                table.AddCell(newcell3)
                Dim newcell4 As New Cell(1, 1)
                newcell4.SetBackgroundColor(colord)
                newcell4.SetTextAlignment(TextAlignment.RIGHT)
                newcell4.SetFontSize(8)
                newcell4.Add(New Paragraph("$" + row(4).ToString))
                table.AddCell(newcell4)
            Next
            header.SetTextAlignment(TextAlignment.CENTER)
            header.SetFontSize(20)
            dire.SetTextAlignment(TextAlignment.CENTER)
            dire.SetFontSize(15)
            subheader.SetTextAlignment(TextAlignment.CENTER)
            subheader.SetFontSize(10)

            totaldeventas.SetTextAlignment(TextAlignment.RIGHT)
            totaldeventas.SetBold()
            totaldeventas.SetFontSize(12)

            cortecaja.SetTextAlignment(TextAlignment.RIGHT)
            cortecaja.SetBold()
            cortecaja.SetFontSize(12)

            moneyfinal.SetTextAlignment(TextAlignment.RIGHT)
            moneyfinal.SetBold()
            moneyfinal.SetFontSize(12)

            Dim separador As New LineSeparator(New SolidLine())
            Dim hora As New Paragraph("Hora de expedición:   " + DateTime.Now.ToShortTimeString)
            hora.SetTextAlignment(TextAlignment.RIGHT)
            hora.SetFontSize(10)



            Document.Add(header)
            Document.Add(dire)
            Document.Add(subheader)
            Document.Add(hora)
            Document.Add(separador)
            Document.Add(table)
            Document.Add(totaldeventas)
            Document.Add(cortecaja)
            Document.Add(moneyfinal)
            Dim n As Integer = pdf.GetNumberOfPages()

            For i As Integer = 1 To n Step 1
                Document.ShowTextAligned(String.Format("Página" + i.ToString + " de " + n.ToString), 559, 806, TextAlignment.RIGHT, VerticalAlignment.TOP, 0)
            Next

            Document.Close()
            If Accion Then
                Imprimir(SaveFileDialog1.FileName)
            Else
                MessageBox.Show("El documento se ha guardado como " + SaveFileDialog1.FileName, "PDF guardado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

    End Sub

    Public Async Function Imprimir(ByVal dir As String) As Task
        ComponentInfo.SetLicense("FREE-LIMITED-KEY")
        Using doc = GemBox.Pdf.PdfDocument.Load(dir)
            doc.Print()
            MessageBox.Show("El documento ha sido enviado al servicio de impresión", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Using
    End Function

    Private Sub txtSucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtSucursal.SelectedIndexChanged

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        cambiarFecha(DateDiff("d", fecha, DateTimePicker1.Value.ToShortDateString))
    End Sub

    Private Sub CargarTabla(ByVal fecha As String)
        dgvReporte.Rows.Clear()

        da2 = conn.ReporteDiario(fecha, My.Settings.sucursal)
        ds2 = New DataSet()
        da2.Fill(ds2)
        Dim index As Integer = 0
        Dim suma As Decimal = 0.0
        Dim total As Decimal = 0.0
        Dim corte As Decimal = 0.0

        For Each fila As DataRow In ds2.Tables(0).Rows()

            If fila(2) = -1 Then
                dgvReporte.Rows.Add(fila(0), fila(1), "Retiro caja", fila(3), "-$" + fila(4).ToString)
                dgvReporte.Rows(index).DefaultCellStyle.BackColor = Drawing.Color.MediumBlue
                suma = suma - fila(4)
                corte = corte + fila(4)
            Else
                dgvReporte.Rows.Add(fila(0), fila(1), fila(2), fila(3), "$" + fila(4).ToString)
                suma = suma + fila(4)
                total = total + fila(4)
            End If
            index = index + 1
        Next
        lblCorte.Text = corte.ToString.Replace(",", ".")
        lblTotal.Text = total.ToString.Replace(",", ".")
        lblMoney.Text = suma.ToString.Replace(",", ".")
    End Sub

    Private Sub Reportes_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        valor = True
        moux = Cursor.Position.X - Me.Left
        mouy = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Reportes_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If valor = True Then
            Me.Top = Cursor.Position.Y - mouy
            Me.Left = Cursor.Position.X - moux
        End If
    End Sub

    Private Sub Reportes_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        valor = False
    End Sub
End Class