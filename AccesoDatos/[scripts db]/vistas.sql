USE HelpFlow;

SELECT * FROM Tickets;

CREATE VIEW VW_GetAllTickets AS
	SELECT
		T.Id, T.Titulo, U.Nombre, T.FechaCreacion, T.FechaActualizacion, T.Descripcion, ET.NombreEstado
	FROM Tickets AS T
	INNER JOIN Usuarios AS U ON T.IdUsuarioCreador = U.Id
	INNER JOIN EstadosTicket AS ET ON T.IdEstado = ET.Id

SELECT * FROM VW_GetAllTickets