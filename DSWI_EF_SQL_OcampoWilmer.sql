USE Negocios2023
GO

/* PAISES */
CREATE OR ALTER PROC SP_PaisesAPI
AS
	SELECT * FROM tb_paises
GO

EXEC SP_PaisesAPI
GO

/* PROVEEDORES */
CREATE OR ALTER PROC SP_ProveedoresAPI
@n VARCHAR(40) = ''
AS
	SELECT *
    FROM tb_proveedores P
    WHERE NombreCia LIKE '%' + @n + '%' OR NombreContacto LIKE '%' + @n + '%'
GO

EXEC SP_ProveedoresAPI
GO

/* GENERAR ID PROVEEDOR */
CREATE OR ALTER PROC SP_GenerarIdProveedorAPI
AS
BEGIN
   SELECT ISNULL(MAX(IdProveedor),0) + 1
   FROM tb_proveedores
END
GO

EXEC SP_GenerarIdProveedorAPI
GO

/* INSERTAR PROVEEDOR */
CREATE OR ALTER PROC SP_InsertarProveedorAPI
(
   @id INT,
   @nom VARCHAR(40),
   @ncon VARCHAR(30),
   @ccon VARCHAR(30),
   @dir VARCHAR(60),
   @idPais CHAR(3),
   @tel VARCHAR(24),
   @fax VARCHAR(24)
)
AS
BEGIN
   INSERT INTO tb_proveedores(IdProveedor,NombreCia,NombreContacto,CargoContacto,Direccion,idpais,Telefono,Fax)
   VALUES(@id,@nom,@ncon,@ccon,@dir,@idPais,@tel,@fax)
END
GO

/* ACTUALIZAR PROVEEDOR */
CREATE OR ALTER PROC SP_ActualizarProveedorAPI
(
   @id INT,
   @nom VARCHAR(40),
   @ncon VARCHAR(30),
   @ccon VARCHAR(30),
   @dir VARCHAR(60),
   @idPais CHAR(3),
   @tel VARCHAR(24),
   @fax VARCHAR(24)
)
AS
BEGIN
   UPDATE tb_proveedores
   SET NombreCia = @nom, NombreContacto = @ncon, CargoContacto = @ccon, Direccion = @dir, idpais = @idPais, Telefono = @tel, Fax = @fax
   WHERE IdProveedor = @id
END
GO

/* ELIMINAR PROVEEDOR */
CREATE OR ALTER PROC SP_EliminarProveedorAPI
(
	@id VARCHAR(5)
)
AS
BEGIN
   DELETE FROM tb_proveedores WHERE IdProveedor = @id
END
GO