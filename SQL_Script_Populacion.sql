Insert into NACIONALIDAD (Nacion) Values ('Costa Rica')
Insert into NACIONALIDAD (Nacion) Values ('Guatemala')
Insert into NACIONALIDAD (Nacion) Values ('El Salvador')
Insert into NACIONALIDAD (Nacion) Values ('Belice')
Insert into NACIONALIDAD (Nacion) Values ('Nicaragua')
Insert into NACIONALIDAD (Nacion) Values ('Honduras')
Insert into NACIONALIDAD (Nacion) Values ('Panama')

Insert into CATEGORIA (Clasificacion) values ('Junior')
Insert into CATEGORIA (Clasificacion) values ('Sub-23')
Insert into CATEGORIA (Clasificacion) values ('Open')
Insert into CATEGORIA (Clasificacion) values ('ELite')
Insert into CATEGORIA (Clasificacion) values ('Master A')
Insert into CATEGORIA (Clasificacion) values ('Master B')
Insert into CATEGORIA (Clasificacion) values ('Master C')

Insert into CLASIFICACION_ACTIVIDAD (Clasificacion) values ('Correr')
Insert into CLASIFICACION_ACTIVIDAD (Clasificacion) values ('Nadar')
Insert into CLASIFICACION_ACTIVIDAD (Clasificacion) values ('Ciclismo')
Insert into CLASIFICACION_ACTIVIDAD (Clasificacion) values ('Senderismo')
Insert into CLASIFICACION_ACTIVIDAD (Clasificacion) values ('Kayak')
Insert into CLASIFICACION_ACTIVIDAD (Clasificacion) values ('Caminata')

Insert into PATROCINADOR (Nmbr_comercial, Nmbr_rep_legal, Logo, Num_rep_legal) values ('VMW', 'Pedro Perico', 'VMW.png', 11111111)
Insert into PATROCINADOR (Nmbr_comercial, Nmbr_rep_legal, Logo, Num_rep_legal) values ('Inca Cola', 'Walter White', 'IncaCola.png', 22222222)
Insert into PATROCINADOR (Nmbr_comercial, Nmbr_rep_legal, Logo, Num_rep_legal) values ('PeraPhone', 'Gojo Satoru', 'PeraPhone.png', 33333333)

Insert into FONDO_ALTURA (Tipo_f_a) values ('Fondo')
Insert into FONDO_ALTURA (Tipo_f_a) values ('Altura')

Insert into CARRERA (Nombre, Fecha, Hora, Precio, Kms_total, Recorrido_gpx, Clase_actividad) values ('Kindora Run 2023', '2023-11-27', '7:00', 30000, 10, 'Aqui va el gpx xd', 'Correr')

Insert into PATROCINA_CARRERA (Nmbr_patrocinador, Nmbr_carrera) values ('VMW', 'Kindora Run 2023')
Insert into PATROCINA_CARRERA (Nmbr_patrocinador, Nmbr_carrera) values ('PeraPhone', 'Kindora Run 2023')

Insert into CUENTA_BANCO (Numero_cuenta, Carrera_dueno) values (4745, 'Kindora Run 2023')

Insert into RETOS (Nombre, Kms_total, Fecha_inicio, Fecha_fin, Fondo_altura, Clase_actividad) values ('Reto Fin de A�o Correr 2023', 100, '2023-12-01', '2024-01-01', 'Fondo', 'Correr')
Insert into RETOS (Nombre, Kms_total, Fecha_inicio, Fecha_fin, Fondo_altura, Clase_actividad) values ('Reto Fin de A�o Bici 2023', 200, '2023-12-01', '2024-01-01', 'Fondo', 'Ciclismo')

Insert into PATROCINA_RETO (Nmbr_patrocinador, Nmbr_reto) values ('Inca Cola', 'Reto Fin de A�o Correr 2023')
Insert into PATROCINA_RETO (Nmbr_patrocinador, Nmbr_reto) values ('Inca Cola', 'Reto Fin de A�o Bici 2023')
