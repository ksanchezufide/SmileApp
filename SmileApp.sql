-- Roles y Seguridad
CREATE DATABASE SmileAppDb;

INSERT INTO Roles (Nombre) VALUES ('Administrador');
INSERT INTO Roles (Nombre) VALUES ('Dentista');

USE SmileAppDb;
GO

INSERT INTO Usuarios (Nombre, Correo, Contrase�aHash, RolId, Estado)
VALUES ('Juan P�rez', 'admin2@clinica.com', 'admin123', 1, 1);


SELECT Id, Nombre
FROM SmileAppDb.dbo.Roles;