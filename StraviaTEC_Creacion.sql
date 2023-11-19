CREATE DATABASE StraviaTECDB;
GO

USE StraviaTECDB;
GO

CREATE TABLE ATLETA
(
	Usuario varchar(20) not null,
	Contrasena binary(64) not null,
	Foto nvarchar(255),
	Nombre varchar(15) not null,
	Apellido_1 varchar(15) not null,
	Apellido_2 varchar(15) not null,
	Fecha_nacimiento date not null,
	Nacionalidad varchar(40) not null,
	Clasificacion varchar(10)not null,
	Primary key (Usuario)
);

CREATE TABLE AMIGOS
(
	Usuario varchar(20) not null,
	Amigo varchar(20) not null,
	Primary key (Usuario, Amigo)
);

CREATE TABLE NACIONALIDAD
(
	Nacion varchar(40) not null,
	Primary key (Nacion)
);

CREATE TABLE CATEGORIA
(
	Clasificacion varchar(10) not null,
	Primary key (Clasificacion)
);

CREATE TABLE GRUPOS
(
	Nombre varchar(30) not null,
	Atleta_admin varchar(20) not null,
	Primary key (Nombre)
);

CREATE TABLE INTEGRANTES
(
	Nmbr_grupo varchar(30) not null,
	Integrante varchar(20) not null,
	Primary key (Nmbr_grupo, Integrante)
);

CREATE TABLE INSCRITO
(
	Carrera varchar(50) not null,
	Atleta varchar(20) not null,
	Pago money,
	Primary key (Carrera, Atleta)
);

CREATE TABLE PRIV_CARRERA
(
	Nmbr_Carrera varchar(50) unique not null,
	Nmbr_Grupo varchar(30) not null,
	Primary key (Nmbr_Carrera, Nmbr_Grupo)
);

CREATE TABLE PRIV_RETO
(
	Nmbr_reto varchar(30) unique not null,
	Nmbr_Grupo varchar(30) not null,
	Primary key (Nmbr_reto, Nmbr_Grupo)
);

CREATE TABLE ACTIVIDAD
(
	Nmbr_Actividad varchar(20) not null,
	Fecha date not null,
	Hora time not null,
	Kms_hechos int not null,
	Duracion time,
	Recorrido_gpx nvarchar(500),
	Atleta varchar(20) not null,
	Clase_actividad varchar(30) not null,
	Primary key (Nmbr_Actividad, Atleta)
);

CREATE TABLE CLASIFICACION_ACTIVIDAD
(
	Clasificacion varchar(30) not null,
	Primary key (Clasificacion)
);

CREATE TABLE CARRERA
(
	Nombre varchar(50) not null,
	Fecha date not null,
	Hora time not null,
	Precio money not null,
	Kms_total int not null,
	Recorrido_gpx nvarchar(500),
	Finalizado bit default 0,
	Clase_actividad varchar(30) not null,
	Primary key (Nombre)
);

CREATE TABLE RETOS
(
	Nombre varchar(30) not null,
	Kms_total int not null,
	Finalizado bit default 0,
	Fecha_inicio date not null,
	Fecha_fin date not null,
	Fondo_altura varchar(10) not null,
	Clase_actividad varchar(30) not null,
	Primary key (Nombre)
);

CREATE TABLE CUENTA_BANCO
(
	Numero_cuenta int not null,
	Carrera_dueno varchar(50) not null,
	Primary key (Numero_cuenta)
);

CREATE TABLE PATROCINA_CARRERA
(
	Nmbr_patrocinador varchar(20) not null,
	Nmbr_carrera varchar(50) not null,
	Primary key (Nmbr_patrocinador, Nmbr_carrera)
);

CREATE TABLE PATROCINA_RETO
(
	Nmbr_patrocinador varchar(20) not null,
	Nmbr_reto varchar(30) not null,
	Primary key (Nmbr_patrocinador, Nmbr_reto)
);

