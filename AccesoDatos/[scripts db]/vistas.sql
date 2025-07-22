USE HelpFlow;

CREATE VIEW VW_GetAllTickets AS
	SELECT
		T.Id AS 'TicketID', T.Titulo, T.IdUsuarioCreador AS 'UsuarioCreadorID', U.Nombre, T.FechaCreacion, T.FechaActualizacion, T.Descripcion, T.IdEstado AS 'EstadoID', ET.NombreEstado
	FROM Tickets AS T
	INNER JOIN Usuarios AS U ON T.IdUsuarioCreador = U.Id
	INNER JOIN EstadosTicket AS ET ON T.IdEstado = ET.Id

SELECT * FROM VW_GetAllTickets