USE master;
GO

CREATE DATABASE veterinaria;
GO

USE veterinaria;
GO

CREATE TABLE cliente (
  id_cliente INT IDENTITY PRIMARY KEY,
  nombre_completo VARCHAR(100) NOT NULL,
  dni VARCHAR(15) UNIQUE,
  telefono VARCHAR(20),
  direccion VARCHAR(150),
  correo VARCHAR(100),
  estado BIT DEFAULT 1
);
GO

INSERT INTO cliente (nombre_completo, dni, telefono, direccion, correo) VALUES
('María García', '12345678', '987654321', 'Av. Los Pinos 123', 'maria.garcia@mail.com'),
('Juan Pérez', '87654321', '912345678', 'Jr. Los Olivos 456', 'juan.perez@mail.com'),
('Ana López', '45678912', '987111222', 'Av. Las Flores 789', 'ana.lopez@mail.com'),
('Carlos Ruiz', '78912345', '933444555', 'Calle Las Violetas 321', 'carlos.ruiz@mail.com'),
('Pedro Sánchez', '24681357', '987222111', 'Av. Las Palmeras 456', 'pedro.sanchez@mail.com'),
('Gabriela Torres', '35792468', '987333222', 'Jr. Los Laureles 321', 'gabriela.torres@mail.com'),
('Fernando Castro', '46813579', '987444333', 'Calle Los Cedros 234', 'fernando.castro@mail.com'),
('Isabel Ramírez', '57924681', '987555444', 'Av. Los Alamos 789', 'isabel.ramirez@mail.com'),
('Ricardo Morales', '68135792', '987666555', 'Jr. Las Amapolas 123', 'ricardo.morales@mail.com'),
('Patricia Herrera', '79246813', '987777666', 'Calle Las Begonias 456', 'patricia.herrera@mail.com'),
('Jorge Vargas', '13572468', '987888777', 'Av. Los Robles 321', 'jorge.vargas@mail.com'),
('Claudia Espinoza', '24683579', '987999888', 'Pasaje Los Tulipanes 654', 'claudia.espinoza@mail.com'),
('Héctor López', '35794681', '987111000', 'Av. Los Jazmines 987', 'hector.lopez@mail.com'),
('Rosa Delgado', '46815792', '987222999', 'Jr. Los Cipreses 852', 'rosa.delgado@mail.com'),
('Manuel Medina', '57926813', '987333888', 'Calle Las Dalias 753', 'manuel.medina@mail.com'),
('Adriana Castillo', '68135724', '987444777', 'Av. Los Eucaliptos 159', 'adriana.castillo@mail.com'),
('Felipe Rojas', '79246835', '987555666', 'Jr. Las Acacias 357', 'felipe.rojas@mail.com'),
('Lorena Cárdenas', '13579248', '987666555', 'Pasaje Los Jacintos 951', 'lorena.cardenas@mail.com'),
('Sergio Paredes', '24681359', '987777444', 'Calle Los Claveles 753', 'sergio.paredes@mail.com'),
('Camila Navarro', '35792461', '987888333', 'Av. Las Rosas 852', 'camila.navarro@mail.com'),
('Alberto Campos', '46813571', '987999222', 'Jr. Los Ceibos 951', 'alberto.campos@mail.com'),
('Mónica Salas', '57924683', '987000111', 'Calle Las Violetas 147', 'monica.salas@mail.com'),
('Andrés Peña', '68135795', '987123456', 'Av. Los Pinos 258', 'andres.pena@mail.com'),
('Daniela León', '79246817', '987234567', 'Jr. Los Geranios 369', 'daniela.leon@mail.com'),
('Lucía Fernández', '13579246', '999888777', 'Pasaje Los Cedros 987', 'lucia.fernandez@mail.com');
GO

CREATE TABLE mascota (
  id_mascota INT IDENTITY PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL,
  especie VARCHAR(50),
  raza VARCHAR(50),
  edad INT,
  sexo VARCHAR(15),
  id_cliente INT,
  estado BIT DEFAULT 1
  FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente)
);
GO

