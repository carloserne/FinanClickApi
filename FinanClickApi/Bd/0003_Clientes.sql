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
INSERT INTO CatalogoDocumentos ( Nombre, Tipo, Estatus, idEmpresa)
VALUES ( 'ACTA CONSTITUTIVA', 'MORAL', 1, 1);


INSERT INTO DocumentosCliente ( DocumentoBase64, Estatus, IdDocumento, IdCliente)
VALUES ( 'JVBERi0xLjQKJeLjz9MKMyAwIG9iaiA8PC9MaW5lYXJpemVkIDEvTCAxNjMzNi9PIDQvRSAxMjM0L04gMS9UIDE1MTI+PnN0cmVhbQpIYWxsbwoKZW5kc3RyZWFtCmVuZG9iago=', 4, 1, 2);
--- Estatus para los documentosCliente
--- 4.- Pendiente, 3.- Por revisar, 2.- Rechazado, 1.- Aprobado


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


CREATE TABLE UsuarioCliente (
    IdUsuarioCliente INT PRIMARY KEY identity,
    IdCliente INT,
    Usuario VARCHAR(255),
    Contrasenia VARCHAR(255),
    Estatus INT,
		    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente)

);

 INSERT INTO UsuarioCliente (IdCliente, Usuario, Contrasenia, Estatus) VALUES (1, 'empresaEjemplo', 'empresa123', 1);  

 INSERT INTO UsuarioCliente (IdCliente, Usuario, Contrasenia, Estatus) VALUES (2, 'juanp', 'juanpe123', 1);

 -- !!!!!!!INSERTAR EN ESTE ORDEN EN ESPECÍFICO PARA QUE FUNCIONE!!!!!!!!!!!!

INSERT INTO Rol (nombreRol, estatus, descripcion)
VALUES ('Promotor', 1, 'Descripción del rol Promotor');

INSERT INTO Rol (nombreRol, estatus, descripcion)
VALUES ('Administrador Empresa', 1, 'Representante de la empresa con privilegios administrativos');

INSERT INTO Rol (nombreRol, estatus, descripcion)
VALUES ('Agente de Clientes', 1, 'Encargado de registrar los clientes');

INSERT INTO Rol (nombreRol, estatus, descripcion)
VALUES ('Agente de Credito', 1, 'Encargado de la apertura de créditos');

INSERT INTO Rol (nombreRol, estatus, descripcion)
VALUES ('Gestor de Cobranza', 1, 'Encargado del proceso post-apertura de un crédito');
-- ----------------------------------------------------------------------------------------


INSERT INTO Usuario (idRol, Contrasenia, ApellidoPaterno, ApellidoMaterno, IdEmpresa, Usuario, Nombre, Imagen)
VALUES (1, 'password123', 'Alvarez', 'Mancilla', 1, 'josef', 'Jose', '');

-- SCRIPTS DE LA CREACIÓN DE TABLAS
CREATE TABLE Plan_empresa(
    IdPlan INT PRIMARY KEY identity,
    Precio FLOAT NOT NULL,
    Descripcion VARCHAR(255) NOT NULL,
    Duracion VARCHAR(255) NOT NULL,
    Estatus INT NOT NULL
);

