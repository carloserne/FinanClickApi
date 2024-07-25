create database FinanclickDB;

use FinanclickDB;


CREATE TABLE Empresa (
    IdEmpresa INT PRIMARY KEY identity,
	NombreEmpresa NVARCHAR(255),
    RazonSocial NVARCHAR(255) NOT NULL,
    FechaConstitucion DATE NOT NULL,
    NumeroEscritura NVARCHAR(50) NOT NULL,
    NombreNotario NVARCHAR(255) NOT NULL,
    NumeroNotario NVARCHAR(50) NOT NULL,
    FolioMercantil NVARCHAR(50) NOT NULL,
    Rfc NVARCHAR(50) NOT NULL,
    NombreRepresentanteLegal NVARCHAR(255) NOT NULL,
    NumeroEscrituraRepLeg NVARCHAR(50) NOT NULL,
    FechaInscripcion DATE NOT NULL,
    Calle NVARCHAR(255) NOT NULL,
    Colonia NVARCHAR(255) NOT NULL,
    Cp NVARCHAR(10) NOT NULL,
    Telefono NVARCHAR(20) NOT NULL,
    Estado NVARCHAR(50) NOT NULL,
    Localidad NVARCHAR(255) NOT NULL,
    NumExterior NVARCHAR(10) NOT NULL,
    NumInterior NVARCHAR(10) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Estatus int NOT NULL,
    Logo NVARCHAR(MAX) NULL
);



INSERT INTO Empresa (
NombreEmpresa,
    RazonSocial,
    FechaConstitucion,
    NumeroEscritura,
    NombreNotario,
    NumeroNotario,
    FolioMercantil,
    Rfc,
    NombreRepresentanteLegal,
    NumeroEscrituraRepLeg,
    FechaInscripcion,
    Calle,
    Colonia,
    Cp,
    Teléfono,
    Estado,
    Localidad,
    NumExterior,
    NumInterior,
    Email,
    Estatus,
    Logo
) VALUES (
'FINANCLICK',
    'SOFOM FinanClick S.A. de C.V.', -- RazonSocial
    '2020-01-01', -- FechaConstitucion
    '12345', -- NumeroEscritura
    'Juan Pérez', -- NombreNotario
    '67890', -- NumeroNotario
    '54321', -- FolioMercantil
    'SOF123456789', -- Rfc
    'Ana Gómez', -- NombreRepresentanteLegal
    '98765', -- NumeroEscrituraRepLeg
    '2020-02-01', -- FechaInscripcion
    'Av. Reforma', -- Calle
    'Colonia Centro', -- Colonia
    '06000', -- Cp
    '5551234567', -- Teléfono
    'Ciudad de México', -- Estado
    'CDMX', -- Localidad
    '123', -- NumExterior
    'A', -- NumInterior
    'contacto@sofomejemplo.com', -- Email
    1, -- Estatus
    'prueba' -- Logo (asumiendo que no se tiene un logo para insertar)
);



