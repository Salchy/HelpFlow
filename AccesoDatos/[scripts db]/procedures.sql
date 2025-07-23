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
EXEC SP_CrearUsuario 'lcorrea', 'Leandro Correa', 'lcorrea@altaplastica.com.ar', '123456789', 0;
EXEC SP_CrearUsuario 'fbileni', 'Francisco Bileni', 'fbileni@altaplastica.com.ar', '123456789', 0;
EXEC SP_CrearUsuario 'jlopez', 'Julieta Lopez', 'jlopez@altaplastica.com.ar', 'abc123456', 1;
EXEC SP_CrearUsuario 'mgarcia', 'Marcos Garcia', 'mgarcia@altaplastica.com.ar', 'marcos2025', 1;
EXEC SP_CrearUsuario 'cortega', 'Camila Ortega', 'cortega@altaplastica.com.ar', 'camila#789', 1;


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

SELECT * FROM EstadosTicket;

EXEC SP_CrearTicket 3, 'Incidencias / Errores - Problemas de hardware', 'El equipo presenta fallas de encendido y sobrecalentamiento. Se requiere revisión del hardware interno y posible reemplazo de componentes defectuosos';
EXEC SP_CrearTicket 5, 'Consultas / Dudas - Asesoramiento técnico', 'Hola, tengo una duda sobre cómo acceder de forma remota al sistema de gestión desde fuera de la oficina. ¿Podrían indicarme si necesito instalar algo adicional o si hay un procedimiento específico que deba seguir? Gracias';
EXEC SP_CrearTicket 4, 'Implementaciones / Proyectos - Actualizaciones de sistemas', 'Buenas, quería consultar si ya está disponible la nueva versión del sistema de facturación que mencionaron la semana pasada. Necesitamos saber si requiere alguna acción de nuestra parte para la actualización o si se hará de forma automática. También nos gustaría saber qué mejoras incluye';
EXEC SP_CrearTicket 2, 'Seguridad Informática - Revisión de logs / auditorías', 'Revisión de logs de VMware';


CREATE PROCEDURE SP_GetTicketInfo (
	@idTicket INT
)
AS BEGIN
	SELECT T.Id, U.Id AS 'IdUsuarioCreador', U.Nombre, U.TipoUsuario, U.Correo, T.Titulo, T.IdEstado, ET.NombreEstado, T.FechaCreacion, T.FechaActualizacion, T.Descripcion FROM Tickets AS T
	INNER JOIN Usuarios AS U ON T.IdUsuarioCreador = U.Id
	INNER JOIN EstadosTicket AS ET ON T.IdEstado = ET.Id
	WHERE T.Id = @idTicket;
END

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

EXEC SP_RegistrarCommit 1, 3, 4, 'Este sería una respuesta del dueño del ticket';
SELECT * FROM Usuarios;

SELECT * FROM Tickets;

SELECT * FROM Commits;

UPDATE Commits SET TipoCommit = 1 Where Id = 3;