CREATE TABLE FONDO_ALTURA
(
	Tipo_f_a varchar(10) not null,
	Primary key (Tipo_f_a)
);

CREATE TABLE PATROCINADOR
(
	Nmbr_comercial varchar(20) not null,
	Nmbr_rep_legal varchar(30) not null,
	Logo nvarchar(255),
	Num_rep_legal int not null,
	Primary key (Nmbr_comercial)
);

CREATE TABLE ACT_CARRERA
(
	Nmbr_Carrera varchar(50) not null,
	Nmbr_Actividad varchar(20) not null,
	Atleta varchar(20) not null,
	Primary key (Nmbr_Carrera, Nmbr_Actividad, Atleta)
);

CREATE TABLE ACT_RETO
(
	Nmbr_Reto varchar(30) not null,
	Nmbr_Actividad varchar(20) not null,
	Atleta varchar(20) not null,
	Primary key (Nmbr_Reto, Nmbr_Actividad, Atleta)
);

ALTER TABLE AMIGOS 
ADD CONSTRAINT AMIGO_USUARIO_FK FOREIGN KEY (Usuario) 
REFERENCES ATLETA (Usuario);

ALTER TABLE AMIGOS
ADD CONSTRAINT AMIGO_AMIGO_FK FOREIGN KEY (Amigo)
REFERENCES ATLETA (Usuario);

ALTER TABLE ATLETA
ADD CONSTRAINT ATLETA_NACIONALIDAD_FK FOREIGN KEY (Nacionalidad)
REFERENCES NACIONALIDAD (Nacion);

ALTER TABLE ATLETA
ADD CONSTRAINT ATLETA_CLASIFICACION FOREIGN KEY (Clasificacion)
REFERENCES CATEGORIA (Clasificacion);

ALTER TABLE GRUPOS
ADD CONSTRAINT GRUPO_ADMIN_FK FOREIGN KEY (Atleta_admin)
REFERENCES ATLETA (Usuario);

ALTER TABLE INTEGRANTES 
ADD CONSTRAINT INTEGRANTE_GRUPO_FK FOREIGN KEY (Nmbr_grupo)
REFERENCES GRUPOS (Nombre);

ALTER TABLE INTEGRANTES
ADD CONSTRAINT INTEGRANTES_ATLETA_FK FOREIGN KEY (Integrante)
REFERENCES ATLETA (Usuario);

ALTER TABLE INSCRITO
ADD CONSTRAINT INSCRITO_CARRERA_FK FOREIGN KEY (Carrera)
REFERENCES CARRERA (Nombre);

ALTER TABLE INSCRITO
ADD CONSTRAINT INSCRITO_ATLETA_FK FOREIGN KEY (Atleta)
REFERENCES ATLETA (Usuario);

ALTER TABLE PRIV_CARRERA
ADD CONSTRAINT PRIV_CARRERA_FK FOREIGN KEY (Nmbr_Carrera)
REFERENCES CARRERA (Nombre);

ALTER TABLE PRIV_CARRERA
ADD CONSTRAINT PRIV_CARRERA_GRUPO_FK FOREIGN KEY (Nmbr_Grupo)
REFERENCES GRUPOS (Nombre);

ALTER TABLE PRIV_RETO
ADD CONSTRAINT PRIV_RETO_FK FOREIGN KEY (Nmbr_Reto)
REFERENCES RETOS (Nombre);

ALTER TABLE PRIV_RETO
ADD CONSTRAINT PRIV_RETO_GRUPO_FK FOREIGN KEY (Nmbr_Grupo)
REFERENCES GRUPOS (Nombre);

ALTER TABLE ACTIVIDAD
ADD CONSTRAINT ACTIVIDAD_ATLETA_FK FOREIGN KEY (Atleta)
REFERENCES ATLETA (Usuario);

ALTER TABLE ACTIVIDAD
ADD CONSTRAINT ACTIVIDAD_CLASIFICACION_FK FOREIGN KEY (Clase_actividad)
REFERENCES CLASIFICACION_ACTIVIDAD (Clasificacion);

