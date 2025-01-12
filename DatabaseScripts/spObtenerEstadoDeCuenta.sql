USE [CreditCardDb]
GO

/****** Object:  StoredProcedure [dbo].[ObtenerEstadoDeCuenta]    Script Date: 11/1/2025 21:41:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Luis Guzman>
-- Create date: <6/1/2025>
-- Description:	<Procedimiento para obtener el estado de cuenta de una tarjeta de crédito>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerEstadoDeCuenta]
@TarjetaID INT
AS
BEGIN
     SELECT 
		t.TarjetaID,
        t.NumeroTarjeta,
        u.Nombre,
        t.LimiteCredito,
        t.SaldoActual,
        t.SaldoDisponible,
        (SELECT SUM(Monto) FROM Transacciones WHERE TarjetaID = @TarjetaID AND MONTH(Fecha) = MONTH(GETDATE())) AS TotalComprasMesActual,
        (SELECT SUM(Monto) FROM Transacciones WHERE TarjetaID = @TarjetaID AND MONTH(Fecha) = MONTH(DATEADD(MONTH, -1, GETDATE()))) AS TotalComprasMesAnterior,
        CAST(t.SaldoActual * 0.25 AS DECIMAL(18,2)) AS InteresBonificable,
        CAST(t.SaldoActual * 0.05 AS DECIMAL(18,2)) AS CuotaMinima,
        t.SaldoActual AS MontoTotal,
        CAST(t.SaldoActual + (t.SaldoActual * 0.25) AS DECIMAL(18,2)) AS PagoContadoConIntereses
    FROM 
        TarjetasDeCredito t
    JOIN 
        Usuarios u ON t.UsuarioID = u.UsuarioID
    WHERE 
        t.TarjetaID = @TarjetaID;
END

GO


