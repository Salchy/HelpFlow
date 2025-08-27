-- USE HelpFlow;

CREATE PROCEDURE SP_CrearCategoria (
	@Nombre VARCHAR(75)
)
AS
BEGIN
	INSERT INTO Categorias(Nombre)
	OUTPUT inserted.Id
	VALUES (@Nombre);
END
GO

CREATE PROCEDURE SP_CrearSubCategoria (
	@Nombre VARCHAR(75),
	@IdCategoriaPadre TINYINT
)
AS
BEGIN
	INSERT INTO SubCategorias(Nombre, IdCategoriaPadre)
	OUTPUT inserted.Id
	VALUES (@Nombre, @IdCategoriaPadre);
END
GO

CREATE PROCEDURE SP_CrearUsuario (
	@userName VARCHAR(50),
	@name VARCHAR(50),
	@email VARCHAR(100),
	@password CHAR(64),
	@idEmpresa TINYINT,
	@TipoUsuario BIT
)
AS
BEGIN
	INSERT INTO Usuarios(UserName, Nombre, Correo, Clave, IdEmpresa, TipoUsuario)
	OUTPUT inserted.Id
	VALUES (@userName, @name, @email, @password, @idEmpresa, @TipoUsuario);
END
GO

CREATE PROCEDURE SP_ModificarUsuario (
	@id INT,
	@name VARCHAR(50),
	@email VARCHAR(100),
	@idEmpresa TINYINT,
	@tipoUsuario BIT
)
AS
BEGIN
	UPDATE Usuarios SET
		Nombre = @name,
		Correo = @email,
		IdEmpresa = @idEmpresa,
		TipoUsuario = @tipoUsuario
	WHERE Id = @id;
END
GO

CREATE PROCEDURE SP_CrearTicket (
	@owner INT,
	@idSubCategoria TINYINT,
	@message VARCHAR(500)
)
AS BEGIN
	INSERT INTO Tickets(IdUsuarioCreador, IdSubCategoria, Descripcion)
	OUTPUT inserted.Id
	VALUES (@owner, @idSubCategoria, @message);
END
GO

CREATE PROCEDURE SP_UpdatePassword (
	@idUsuario INT,
	@clave CHAR(64)
)
AS BEGIN
	UPDATE Usuarios SET Clave = @clave WHERE Id = @idUsuario;
END
GO

CREATE PROCEDURE SP_RegistrarCommit (
	@typeCommit INT,
	@idTicket INT,
	@idAutor INT,
	@message VARCHAR(500)
)
AS BEGIN
	INSERT INTO Commits(TipoCommit, IdTicketRelacionado, IdAutor, Mensaje)
	OUTPUT inserted.Id
	VALUES (@typeCommit, @idTicket, @idAutor, @message);
END
GO

CREATE PROCEDURE SP_GetTicketsCountByUser
    @idUsuario INT
AS
BEGIN
    SELECT
        ET.NombreEstado AS Estado,
        COUNT(*) AS Cantidad
    FROM Tickets AS T
    INNER JOIN EstadosTicket AS ET ON T.idEstado = ET.Id
    WHERE T.IdUsuarioCreador = @idUsuario
    GROUP BY ET.NombreEstado;
END
GO

