CREATE DATABASE CreditCardDb

USE [CreditCardDb]
GO

CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100) NOT NULL UNIQUE
);
CREATE TABLE TarjetasDeCredito (
    TarjetaID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT FOREIGN KEY REFERENCES Usuarios(UsuarioID),
    NumeroTarjeta NVARCHAR(16) NOT NULL,
    LimiteCredito DECIMAL(18,2) NOT NULL,
    SaldoActual DECIMAL(18,2) NOT NULL,
    SaldoDisponible AS (LimiteCredito - SaldoActual)
);
CREATE TABLE Transacciones (
    TransaccionID INT PRIMARY KEY IDENTITY(1,1),
    TarjetaID INT FOREIGN KEY REFERENCES TarjetasDeCredito(TarjetaID),
    Fecha DATE NOT NULL,
    Descripcion NVARCHAR(200),
    Monto DECIMAL(18,2) NOT NULL,
    TipoTransaccion nvarchar(50) NOT NULL
);


