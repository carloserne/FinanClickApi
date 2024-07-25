use FinanclickDB;

-- Crear tabla Rol
CREATE TABLE Rol (
    IdRol INT PRIMARY KEY IDENTITY,
    NombreRol NVARCHAR(255) NOT NULL,
    Estatus BIT NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL
);

-- Crear tabla Modulo
CREATE TABLE Modulo (
    IdModulo INT PRIMARY KEY IDENTITY,
    NombreModulo NVARCHAR(255) NOT NULL,
    Estatus BIT NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL
);

-- Crear tabla Usuario
CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY IDENTITY,
    IdRol INT,
    Contrasenia NVARCHAR(255) NOT NULL,
	Nombre NVARCHAR(35) NOT NULL,
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


CREATE TABLE Cliente (
    IdCliente INT PRIMARY KEY identity,          -- Clave primaria de tipo entero
    RegimenFiscal varchar(10),                  -- RegimenFiscal de tipo bit (0 o 1)
    idEmpresa INT,
    Estatus int not null, -- idEmpresa de tipo entero, presumiblemente una clave foránea
	FOREIGN KEY (idEmpresa) REFERENCES Empresa(idEmpresa),
);



-- Insertar registros en la tabla Rol
INSERT INTO Rol ( NombreRol, Estatus, Descripcion) VALUES
( 'Administrador', 1, 'Rol con privilegios administrativos');

-- Insertar registros en la tabla Modulo
INSERT INTO Modulo ( NombreModulo, Estatus, Descripcion) VALUES
( 'Gestión de Usuarios', 1, 'Módulo para gestionar usuarios del sistema');

-- Insertar registros en la tabla Usuario
INSERT INTO Usuario ( IdRol, Contrasenia, ApellidoPaterno, ApellidoMaterno, IdEmpresa, Usuario, Nombre, Imagen) VALUES
( 1, 'password123', 'García', 'López', 1, 'davidf', 'David', '');

-- Insertar registros en la tabla DetalleModuloUsuario
INSERT INTO DetalleModuloUsuario (IdModulo, IdUsuario) VALUES
(1, 1);
