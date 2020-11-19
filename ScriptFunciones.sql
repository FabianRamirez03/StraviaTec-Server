--Creacion de funciones

--*************************USUARIO*************************
/*Crear un usuario 
1 -> deportista
2 -> administrador
*/
CREATE OR REPLACE FUNCTION crearUsuario (username varchar, contra varchar, nombre varchar, apellido varchar, nacimiento date, pais varchar,
										 imagen varchar, administra integer) RETURNS void AS $$
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
insert into usuario (nombreUsuario,contrasena, primernombre, apellidos, fechaNacimiento, nacionalidad, foto, edad, categoria,administrador) 
values (username, contra,nombre,apellido,nacimiento,pais, imagen, age,categ, administra);
END;
$$ LANGUAGE plpgsql;


--Verifica si ya existe un nombre de usuario
create or replace function verificarUsuarioExiste (nombuser varchar) returns boolean
as
$$
select exists (Select 1 from usuario where nombreusuario = nombuser)
$$
Language sql


--Buscar usuario por ID
create or replace function buscarUsuarioId (iduser int) returns usuario
as
$$
Select * from usuario 
where idusuario = iduser;
$$
Language sql


--Buscar usuario por nombre, apellido o nombre de usuario
create or replace function buscaUsuarioSimilar (nombBusc varchar) RETURNS table (idUsuario integer, nombreUsuario varchar, primerNombre varchar, apellido varchar) 
AS $$
select idusuario, nombreusuario, primernombre, apellidos from usuario
where Upper (primernombre) like '%' ||Upper(nombBusc)|| '%' 
or Upper (apellidos) like '%' ||Upper(nombBusc)|| '%'
or Upper (nombreusuario) like '%' ||Upper(nombBusc)|| '%'
$$ LANGUAGE sql;


--Buscar usuario por nombre de usuario
create or replace function buscarUsuariousername (username varchar) returns usuario
as
$$
select * from usuario where Upper(nombreusuario) = Upper(username)
$$
Language sql


/* Indica si un usuario existe y si es administrador o deportista
0 -> no existe
1 -> deportista
2 -> administrador
*/
create or replace function tipoCuentaUsuario (username varchar, clave varchar) returns integer
as
$$
declare exist boolean := validarCuenta (username, clave);
declare tipo integer;
Begin
	If (exist) then tipo := (select administrador from usuario where nombreusuario = username and contrasena = clave);
	Else
		tipo := 0;
	end if;
	return tipo;
End;
$$
Language plpgsql


--Validacion de usuario y contrasena
create or replace function validarCuenta (username varchar, clave varchar)
returns boolean
as
$$
select exists (select from Usuario where nombreUsuario = username and contrasena = clave);
$$
Language sql


--Agregar amigos a un usuario por id
create or replace function agregarAmigo (iduser integer, idamigo integer) returns void
as
$$
insert into amigosUsuario (iddeportista, idamigo)
select iduser, idamigo
where exists (select 1 from usuario where idusuario = idamigo and iduser != idamigo); --Valida que el amigo exista antes de agregarlo
$$
Language sql


--Actualizar un usuario
create or replace function modificarUsuario (iduser int,username varchar, contra varchar, nombre varchar, apellido varchar, nacimiento date,pais varchar,imagen varchar)
returns void
as
$$
update usuario
set NombreUsuario = username, contrasena = contra, primernombre = nombre, apellidos = apellido, fechaNacimiento = nacimiento, nacionalidad = pais, foto = imagen
where idusuario = iduser;
$$
Language sql


--Eliminar un usuario por su ID
create or replace function eliminarUsuarioID (iduser int)
returns void
as
$$
Delete from actividadDeportista where iddeportista = iduser;
Delete from usuariosPorGrupo where idusuario = iduser;
Delete from amigosUsuario where iddeportista = iduser or idamigo = iduser;
Delete from carrera where idorganizador = iduser;
Delete from solicitudesCarrera where idusuario = iduser;
Delete from usuariosCarrera where iddeportista = iduser;
Delete from Reto where idorganizador = iduser;
Delete from usuariosReto where iddeportista = iduser;
Delete from grupo where idadministrador = iduser;
Delete from usuario where idusuario = iduser;
$$
Language sql

