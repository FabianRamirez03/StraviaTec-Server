--Creacion de funciones

--Crear un usuario
CREATE OR REPLACE FUNCTION crearUsuario (userName varchar, contra varchar, nombre varchar, apellido varchar, nacimiento date, pais varchar, imagen bytea) RETURNS void AS $$
DECLARE
	age INTEGER;
	categ varchar;
BEGIN
  age := extract (year from age(nacimiento));--Calcula la edad y asigna la categoria al usuario
	if age < 15 then categ = 'Junior';
	elseif age > 15 and age < 23 then categ = 'Sub-23';
	elseif age > 24 and age < 30 then categ = 'Open';
	elseif age > 30 and age < 40 then categ = 'Master-A';
	elseif age > 41 and age < 50 then categ = 'Master-B';
	elseif age > 51 then categ = 'Master-C';
end if;
insert into usuario (nombreUsuario,contrasena, primerNombre, apellidos, fechaNacimiento, nacionalidad, foto, edad, categoria) 
values (userName, contra,nombre,apellido,nacimiento,pais, imagen, age,categ);
END;
$$ LANGUAGE plpgsql;


--Asignar categoria segun la edad
create or replace function asignarCategoriaUsuario(userName varchar)
as
$$
declare 
	deportista Usuario  := buscarUsuarioUserName(userName);
begin
	if deportista.usuario.edad <= 15 then 
end
$$
Language sql
select * from usuario


--Buscar usuario por ID
create or replace function buscarUsuarioId (idUser int) returns usuario
as
$$
Select * from usuario
where idUsuario = idUser;
$$
Language sql

--Buscar usuario por User Name
create or replace function buscarUsuarioUserName (userName varchar) returns usuario
as
$$
Select * from usuario
where nombreUsuario = userName;
$$
Language sql


--Buscar usuario por Nombre
create or replace function buscarUsuarioNombre (nombre varchar) returns usuario
as
$$
Select * from usuario
where Upper (primerNombre) ~* Upper (nombre);
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
create or replace function agregarAmigo (idUser integer, idAmigo integer) returns void
as
$$
insert into amigosUsuario (idDeportista, idAmigo)
select idUser, idAmigo
where exists (select 1 from usuario where idUsuario = idAmigo and idUser != idAmigo); --Valida que el amigo exista antes de agregarlo
$$
Language sql


--Actualizar un usuario
create or replace function modificarUsuario (idUser int,userName varchar, contra varchar, nombre varchar, apellido varchar, nacimiento date,pais varchar,imagen bytea)
returns void
as
$$
update usuario
set NombreUsuario = userName, contrasena = contra, primerNombre = nombre, apellidos = apellido, fechaNacimiento = nacimiento, nacionalidad = pais, foto = imagen
where idUsuario = idUser;
$$
Language sql


--Eliminar un usuario por su ID
create or replace function eliminarUsuarioID (idUser int)
returns void
as
$$
Delete from usuario where idUsuario = idUser;
Delete from actividadDeportista where idDeportista = idUser;
Delete from usuariosPorGrupo where idUsuario = idUser;
Delete from amigosUsuario where idDeportista = idUser or idAmigo = idUser;
Delete from carrera where idOrganizador = idUser;
Delete from solicitudesCarrera where idUsuario = idUser;
Delete from usuariosCarrera where idDeportista = idUser;
Delete from Reto where idOrganizador = idUser;
Delete from usuariosReto where idDeportista = idUser;
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
	idOrga int, nombCarr varchar, fechaEvento timestamp, tipoCarrera varchar, carreraCategoria varchar, recorridoCarrera xml, privacidad boolean, precio int, cuentaBanc varchar) 
	returns void
	as
	$$
	insert into Carrera (idOrganizador, nombreCarrera, fechaCarrera, TipoActividad, recorrido, privada, costo, cuentaBancaria) 
	values (idOrga, nombCarr, fechaEvento, tipoCarrera, recorridoCarrera, privacidad, precio, cuentaBanc);
	insert into categoriaCarrera (select obteneridCarrera(nombCarr), carreraCategoria)