CREATE TABLE VentaProspecto (
    IdVenta INT PRIMARY KEY identity,
	IdPlan INT NOT NULL,
	IdUsuario INT,
	fechaSolicitud DATE NOT NULL,
	nombreCliente VARCHAR(50) NOT NULL,
	nombreEmpresa VARCHAR(60) NOT NULL,
	correo VARCHAR(50) NOT NULL,
	domicilio VARCHAR(100) NOT NULL,
	ciudad VARCHAR(50) NOT NULL,
	estado VARCHAR(50) NOT NULL,
	rfc VARCHAR(13) NOT NULL,
	numeroContacto VARCHAR(10) NOT NULL
	FOREIGN KEY (IdPlan) REFERENCES Plan_empresa(IdPlan),
	FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

-- Inserciones para la tabla Plan_empresa
INSERT INTO Plan_empresa (Precio, Descripcion, Duracion, Estatus)
VALUES (199.99, 'Plan Básico', '1 mes', 1);

INSERT INTO Plan_empresa (Precio, Descripcion, Duracion, Estatus)
VALUES (499.99, 'Plan Profesional', '6 meses', 1);

INSERT INTO Plan_empresa (Precio, Descripcion, Duracion, Estatus)
VALUES (899.99, 'Plan Empresarial', '12 meses', 1);

-- Inserciones para la tabla VentaProspecto
INSERT INTO VentaProspecto (IdPlan, IdUsuario, fechaSolicitud, nombreCliente, nombreEmpresa, correo, domicilio, ciudad, estado, rfc, numeroContacto)
VALUES (1, 4, '2024-10-20', 'Juan Pérez', 'Tecnología Global', 'juan.perez@tecnologiaglobal.com', 'Av. Siempre Viva 123', 'Ciudad de México', 'CDMX', 'JPR1234567890', '4776009669');

INSERT INTO VentaProspecto (IdPlan, IdUsuario, fechaSolicitud, nombreCliente, nombreEmpresa, correo, domicilio, ciudad, estado, rfc, numeroContacto)
VALUES (2, 4, '2024-10-21', 'María Gómez', 'Consultoría IT', 'maria.gomez@consultoriait.com', 'Calle Falsa 456', 'Guadalajara', 'Jalisco', 'MGM1234567890', '4776009669');

INSERT INTO VentaProspecto (IdPlan, IdUsuario, fechaSolicitud, nombreCliente, nombreEmpresa, correo, domicilio, ciudad, estado, rfc, numeroContacto)
VALUES (3, 5, '2024-10-21', 'Carlos Sánchez', 'Servicios Financieros', 'carlos.sanchez@serviciosfinancieros.com', 'Blvd. de los Héroes 789', 'Monterrey', 'Nuevo León', 'CSN1234567890', '4776009669');

--- Actualizar el IdRol y NombreRol de cada registro para corregir el orden y eliminar duplicados
UPDATE Rol
SET NombreRol = 'Promotor', Descripcion = 'Descripción del rol Promotor'
WHERE IdRol = 2;

UPDATE Rol
SET NombreRol = 'Administrador Empresa', Descripcion = 'Representante de la empresa con privilegios administrativos'
WHERE IdRol = 3;

UPDATE Rol
SET NombreRol = 'Agente de Clientes', Descripcion = 'Encargado de registrar los clientes'
WHERE IdRol = 4;

UPDATE Rol
SET NombreRol = 'Agente de Crédito', Descripcion = 'Encargado de la apertura de créditos'
WHERE IdRol = 5;

UPDATE Rol
SET NombreRol = 'Gestor de Cobranza', Descripcion = 'Encargado del proceso post-apertura de un crédito'
WHERE IdRol = 6;

-- Nuevo rol para IdRol = 7
UPDATE Rol
SET NombreRol = 'Encargado Ventas', Descripcion = 'Encargado de llamar a los clientes interesados'
WHERE IdRol = 7;

INSERT INTO Usuario (IdRol, Contrasenia, ApellidoPaterno, ApellidoMaterno, IdEmpresa, Usuario, Nombre, Imagen)
VALUES (7, 'contraseña123', 'Perez', 'Garcia', 1, 'usuario.prueba', 'Juan', NULL);

INSERT INTO VentaProspecto (IdPlan, IdUsuario, fechaSolicitud, nombreCliente, nombreEmpresa, correo, domicilio, ciudad, estado, rfc, numeroContacto)
VALUES (3, null, '2024-10-21', 'Carlos Sánchez', 'Servicios Financieros', 'carlos.sanchez@serviciosfinancieros.com', 'Blvd. de los Héroes 789', 'Monterrey', 'Nuevo León', 'CSN1234567890', '4776009669');




-- TABLA DE INGRESOS_EGRESOS Y LAS VISTAS CORRESPONDIENTES PARA EL DASHBOARD
CREATE TABLE Ingresos_Egresos (
    [Id_Ingresos_Egresos] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
	[TipoTransaccion] [int] NULL,
	[Monto] [decimal](18, 2) NOT NULL,
	[Descripcion] [nvarchar](255) NULL,
	[Categoria] [varchar](100) NULL,
	[Estatus] [int] NULL,
);

INSERT INTO Ingresos_Egresos (Fecha, TipoTransaccion, Monto, Descripcion, Categoria, Estatus)
VALUES
    ('2023-11-01', 1, 1500.00, 'Venta de producto X', 'Ventas',1),
    ('2023-11-05', 2, 500.00, 'Pago de alquiler', 'Gastos Operativos',1),
    ('2023-11-10', 1, 800.00, 'Servicio de consultoría', 'Ventas',1),
    ('2023-11-15', 2, 200.00, 'Compra de suministros de oficina', 'Gastos Operativos',1),
    ('2023-11-20', 1, 3000.00, 'Venta de servicio Y', 'Ventas',1);
/*
CREATE VIEW TotalesMensuales AS
SELECT 
    YEAR(Fecha) AS Anio,
    MONTH(Fecha) AS Mes,
    SUM(CASE WHEN TipoTransaccion = 1 AND Estatus = 1 THEN Monto ELSE 0 END) AS TotalIngresos,
    SUM(CASE WHEN TipoTransaccion = 2 AND Estatus = 1 THEN Monto ELSE 0 END) AS TotalEgresos
FROM 
    Ingresos_Egresos
WHERE 
    Estatus = 1
GROUP BY 
    YEAR(Fecha), MONTH(Fecha);

    
  CREATE VIEW AcumuladoAnual AS
SELECT 
    Fecha,
    SUM(CASE WHEN TipoTransaccion = 1 AND Estatus = 1 THEN Monto ELSE 0 END) 
        OVER (ORDER BY Fecha ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS IngresosAcumulados,
    SUM(CASE WHEN TipoTransaccion = 2 AND Estatus = 1 THEN Monto ELSE 0 END) 
        OVER (ORDER BY Fecha ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS EgresosAcumulados
FROM 
    Ingresos_Egresos
WHERE 
    Estatus = 1;

  
 */

