DROP DATABASE IF EXISTS `ahorcado`;

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
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jugadores`
--

LOCK TABLES `jugadores` WRITE;
/*!40000 ALTER TABLE `jugadores` DISABLE KEYS */;
INSERT INTO `jugadores` VALUES (1,'admin','1234',0,'Administrador'),(2,'serena','1234',48,'Jugador'),(3,'alejandro','12343',5,'Jugador'),(4,'noelia','1234',109,'Jugador'),(6,'Romero','1234',16,'Jugador'),(8,'Lucia','1234',0,'Jugador'),(39,'Rodrigo','1234',0,'Jugador'),(40,'Marcos','1234',0,'Jugador'),(41,'javier','1234',36,'Jugador'),(42,'julian','1234',22,'Jugador');
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
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `palabras`
--

LOCK TABLES `palabras` WRITE;
/*!40000 ALTER TABLE `palabras` DISABLE KEYS */;
INSERT INTO `palabras` VALUES (1,'Huelva','es la ciudad más antigua de occidente','Historia'),(2,'Plus ultra','Fue el primer hidroavion en realizar un vuelo entre España y Sudamérica, partiendo desde Palos de la Frontera en 1926.','Historia'),(3,'Colon','Descubridor del nuevo mundo','Historia'),(4,'El quijote','En un lugar de La Mancha, de cuyo nombre no quiero acordarme...','Libros'),(5,'Platero y yo',' es pequeño, peludo, suave; tan blando por fuera, que se diría todo de algodón, que no lleva huesos.','Libros'),(6,'Dracula','Abraham \"Bram\" Stoker fue novelista y escritor irlandés, conocido por su novela...','Libros'),(7,'La historia interminable','pista para la historia interminable','Libros'),(8,'Rapela Felix','Minero de Nerva conocido por su buen humorrr','Historia');
/*!40000 ALTER TABLE `palabras` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `partidas`
--

DROP TABLE IF EXISTS `partidas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `partidas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `idJugador` int DEFAULT NULL,
  `puntuacion` int DEFAULT NULL,
  `fecha` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `idJugador` (`idJugador`),
  CONSTRAINT `partidas_ibfk_1` FOREIGN KEY (`idJugador`) REFERENCES `jugadores` (`idJugador`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `partidas`
--

LOCK TABLES `partidas` WRITE;
/*!40000 ALTER TABLE `partidas` DISABLE KEYS */;
INSERT INTO `partidas` VALUES (4,4,3,'2024-09-05 12:24:13'),(11,3,5,'2024-02-09 13:09:54'),(13,2,18,'2024-02-09 13:11:45'),(14,2,12,'2024-02-09 13:14:26'),(15,4,31,'2024-02-09 13:16:49'),(16,4,26,'2024-02-09 18:21:25'),(17,4,14,'2024-02-09 18:23:53'),(18,4,19,'2024-02-09 18:30:46'),(19,4,16,'2023-08-09 18:33:15'),(22,6,16,'2024-02-10 16:45:59'),(23,2,18,'2024-02-11 21:10:14'),(28,41,15,'2024-02-11 23:33:48'),(29,41,21,'2024-02-11 23:34:45'),(30,42,22,'2024-02-11 23:40:42');
/*!40000 ALTER TABLE `partidas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-02-11 23:44:05
