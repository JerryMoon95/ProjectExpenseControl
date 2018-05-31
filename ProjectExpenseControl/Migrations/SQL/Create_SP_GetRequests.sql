USE DB_A39AAA_net
if exists (select * from dbo.sysobjects where id = object_id('SP_GetRequests') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].SP_GetRequests
GO
	CREATE PROCEDURE SP_GetRequests	
			@IDE_USER int ,
			@TYPE_USER int

AS

BEGIN
	DECLARE @ErrorMessage varchar(max),
					@Permission varchar(4)
	DECLARE @temp TABLE(permission int NOT NULL)

	IF  @TYPE_USER =	1 
	BEGIN 
		INSERT INTO @temp  select STA_IDE_STATUS_APROV from StatusAprovs
	END

	IF  (@TYPE_USER =	2 )
	BEGIN 
		INSERT INTO @temp  values(5)
		INSERT INTO @temp  values(6)
		INSERT INTO @temp  values(7)
		INSERT INTO @temp  values(8)
		INSERT INTO @temp  values(9)
		INSERT INTO @temp  values(10)
		INSERT INTO @temp  values(11)
		INSERT INTO @temp  values(12)
		INSERT INTO @temp  values(13)
	END
	IF  @TYPE_USER =3 
	BEGIN
		INSERT INTO @temp  values(5)
		INSERT INTO @temp  values(8)
	END
	IF  @TYPE_USER =	4 
	BEGIN 
		INSERT INTO @temp  values(11)
	END

	
	BEGIN TRY					
			SELECT 
				*
			FROM 
				Requests
			WHERE
				((REQ_IDE_USER = @IDE_USER AND @TYPE_USER = 2) OR
				(REQ_IDE_USER <> @IDE_USER AND (@TYPE_USER = 1 OR @TYPE_USER = 3 OR @TYPE_USER = 4))) 
				AND REQ_IDE_STATUS_APROV in (select * from @temp)
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT <> 0
			BEGIN
				SET @ErrorMessage = ERROR_MESSAGE()
				RAISERROR('Error has occurred in ...', 16, 1, @ErrorMessage)
				RETURN 1 
			END
	END CATCH
END

