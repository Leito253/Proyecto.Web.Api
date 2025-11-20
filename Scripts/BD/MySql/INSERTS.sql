USE Boleteria;
-- Locales
INSERT INTO Local (Nombre, Direccion, Capacidad, Telefono) VALUES
('Estadio Central', 'Av. Siempre Viva 123', 50000, '123456789'),
('Teatro Principal', 'Calle Falsa 456', 2000, '987654321');

-- Sectores
INSERT INTO Sector (Nombre, Descripcion, Capacidad, Precio, idLocal) VALUES
('Sector A', 'Lateral Izquierdo', 10000, 5000, 1),
('Sector B', 'Lateral Derecho', 10000, 5500, 1),
('Platea', 'Platea Baja', 500, 10000, 2);

-- Eventos
INSERT INTO Evento (Nombre, Fecha, Lugar, Tipo, idLocal, Activo, Publicado, Cancelado) VALUES
('Concierto Rock', '2025-12-10 20:00:00', 'Estadio Central', 'Concierto', 1, TRUE, FALSE, FALSE),
('Obra de Teatro', '2025-12-15 19:00:00', 'Teatro Principal', 'Teatro', 2, TRUE, FALSE, FALSE);

-- Funciones
INSERT INTO Funcion (Descripcion, FechaHora, Estado, IdEvento, IdLocal) VALUES
('Función única', '2025-12-10 20:00:00', 'Pendiente', 1, 1),
('Función única', '2025-12-15 19:00:00', 'Pendiente', 2, 2);

-- Clientes
INSERT INTO Cliente (DNI, Nombre, Apellido, Email, Telefono) VALUES
(12345678, 'Juan', 'Pérez', 'juan.perez@email.com', '111111111'),
(87654321, 'María', 'Gómez', 'maria.gomez@email.com', '222222222');

-- Tarifa
INSERT INTO Tarifa (Precio, Descripcion, Stock, Activa, idSector, IdFuncion, IdEvento) VALUES
(5000, 'Entrada General', 1000, TRUE, 1, 1, 1),
(5500, 'Entrada VIP', 500, TRUE, 2, 1, 1);
