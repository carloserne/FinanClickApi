

-- Crear tabla Rol
CREATE TABLE Rol (
    IdRol INT PRIMARY KEY,
    NombreRol NVARCHAR(255) NOT NULL,
    Estatus BIT NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL
);

-- Crear tabla Modulo
CREATE TABLE Modulo (
    IdModulo INT PRIMARY KEY,
    NombreModulo NVARCHAR(255) NOT NULL,
    Estatus BIT NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL
);

-- Crear tabla Usuario
CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY,
    IdRol INT,
    Contrasenia NVARCHAR(255) NOT NULL,
    ApellidoPaterno NVARCHAR(255) NOT NULL,
    ApellidoMaterno NVARCHAR(255) NOT NULL,
    IdEmpresa INT,
    Usuario NVARCHAR(255) NOT NULL,
    Imagen NVARCHAR (max) null, 
    FOREIGN KEY (IdRol) REFERENCES Rol(IdRol),
    FOREIGN KEY (IdEmpresa) REFERENCES Empresa(IdEmpresa)
);

-- Crear tabla DetalleModuloUsuario
CREATE TABLE DetalleModuloUsuario (
    IdModulo INT,
    IdUsuario INT,
    PRIMARY KEY (IdModulo, IdUsuario),
    FOREIGN KEY (IdModulo) REFERENCES Modulo(IdModulo),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

-- Insertar registros en la tabla Rol
INSERT INTO Rol (IdRol, NombreRol, Estatus, Descripcion) VALUES
(1, 'Administrador', 1, 'Rol con privilegios administrativos');

-- Insertar registros en la tabla Modulo
INSERT INTO Modulo (IdModulo, NombreModulo, Estatus, Descripcion) VALUES
(1, 'Gestión de Usuarios', 1, 'Módulo para gestionar usuarios del sistema');

-- Insertar registros en la tabla Usuario
INSERT INTO Usuario (IdUsuario, IdRol, Contrasenia, ApellidoPaterno, ApellidoMaterno, IdEmpresa, Usuario) VALUES
(1, 1, 'password123', 'García', 'López', 1, 'davidf');

-- Insertar registros en la tabla DetalleModuloUsuario
INSERT INTO DetalleModuloUsuario (IdModulo, IdUsuario) VALUES
(1, 1);

CREATE TABLE Cliente (
    IdCliente INT PRIMARY KEY,          -- Clave primaria de tipo entero
    RegimenFiscal BIT,                  -- RegimenFiscal de tipo bit (0 o 1)
    idEmpresa INT                       -- idEmpresa de tipo entero, presumiblemente una clave foránea
	FOREIGN KEY (idEmpresa) REFERENCES Empresa(idEmpresa),
);

IF NOT EXISTS (SELECT * FROM sys.columns 
               WHERE Name = N'Imagen' 
               AND Object_ID = Object_ID(N'Usuario'))
BEGIN
    ALTER TABLE Usuario ADD Imagen NVARCHAR(MAX) NULL;
END