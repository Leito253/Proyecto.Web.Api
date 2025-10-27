USE EventosBD
DROP USER IF EXISTS "Administrador"@"localhost";
DROP USER IF EXISTS 'Cliente'@'%';
CREATE USER "Administrador"@"localhost" IDENTIFIED BY 'Admin123';
GRANT ALL ON EventosBD.* TO "Administrador"@"localhost";
CREATE USER "Cliente"@"%" IDENTIFIED BY 'Cliente123';
GRANT SELECT ON Eventos TO 'Cliente'@'%';
GRANT SELECT ON Funcion TO 'Cliente'@'%';
GRANT SELECT ON Local TO 'Cliente'@'%';
GRANT SELECT, INSERT ON Entrada TO 'Cliente'@'%';
GRANT SELECT ON Tarifa TO 'Cliente'@'%';
GRANT SELECT ON Sector TO 'Cliente'@'%';