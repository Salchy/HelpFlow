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

CREATE TABLE Categorias (
	Id TINYINT PRIMARY KEY IDENTITY(1, 1),
	Nombre VARCHAR(75) NOT NULL UNIQUE,
);
GO

CREATE TABLE SubCategorias (
	Id TINYINT PRIMARY KEY IDENTITY(1, 1),
	Nombre VARCHAR(75) NOT NULL UNIQUE,
	IdCategoriaPadre TINYINT NOT NULL,

	FOREIGN KEY (IdCategoriaPadre) REFERENCES Categorias(Id),
);
GO

CREATE TABLE Tickets (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	IdUsuarioCreador INT NOT NULL,
	IdSubCategoria TINYINT NOT NULL,
	IdEstado TINYINT NOT NULL DEFAULT 0,
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	FechaActualizacion DATETIME NOT NULL DEFAULT GETDATE(),
	Descripcion VARCHAR(500),

	FOREIGN KEY (IdUsuarioCreador) REFERENCES Usuarios(Id),
	FOREIGN KEY (IdSubCategoria) REFERENCES SubCategorias(Id),
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
 
-- Categorías
INSERT INTO Categorias (Nombre)
VALUES
	('Incidencias / Errores'),
	('Consultas / Dudas'),
	('Solicitudes / Requerimientos'),
	('Implementaciones / Proyectos'),
	('Mantenimiento Preventivo / Correctivo'),
	('Seguridad Informática'),
	('Mejoras / Sugerencias');

-- SubCategorías
INSERT INTO SubCategorias (Nombre, IdCategoriaPadre)
VALUES
	('Problemas de hardware', 1),
	('Problemas de software', 1),
	('Problemas de red / conectividad', 1),
	('Errores en sistemas internos', 1),
	('Fallos en impresoras / periféricos', 1),
	
	('Consultas sobre uso de software', 2),
	('Asesoramiento técnico', 2),
	('Solicitud de información', 2),
	
	('Instalación de software', 3),
	('Alta/Baja/Modificación de usuarios', 3),
	('Cambio de hardware', 3),
	('Solicitud de acceso a sistemas', 3),
	('Compra de equipos / licencias', 3),
	
	('Nuevos desarrollos internos', 4),
	('Actualizaciones de sistemas', 4),
	('Configuraciones especiales', 4),
	
	('Actualización de software', 5),
	('Backup / Restauración', 5),
	('Limpieza / Mantenimiento físico', 5),
	('Revisiones periódicas', 5),
	
	('Cambio de contraseñas', 6),
	('Incidentes de seguridad', 6),
	('Accesos no autorizados', 6),
	('Revisión de logs / auditorías', 6),
	
	('Propuestas de optimización', 7),
	('Recomendaciones de usuarios', 7),
	('Sugerencias sobre sistemas o infraestructura', 7);

 -- Estados tickets
 INSERT INTO EstadosTicket VALUES
	('Solicitado'),
	('En progreso'),
	('Resuelto'),
	('Cerrado');