
CREATE TRIGGER trigger_KgMonitoring
ON KgMonitoring
AFTER INSERT
AS
	-- tomaremos por fecha
	DECLARE @fecha DATE = ( SELECT CONVERT(date, GETDATE()) );

	SELECT @fecha;