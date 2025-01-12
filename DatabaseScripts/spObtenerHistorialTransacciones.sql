USE [CreditCardDb]
GO

/****** Object:  StoredProcedure [dbo].[ObtenerHistorialTransacciones]    Script Date: 11/1/2025 21:42:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Luis Guzman>
-- Create date: <7/1/2025>
-- Description:	<Muestra el hitorial de transacciones ya sea de pago o compras>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerHistorialTransacciones]
    @TarjetaID INT
AS
BEGIN
    SELECT 
        TransaccionID,
        TarjetaID,
        Fecha,
        Descripcion,
        Monto,
        TipoTransaccion
    FROM 
        Transacciones
    WHERE 
        TarjetaID = @TarjetaID
    ORDER BY 
        Fecha DESC;
END;
GO