--*************************USUARIO*************************



--*************************GRUPO*************************

--Crear un grupo y asignar el administrador
create or replace function crearGrupo (nombgrup varchar, idadmin integer) returns void
as
$$
insert into Grupo (nombre,idadministrador) values (nombgrup, idadmin);
insert into usuariosPorGrupo (idusuario, idgrupo) values (idadmin, (select idgrupo from buscarGrupoNombre(nombgrup)) );
$$
Language sql


--Buscar grupo por nombre
create or replace function buscarGrupoNombre (nombregrupo varchar) returns grupo
as
$$
select * from grupo where nombre = nombregrupo;
$$
Language sql


--Modificar un grupo
create or replace function modificarGrupo (idgroup int, nombgrup varchar) returns void
as
$$
Update Grupo set nombre = nombgrup
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


--Ver todos los grupos disponibles que existen
create or replace function buscarGrupos() returns table (nombreGrupo varchar, nombreAdmin varchar, apellidoAdmin varchar)
as
$$
select gr.nombre, ad.primerNombre, ad.apellidos from grupo as gr
inner join usuario as ad on gr.idadministrador = ad.idusuario
$$
Language sql


--Ver los grupos a los que pertenece un usuario
create or replace function buscarGruposUsuario(idUser integer)returns table (nombreGrupo varchar)
as
$$
select gr.nombre from grupo as gr
inner join usuariosPorGrupo as ug on gr.idgrupo = ug.idgrupo and ug.idusuario = idUser;
$$
Language sql


--Agregar usuario a un grupo por el id del usuario y el id del grupo
create or replace function agregarUsuarioGrupo (iduser integer, idgroup integer)
returns void
as
$$
insert into usuariosPorGrupo (idusuario, idGrupo) values (iduser, idgroup);
$$
Language sql


--Eliminar usuario de un grupo
create or replace function eliminarUsuarioGrupo (iduser integer, idgroup integer)
returns void
as
$$
Delete from usuariosPorGrupo where idusuario = iduser and idgroup = idusuario;
$$
Language sql


--Ver usuarios de un grupo en especifico
create or replace function usuariosGrupo (idgroup integer)
returns table (NombreGrupo varchar, idusuario integer, NombreUsuario varchar, ApellidosUsuario varchar)
as
$$
Select gr.Nombre, u.idusuario, u.primernombre, u.Apellidos
	from usuario as u
	inner join usuariosPorGrupo as ug
	on u.idusuario = ug.idusuario
		inner join Grupo as gr
		on gr.idgrupo = ug.idgrupo
		where idgroup = gr.idGrupo
$$
Language sql


--Agregar carreras a un grupo
create or replace function agregarCarreraGrupo (idgroup integer, idcarr integer) returns void
as 
$$
insert into CarrerasGrupo (idGrupo, idCarrera) values (idgroup, idcarr);
$$
Language sql


--Agregar retos a un grupo
create or replace function agregarRetoGrupo (idgroup integer, idret integer) returns void
as 
$$
insert into RetosGrupo (idGrupo, idReto) values (idgroup, idret);
$$
Language sql

--Ver carreras de un grupo por su id
create or replace function buscarCarrerasGrupo(idgr integer) returns
table (idCarrera int, idOrg int, nombCar varchar, fecha timestamp, tipo varchar, costo integer, cuenta varchar, mapa varchar)
as
$$
select ca.idCarrera, ca.idOrganizador, ca.nombreCarrera, ca.fechaCarrera, ca.tipoActividad, ca.costo, ca.cuentaBancaria, ca.recorrido 
from carrera as ca
	inner join carrerasGrupo as cg on ca.idcarrera = cg.idcarrera
	where cg.idgrupo = idgr;
$$
Language sql


