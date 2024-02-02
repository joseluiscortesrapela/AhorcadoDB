CREATE DATABASE  IF NOT EXISTS `ahorcado` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `ahorcado`;
-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: ahorcado
-- ------------------------------------------------------
-- Server version	8.0.35

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
-- Table structure for table `jugadores`
--

DROP TABLE IF EXISTS `jugadores`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `jugadores` (
  `idJugador` int NOT NULL AUTO_INCREMENT,
  `jugador` varchar(45) NOT NULL,
  `contraseña` varchar(64) NOT NULL,
  `puntuacion` int NOT NULL DEFAULT '0',
  `tipo` varchar(15) NOT NULL DEFAULT 'Jugador',
  PRIMARY KEY (`idJugador`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jugadores`
--

LOCK TABLES `jugadores` WRITE;
/*!40000 ALTER TABLE `jugadores` DISABLE KEYS */;
INSERT INTO `jugadores` VALUES (1,'jose','admin',0,'Administrador'),(2,'serena','1234',0,'Jugador'),(3,'alejandro','12343',0,'Jugador'),(4,'maria','1234',0,'Jugador'),(5,'mariano','2345',0,'Jugador'),(6,'Romero','2333',0,'Jugador'),(7,'Javier','1234',0,'Jugador'),(8,'Lucia','1234',0,'Jugador'),(9,'Manolo','1234',0,'Jugador'),(10,'Jesus','1234',0,'Jugador'),(11,'Manuel','12345',0,'Jugador');
/*!40000 ALTER TABLE `jugadores` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `palabras`
--

DROP TABLE IF EXISTS `palabras`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `palabras` (
  `idPalabra` int NOT NULL AUTO_INCREMENT,
  `palabra` varchar(50) DEFAULT NULL,
  `pista` varchar(150) DEFAULT NULL,
  `categoria` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idPalabra`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `palabras`
--

LOCK TABLES `palabras` WRITE;
/*!40000 ALTER TABLE `palabras` DISABLE KEYS */;
INSERT INTO `palabras` VALUES (1,'Huelva','es la ciudad más antigua de occidente','Historia'),(2,'Plus ultra','Fue el primer hidroavion en realizar un vuelo entre España y Sudamérica, partiendo desde Palos de la Frontera en 1926.','Historia'),(3,'Colon','Descubridor del nuevo mundo','Historia'),(4,'El quijote','En un lugar de La Mancha, de cuyo nombre no quiero acordarme...','Libros'),(5,'Platero y yo',' es pequeño, peludo, suave; tan blando por fuera, que se diría todo de algodón, que no lleva huesos.','Libros'),(6,'Dracula','Abraham \"Bram\" Stoker fue un novelista y escritor irlandés, conocido por su novela...','Libros'),(7,'La historia interminable','pista para la historia interminable','Libros');
/*!40000 ALTER TABLE `palabras` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-02-02 15:45:35
