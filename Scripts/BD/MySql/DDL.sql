-- Crear base de datos
CREATE DATABASE IF NOT EXISTS EventoBd;
USE EventoBd;

-- Tabla Local
CREATE TABLE Local (
    idLocal INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(255) NOT NULL,
    Direccion VARCHAR(255) NOT NULL,
    Capacidad INT NOT NULL,
    Telefono VARCHAR(50) NOT NULL
);

-- Tabla Sector
CREATE TABLE Sector (
    idSector INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(255) NOT NULL,
    Descripcion TEXT,
    Capacidad INT NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    idLocal INT NOT NULL,
    FOREIGN KEY (idLocal) REFERENCES Local(idLocal) ON DELETE RESTRICT
);

-- Tabla Evento
CREATE TABLE Evento (
    idEvento INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(255) NOT NULL,
    Fecha DATETIME NOT NULL,
    Lugar VARCHAR(255) NOT NULL,
    Tipo VARCHAR(100) NOT NULL,
    idLocal INT NOT NULL,
    Activo BOOLEAN DEFAULT TRUE,
    Publicado BOOLEAN DEFAULT FALSE,
    Cancelado BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (idLocal) REFERENCES Local(idLocal) ON DELETE CASCADE
);

-- Tabla Funcion
CREATE TABLE Funcion (
    IdFuncion INT AUTO_INCREMENT PRIMARY KEY,
    Descripcion VARCHAR(255) DEFAULT '',
    FechaHora DATETIME NOT NULL,
    Estado VARCHAR(50) DEFAULT 'Pendiente',
    IdEvento INT NOT NULL,
    IdLocal INT NOT NULL,
    FOREIGN KEY (IdEvento) REFERENCES Evento(idEvento) ON DELETE CASCADE,
    FOREIGN KEY (IdLocal) REFERENCES Local(idLocal) ON DELETE CASCADE
);

-- Tabla Cliente
CREATE TABLE Cliente (
    idCliente INT AUTO_INCREMENT PRIMARY KEY,
    DNI INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Telefono VARCHAR(50) NOT NULL
);

-- Tabla Orden
CREATE TABLE Orden (
    idOrden INT AUTO_INCREMENT PRIMARY KEY,
    Fecha DATETIME NOT NULL,
    Estado VARCHAR(50) NOT NULL,
    idCliente INT NOT NULL,
    NumeroOrden INT NOT NULL,
    Total DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (idCliente) REFERENCES Cliente(idCliente) ON DELETE CASCADE
);

-- Tabla DetalleOrden
CREATE TABLE DetalleOrden (
    IdDetalleOrden INT AUTO_INCREMENT PRIMARY KEY,
    IdOrden INT NOT NULL,
    IdEvento INT NOT NULL,
    IdFuncion INT NOT NULL,
    IdTarifa INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (IdOrden) REFERENCES Orden(idOrden) ON DELETE CASCADE
);

-- Tabla Entrada
CREATE TABLE Entrada (
    IdEntrada INT AUTO_INCREMENT PRIMARY KEY,
    Precio DECIMAL(10,2) NOT NULL,
    QR VARCHAR(255) DEFAULT '',
    Usada BOOLEAN DEFAULT FALSE,
    Anulada BOOLEAN DEFAULT FALSE,
    FechaUso DATETIME NULL,
    Numero VARCHAR(50) NOT NULL,
    IdDetalleOrden INT NOT NULL,
    IdCliente INT NOT NULL,
    IdFuncion INT NOT NULL,
    IdSector INT NOT NULL,
    FOREIGN KEY (IdDetalleOrden) REFERENCES DetalleOrden(IdDetalleOrden) ON DELETE CASCADE,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(idCliente) ON DELETE CASCADE,
    FOREIGN KEY (IdFuncion) REFERENCES Funcion(IdFuncion) ON DELETE CASCADE,
    FOREIGN KEY (IdSector) REFERENCES Sector(idSector) ON DELETE CASCADE
);

-- Tabla Tarifa
CREATE TABLE Tarifa (
    idTarifa INT AUTO_INCREMENT PRIMARY KEY,
    Precio DECIMAL(10,2) NOT NULL,
    Descripcion VARCHAR(255) DEFAULT '',
    Stock INT NOT NULL,
    Activa BOOLEAN DEFAULT TRUE,
    idSector INT NOT NULL,
    IdFuncion INT NOT NULL,
    IdEvento INT NOT NULL,
    FOREIGN KEY (idSector) REFERENCES Sector(idSector) ON DELETE CASCADE,
    FOREIGN KEY (IdFuncion) REFERENCES Funcion(IdFuncion) ON DELETE CASCADE,
    FOREIGN KEY (IdEvento) REFERENCES Evento(idEvento) ON DELETE CASCADE
);

-- Tabla QR
CREATE TABLE QR (
    idQR INT AUTO_INCREMENT PRIMARY KEY,
    IdEntrada INT NOT NULL,
    Codigo VARCHAR(255) NOT NULL,
    FechaCreacion DATETIME NOT NULL,
    FOREIGN KEY (IdEntrada) REFERENCES Entrada(IdEntrada) ON DELETE CASCADE
);

-- Tabla Rol
CREATE TABLE Rol (
    IdRol INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

-- Tabla Usuario
CREATE TABLE Usuario (
    IdUsuario INT AUTO_INCREMENT PRIMARY KEY,
    NombreUsuario VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Contrasena VARCHAR(255) NOT NULL,
    Activo BOOLEAN DEFAULT TRUE
);

-- Tabla UsuarioRol
CREATE TABLE UsuarioRol (
    IdUsuario INT NOT NULL,
    IdRol INT NOT NULL,
    PRIMARY KEY (IdUsuario, IdRol),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario) ON DELETE CASCADE,
    FOREIGN KEY (IdRol) REFERENCES Rol(IdRol) ON DELETE CASCADE
);