--Ver retos de un grupo por su id
create or replace function buscarRetosGrupo(idgr integer) returns
table (idReto int, idOrg int, nombRet varchar, tipoAct varchar, tipoRet varchar, fechaIn timestamp, fechaFin timestamp, objetivo varchar)
as
$$
select rt.idReto, rt.idOrganizador, rt.nombreReto, rt.tipoActividad, rt.tipoReto, rt.fechaInicio, rt.fechaFinaliza, rt.objetivoReto
from reto as rt
	inner join retosGrupo as rg on rt.idreto = rg.idreto
	where rg.idgrupo = idgr;
$$
Language sql
--*************************GRUPO*************************



--*************************ACTIVIDAD*************************

--Crear actividad 
create or replace function crearActividad (iddep integer, nombre varchar, kilom varchar, alt varchar, mapa varchar, tiempo varchar, fechaact timestamp, tipo varchar)
returns void
as
$$
insert into actividad (nombreactividad, fecha, tipoactividad)
values (nombre, fechaact, tipo);
insert into actividadDeportista (idactividad, iddeportista, kilometraje, altura, recorrido, duracion )
values ((select idactividad from buscaractividadnombre(nombre)), iddep, kilom, alt, mapa, tiempo);
$$
Language sql


--Modificar el nombre de una Actividad
create or replace function modificarActividad (idact integer, nombre varchar) returns void
as
$$
update actividad
set nombreActividad = nombre
where idact = idactividad
$$
Language sql


--Eliminar una actividad
create or replace function eliminarActividad (idact integer) returns void
as
$$
Delete from Actividad where idactividad = idact;
Delete from ActividadDeportista where idactividad = idact
$$
Language sql


--Actualizar datos de una actividad 
create or replace function actualizarDatosActividadUsuario(idact integer, iddep integer, dist varchar, alt varchar, tiempo varchar, mapa varchar) 
returns void
as
$$
Update actividadDeportista set kilometraje = dist, altura = alt, duracion = tiempo, recorrido = mapa
where idActividad = idact and idDeportista = iddep;
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
where idactividad = idact;
$$
Language sql


--Actividades de los usuarios
create or replace function actividadesUsuarios () returns table
(idusuario int, primernombre varchar, idactividad int, nombreActividad varchar, tipoactividad varchar,
 fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido varchar)
as
$$
Select d.idusuario, d.primernombre, ad.idactividad, act.nombreActividad, act.tipoactividad, act.fecha, ad.kilometraje, ad.altura, ad.duracion, ad.recorrido
from usuario d
inner join ActividadDeportista ad
	on d.idusuario = ad.iddeportista
	inner join actividad act
		on act.idactividad = ad.idactividad
		order by act.fecha desc
$$
Language sql


--Actividad por id de usuario
create or replace function actividadPorUsuario (iduser int) returns table 
(idusuario int, primernombre varchar, idactividad int, nombreActividad varchar,tipoactividad varchar,
 fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido varchar)
as
$$
Select d.idusuario, d.primernombre, ad.idactividad, act.nombreActividad, act.tipoactividad, act.fecha, ad.kilometraje, ad.altura, ad.duracion, ad.recorrido
from usuario d
inner join actividadDeportista ad
	on d.idusuario = ad.iddeportista and d.idusuario = iduser
	inner join actividad act
		on act.idactividad = ad.idactividad
		order by act.fecha desc
$$
Language sql


-- Ver actividades de los amigos del deportista 
create or replace function actividadesAmigos (iduser int)
returns table (nombreAmigo varchar, nombreActividad varchar, tipoactividad varchar,
			   fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido varchar)
as
$$
select 
primernombre,nombreActividad,tipoactividad, fecha, kilometraje, altura, duracion, recorrido  from actividadesUsuarios() as actUser
inner join amigosUsuario as amigUser 
on amigUser.idamigo = actUser.idusuario
where iduser = amigUser.iddeportista
order by fecha desc
$$
Language sql

--*************************ACTIVIDAD*************************



--*************************CARRERA*************************

