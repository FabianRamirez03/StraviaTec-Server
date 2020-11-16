--Creacion de funciones

--Crear un usuario
create or replace function crearUsuario (username varchar, contra varchar, nombre varchar, apellido varchar, nacimiento date,pais  varchar, imagen bytea) returns void
as
$$
insert into usuario (nombreUsuario,contrasena, primerNombre, apellidos, fechaNacimiento, nacionalidad, foto, edad) 
values (username, contra,nombre,apellido,nacimiento,pais, imagen, extract (year from age(nacimiento)));--Calcula la edad
$$
Language sql



--Buscar usuario por ID
create or replace function buscarUsuarioId (iduser int) returns usuario
as
$$
Select * from usuario
where idUsuario = iduser;
$$
Language sql

--Buscar usuario por User Name
create or replace function buscarUsuarioUserName (username varchar) returns usuario
as
$$
Select * from usuario
where nombreUsuario = username;
$$
Language sql


--Buscar usuario por Nombre
create or replace function buscarUsuarioNombre (nombre varchar) returns usuario
as
$$
Select * from usuario
where Upper (primerNombre) = Upper (nombre);
$$
Language sql


--Buscar usuario por nombre y apellido
create or replace function buscarUsuarioNombreApellido (nombre varchar, apellido varchar) returns usuario
as
$$
Select * from usuario
where Upper (primerNombre) = Upper (nombre) and Upper(apellidos) = Upper(apellido);
$$
Language sql


--Agregar amigos a un usuario por id
create or replace function agregarAmigo (iduser integer, idamigo integer) returns void
as
$$
insert into amigosUsuario (idDeportista, idAmigo)
select idUser, idAmigo
where exists (select 1 from usuario where idUsuario = idamigo and idUser != idamigo); --Valida que el amigo exista antes de agregarlo
$$
Language sql


--Actualizar un usuario
create or replace function modificarUsuario (iduser int,username varchar, contra varchar, nombre varchar, apellido varchar, nacimiento date,pais varchar,imagen bytea)
returns void
as
$$
update usuario
set NombreUsuario = username, contrasena = contra, primerNombre = nombre, apellidos = apellido, fechaNacimiento = nacimiento, nacionalidad = pais, foto = imagen
where idUsuario = iduser;
$$
Language sql


--Eliminar un usuario por su ID
create or replace function eliminarUsuarioID (iduser int)
returns void
as
$$
Delete from usuario where idUsuario = iduser;
Delete from actividadDeportista where idDeportista = idUser;
Delete from usuariosPorGrupo where idUsuario = iduser;
Delete from amigosUsuario where idDeportista = iduser or idAmigo = iduser;
Delete from carrera where idOrganizador = iduser;
Delete from solicitudesCarrera where idUsuario = iduser;
Delete from usuariosCarrera where idDeportista = iduser;
Delete from Reto where idOrganizador = iduser;
Delete from usuariosReto where idDeportista = iduser;
$$
Language sql


--Buscar actividad por nombre
create or replace function buscarActividadNombre (nombre varchar) returns actividad
as
$$
Select * from actividad
where Upper(nombreActividad) = Upper(nombre);
$$
Language sql


--Buscar actividad por id
create or replace function buscarActividadID (idact int) returns actividad
as
$$
Select * from actividad
where idActividad = idact;
$$
Language sql


--Actividades de los usuarios
create or replace function actividadesUsuarios () returns table (
	idUsuario int, primerNombre varchar, idActividad int, nombreActividad varchar, tipoActividad varchar, fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido xml)
as
$$
Select d.idUsuario, d.primerNombre, ad.idActividad, act.nombreActividad, act.tipoActividad, act.fecha, ad.kilometraje, ad.altura, ad.duracion, ad.recorrido
from usuario d
inner join actividadDeportista ad
	on d.idUsuario = ad.idDeportista
	inner join actividad act
		on act.idActividad = ad.idActividad
		order by act.fecha desc
