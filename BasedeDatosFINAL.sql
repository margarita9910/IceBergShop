-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: heisenberg
-- ------------------------------------------------------
-- Server version	8.0.20

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `productos`
--

DROP TABLE IF EXISTS `productos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productos` (
  `idProducto` int unsigned NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  `precio` decimal(10,2) unsigned NOT NULL,
  PRIMARY KEY (`idProducto`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos`
--

LOCK TABLES `productos` WRITE;
/*!40000 ALTER TABLE `productos` DISABLE KEYS */;
INSERT INTO `productos` VALUES (1,'Nieve Sencilla',16.00),(2,'Nieve Doble',24.00),(3,'Nieve Triple',31.00),(4,'Nieve de 1/2 L',30.00),(5,'Nieve de 1 Litro',56.00),(6,'Nieve de 5 Litros',160.00),(7,'Paleta de Fruta',8.00),(8,'Paleta de Crema',13.00),(9,'Paleta Especial',20.00),(10,'Frapuchino Chico',26.00),(11,'Frapuchino Grande',32.00),(12,'Chaskafruta Chica',26.00),(13,'Chaskafruta Grande',32.00),(14,'Chaskafruta Extra',3.00),(15,'Agua Chica',16.00),(16,'Agua Grande',22.00),(17,'Italiana Chica',16.00),(18,'Italiana Grande',32.00),(19,'Snacks Solos',16.00),(20,'Snacks con Queso Normal',26.00),(21,'Snacks con Queso Extra',32.00),(22,'Importe de Recipiente',30.00),(23,'Fresas Solas',17.00),(24,'Fresas Dobles',26.00),(25,'Fresas Triples',32.00);
/*!40000 ALTER TABLE `productos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sucursales`
--

DROP TABLE IF EXISTS `sucursales`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sucursales` (
  `idSucursales` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `direccion` varchar(250) NOT NULL,
  PRIMARY KEY (`idSucursales`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sucursales`
--

LOCK TABLES `sucursales` WRITE;
/*!40000 ALTER TABLE `sucursales` DISABLE KEYS */;
INSERT INTO `sucursales` VALUES (1,'IceBerg Aguascalientes','Av. de la Charrería XD'),(2,'IceBerg Villas','Villas del atracón');
/*!40000 ALTER TABLE `sucursales` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `idUsuario` int unsigned NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) DEFAULT NULL,
  `acceso` enum('C','A','S') NOT NULL,
  `contrasena` varchar(100) NOT NULL,
  `usuario` varchar(100) NOT NULL,
  `idsucursal` int DEFAULT NULL,
  PRIMARY KEY (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'Javier','S','123','123',1),(2,'Panchito Suárez','C','qwerty','qwerty',1),(4,'Albertano Santa Cruz','C','h6wppjxgfr','PedritoSola',1),(10,'Juan Reyes','C','123123','123123',1),(11,'Javier','C','123123','javier123',1),(12,'Margarita','C','123123123','123123',1);
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ventas`
--

DROP TABLE IF EXISTS `ventas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ventas` (
  `turno` int NOT NULL,
  `total` decimal(10,2) unsigned NOT NULL,
  `idUsuario` int unsigned NOT NULL,
  `idVenta` int unsigned NOT NULL AUTO_INCREMENT,
  `fecha` varchar(10) NOT NULL,
  `hora` varchar(8) NOT NULL,
  PRIMARY KEY (`idVenta`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ventas`
--

LOCK TABLES `ventas` WRITE;
/*!40000 ALTER TABLE `ventas` DISABLE KEYS */;
INSERT INTO `ventas` VALUES (1,62.00,2,4,'23/08/2022','05:39:10'),(1,146.00,2,5,'15/08/2022','09:27:36'),(2,327.00,2,6,'22/08/2022','09:28:55'),(1,590.00,2,7,'23/08/2022','09:44:59'),(1,277.00,10,8,'18/08/2022','14:44:11'),(-1,300.00,1,9,'23/08/2022','14:45:11'),(1,56.00,1,10,'24/08/2022','13:21:57'),(-1,10.00,1,11,'24/08/2022','13:31:06'),(-1,35.00,1,12,'24/08/2022','13:33:51'),(-1,10.00,1,13,'24/08/2022','13:39:07'),(1,16.00,1,14,'24/08/2022','14:22:09'),(1,16.00,1,15,'24/08/2022','14:31:56'),(2,32.00,1,16,'24/08/2022','14:32:33'),(1,16.00,1,17,'25/08/2022','09:24:10'),(-1,16.00,1,18,'25/08/2022','09:26:39'),(1,262.00,1,19,'29/08/2022','08:27:40'),(1,1000.00,1,20,'29/08/2022','08:41:01'),(1,403.00,1,21,'29/08/2022','14:42:16'),(-1,100.00,1,22,'29/08/2022','14:49:28'),(1,110.00,1,23,'29/08/2022','14:52:08'),(2,310.00,1,24,'29/08/2022','14:52:23'),(3,132.00,1,25,'29/08/2022','14:52:35'),(1,594.00,1,26,'30/08/2022','09:46:43'),(1,16.00,1,27,'31/08/2022','18:16:45'),(-1,10.00,1,28,'31/08/2022','22:29:45');
/*!40000 ALTER TABLE `ventas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'heisenberg'
--

--
-- Dumping routines for database 'heisenberg'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-08-31 22:49:39