ALTER TABLE CARRERA
ADD CONSTRAINT CARRERA_CLASIFICACION_FK FOREIGN KEY (Clase_actividad)
REFERENCES CLASIFICACION_ACTIVIDAD (Clasificacion);

ALTER TABLE RETOS
ADD CONSTRAINT RETOS_CLASIFICACION_FK FOREIGN KEY (Clase_actividad)
REFERENCES CLASIFICACION_ACTIVIDAD (Clasificacion);

ALTER TABLE RETOS
ADD CONSTRAINT RETO_FONDO_ALTURA_FK FOREIGN KEY (Fondo_altura)
REFERENCES FONDO_ALTURA (Tipo_f_a);

ALTER TABLE CUENTA_BANCO
ADD CONSTRAINT CUENTA_BANCO_CARRERA_FK FOREIGN KEY (Carrera_dueno)
REFERENCES CARRERA (Nombre);

ALTER TABLE PATROCINA_CARRERA
ADD CONSTRAINT PATROCINA_CARRERA_PATROCINADOR_FK FOREIGN KEY (Nmbr_patrocinador)
REFERENCES PATROCINADOR (Nmbr_comercial);

ALTER TABLE PATROCINA_CARRERA
ADD CONSTRAINT PATROCINA_CARRERA_CARRERA_FK FOREIGN KEY (Nmbr_carrera)
REFERENCES CARRERA (Nombre);

ALTER TABLE PATROCINA_RETO
ADD CONSTRAINT PATROCINA_RETO_PATROCINADOR_FK FOREIGN KEY (Nmbr_patrocinador)
REFERENCES PATROCINADOR (Nmbr_comercial);

ALTER TABLE PATROCINA_RETO
ADD CONSTRAINT PATROCINA_RETO_RETO_FK FOREIGN KEY (Nmbr_reto)
REFERENCES RETOS (Nombre);

ALTER TABLE ACT_CARRERA
ADD CONSTRAINT ACT_CARRERA_CARRERA_FK FOREIGN KEY (Nmbr_Carrera)
REFERENCES CARRERA (Nombre);

ALTER TABLE ACT_CARRERA
ADD CONSTRAINT ACT_CARRERA_ACT_FK FOREIGN KEY (Nmbr_Actividad, Atleta)
REFERENCES ACTIVIDAD (Nmbr_Actividad, Atleta);


ALTER TABLE ACT_RETO
ADD CONSTRAINT ACT_RETO_FK FOREIGN KEY (Nmbr_Reto)
REFERENCES RETOS (Nombre);

ALTER TABLE ACT_RETO
ADD CONSTRAINT ACT_RETO_ACT_FK FOREIGN KEY (Nmbr_Actividad, Atleta)
REFERENCES ACTIVIDAD (Nmbr_Actividad, Atleta);
GO

--Views

CREATE VIEW ACTIVIDAD_RECIENTE AS
SELECT Usuario, Nombre, Apellido_1, Apellido_2, Nmbr_Actividad, Clase_actividad, Hora, Recorrido_gpx, Kms_hechos
FROM ATLETA, ACTIVIDAD 
WHERE Usuario = Atleta AND Hora = (SELECT MAX(Hora) FROM ACTIVIDAD WHERE Atleta = Usuario);
GO

CREATE VIEW ATLETAS_POR_CARRERA AS
SELECT ATLETA.Nombre, CONCAT(ATLETA.Apellido_1, ' ', ATLETA.Apellido_2) AS Apellidos, (0 + Convert(Char(8),GETDATE(),112) - Convert(Char(8),ATLETA.Fecha_nacimiento,112)) / 10000 AS EDAD, INSCRITO.Carrera, ATLETA.Clasificacion
FROM ATLETA, INSCRITO
WHERE ATLETA.Usuario= INSCRITO.Atleta;
GO