$$
Language sql


--Actividad por id de usuario
create or replace function actividadPorUsuario (iduser int) returns table (
	idUsuario int, primerNombre varchar, idActividad int, nombreActividad varchar,tipoActividad varchar, fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido xml)
as
$$
Select d.idUsuario, d.primerNombre, ad.idActividad, act.nombreActividad, act.tipoActividad, act.fecha, ad.kilometraje, ad.altura, ad.duracion, ad.recorrido
from usuario d
inner join actividadDeportista ad
	on d.idUsuario = ad.idDeportista and d.idUsuario = iduser
	inner join actividad act
		on act.idActividad = ad.idActividad
		order by act.fecha desc
$$
Language sql



--Crear carrera
create or replace function crearCarrera (
	idorga int, nombcarr varchar, fechaevento timestamp, tipocarrera varchar, recorridocarrera xml, privacidad boolean, precio int, cuentabanc varchar) 
	returns void
	as
	$$
	insert into Carrera (idOrganizador, nombreCarrera, fechaCarrera, TipoActividad, recorrido, privada, costo, cuentaBancaria) 
	values (idorga, nombcarr, fechaevento, tipocarrera, recorridocarrera, privacidad, precio, cuentabanc);
$$
Language sql


--Agregar patrocinador a una carrera, valida que exista el patrocinador
create or replace function agregarPatrocinadorCarrera (idcarr int, nombrepatrocinador varchar)
	returns void
	as
	$$
	insert into PatrocinadoresCarrera (idCarrera, nombreComercial)
	select idcarr, nombrepatrocinador
	where exists (select 1 from Patrocinador where nombreComercial = nombrepatrocinador);
	$$
Language sql


--Modificar carrera
create or replace function modificarCarrera (
	idcarr int, idorg int, nomcarr varchar, fechcarr timestamp, tipact varchar, ruta xml, privacidad boolean, precio int, cuenta varchar)
	returns void
	as
	$$
	update Carrera set idOrganizador = idorg, nombreCarrera = nomcarr, fechaCarrera = fechcarr, TipoActividad = tipact,
	recorrido = ruta, privada = privacidad, costo = precio, cuentaBancaria = cuenta
	where idCarrera = idCarr
	$$
Language sql


--Eliminar una carrera
create or replace function eliminarCarreraID (idcarr int)
returns void
as
$$
Delete from carrera where idCarrera = idcarr;
Delete from carrerasGrupo where idCarrera = idcarr;
Delete from categoriaCarrera where idCarrera = idcarr;
Delete from solicitudesCarrera where idCarrera = idcarr;
Delete from usuariosCarrera where idCarrera = idcarr;
Delete from patrocinadoresCarrera where idCarrera = idcarr;
$$
Language sql


--Agregar solicitud de afiliacion a una carrera
create or replace function enviarSolicitudCarrera (idcarr int, iduser int, recib bytea)
returns void
as
$$
insert into solicitudesCarrera (idCarrera, idUsuario, recibo)
select idcarr, iduser, recib
where exists (select 1 from Carrera where idCarrera = idcarr);
$$
Language sql


--Aceptar solicitud de afiliacion a la carrera
create or replace function aceptarSolicitud (idcarr int, iduser int) returns void
as
$$
select agregarUsuarioCarrera (iduser,idcarr)
$$
Language sql


--Eliminar solicitud de Afiliacion
create or replace function eliminarSolicitud (iduser int, idcarr int)
returns void
as
$$
Delete from solicitudesCarrera where idCarrera = idcarr and idUsuario = iduser
$$
Language sql


--Carreras de los usuario
create or replace function CarrerasUsuarios () returns table (
	idUsuario int, primerNombre varchar, apellidos varchar, idCarrera int, nombreCarrera varchar, tipoActividad varchar,
	fechaCarrera timestamp, kilometraje varchar, altura varchar, duracion varchar, completitud boolean, recorrido xml)
