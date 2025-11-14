/*PROCEDIMIENTOS ALMACENADO VETERINARIA*/

-- ===========================================
-- PABLO FLORES REYES
-- ===========================================

-- ===========================================
-- CITAS
-- ===========================================

USE veterinaria
GO

-- Listar Citas
CREATE OR ALTER PROCEDURE SP_GetCitas
AS
BEGIN
	SELECT 
		-- Cita
		C.id_cita,          -- 0
		CAST(C.fecha_hora AS DATE),       -- 1
		C.motivo,           -- 2
		C.id_mascota,       -- 3
		C.id_veterinario,   -- 4
		C.estadoCita,       -- 5
		C.estado,           -- 6
    
		-- Mascota
		M.id_mascota,       -- 7
		M.nombre,           -- 8
		M.especie,          -- 9
		M.edad,             -- 10
		M.sexo,             -- 11
		M.id_cliente,       -- 12
		M.estado,           -- 13
    
		-- Cliente
		CL.id_cliente,      -- 14
		CL.nombre_completo, -- 15
		CL.dni,             -- 16
		CL.telefono,        -- 17
		CL.direccion,       -- 18
		CL.correo,          -- 19
		CL.estado,          -- 20
    
		-- Veterinario
		V.id_veterinario,   -- 21
		V.nombre_completo,  -- 22
		V.dni,              -- 23
		V.telefono,         -- 24
		V.especialidad,     -- 25
		V.correo,           -- 26
		V.estado            -- 27
FROM cita AS C 
JOIN mascota AS M ON C.id_mascota = M.id_mascota
JOIN cliente AS CL ON M.id_cliente = CL.id_cliente
JOIN veterinario AS V ON C.id_veterinario = V.id_veterinario
WHERE C.estado = 1;
END
GO

-- Buscar Cita por ID
CREATE OR ALTER PROCEDURE SP_GetCitasByID
(
	@ID INT
)
AS
BEGIN
	SELECT 
		-- Cita
		C.id_cita,          -- 0
		CAST(C.fecha_hora AS DATE),       -- 1
		C.motivo,           -- 2
		C.id_mascota,       -- 3
		C.id_veterinario,   -- 4
		C.estadoCita,       -- 5
		C.estado,           -- 6
    
		-- Mascota
		M.id_mascota,       -- 7
		M.nombre,           -- 8
		M.especie,          -- 9
		M.edad,             -- 10
		M.sexo,             -- 11
		M.id_cliente,       -- 12
		M.estado,           -- 13
    
		-- Cliente
		CL.id_cliente,      -- 14
		CL.nombre_completo, -- 15
		CL.dni,             -- 16
		CL.telefono,        -- 17
		CL.direccion,       -- 18
		CL.correo,          -- 19
		CL.estado,          -- 20
    
		-- Veterinario
		V.id_veterinario,   -- 21
		V.nombre_completo,  -- 22
		V.dni,              -- 23
		V.telefono,         -- 24
		V.especialidad,     -- 25
		V.correo,           -- 26
		V.estado            -- 27
FROM cita AS C 
JOIN mascota AS M ON C.id_mascota = M.id_mascota
JOIN cliente AS CL ON M.id_cliente = CL.id_cliente
JOIN veterinario AS V ON C.id_veterinario = V.id_veterinario
WHERE C.Id_Cita = @ID AND C.estado = 1
END
GO

SP_GetCitasByID 5

SELECT * FROM cliente
GO

