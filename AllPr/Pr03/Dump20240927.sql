-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: autosalon
-- ------------------------------------------------------
-- Server version	8.0.39

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
-- Table structure for table `auto`
--

DROP TABLE IF EXISTS `auto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `auto` (
  `auto_id` int NOT NULL AUTO_INCREMENT,
  `brand` varchar(45) NOT NULL,
  `model` varchar(45) NOT NULL,
  `year` int NOT NULL,
  `colour` varchar(45) NOT NULL,
  `engine_type` varchar(45) NOT NULL,
  `drive_type` varchar(45) NOT NULL,
  `price` float NOT NULL,
  PRIMARY KEY (`auto_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `auto`
--

LOCK TABLES `auto` WRITE;
/*!40000 ALTER TABLE `auto` DISABLE KEYS */;
INSERT INTO `auto` VALUES (1,'Lada','Priora 1',2012,'Black','1.6 L','FWD',350000),(2,'Lada','Granta',2013,'White','1.6 L','FWD',500000),(3,'BMW','1 Series',2015,'Blue','2 L','RWD',1000000),(4,'BMW','X6',2021,'Teal Green','4.4 L','AWD',8000000),(5,'Ford','F-150',2023,'Red','3.5 L','4WD',3250000),(6,'Ford','F-150',2023,'Blue','5 L','4WD',4330000),(7,'Chevrolet','Camaro',2019,'Yellow','6.2 L','RWD',9000000),(8,'RAM','TRX',2021,'Silver','6.2 L','4WD',12000000),(9,'GAZ','Volga',1998,'Grey','2.4 L','RWD',450000),(10,'Volkswagen','Polo',2013,'Black','1.6 L','FWD',600000);
/*!40000 ALTER TABLE `auto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `client`
--

DROP TABLE IF EXISTS `client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `client` (
  `client_id` int NOT NULL AUTO_INCREMENT,
  `surname` varchar(45) NOT NULL,
  `name` varchar(45) NOT NULL,
  `middle_name` varchar(45) DEFAULT NULL,
  `phone_number` varchar(45) NOT NULL,
  `email` varchar(50) DEFAULT NULL,
  `adress` varchar(90) DEFAULT NULL,
  PRIMARY KEY (`client_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client`
--

LOCK TABLES `client` WRITE;
/*!40000 ALTER TABLE `client` DISABLE KEYS */;
INSERT INTO `client` VALUES (1,'Sidorkin','Daniil','Dmitrievich','+79991234567','sidorkin@gmail.com','Moscow, Russia'),(2,'Ivanov','Ivan','Ivanovich','+79997654321',NULL,'New York, USA'),(3,'Myers','Isabel',NULL,'+34123456789','Isamyr@gmail.com','Madrid, Spain'),(4,'Jordan','Michael',NULL,'+525511223344','NikeJordan@gmail.com','Florida, USA'),(5,'Myers','Emma',NULL,'+82105551234','Emmamyers@gmail.com','Seoul, South Korea'),(6,'Muravyov','Maxim','Alekseevich','+393331234567','muravyer@mail.ru','Voronezh, Russia'),(7,'Baryshev','Ivan','Romanovich','+441234567890','kobani@mail.ru','Smirino, Russia'),(8,'Jordan','BeeBee',NULL,'+49891234567','BeeBeeJ@gmail.com','New York, USA'),(9,'Shikhanova','Daria','Sergeevna','+33612345678','DariaDora@yandex.ru','Moscow, Russia'),(10,'Sudarkin','Denchik','Alekseevich','+61212345678','Sidor52@gmail.com','Nizhniy Novgorod, Russia');
/*!40000 ALTER TABLE `client` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employee` (
  `employee_id` int NOT NULL AUTO_INCREMENT,
  `surname` varchar(45) NOT NULL,
  `name` varchar(45) NOT NULL,
  `middle_name` varchar(45) DEFAULT NULL,
  `phone_number` varchar(45) NOT NULL,
  `job_title` varchar(45) NOT NULL,
  `adress` varchar(90) CHARACTER SET armscii8 COLLATE armscii8_general_ci DEFAULT NULL,
  PRIMARY KEY (`employee_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employee`
--

LOCK TABLES `employee` WRITE;
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
INSERT INTO `employee` VALUES (1,'Ivanova','Anastasia	','Vladimirovna','+75678901235','Manager','Saint Petersburg, Russia'),(2,'Petrov','Petr','Petrovich','+72345678901','Analyst','Vladivostok, Russia'),(3,'Sidorov','Sidor','Sidorovich','+73456789012','Manager','Ufa, Lenin Avenue'),(4,'Smirnov','Alexey','Sergeevich','+74567890123','Consultant','Nizhny Novgorod, Minin St'),(5,'Kuznetsov','Sergey','Mikhailovich','+77890123456','Consultant','Kazan, Russia'),(6,'Sokolov','Pavel','Petrovich','+70123456789','Seller','Samara, Russia'),(7,'Novikov','Nikolay','Dmitrievich','+71234567891','Controller','Lviv, Ukraine'),(8,'Orlov','Dmitriy','Ivanovich','+72345678902','Consultant','Chelyabinsk, Russia'),(9,'Petrenko','Igor','Sergeevich','+74567890124','Analyst','Tyumen, Russia'),(10,'Orlov','Alexey','Ivanovich','+75678901235','Seller','Chelyabinsk, Russia');
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment_method`
--

DROP TABLE IF EXISTS `payment_method`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payment_method` (
  `payment_method_id` int NOT NULL AUTO_INCREMENT,
  `payment_date` date NOT NULL,
  `payment_method` varchar(45) NOT NULL,
  `payment_amount` float NOT NULL,
  PRIMARY KEY (`payment_method_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_method`
--

LOCK TABLES `payment_method` WRITE;
/*!40000 ALTER TABLE `payment_method` DISABLE KEYS */;
INSERT INTO `payment_method` VALUES (1,'2024-10-12','Credit Card',350000),(2,'2024-01-15','Cash',3250000),(3,'2024-02-12','PayPal',4330000),(4,'2024-10-22','PayPal',4330000),(5,'2024-05-01','Credit Card',8000000),(6,'2024-06-26','Credit Card',9000000),(7,'2024-09-29','Cash',350000),(8,'2024-04-30','Cash',450000),(9,'2024-12-12','Credit Card',600000),(10,'2024-07-07','Cash',8000000);
/*!40000 ALTER TABLE `payment_method` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transaction_purchase_sale`
--

DROP TABLE IF EXISTS `transaction_purchase_sale`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transaction_purchase_sale` (
  `sales_id` int NOT NULL AUTO_INCREMENT,
  `client_id` int NOT NULL,
  `auto_id` int NOT NULL,
  `transaction_date` date NOT NULL,
  `payment_method_id` int NOT NULL,
  `employee_id` int NOT NULL,
  PRIMARY KEY (`sales_id`),
  KEY `fk_client_id_idx` (`client_id`),
  KEY `fk_auto_id_idx` (`auto_id`),
  KEY `fk_payment_method_id_idx` (`payment_method_id`),
  KEY `employee_id_idx` (`employee_id`),
  KEY `fk_sales_id_idx` (`sales_id`),
  CONSTRAINT `fk_auto_id` FOREIGN KEY (`auto_id`) REFERENCES `auto` (`auto_id`),
  CONSTRAINT `fk_client_id` FOREIGN KEY (`client_id`) REFERENCES `client` (`client_id`),
  CONSTRAINT `fk_employee_id` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`),
  CONSTRAINT `fk_payment_method_id` FOREIGN KEY (`payment_method_id`) REFERENCES `payment_method` (`payment_method_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transaction_purchase_sale`
--

LOCK TABLES `transaction_purchase_sale` WRITE;
/*!40000 ALTER TABLE `transaction_purchase_sale` DISABLE KEYS */;
INSERT INTO `transaction_purchase_sale` VALUES (1,3,6,'2024-05-13',1,4),(2,1,4,'2024-10-06',2,1),(3,1,4,'2024-10-14',5,8),(4,6,5,'2024-04-05',7,9),(5,8,9,'2024-02-25',6,10),(6,4,1,'2024-08-17',4,5),(7,7,5,'2024-11-12',10,3),(8,6,1,'2024-09-19',8,2),(9,10,9,'2024-03-11',9,7),(10,6,7,'2024-10-22',5,8);
/*!40000 ALTER TABLE `transaction_purchase_sale` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'autosalon'
--

--
-- Dumping routines for database 'autosalon'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-09-27 17:10:08