as
$$
Select d.idUsuario, d.primerNombre, d.apellidos, uc.idCarrera, carr.nombreCarrera, carr.tipoActividad, carr.fechaCarrera,
		uc.kilometraje, uc.altura, uc.tiempoRegistrado,uc.completitud, uc.recorrido
from usuario d
inner join usuariosCarrera as uc
	on d.idUsuario = uc.idDeportista
	inner join Carrera as carr
		on carr.idCarrera = uc.idCarrera
		order by carr.fechaCarrera desc
$$
Language sql



--Crear un nuevo reto
create or replace function crearReto (
	idorga int, nombreto varchar,obj varchar, fechainc timestamp, fechafin timestamp, tipoact varchar,
	tiporet varchar, privacidad boolean) 
	returns void
	as
	$$
	insert into Reto (idOrganizador, nombreReto, objetivoReto, fechaInicio, fechaFinaliza, tipoActividad,
					  tipoReto, privada) 
	values (idorga, nombreto, obj, fechainc, fechaFin, tipoact, tiporet, privacidad);
	$$
Language sql


--Modificar Reto
create or replace function modificarReto (
	idret int, idorga int, nombreto varchar,obj varchar, fechainc timestamp, fechafin timestamp, tipoact varchar,
	tiporet varchar, privacidad boolean)
	returns void
	as
	$$
	update Reto set idOrganizador = idorga, nombrereto = nombreto, objetivoReto = obj, fechaInicio = fechainc,
	fechaFinaliza = fechafin, tipoActividad = tipoact, tiporeto = tiporet, privada = privacidad
	where idReto = idret
	$$
Language sql


--Eliminar Reto
create or replace function eliminarRetoID (idret int)
returns void
as
$$
Delete from Reto where idReto = idret;
Delete from RetosGrupo where idReto = idret;
Delete from usuariosReto where idReto = idret;
Delete from patrocinadoresReto where idReto = idret
$$
Language sql


--Retos de los usuarios
create or replace function RetosUsuarios () returns table (
	idUsuario int, primerNombre varchar, idReto int, nombreReto varchar, objetivoReto varchar, tipoActividad varchar,
	tipoReto varchar, fechaInicio timestamp, fechaFinaliza timestamp, kilometraje varchar, altura varchar,
	duracion varchar, completitud boolean, recorrido xml)
as
$$
Select d.idUsuario, d.primerNombre, ur.idReto, ret.nombreReto, ret.objetivoReto ,ret.tipoActividad, ret.tipoReto, 
ret.fechaInicio, ret.fechaFinaliza, ur.kilometraje, ur.altura, ur.duracion, ur.completitud, ur.recorrido
from usuario d
inner join usuariosReto as ur
	on d.idUsuario = ur.idDeportista
	inner join Reto as ret
		on ret.idReto = ur.idReto
		order by ret.fechaInicio desc
$$
Language sql


--Validacion de usuario y contrasena
create or replace function validacionDeUsuario (username varchar, clave varchar)
returns boolean
as
$$
select exists (select from Usuario where nombreUsuario = username and contrasena = clave)
$$
Language sql


-- Ver actividades de los amigos del deportista 
create or replace function actividadesAmigos (iduser int)
returns table (nombreAmigo varchar, nombreActividad varchar, tipoActividad varchar,
			   fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido xml)
as
$$
select 
primerNombre,nombreActividad,tipoActividad, fecha, kilometraje, altura, duracion, recorrido  from actividadesUsuarios() as actUser
inner join amigosUsuario as amigUser 
on amigUser.idAmigo = actUser.idUsuario
where iduser = amigUser.idDeportista
order by fecha desc
$$
Language sql


-- Ver retos de los amigos del deportista
create or replace function retosAmigos (iduser int)
returns table (nombreAmigo varchar, nombreReto varchar, tipoReto varchar, tipoActividad varchar,
			   fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido xml)
