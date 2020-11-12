--Creacion de funciones

--Crear un usuario
create or replace function crearUsuario (usuario varchar, contra varchar, nombre varchar, apellido varchar, nacimiento date,pais  varchar) returns void
as
$$
insert into usuario (nombreUsuario,contrasena, primerNombre, apellidos, fechaNacimiento, nacionalidad) 
values (usuario, contra,nombre,apellido,nacimiento,pais);
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

--Buscar usuario por nombre
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

--Crear actividad 
create or replace function crearActividad (nombre varchar, fechaAct timestamp, tipo varchar) returns void
as
$$
insert into actividad (nombreactividad, fecha, tipoactividad) 
values (nombre, fechaAct, tipo);
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

--CORREGIR ESTO************************** es para mostrar usuarios y sus actividades
select (nombreActividad) from actividadDeportista as nomAct
inner join actividad as act
On nomAct.idactividad = act.idactividad
inner join usuario as usr
On usr.idusuario = nomAct.iddeportista


--Agregar amigos a un usuario por id
create or replace function agregarAmigo (idUser integer, idAmigo integer) returns void
as
$$
Select * from actividad
where nombreActividad = nombre;
$$
Language sql