CREATE VIEW RECORD_POR_CARRERA AS
SELECT ATLETA.Nombre, CONCAT(ATLETA.Apellido_1, ' ', ATLETA.Apellido_2) AS Apellidos, (0 + Convert(Char(8),GETDATE(),112) - Convert(Char(8),ATLETA.Fecha_nacimiento,112)) / 10000 AS EDAD, ACT_CARRERA.Nmbr_Carrera, ATLETA.Clasificacion, ACTIVIDAD.Duracion
FROM ATLETA, ACT_CARRERA, ACTIVIDAD
WHERE ATLETA.Usuario= ACT_CARRERA.Atleta AND ACTIVIDAD.Nmbr_Actividad = ACT_CARRERA.Nmbr_Actividad
GO

--STORED PROCEDURES

CREATE PROCEDURE Registrar
	@Usuario varchar(20), @Contrasena varchar(15), @Foto nvarchar(255), @Nombre varchar(15), @Apellido_1 varchar(15),
	@Apellido_2 varchar(15), @Fecha_nacimiento date, @Nacionalidad varchar(40), @Clasificacion varchar(40)
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT Usuario FROM Atleta WHERE Usuario = @Usuario)
	BEGIN
		SELECT -1 --Atleta ya existe
	END
	ELSE
	BEGIN
		INSERT INTO ATLETA
			(Usuario, Contrasena, Foto, Nombre, Apellido_1, Apellido_2, Fecha_nacimiento,
			Nacionalidad, Clasificacion)
		VALUES (
			@Usuario, HASHBYTES('SHA2_512', @Contrasena), @Foto, @Nombre, @Apellido_1, @Apellido_2,
			@Fecha_nacimiento, @Nacionalidad, @Clasificacion)
		SELECT 0 --Atleta registrado
	END
END;
GO

CREATE PROCEDURE LoginAtleta
	@Usuario varchar(20), @Contrasena varchar(15)
AS
BEGIN
	SET NOCOUNT ON;
	IF NOT EXISTS (SELECT Usuario FROM ATLETA WHERE Usuario = @Usuario)
	BEGIN
		SELECT -1 --Atleta no encontrado
	END
	ELSE IF NOT EXISTS (SELECT Usuario FROM ATLETA WHERE Usuario = @Usuario AND Contrasena = HASHBYTES('SHA2_512', @Contrasena))
	BEGIN
		SELECT -2 --Contrasena Incorrecta
	END
	ELSE
		BEGIN
			SELECT 0 --Atleta accedido
		END
END;
GO

CREATE PROCEDURE UpdateAtleta
		@Usuario varchar(20), @Contrasena varchar(15), @Foto nvarchar(255), @Nombre varchar(15), @Apellido_1 varchar(15),
		@Apellido_2 varchar(15), @Fecha_nacimiento date, @Nacionalidad varchar(40), @Clasificacion varchar(40)
AS
BEGIN
	SET NOCOUNT ON;
	IF NOT EXISTS (SELECT Usuario FROM ATLETA WHERE Usuario = @Usuario)
	BEGIN
		SELECT -1 --Atleta no encontrado
	END
	ELSE IF NOT EXISTS (SELECT Usuario FROM ATLETA WHERE Usuario = @Usuario AND Contrasena =  HASHBYTES('SHA2_512', @Contrasena))
	BEGIN
		SELECT -2 --Contrasena equivocada
	END
	ELSE 
	BEGIN
		UPDATE ATLETA
		SET			Foto = @Foto,
					Nombre = @Nombre,
					Apellido_1 = @Apellido_1,
					Apellido_2 = @Apellido_2,
					Fecha_nacimiento = @Fecha_nacimiento,
					Nacionalidad = @Nacionalidad,
					Clasificacion = @Clasificacion
			WHERE Usuario = @Usuario;
		SELECT 0 --Atleta actualizado
	END
END;
GO

CREATE PROCEDURE ActividadRecienteAmigos
	@Usuario varchar(20)
