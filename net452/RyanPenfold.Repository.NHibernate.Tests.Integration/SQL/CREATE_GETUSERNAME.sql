

CREATE FUNCTION [dbo].[GetUserName]
(
	-- Add the parameters for the function here
	 @FirstName AS NVARCHAR(MAX)
	,@LastName AS NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(MAX)

	-- Add the T-SQL statements to compute the return value here
	IF @LastName IS NOT NULL AND LEN(RTRIM(LTRIM(@LastName))) > 0
	BEGIN
		SET @ResultVar = @LastName
	END
	
	IF @FirstName IS NOT NULL AND LEN(RTRIM(LTRIM(@FirstName))) > 0
	BEGIN
		IF @ResultVar IS NULL 
		BEGIN
			SET @ResultVar = ''
		END
				
		IF LEN(RTRIM(LTRIM(@ResultVar))) > 0
		BEGIN
			SET @ResultVar = @ResultVar + ', '
		END
		
		SET @ResultVar = @ResultVar + @FirstName
	END

	-- Return the result of the function
	RETURN @ResultVar

END

