USE FINANCEIRA

--------------INSERÇÃO DADOS TESTE CLIENTE------------------

INSERT INTO Cliente (CPF, Nome, UF, Celular) VALUES
('11111111111', 'João Silva', 'SP', '(11) 91234-5678'),
('22222222222', 'Maria Souza', 'RJ', '(21) 98765-4321'),
('33333333333', 'Pedro Oliveira', 'SP', '(31) 99876-5432'),
('44444444444', 'Ana Santos', 'BA', '(71) 87654-3210'),
('55555555555', 'Luiz Pereira', 'RS', '(51) 76543-2109'),
('66666666666', 'Carla Ferreira', 'SP', '(48) 65432-1098'),
('77777777777', 'Paulo Costa', 'PE', '(81) 54321-0987'),
('88888888888', 'Fernanda Almeida', 'SP', '(41) 43210-9876'),
('99999999999', 'Mariana Ribeiro', 'CE', '(85) 32109-8765'),
('12345678900', 'Rafaela Nunes', 'SP', '(62) 21098-7654'),
('23456789012', 'Marcos Lima', 'SP', '(91) 10987-6543'),
('34567890123', 'Gustavo Castro', 'MT', '(65) 98765-4321'),
('45678901234', 'Aline Gomes', 'SP', '(27) 87654-3210'),
('56789012345', 'Bruno Vieira', 'MS', '(67) 76543-2109'),
('67890123456', 'Camila Martins', 'SP', '(83) 65432-1098'),
('78901234567', 'Daniel Rodrigues', 'PI', '(86) 54321-0987'),
('89012345678', 'Eduardo Oliveira', 'SP', '(92) 43210-9876'),
('90123456789', 'Isabela Silva', 'SP', '(69) 32109-8765'),
('01234567890', 'Roberta Sousa', 'AC', '(68) 21098-7654'),
('98765432109', 'Thiago Costa', 'TO', '(63) 10987-6543');

SELECT * FROM Cliente
-----------------------------------------------------------


--------------INSERÇÃO DADOS FINANCIAMENTO-----------------

DECLARE @CPF VARCHAR(11)
DECLARE @Contador INT = 1

DECLARE ClientesCursor CURSOR FOR
SELECT CPF FROM Cliente

OPEN ClientesCursor
FETCH NEXT FROM ClientesCursor INTO @CPF

WHILE @@FETCH_STATUS = 0
BEGIN
    WHILE @Contador <= 2
    BEGIN
        INSERT INTO Financiamento (CPF, TipoFinanciamento, ValorTotal, DataUltimoVencimento)
        VALUES (@CPF, 'Financiamento ' + CAST(@Contador AS VARCHAR), ROUND(RAND() * 100000, 2), DATEADD(DAY, RAND() * 365, GETDATE()))
        
        SET @Contador = @Contador + 1
    END
    
    SET @Contador = 1
    FETCH NEXT FROM ClientesCursor INTO @CPF
END

CLOSE ClientesCursor
DEALLOCATE ClientesCursor

SELECT * FROM Financiamento
-----------------------------------------------------------
-----------------INSERÇÃO DADOS PARCELAS-------------------
DECLARE @IdFinanciamento INT
DECLARE @NumeroParcelas INT
DECLARE @Contador INT = 1

DECLARE cursorFinanciamento CURSOR FOR
    SELECT IdFinanciamento, ROUND(RAND() * (359 - 11) + 11, 0) AS NumeroParcelas
    FROM Financiamento

OPEN cursorFinanciamento
FETCH NEXT FROM cursorFinanciamento INTO @IdFinanciamento, @NumeroParcelas

WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @ParcelaPagamento INT
    SET @ParcelaPagamento = ROUND(RAND() * (@NumeroParcelas - 1) + 1, 0) -- Escolhe aleatoriamente uma parcela para definir a data de pagamento

    WHILE @Contador <= @NumeroParcelas
    BEGIN
        DECLARE @ValorParcela DECIMAL(18, 2)
        DECLARE @DataVencimento DATE
        DECLARE @DataPagamento DATE

        SET @ValorParcela = (SELECT ValorTotal / @NumeroParcelas FROM Financiamento WHERE IdFinanciamento = @IdFinanciamento)
        SET @DataVencimento = DATEADD(MONTH, -@Contador, (SELECT DataUltimoVencimento FROM Financiamento WHERE IdFinanciamento = @IdFinanciamento))

        SET @DataPagamento = CASE
                                WHEN @NumeroParcelas - @Contador <= @ParcelaPagamento THEN DATEADD(DAY, -1, @DataVencimento) -- Um dia antes da data de vencimento
                                ELSE NULL
                            END

        INSERT INTO Parcela (IdFinanciamento, NumeroParcela, ValorParcela, DataVencimento, DataPagamento)
        VALUES (@IdFinanciamento, @NumeroParcelas - @Contador + 1, @ValorParcela, @DataVencimento, @DataPagamento)

        SET @Contador = @Contador + 1
    END

    SET @Contador = 1
    FETCH NEXT FROM cursorFinanciamento INTO @IdFinanciamento, @NumeroParcelas
END

CLOSE cursorFinanciamento
DEALLOCATE cursorFinanciamento

select * from parcela
order by IdFinanciamento, NumeroParcela
-----------------------------------------------------------

