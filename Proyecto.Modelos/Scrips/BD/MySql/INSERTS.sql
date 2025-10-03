-- Active: 1700068523370@@127.0.0.1@3306@EventosBD
USE EventosBD;

-- Insertar locales
INSERT INTO Local (nombre, direccion, capacidad, telefono) VALUES
('Cancha Monumental', 'Av. Figueroa Alcorta 7597, CABA', 83000, '011-12345678'),
('Estadio Libertadores de América', 'Av. Pres. Figueroa Alcorta 7597, CABA', 66000, '011-87654321'),
('Estadio Ciudad de La Plata', 'Av. 25 y 32, La Plata, Buenos Aires', 53000, '0221-5555555'),
('Luna Park', 'Av. Eduardo Madero 470, C1106 CABA', 8400, '011-44445555'),
('Estadio Único Diego Armando Maradona', 'Av. Pres. Juan Domingo Perón 3500, La Plata, Buenos Aires', 53000, '0221-66667777');

-- Insertar sectores para los locales
INSERT INTO Sector (Nombre, Descripcion, Capacidad, Precio, idSector) VALUES
('Platea Baja', 'Platea cerca del escenario', 5000, 2000, 1),
('Platea Alta', 'Platea elevada', 8000, 1500, 1),
('VIP', 'Zona VIP', 500, 5000, 2),
('General', 'Asientos generales', 30000, 1000, 2),
('Tribuna Norte', 'Tribuna norte del estadio', 10000, 1200, 3),
('Tribuna Sur', 'Tribuna sur del estadio', 10000, 1200, 3),
('Ring', 'Alrededor del ring', 1000, 2500, 4),
('Palco', 'Palco privado', 200, 6000, 5);

-- Insertar eventos
INSERT INTO Evento (Nombre, Fecha, Tipo, idLocal, idEvento, Lugar, Local) VALUES
('Concierto de Rock', '2025-12-15', 'Concierto', 1, 1, '',),
('Partido de Fútbol', '2025-11-01', 'Deportivo', 2, 2, '',),
('Concierto de Pop', '2025-10-20', 'Concierto', 3, 3, '',),
('Evento de Box', '2025-09-30', 'Deportivo', 4, 4, '',),
('Festival de Música', '2025-12-25', 'Concierto', 5, 5, '',);

-- Insertar clientes
INSERT INTO Clientes (DNI, Nombre, Apellido, Email, contrasenia) VALUES
('00000001', 'Fulano', 'Gutierrez', 'fulanogutierrez@gmail.com', 'fulano123'),
('00000002', 'Cesar', 'Torres', 'cesar@gmail.com', 'cesar123'),
('00000003', 'Alpaka', 'Delvalle', 'alpaka@gmail.com', 'alpaka123'),
('00000004', 'Hernan', 'Lopez', 'hernan@gmail.com', 'hernan123'),
('00000005', 'Tiago', 'Videira', 'tiago@gmail.com', 'tiago123');

INSERT INTO Entrada (Precio, QR, Usada, idTarifa, idFuncion, Estado, idEntrada) VALUES 
(15000.00, '', 0, 1, 1, 'Activa', 1),
(10000.00, '', 0, 0, 2, 'Activa', 2)
(10500.00, '', 0, 3, 6, 'Activa', 3),
(14000.00, '', 0, 4, 1, 'Activa', 4)
(15000.00, '', 0, 2, 5, 'Activa', 5),
(13000.00, '', 0, 6, 1, 'Activa', 6)

INSERT INTO Funcion (Descripcion, FechaHora, funcionId) VALUES 
('Concierto Coldplay - Día 1', '2025-11-20 20:00:00', 1);
('Concierto Coldplay - Día 2', '2025-11-21 21:00:00', 2);
('Partido Argentina vs Brasil', '2025-11-30 18:00:00', 3);
('Concierto Taylor Swift', '2025-12-01 20:30:00', 4);
('Evento de Boxeo - Campeonato Mundial', '2025-12-03 21:00:00', 5);
('Obra de teatro - Romeo y Julieta', '2025-12-05 19:30:00', 6);
('Cine - Estreno Avengers 6', '2025-12-10 22:00:00', 7);
('Partido Boca vs River', '2025-12-15 17:00:00', 8);
('Concierto de Música Clásica', '2025-12-20 20:00:00', 9);
('Festival de Jazz', '2025-12-25 21:00:00', 10);

INSERT INTO Orden (Fecha, Estado, ClienteId) VALUES 
('2025-10-01 15:30:00', 'Pendiente', 1);
('2025-10-02 18:45:00', 'Pagada', 2);
('2025-10-03 20:15:00', 'Anulada', 3);
('2025-10-05 11:00:00', 'Pendiente', 1);
('2025-10-06 19:00:00', 'Pagada', 4);


INSERT INTO Tarifa (Precio, Descripcion, SectorId, FuncionId) VALUES 
(25000, 'Platea Baja - General', 1, 1);
(18000, 'Platea Alta - General', 2, 1);
(50000, 'Palco VIP', 3, 1);
(15000, 'Campo General', 4, 2);
(12000, 'Galería - Promoción', 5, 3);