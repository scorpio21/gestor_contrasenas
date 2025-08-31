-- Script SQL para recrear la base de datos y tablas que espera la aplicación
-- Motor: MySQL 8+

-- Crear base de datos (si no existe) y usarla
CREATE DATABASE IF NOT EXISTS gestor
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_general_ci;

USE gestor;

-- Reinicio limpio (eliminar tablas si existen)
-- Nota: eliminar primero la tabla hija por la FK
DROP TABLE IF EXISTS entradas;
DROP TABLE IF EXISTS usuarios;

-- Tabla de usuarios
CREATE TABLE usuarios (
  id              INT AUTO_INCREMENT PRIMARY KEY,
  email           VARCHAR(255) NOT NULL UNIQUE,
  password_hash   VARBINARY(64) NOT NULL,
  password_salt   VARBINARY(16) NOT NULL,
  master_key_blob TEXT NULL,
  creado_en       TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabla de entradas
CREATE TABLE entradas (
  id              INT AUTO_INCREMENT PRIMARY KEY,
  servicio        VARCHAR(255) NOT NULL,
  usuario         VARCHAR(255) NOT NULL,
  secreto_cifrado TEXT NOT NULL,
  login_url       VARCHAR(500) NULL,
  usuario_id      INT NULL,
  creado_en       TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  INDEX ix_entradas_usuario_id (usuario_id),
  INDEX ix_entradas_servicio_usuario (servicio, usuario),
  CONSTRAINT fk_entradas_usuario
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
    ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
