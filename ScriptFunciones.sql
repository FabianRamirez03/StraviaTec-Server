--Creacion de funciones

--Crear un usuario
create or replace function crearUsuario (userName varchar, contra varchar, nombre varchar, apellido varchar, nacimiento date,pais  varchar) returns void
as
$$
insert into usuario (nombreUsuario,contrasena, primerNombre, apellidos, fechaNacimiento, nacionalidad) 
values (userName, contra,nombre,apellido,nacimiento,pais);
$$
Language sql


--Buscar usuario por ID
create or replace function buscarUsuarioId (int) returns usuario
as
$$
Select * from usuario
where idUsuario = $1;
$$
Language sql


--Buscar usuario por Nombre
create or replace function buscarUsuarioNombre (nombre varchar) returns usuario
as
$$
Select * from usuario
where primerNombre = nombre;
$$
Language sql


--Buscar usuario por nombre y apellido
create or replace function buscarUsuarioNombreApellido (nombre varchar, apellido varchar) returns usuario
as
$$
Select * from usuario
where primerNombre = nombre and apellidos = apellido;
$$
Language sql


--Agregar amigos a un usuario por id
create or replace function agregarAmigo (idUser integer, idAmigo integer) returns void
as
$$
insert into amigosUsuario (idDeportista, idAmigo)
select idUser, idAmigo
where exists (select 1 from usuario where idUsuario = idAmigo and idUser != idAmigo); --Valida que el amigo exista antes de agregarlo
$$
Language sql


--Actualizar un usuario
create or replace function modificarUsuario (idUser int,userName varchar, contra varchar, nombre varchar, apellido varchar, nacimiento date,pais varchar)
returns void
as
$$
update usuario
set NombreUsuario = userName, contrasena = contra, primerNombre = nombre, apellidos = apellido, fechaNacimiento = nacimiento, nacionalidad = pais
where idUsuario = idUser;
$$
Language sql


--Eliminar un usuario por su ID
create or replace function eliminarUsuarioID (idUser int)
returns void
as
$$
Delete from usuario where idUsuario = idUser
$$
Language sql

--Eliminar un usuario por su nombre de usuario
create or replace function eliminarUsuarioNombre (userName varchar)
returns void
as
$$
Delete from usuario where nombreUsuario = userName
$$
Language sql


--Buscar actividad por nombre
create or replace function buscarActividadNombre (nombre varchar) returns actividad
as
$$
Select * from actividad
where nombreActividad = nombre;
$$
Language sql


--Buscar actividad por id
create or replace function buscarActividadID (idAct int) returns actividad
as
$$
Select * from actividad
where idActividad = idAct;
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
create or replace function actividadPorUsuario (idUser int) returns table (
	idUsuario int, primerNombre varchar, idActividad int, nombreActividad varchar,tipoActividad varchar, fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido xml)
as
$$
Select d.idUsuario, d.primerNombre, ad.idActividad, act.nombreActividad, act.tipoActividad, act.fecha, ad.kilometraje, ad.altura, ad.duracion, ad.recorrido
from usuario d
inner join actividadDeportista ad
	on d.idUsuario = ad.idDeportista and d.idUsuario = idUser
	inner join actividad act
		on act.idActividad = ad.idActividad
		order by act.fecha desc
$$
Language sql



--Crear carrera
create or replace function crearCarrera (
	idOrga int, nombCarr varchar, fechaEvento timestamp, tipoCarrera varchar, recorridoCarrera xml, privacidad boolean, precio int, cuentaBanc varchar) 
	returns void
	as
	$$
	insert into Carrera (idOrganizador, nombreCarrera, fechaCarrera, TipoActividad, recorrido, privada, costo, cuentaBancaria) 
	values (idOrga, nombCarr, fechaEvento, tipoCarrera, recorridoCarrera, privacidad, precio, cuentaBanc);
$$
Language sql


--Agregar patrocinador a una carrera, valida que exista el patrocinador
create or replace function agregarPatrocinadorCarrera (idCarr int, nombrePatrocinador varchar)
	returns void
	as
	$$
	insert into PatrocinadoresCarrera (idCarrera, nombreComercial)
	select idCarr, nombrePatrocinador
	where exists (select 1 from Patrocinador where nombreComercial = nombrePatrocinador);
	$$
Language sql


--Modificar carrera
create or replace function modificarCarrera (
	idCarr int, idOrg int, nomCarr varchar, fechCarr timestamp, tipAct varchar, ruta xml, privacidad boolean, precio int, cuenta varchar)
	returns void
	as
	$$
	update Carrera set idOrganizador = idOrg, nombreCarrera = nomCarr, fechaCarrera = fechCarr, TipoActividad = tipAct,
	recorrido = ruta, privada = privacidad, costo = precio, cuentaBancaria = cuenta
	where idCarrera = idCarr
	$$
Language sql


--Eliminar una carrera
create or replace function eliminarCarreraID (idCarr int)
returns void
as
$$
Delete from carrera where idCarrera = idCarr
$$
Language sql


--Agregar solicitud de afiliacion a una carrera
create or replace function enviarSolicitudCarrera (idCarr int, idUser int, recib bytea)
returns void
as
$$
insert into solicitudesCarrera (idCarrera, idUsuario, recibo)
select idCarr, idUser, recib
where exists (select 1 from Carrera where idCarrera = idCarr);
$$
Language sql


