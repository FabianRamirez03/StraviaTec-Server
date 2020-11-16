--Llenado de las tablas constantes
--Tabla Usuarios
insert into usuario (nombreUsuario,contrasena, primerNombre, apellidos, fechaNacimiento, nacionalidad) 
values ('mario123', '123','Mario','Araya','1999-03-02','costarricense');
insert into usuario (nombreUsuario,contrasena, primerNombre, apellidos, fechaNacimiento, nacionalidad) 
values ('wajo10', '123','Wajib','Zaglul','1999-04-02','costarricense');


--Tabla de los amigos de los usuarios
insert into AmigosUsuario values (1,2);
insert into AmigosUsuario values (2,1);

--Tabla Actividad
insert into actividad(nombreActividad,fecha,tipoActividad) 
values ('Caminata Recreacional','2018-06-22 20:10:25','caminar');

--Tabla ActividadDeportista
insert into ActividadDeportista (idActividad,idDeportista) values ('1','1');
insert into ActividadDeportista (idActividad,idDeportista) values ('2','1');

--Tabla grupo
insert into Grupo(nombre,idAdministrador) values ('Moncho Bikers','1');

--Tabla UsuariosPorGrupo
insert into UsuariosPorGrupo values ('1','1');
insert into UsuariosPorGrupo values ('2','1');

--Tabla Carrera
insert into Carrera (idorganizador,nombrecarrera,fechacarrera,tipoactividad,costo,cuentabancaria)
values ('3','Vuelta al Arenal','2016-06-22 19:10:25','Bicicleta',10500,'CR-5412378');

insert into Carrera (idorganizador,nombrecarrera,fechacarrera,tipoactividad,costo,cuentabancaria,privada)
values ('3','Palmarin','2020-06-22 19:10:25','Caminata',5000,'CR-5412378','True');
--Tabla carrerasGrupo
insert into CarrerasGrupo values ('1','4')

--Tabla solicitudesCarrera
insert into solicitudesCarrera (idCarrera,idUsuario) values (1,4);

--Tabla UsuariosCarrera
insert into usuariosCarrera (idDeportista,idcarrera) values ('2','1');
insert into usuariosCarrera (idDeportista,idcarrera) values ('2','3');
insert into usuariosCarrera (idDeportista,idcarrera) values ('4','1');
insert into usuariosCarrera (idDeportista,idcarrera) values ('4','5');

insert into usuariosCarrera (idDeportista,idcarrera) values ('9','4');
select * from carrera


--Tabla categoriaCarrera
insert into categoriaCarrera values ('1','Elite');
insert into categoriaCarrera values ('1','MasterA');

--Tabla Reto
insert into Reto (idOrganizador,nombreReto,objetivoReto,fechaInicio,fechaFinaliza,tipoActividad,tipoReto)
values ('1','2000 metros de ascenso','Mejorar el rendimiento','2020-10-20 7:00:00','2020-10-25 7:00:00','Senderismo','altitud');
insert into Reto (idOrganizador,nombreReto,objetivoReto,fechaInicio,fechaFinaliza,tipoActividad,tipoReto)
values ('1','Fondo de 3 dias', '100km','2020-10-20 7:00:00','2020-10-23 7:00:00','Ciclismo','fondo');
insert into Reto (idOrganizador,nombreReto,objetivoReto,fechaInicio,fechaFinaliza,tipoActividad,tipoReto, privada)
values ('1','Corrientes del Arenal','5km','2021-06-22 7:00:00','2021-06-22 9:00:00','Kayak','Fondo','True');

--Tabla carrerasGrupo
insert into RetosGrupo values ('1','3')

--Tabla usuariosReto
insert into usuariosReto (idDeportista,idReto) values ('2','1');
insert into usuariosReto (idDeportista,idReto) values ('4','3');


--Tabla de Patrocinadores 
insert into Patrocinador(nombreComercial, representante, numeroTelefono) 
values ('Piros', 'Julio Vargas', '22655331');
insert into Patrocinador(nombreComercial, representante, numeroTelefono) 
values ('Treck', 'Richard Burke', '18005858735');
insert into Patrocinador(nombreComercial, representante, numeroTelefono) 
values ('San Antonio', 'Jorge Zaglul', '24455190');
insert into Patrocinador(nombreComercial, representante, numeroTelefono) 
values ('Gatorade', 'Robert Cade', '24376700');
insert into Patrocinador(nombreComercial, representante, numeroTelefono) 
values ('On Wheels', 'Pedro Perico', '24472525');

--Tabla PatrocinadoresReto
insert into PatrocinadoresReto values ('1','Piros');

--Tabla PatrocinadoresReto
insert into PatrocinadoresCarrera values ('1','San Antonio');