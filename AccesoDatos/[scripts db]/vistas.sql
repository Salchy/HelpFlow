USE HelpFlow;

CREATE VIEW VW_GetAllTickets AS
	SELECT
		T.Id AS 'TicketID', T.Titulo, T.IdUsuarioCreador AS 'UsuarioCreadorID', U.Nombre, T.FechaCreacion, T.FechaActualizacion, T.Descripcion, T.IdEstado AS 'EstadoID', ET.NombreEstado
	FROM Tickets AS T
	INNER JOIN Usuarios AS U ON T.IdUsuarioCreador = U.Id
	INNER JOIN EstadosTicket AS ET ON T.IdEstado = ET.Id
GO;

CREATE VIEW VW_GetTicketInfo AS
	SELECT
		T.Id, U.Id AS 'IdUsuarioCreador', U.Nombre, U.TipoUsuario, U.Correo, T.Titulo, T.IdEstado, ET.NombreEstado, T.FechaCreacion, T.FechaActualizacion, T.Descripcion
	FROM Tickets AS T
	INNER JOIN Usuarios AS U ON T.IdUsuarioCreador = U.Id
	INNER JOIN EstadosTicket AS ET ON T.IdEstado = ET.Id

CREATE VIEW VW_GetCommits AS
	SELECT
		C.Id, C.TipoCommit, C.IdTicketRelacionado, C.IdAutor, U.Nombre, C.Fecha, C.Mensaje
	FROM Commits AS C
	INNER JOIN Usuarios AS U ON C.IdAutor = U.Id