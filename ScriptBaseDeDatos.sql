--Creacion de la base de datos
--create database basedatosstraviatec;

--Creacion de las tablas con sus llaves primarias
create table usuario(
	idUsuario serial primary key not null,
	NombreUsuario text not null,
	contrasena text not null,
	primerNombre text not null,
	apellidos text not null,
	fechaNacimiento date not null,
	nacionalidad text default 'No indica' ,
	foto bytea	
);

create table AmigosUsuario(
	idDeportista int not null,
	idAmigo int not null,
	primary key (idDeportista, idAmigo)
);

create table Actividad(
	idActividad serial primary key not null,
	nombreActividad text not null,
	fecha timestamp,
	tipoActividad text not null
);

create table ActividadDeportista(
	idActividad int not null,
	idDeportista int not null,
	kilometraje text,
	altura text,
	recorrido xml,
	duracion text,
	primary key (idActividad,idDeportista)
);

create table Grupo(
	idGrupo serial primary key not null,
	nombre text not null,
	idAdministrador int not null	
);

create table usuariosPorGrupo(
	idUsuario int not null,
	idGrupo int not null,
	primary key (idUsuario, idGrupo)
);

create table CarrerasGrupo(
	idGrupo int not null,
	idCarrera int not null,
	primary key (idGrupo,idCarrera)
);

create table RetosGrupo(
	idGrupo int not null,
	idReto int not null,
	primary key (idGrupo,idReto)
);

create table Carrera(
	idCarrera serial primary key not null,
	idOrganizador int not null,
	nombreCarrera text not null,
	fechaCarrera timestamp not null,
	TipoActividad text not null,
	recorrido xml,
	privada boolean default 'False',
	costo int not null,
	cuentaBancaria text not null
);

create table categoriaCarrera(
	idCarrera int not null,
	categoria text not null,
	primary key (idCarrera, categoria)
);

create table solicitudesCarrera(
	idCarrera int not null,
	idUsuario int not null,
	recibo bytea,
	primary key (idCarrera, idUsuario)
);

create table usuariosCarrera (
	idDeportista int not null,
	idCarrera int not null,
	tiempoRegistrado text,
	kilometraje text,
	altura text,
	completitud boolean default 'False',
	Recorrido xml,
	primary key (idDeportista,idCarrera)
);

create table Reto(
	idReto serial primary key not null,
	idOrganizador int not null,
	nombreReto text not null,
	objetivoReto text not null,
	fechaInicio timestamp,
	fechaFinaliza timestamp,
	tipoActividad text not null,
	tipoReto text not null,
	privada boolean default 'False'
);

create table usuariosReto (
	idDeportista int not null,
	idReto int not null,
	duracion text,
	kilometraje text,
	altura text,
	completitud boolean default 'False',
	Recorrido xml,
	primary key (idDeportista,idReto)
);

--Tabla Constante
create table Patrocinador(
	nombreComercial text primary key not null,
	representante text not null,
	numeroTelefono text not null,
	logo bytea
);

--Tabla Patrocinadores de cada reto
create table PatrocinadoresReto(
	idReto int not null,
	nombreComercial text not null,
	primary key (idReto,nombreComercial)
);

--Tabla Patrocinadores de cada carrera
create table PatrocinadoresCarrera(
	idCarrera int not null,
	nombreComercial text not null,
	primary key (idCarrera,nombreComercial)
);

--Modificacion de tablas

--Tabla usuario
alter table usuario
add constraint UQ_nombreUsuario
unique (NombreUsuario)

--Tabla Grupo
alter table Grupo 
add constraint FK_administrador 
foreign key (idAdministrador) references usuario (idUsuario);

alter table Grupo 
add constraint UQ_nombreGrupo 
unique (nombre);


--Tabla Carrera
alter table Carrera 
add constraint FK_organizadorCarrera
foreign key (idOrganizador) references usuario (idUsuario);

--Tabla Reto
alter table Reto
add constraint FK_organizadorReto
foreign key (idOrganizador) references usuario (idUsuario);

alter table Reto
add constraint UQ_nombreUnico
unique(nombreReto)

