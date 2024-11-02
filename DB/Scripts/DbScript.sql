CREATE SCHEMA IF NOT EXISTS "public";

CREATE  TABLE "public".tbl_caracteristicas ( 
	idcaracteristica     integer  NOT NULL GENERATED BY DEFAULT AS IDENTITY  ,
	descripcioncaracteristica varchar(9999999)  NOT NULL  ,
	orden                integer    ,
	CONSTRAINT pk_tbl_caracteristicas PRIMARY KEY ( idcaracteristica )
 );

CREATE  TABLE "public".tbl_cat_paises ( 
	idpais               integer  NOT NULL GENERATED  BY DEFAULT AS IDENTITY ,
	descripcion          varchar  NOT NULL  ,
	orden                integer    ,
	CONSTRAINT pk_tbl_cat_paises PRIMARY KEY ( idpais )
 );

CREATE  TABLE "public".tbl_cat_roles ( 
	idrol                integer  NOT NULL GENERATED  BY DEFAULT AS IDENTITY ,
	descripcion          varchar  NOT NULL  ,
	orden                integer    ,
	CONSTRAINT pk_tbl_cat_roles PRIMARY KEY ( idrol )
 );

CREATE  TABLE "public".tbl_cat_tiposcultivos ( 
	idtipocultivo        integer  NOT NULL GENERATED  BY DEFAULT AS IDENTITY ,
	descripcion          varchar(9999999)  NOT NULL  ,
	orden                integer  NOT NULL  ,
	CONSTRAINT pk_tbl_cat_tiposcultivos PRIMARY KEY ( idtipocultivo )
 );

CREATE  TABLE "public".tbl_cat_usuario ( 
	idusuario            integer  NOT NULL GENERATED  BY DEFAULT AS IDENTITY ,
	nombre               varchar  NOT NULL  ,
	apellido1            varchar  NOT NULL  ,
	apellido2            varchar  NOT NULL  ,
	correo               varchar  NOT NULL  ,
	"password"           varchar  NOT NULL  ,
	estado               integer    ,
	CONSTRAINT pk_tbl_cat_usuario PRIMARY KEY ( idusuario )
 );

CREATE  TABLE "public".tbl_caracteristicas_tipocultivo ( 
	iddetallecaracteristica integer  NOT NULL GENERATED  BY DEFAULT AS IDENTITY ,
	idtipocultivo        integer  NOT NULL  ,
	descripcion          varchar(9999999)  NOT NULL  ,
	idcaracteristica     integer  NOT NULL  ,
	CONSTRAINT pk_tbl_caracteristicas_tipocultivo PRIMARY KEY ( iddetallecaracteristica ),
	CONSTRAINT fk_tbl_caracteristicas_tipocultivo_idtipo FOREIGN KEY ( idtipocultivo ) REFERENCES "public".tbl_cat_tiposcultivos( idtipocultivo )   ,
	CONSTRAINT fk_tbl_caracteristicas_tipocultivo_idcaracteristica FOREIGN KEY ( idcaracteristica ) REFERENCES "public".tbl_caracteristicas( idcaracteristica )   
 );

CREATE  TABLE "public".tbl_cat_empresas ( 
	idempresa            integer  NOT NULL GENERATED  BY DEFAULT AS IDENTITY ,
	idpais               integer  NOT NULL  ,
	descripcion          varchar  NOT NULL  ,
	direccion            varchar  NOT NULL  ,
	telefono1            varchar  NOT NULL  ,
	telefono2            varchar  NOT NULL  ,
	website              varchar  NOT NULL  ,
	email                varchar  NOT NULL  ,
	logo                 bytea    ,
	CONSTRAINT pk_tbl_cat_empresas PRIMARY KEY ( idempresa ),
	CONSTRAINT fk_tbl_cat_empresas_paises FOREIGN KEY ( idpais ) REFERENCES "public".tbl_cat_paises( idpais )   
 );

CREATE  TABLE "public".tbl_cat_rolesusuario ( 
	idrolusuario         integer  NOT NULL GENERATED  BY DEFAULT AS IDENTITY ,
	idusuario            integer    ,
	idrol                integer    ,
	CONSTRAINT pk_tbl_cat_rolesusuario PRIMARY KEY ( idrolusuario ),
	CONSTRAINT fk_tbl_cat_rolesusuario_idusuario FOREIGN KEY ( idusuario ) REFERENCES "public".tbl_cat_usuario( idusuario )   ,
	CONSTRAINT fk_tbl_cat_rolesusuario_idrol FOREIGN KEY ( idrol ) REFERENCES "public".tbl_cat_roles( idrol )   
 );

CREATE  TABLE "public".tbl_cultivos ( 
	idcultivo            integer  NOT NULL GENERATED  BY DEFAULT AS IDENTITY ,
	descripcion          varchar(9999999)  NOT NULL  ,
	orden                integer  NOT NULL  ,
	ubicacion            varchar  NOT NULL  ,
	etapafenologica      integer  NOT NULL  ,
	fechasiembra         date  NOT NULL  ,
	variedad             varchar  NOT NULL  ,
	idempresa            integer  NOT NULL  ,
	idtipocultivo        integer  NOT NULL  ,
	CONSTRAINT pk_tbl_cultivos PRIMARY KEY ( idcultivo ),
	CONSTRAINT fk_tbl_cultivos_idempresa FOREIGN KEY ( idempresa ) REFERENCES "public".tbl_cat_empresas( idempresa )   ,
	CONSTRAINT fk_tbl_cultivos_idtipocultivo FOREIGN KEY ( idtipocultivo ) REFERENCES "public".tbl_cat_tiposcultivos( idtipocultivo )   
 );

CREATE  TABLE "public".tbl_muestras ( 
	idmuestra            integer  NOT NULL GENERATED  BY DEFAULT AS IDENTITY ,
	fechahora            date  NOT NULL  ,
	latitud              varchar  NOT NULL  ,
	idusuario            integer  NOT NULL  ,
	idcultivo            integer  NOT NULL  ,
	fechahoramodificacion date  NOT NULL  ,
	idusuariomodificacion integer  NOT NULL  ,
	comentario           varchar    ,
	foto                 bytea    ,
	CONSTRAINT pk_tbl_muestras PRIMARY KEY ( idmuestra ),
	CONSTRAINT fk_tbl_muestras_idusuario FOREIGN KEY ( idusuario ) REFERENCES "public".tbl_cat_usuario( idusuario )   ,
	CONSTRAINT fk_tbl_muestras_idusuariomodificacion FOREIGN KEY ( idusuariomodificacion ) REFERENCES "public".tbl_cat_usuario( idusuario )   ,
	CONSTRAINT fk_tbl_muestras_idcultivo FOREIGN KEY ( idcultivo ) REFERENCES "public".tbl_cultivos( idcultivo )   
 );

CREATE  TABLE "public".tbl_caracteristicas_muestras ( 
	iddetallemuestracaracteristica integer  NOT NULL GENERATED BY DEFAULT AS IDENTITY  ,
	idmuestra            integer    ,
	idcaracteristica     integer    ,
	valor                varchar    ,
	CONSTRAINT pk_tbl_caracteristicas_muestras PRIMARY KEY ( iddetallemuestracaracteristica ),
	CONSTRAINT fk_tbl_caracteristicas_muestras_idmuestra FOREIGN KEY ( idmuestra ) REFERENCES "public".tbl_muestras( idmuestra )   ,
	CONSTRAINT fk_tbl_caracteristicas_muestras_idcaracteristica FOREIGN KEY ( idcaracteristica ) REFERENCES "public".tbl_caracteristicas( idcaracteristica )   
 );
