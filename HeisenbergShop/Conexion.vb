Imports MySql.Data.MySqlClient

Public Class Conexion
    Dim cadenaConexion As String = "server=" + My.Settings.host + ";database=" + My.Settings.database + ";user id=" + My.Settings.user + ";password=" + My.Settings.pass + ";port=3306;"
    Dim conn As New MySqlConnection(cadenaConexion)
    Dim cm As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet

    Public Function ReporteDiario(ByVal fecha As String, ByVal sucursal As Integer) As MySqlDataAdapter
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim Sql As String = "SELECT v.fecha, v.hora, v.turno, u.nombre, v.total FROM usuarios u inner join sucursales on sucursales.idSucursales = u.idsucursal inner join ventas v on v.idusuario = u.idusuario where u.idsucursal = @a and fecha =@b order by v.hora asc"


        Dim cmd = New MySqlCommand()
        cmd.CommandText = Sql
        cmd.CommandType = CommandType.Text
        cmd.Connection = conn
        cmd.Parameters.AddWithValue("@a", sucursal)
        cmd.Parameters.AddWithValue("@b", fecha)
        da = New MySqlDataAdapter(cmd)

        Return da
    End Function
    Public Sub Corte()

    End Sub
    Public Function Comprobar() As ConnectionState
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Return conn.State
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Hubo un error al momento de realizar la conexión. Verifica las variables e intenta nuevamente.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            BD.Show()
        End Try


    End Function
    Public Function EliminarUsuario(ByVal id As Integer) As Boolean
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Dim sql = "delete from usuarios where idusuario =@a"

        Dim cmd As New MySqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@a", id)
        cmd.ExecuteNonQuery()
    End Function

    Public Function EditarUsuario(ByVal id As Integer, ByVal nombre As String, ByVal acceso As String, ByVal contrasena As String, ByVal usuario As String, ByVal sucursal As Integer) As Boolean

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim sql = "update usuarios set nombre=@a, acceso=@b, contrasena=@c, usuario=@d, idsucursal=@e where idusuario = @f"

        Dim cmd As New MySqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@a", nombre)
        cmd.Parameters.AddWithValue("@b", acceso)
        cmd.Parameters.AddWithValue("@c", contrasena)
        cmd.Parameters.AddWithValue("@d", usuario)
        cmd.Parameters.AddWithValue("@e", sucursal)
        cmd.Parameters.AddWithValue("@f", id)

        cmd.ExecuteNonQuery()
    End Function
    Public Function VerUsuarios() As MySqlDataAdapter

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim Sql As String = "SELECT idUsuario, usuarios.nombre, usuarios.usuario, contrasena, sucursales.nombre, acceso FROM usuarios inner join sucursales on sucursales.idSucursales = usuarios.idsucursal
            where acceso <> 'S'"
        cm = New MySqlCommand()
        cm.CommandText = Sql
        cm.CommandType = CommandType.Text
        cm.Connection = conn
        da = New MySqlDataAdapter(cm)
        Return da

    End Function

    Public Function VerSucursales() As MySqlDataAdapter

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim Sql As String = "SELECT * FROM sucursales"
        cm = New MySqlCommand()
        cm.CommandText = Sql
        cm.CommandType = CommandType.Text
        cm.Connection = conn
        da = New MySqlDataAdapter(cm)
        Return da

    End Function

    Public Function VerUsuarios(ByVal sucursal As Integer) As MySqlDataAdapter

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Dim Sql As String = "SELECT * FROM usuarios where acceso <> 'S' and idSucursal = @a"
        cm = New MySqlCommand()
        cm.CommandText = Sql
        cm.CommandType = CommandType.Text
        cm.Connection = conn
        cm.Parameters.AddWithValue("@a", sucursal)

        da = New MySqlDataAdapter(cm)
        Return da

    End Function

    Public Function AgregarUsuario(ByVal nombre As String, ByVal acceso As String, ByVal contrasena As String, ByVal usuario As String, ByVal sucursal As Integer) As Boolean

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Dim sql = "insert into usuarios (nombre, acceso, contrasena, usuario, idsucursal) values (@a,@b,@c,@d,@e)"

        Dim cmd As New MySqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@a", nombre)
        cmd.Parameters.AddWithValue("@b", acceso)
        cmd.Parameters.AddWithValue("@c", contrasena)
        cmd.Parameters.AddWithValue("@d", usuario)
        cmd.Parameters.AddWithValue("@e", sucursal)

        cmd.ExecuteNonQuery()

    End Function

    Public Function actualizarPrecio(ByVal newprecio As Decimal, ByVal id As Integer)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim sql = "update productos set precio=@a where idProducto = @b"

        Dim cmd As New MySqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@a", newprecio)
        cmd.Parameters.AddWithValue("@b", id)


        cmd.ExecuteNonQuery()
    End Function
    Public Function CargarPrecios() As MySqlDataAdapter
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim Sql As String = "SELECT * FROM productos"
        cm = New MySqlCommand()
        cm.CommandText = Sql
        cm.CommandType = CommandType.Text
        cm.Connection = conn
        da = New MySqlDataAdapter(cm)
        Return da
    End Function

    Public Function RegistrarVenta(ByVal turno As Integer, ByVal total As Decimal, ByVal usuario As Integer) As Boolean
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim sql = "insert into ventas (turno, total, idUsuario, fecha, hora) values (@a,@b,@c,@d,@e)"

        Dim cmd As New MySqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@a", turno)
        cmd.Parameters.AddWithValue("@b", total)
        cmd.Parameters.AddWithValue("@c", usuario)
        cmd.Parameters.AddWithValue("@d", DateTime.Now.ToShortDateString())
        cmd.Parameters.AddWithValue("@e", DateTime.Now.ToString("HH:mm:ss"))
        cmd.ExecuteNonQuery()

    End Function

    Public Function Login(ByVal us As String, ByVal st As String) As MySqlDataReader
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Dim sql = "select idusuario, acceso, nombre, idsucursal from usuarios where usuario = @user and contrasena = @pass"

        Dim cmd As New MySqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@user", us)
        cmd.Parameters.AddWithValue("@pass", st)

        Return cmd.ExecuteReader() 'Devuelve el resultado, si no existe retorna vacío

        conn.Close()

    End Function

    Public Function Verificar(ByVal us As String, ByVal st As String, ByVal acc As String) As String
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Dim sql = "select idUsuario from usuarios where usuario = @user and contrasena = @pass and acceso = @acc"

        Dim cmd As New MySqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@user", us)
        cmd.Parameters.AddWithValue("@pass", st)
        cmd.Parameters.AddWithValue("@acc", acc)

        Return CStr(cmd.ExecuteScalar) 'Devuelve el resultado, si no existe retorna vacío
        conn.Close()
    End Function

    Public Function RetornarSucursal(ByVal id As Integer) As MySqlDataReader

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim Sql As String = "SELECT * FROM sucursales where idsucursales = @a"
        cm = New MySqlCommand()
        cm.CommandText = Sql
        cm.CommandType = CommandType.Text
        cm.Connection = conn
        cm.Parameters.AddWithValue("@a", id)

        Return cm.ExecuteReader()

    End Function

End Class
