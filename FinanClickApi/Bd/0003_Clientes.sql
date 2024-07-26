use FinanclickDB;

select * from CatalogoDocumentos;

CREATE TABLE CatalogoDocumentos (
    IdCatalogoDocumento INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(255) NOT NULL,
    Tipo VARCHAR(255) NOT NULL,
    Estatus INT NOT NULL
);


CREATE TABLE DocumentosCliente (
    IdDocumentoCliente INT PRIMARY KEY IDENTITY,
    DocumentoBase64 TEXT NOT NULL,
    Estatus INT NOT NULL,
    IdDocumento INT,
    IdCliente INT,
    FOREIGN KEY (IdDocumento) REFERENCES CatalogoDocumentos(IdCatalogoDocumento),
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente)
);



select * from CatalogoDocumentos;
--- Tabla inicial del Cliente  
INSERT INTO Cliente (RegimenFiscal, IdEmpresa, Estatus)
VALUES ('MORAL', 1, 1);

INSERT INTO Cliente (RegimenFiscal, IdEmpresa, Estatus)
VALUES ('FISICA', 1, 1);


-- Documentos que tiene que subir el cliente
INSERT INTO CatalogoDocumentos ( Nombre, Tipo, Estatus)
VALUES ( 'ACTA CONSTITUTIVA', 'MORAL', 1);


INSERT INTO DocumentosCliente ( DocumentoBase64, Estatus, IdDocumento, IdCliente)
VALUES ( 'JVBERi0xLjQKJeLjz9MKMyAwIG9iaiA8PC9MaW5lYXJpemVkIDEvTCAxNjMzNi9PIDQvRSAxMjM0L04gMS9UIDE1MTI+PnN0cmVhbQpIYWxsbwoKZW5kc3RyZWFtCmVuZG9iago=', 4, 1, 2);
--- Estatus para los documentosCliente
--- 4.- Pendiente, 3.- Por revisar, 2.- Rechazado, 3.- Aprobado


CREATE TABLE Persona (
    IdPersona INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(255) NOT NULL,
    ApellidoPaterno VARCHAR(255) NOT NULL,
    ApellidoMaterno VARCHAR(255) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    PaisNacimiento VARCHAR(255) NOT NULL,
    EstadoNacimiento VARCHAR(255) NOT NULL,
    Genero VARCHAR(50) NOT NULL,
    RFC VARCHAR(13) NOT NULL,
    CURP VARCHAR(18) NOT NULL,
    ClaveElector VARCHAR(20),
    Nacionalidad VARCHAR(255) NOT NULL,
    EstadoCivil VARCHAR(50),
    RegimenMatrimonial VARCHAR(50),
    NombreConyuge VARCHAR(255),
    Calle VARCHAR(255) NOT NULL,
    NumExterior VARCHAR(50) NOT NULL,
    NumInterior VARCHAR(50),
    Colonia VARCHAR(255) NOT NULL,
    CodigoPostal VARCHAR(10) NOT NULL,
    PaisResidencia VARCHAR(255) NOT NULL,
    EstadoResidencia VARCHAR(255) NOT NULL,
    CiudadResidencia VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Telefono VARCHAR(20)
);

CREATE TABLE PersonaMoral (
    IdPersonaMoral INT PRIMARY KEY IDENTITY,
    RazonSocial VARCHAR(255) NOT NULL,
    RazonComercial VARCHAR(255),
    FechaConstitucion DATE NOT NULL,
    RFC VARCHAR(13) NOT NULL,
    Nacionalidad VARCHAR(255) NOT NULL,
    PaisRegistro VARCHAR(255) NOT NULL,
    EstadoRegistro VARCHAR(255) NOT NULL,
    CiudadRegistro VARCHAR(255) NOT NULL,
    NumEscritura VARCHAR(255) NOT NULL,
    FechaRPPC DATE,
    NombreNotario VARCHAR(255),
    NumNotario VARCHAR(255),
    FolioMercantil VARCHAR(255),
    Calle VARCHAR(255) NOT NULL,
    NumExterior VARCHAR(50) NOT NULL,
    NumInterior VARCHAR(50),
    Colonia VARCHAR(255) NOT NULL,
    CodigoPostal VARCHAR(10) NOT NULL,
    PaisResidencia VARCHAR(255) NOT NULL,
    EstadoResidencia VARCHAR(255) NOT NULL,
    CiudadResidencia VARCHAR(255) NOT NULL
);


CREATE TABLE DatosClienteFisica (
    IdClienteFisica INT PRIMARY KEY IDENTITY,
    IdPersona INT,
    IdCliente INT,
    FOREIGN KEY (IdPersona) REFERENCES Persona(IdPersona),
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente)
);



CREATE TABLE DatosClienteMoral (
    IdClienteMoral INT PRIMARY KEY IDENTITY,
    IdPersonaMoral INT,
    NombreRepLegal VARCHAR(255) NOT NULL,
    RFCRepLegal VARCHAR(13) NOT NULL,
    IdCliente INT,
    FOREIGN KEY (IdPersonaMoral) REFERENCES PersonaMoral(IdPersonaMoral),
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente)
);


INSERT INTO Persona ( Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento, PaisNacimiento, EstadoNacimiento, Genero, RFC, CURP, ClaveElector, Nacionalidad, EstadoCivil, RegimenMatrimonial, NombreConyuge, Calle, NumExterior, NumInterior, Colonia, CodigoPostal, PaisResidencia, EstadoResidencia, CiudadResidencia, Email, Telefono)
VALUES ( 'Juan', 'Perez', 'Lopez', '1980-01-01', 'México', 'Ciudad de México', 'Masculino', 'PEJL800101HDT', 'PEJL800101HDFRZN09', '1234567890123', 'Mexicana', 'Casado', 'Bienes Separados', 'Maria Sanchez', 'Calle Falsa', '123', 'A', 'Centro', '06000', 'México', 'CDMX', 'CDMX', 'juan.perez@example.com', '5551234567');


INSERT INTO PersonaMoral (RazonSocial, RazonComercial, FechaConstitucion, RFC, Nacionalidad, PaisRegistro, EstadoRegistro, CiudadRegistro, NumEscritura, FechaRPPC, NombreNotario, NumNotario, FolioMercantil, Calle, NumExterior, NumInterior, Colonia, CodigoPostal, PaisResidencia, EstadoResidencia, CiudadResidencia)
VALUES ( 'Empresa Ejemplo S.A. de C.V.', 'Comercial Ejemplo', '2010-01-01', 'EJEM100101XXX', 'Mexicana', 'México', 'CDMX', 'CDMX', '123456', '2010-01-10', 'Notario Ejemplo', '789', '123456789', 'Calle Ejemplo', '456', 'B', 'Colonia Ejemplo', '06700', 'México', 'CDMX', 'CDMX');

select * from DatosClienteFisica

INSERT INTO DatosClienteFisica ( IdPersona, IdCliente)
VALUES ( 1, 2);

INSERT INTO DatosClienteMoral ( IdPersonaMoral, NombreRepLegal, RFCRepLegal, IdCliente)
VALUES ( 1, 'Juan Perez', 'PEJL800101HDF', 1);