INSERT INTO mascota (nombre, especie, raza, edad, sexo, id_cliente) VALUES
('Max', 'Perro', 'Labrador', 5, 'Macho', 1),
('Luna', 'Gato', 'Persa', 3, 'Hembra', 2),
('Rocky', 'Perro', 'Bulldog', 4, 'Macho', 3),
('Mia', 'Gato', 'Siames', 2, 'Hembra', 4),
('Toby', 'Perro', 'Golden Retriever', 6, 'Macho', 5),
('Simba', 'Gato', 'Bengalí', 2, 'Macho', 6),
('Nala', 'Gato', 'Angora', 4, 'Hembra', 7),
('Thor', 'Perro', 'Pastor Alemán', 3, 'Macho', 8),
('Bella', 'Perro', 'Beagle', 5, 'Hembra', 9),
('Coco', 'Loro', 'Amazona', 7, 'Macho', 10),
('Kira', 'Perro', 'Husky Siberiano', 2, 'Hembra', 11),
('Oliver', 'Gato', 'Sphynx', 1, 'Macho', 12),
('Molly', 'Conejo', 'Enano Holandés', 2, 'Hembra', 13),
('Zeus', 'Perro', 'Doberman', 4, 'Macho', 14),
('Chispa', 'Hámster', 'Sirio', 1, 'Hembra', 15),
('Loki', 'Perro', 'Pug', 3, 'Macho', 16),
('Daisy', 'Perro', 'Cocker Spaniel', 6, 'Hembra', 17),
('Kiwi', 'Ave', 'Periquito Australiano', 2, 'Macho', 18),
('Lola', 'Gato', 'Maine Coon', 5, 'Hembra', 19),
('Bruno', 'Perro', 'Boxer', 3, 'Macho', 20),
('Tommy', 'Tortuga', 'Orejas Rojas', 8, 'Macho', 21),
('Canela', 'Conejo', 'Mini Rex', 2, 'Hembra', 22),
('Rex', 'Perro', 'Dálmata', 4, 'Macho', 23),
('Mimi', 'Gato', 'Bombay', 1, 'Hembra', 24),
('Apolo', 'Perro', 'San Bernardo', 5, 'Macho', 25);
GO

CREATE TABLE veterinario (
  id_veterinario INT IDENTITY PRIMARY KEY,
  nombre_completo VARCHAR(100),
  dni VARCHAR(15) UNIQUE,
  telefono VARCHAR(20),
  especialidad VARCHAR(100),
  correo VARCHAR(100),
  estado BIT DEFAULT 1
);
GO

INSERT INTO veterinario (nombre_completo, dni, telefono, especialidad, correo) VALUES
('Dr. Pablo Medina', '66778899', '986789012', 'Traumatología Animal', 'pablo.medina@vetclinic.pe'),
('Dra. Rosa Campos', '77889900', '987890123', 'Anestesiología Veterinaria', 'rosa.campos@vetclinic.pe'),
('Dr. Esteban Silva', '88990011', '988901234', 'Oftalmología Veterinaria', 'esteban.silva@vetclinic.pe'),
('Dra. Verónica Aguilar', '99001122', '989012345', 'Cardiología Veterinaria', 'veronica.aguilar@vetclinic.pe'),
('Dr. Julio Herrera', '10111213', '980123456', 'Neurología Veterinaria', 'julio.herrera@vetclinic.pe'),
('Dra. Patricia Torres', '11121314', '981234568', 'Oncología Veterinaria', 'patricia.torres@vetclinic.pe'),
('Dr. Ricardo Vega', '12131415', '982345679', 'Ortopedia Veterinaria', 'ricardo.vega@vetclinic.pe'),
('Dra. Lorena Navarro', '13141516', '983456780', 'Rehabilitación Animal', 'lorena.navarro@vetclinic.pe'),
('Dr. Alejandro Paredes', '14151617', '984567891', 'Medicina Preventiva', 'alejandro.paredes@vetclinic.pe'),
('Dr. Andrea Rojas', '11223344', '981234567', 'Cirugía General', 'andrea.rojas@vetclinic.pe'),
('Dr. Luis González', '22334455', '982345678', 'Odontología Animal', 'luis.gonzalez@vetclinic.pe'),
('Dra. Carla Fernández', '33445566', '983456789', 'Medicina Interna', 'carla.fernandez@vetclinic.pe'),
('Dr. Mario Salazar', '44556677', '984567890', 'Dermatología Veterinaria', 'mario.salazar@vetclinic.pe'),
('Dra. Elena Martín', '55667788', '985678901', 'Vacunación y Profilaxis', 'elena.martin@vetclinic.pe'),
('Dra. Mónica Salcedo', '15161718', '985678902', 'Nutrición Animal', 'monica.salcedo@vetclinic.pe');
GO

