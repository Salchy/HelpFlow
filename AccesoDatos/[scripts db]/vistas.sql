USE HelpFlow;
GO

CREATE VIEW VW_GetAllTickets AS
	SELECT
		T.Id AS 'TicketID',
		C.Nombre + ' - ' + SC.Nombre AS 'Titulo',
		T.IdUsuarioCreador AS 'IdUsuarioCreador',
		U.Nombre,
		T.FechaCreacion,
		T.FechaActualizacion,
		T.Descripcion,
		T.IdEstado AS 'EstadoID',
		ET.NombreEstado
	FROM Tickets AS T
	INNER JOIN SubCategorias AS SC ON T.IdSubCategoria = SC.Id
	INNER JOIN Categorias AS C ON SC.IdCategoriaPadre = C.Id
	INNER JOIN Usuarios AS U ON T.IdUsuarioCreador = U.Id
	INNER JOIN EstadosTicket AS ET ON T.IdEstado = ET.Id
GO

CREATE VIEW VW_GetAllTicketsWithColaborators AS
	SELECT
		T.Id AS 'TicketID',
		C.Nombre + ' - ' + SC.Nombre AS 'Titulo',
		T.IdUsuarioCreador AS 'IdUsuarioCreador',
		U.Nombre,
		T.FechaCreacion,
		T.FechaActualizacion,
		T.Descripcion,
		T.IdEstado AS 'EstadoID',
		ET.NombreEstado,

		ISNULL((
			SELECT STRING_AGG(U2.Nombre, ', ')
			FROM ColaboradoresTickets CT
			INNER JOIN Usuarios U2 ON CT.IdUsuario = U2.Id
			WHERE CT.IdTicket = T.Id
		), 'Sin colaboradores') AS 'Colaboradores'

	FROM Tickets AS T
	INNER JOIN SubCategorias AS SC ON T.IdSubCategoria = SC.Id
	INNER JOIN Categorias AS C ON SC.IdCategoriaPadre = C.Id
	INNER JOIN Usuarios AS U ON T.IdUsuarioCreador = U.Id
	INNER JOIN EstadosTicket AS ET ON T.IdEstado = ET.Id
GO

CREATE VIEW VW_GetCollaboratorsPerTicket AS
	SELECT
		CT.IdTicket,
		CT.IdUsuario,
		U.Nombre,
		U.Correo
	FROM ColaboradoresTickets AS CT
	INNER JOIN Usuarios AS U ON CT.IdUsuario = U.Id
GO

SELECT * FROM VW_GetCollaboratorsPerTicket Where IdTicket = 1
GO

CREATE VIEW VW_GetTicketInfo AS
	SELECT
		T.Id,
		U.Id AS 'IdUsuarioCreador',
		U.Nombre,
		U.TipoUsuario,
		U.Correo,
		C.Nombre + ' - ' + SC.Nombre AS 'Titulo',
		T.IdEstado,
		ET.NombreEstado,
		T.FechaCreacion,
		T.FechaActualizacion,
		T.Descripcion
	FROM Tickets AS T
	INNER JOIN SubCategorias AS SC ON T.IdSubCategoria = SC.Id
	INNER JOIN Categorias AS C ON SC.IdCategoriaPadre = C.Id
	INNER JOIN Usuarios AS U ON T.IdUsuarioCreador = U.Id
	INNER JOIN EstadosTicket AS ET ON T.IdEstado = ET.Id
GO

CREATE VIEW VW_GetCommits AS
	SELECT
		C.Id,
		C.TipoCommit,
		C.IdTicketRelacionado,
		C.IdAutor,
		U.Nombre,
		C.Fecha,
		C.Mensaje
	FROM Commits AS C
	INNER JOIN Usuarios AS U ON C.IdAutor = U.Id
GO

CREATE VIEW VW_GetTicketsCount AS
	SELECT
		ET.NombreEstado AS 'Estado',
		COUNT (ET.NombreEstado) AS 'Cantidad'
	FROM Tickets AS T
	INNER JOIN EstadosTicket AS ET ON T.idEstado = ET.Id
	GROUP BY ET.NombreEstado
GO