-- Buscar Cita por Fecha
CREATE OR ALTER PROCEDURE SP_GetCitasByFecha
(
	@Fecha DATE
)
AS
BEGIN
	SELECT 
		-- Cita
		C.id_cita,          -- 0
		CAST(C.fecha_hora AS DATE),       -- 1
		C.motivo,           -- 2
		C.id_mascota,       -- 3
		C.id_veterinario,   -- 4
		C.estadoCita,       -- 5
		C.estado,           -- 6
    
		-- Mascota
		M.id_mascota,       -- 7
		M.nombre,           -- 8
		M.especie,          -- 9
		M.edad,             -- 10
		M.sexo,             -- 11
		M.id_cliente,       -- 12
		M.estado,           -- 13
    
		-- Cliente
		CL.id_cliente,      -- 14
		CL.nombre_completo, -- 15
		CL.dni,             -- 16
		CL.telefono,        -- 17
		CL.direccion,       -- 18
		CL.correo,          -- 19
		CL.estado,          -- 20
    
		-- Veterinario
		V.id_veterinario,   -- 21
		V.nombre_completo,  -- 22
		V.dni,              -- 23
		V.telefono,         -- 24
		V.especialidad,     -- 25
		V.correo,           -- 26
		V.estado            -- 27
FROM cita AS C 
JOIN mascota AS M ON C.id_mascota = M.id_mascota
JOIN cliente AS CL ON M.id_cliente = CL.id_cliente
JOIN veterinario AS V ON C.id_veterinario = V.id_veterinario
WHERE CAST(C.fecha_hora AS DATE) = @Fecha AND C.estado = 1
END
GO

SP_GetCitasByFecha '2024-02-01'
GO

-- Agregar Cita
CREATE OR ALTER PROCEDURE sp_CrearCita
    @Fecha DATETIME,
    @Motivo VARCHAR(MAX),
    @IdMascota INT,
    @IdVeterinario INT
AS
BEGIN
    INSERT INTO Cita (fecha_hora, motivo, id_mascota, id_veterinario)
    VALUES (@Fecha, @Motivo, @IdMascota, @IdVeterinario)
SELECT @@IDENTITY
END
go

--Actualizar Cita
CREATE OR ALTER PROCEDURE sp_ActualizarCita
    @IdCita INT,
    @Fecha DATETIME,
    @Motivo VARCHAR(MAX),
    @IdMascota INT,
    @IdVeterinario INT
AS
BEGIN
    UPDATE Cita
    SET fecha_hora = @Fecha,
        motivo = @Motivo,
        id_mascota = @IdMascota,
        id_veterinario = @IdVeterinario
    WHERE id_cita = @IdCita
END
go

-- Eliminar Cita
CREATE OR ALTER PROCEDURE sp_EliminarCita
    @IdCita INT
AS
BEGIN
    UPDATE Cita
    SET estado = 0
    WHERE id_cita = @IdCita
END
GO

-- ===========================================
-- VENTAS
-- ===========================================