--Crear carrera con id del organizador, nombre, fecha, tipo de actividad, categoria, mapa de la carrera, privacidad, precio, cuenta bancaria
create or replace function crearCarrera (
	idorga int, nombcarr varchar, fechaevento timestamp, tipocarrera varchar, recorridocarrera varchar,
	privacidad boolean, precio int, cuentabanc varchar) 
	returns void
	as
	$$
	insert into Carrera (idorganizador, nombrecarrera, fechacarrera, tipoactividad, recorrido, privada, costo, cuentabancaria) 
	values (idorga, nombcarr, fechaevento, tipocarrera, recorridocarrera, privacidad, precio, cuentabanc);
$$
Language sql


--Asignar categoria a la carrera
create or replace function asignarCategoriaCarrera (idcarr integer, categ varchar) returns void
as
$$
insert into categoriaCarrera (idcarrera, categoria)
select idcarr,categ
where exists (select 1 from carrera where idCarrera = idcarr);
$$
Language sql


--Modificar carrera
create or replace function modificarCarrera (
	idcarr int, idorg int, nomcarr varchar, fechcarr timestamp, tipact varchar, ruta varchar, privacidad boolean, precio int, cuenta varchar)
	returns void
	as
	$$
	update Carrera set idorganizador = idorg, nombrecarrera = nomcarr, fechacarrera = fechcarr, tipoactividad = tipact,
	recorrido = ruta, privada = privacidad, costo = precio, cuentaBancaria = cuenta
	where idcarrera = idcarr
	$$
Language sql


--Eliminar una carrera
create or replace function eliminarCarreraID (idcarr int)
returns void
as
$$
Delete from carrera where idcarrera = idcarr;
Delete from carrerasGrupo where idcarrera = idcarr;
Delete from categoriaCarrera where idcarrera = idcarr;
Delete from solicitudesCarrera where idcarrera = idcarr;
Delete from usuariosCarrera where idcarrera = idcarr;
Delete from patrocinadoresCarrera where idcarrera = idcarr;
$$
Language sql


--Obtener el id de una carrera a partir de su nombre
create or replace function obteneridcarrera(nombrecarr varchar) returns integer
as
$$
select idcarrera from carrera where nombrecarrera = nombrecarr;
$$
Language sql


--Buscar una carrera a partir de su id
create or replace function buscarCarreraId (idcarr integer) returns carrera
as
$$
select * from carrera where idCarrera = idcarr;
$$
Language sql


--Buscar la categoria de una carrera a partir de su id
create or replace function buscarCategoriaCarrera (idcarr integer) returns table(categoriasCarrera varchar)
as
$$
select categoria from categoriaCarrera where idCarrera = idcarr;
$$
Language sql


--Ver carreras creadas por un administrador
create or replace function verCarrerasAdmin (idadmin integer) returns table 
(NombreCarrera varchar, fecha timestamp, tipoActividad varchar, privacidad boolean, costo integer, cuenta varchar, mapa varchar)
as
$$
select nombreCarrera, fechaCarrera, tipoActividad, privada, costo, cuentabancaria, recorrido from  carrera 
where idOrganizador = idadmin ;
$$
Language sql


--Agregar patrocinador a una carrera, valida que exista el patrocinador
create or replace function agregarPatrocinadorCarrera (idcarr int, nombrepatrocinador varchar)
	returns void
	as
	$$
	insert into PatrocinadoresCarrera (idcarrera, nombreComercial)
	select idcarr, nombrepatrocinador
	where exists (select 1 from Patrocinador where nombreComercial = nombrepatrocinador);
	$$
Language sql


--Agregar solicitud de afiliacion a una carrera
create or replace function enviarSolicitudCarrera (idcarr int, iduser int, categoria varchar, recib varchar)
returns void
as
$$
insert into solicitudesCarrera (idcarrera, idusuario, categoriaCarrera, recibo)
select idcarr, iduser, categoria, recib
where exists (select 1 from Carrera where idcarrera = idcarr);
$$
Language sql