as
$$
select 
primerNombre, nombreReto, tipoReto, tipoActividad, fechaInicio, kilometraje, altura, duracion, recorrido  from retosUsuarios() as retUser
inner join amigosUsuario as amigUser 
on amigUser.idAmigo = retUser.idUsuario
where iduser = amigUser.idDeportista
order by fechaInicio desc
$$
Language sql


--Ver carreras de los amigos del deportista
create or replace function carrerasAmigos (iduser int)
returns table (nombreAmigo varchar, nombreCarrera varchar, tipoActividad varchar,
			   fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido xml)
as
$$
select 
primerNombre, nombreCarrera, tipoActividad, fechaCarrera, kilometraje, altura, duracion, recorrido  from carrerasUsuarios() as carrUser
inner join amigosUsuario as amigUser 
on amigUser.idAmigo = carrUser.idUsuario
where iduser = amigUser.idDeportista
order by fechaCarrera desc
$$
Language sql


--Ver TODAS las actividades, carreras y retos de amigos
create or replace function TodasActividadesAmigos(idusuario integer)
returns table (NombreAmigo varchar, NombreAct varchar,TipoActividad varchar, FechaActividad timestamp, mapa varchar, KM_Recorridos varchar)
as
$$
select nombreAmigo, nombreActividad, tipoActividad, fecha, cast(recorrido as varchar), kilometraje from actividadesAmigos(idusuario)
Union
select nombreAmigo, nombreCarrera, tipoActividad, fecha, cast(recorrido as varchar), kilometraje from carrerasAmigos(idusuario)
Union
select nombreAmigo, nombreReto, tipoActividad, fecha, cast(recorrido as varchar), kilometraje from retosAmigos(idusuario)
order by fecha desc
$$
Language sql


--Crear actividad 
create or replace function crearActividad (nombre varchar, fechact timestamp, tipo varchar) returns void
as
$$
insert into actividad (nombreactividad, fecha, tipoactividad) 
values (nombre, fechact, tipo);
$$
Language sql


--Modificar el nombre de una Actividad
create or replace function modificarActividad (idact integer, nombre varchar) returns void
as
$$
update actividad
set nombreActividad = nombre
where idact = idActividad
$$
Language sql

--Eliminar una actividad
create or replace function eliminarActividad (idact integer) returns void
as
$$
Delete from Actividad where idActividad = idact;
Delete from ActividadDeportista where idActividad = idact
$$
Language sql


--Actualizar reto
create or replace function actualizarReto (idret integer, distancianueva varchar, tiempo varchar) returns void
as
$$
Update usuariosReto set kilometraje = distancianueva, duracion = tiempo
where idReto = idret
$$
Language sql


--Agregar un usuario a una carrera
create or replace function agregarUsuarioCarrera (iddep integer, idcarr integer) returns void
as
$$
insert into usuariosCarrera (idDeportista,idcarrera) values (iddep, idcarr)
$$
Language sql


--Eliminar un usuario de una carrera
create or replace function eliminarUsuarioCarrera (iddep integer, idcarr integer) returns void
as
$$
Delete from usuariosCarrera where idDeportista = iddep and idCarrera = idcarr
$$
Language sql


--Agregar usuario a un reto
create or replace function agregarUsuarioReto(iddep integer, idret integer) returns void
as
$$
insert into usuariosReto (idDeportista,idReto) values (iddep, idret)
$$
Language sql


--Eliminar un usuario de un reto
create or replace function eliminarUsuarioReto (iddep integer, idret integer) returns void
as
$$
Delete from usuariosReto where idDeportista = iddep and idReto = idret
$$
Language sql


--Buscar retos por usuario
create or replace function retosPorUsuario (iduser integer) 
returns table (idUsuario int, NombreUsuario varchar, idReto integer, nombreReto varchar, objetivoReto varchar, tipoActividad varchar, tipoReto varchar,
			   fechaInicio timestamp, fechaFinaliza timestamp, kilometraje varchar, altura varchar, duracion varchar, completitud boolean, recorrido xml)
