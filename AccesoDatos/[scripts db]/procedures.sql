-- USE HelpFlow;

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
	@title VARCHAR(75),
	@message VARCHAR(500)
)
AS BEGIN
	INSERT INTO Tickets(IdUsuarioCreador, Titulo, Descripcion)
	OUTPUT inserted.Id
	VALUES (@owner, @title, @message);
END
go

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