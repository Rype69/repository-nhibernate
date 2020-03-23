

IF EXISTS (SELECT * FROM sysobjects WHERE name = 'GetUserName' AND type = 'FN')
BEGIN
    DROP FUNCTION [dbo].[GetUserName]
END