--Eliminar solicitud de Afiliacion
create or replace function eliminarSolicitud (idUser int, idCarr int)
returns void
as
$$
Delete from solicitudesCarrera where idCarrera = idCarr and idUsuario = idUser
$$
Language sql


--Carreras de los usuario
create or replace function CarrerasUsuarios () returns table (
	idUsuario int, primerNombre varchar, idCarrera int, nombreCarrera varchar, tipoActividad varchar,
	fechaCarrera timestamp, kilometraje varchar, altura varchar, duracion varchar, completitud boolean, recorrido xml)
as
$$
Select d.idUsuario, d.primerNombre, uc.idCarrera, carr.nombreCarrera, carr.tipoActividad, carr.fechaCarrera,
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
	idOrga int, nombReto varchar,obj varchar, fechaInc timestamp, fechaFin timestamp, tipoAct varchar,
	tipoRet varchar, privacidad boolean) 
	returns void
	as
	$$
	insert into Reto (idOrganizador, nombreReto, objetivoReto, fechaInicio, fechaFinaliza, tipoActividad,
					  tipoReto, privada) 
	values (idOrga, nombReto, obj, fechaInc, fechaFin, tipoAct, tipoRet, privacidad);
	$$
Language sql


--Modificar Reto
create or replace function modificarReto (
	idRet int, idOrga int, nombReto varchar,obj varchar, fechaInc timestamp, fechaFin timestamp, tipoAct varchar,
	tipoRet varchar, privacidad boolean)
	returns void
	as
	$$
	update Reto set idOrganizador = idOrga, nombreReto = nombReto, objetivoReto = obj, fechaInicio = fechaInc,
	fechaFinaliza = fechaFin, tipoActividad = tipoAct, tipoReto = tipoRet, privada = privacidad
	where idReto = idRet
	$$
Language sql


--Eliminar Reto
create or replace function eliminarRetoID (idRet int)
returns void
as
$$
Delete from Reto where idReto = idRet
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
create or replace function validacionDeUsuario (userName varchar, clave varchar)
returns boolean
as
$$
select exists (select from Usuario where nombreUsuario = userName and contrasena = clave)
$$
Language sql


-- Ver actividades de los amigos del deportista 
create or replace function actividadesAmigos (idUser int)
returns table (nombreAmigo varchar, nombreActividad varchar, tipoActividad varchar,
			   fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido xml)
as
$$
select 
primerNombre,nombreActividad,tipoActividad, fecha, kilometraje, altura, duracion, recorrido  from actividadesUsuarios() as actUser
inner join amigosUsuario as amigUser 
on amigUser.idAmigo = actUser.idUsuario
where idUser = amigUser.idDeportista
order by fecha desc
$$
Language sql


-- Ver retos de los amigos del deportista
create or replace function retosAmigos (idUser int)
returns table (nombreAmigo varchar, nombreReto varchar, tipoReto varchar, tipoActividad varchar,
			   fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido xml)
as
$$
select 
primerNombre, nombreReto, tipoReto, tipoActividad, fechaInicio, kilometraje, altura, duracion, recorrido  from retosUsuarios() as retUser
inner join amigosUsuario as amigUser 
on amigUser.idAmigo = retUser.idUsuario
where idUser = amigUser.idDeportista
order by fechaInicio desc
$$
Language sql


--Ver carreras de los amigos del deportista
create or replace function carrerasAmigos (idUser int)
returns table (nombreAmigo varchar, nombreCarrera varchar, tipoActividad varchar,
			   fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido xml)
as
$$
select 
primerNombre, nombreCarrera, tipoActividad, fechaCarrera, kilometraje, altura, duracion, recorrido  from carrerasUsuarios() as carrUser
inner join amigosUsuario as amigUser 
on amigUser.idAmigo = carrUser.idUsuario
where idUser = amigUser.idDeportista
order by fechaCarrera desc
$$
Language sql


--Ver TODAS las actividades, carreras y retos de amigos
create or replace function TodasActividadesAmigos(idUsuario integer)
returns table (NombreAmigo varchar, NombreAct varchar,TipoActividad varchar, FechaActividad timestamp, mapa varchar, KM_Recorridos varchar)
as
$$
select nombreAmigo, nombreActividad, tipoActividad, fecha, cast(recorrido as varchar), kilometraje from actividadesAmigos(idUsuario)
Union
select nombreAmigo, nombreCarrera, tipoActividad, fecha, cast(recorrido as varchar), kilometraje from carrerasAmigos(idUsuario)
Union
select nombreAmigo, nombreReto, tipoActividad, fecha, cast(recorrido as varchar), kilometraje from retosAmigos(idUsuario)
order by fecha desc
$$
Language sql













--Crear actividad 
create or replace function crearActividad (nombre varchar, fechaAct timestamp, tipo varchar) returns void
as
$$
insert into actividad (nombreactividad, fecha, tipoactividad) 
values (nombre, fechaAct, tipo);
$$
Language sql


--Asociar deportista con actividad
create or replace function UnirActividadDeportista (idDep integer, idActiv integer) returns void
as
$$
insert into actividadDeportista (idActividad,idDeportista) values (idActiv, idDep);
$$
Language sql

--Buscar Actividades por deportista
create or replace function BuscActivDeport (idDep integer, idActiv integer) returns void
as
$$
insert into actividadDeportista (idActividad,idDeportista) values (idActiv, idDep);
$$
Language sql