-- Tabla: cita
CREATE TABLE cita (
  id_cita INT IDENTITY(1,1) PRIMARY KEY,
  fecha_hora DATETIME,
  motivo VARCHAR(MAX),
  id_mascota INT,
  id_veterinario INT,
  estadoCita VARCHAR(20) DEFAULT 'Pendiente',
  estado BIT DEFAULT 1,
  FOREIGN KEY (id_mascota) REFERENCES mascota(id_mascota),
  FOREIGN KEY (id_veterinario) REFERENCES veterinario(id_veterinario),
  CHECK (estadoCita IN ('Pendiente', 'Atendida', 'Cancelada'))
);
GO

INSERT INTO cita (fecha_hora, motivo, id_mascota, id_veterinario, estadoCita) VALUES
('2024-02-01 10:00', 'Consulta general', 1, 1, 'Pendiente'),
('2024-02-01 11:00', 'Vacunación', 2, 5, 'Pendiente'),
('2024-02-01 12:00', 'Control dental', 3, 2, 'Atendida'),
('2024-02-02 09:30', 'Revisión dermatológica', 4, 4, 'Pendiente'),
('2024-02-02 10:30', 'Chequeo anual', 5, 3, 'Cancelada'),
('2024-02-03 09:00', 'Vacunación anual', 6, 6, 'Pendiente'),
('2024-02-03 10:30', 'Chequeo general', 7, 7, 'Atendida'),
('2024-02-03 15:00', 'Control de peso', 8, 8, 'Pendiente'),
('2024-02-04 08:30', 'Desparasitación', 9, 9, 'Atendida'),
('2024-02-04 10:00', 'Control posoperatorio', 10, 10, 'Pendiente'),
('2024-02-04 11:15', 'Consulta por alergia', 11, 11, 'Pendiente'),
('2024-02-05 09:45', 'Chequeo dental', 12, 12, 'Atendida'),
('2024-02-05 11:30', 'Consulta por caída de pelo', 13, 13, 'Pendiente'),
('2024-02-05 16:00', 'Vacunación antirrábica', 14, 14, 'Pendiente'),
('2024-02-06 08:45', 'Revisión de herida', 15, 15, 'Atendida'),
('2024-02-06 10:00', 'Control nutricional', 16, 1, 'Pendiente'),
('2024-02-06 11:30', 'Chequeo rutinario', 17, 2, 'Pendiente'),
('2024-02-07 09:00', 'Vacunación triple', 18, 3, 'Atendida'),
('2024-02-07 10:15', 'Consulta por tos', 19, 4, 'Pendiente'),
('2024-02-07 11:45', 'Control de crecimiento', 20, 5, 'Pendiente');
GO

-- Tabla: atencion
CREATE TABLE atencion (
  id_atencion INT IDENTITY(1,1) PRIMARY KEY,
  id_cita INT UNIQUE,
  diagnostico VARCHAR(MAX),
  tratamiento VARCHAR(MAX),
  observaciones VARCHAR(MAX),
  estado BIT DEFAULT 1,
  fecha DATE,
  FOREIGN KEY (id_cita) REFERENCES cita(id_cita)
);
GO

INSERT INTO atencion (id_cita, diagnostico, tratamiento, observaciones, fecha) VALUES
(1, 'Infección de oídos leve', 'Limpieza y gotas óticas', 'Revisar en 1 semana', '2024-02-01'),
(4, 'Dermatitis alérgica', 'Antihistamínicos y baño medicado', 'Evitar alérgenos conocidos', '2024-02-02'),
(6, 'Vacunación incompleta', 'Aplicación de segunda dosis', 'Revisar calendario de vacunas', '2024-02-03'),
(7, 'Chequeo general', 'Sin hallazgos preocupantes', 'Recomendado control anual', '2024-02-03'),
(8, 'Sobrepeso leve', 'Dieta y ejercicio', 'Reducir raciones en 20%', '2024-02-03'),
(10, 'Postoperatorio normal', 'Curación y analgesia', 'Retirar puntos en 10 días', '2024-02-04'),
(11, 'Alergia estacional', 'Antihistamínicos y suplemento', 'Evitar exposición a polen', '2024-02-04'),
(12, 'Chequeo dental rutinario', 'Limpieza ligera', 'Mantener higiene diaria', '2024-02-05'),
(13, 'Pérdida de pelo', 'Examen de tiroides', 'Iniciar tratamiento hormonal si positivo', '2024-02-05'),
(14, 'Vacunación antirrábica', 'Refuerzo aplicado', 'Sin reacciones', '2024-02-05'),
(3, 'Sarro dental leve', 'Limpieza dental', 'Se recomienda control en 6 meses', '2024-02-01'),
(5, 'Chequeo preventivo', 'Vitaminas y control', 'Mascota saludable', '2024-02-02'),
(2, 'Vacunación completa', 'Aplicación triple felina', 'Todo correcto', '2024-02-01');
GO

