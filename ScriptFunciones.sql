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
create or replace function buscarActividadNombre (idAct int) returns actividad
as
$$
Select * from actividad
where idActividad = idAct;
$$
Language sql


--Actividades de los usuarios
create or replace function actividadesUsuario () returns table (
	idUsuario int, primerNombre varchar, idActividad int, nombreActividad varchar, fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido xml)
as
$$
Select d.idUsuario, d.primerNombre, ad.idActividad, act.nombreActividad, act.fecha, ad.kilometraje, ad.altura, ad.duracion, ad.recorrido
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
	idUsuario int, primerNombre varchar, idActividad int, nombreActividad varchar, fecha timestamp, kilometraje varchar, altura varchar, duracion varchar, recorrido xml)
as
$$
Select d.idUsuario, d.primerNombre, ad.idActividad, act.nombreActividad, act.fecha, ad.kilometraje, ad.altura, ad.duracion, ad.recorrido
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


--Agregar patrocinador a una carrera
create or replace function agregarPatrocinadorCarrera (idCarr int, nombrePatrocinador varchar)
	returns void
	as
$$
	insert into PatrocinadoresCarrera (idCarrera, nombreComercial)
	values (idCarr, nombrePatrocinador);

$$
Language sql
select * from patrocinador
select agregarPatrocinadorCarrera (5,'Gatorade')






















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



--Agregar amigos a un usuario por id
create or replace function agregarAmigo (idUser integer, idAmigo integer) returns void
as
$$
Select * from actividad
where nombreActividad = nombre;
$$
Language sql
