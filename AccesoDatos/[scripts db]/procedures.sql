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

-- Creacion de usuarios testing:
EXEC SP_CrearUsuario 'lcorrea', 'Leandro Correa', 'leandrocg524@gmail.com', '123456789', 0;

SELECT * FROM Usuarios;
 
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

SELECT * FROM Tickets;