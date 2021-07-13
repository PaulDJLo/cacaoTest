CREATE DATABASE CacaoUniversity
GO
USE CacaoUniversity
GO
CREATE TABLE Calificaciones
(
	Id INT IDENTITY PRIMARY KEY,
	CalificacionOriginal INT,
	CalificacionFinal INT
)
GO
CREATE TABLE Estudiantes
(
	Id INT IDENTITY PRIMARY KEY,
	Nombre VARCHAR(200),
	ApellidoPaterno VARCHAR(200),
	ApellidoMaterno VARCHAR(200),
	IdCalificaciones INT FOREIGN KEY  REFERENCES Calificaciones(id)
)
GO
CREATE PROCEDURE SP_INSERT_ESTUDIANTES
@Nombre VARCHAR(200),
@ApellidoPaterno VARCHAR(200),
@ApellidoMaterno VARCHAR(200),
@CalifOrig INT,
@CalifFinal INT
AS
BEGIN
	DECLARE  @IdCalifs table(id int); --tabla para obtener el id del nuevo alumno guardado
	DECLARE @idValue int; --variable donde se recupera el id de la tabla anterior

	--guardamos primero las calificaciones y el id generado se guarda en la variable tipo tabla 
	INSERT INTO Calificaciones 
	OUTPUT INSERTED.Id INTO @IdCalifs
    VALUES (@CalifOrig,@CalifFinal)

	--se obtiene el id guardado de la variable tipo tabla
	SELECT @idValue=id 
	FROM  @IdCalifs;
	--se guarda el nuevo estudiante 
	INSERT INTO Estudiantes
    VALUES (@Nombre,@ApellidoPaterno,@ApellidoMaterno, @idValue);
END
GO

CREATE PROCEDURE SP_LISTA_ESTUDIANTES
AS
BEGIN
	SELECT s.Nombre,s.ApellidoPaterno,s.ApellidoMaterno,c.CalificacionOriginal,c.CalificacionFinal 
	FROM Estudiantes s 
	INNER JOIN Calificaciones c
	on s.Id = c.Id
END
GO

CREATE PROCEDURE SP_TOTAL_ESTUDIANTES
AS
BEGIN
	SELECT  COUNT(1) as 'TotalEstudiantes'  
	FROM Estudiantes
END
GO

--EXEC SP_INSERT_ESTUDIANTES 'CHUMEL TORRES',38,40
--EXEC SP_LISTA_ESTUDIANTES
--EXEC SP_TOTAL_ESTUDIANTES