-- Tabla: proveedor
CREATE TABLE proveedor (
  id_proveedor INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(100),
  ruc VARCHAR(20) UNIQUE,
  telefono VARCHAR(20),
  direccion VARCHAR(150),
  correo VARCHAR(100),
  estado BIT DEFAULT 1
);
GO

INSERT INTO proveedor (nombre, ruc, telefono, direccion, correo) VALUES
('Proveedor VetMedic', '20123456789', '987111222', 'Av. Industrial 100', 'ventas@vetmedic.com'),
('Distribuidora PetFood', '20987654321', '988222333', 'Jr. Comercio 200', 'contacto@petfood.com'),
('Accesorios PetLife', '20555544433', '977333444', 'Av. Mascotas 300', 'info@petlife.com'),
('Farmavet SAC', '20111222334', '987444555', 'Av. Veterinaria 450', 'contacto@farmavet.com'),
('Pet Salud EIRL', '20222333445', '987555666', 'Jr. Bienestar Animal 123', 'ventas@petsalud.com'),
('Mascota Feliz', '20333444556', '987666777', 'Calle Los Animales 987', 'info@mascotafeliz.com'),
('VetMarket Distribuciones', '20444555667', '987777888', 'Av. Industrial Sur 456', 'ventas@vetmarket.com'),
('Agrovet Perú', '20555666778', '987888999', 'Carretera Central Km 25', 'ventas@agrovetperu.com'),
('Clinivet Proveedores', '20666777889', '987999000', 'Av. Las Mascotas 741', 'contacto@clinivet.com'),
('Pet World SAC', '20777888990', '986111222', 'Jr. Comercio Pet 321', 'info@petworld.com'),
('AlfaVet Distribuidora', '20888999001', '986222333', 'Av. Industrial Norte 159', 'ventas@alfavet.com'),
('Pet House Insumos', '20999000112', '986333444', 'Pasaje Animal Lovers 753', 'contacto@pethouse.com'),
('Global Pet Supply', '20011112223', '986444555', 'Av. Internacional 258', 'ventas@globalpet.com');
GO

-- Tabla: producto
CREATE TABLE producto (
  id_producto INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(100),
  descripcion VARCHAR(MAX),
  precio DECIMAL(10,2),
  stock INT,
  tipo VARCHAR(20),
  id_proveedor INT,
  estado BIT DEFAULT 1,
  FOREIGN KEY (id_proveedor) REFERENCES proveedor(id_proveedor),
  CHECK (tipo IN ('Medicamento', 'Comida', 'Juguete', 'Accesorio', 'Otro'))
);
GO

INSERT INTO producto (nombre, descripcion, precio, stock, tipo, id_proveedor) VALUES
('Vacuna Antirrábica', 'Vacuna para perros y gatos', 50.00, 100, 'Medicamento', 1),
('Alimento Premium Perro', 'Bolsa 10kg', 120.00, 50, 'Comida', 2),
('Juguete Pelota', 'Pelota de goma resistente', 15.00, 200, 'Juguete', 3),
('Correa Ajustable', 'Correa para perro mediano', 35.00, 80, 'Accesorio', 3),
('Vitaminas Mascotas', 'Suplemento multivitamínico', 25.00, 60, 'Medicamento', 1),
('Antibiótico Oral', 'Tratamiento para infecciones bacterianas', 80.00, 120, 'Medicamento', 1),
('Shampoo Medicado', 'Limpieza para piel sensible', 45.50, 75, 'Accesorio', 6),
('Snacks Saludables Gato', 'Bocaditos bajos en grasa', 22.00, 150, 'Comida', 2),
('Rascador para Gatos', 'Madera con sisal y cojín', 55.00, 40, 'Accesorio', 9),
('Plato Antideslizante', 'Para comida y agua, fácil de lavar', 18.00, 90, 'Accesorio', 7),
('Kit de Cepillado', 'Cepillo y peine para pelo largo', 30.00, 65, 'Accesorio', 8),
('Collar LED', 'Collar con luz para paseos nocturnos', 28.00, 110, 'Accesorio', 10),
('Comida Húmeda Gato', 'Lata de salmón y vegetales', 12.00, 300, 'Comida', 2),
('Suplemento Articular', 'Glucosamina para perros mayores', 60.00, 80, 'Medicamento', 5),
('Cama Ortopédica', 'Cama con soporte para articulaciones', 150.00, 25, 'Otro', 11),
('Antipulgas en Collar', 'Protección continua por 3 meses', 70.00, 140, 'Medicamento', 4),
('Juguete Interactivo', 'Dispensador de premios', 40.00, 95, 'Juguete', 3),
('Bloque de Heno', 'Alimento para conejos y roedores', 10.00, 200, 'Comida', 12),
('Fuente de Agua', 'Circulación continua para gatos', 85.00, 50, 'Otro', 13),
('Cepillo Dental Mascota', 'Higiene bucal diaria', 15.50, 180, 'Accesorio', 6);
GO