--Aceptar solicitud de afiliacion a la carrera
create or replace function aceptarSolicitud (idcarr int, iduser int, categoria varchar) returns void
as
$$
select agregarUsuarioCarrera (iduser,idcarr,categoria);
$$
Language sql


--Eliminar solicitud de Afiliacion
create or replace function eliminarSolicitud (iduser int, idcarr int, catCarr varchar)
returns void
as
$$
Delete from solicitudesCarrera where idcarrera = idcarr and idusuario = iduser and categoriaCarrera = catCarr;
$$
Language sql


--Agregar un usuario a una carrera por su id, el id de la carrera y la categoria de la carrera en al que va a participar
create or replace function agregarUsuarioCarrera (iddep integer, idcarr integer, catCarr varchar) returns void
as
$$
declare usAgregar varchar := (select categoria from buscarUsuarioId(iddep)); -- categoria del usuario
Begin
	if  exists (select 1 from categoriaCarrera where categoria = catCarr and idCarrera = idcarr) and catCarr = usAgregar
	and not exists (select * from usuariosCarrera where iddeportista = iddep and idcarrera = idcarr) then 
		insert into usuariosCarrera (iddeportista, idcarrera, categoriacompite) values (iddep, idcarr, catCarr);
	elseif catCarr = 'Elite' then
		insert into usuariosCarrera (iddeportista,idcarrera) values (iddep, idcarr);
	else
		raise notice 'No coincide la categoria';
	end if;
End
$$
Language plpgsql


--Eliminar un usuario de una carrera
create or replace function eliminarUsuarioCarrera (iddep integer, idcarr integer) returns void
as
$$
Delete from usuariosCarrera where iddeportista = iddep and idcarrera = idcarr;
$$
Language sql

--Crear un nuevo reto
create or replace function crearReto (
	idorga int, nombreto varchar,obj varchar, fechainc timestamp, fechafin timestamp, tipoact varchar,
	tiporet varchar, privacidad boolean) 
	returns void
	as
	$$
	insert into Reto (idorganizador, nombreReto, objetivoReto, fechainicio, fechafinaliza, tipoactividad,
					  tiporeto, privada) 
	values (idorga, nombreto, obj, fechainc, fechafin, tipoact, tiporet, privacidad);
	$$
Language sql

--Actualizar datos de un usuario en una carrera
create or replace function actualizarDatosCarreraUsuario 
(iduser int, idcarr int, distancia varchar, alt varchar, tiempo varchar, completado boolean, mapa varchar)
returns void
as
$$
update usuariosCarrera
set kilometraje = distancia, altura = alt, tiemporegistrado = tiempo, completitud = completado, recorrido = mapa
where idDeportista = iduser and idcarrera = idcarr;
$$
Language sql


--Ver las carreras de todos los usuario
create or replace function CarrerasUsuarios () returns table (
	idusuario int, primernombre varchar, apellidos varchar, categoria varchar, idcarrera int, nombrecarrera varchar, tipoactividad varchar,
	fechacarrera timestamp, kilometraje varchar, altura varchar, duracion varchar, completitud boolean, recorrido varchar)
as
$$
Select d.idusuario, d.primernombre, d.apellidos, uc.categoriaCompite,uc.idcarrera, carr.nombrecarrera, carr.tipoactividad, carr.fechacarrera,
		uc.kilometraje, uc.altura, uc.tiempoRegistrado,uc.completitud, uc.recorrido
from usuario d
inner join usuariosCarrera as uc
	on d.idusuario = uc.iddeportista
	inner join Carrera as carr
		on carr.idcarrera = uc.idcarrera
		order by carr.fechacarrera desc
$$
Language sql


--Ver carreras de los amigos del deportista por medio del ID
create or replace function carrerasAmigos (iduser int)
returns table (nombreAmigo varchar, nombrecarrera varchar, tipoactividad varchar,
			   fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido varchar)
