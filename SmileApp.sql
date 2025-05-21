-- Roles y Seguridad
CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(50) NOT NULL
);

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Correo NVARCHAR(100) UNIQUE,
    ContraseñaHash NVARCHAR(255),
    RolId INT,
    Estado BIT DEFAULT 1,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RolId) REFERENCES Roles(Id)
);

CREATE TABLE Permisos (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Descripcion NVARCHAR(255)
);

CREATE TABLE RolPermisos (
    Id INT PRIMARY KEY IDENTITY,
    RolId INT,
    PermisoId INT,
    FOREIGN KEY (RolId) REFERENCES Roles(Id),
    FOREIGN KEY (PermisoId) REFERENCES Permisos(Id)
);

CREATE TABLE Auditorias (
    Id INT PRIMARY KEY IDENTITY,
    UsuarioId INT,
    Accion NVARCHAR(100),
    Modulo NVARCHAR(50),
    Descripcion NVARCHAR(255),
    FechaHora DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);

-- Gestión de Pacientes
CREATE TABLE Pacientes (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Apellido NVARCHAR(100),
    FechaNacimiento DATE,
    Sexo NVARCHAR(10),
    Telefono NVARCHAR(20),
    Direccion NVARCHAR(255),
    Correo NVARCHAR(100),
    FechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE HistorialClinico (
    Id INT PRIMARY KEY IDENTITY,
    PacienteId INT,
    Fecha DATETIME,
    Diagnostico NVARCHAR(255),
    Tratamiento NVARCHAR(255),
    Notas TEXT,
    MedicoId INT,
    FOREIGN KEY (PacienteId) REFERENCES Pacientes(Id),
    FOREIGN KEY (MedicoId) REFERENCES Usuarios(Id)
);

-- Citas Médicas
CREATE TABLE Citas (
    Id INT PRIMARY KEY IDENTITY,
    PacienteId INT,
    MedicoId INT,
    FechaHora DATETIME,
    Estado NVARCHAR(50),
    Notas NVARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (PacienteId) REFERENCES Pacientes(Id),
    FOREIGN KEY (MedicoId) REFERENCES Usuarios(Id)
);

CREATE TABLE Cancelaciones (
    Id INT PRIMARY KEY IDENTITY,
    CitaId INT,
    UsuarioId INT,
    Motivo NVARCHAR(255),
    FechaCancelacion DATETIME DEFAULT GETDATE(),
    Penalizacion DECIMAL(10,2),
    FOREIGN KEY (CitaId) REFERENCES Citas(Id),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);

-- Inventario
CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Tipo NVARCHAR(50),
    Descripcion NVARCHAR(255),
    StockActual INT,
    StockMinimo INT,
    PrecioCompra DECIMAL(10,2),
    PrecioVenta DECIMAL(10,2)
);

CREATE TABLE EntradasInventario (
    Id INT PRIMARY KEY IDENTITY,
    ProductoId INT,
    Cantidad INT,
    Fecha DATETIME,
    Proveedor NVARCHAR(100),
    CostoTotal DECIMAL(10,2),
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);

CREATE TABLE SalidasInventario (
    Id INT PRIMARY KEY IDENTITY,
    ProductoId INT,
    Cantidad INT,
    Fecha DATETIME,
    Motivo NVARCHAR(100),
    RelacionPacienteId INT,
    MedicoId INT,
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id),
    FOREIGN KEY (RelacionPacienteId) REFERENCES Pacientes(Id),
    FOREIGN KEY (MedicoId) REFERENCES Usuarios(Id)
);

-- Finanzas
CREATE TABLE Pagos (
    Id INT PRIMARY KEY IDENTITY,
    PacienteId INT,
    Fecha DATETIME,
    Monto DECIMAL(10,2),
    TipoPago NVARCHAR(50),
    Detalle NVARCHAR(255),
    RelacionCitaId INT,
    FOREIGN KEY (PacienteId) REFERENCES Pacientes(Id),
    FOREIGN KEY (RelacionCitaId) REFERENCES Citas(Id)
);

CREATE TABLE Gastos (
    Id INT PRIMARY KEY IDENTITY,
    Fecha DATETIME,
    Monto DECIMAL(10,2),
    Concepto NVARCHAR(255),
    ResponsableId INT,
    FOREIGN KEY (ResponsableId) REFERENCES Usuarios(Id)
);

CREATE TABLE Facturas (
    Id INT PRIMARY KEY IDENTITY,
    PacienteId INT,
    Fecha DATETIME,
    Total DECIMAL(10,2),
    Detalle NVARCHAR(255),
    FOREIGN KEY (PacienteId) REFERENCES Pacientes(Id)
);