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
INSERT INTO Sector (nombre, descripcion, capacidad, precio, idLocal) VALUES
('Platea Baja', 'Platea cerca del escenario', 5000, 2000, 1),
('Platea Alta', 'Platea elevada', 8000, 1500, 1),
('VIP', 'Zona VIP', 500, 5000, 2),
('General', 'Asientos generales', 30000, 1000, 2),
('Tribuna Norte', 'Tribuna norte del estadio', 10000, 1200, 3),
('Tribuna Sur', 'Tribuna sur del estadio', 10000, 1200, 3),
('Ring', 'Alrededor del ring', 1000, 2500, 4),
('Palco', 'Palco privado', 200, 6000, 5);

-- Insertar eventos
INSERT INTO Evento (nombre, fecha, tipo, idLocal) VALUES
('Concierto de Rock', '2025-12-15', 'Concierto', 1),
('Partido de Fútbol', '2025-11-01', 'Deportivo', 2),
('Concierto de Pop', '2025-10-20', 'Concierto', 3),
('Evento de Box', '2025-09-30', 'Deportivo', 4),
('Festival de Música', '2025-12-25', 'Concierto', 5);

-- Insertar Clientes
INSERT INTO Clientes (DNI, Nombre, Apellido, Email, contrasenia) VALUES
('00000001', 'Fulano', 'Gutierrez', 'fulanogutierrez@gmail.com', 'fulano123'),
('00000002', 'Cesar', 'Torres', 'cesar@gmail.com', 'cesar123'),
('00000003', 'Alpaka', 'Delvalle', 'alpaka@gmail.com', 'alpaka123'),
('00000004', 'Hernan', 'Lopez', 'hernan@gmail.com', 'hernan123'),
('00000005', 'Tiago', 'Videira', 'tiago@gmail.com', 'tiago123');