as
$$
select 
primernombre, nombrecarrera, tipoactividad, fechacarrera, kilometraje, altura, duracion, recorrido  from carrerasUsuarios() as carrUser
inner join amigosUsuario as amigUser 
on amigUser.idamigo = carrUser.idusuario
where iduser = amigUser.iddeportista
order by fechacarrera desc
$$
Language sql


--Ver carreras por ID del usuario
create or replace function buscarCarrerasPorUsuaio (iduser integer)
returns table (idusuario int, primernombre varchar, apellidos varchar, categoriaCompite varchar, idcarrera int, nombrecarrera varchar, tipoactividad varchar,
				fechacarrera timestamp, kilometraje varchar, altura varchar, duracion varchar, completitud boolean, recorrido varchar)
as
$$
select * from carrerasUsuarios() 
where idusuario = iduser;
$$
Language sql


--lista de participantes en la carrera
create or replace function participantesCarrera (idcarr integer)
returns table (nombreDeportista varchar, apellidoDeportista varchar, edad varchar, categoriaCompite varchar)
as
$$
select cu.primernombre, cu.apellidos, u.edad, cu.categoria from carrerasUsuarios() as cu
inner join usuario as u
on u.primernombre = cu.primernombre and u.apellidos = cu.apellidos
where cu.idcarrera = idcarr
order by cu.categoria
$$
Language sql


--Ver posiciones de la carrera por medio del ID
create or replace function posicionesCarrera (idcarr integer)
returns table (NombreDeportista varchar, apellidoDeportista varchar, edad varchar, categoriadeportista varchar, tiempo varchar)
as
$$
Select cu.primernombre, cu.Apellidos, u.edad, cu.categoria, cu.duracion from carrerasUsuarios() as cu
	inner join usuario as u
	on u.idusuario = cu.idusuario
	where idcarr = cu.idcarrera
	order by cu.categoria,cu.duracion asc
$$
Language sql

--*************************CARRERA*************************



--*************************RETO*************************

--Crear un nuevo reto
create or replace function crearReto (
	idorga int, nombreto varchar,obj varchar, fechainc timestamp, fechafin timestamp, tipoact varchar,
	tiporet varchar, privacidad boolean) 
	returns void
	as
	$$
	insert into Reto (idorganizador, nombreReto, objetivoReto, fechainicio, fechafinaliza, tipoactividad,
					  tiporeto, privada) 
	values (idorga, nombreto, obj, fechainc, fechafin, tipoact, tiporet, privacidad);
	$$
Language sql


--Modificar Reto
create or replace function modificarReto (
	idret int, idorga int, nombreto varchar,obj varchar, fechainc timestamp, fechafin timestamp, tipoact varchar,
	tiporet varchar, privacidad boolean)
	returns void
	as
	$$
	update Reto set idorganizador = idorga, nombreReto = nombreto, objetivoReto = obj, fechainicio = fechainc,
	fechafinaliza = fechafin, tipoactividad = tipoact, tiporeto = tiporet, privada = privacidad
	where idreto = idret
	$$
Language sql


--Eliminar Reto
create or replace function eliminarRetoID (idret int)
returns void
as
$$
Delete from Reto where idreto = idret;
Delete from RetosGrupo where idreto = idret;
Delete from usuariosReto where idreto = idret;
Delete from patrocinadoresReto where idreto = idret
$$
Language sql


--Ver retos de un administrador por el id del organizador
create or replace function verRetosAdmin (idadmin integer) returns table 
(idReto integer, NombreReto varchar, tipoActividad varchar, tipoReto varchar, objetivo varchar,
 privacidad boolean,  fechaInicio timestamp, fechaFinaliza timestamp)
as
$$
select idReto, nombreReto,tipoActividad, tipoReto, objetivoReto, privada, fechaInicio, fechaFinaliza  from  reto 
where idOrganizador = idadmin ;
$$
Language sql


