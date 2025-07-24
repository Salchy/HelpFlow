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
	@TipoUsuario BIT
)
AS
BEGIN
	INSERT INTO Usuarios(UserName, Nombre, Correo, Clave, TipoUsuario)
	OUTPUT inserted.Id
	VALUES (@userName, @name, @email, @password, @TipoUsuario);
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