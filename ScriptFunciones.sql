--Creacion de funciones
create function buscarUsuarioId (int) returns usuario
as
$$
Select * from usuario
where idUsuario = $1;
$$
Language sql
select buscarUsuarioId(1)