--Agregar patrocinador a una reto, valida que exista el patrocinador
create or replace function agregarPatrocinadorReto (idret int, nombrepatrocinador varchar)
	returns void
	as
	$$
	insert into PatrocinadoresReto (idReto, nombreComercial)
	select idret, nombrepatrocinador
	where exists (select 1 from Patrocinador where nombreComercial = nombrepatrocinador);
	$$
Language sql


--Agregar usuario a un reto
create or replace function agregarUsuarioReto(iddep integer, idret integer) returns void
as
$$
insert into usuariosReto (iddeportista,idreto) values (iddep, idret)
$$
Language sql


--Eliminar un usuario de un reto
create or replace function eliminarUsuarioReto (iddep integer, idret integer) returns void
as
$$
Delete from usuariosReto where iddeportista = iddep and idreto = idret
$$
Language sql


--Buscar retos por ID del usuario
create or replace function retosPorUsuario (iduser integer) 
returns table (idusuario int, NombreUsuario varchar, idreto integer, nombreReto varchar, objetivoReto varchar, tipoactividad varchar, tiporeto varchar,
			   fechainicio timestamp, fechafinaliza timestamp, kilometraje varchar, altura varchar, duracion varchar, completitud boolean, recorrido varchar)
as
$$
select * from retosUsuarios()
where idusuario = iduser
$$
Language sql


--Actualizar los datos del reto segun del usuario
create or replace function actualizarDatosRetoUsuario 
(idret integer, iduser integer,tiempo varchar, distancia varchar, alt varchar, completado boolean, mapa varchar ) returns void
as
$$
Update usuariosReto set kilometraje = distancia, duracion = tiempo, altura = alt, completitud = completado, recorrido = mapa
where idreto = idret and iddeportista = iduser;
$$
Language sql


--Retos de todos los usuarios
create or replace function RetosUsuarios () returns table (
	idusuario int, primernombre varchar, idreto int, nombreReto varchar, objetivoReto varchar, tipoactividad varchar,
	tiporeto varchar, fechainicio timestamp, fechafinaliza timestamp, kilometraje varchar, altura varchar,
	duracion varchar, completitud boolean, recorrido varchar)
as
$$
Select d.idusuario, d.primernombre, ur.idreto, ret.nombreReto, ret.objetivoReto ,ret.tipoactividad, ret.tiporeto, 
ret.fechainicio, ret.fechafinaliza, ur.kilometraje, ur.altura, ur.duracion, ur.completitud, ur.recorrido
from usuario d
inner join usuariosReto as ur
	on d.idusuario = ur.iddeportista
	inner join Reto as ret
		on ret.idreto = ur.idreto
		order by ret.fechainicio desc
$$
Language sql


-- Ver retos de los amigos del deportista
create or replace function retosAmigos (iduser int)
returns table (nombreAmigo varchar, nombreReto varchar, tiporeto varchar, tipoactividad varchar,
			   fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido varchar)
as
$$
select 
primernombre, nombreReto, tiporeto, tipoactividad, fechainicio, kilometraje, altura, duracion, recorrido  from retosUsuarios() as retUser
inner join amigosUsuario as amigUser 
on amigUser.idamigo = retUser.idusuario
where iduser = amigUser.iddeportista
order by fechainicio desc
$$
Language sql
--*************************Reto*************************



--*************************GENERAL*************************
--Ver TODAS las actividades, carreras y retos de amigos
create or replace function TodasActividadesAmigos(idusuario integer)
returns table (NombreAmigo varchar, NombreAct varchar,tipoactividad varchar, fechaactividad timestamp, mapa varchar, KM_Recorridos varchar)
as
$$
select nombreAmigo, nombreActividad, tipoactividad, fecha, cast(recorrido as varchar), kilometraje from actividadesAmigos(idusuario)
Union
select nombreAmigo, nombrecarrera, tipoactividad, fecha, cast(recorrido as varchar), kilometraje from carrerasAmigos(idusuario)
Union
select nombreAmigo, nombreReto, tipoactividad, fecha, cast(recorrido as varchar), kilometraje from retosAmigos(idusuario)
order by fecha desc
$$
Language sql
--*************************GENERAL*************************
