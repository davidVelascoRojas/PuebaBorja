
create database LABORJA
GO
use LABORJA
GO

Create table Sucursal
(
	ClaveSuc varchar(10) Not Null primary key,
	Descripcion varchar(60)
)
GO
insert into Sucursal Values ('suc1','GUADALAJARA')
insert into Sucursal Values ('suc2','ZAPOPAN')
insert into Sucursal Values ('suc3','TONALA')
insert into Sucursal Values ('suc4','TLAQUEPAQUE')
GO

create table Departamento
(
	ClaveDep varchar(10) Not Null primary key,
	ClaveSuc varchar(10) not Null,
	Descripcion varchar(60),
	CONSTRAINT fk_Dep_CalveSuc FOREIGN KEY (ClaveSuc) REFERENCES Sucursal (ClaveSuc)
)
GO

insert into Departamento values ('s1d1','suc1','Frutas y Verduras')
insert into Departamento values ('s1d2','suc1','Carniceria')
insert into Departamento values ('s1d3','suc1','Panadeia')
insert into Departamento values ('s1d4','suc1','Taqueria')
insert into Departamento values ('s2d1','suc2','Frutas y Verduras')
insert into Departamento values ('s2d2','suc2','Carniceria')
insert into Departamento values ('s2d3','suc2','Panadeia')
insert into Departamento values ('s2d4','suc2','Taqueria')
insert into Departamento values ('s3d1','suc3','Frutas y Verduras')
insert into Departamento values ('s3d2','suc3','Carniceria')
insert into Departamento values ('s3d3','suc3','Panadeia')
insert into Departamento values ('s3d4','suc3','Taqueria')
insert into Departamento values ('s4d1','suc4','Frutas y Verduras')
insert into Departamento values ('s4d2','suc4','Carniceria')
insert into Departamento values ('s4d3','suc4','Panadeia')
insert into Departamento values ('s4d4','suc4','Taqueria')


GO

create table Pregunta
(
	ClavePre varchar(10) Not Null primary key,
	ClaveSuc varchar(10),
	ClaveDep varchar(10),
	Descripcion varchar(100),
	CONSTRAINT fk_Pre_CalveDep FOREIGN KEY (ClaveDep) REFERENCES Departamento (ClaveDep),
	CONSTRAINT fk_Pre_CalveSuc FOREIGN KEY (ClaveSuc) REFERENCES Sucursal (ClaveSuc)
)
GO


insert into Pregunta values ('001','suc1','s1d1','El piso se encuentra limpio')
insert into Pregunta values ('002','suc1','s1d1','La temperatura es de 20°')
insert into Pregunta values ('003','suc1','s1d2','El piso se encuentra limpio')
insert into Pregunta values ('004','suc1','s1d2','Los precios están correctamente ubicados')
insert into Pregunta values ('005','suc1','s1d2','Los vidrios están limpios')
insert into Pregunta values ('006','suc1','s1d3','El piso se encuentra limpio')
insert into Pregunta values ('007','suc1','s1d3','La vitrina se encuentra llena')
insert into Pregunta values ('008','suc1','s1d4','El piso se encuentra limpio')
insert into Pregunta values ('009','suc1','s1d4','Los vidrios están limpios')
insert into Pregunta values ('010','suc1','s1d4','Hay especial del día durante el día')

insert into Pregunta values ('011','suc2','s2d1','El piso se encuentra limpio')
insert into Pregunta values ('012','suc2','s2d1','La temperatura es de 20°')
insert into Pregunta values ('013','suc2','s2d2','El piso se encuentra limpio')
insert into Pregunta values ('014','suc2','s2d2','Los precios están correctamente ubicados')
insert into Pregunta values ('015','suc2','s2d2','Los vidrios están limpios')
insert into Pregunta values ('016','suc2','s2d3','El piso se encuentra limpio')
insert into Pregunta values ('017','suc2','s2d3','La vitrina se encuentra llena')
insert into Pregunta values ('018','suc2','s2d4','El piso se encuentra limpio')
insert into Pregunta values ('019','suc2','s2d4','Los vidrios están limpios')
insert into Pregunta values ('020','suc2','s2d4','Hay especial del día durante el día')

insert into Pregunta values ('021','suc3','s3d1','El piso se encuentra limpio')
insert into Pregunta values ('022','suc3','s3d1','La temperatura es de 20°')
insert into Pregunta values ('023','suc3','s3d2','El piso se encuentra limpio')
insert into Pregunta values ('024','suc3','s3d2','Los precios están correctamente ubicados')
insert into Pregunta values ('025','suc3','s3d2','Los vidrios están limpios')
insert into Pregunta values ('026','suc3','s3d3','El piso se encuentra limpio')
insert into Pregunta values ('027','suc3','s3d3','La vitrina se encuentra llena')
insert into Pregunta values ('028','suc3','s3d4','El piso se encuentra limpio')
insert into Pregunta values ('029','suc3','s3d4','Los vidrios están limpios')
insert into Pregunta values ('030','suc3','s3d4','Hay especial del día durante el día')

