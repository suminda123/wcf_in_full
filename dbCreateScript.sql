USE [master]
GO
/****** Object:  Database [Vehicles]    Script Date: 16/05/2016 1:05:13 PM ******/
CREATE DATABASE [Vehicles]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Vehicles', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Vehicles.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Vehicles_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Vehicles_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Vehicles] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Vehicles].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
USE [Vehicles]
GO
CREATE TABLE [dbo].[tblVehicles](
	[Id] [uniqueidentifier] NOT NULL,
	[Make] [nchar](50) NOT NULL,
	[Description] [nchar](500) NOT NULL,
 CONSTRAINT [PK_tblVehicles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE PROCEDURE spCreateVehicle
	@Id uniqueidentifier,
	@Make varchar(50),
	@Description varchar(500)
AS 
BEGIN
	INSERT INTO tblVehicles
			   ([Id]
			   ,[Make]
			   ,[Description])
		 VALUES
			   (@Id,
			   @Make,
			   @Description)

	SELECT @Id
END

GO

CREATE PROCEDURE spDeleteVehicle
	@Id uniqueidentifier
AS 
BEGIN
	DELETE FROM tblVehicles WHERE Id=@id
	return 1
END

GO
CREATE PROCEDURE spGetAllVehicles
AS
BEGIN
SELECT 
	Id,
	Make, 
	Description
FROM
	tblVehicles
END
GO

CREATE PROCEDURE spUpdateVehicle
	@Id uniqueidentifier,
	@Make varchar(50),
	@Description varchar(500)
AS 
BEGIN
	UPDATE tblVehicles
		SET Make=@Make, [Description]= @Description
	WHERE
	  Id=@Id
END

GO
USE [master]
GO
ALTER DATABASE [Vehicles] SET  READ_WRITE 
GO
