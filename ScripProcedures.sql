--Obtener un usuario por su id
create or replace procedure getUsuarioPorID(idBuscar int)
language plpgsql
as
$$
begin
select nombreUsuario,primerNombre,apellidos
from usuario
where idUsuario = idBuscar;
end
$$

--Agregar un usuario
create or replace procedure agregarUsuario(userName varchar, contra varchar, nombre varchar, apellido varchar, nacimiento date, pais  varchar)
language plpgsql
as
$$
begin
insert into usuario (nombreUsuario,contrasena, primerNombre, apellidos, fechaNacimiento, nacionalidad) 
values (userName, contra,nombre,apellido,nacimiento,pais);
end
$$

