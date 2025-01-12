USE [CreditCardDb]
GO

/****** Object:  StoredProcedure [dbo].[AgregarTransaccion]    Script Date: 11/1/2025 21:41:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Luis Guzman>
-- Create date: <6/1/2025>
-- Description:	<Procedimiento para agregar una nueva transacción>
-- =============================================
CREATE PROCEDURE [dbo].[AgregarTransaccion]
@TarjetaID INT,
@Fecha DATE,
@Descripcion NVARCHAR(200),
@Monto DECIMAL(18,2),
@Tipo NVARCHAR(50)
AS
BEGIN
    INSERT INTO Transacciones (TarjetaID, Fecha, Descripcion, Monto, TipoTransaccion)
    VALUES (@TarjetaID, @Fecha, @Descripcion, @Monto, @Tipo);

    UPDATE TarjetasDeCredito
    SET SaldoActual = SaldoActual + @Monto
    WHERE TarjetaID = @TarjetaID;
END

GO


