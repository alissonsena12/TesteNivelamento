CREATE DATABASE Financeira

USE Financeira

CREATE TABLE Cliente (
    CPF     VARCHAR(11) PRIMARY KEY,
    Nome    VARCHAR(100),
    UF      VARCHAR(2),
    Celular VARCHAR(20)
);

CREATE TABLE Financiamento (
    IdFinanciamento         INT PRIMARY KEY IDENTITY(1,1),
    CPF                     VARCHAR(11),
    TipoFinanciamento       VARCHAR(50),
    ValorTotal              DECIMAL(18,2),
    DataUltimoVencimento    DATE,
    FOREIGN KEY (CPF)       REFERENCES Cliente (CPF)
);

CREATE TABLE Parcela (
    IdFinanciamento     INT,
    NumeroParcela       INT,
    ValorParcela        DECIMAL(18,2),
    DataVencimento      DATE,
    DataPagamento       DATE NULL,
    PRIMARY KEY (IdFinanciamento, NumeroParcela),
    FOREIGN KEY (IdFinanciamento) REFERENCES Financiamento (IdFinanciamento)
);