$$
Language sql


--Obtener el id de una carrera a partir de su nombre
create or replace function obteneridCarrera(nombreCarr varchar) returns integer
as
$$
select idCarrera from carrera where nombreCarrera = nombreCarr;
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
Delete from carrera where idCarrera = idCarr;
Delete from carrerasGrupo where idCarrera = idCarr;
Delete from categoriaCarrera where idCarrera = idCarr;
Delete from solicitudesCarrera where idCarrera = idCarr;
Delete from usuariosCarrera where idCarrera = idCarr;
Delete from patrocinadoresCarrera where idCarrera = idCarr;
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


--Aceptar solicitud de afiliacion a la carrera
create or replace function aceptarSolicitud (idCarr int, idUser int) returns void
as
$$
select agregarUsuarioCarrera (idUser,idCarr)
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
	idUsuario int, primerNombre varchar, apellidos varchar, categoriaDeportista varchar, idCarrera int, nombreCarrera varchar, tipoActividad varchar,
	fechaCarrera timestamp, kilometraje varchar, altura varchar, duracion varchar, completitud boolean, recorrido xml)
as
$$
Select d.idUsuario, d.primerNombre, d.apellidos, d.categoria,uc.idCarrera, carr.nombreCarrera, carr.tipoActividad, carr.fechaCarrera,
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
Delete from Reto where idReto = idRet;
Delete from RetosGrupo where idReto = idRet;
Delete from usuariosReto where idReto = idRet;
Delete from patrocinadoresReto where idReto = idRet
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


--Modificar el nombre de una Actividad
create or replace function modificarActividad (idAct integer, nombre varchar) returns void
as
$$
update actividad
set nombreActividad = nombre
where idAct = idActividad
$$
Language sql

--Eliminar una actividad
create or replace function eliminarActividad (idAct integer) returns void
as
$$
Delete from Actividad where idActividad = idAct;
Delete from ActividadDeportista where idActividad = idAct
$$
Language sql


--Actualizar reto
create or replace function actualizarReto (idRet integer, distanciaNueva varchar, tiempo varchar) returns void
as
$$
Update usuariosReto set kilometraje = distanciaNueva, duracion = tiempo
where idReto = idRet
$$
Language sql


--Agregar un usuario a una carrera
create or replace function agregarUsuarioCarrera (idDep integer, idCarr integer) returns void
as
$$
insert into usuariosCarrera (idDeportista,idcarrera) values (idDep, idCarr)
$$
Language sql


--Eliminar un usuario de una carrera
create or replace function eliminarUsuarioCarrera (idDep integer, idCarr integer) returns void
as
$$
Delete from usuariosCarrera where idDeportista = idDep and idCarrera = idCarr
$$
Language sql


--Agregar usuario a un reto
create or replace function agregarUsuarioReto(idDep integer, idRet integer) returns void
as
$$
insert into usuariosReto (idDeportista,idReto) values (idDep, idRet)
$$
Language sql


--Eliminar un usuario de un reto
create or replace function eliminarUsuarioReto (idDep integer, idRet integer) returns void
as
$$
Delete from usuariosReto where idDeportista = idDep and idReto = idRet
$$
Language sql


--Buscar retos por usuario
create or replace function retosPorUsuario (idUser integer) 
returns table (idUsuario int, NombreUsuario varchar, idReto integer, nombreReto varchar, objetivoReto varchar, tipoActividad varchar, tipoReto varchar,
			   fechaInicio timestamp, fechaFinaliza timestamp, kilometraje varchar, altura varchar, duracion varchar, completitud boolean, recorrido xml)
as
$$
select * from retosUsuarios()
where idUsuario = idUser
$$
Language sql


