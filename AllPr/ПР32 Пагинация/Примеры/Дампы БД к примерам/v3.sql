CREATE DATABASE  IF NOT EXISTS `db49` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `db49`;
-- MySQL dump 10.13  Distrib 8.0.28, for Win64 (x86_64)
--
-- Host: 10.207.106.12    Database: db49
-- ------------------------------------------------------
-- Server version	8.0.31

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
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `category` (
  `CategoryID` int NOT NULL AUTO_INCREMENT,
  `CategoryName` varchar(100) NOT NULL,
  `CategoryDescription` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` VALUES (1,'Вилки',NULL),(2,'Ложки',NULL),(3,'Наборы',NULL),(4,'Ножи',NULL),(5,'fgdfg',NULL);
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order` (
  `OrderID` int NOT NULL AUTO_INCREMENT,
  `OrderDate` date NOT NULL,
  `OrderDeliveryDate` date NOT NULL,
  `PickupPointID` int NOT NULL,
  `UserID` int NOT NULL,
  `OrderPickupCode` int NOT NULL,
  `OrderStatus` enum('Завершен','Новый','Корзина') NOT NULL,
  PRIMARY KEY (`OrderID`),
  KEY `PickupPointID` (`PickupPointID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `order_ibfk_1` FOREIGN KEY (`PickupPointID`) REFERENCES `pickuppoint` (`PickupPointID`),
  CONSTRAINT `order_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES `order` WRITE;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
INSERT INTO `order` VALUES (1,'2016-05-20','2022-05-20',1,1,801,'Завершен'),(2,'2016-05-20','2022-05-20',14,2,802,'Новый'),(3,'2017-05-20','2023-05-20',2,7,803,'Новый'),(4,'2017-05-20','2023-05-20',22,13,804,'Новый'),(5,'2019-05-20','2025-05-20',2,16,805,'Новый'),(6,'2020-05-20','2026-05-20',28,17,806,'Новый'),(7,'2022-05-20','2028-05-20',3,18,807,'Новый'),(8,'2022-05-20','2028-05-20',32,22,808,'Новый'),(9,'2024-05-20','2030-05-20',5,24,809,'Новый'),(10,'2024-05-20','2030-05-20',36,25,810,'Новый');
/*!40000 ALTER TABLE `order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderproduct`
--

DROP TABLE IF EXISTS `orderproduct`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderproduct` (
  `OrderID` int NOT NULL,
  `ProductArticleNumber` varchar(100) NOT NULL,
  `OrderProductCount` int NOT NULL,
  PRIMARY KEY (`OrderID`,`ProductArticleNumber`),
  KEY `ProductArticleNumber` (`ProductArticleNumber`),
  CONSTRAINT `orderproduct_ibfk_1` FOREIGN KEY (`OrderID`) REFERENCES `order` (`OrderID`),
  CONSTRAINT `orderproduct_ibfk_2` FOREIGN KEY (`ProductArticleNumber`) REFERENCES `product` (`ProductArticleNumber`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderproduct`
--

LOCK TABLES `orderproduct` WRITE;
/*!40000 ALTER TABLE `orderproduct` DISABLE KEYS */;
INSERT INTO `orderproduct` VALUES (1,'T793T4',3),(1,'А112Т4',2),(2,'F573T5',10),(2,'G387Y6',16),(3,'B736H6',20),(3,'D735T5',20),(4,'H384H3',2),(4,'K437E6',2),(5,'E732R7',4),(5,'R836H6',3),(6,'F839R6',4),(6,'G304H6',4),(7,'C430T4',10),(7,'C946Y6',3),(8,'B963H5',5),(8,'V403G6',5),(9,'V026J4',2),(9,'V727Y6',2),(10,'C635Y6',2),(10,'W405G6',2);
/*!40000 ALTER TABLE `orderproduct` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pickuppoint`
--

DROP TABLE IF EXISTS `pickuppoint`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pickuppoint` (
  `PickupPointID` int NOT NULL AUTO_INCREMENT,
  `PickupPointAddress` varchar(100) NOT NULL,
  PRIMARY KEY (`PickupPointID`)
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pickuppoint`
--

LOCK TABLES `pickuppoint` WRITE;
/*!40000 ALTER TABLE `pickuppoint` DISABLE KEYS */;
INSERT INTO `pickuppoint` VALUES (1,'344288, г. Талнах, ул. Чехова, 1'),(2,'614164, г.Талнах,  ул. Степная, 30'),(3,'394242, г. Талнах, ул. Коммунистическая, 43'),(4,'660540, г. Талнах, ул. Солнечная, 25'),(5,'125837, г. Талнах, ул. Шоссейная, 40'),(6,'125703, г. Талнах, ул. Партизанская, 49'),(7,'625283, г. Талнах, ул. Победы, 46'),(8,'614611, г. Талнах, ул. Молодежная, 50'),(9,'454311, г.Талнах, ул. Новая, 19'),(10,'660007, г.Талнах, ул. Октябрьская, 19'),(11,'603036, г. Талнах, ул. Садовая, 4'),(12,'450983, г.Талнах, ул. Комсомольская, 26'),(13,'394782, г. Талнах, ул. Чехова, 3'),(14,'603002, г. Талнах, ул. Дзержинского, 28'),(15,'450558, г. Талнах, ул. Набережная, 30'),(16,'394060, г.Талнах, ул. Фрунзе, 43'),(17,'410661, г. Талнах, ул. Школьная, 50'),(18,'625590, г. Талнах, ул. Коммунистическая, 20'),(19,'625683, г. Талнах, ул. 8 Марта'),(20,'400562, г. Талнах, ул. Зеленая, 32'),(21,'614510, г. Талнах, ул. Маяковского, 47'),(22,'410542, г. Талнах, ул. Светлая, 46'),(23,'620839, г. Талнах, ул. Цветочная, 8'),(24,'443890, г. Талнах, ул. Коммунистическая, 1'),(25,'603379, г. Талнах, ул. Спортивная, 46'),(26,'603721, г. Талнах, ул. Гоголя, 41'),(27,'410172, г. Талнах, ул. Северная, 13'),(28,'420151, г. Талнах, ул. Вишневая, 32'),(29,'125061, г. Талнах, ул. Подгорная, 8'),(30,'630370, г. Талнах, ул. Шоссейная, 24'),(31,'614753, г. Талнах, ул. Полевая, 35'),(32,'426030, г. Талнах, ул. Маяковского, 44'),(33,'450375, г. Талнах ул. Клубная, 44'),(34,'625560, г. Талнах, ул. Некрасова, 12'),(35,'630201, г. Талнах, ул. Комсомольская, 17'),(36,'190949, г. Талнах, ул. Мичурина, 26');
/*!40000 ALTER TABLE `pickuppoint` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `ProductArticleNumber` varchar(10) NOT NULL,
  `ProductName` text NOT NULL,
  `UnitID` int NOT NULL,
  `ProductCost` decimal(19,4) NOT NULL,
  `ProductDiscountMax` tinyint DEFAULT NULL,
  `ProductDealer` varchar(50) DEFAULT NULL,
  `SupplierID` int NOT NULL,
  `CategoryID` int NOT NULL,
  `ProductDiscountAmount` tinyint DEFAULT NULL,
  `ProductQuantityInStock` int NOT NULL,
  `ProductDescription` text NOT NULL,
  `ProductPhoto` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ProductArticleNumber`),
  KEY `UnitID` (`UnitID`),
  KEY `SupplierID` (`SupplierID`),
  KEY `CategoryID` (`CategoryID`),
  CONSTRAINT `product_ibfk_1` FOREIGN KEY (`UnitID`) REFERENCES `unit` (`UnitID`),
  CONSTRAINT `product_ibfk_2` FOREIGN KEY (`SupplierID`) REFERENCES `supplier` (`SupplierID`),
  CONSTRAINT `product_ibfk_3` FOREIGN KEY (`CategoryID`) REFERENCES `category` (`CategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES ('B736H6','Вилка столовая',1,220.0000,5,'Alaska',2,1,3,4,'Вилка столовая 21,2 см «Аляска бэйсик» сталь KunstWerk','B736H6.jpg'),('B963H5','Ложка',1,800.0000,5,'Smart Home',2,2,3,8,'Ложка 21 мм металлическая (медная) (Упаковка 10 шт)',''),('C430T4','Набор на одну персону',1,1600.0000,30,'Attribute',2,3,3,6,'Набор на одну персону (4 предмета) серия \"Bistro\"\", нерж. сталь, Was, Германия.\"',''),('C635Y6','Детский столовый набор',1,1000.0000,15,'Apollo',1,3,4,25,'Детский столовый набор Fissman «Зебра» ',''),('C730R7','Ложка детская',1,300.0000,5,'Smart Home',2,2,3,17,'Ложка детская столовая',''),('C943G5','Набор чайных ложек',1,200.0000,5,'Attribute',1,3,4,12,'Attribute Набор чайных ложек Baguette 3 предмета серебристый',''),('C946Y6','Вилка столовая',1,300.0000,15,'Apollo',2,1,2,16,'Вилка детская столовая',''),('D735T5','Ложка чайная',1,220.0000,5,'Alaska',2,2,2,13,'Ложка чайная ALASKA Eternum','D735T5.jpg'),('E732R7','Набор столовых приборов',1,990.0000,15,'Smart Home',1,3,5,6,'Набор столовых приборов Smart Home20 Black в подарочной коробке, 4 шт','E732R7.jpg'),('F392G6','Набор столовых приборов',1,490.0000,10,'Apollo',2,3,4,9,'Apollo Набор столовых приборов Chicago 4 предмета серебристый',''),('F573T5','Вилки столовые',1,650.0000,15,'Davinci',1,1,3,4,'Вилки столовые на блистере / 6 шт.','F573T5.jpg'),('F745K4','Столовые приборы для салата',1,2000.0000,10,'Mayer & Boch',2,3,3,2,'Столовые приборы для салата Orskov Lava, 2шт',''),('F839R6','Ложка чайная',1,400.0000,15,'Doria',1,2,2,6,'Ложка чайная DORIA Eternum',''),('G304H6','Набор ложек',1,500.0000,5,'Apollo',1,2,4,12,'Набор ложек столовых APOLLO \"Bohemica\"\" 3 пр.\"',''),('G387Y6','Ложка столовая',1,441.0000,5,'Doria',1,2,4,23,'Ложка столовая DORIA L=195/60 мм Eternum','G387Y6.jpg'),('H384H3','Набор столовых приборов',1,600.0000,15,'Apollo',1,3,2,9,'Набор столовых приборов для торта Palette 7 предметов серебристый','H384H3.jpg'),('H495H6','Набор стейковых ножей',1,7000.0000,15,'Mayer & Boch',2,4,2,15,'Набор стейковых ножей 4 пр. в деревянной коробке',''),('K437E6','Набор вилок',1,530.0000,5,'Apollo',1,3,3,16,'Набор вилок столовых APOLLO \"Aurora\"\" 3шт.\"','K437E6.jpg'),('L593H5','набор ножей',1,1300.0000,25,'Mayer & Boch',1,3,5,14,'Набор ножей Mayer & Boch, 4 шт',''),('N493G6','Набор для серверовки',1,2550.0000,15,'Smart Home',2,3,4,6,'Набор для сервировки сыра Select',''),('R836H6','Набор  столовых ножей',1,250.0000,5,'Attribute',2,4,3,16,'Attribute Набор столовых ножей Baguette 2 предмета серебристый','R836H6.jpg'),('S394J5','Набор чайных ложек',1,170.0000,5,'Attribute',2,3,3,4,'Attribute Набор чайных ложек Chaplet 3 предмета серебристый',''),('S395B5','Нож для стейка',1,600.0000,10,'Apollo',2,4,4,15,'Нож для стейка 11,5 см серебристый/черный',''),('T793T4','Набор ложек',1,250.0000,10,'Attribute',2,2,3,16,'Набор столовых ложек Baguette 3 предмета серебристый','T793T4.jpg'),('V026J4','набор ножей',1,700.0000,15,'Apollo',1,3,3,9,'абор ножей для стейка и пиццы Swiss classic 2 шт. желтый',''),('V403G6','Ложка чайная',1,600.0000,15,'Doria',1,2,5,24,'Ложка чайная DORIA Eternum',''),('V727Y6','Набор десертных ложек',1,3000.0000,10,'Mayer & Boch',2,2,4,8,'Набор десертных ложек на подставке Размер: 7*7*15 см',''),('W295Y5','Сервировочный набор для торта',1,1100.0000,15,'Attribute',1,3,2,16,'Набор сервировочный для торта \"Розанна\"\"\"',''),('W405G6','Набор столовых приборов',1,1300.0000,25,'Attribute',2,3,3,4,'Набор сервировочных столовых вилок Цветы',''),('А112Т4','Набор вилок',1,1600.0000,30,'Davinci',1,1,2,6,'Набор столовых вилок Davinci, 20 см 6 шт.','А112Т4.jpg');
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `RoleID` int NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(100) NOT NULL,
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'Клиент'),(2,'Менеджер'),(3,'Администратор');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supplier`
--

DROP TABLE IF EXISTS `supplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `supplier` (
  `SupplierID` int NOT NULL AUTO_INCREMENT,
  `SupplierTitle` varchar(150) NOT NULL,
  `SupplierDescription` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`SupplierID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supplier`
--

LOCK TABLES `supplier` WRITE;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
INSERT INTO `supplier` VALUES (1,'Максидом',NULL),(2,'LeroiMerlin',NULL);
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `unit`
--

DROP TABLE IF EXISTS `unit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `unit` (
  `UnitID` int NOT NULL AUTO_INCREMENT,
  `UnitName` varchar(100) NOT NULL,
  `UnitDescription` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`UnitID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `unit`
--

LOCK TABLES `unit` WRITE;
/*!40000 ALTER TABLE `unit` DISABLE KEYS */;
INSERT INTO `unit` VALUES (1,'шт.','Штуки');
/*!40000 ALTER TABLE `unit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `UserSurname` varchar(100) NOT NULL,
  `UserName` varchar(100) NOT NULL,
  `UserPatronymic` varchar(100) NOT NULL,
  `UserLogin` text NOT NULL,
  `UserPassword` text NOT NULL,
  `UserRole` int NOT NULL,
  PRIMARY KEY (`UserID`),
  KEY `UserRole` (`UserRole`),
  CONSTRAINT `user_ibfk_1` FOREIGN KEY (`UserRole`) REFERENCES `role` (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'Ефремов','Сергей','Пантелеймонович','loginDEppn2018','6}i+FD',1),(2,'Родионова','Тамара','Валентиновна','loginDElqb2018','RNynil',1),(3,'Миронова','Галина','Улебовна','loginDEydn2018','34I}X9',2),(4,'Сидоров','Роман','Иринеевич','loginDEijg2018','4QlKJW',2),(5,'Ситников','Парфений','Всеволодович','loginDEdpy2018','MJ0W|f',2),(6,'Никонов','Роман','Геласьевич','loginDEwdm2018','&PynqU',2),(7,'Щербаков','Владимир','Матвеевич','loginDEdup2018','JM+2{s',1),(8,'Кулаков','Мартын','Михаилович','loginDEhbm2018','9aObu4',2),(9,'Сазонова','Оксана','Лаврентьевна','loginDExvq2018','hX0wJz',3),(10,'Архипов','Варлам','Мэлорович','loginDErks2018','LQNSjo',2),(11,'Устинова','Ираида','Мэлоровна','loginDErvb2018','ceAf&R',3),(12,'Лукин','Георгий','Альбертович','loginDEulo2018','#ИМЯ?',3),(13,'Кононов','Эдуард','Валентинович','loginDEgfw2018','3c2Ic1',1),(14,'Орехова','Клавдия','Альбертовна','loginDEmxb2018','ZPXcRS',2),(15,'Яковлев','Яков','Эдуардович','loginDEgeq2018','&&Eim0',2),(16,'Воронов','Мэлс','Семёнович','loginDEkhj2018','Pbc0t{',1),(17,'Вишнякова','Ия','Данииловна','loginDEliu2018','32FyTl',1),(18,'Третьяков','Фёдор','Вадимович','loginDEsmf2018','{{O2QG',1),(19,'Макаров','Максим','Ильяович','loginDEutd2018','GbcJvC',2),(20,'Шубина','Маргарита','Анатольевна','loginDEpgh2018','YV2lvh',2),(21,'Блинова','Ангелина','Владленовна','loginDEvop2018','pBP8rO',2),(22,'Воробьёв','Владлен','Фролович','loginDEwjo2018','EQaD|d',1),(23,'Сорокина','Прасковья','Фёдоровна','loginDEbur2018','aZKGeI',2),(24,'Давыдов','Яков','Антонович','loginDEszw2018','EGU{YE',1),(25,'Рыбакова','Евдокия','Анатольевна','loginDExsu2018','*2RMsp',1),(26,'Маслов','Геннадий','Фролович','loginDEztn2018','nJBZpU',1),(27,'Цветкова','Элеонора','Аристарховна','loginDEtmn2018','UObB}N',1),(28,'Евдокимов','Ростислав','Александрович','loginDEhep2018','SwRicr',1),(29,'Никонова','Венера','Станиславовна','loginDEevr2018','zO5l}l',1),(30,'Громов','Егор','Антонович','loginDEnpa2018','M*QLjf',1),(31,'Суворова','Валерия','Борисовна','loginDEgyt2018','Pav+GP',3),(32,'Мишина','Елизавета','Романовна','loginDEbrr2018','Z7L|+i',1),(33,'Зимина','Ольга','Аркадьевна','loginDEyoo2018','UG1BjP',3),(34,'Игнатьев','Игнатий','Антонинович','loginDEaob2018','3fy+3I',3),(35,'Пахомова','Зинаида','Витальевна','loginDEwtz2018','&GxSST',1),(36,'Устинов','Владимир','Федосеевич','loginDEctf2018','sjt*3N',3),(37,'Кулаков','Мэлор','Вячеславович','loginDEipm2018','MAZl6|',2),(38,'Сазонов','Авксентий','Брониславович','loginDEjoi2018','o}C4jv',1),(39,'Бурова','Наина','Брониславовна','loginDEwap2018','4hny7k',2),(40,'Фадеев','Демьян','Федосеевич','loginDEaxm2018','BEc3xq',1),(41,'Бобылёва','Дарья','Якуновна','loginDEsmq2018','ATVmM7',1),(42,'Виноградов','Созон','Арсеньевич','loginDEeur2018','n4V{wP',1),(43,'Гордеев','Владлен','Ефимович','loginDEvke2018','WQLXSl',1),(44,'Иванова','Зинаида','Валерьевна','loginDEvod2018','0EW93v',2),(45,'Гусев','Руслан','Дамирович','loginDEjaw2018','h6z&Ky',1),(46,'Маслов','Дмитрий','Иванович','loginDEpdp2018','8NvRfC',2),(47,'Антонова','Ульяна','Семёновна','loginDEjpp2018','oMOQq3',1),(48,'Орехова','Людмила','Владимировна','loginDEkiy2018','BQzsts',2),(49,'Авдеева','Жанна','Куприяновна','loginDEhmn2018','a|Iz|7',2),(50,'Кузнецов','Фрол','Варламович','loginDEfmn2018','cw3|03',1),(51,'Иванов','Иван','Иванович','admin','admin',3),(52,'Петров','Петр','Петрович','user','user',1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'db49'
--

--
-- Dumping routines for database 'db49'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-02-16  9:21:51
