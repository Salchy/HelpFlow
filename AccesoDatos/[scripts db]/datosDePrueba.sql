-- Datos de prueba:
USE HelpFlow

-- Usuarios:
EXEC SP_CrearUsuario 'lcorrea', 'Leandro Correa', 'lcorrea@altaplastica.com.ar', '3323667C2888FFA309A45D535E80FB42DC5276C2212A94075796934084D6012B', 0;
EXEC SP_CrearUsuario 'jlopez', 'Julieta Lopez', 'jlopez@altaplastica.com.ar', 'abc123456', 1;
EXEC SP_CrearUsuario 'mgarcia', 'Marcos Garcia', 'mgarcia@altaplastica.com.ar', 'marcos2025', 1;
EXEC SP_CrearUsuario 'cortega', 'Camila Ortega', 'cortega@altaplastica.com.ar', 'camila#789', 1;
EXEC SP_CrearUsuario 'rduarte', 'Rodrigo Duarte', 'rduarte@altaplastica.com.ar', '124536', 1;
EXEC SP_CrearUsuario 'molmedo', 'Martin Olmedo', 'molmedo@altaplastica.com.ar', '2345623', 1;

-- Tickets
EXEC SP_CrearTicket 3, 1, 'La computadora no enciende y emite un pitido al intentar arrancar.';
EXEC SP_CrearTicket 6, 5, 'No se puede imprimir desde ninguna aplicaci�n, parece haber un error en el controlador de la impresora.';
EXEC SP_CrearTicket 4, 8, 'El usuario solicita acceso a la base de datos de clientes para su trabajo diario.';
EXEC SP_CrearTicket 2, 14, 'Se requiere el desarrollo de una herramienta interna para generar reportes autom�ticos.';
EXEC SP_CrearTicket 5, 17, 'Solicita la actualizaci�n del software antivirus a la �ltima versi�n disponible.';
EXEC SP_CrearTicket 6, 21, 'No puede cambiar la contrase�a porque el sistema arroja un error inesperado.';
EXEC SP_CrearTicket 2, 25, 'Sugiere implementar una soluci�n para optimizar los tiempos de carga del sistema.';
EXEC SP_CrearTicket 3, 4, 'Errores intermitentes en el sistema de gesti�n contable.';
EXEC SP_CrearTicket 4, 10, 'Solicita el reemplazo del monitor por uno de mayor tama�o.';
EXEC SP_CrearTicket 5, 2, 'La aplicaci�n se congela al abrir archivos grandes, se cree que es un problema del software.';
EXEC SP_CrearTicket 6, 6, 'Consulta sobre c�mo configurar macros en Excel para automatizar tareas.';
EXEC SP_CrearTicket 3, 15, 'Se necesita actualizar los servidores con los �ltimos parches de seguridad.';
EXEC SP_CrearTicket 2, 23, 'Not� accesos sospechosos a su cuenta fuera del horario laboral.';
EXEC SP_CrearTicket 4, 13, 'Requiere comprar una nueva licencia de Adobe Acrobat Pro para edici�n de PDFs.';
EXEC SP_CrearTicket 5, 19, 'Solicita limpieza preventiva de los equipos del laboratorio t�cnico.';