CREATE DATABASE IF NOT EXISTS turnos_app
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE turnos_app;

START TRANSACTION;

CREATE TABLE IF NOT EXISTS clientes (
  id INT NOT NULL AUTO_INCREMENT,
  nombre VARCHAR(100) NOT NULL,
  apellido VARCHAR(100) NOT NULL,
  telefono VARCHAR(40) NOT NULL,
  email VARCHAR(150) NOT NULL,
  PRIMARY KEY (id)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS profesionales (
  id INT NOT NULL AUTO_INCREMENT,
  nombre VARCHAR(150) NOT NULL,
  especialidad VARCHAR(120) NOT NULL,
  PRIMARY KEY (id)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS servicios (
  id INT NOT NULL AUTO_INCREMENT,
  nombre VARCHAR(120) NOT NULL,
  duracion_minutos INT NOT NULL,
  precio DECIMAL(10, 2) NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT chk_servicios_duracion CHECK (duracion_minutos > 0),
  CONSTRAINT chk_servicios_precio CHECK (precio >= 0)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS turnos (
  id INT NOT NULL AUTO_INCREMENT,
  cliente_id INT NOT NULL,
  profesional_id INT NOT NULL,
  servicio_id INT NOT NULL,
  fecha_hora DATETIME NOT NULL,
  estado VARCHAR(30) NOT NULL DEFAULT 'Solicitado',
  PRIMARY KEY (id),
  CONSTRAINT fk_turnos_clientes
    FOREIGN KEY (cliente_id) REFERENCES clientes (id),
  CONSTRAINT fk_turnos_profesionales
    FOREIGN KEY (profesional_id) REFERENCES profesionales (id),
  CONSTRAINT fk_turnos_servicios
    FOREIGN KEY (servicio_id) REFERENCES servicios (id),
  CONSTRAINT uq_turnos_profesional_fecha
    UNIQUE (profesional_id, fecha_hora)
) ENGINE = InnoDB;

INSERT INTO clientes (id, nombre, apellido, telefono, email) VALUES
  (1, 'Ana', 'Garcia', '11-2456-1001', 'ana.garcia@example.com'),
  (2, 'Bruno', 'Martinez', '11-2456-1002', 'bruno.martinez@example.com'),
  (3, 'Carla', 'Lopez', '11-2456-1003', 'carla.lopez@example.com'),
  (4, 'Diego', 'Fernandez', '11-2456-1004', 'diego.fernandez@example.com'),
  (5, 'Elena', 'Suarez', '11-2456-1005', 'elena.suarez@example.com'),
  (6, 'Federico', 'Romero', '11-2456-1006', 'federico.romero@example.com'),
  (7, 'Gabriela', 'Torres', '11-2456-1007', 'gabriela.torres@example.com'),
  (8, 'Hernan', 'Diaz', '11-2456-1008', 'hernan.diaz@example.com'),
  (9, 'Irene', 'Navarro', '11-2456-1009', 'irene.navarro@example.com'),
  (10, 'Javier', 'Molina', '11-2456-1010', 'javier.molina@example.com');

INSERT INTO profesionales (id, nombre, especialidad) VALUES
  (1, 'Laura Benitez', 'Clinica medica'),
  (2, 'Martin Quiroga', 'Dermatologia'),
  (3, 'Sofia Rivas', 'Kinesiologia'),
  (4, 'Pablo Herrera', 'Odontologia'),
  (5, 'Valeria Castro', 'Nutricion'),
  (6, 'Nicolas Vega', 'Psicologia'),
  (7, 'Camila Ortega', 'Cardiologia'),
  (8, 'Santiago Pereyra', 'Oftalmologia'),
  (9, 'Paula Acosta', 'Pediatria'),
  (10, 'Ignacio Silva', 'Traumatologia');

INSERT INTO servicios (id, nombre, duracion_minutos, precio) VALUES
  (1, 'Consulta general', 30, 8500.00),
  (2, 'Control dermatologico', 30, 9500.00),
  (3, 'Sesion de kinesiologia', 45, 7800.00),
  (4, 'Limpieza dental', 40, 12000.00),
  (5, 'Plan nutricional inicial', 60, 11000.00),
  (6, 'Entrevista psicologica', 50, 10000.00),
  (7, 'Control cardiologico', 45, 15000.00),
  (8, 'Examen visual', 30, 9000.00),
  (9, 'Consulta pediatrica', 30, 8700.00),
  (10, 'Evaluacion traumatologica', 40, 13000.00);

INSERT INTO turnos (id, cliente_id, profesional_id, servicio_id, fecha_hora, estado) VALUES
  (1, 1, 1, 1, '2026-08-03 09:00:00', 'Solicitado'),
  (2, 2, 2, 2, '2026-08-03 10:00:00', 'Confirmado'),
  (3, 3, 3, 3, '2026-08-03 11:00:00', 'Solicitado'),
  (4, 4, 4, 4, '2026-08-04 09:30:00', 'Confirmado'),
  (5, 5, 5, 5, '2026-08-04 10:30:00', 'Solicitado'),
  (6, 6, 6, 6, '2026-08-04 11:30:00', 'Cancelado'),
  (7, 7, 7, 7, '2026-08-05 09:00:00', 'Confirmado'),
  (8, 8, 8, 8, '2026-08-05 10:00:00', 'Solicitado'),
  (9, 9, 9, 9, '2026-08-05 11:00:00', 'Atendido'),
  (10, 10, 10, 10, '2026-08-06 09:30:00', 'Solicitado');

COMMIT;
