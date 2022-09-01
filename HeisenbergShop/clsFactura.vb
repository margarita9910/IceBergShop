Imports System.Text
Imports System.Runtime.InteropServices
Public Class clsFactura
    Dim driver As New Driver
    Public Shared line As StringBuilder = New StringBuilder()
    Private ticket As String = ""
    Private parte1, parte2 As String
    Public Shared max As Integer = 40
    Private cort As Integer

    Public Shared Function LineasGuion() As String
        Dim LineaGuion As String = "----------------------------------------"
        Return line.AppendLine(LineaGuion).ToString()
    End Function

    Public Shared Sub EncabezadoVenta()
        Dim LineEncavesado As String = " Prod          Cant   Precio    Total"
        line.AppendLine(LineEncavesado)
    End Sub

    Public Sub TextoIzquierda(ByVal par1 As String)
        max = par1.Length

        If max > 40 Then
            cort = max - 40
            parte1 = par1.Remove(40, cort)
        Else
            parte1 = par1
        End If

        line.AppendLine(CSharpImpl.__Assign(ticket, parte1))
    End Sub

    Public Sub TextoDerecha(ByVal par1 As String)
        ticket = ""
        max = par1.Length

        If max > 40 Then
            cort = max - 40
            parte1 = par1.Remove(40, cort)
        Else
            parte1 = par1
        End If

        max = 40 - par1.Length

        For i As Integer = 0 To max - 1
            ticket += " "
        Next

        line.AppendLine(CSharpImpl.__Assign(ticket, parte1 & vbLf))
    End Sub

    Public Sub TextoCentro(ByVal par1 As String)
        ticket = ""
        max = par1.Length

        If max > 40 Then
            cort = max - 40
            parte1 = par1.Remove(40, cort)
        Else
            parte1 = par1
        End If

        max = CInt((40 - parte1.Length)) / 2

        For i As Integer = 0 To max - 1
            ticket += " "
        Next

        line.AppendLine(CSharpImpl.__Assign(ticket, parte1 & vbLf))
    End Sub

    Public Sub TextoExtremos(ByVal par1 As String, ByVal par2 As String)
        max = par1.Length

        If max > 18 Then
            cort = max - 18
            parte1 = par1.Remove(18, cort)
        Else
            parte1 = par1
        End If

        ticket = parte1
        max = par2.Length

        If max > 18 Then
            cort = max - 18
            parte2 = par2.Remove(18, cort)
        Else
            parte2 = par2
        End If

        max = 40 - (parte1.Length + parte2.Length)

        For i As Integer = 0 To max - 1
            ticket += " "
        Next

        line.AppendLine(CSharpImpl.__Assign(ticket, parte2 & vbLf))
    End Sub

    Public Sub AgregaTotales(ByVal par1 As String, ByVal total As Double)
        max = par1.Length

        If max > 25 Then
            cort = max - 25
            parte1 = par1.Remove(25, cort)
        Else
            parte1 = par1
        End If

        ticket = parte1
        parte2 = total.ToString("c")
        max = 40 - (parte1.Length + parte2.Length)

        For i As Integer = 0 To max - 1
            ticket += " "
        Next

        line.AppendLine(CSharpImpl.__Assign(ticket, parte2 & vbLf))
    End Sub

    Public Sub AgregaArticulo(ByVal Articulo As String, ByVal precio As Double, ByVal cant As Integer, ByVal subtotal As Double)
        If cant.ToString().Length <= 3 AndAlso precio.ToString("c").Length <= 10 AndAlso subtotal.ToString("c").Length <= 11 Then
            Dim elementos As String = "", espacios As String = ""
            Dim bandera As Boolean = False
            Dim nroEspacios As Integer = 0
            If Articulo.Length > 40 Then
                nroEspacios = (3 - cant.ToString().Length)
                espacios = ""

                For i As Integer = 0 To nroEspacios - 1
                    espacios += " "
                Next

                elementos += espacios & cant.ToString()
                nroEspacios = (10 - precio.ToString().Length)
                espacios = ""

                For i As Integer = 0 To nroEspacios - 1
                    espacios += " "
                Next

                elementos += espacios & precio.ToString()
                nroEspacios = (11 - subtotal.ToString().Length)
                espacios = ""

                For i As Integer = 0 To nroEspacios - 1
                    espacios += " "
                Next

                elementos += espacios & subtotal.ToString()
                Dim CaracterActual As Integer = 0
                Dim Longtext As Integer = Articulo.Length

                While Longtext > 16

                    If bandera = False Then
                        line.AppendLine(Articulo.Substring(CaracterActual, 16) & elementos)
                        bandera = True
                    Else
                        line.AppendLine(Articulo.Substring(CaracterActual, 16))
                    End If

                    CaracterActual += 16
                    Longtext += 1
                End While

                line.AppendLine(Articulo.Substring(CaracterActual, Articulo.Length - CaracterActual))
            Else

                For i As Integer = 0 To (16 - Articulo.Length) - 1
                    espacios += " "
                Next

                elementos = Articulo & espacios
                nroEspacios = (3 - cant.ToString().Length)
                espacios = ""

                For i As Integer = 0 To nroEspacios - 1
                    espacios += " "
                Next

                elementos += espacios & cant.ToString()
                nroEspacios = (10 - precio.ToString().Length)
                espacios = ""

                For i As Integer = 0 To nroEspacios - 1
                    espacios += " "
                Next

                elementos += espacios & precio.ToString()
                nroEspacios = (11 - subtotal.ToString().Length)
                espacios = ""

                For i As Integer = 0 To nroEspacios - 1
                    espacios += " "
                Next

                elementos += espacios & subtotal.ToString()
                line.AppendLine(elementos)
            End If
        Else
        End If
    End Sub

    Public Sub ImprimirTiket(ByVal stringimpresora As String)
        Driver.XD.SendStringToPrinter(stringimpresora, line.ToString())
        line = New StringBuilder()
    End Sub

    Private Class CSharpImpl
        <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
        Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Class
