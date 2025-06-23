-- Roles y Seguridad
CREATE DATABASE SmileAppDb;

USE SmileAppDb;
GO

INSERT INTO Roles (Nombre) VALUES ('Administrador');
INSERT INTO Roles (Nombre) VALUES ('Dentista');

USE SmileAppDb;
GO

INSERT INTO Usuarios (Nombre, Correo, Contrase�aHash, RolId, Estado)
VALUES ('Juan P�rez', 'admin2@clinica.com', 'admin123', 1, 1);

INSERT INTO Usuarios (Nombre, Correo, Contrase�aHash, RolId, Estado)
VALUES ('Carlos Sol�s', 'dentista2@clinica.com', 'dentista123', 2, 1);

SELECT Id, Nombre
FROM SmileAppDb.dbo.Roles;

SELECT 
    u.Id AS UsuarioId,
    u.Nombre AS NombreUsuario,
    u.Correo,
    u.Contrase�aHash,
    u.Estado,
    u.FechaRegistro,
    r.Nombre AS RolNombre
FROM SmileAppDb.dbo.Usuarios u
LEFT JOIN SmileAppDb.dbo.Roles r ON u.RolId = r.Id;