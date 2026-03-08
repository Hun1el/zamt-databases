-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: 14
-- ------------------------------------------------------
-- Server version	8.0.30

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
-- Table structure for table `Flight`
--

DROP TABLE IF EXISTS `Flight`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Flight` (
  `id` int NOT NULL AUTO_INCREMENT,
  `trainnumber` varchar(20) NOT NULL,
  `station` varchar(50) NOT NULL,
  `DepartureTime` datetime NOT NULL,
  `ArrivalTime` datetime NOT NULL,
  `trainpath` varchar(90) NOT NULL,
  `pathprice` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Flight`
--

LOCK TABLES `Flight` WRITE;
/*!40000 ALTER TABLE `Flight` DISABLE KEYS */;
INSERT INTO `Flight` VALUES (1,'1','Победа','2025-01-25 15:30:00','2025-01-25 16:30:00','Москва-Нижний',1000),(2,'2','Революция','2025-01-25 16:30:00','2025-01-25 18:30:00','Москва-Питер',2000),(3,'3','Московская','2024-01-22 12:30:00','2024-01-22 14:30:00','Нижний-Москва',3000),(4,'4','Нижегородская','2024-02-12 13:00:00','2024-02-12 16:00:00','Москва-Заволжье',2500),(5,'5','Союз','2024-02-01 15:30:00','2024-02-01 17:30:00','Питер-Москва',1500);
/*!40000 ALTER TABLE `Flight` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Passenger`
--

DROP TABLE IF EXISTS `Passenger`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Passenger` (
  `id` int NOT NULL AUTO_INCREMENT,
  `surname` varchar(50) NOT NULL,
  `firstname` varchar(50) NOT NULL,
  `middlename` varchar(50) DEFAULT NULL,
  `passportnumber` varchar(20) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `passportnumber` (`passportnumber`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Passenger`
--

LOCK TABLES `Passenger` WRITE;
/*!40000 ALTER TABLE `Passenger` DISABLE KEYS */;
INSERT INTO `Passenger` VALUES (1,'Муравев','Максим','Андреевич','514682'),(2,'Кирсанов','Даниил','Даниилович','582846'),(3,'Чирков','Роман','Романович','378127'),(4,'Судоверкин','Иван','Семенович','536732'),(5,'Барышев','Иван','Петрович','537891');
/*!40000 ALTER TABLE `Passenger` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Roles`
--

DROP TABLE IF EXISTS `Roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Roles` (
  `id` int NOT NULL AUTO_INCREMENT,
  `role_name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Roles`
--

LOCK TABLES `Roles` WRITE;
/*!40000 ALTER TABLE `Roles` DISABLE KEYS */;
INSERT INTO `Roles` VALUES (1,'Администратор'),(2,'Кассир');
/*!40000 ALTER TABLE `Roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TrainTicket`
--

DROP TABLE IF EXISTS `TrainTicket`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TrainTicket` (
  `id` int NOT NULL AUTO_INCREMENT,
  `idpassenger` int NOT NULL,
  `idflight` int NOT NULL,
  `price` int NOT NULL,
  `seatnumber` varchar(10) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idpassenger` (`idpassenger`),
  KEY `idflight` (`idflight`),
  CONSTRAINT `trainticket_ibfk_1` FOREIGN KEY (`idpassenger`) REFERENCES `Passenger` (`id`),
  CONSTRAINT `trainticket_ibfk_2` FOREIGN KEY (`idflight`) REFERENCES `Flight` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TrainTicket`
--

LOCK TABLES `TrainTicket` WRITE;
/*!40000 ALTER TABLE `TrainTicket` DISABLE KEYS */;
INSERT INTO `TrainTicket` VALUES (1,1,3,3000,'64'),(2,2,4,2500,'15'),(3,3,5,1500,'2'),(4,4,2,2000,'6'),(5,5,1,1000,'1'),(6,1,4,2500,'71'),(7,1,4,2500,'72'),(8,1,4,2500,'73'),(9,2,1,1000,'22'),(10,3,5,1500,'12'),(11,4,5,1500,'13'),(13,4,3,3000,'62');
/*!40000 ALTER TABLE `TrainTicket` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `last_name` varchar(100) NOT NULL,
  `first_name` varchar(100) NOT NULL,
  `middle_name` varchar(100) DEFAULT NULL,
  `login_name` varchar(50) NOT NULL,
  `password_hash` varchar(64) NOT NULL,
  `role` int NOT NULL,
  `data_created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `login_name_UNIQUE` (`login_name`),
  KEY `FK_role_idx` (`role`),
  CONSTRAINT `FK_role` FOREIGN KEY (`role`) REFERENCES `Roles` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES (6,'Муравьев','Максим','Алексеевич','muravev52','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,'2025-01-29 20:30:12'),(7,'Беляева','Кристина','Олеговна','cryystal','ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f',1,'2025-01-29 20:31:44'),(8,'Сидоркин','Даниил','Дмитриевич','sidr','c6ba91b90d922e159893f46c387e5dc1b3dc5c101a5a4522f03b987177a24a91',1,'2025-01-29 20:35:06');
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database '14'
--

--
-- Dumping routines for database '14'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-01-29 20:36:13
