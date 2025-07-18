CREATE DATABASE HelpFlow;
GO
USE HelpFlow;
GO

CREATE TABLE EstadosTicket (
	Id TINYINT PRIMARY KEY IDENTITY(0, 1),
	NombreEstado VARCHAR(50) NOT NULL UNIQUE
);

 CREATE TABLE Usuarios (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	UserName VARCHAR(25) NOT NULL UNIQUE,
	Nombre VARCHAR(50) NOT NULL,
	Correo VARCHAR(100) UNIQUE,
	Clave CHAR(64),
	TipoUsuario BIT NOT NULL DEFAULT 1,
	Estado BIT NOT NULL DEFAULT 1,
 );
 GO

-- Crear Indice en UserName

CREATE TABLE Tickets (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	IdUsuarioCreador INT NOT NULL,
	Titulo VARCHAR(75) NOT NULL,
	IdEstado TINYINT NOT NULL DEFAULT 0,
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	FechaActualizacion DATETIME NOT NULL DEFAULT GETDATE(),
	Descripcion VARCHAR(500),

	FOREIGN KEY (IdUsuarioCreador) REFERENCES Usuarios(Id),
	FOREIGN KEY (IdEstado) REFERENCES EstadosTicket(Id),
);
GO

 CREATE TABLE ColaboradoresTickets (
	IdTicket INT NOT NULL,
	IdUsuario INT NOT NULL,

	PRIMARY KEY (IdTicket, IdUsuario),
	FOREIGN KEY (IdTicket) REFERENCES Tickets(Id),
	FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id),
);

 CREATE TABLE Commits (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	TipoCommit BIT NOT NULL,
	IdTicketRelacionado INT NOT NULL,
	IdAutor INT NOT NULL,
	Fecha DATETIME NOT NULL DEFAULT GETDATE(),
	Mensaje VARCHAR(500) NOT NULL,

	FOREIGN KEY (IdTicketRelacionado) REFERENCES Tickets(Id),
	FOREIGN KEY (IdAutor) REFERENCES Usuarios(Id),
 );
 GO

 -- Insert defaults values
 
 -- Estados tickets
 INSERT INTO EstadosTicket VALUES
	('Abierto'),
	('En progreso'),
	('Resuelto'),
	('Cerrado');