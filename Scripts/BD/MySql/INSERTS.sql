USE EventoBd;
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

INSERT INTO Orden (Fecha, Estado, idCliente, NumeroOrden, Total)
VALUES (NOW(), 'Pendiente', 1, 10001, 0);

INSERT INTO DetalleOrden (IdOrden, IdEvento, IdFuncion, IdTarifa, Cantidad, PrecioUnitario)
VALUES (1, 1, 1, 1, 1, 5000);

INSERT INTO Entrada
(Precio, QR, Usada, Anulada, FechaUso, Numero, IdDetalleOrden, IdCliente, IdFuncion, IdSector)
VALUES
(5000, NULL, FALSE, FALSE, NULL, 'A-001', 1, 1, 1, 1);

INSERT INTO QR (IdEntrada, Codigo, FechaCreacion)
VALUES (1, 'QR-entrada-1-XYZ123ABC', NOW());
use EventoBd;
INSERT INTO Cliente (DNI, Nombre, Apellido, Email, Telefono)
VALUES (34567890, 'Carlos', 'López', 'carlos.lopez@email.com', '333333333');

INSERT INTO Orden (Fecha, Estado, idCliente, NumeroOrden, Total)
VALUES (NOW(), 'Pendiente', 2, 10002, 0);

INSERT INTO DetalleOrden (IdOrden, IdEvento, IdFuncion, IdTarifa, Cantidad, PrecioUnitario)
VALUES (2, 1, 1, 1, 2, 5000);

INSERT INTO Sector (Nombre, Descripcion, Capacidad, Precio, idLocal)
VALUES ('Sector C', 'Central', 8000, 6000, 1);

INSERT INTO Funcion (Descripcion, FechaHora, Estado, IdEvento, IdLocal)
VALUES ('Función especial', '2025-12-20 21:00:00', 'Pendiente', 1, 1);

INSERT INTO Entrada
(Precio, QR, Usada, Anulada, FechaUso, Numero, IdDetalleOrden, IdCliente, IdFuncion, IdSector)
VALUES
(6000, NULL, FALSE, FALSE, NULL, 'B-001', 2, 2, 2, 3);

INSERT INTO Entrada
(Precio, QR, Usada, Anulada, FechaUso, Numero, IdDetalleOrden, IdCliente, IdFuncion, IdSector)
VALUES
(6000, NULL, FALSE, FALSE, NULL, 'B-001', 2, 2, 2, 3);

SET @IdEntrada = LAST_INSERT_ID();

INSERT INTO QR (IdEntrada, Codigo, FechaCreacion)
VALUES (@IdEntrada, 'QR-entrada-2-ABC456DEF', NOW());

INSERT INTO QR (IdEntrada, Codigo, FechaCreacion)
VALUES (1, 'QR-entrada-2-ABC456DEF', NOW());