as
$$
select * from retosUsuarios()
where idUsuario = iduser
$$
Language sql


--Agregar un deportista a una actividad
create or replace function agregarUsuarioActividad (iddep integer, idactiv integer) returns void
as
$$
insert into actividadDeportista (idActividad,idDeportista) values (idactiv, iddep);
$$
Language sql


--Agregar un grupo
create or replace function crearGrupo (nombgrup varchar, idadmin integer) returns void
as
$$
insert into Grupo (nombre,idAdministrador) values (nombgrup, idadmin);
$$
Language sql


--Modificar un grupo
create or replace function modificarGrupo (idgroup int, nombgrup varchar, idadmin integer) returns void
as
$$
Update Grupo set nombre = nombgrup, idAdministrador = idadmin
where idGrupo = idgroup;
$$
Language sql


--Eliminar un grupo
create or replace function EliminarGrupo (idgroup int) returns void
as
$$
Delete from Grupo where idGrupo = idgroup;
Delete from UsuariosPorGrupo where idGrupo = idgroup;
Delete from RetosGrupo where idGrupo = idgroup;
Delete from CarrerasGrupo where idGrupo = idgroup;
$$
Language sql

--Ver usuarios del grupo
create or replace function usuariosGrupo (idgroup integer)
returns table (NombreGrupo varchar, idUsuario integer, NombreUsuario varchar, ApellidosUsuario varchar)
as
$$
Select gr.Nombre, u.idUsuario, u.PrimerNombre, u.Apellidos
	from usuario as u
	inner join usuariosPorGrupo as ug
	on u.idUsuario = ug.idUsuario
		inner join Grupo as gr
		on gr.idgrupo = ug.idgrupo
		where idgroup = gr.idGrupo
$$
Language sql


--Agregar usuario a un grupo
create or replace function agregarUsuarioGrupo (iduser integer, idgroup integer)
returns void
as
$$
insert into usuariosPorGrupo (idUsuario, idGrupo) values (iduser, idgroup);
$$
Language sql


--Eliminar usuario de un grupo
create or replace function eliminarUsuarioGrupo (iduser integer, idgroup integer)
returns void
as
$$
Delete from usuariosPorGrupo where idUsuario = iduser;
$$
Language sql

--Ver carreras por usuario
create or replace function buscarCarrerasPorUsuaio (iduser integer)
returns table (idUsuario int, primerNombre varchar, apellidos varchar, idCarrera int, nombreCarrera varchar, tipoActividad varchar,
				fechaCarrera timestamp, kilometraje varchar, altura varchar, duracion varchar, completitud boolean, recorrido xml)
as
$$
select * from carrerasUsuarios() 
where idUsuario = iduser;
$$
Language sql

select * from usuariosCarrera


--lista de participantes en la carrera
create or replace function participantesCarrera (idcarr integer)
returns table (nombreDeportista varchar, apellidoDeportista varchar, edad varchar, categoria varchar)
as
$$
select cu.primerNombre, cu.apellidos, u.fechaNacimiento, cc.categoria from carrerasUsuarios() as cu
inner join usuario as u
on u.primerNombre = cu.primerNombre and u.apellidos = cu.apellidos
inner join categoriaCarrera as cc
on cc.idCarrera = cu.idCarrera
where cu.idCarrera = idcarr
order by cc.categoria
$$
Language sql


--Ver posiciones de la carrera
create or replace function posicionesCarrera (idcarr integer)
returns table (NombreUsuario varchar, apellido varchar, edad varchar, tiempo varchar, categoria varchar)
as
$$
Select d.PrimerNombre, d.Apellidos, edad, uc.tiempoRegistrado from usuario as d
	Inner join usuariosCarrera as uc
	on d.idUsuario = uc.idDeportista
	where idcarr = uc.idCarrera
	order by uc.TiempoRegistrado asc
$$
Language sql

select * from participantesCarrera (1)

select extract (year from age((select fechaNacimiento from usuario where primerNombre = 'Mario')))




