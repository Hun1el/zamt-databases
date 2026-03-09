CREATE DATABASE  IF NOT EXISTS `15` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `15`;
-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: 15
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
-- Table structure for table `Check`
--

DROP TABLE IF EXISTS `Check`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Check` (
  `id` int NOT NULL AUTO_INCREMENT,
  `med_name` varchar(100) NOT NULL,
  `quantity` int NOT NULL,
  `unit_price` int NOT NULL,
  `total_price` int GENERATED ALWAYS AS ((`quantity` * `unit_price`)) VIRTUAL,
  `payment_method` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Check`
--

LOCK TABLES `Check` WRITE;
/*!40000 ALTER TABLE `Check` DISABLE KEYS */;
INSERT INTO `Check` (`id`, `med_name`, `quantity`, `unit_price`, `payment_method`) VALUES (1,'Таблетки от головы',2,1000,'Карта'),(2,'Полисорб',5,500,'Наличка'),(3,'Активированный уголь',1,100,'Наличка'),(4,'Бинты',1,150,'Наличка'),(5,'Медицинский спирт',2,600,'Карта'),(6,'Витаминки',1,149,'Карта'),(7,'Аспирин',4,170,'Карта'),(8,'Корега',2,400,'Карта'),(9,'Гомеовокс',1,600,'Наличка'),(10,'Стодаль',4,476,'Карта');
/*!40000 ALTER TABLE `Check` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database '15'
--

--
-- Dumping routines for database '15'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-01-30 17:25:49
