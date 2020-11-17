--Llenado de la tabla constante
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

--Creacion de usuarios
select crearUsuario('robert25', 'robert25','Roberto','Arias','1989-10-12','Costarricense', bytea('https://e00-marca.uecdn.es/assets/multimedia/imagenes/2020/08/06/15967015067257.jpg'));
select crearUsuario('mari123', 'mari123','Marie','Smith','1995-05-04','Canadiense', bytea('https://www.mi-deporte.com/wp-content/uploads/2018/08/la-mejor-ropa-mujer-ciclismo.jpg'));
select crearUsuario('pedro123', 'pedro123','Pedro','Perico','1995-05-04','Los Palotes', bytea('https://www.bestfunnies.com/wp-content/uploads/2015/05/Funny-Cyclist-22.jpg'));
select crearUsuario('andrey', 'andrey','Andrey','Amador','1986-08-29','Costarricense', bytea('https://elguardian.cr/wp-content/uploads/2016/12/Amador.jpg'));


--Agregar amigos entre usuario
select agregarAmigo('1','2');
select agregarAmigo('1','3');
select agregarAmigo('1','4');
select agregarAmigo('2','1');
select agregarAmigo('2','4');
select agregarAmigo('4','3');
select agregarAmigo('3','4');


--Creacion de actividades y asignacion al usuario segun su id
select crearActividad (2,'Vueltas al plano', '2020-11-16','Correr');
select crearActividad (3,'cruzar el arenal', '2020-5-4','Kayak');


--Creacion de un grupo y asignacion de su administrador
select crearGrupo ('Moncho Bikers','1')


--Agregar usuarios al grupo por el id del usuario y el id del grupo
select agregarUsuarioGrupo ('2','1');
select agregarUsuarioGrupo ('3','1');


--Crear una carrera
select crearCarrera ('1','Vuelta al Arenal','2020-11-22 8:00:00','Bicicleta','Master-A','<xml><xml>', 'false', 10500,'CR-5412378');
select crearCarrera ('1','Palmarin','2020-06-22 11:30:00','Caminata','Sub-23','<xml><xml>', 'true',5000,'CR-5412378');


--Agregar patrocinadores a la carrera
select agregarPatrocinadorCarrera (1,'Piros');
select agregarPatrocinadorCarrera (1,'Treck');
select agregarPatrocinadorCarrera (2,'Gatorade');


--Asignacion de una carrera privada a un grupo por id del grupo y id de la carrera
select agregarCarreraGrupo (1,1)


--Enviar solicitud de afiliacion a una carrera con idCarrera, idUsuario y foto del recibo
select enviarSolicitudCarrera(1,2,bytea('https://www.nwcu.com/storage/app/media/Check-Image-Example.jpg'));


--Agregar usuarios a la carrera por el id del deportista y el id de la carrera
select agregarUsuarioCarrera (1,1);
select agregarUsuarioCarrera (2,1); --Este no lo agrega porque no coincide la categoria
select agregarUsuarioCarrera (4,1);


--Crear reto 
select crearReto ('1','2000 metros de ascenso','2','2020-10-20 7:00:00','2020-10-25 7:00:00','Senderismo','altitud','false');
select crearReto ('1','100Km en 3 dias', '100','2020-10-20 7:00:00','2020-10-23 7:00:00','Ciclismo','fondo','false');
select crearReto ('1','Corrientes del Arenal','5','2021-06-22 7:00:00','2021-06-22 9:00:00','Kayak','Fondo','True');


--Agregar un reto privado a un grupo por id del grupo y id del reto
select agregarRetoGrupo(1,3);


--Agregar un usuario a un reto por el id del deportista y el id del reto
select agregarUsuarioReto (3,1);
select agregarUsuarioReto (2,1)
select agregarUsuarioReto (4,2);
select agregarUsuarioReto (2,2);


--Agregar patrocinadores a un reto por el id del reto y el nombre del patrocinador
select agregarPatrocinadorReto(1,'Piros')
select agregarPatrocinadorReto(2,'San Antonio')
select agregarPatrocinadorReto(2,'On wheels')