AS
BEGIN
	SELECT * FROM ACTIVIDAD_RECIENTE
		WHERE EXISTS(
				SELECT Amigo FROM AMIGOS 
				WHERE ACTIVIDAD_RECIENTE.Usuario = AMIGOS.Amigo AND AMIGOS.Usuario = @Usuario)
END;
GO

CREATE PROCEDURE AmigosDisponibles
	@Usuario varchar(20)
AS
BEGIN
	SELECT Usuario, Nombre, Apellido_1, Fecha_nacimiento, Foto FROM ATLETA
	WHERE ATLETA.Usuario IN(
		SELECT AMIGOS.Amigo FROM AMIGOS
		Where Usuario = @Usuario)
		AND ATLETA.Usuario != @Usuario;
END;
GO

CREATE PROCEDURE getAmigos
	@Usuario varchar(20)
AS
BEGIN
	SELECT Usuario, Nombre, Apellido_1, Apellido_2, Nacionalidad
	FROM ATLETA
	WHERE ATLETA.Usuario NOT IN (
		SELECT AMIGOS.Amigo FROM AMIGOS
		WHERE Usuario = @Usuario)
		AND ATLETA.Usuario != @Usuario
END;
GO

CREATE PROCEDURE GruposDisponibles
	@Usuario varchar(20)
AS
BEGIN
	SELECT * FROM GRUPOS
		WHERE Nombre NOT IN(
			SELECT INTEGRANTES.Nmbr_grupo FROM INTEGRANTES
			WHERE INTEGRANTES.Integrante = @Usuario)
			AND Atleta_admin != @Usuario;
END;
GO

CREATE PROCEDURE getGrupos
	@Usuario varchar(20)
AS
BEGIN
	SELECT * FROM GRUPOS
		WHERE Nombre IN(
			SELECT INTEGRANTES.Nmbr_grupo FROM INTEGRANTES
			WHERE INTEGRANTES.Integrante = @Usuario)
			OR Atleta_admin = @Usuario;
END;
GO	

CREATE PROCEDURE AddActividad
	@Nmbr_Actividad varchar(20), @Fecha date, @Hora time, @Kms_hechos int, @Duracion time,
	@Recorrido_gpx nvarchar(500), @Atleta varchar(20), @Clase_actividad varchar(20), @Car_Ret varchar(15), @Car_Ret_Nmbr varchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS (SELECT Atleta FROM ACTIVIDAD Where Atleta = @Atleta AND Nmbr_Actividad = @Nmbr_Actividad)
	BEGIN
		SELECT -1 --Una actividad de ese nombre ya existe
	END
	ELSE
		IF (@Car_Ret = 'Carrera')
			BEGIN
				IF EXISTS (SELECT Nombre FROM CARRERA, INSCRITO WHERE CARRERA.Nombre = @Car_Ret_Nmbr AND INSCRITO.Atleta = @Atleta AND INSCRITO.Carrera = Carrera.Nombre)
					BEGIN
						INSERT INTO ACTIVIDAD
							(Nmbr_Actividad, Fecha, Hora, Kms_hechos, Duracion, Recorrido_gpx, Atleta, Clase_actividad)
						VALUES (@Nmbr_Actividad, @Fecha, @Hora, @Kms_hechos, @Duracion, @Recorrido_gpx, @Atleta, @Clase_actividad)
						INSERT INTO ACT_CARRERA
							(Nmbr_Carrera, Nmbr_Actividad, Atleta)
						VALUES (@Car_Ret_Nmbr, @Nmbr_Actividad, @Atleta)
						SELECT 0 --Actividad agregada
					END
				ELSE
					BEGIN
						SELECT -3 --Carrera o Usuario no existen
					END
			END
		ELSE IF (@Car_Ret = 'Reto')
			BEGIN
				IF EXISTS (SELECT Nombre FROM RETOS WHERE RETOS.Nombre = @Car_Ret_Nmbr)
					BEGIN
						INSERT INTO ACTIVIDAD
							(Nmbr_Actividad, Fecha, Hora, Kms_hechos, Duracion, Recorrido_gpx, Atleta, Clase_actividad)
						VALUES (@Nmbr_Actividad, @Fecha, @Hora, @Kms_hechos, @Duracion, @Recorrido_gpx, @Atleta, @Clase_actividad)
						INSERT INTO ACT_RETO
							(Nmbr_Reto, Nmbr_Actividad, Atleta)
						VALUES (@Car_Ret_Nmbr, @Nmbr_Actividad, @Atleta)
						SELECT 0 --Actividad agregada
					END
				ELSE
					BEGIN
						SELECT -4 --Reto no existe
					END
			END
		ELSE
			INSERT INTO ACTIVIDAD
				(Nmbr_Actividad, Fecha, Hora, Kms_hechos, Duracion, Recorrido_gpx, Atleta, Clase_actividad)
			VALUES (@Nmbr_Actividad, @Fecha, @Hora, @Kms_hechos, @Duracion, @Recorrido_gpx, @Atleta, @Clase_actividad)
			SELECT 0 --Actividad agregada