-- Tabla: servicio
CREATE TABLE servicio (
  id_servicio INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(100),
  descripcion VARCHAR(MAX),
  precio DECIMAL(10,2),
  estado BIT DEFAULT 1
);
GO

INSERT INTO servicio (nombre, descripcion, precio) VALUES
('Consulta General', 'Evaluación médica de la mascota', 50.00),
('Baño y Corte', 'Servicio de aseo completo', 70.00),
('Vacunación Completa', 'Aplicación de vacunas según especie y edad', 90.00),
('Desparasitación Interna', 'Tratamiento para parásitos internos', 40.00),
('Desparasitación Externa', 'Tratamiento para pulgas y garrapatas', 45.00),
('Control Dental', 'Limpieza y revisión de dientes', 120.00),
('Cirugía Menor', 'Procedimientos quirúrgicos simples', 250.00),
('Esterilización', 'Esterilización de perros o gatos', 300.00),
('Guardería de Mascotas', 'Cuidado diario para mascotas', 80.00),
('Peluquería Especial', 'Corte de raza y estilizado profesional', 100.00),
('Análisis de Laboratorio', 'Exámenes de sangre y orina', 150.00),
('Revisión Dermatológica', 'Chequeo de piel y tratamiento de alergias', 130.00);
GO
-- Tabla: venta
CREATE TABLE venta (
  id_venta INT IDENTITY(1,1) PRIMARY KEY,
  fecha DATETIME,
  id_cliente INT,
  total DECIMAL(10,2),
  estado BIT DEFAULT 1
  FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente)
);
GO

INSERT INTO venta (fecha, id_cliente, total) VALUES
('2024-02-01 13:00', 1, 170.00),
('2024-02-02 15:00', 2, 85.00),
('2024-02-03 10:00', 3, 95.00),
('2024-02-03 16:00', 4, 120.00),
('2024-02-04 09:30', 5, 250.00),
('2024-02-04 15:45', 6, 80.00),
('2024-02-05 10:15', 7, 95.50),
('2024-02-05 17:30', 8, 160.00),
('2024-02-06 11:00', 9, 210.00),
('2024-02-06 16:45', 10, 70.00),
('2024-02-07 09:20', 11, 310.00),
('2024-02-07 14:10', 12, 145.00),
('2024-02-08 10:50', 13, 55.00),
('2024-02-08 15:35', 14, 200.00),
('2024-02-09 09:05', 15, 85.00),
('2024-02-09 12:40', 16, 275.00),
('2024-02-10 11:15', 17, 120.00),
('2024-02-10 16:30', 18, 95.00);
GO

-- Tabla: detalle_venta
CREATE TABLE detalle_venta (
  id_detalle INT IDENTITY(1,1) PRIMARY KEY,
  id_venta INT,
  id_producto INT NULL,
  id_servicio INT NULL,
  cantidad INT,
  subtotal DECIMAL(10,2),
  FOREIGN KEY (id_venta) REFERENCES venta(id_venta),
  FOREIGN KEY (id_producto) REFERENCES producto(id_producto),
  FOREIGN KEY (id_servicio) REFERENCES servicio(id_servicio)
);
GO

INSERT INTO detalle_venta (id_venta, id_producto, id_servicio, cantidad, subtotal) VALUES
(1, 1, NULL, 2, 100.00),
(1, 3, NULL, 2, 30.00),
(2, NULL, 1, 1, 50.00),
(2, 5, NULL, 1, 25.00),
(3, NULL, 2, 1, 70.00),
(4, 2, NULL, 1, 120.00),                  
(5, NULL, 5, 1, 250.00),                  
(6, 3, NULL, 2, 30.00),                   
(6, NULL, 3, 1, 45.00),                   
(7, NULL, 1, 1, 50.00),                   
(7, 5, NULL, 1, 25.50),                   
(8, NULL, 4, 1, 120.00),                  
(9, 4, NULL, 2, 70.00),                   
(9, NULL, 2, 1, 70.00),                  
(10, NULL, 10, 1, 130.00); 
GO