-- Insertar en cabecera
CREATE OR ALTER PROCEDURE sp_AgregarVentaCabecera
(
    @id_cliente INT,
    @total DECIMAL(10,2),
    @idVenta INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO venta (fecha, id_cliente, total)
    VALUES (GETDATE(), @id_cliente, @total);

    SET @idVenta = SCOPE_IDENTITY();
END;
GO

-- Insertar en detalle
CREATE OR ALTER PROCEDURE sp_AgregarVentaDetalle
    @id_venta INT,
    @id_producto INT = NULL,
    @id_servicio INT = NULL,
    @cantidad INT
AS
BEGIN
    DECLARE @precio DECIMAL(10,2);

    IF @id_producto IS NOT NULL
        SELECT @precio = precio FROM producto WHERE id_producto = @id_producto;

    IF @id_servicio IS NOT NULL
        SELECT @precio = precio FROM servicio WHERE id_servicio = @id_servicio;

    INSERT INTO detalle_venta (id_venta, id_producto, id_servicio, cantidad, subtotal)
    VALUES (@id_venta, @id_producto, @id_servicio, @cantidad, @precio * @cantidad);

    -- Actualizar stock si es producto
    IF @id_producto IS NOT NULL
        UPDATE producto SET stock = stock - @cantidad WHERE id_producto = @id_producto;

    -- Actualizar total de la venta
    UPDATE venta
    SET total = (SELECT SUM(subtotal) FROM detalle_venta WHERE id_venta = @id_venta)
    WHERE id_venta = @id_venta;
END;
GO

CREATE OR ALTER PROCEDURE SP_ObtenerDetallesVentaPorIdVenta
(@Id INT)
AS
BEGIN
	SELECT 
		D.id_detalle    AS Detalle_IdDetalle,
		D.id_venta      AS Detalle_IdVenta,
		D.id_producto   AS Detalle_IdProducto,
		D.id_servicio   AS Detalle_IdServicio,
		D.cantidad      AS Detalle_Cantidad,
		D.subtotal      AS Detalle_SubTotal,

		V.id_venta      AS Venta_IdVenta,
		V.fecha         AS Venta_Fecha,
		V.id_cliente    AS Venta_IdCliente,
		V.total         AS Venta_Total,

		P.id_producto   AS Producto_IdProducto,
		P.nombre        AS Producto_Nombre,
		P.descripcion   AS Producto_Descripcion,
		P.precio        AS Producto_Precio,
		P.stock         AS Producto_Stock,
		P.tipo          AS Producto_Tipo,
		P.id_proveedor  AS Producto_IdProveedor,

		S.id_servicio   AS Servicio_IdServicio,
		S.nombre        AS Servicio_Nombre,
		S.descripcion   AS Servicio_Descripcion,
		S.precio        AS Servicio_Precio,

		C.id_cliente    AS Cliente_IdCliente,
		C.nombre_completo        AS Cliente_Nombre,
		C.dni           AS Cliente_DNI,
		C.telefono      AS Cliente_Telefono,
		C.direccion     AS Cliente_Direccion,
		C.correo        AS Cliente_Correo
	FROM detalle_venta D
	JOIN venta V ON D.id_venta = V.id_venta
	LEFT JOIN producto P ON D.id_producto = P.id_producto
	LEFT JOIN servicio S ON D.id_servicio = S.id_servicio
	JOIN cliente C ON V.id_cliente = C.id_cliente
	WHERE D.id_venta = @Id
END

SELECT * FROM venta
SELECT * FROM cliente
SELECT * FROM detalle_venta
GO 

CREATE OR ALTER PROCEDURE sp_obtenerVentaCabecera
(@id INT)
AS
BEGIN
	SELECT V.id_venta AS idVenta,
			V.fecha,
			V.id_cliente AS idCliente,
			C.nombre_completo AS nombreCliente,
			C.telefono,
			C.dni,
			C.direccion,
			V.total
	FROM venta AS V
	JOIN cliente AS C ON V.id_cliente = C.id_cliente
	WHERE id_venta = @id
END
GO

sp_obtenerVenta 1
GO

-- ===========================================
-- CARLOS CORNEJO PEÑALOZA
-- ===========================================

-- ===========================================
-- VETERINARIO
-- ===========================================

----------LISTAR  VETERINARIOS -------

CREATE PROC SP_GetVeterinarios
AS
BEGIN
    SELECT 
        V.id_veterinario AS IDVeterinario,
        V.nombre_completo AS NombreCompleto,
        V.dni AS DNI,
        V.telefono AS Telefono,
        V.especialidad AS Especialidad,
        V.correo AS Correo,
        V.estado AS Estado
    FROM veterinario V
    WHERE V.estado = 1
END
GO

---------REGISTRAR-------

CREATE PROC SP_InsertVeterinario
  @nombre_completo VARCHAR(100),
  @dni VARCHAR(15),
  @telefono VARCHAR(20),
  @especialidad VARCHAR(100),
  @correo VARCHAR(100),
  @estado TINYINT
AS
INSERT INTO veterinario(nombre_completo, dni, telefono, especialidad, correo,estado)
VALUES (@nombre_completo, @dni, @telefono, @especialidad, @correo, @estado);
SELECT @@IDENTITY
GO

---------ACTUALIZAR----------

CREATE PROC SP_UpdateVeterinario
  @id_veterinario INT,
  @nombre_completo VARCHAR(100),
  @dni VARCHAR(15),
  @telefono VARCHAR(20),
  @especialidad VARCHAR(100),
  @correo VARCHAR(100),
  @estado TINYINT
AS
UPDATE veterinario
SET nombre_completo = @nombre_completo,
    dni = @dni,
    telefono = @telefono,
    especialidad = @especialidad,
    correo = @correo,
    estado = @estado
WHERE id_veterinario = @id_veterinario;
GO

----------ELIMINAR --------

CREATE OR ALTER PROCEDURE SP_DeleteVeterinario
    @id_veterinario INT
AS
BEGIN
    UPDATE veterinario
	SET estado = 0;
END
GO

--------OBTENER-----

CREATE PROC SP_GetVeterinarioById
    @ID INT
AS
BEGIN
    SELECT 
        V.id_veterinario  AS IDVeterinario,
        V.nombre_completo AS NombreCompleto,
        V.dni             AS DNI,
        V.telefono        AS Telefono,
        V.especialidad    AS Especialidad,
        V.correo          AS Correo,
        V.estado          AS Estado
    FROM veterinario V
    WHERE V.id_veterinario = @ID;
END
GO

----------------------------------------- PRODUCTOS

--------LISTAR PRODUCTOS-----

CREATE OR ALTER PROCEDURE SP_GetProductos
AS
BEGIN
	SELECT *, 
		P.id_producto AS 'IdProducto', 
		P.nombre AS 'Nombre',
		P.descripcion AS 'Descripcion',
		P.precio AS 'Precio',
		P.stock AS 'Stock',
		P.tipo AS 'Tipo',
		p.id_proveedor as 'IdProveedor',
		P.estado AS 'Estado'
	FROM producto AS P
	WHERE estado = 1
END

-----INSERTAR----

CREATE PROCEDURE SP_InsertProducto
    @Nombre        VARCHAR(100),
    @Descripcion   VARCHAR(MAX),
    @Precio        DECIMAL(10,2),
    @Stock         INT,
    @Tipo          VARCHAR(20),
    @IdProveedor   INT,
    @Estado        BIT
AS
BEGIN
    INSERT INTO producto
    (nombre, descripcion, precio, stock, tipo, id_proveedor, estado)
    VALUES
    (@Nombre, @Descripcion, @Precio, @Stock, @Tipo, @IdProveedor, @Estado);

    SELECT @@IDENTITY;
END;

-------OBTENER PRODUCTO-----

CREATE PROC SP_GetProductoById
    @ID INT
AS
BEGIN
    SELECT 
        P.id_producto   AS IDProducto,
        P.nombre        AS Nombre,
        P.descripcion   AS Descripcion,
        P.precio        AS Precio,
        P.stock         AS Stock,
        P.tipo          AS Tipo,
        PR.id_proveedor AS IDProveedor,
        PR.nombre       AS NombreProveedor,
        P.estado        AS Estado
    FROM producto P
    INNER JOIN proveedor PR ON P.id_proveedor = PR.id_proveedor
    WHERE P.id_producto = @ID;
END
GO

--------ACTUALIZAR-------
CREATE PROC SP_UpdateProducto
    @id_producto INT,
    @nombre VARCHAR(100),
    @descripcion VARCHAR(MAX),
    @precio DECIMAL(10,2),
    @stock INT,
    @tipo VARCHAR(20),
    @id_proveedor INT,
    @estado BIT
AS
BEGIN
    UPDATE producto
    SET nombre       = @nombre,
        descripcion  = @descripcion,
        precio       = @precio,
        stock        = @stock,
        tipo         = @tipo,
        id_proveedor = @id_proveedor,
        estado       = @estado
    WHERE id_producto = @id_producto
END
GO

-----DELETE----

CREATE PROCEDURE SP_DeleteProducto
    @id_producto INT
AS
BEGIN
    UPDATE producto
	SET estado = 0;
END

-- ===========================================
-- AKEMY CARPIO
-- ===========================================

-- ===========================================
-- MASCOTAS
-- ===========================================

CREATE PROCEDURE SP_GetMascotas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        -- Mascota
        IdMascota = m.id_mascota,
        Nombre    = m.nombre,
        Especie   = m.especie,
        Raza      = m.raza,            -- ← NUEVO
        Edad      = m.edad,
        Sexo      = m.sexo,
        IdCliente = m.id_cliente,
        Estado    = CAST(m.estado AS bit),

        -- Cliente
        ClienteId        = c.id_cliente,
        ClienteNombre    = c.nombre_completo,
        ClienteDNI       = c.dni,
        ClienteTelefono  = c.telefono,
        ClienteDireccion = c.direccion,
        ClienteCorreo    = c.correo,
        ClienteEstado    = CAST(c.estado AS bit)
    FROM dbo.mascota AS m
    LEFT JOIN dbo.cliente AS c
        ON c.id_cliente = m.id_cliente;
END
GO

-- ===========================================
-- SP_GetMascotaById (@Id)
-- ===========================================

CREATE PROCEDURE SP_GetMascotaById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdMascota = m.id_mascota,
        Nombre    = m.nombre,
        Especie   = m.especie,
        Raza      = m.raza,            -- ← NUEVO
        Edad      = m.edad,
        Sexo      = m.sexo,
        IdCliente = m.id_cliente,
        Estado    = CAST(m.estado AS bit),

        ClienteId        = c.id_cliente,
        ClienteNombre    = c.nombre_completo,
        ClienteDNI       = c.dni,
        ClienteTelefono  = c.telefono,
        ClienteDireccion = c.direccion,
        ClienteCorreo    = c.correo,
        ClienteEstado    = CAST(c.estado AS bit)
    FROM dbo.mascota AS m
    LEFT JOIN dbo.cliente AS c
        ON c.id_cliente = m.id_cliente
    WHERE m.id_mascota = @Id;
END
GO

-- ===========================================
-- SP_BuscarMascotaPorNombre (@Nombre)
-- ===========================================

CREATE PROCEDURE SP_BuscarMascotaPorNombre
    @Nombre VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdMascota = m.id_mascota,
        Nombre    = m.nombre,
        Especie   = m.especie,
        Raza      = m.raza,            -- ← NUEVO
        Edad      = m.edad,
        Sexo      = m.sexo,
        IdCliente = m.id_cliente,
        Estado    = CAST(m.estado AS bit),

        ClienteId        = c.id_cliente,
        ClienteNombre    = c.nombre_completo,
        ClienteDNI       = c.dni,
        ClienteTelefono  = c.telefono,
        ClienteDireccion = c.direccion,
        ClienteCorreo    = c.correo,
        ClienteEstado    = CAST(c.estado AS bit)
    FROM dbo.mascota AS m
    LEFT JOIN dbo.cliente AS c
        ON c.id_cliente = m.id_cliente
    WHERE m.nombre LIKE '%' + @Nombre + '%';
END
GO


-- ===========================================
-- sp_CrearMascota  (INSERT)  → devuelve Id nuevo (INT)
-- ===========================================

CREATE PROCEDURE sp_CrearMascota
    @Nombre    VARCHAR(50),
    @Especie   VARCHAR(50),
    @Raza      VARCHAR(50),
    @Edad      INT,
    @Sexo      VARCHAR(15),
    @IdCliente INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.mascota (nombre, especie, raza, edad, sexo, id_cliente)
    VALUES (@Nombre, @Especie, @Raza, @Edad, @Sexo, @IdCliente);

    SELECT CONVERT(INT, SCOPE_IDENTITY()) AS NewId;
END
GO

-- ===========================================
-- sp_ActualizarMascota  (UPDATE)
-- ===========================================

CREATE PROCEDURE sp_ActualizarMascota
    @IdMascota INT,
    @Nombre    VARCHAR(50),
    @Especie   VARCHAR(50),
    @Raza      VARCHAR(50),
    @Edad      INT,
    @Sexo      VARCHAR(15)
    --@IdCliente INT,
    --@Estado    BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.mascota
       SET nombre     = @Nombre,
           especie    = @Especie,
           raza       = @Raza,
           edad       = @Edad,
           sexo       = @Sexo
          -- id_cliente = @IdCliente,
          -- estado     = CAST(@Estado AS TINYINT)
     WHERE id_mascota = @IdMascota;
END
GO

-- ===========================================
-- sp_EliminarMascota  (DELETE logico)
-- ===========================================

CREATE PROCEDURE sp_EliminarMascota
    @IdMascota INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.mascota SET estado = 0 WHERE id_mascota = @IdMascota;
END
GO

-- ===========================================
-- CLIENTES
-- ===========================================
-- ===========================================
-- SP_GetCliente (Listar a todo cliente)
-- ===========================================

CREATE PROCEDURE SP_GetCliente
AS
BEGIN
    SET NOCOUNT ON;

	SELECT 
	--Cliente
	  IdCliente   = c.id_cliente,
        Nombre      = c.nombre_completo,
        DNI         = c.dni,
        Telefono    = c.telefono,
        Direccion   = c.direccion,
        Correo      = c.correo,
        Estado      = CAST(c.estado AS bit)
		FROM dbo.cliente AS c;
END
GO

-- ===========================================
-- SP_GetClienteById (@Id)
-- ===========================================

CREATE PROCEDURE SP_GetClienteById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
	--cliente
        IdCliente  = c.id_cliente,
        Nombre     = c.nombre_completo,
        DNI        = c.dni,
        Telefono   = c.telefono,       
        Direccion  = c.direccion,
        Correo     = c.correo,
        Estado    = CAST(c.estado AS bit)
    FROM dbo.cliente AS c
	WHERE c.id_cliente = @Id;
END
GO


-- ===========================================
-- SP_BuscarClientePorNombre (@Nombre)
-- ===========================================

CREATE PROCEDURE SP_BuscarClientePorNombre
    @Nombre VARCHAR(100)  
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdCliente = c.id_cliente,
        Nombre    = c.nombre_completo,
        DNI       = c.dni,
        Telefono  = c.telefono,
        Direccion = c.direccion,
        Correo    = c.correo,
        Estado    = CAST(c.estado AS bit)
    FROM dbo.cliente AS c
    WHERE c.nombre_completo LIKE '%' + @Nombre + '%';
END
GO
-- ===========================================
-- sp_CrearCliente (INSERT)  → devuelve Id nuevo (INT)
-- ===========================================
IF EXISTS (SELECT 1 
           FROM sys.objects 
           WHERE type = 'P' 
           AND name = 'sp_CrearCliente')
BEGIN
    DROP PROCEDURE dbo.sp_CrearCliente;
END
GO
CREATE PROCEDURE dbo.sp_CrearCliente
    @Nombre_completo VARCHAR(100),
    @DNI       VARCHAR(15),
    @Telefono  VARCHAR(20),
    @Direccion VARCHAR(200),
    @Correo    VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.cliente (nombre_completo, dni, telefono, direccion, correo)
    VALUES (@Nombre_completo, @DNI, @Telefono, @Direccion, @Correo);

     SELECT SCOPE_IDENTITY() AS NuevoIdCliente;
END
GO

-- ===========================================
-- sp_ActualizarCliente  (UPDATE)
-- ===========================================

CREATE PROCEDURE sp_ActualizarCliente
    @IdCliente INT,
    @Nombre    VARCHAR(50),
    @DNI      VARCHAR(15),
    @Telefono  VARCHAR(20),
    @Direccion VARCHAR(200),
    @Correo    VARCHAR(100),
    @Estado    BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.cliente
       SET nombre_completo    = @Nombre,
           dni                = @DNI,
           telefono           = @Telefono,
           direccion          = @Direccion,
           correo             = @Correo,
		   estado             = @Estado
     WHERE id_cliente = @IdCliente;
END
GO

-- ===========================================
-- sp_EliminarCliente  (DELETE logico)
-- ===========================================

CREATE PROCEDURE sp_EliminarCliente
    @IdCliente INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.cliente SET estado = 0 WHERE id_cliente = @IdCliente;
END
GO

-- ===========================================
-- Edgar Yuberli
-- ===========================================

----------------------------------------- PROCEDIMIENTOS ALMACENADOS PROVEEDORES ------------------------------------------
-- 1. Obtener todos los proveedores
CREATE PROCEDURE SP_GetProveedores
AS
BEGIN
    SELECT 
        id_proveedor,
        nombre,
        ruc,
        telefono,
        direccion,
        correo,
        estado
    FROM proveedor
    WHERE estado = 1
    ORDER BY nombre;
END;
GO

-- 2. Obtener proveedor por ID
CREATE PROCEDURE SP_GetProveedorById
    @IdProveedor INT
AS
BEGIN
    SELECT 
        id_proveedor,
        nombre,
        ruc,
        telefono,
        direccion,
        correo,
        estado
    FROM proveedor
    WHERE id_proveedor = @IdProveedor AND estado = 1;
END;
GO

-- 3. Insertar nuevo proveedor
CREATE PROCEDURE SP_InsertProveedor
    @Nombre VARCHAR(100),
    @Ruc VARCHAR(20),
    @Telefono VARCHAR(20),
    @Direccion VARCHAR(150),
    @Correo VARCHAR(100)
AS
BEGIN
    INSERT INTO proveedor (
        nombre, 
        ruc, 
        telefono, 
        direccion, 
        correo, 
        estado
    )
    VALUES (
        @Nombre,
        @Ruc,
        @Telefono,
        @Direccion,
        @Correo,
        1
    );
    
    SELECT SCOPE_IDENTITY();
END;
GO

-- 4. Actualizar proveedor
CREATE PROCEDURE SP_UpdateProveedor
    @IdProveedor INT,
    @Nombre VARCHAR(100),
    @Ruc VARCHAR(20),
    @Telefono VARCHAR(20),
    @Direccion VARCHAR(150),
    @Correo VARCHAR(100)
AS
BEGIN
    UPDATE proveedor 
    SET 
        nombre = @Nombre,
        ruc = @Ruc,
        telefono = @Telefono,
        direccion = @Direccion,
        correo = @Correo
    WHERE id_proveedor = @IdProveedor;
END;
GO

-- 5. Crear el procedimiento con eliminación LÓGICA
CREATE PROCEDURE SP_DeleteProveedor
    @IdProveedor INT
AS
BEGIN
    UPDATE proveedor 
    SET estado = 0 
    WHERE id_proveedor = @IdProveedor;
END;
GO

----------------------------------------- PROCEDIMIENTOS ALMACENADOS SERVICIOS ------------------------------------------

-- 1. Obtener todos los servicios
CREATE PROCEDURE SP_GetServicios
AS
BEGIN
    SELECT 
        id_servicio,
        nombre,
        descripcion,
        precio,
        estado
    FROM servicio
    WHERE estado = 1
    ORDER BY nombre;
END;
GO

-- 2. Obtener servicio por ID
CREATE PROCEDURE SP_GetServicioById
    @IdServicio INT
AS
BEGIN
    SELECT 
        id_servicio,
        nombre,
        descripcion,
        precio,
        estado
    FROM servicio
    WHERE id_servicio = @IdServicio AND estado = 1;
END;
GO

-- 3. Insertar nuevo servicio
CREATE PROCEDURE SP_InsertServicio
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(MAX),
    @Precio DECIMAL(10,2)
AS
BEGIN
    INSERT INTO servicio (nombre, descripcion, precio, estado)
    VALUES (@Nombre, @Descripcion, @Precio, 1);
    
    SELECT SCOPE_IDENTITY();
END;
GO

-- 4. Actualizar servicio
CREATE PROCEDURE SP_UpdateServicio
    @IdServicio INT,
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(MAX),
    @Precio DECIMAL(10,2)
AS
BEGIN
    UPDATE servicio 
    SET 
        nombre = @Nombre,
        descripcion = @Descripcion,
        precio = @Precio
    WHERE id_servicio = @IdServicio;
END;
GO

-- 5. Eliminar servicio (eliminación lógica)
CREATE PROCEDURE SP_DeleteServicio
    @IdServicio INT
AS
BEGIN
    UPDATE servicio 
    SET estado = 0 
    WHERE id_servicio = @IdServicio;
END;
GO

----------------------------------------- PROCEDIMIENTOS ALMACENADOS HISTORIAL ------------------------------------------

-- 1. Obtener todos los historiales
CREATE PROCEDURE SP_GetHistoriales
AS
BEGIN
    SELECT 
        a.id_atencion,
        a.diagnostico,
        a.tratamiento,
        a.observaciones,
        a.fecha,
        c.motivo,
        c.estadoCita,
        
        m.id_mascota,
        m.nombre,
        m.especie,
        m.edad,
        
        cl.id_cliente,
        cl.nombre_completo,
        cl.dni,
        
        v.id_veterinario,
        v.nombre_completo,
        v.especialidad
        
    FROM atencion a
    INNER JOIN cita c ON a.id_cita = c.id_cita
    INNER JOIN mascota m ON c.id_mascota = m.id_mascota
    INNER JOIN cliente cl ON m.id_cliente = cl.id_cliente
    INNER JOIN veterinario v ON c.id_veterinario = v.id_veterinario
    WHERE a.estado = 1
    ORDER BY a.fecha DESC;
END;
GO

-- 2. Obtener historial por ID
CREATE PROCEDURE SP_GetHistorialById
    @IdAtencion INT
AS
BEGIN
    SELECT 
        a.id_atencion,
        a.diagnostico,
        a.tratamiento,
        a.observaciones,
        a.fecha,
        c.motivo,
        c.estadoCita,
        
        m.id_mascota,
        m.nombre,
        m.especie,
        m.edad,
        
        cl.id_cliente,
        cl.nombre_completo,
        cl.dni,
        
        v.id_veterinario,
        v.nombre_completo,
        v.especialidad
        
    FROM atencion a
    INNER JOIN cita c ON a.id_cita = c.id_cita
    INNER JOIN mascota m ON c.id_mascota = m.id_mascota
    INNER JOIN cliente cl ON m.id_cliente = cl.id_cliente
    INNER JOIN veterinario v ON c.id_veterinario = v.id_veterinario
    WHERE a.id_atencion = @IdAtencion AND a.estado = 1;
END;
GO

-- 3. Buscar historial por nombre de mascota
CREATE PROCEDURE SP_BuscarHistorialPorNombre
    @Nombre VARCHAR(50)
AS
BEGIN
    SELECT 
        a.id_atencion,
        a.diagnostico,
        a.tratamiento,
        a.observaciones,
        a.fecha,
        c.motivo,
        c.estadoCita,
        
        m.id_mascota,
        m.nombre,
        m.especie,
        m.edad,
        
        cl.id_cliente,
        cl.nombre_completo,
        cl.dni,
        
        v.id_veterinario,
        v.nombre_completo,
        v.especialidad
        
    FROM atencion a
    INNER JOIN cita c ON a.id_cita = c.id_cita
    INNER JOIN mascota m ON c.id_mascota = m.id_mascota
    INNER JOIN cliente cl ON m.id_cliente = cl.id_cliente
    INNER JOIN veterinario v ON c.id_veterinario = v.id_veterinario
    WHERE m.nombre LIKE '%' + @Nombre + '%' AND a.estado = 1
    ORDER BY a.fecha DESC;
END;
GO

use veterinaria