END;
GO

CREATE PROCEDURE RegistrarReto
	@Nombre varchar(30), @Kms_total int, @Finalizado bit, @Fecha_inicio date, @Fecha_fin date, @Fondo_altura varchar(10), 
	@Clase_actividad varchar(30), @Grupo varchar(30), @Privado bit
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS( SELECT Nombre FROM RETOS WHERE Nombre = @Nombre)
		BEGIN
			SELECT -1 --Reto ya existe
		END
	ELSE IF NOT EXISTS (SELECT Clasificacion FROM CLASIFICACION_ACTIVIDAD WHERE Clasificacion = @Clase_actividad)
		BEGIN
			SELECT -2 --Tipo de actividad no encontrado
		END
	ELSE IF NOT EXISTS (SELECT Tipo_f_a FROM FONDO_ALTURA WHERE Tipo_f_a = @Fondo_altura)
		BEGIN
			SELECT -3 --No existe tipo de fondo o altura
		END
	ELSE
	BEGIN
		INSERT INTO RETOS
			(Nombre, Kms_total, Finalizado, Fecha_inicio, Fecha_fin, Fondo_altura, Clase_actividad)
		VALUES (@Nombre, @Kms_total, @Finalizado, @Fecha_inicio, @Fecha_fin, @Fondo_altura, @Clase_actividad)
		SELECT 0 --Reto registrado
	END
	IF (@Privado = 1)
		BEGIN
			INSERT INTO PRIV_RETO
				(Nmbr_reto, Nmbr_Grupo)
			VALUES (@Nombre, @Grupo)
		END
END;
GO

CREATE PROCEDURE RegistrarCarrera
	@Nombre varchar(50), @Fecha date, @Hora time, @Precio money, @Kms_total int, @Recorrido_gpx nvarchar(500),
	@Finalizado bit, @Clase_actividad varchar (30), @Privado bit, @Grupo varchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT Nombre FROM CARRERA WHERE Nombre = @Nombre)
		BEGIN
			SELECT -1 --Carrera ya existe
		END
	ELSE IF NOT EXISTS (SELECT Clasificacion FROM CLASIFICACION_ACTIVIDAD WHERE Clasificacion = @Clase_actividad)
		BEGIN
			SELECT -2 --Tipo de actividad no encontrado
		END
	ELSE
	BEGIN
		INSERT INTO CARRERA
			(Nombre , Fecha, Hora, Precio, Kms_total, Recorrido_gpx, Finalizado, Clase_actividad)
		VALUES (@Nombre, @Fecha, @Hora, @Precio, @Kms_total, @Recorrido_gpx, @Finalizado, @Clase_actividad)
		SELECT 0 --Carrera registrada
	END
	IF (@Privado =1)
		BEGIN
			INSERT INTO PRIV_CARRERA
				(Nmbr_Carrera, Nmbr_Grupo)
			VALUES (@Nombre, @Grupo)
		END
END;
GO

CREATE PROCEDURE Registrar_en_Carrera
	@Usuario varchar(20), 