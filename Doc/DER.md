erDiagram
    LOCAL {
        int idLocal PK
        string Nombre
        string Direccion
        int Capacidad
        string Telefono
    }

    SECTOR {
        int idSector PK
        string Nombre
        string Descripcion
        int Capacidad
        decimal Precio
        int idLocal FK
    }

    EVENTO {
        int idEvento PK
        string Nombre
        date Fecha
        string Lugar
        string Tipo
        int idLocal FK
    }

    CLIENTE {
        int idCliente PK
        int DNI
        string Nombre
        string Apellido
        string Email
        string Telefono
    }

    ORDEN {
        int idOrden PK
        date Fecha
        decimal Total
        string Estado
        int idCliente FK
    }

    TARIFA {
        int idTarifa PK
        decimal Precio
        string Tipo
        string Descripcion
        int idEvento FK
        int idSector FK
        int idFuncion FK
    }

    DETALLEORDEN {
        int idDetalleOrden PK
        int Cantidad
        decimal Precio
        int idOrden FK
        int idTarifa FK
    }

    ENTRADA {
        int idEntrada PK
        decimal Precio
        string QR
        int idDetalleOrden FK
        int idSector FK
        int idTarifa FK
        int idFuncion FK
        bool Anulada
        bool Usada
        string Estado
        int Numero
    }

    FUNCION {
        int idFuncion PK
        string Descripcion
        datetime FechaHora
    }

    USUARIO {
        int idUsuario PK
        string usuario
        string Email
        string Contrasena
        bool Activo
    }

    ROL {
        int idRol PK
        string Nombre
    }

    USUARIOROL {
        int idUsuario FK
        int idRol FK
        PK (idUsuario, idRol)
    }

    %% Relaciones
    LOCAL ||--o{ SECTOR : "contiene"
    LOCAL ||--o{ EVENTO : "organiza"

    CLIENTE ||--o{ ORDEN : "realiza"

    ORDEN ||--o{ DETALLEORDEN : "incluye"
    DETALLEORDEN }o--|| TARIFA : "usa"
    DETALLEORDEN ||--o{ ENTRADA : "genera"

    TARIFA ||--o{ DETALLEORDEN : "detalle"
    TARIFA ||--o{ ENTRADA : "aplica"

    EVENTO ||--o{ TARIFA : "tiene"
    SECTOR ||--o{ TARIFA : "pertenece"
    FUNCION ||--o{ TARIFA : "asocia"

    SECTOR ||--o{ ENTRADA : "corresponde"

    USUARIO ||--o{ USUARIOROL : "posee"
    ROL ||--o{ USUARIOROL : "asigna"