insert into Pregunta values ('031','suc4','s4d1','El piso se encuentra limpio')
insert into Pregunta values ('032','suc4','s4d1','La temperatura es de 20°')
insert into Pregunta values ('033','suc4','s4d2','El piso se encuentra limpio')
insert into Pregunta values ('034','suc4','s4d2','Los precios están correctamente ubicados')
insert into Pregunta values ('035','suc4','s4d2','Los vidrios están limpios')
insert into Pregunta values ('036','suc4','s4d3','El piso se encuentra limpio')
insert into Pregunta values ('037','suc4','s4d3','La vitrina se encuentra llena')
insert into Pregunta values ('038','suc4','s4d4','El piso se encuentra limpio')
insert into Pregunta values ('039','suc4','s4d4','Los vidrios están limpios')
insert into Pregunta values ('040','suc4','s4d4','Hay especial del día durante el día')
GO


create table Agente
(
	ClaveAge varchar(10) not Null primary Key,
	Nombre varchar(80) not Null,
	Password varchar(15) not null
)
GO
insert into Agente values('age1','Supervisor','123')
insert into Agente values('age2','Agente','123')
GO

create table Encabezado
(
	IdEnc int Not Null identity Primary key,
	ClaveSuc varchar(10),
	ClaveAge varchar(10),
	Fecha date,
	CONSTRAINT fk_Enc_CalveAge FOREIGN KEY (ClaveAge) REFERENCES Agente (ClaveAge),
	CONSTRAINT fk_Enc_CalveSuc FOREIGN KEY (ClaveSuc) REFERENCES Sucursal (ClaveSuc)
)
GO

create table Partida
(
	Id bigint Not Null identity Primary key,
	IdEnc int Not Null,
	ClaveDep varchar(10) not Null,
	ClavePre varchar(10) not Null,
	Respuesta char(1) not Null,
	CONSTRAINT fk_Par_CalveEnc FOREIGN KEY (IdEnc) REFERENCES Encabezado (IdEnc),
	CONSTRAINT fk_Par_CalveDep FOREIGN KEY (ClaveDep) REFERENCES Departamento (ClaveDep),
	CONSTRAINT fk_Par_CalvePre FOREIGN KEY (ClavePre) REFERENCES Pregunta (ClavePre)
)
GO


create procedure insert_Encabezado
(
	@ClaveSuc varchar(10),
	@ClaveAge varchar(10),
	@Fecha dateTime,
	@resp int output
)
as
begin
	insert into Encabezado values (@ClaveSuc,@ClaveAge,@Fecha)
	select @resp = scope_identity()
	select @resp
end
GO

create procedure insert_Partida
(
	@IdEnc int,
	@ClaveDep varchar(10),
	@ClavePre varchar(10),
	@Respuesta char(1)
)
as
begin
	insert into Partida values (@IdEnc,@ClaveDep,@ClavePre,@Respuesta)
end
GO

create procedure Select_Agente
(
	@ClaveAge varchar(10),
	@Password varchar(10)
)
as
begin
	select * from Agente where ClaveAge = @ClaveAge and Password = @Password
end
GO


create procedure Select_Sucursal
(
	@ClaveSuc varchar(10) = ''
)
as
begin
	select * from Sucursal where ClaveSuc = @ClaveSuc or @ClaveSuc = ''
end
GO

create procedure Select_Departamento
(
	@ClaveSuc varchar(10) = ''
)
as
begin
	select * from Departamento where ClaveSuc = @ClaveSuc or @ClaveSuc = ''
end
GO


create procedure Select_Pregunta
(
	@ClaveSuc varchar(10)='',
	@ClaveDep varchar(10)=''
)
as
begin
	select * from Pregunta where (ClaveSuc = @ClaveSuc or @ClaveSuc = '') and (ClaveDep = @ClaveDep or @ClaveDep = '')
end
GO

create procedure rep_Encuestas
(
	@suc varchar(10) = '',
	@age varchar(10) = '',
	@fechaIni date = '',
	@fechaFin date = ''
)
as
begin
	--exec rep_Encuestas
	select suc.Descripcion as Sucursal,age.Nombre as Agente,enc.Fecha, 
		count(DISTINCT enc.IdEnc) as nEncuestas,
		count(DISTINCT part.ClaveDep) as departamentos,
		count(enc.IdEnc) as preguntas,
		sum(IIF(part.Respuesta = 'S',1,0)) as respSi,
		sum(IIF(part.Respuesta = 'N',1,0)) as respNo
		from Encabezado as enc
		inner join Partida as part on enc.IdEnc=part.IdEnc
		inner join Sucursal as suc on enc.ClaveSuc = suc.ClaveSuc
		inner join Agente as age on enc.ClaveAge = age.ClaveAge
		where (enc.ClaveSuc = @suc or @suc = '')
				and (enc.ClaveAge = @age or @age = '')
				and (enc.Fecha >= @fechaIni or @fechaIni = '')
				and (enc.Fecha <= @fechaFin or @fechaFin = '')
		group by suc.Descripcion,age.Nombre,enc.Fecha
		
end
GO

