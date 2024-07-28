USE FinanclickDB;

SELECT * FROM Producto;
-- Script for the Producto table
CREATE TABLE Producto (
    IdProducto INT PRIMARY KEY IDENTITY,
    NombreProducto VARCHAR(255) NOT NULL,
    Reca DECIMAL(18, 2),
    MetodoCalculo VARCHAR(255),
    SubMetodo VARCHAR(255),
    Monto DECIMAL(18, 2),
    Periodicidad VARCHAR(255),
    NumPagos INT,
    InteresAnual DECIMAL(18, 2),
    IVA DECIMAL(18, 2),
    InteresMoratorio DECIMAL(18, 2),
    PagoAnticipado BIT,
    AplicacionDePagos VARCHAR(255),
    IdEmpresa INT,
    IdDetalleProductos INT,
    Estatus INT,
    FOREIGN KEY (IdEmpresa) REFERENCES Empresa(IdEmpresa),
);

-- Script for the DetalleProductos table
CREATE TABLE DetalleProductos (
    IdDetalleProductos INT PRIMARY KEY IDENTITY,
    IdProducto INT,
    Valor DECIMAL(18, 2),
    TipoValor VARCHAR(255),
    IVA DECIMAL(18, 2),
    IdConcepto INT,
    Estatus INT,
    FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto),
    FOREIGN KEY (IdConcepto) REFERENCES CatConceptos(IdConcepto)
);


-- Script for the CatConceptos table
CREATE TABLE CatConceptos (
    IdConcepto INT PRIMARY KEY IDENTITY,
    NombreConcepto VARCHAR(255),
    Valor DECIMAL(18, 2),
    TipoValor VARCHAR(255),
    IVA DECIMAL(18, 2),
    Estatus INT,
	    IdEmpresa INT,
	FOREIGN KEY (IdEmpresa) REFERENCES Empresa(IdEmpresa),
);
