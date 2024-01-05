
--------------------------------------------------------------------------------------------------
------------LISTAR TODOS OS CLIENTES DE SP QUE POSSUEM MAIS DE 60% DAS PARCELAS PAGAS-------------
--------------------------------------------------------------------------------------------------
SELECT  c.CPF, 
        c.Nome, 
        c.UF,
        c.Celular,
        COUNT(CASE WHEN p.DataPagamento IS NOT NULL THEN 1 END) AS ParcelasPagas,
        COUNT(*) AS TotalParcelas,
	    ROUND((SUM(CASE WHEN p.DataPagamento IS NOT NULL THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2) AS PercentualParcelasPagas
FROM Cliente c
JOIN Financiamento f ON c.CPF = f.CPF
LEFT JOIN Parcela p ON f.IdFinanciamento = p.IdFinanciamento
WHERE   c.UF = 'SP'
GROUP BY    c.CPF, 
            c.Nome, 
            c.UF,
            c.Celular
HAVING COUNT(CASE WHEN p.DataPagamento IS NOT NULL THEN 1 END) * 1.0 / COUNT(*) >= 0.6
ORDER BY PercentualParcelasPagas
--------------------------------------------------------------------------------------------------
---------LISTAR OS PRIMEIROS QUATRO CLIENTES QUE POSSUEM ALGUAM PARCELA COM MAIS DE CINCO---------
---------DIAS EM ATRASO (DATA VENCIMENTO MAIOR QUE DATA ATUAL E DATA PAGAMENTO NULA      ---------
--------------------------------------------------------------------------------------------------

SELECT DISTINCT 
    C.CPF, 
    C.Nome,
    c.UF,
    c.Celular
FROM Cliente c
INNER JOIN Financiamento f
ON C.CPF = F.CPF
INNER JOIN Parcela p
ON f.IdFinanciamento = p.IdFinanciamento
WHERE P.DataPagamento IS NULL
AND P.DataVencimento < GETDATE() - 5