--Agregar un deportista a una actividad
create or replace function agregarUsuarioActividad (idDep integer, idActiv integer) returns void
as
$$
insert into actividadDeportista (idActividad,idDeportista) values (idActiv, idDep);
$$
Language sql


--Agregar un grupo
create or replace function crearGrupo (nombGrup varchar, idAdmin integer) returns void
as
$$
insert into Grupo (nombre,idAdministrador) values (nombGrup, idAdmin);
$$
Language sql


--Modificar un grupo
create or replace function modificarGrupo (idGroup int, nombGrup varchar, idAdmin integer) returns void
as
$$
Update Grupo set nombre = nombGrup, idAdministrador = idAdmin
where idGrupo = idGroup;
$$
Language sql


--Eliminar un grupo
create or replace function EliminarGrupo (idGroup int) returns void
as
$$
Delete from Grupo where idGrupo = idGroup;
Delete from UsuariosPorGrupo where idGrupo = idGroup;
Delete from RetosGrupo where idGrupo = idGroup;
Delete from CarrerasGrupo where idGrupo = idGroup;
$$
Language sql

--Ver usuarios del grupo
create or replace function usuariosGrupo (idGroup integer)
returns table (NombreGrupo varchar, idUsuario integer, NombreUsuario varchar, ApellidosUsuario varchar)
as
$$
Select gr.Nombre, u.idUsuario, u.PrimerNombre, u.Apellidos
	from usuario as u
	inner join usuariosPorGrupo as ug
	on u.idUsuario = ug.idUsuario
		inner join Grupo as gr
		on gr.idgrupo = ug.idgrupo
		where idGroup = gr.idGrupo
$$
Language sql


--Agregar usuario a un grupo
create or replace function agregarUsuarioGrupo (idUser integer, idGroup integer)
returns void
as
$$
insert into usuariosPorGrupo (idUsuario, idGrupo) values (idUser, idGroup);
$$
Language sql


--Eliminar usuario de un grupo
create or replace function eliminarUsuarioGrupo (idUser integer, idGroup integer)
returns void
as
$$
Delete from usuariosPorGrupo where idUsuario = idUser and idGroup = idUsuario;
$$
Language sql

--Ver carreras por usuario
create or replace function buscarCarrerasPorUsuaio (idUser integer)
returns table (idUsuario int, primerNombre varchar, apellidos varchar, idCarrera int, nombreCarrera varchar, tipoActividad varchar,
				fechaCarrera timestamp, kilometraje varchar, altura varchar, duracion varchar, completitud boolean, recorrido xml)
as
$$
select * from carrerasUsuarios() 
where idUsuario = idUser;
$$
Language sql


--lista de participantes en la carrera
create or replace function participantesCarrera (idCarr integer)
returns table (nombreDeportista varchar, apellidoDeportista varchar, edad varchar, categoriaDeportista varchar)
as
$$
select cu.primerNombre, cu.apellidos, u.edad, cu.categoriaDeportista from carrerasUsuarios() as cu
inner join usuario as u
on u.primerNombre = cu.primerNombre and u.apellidos = cu.apellidos
inner join categoriaCarrera as cc
on cc.idCarrera = cu.idCarrera
where cu.idCarrera = idCarr
order by cu.categoriaDeportista
$$
Language sql
select * from carrerasUsuarios()

--Ver posiciones de la carrera
create or replace function posicionesCarrera (idCarr integer)
returns table (NombreDeportista varchar, apellidoDeportista varchar, edad varchar, categoriaDeportista varchar, tiempo varchar)
as
$$
Select cu.PrimerNombre, cu.Apellidos, u.edad, cu.categoriaDeportista, cu.duracion from carrerasUsuarios() as cu
	inner join usuario as u
	on u.idUsuario = cu.idUsuario
	where idCarr = cu.idCarrera
	order by cu.duracion asc
$$
Language sql

