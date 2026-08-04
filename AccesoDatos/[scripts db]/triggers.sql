CREATE TRIGGER TR_OnTicketDelete
ON Tickets
INSTEAD OF DELETE
AS
BEGIN
    BEGIN TRY
		-- Borro los commits referenciados al ticket a borrar
        DELETE C FROM Commits AS C INNER JOIN deleted AS D ON C.IdTicketRelacionado = D.Id;

		-- Borro los colaboradores asociados al ticket a borrar
        DELETE CT FROM ColaboradoresTickets AS CT INNER JOIN deleted AS D ON CT.IdTicket = D.Id;

        DELETE T FROM Tickets AS T INNER JOIN deleted AS D ON T.Id = D.Id;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO