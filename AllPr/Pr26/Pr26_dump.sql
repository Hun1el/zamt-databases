CREATE DATABASE  IF NOT EXISTS `db67` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `db67`;
-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: db67
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
-- Table structure for table `Order`
--

DROP TABLE IF EXISTS `Order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Order` (
  `OrderID` int NOT NULL AUTO_INCREMENT,
  `OrderStatus` text NOT NULL,
  `OrderDeliveryDate` datetime NOT NULL,
  `OrderDate` datetime NOT NULL,
  `OrderPickupPoint` int NOT NULL,
  `OrderCode` int NOT NULL,
  `UserID` int NOT NULL,
  PRIMARY KEY (`OrderID`),
  KEY `fk_order_user_idx` (`UserID`),
  KEY `fk_order_pickuppoint_idx` (`OrderPickupPoint`),
  CONSTRAINT `fk_order_pickuppoint` FOREIGN KEY (`OrderPickupPoint`) REFERENCES `PickupPoint` (`PickupPointID`),
  CONSTRAINT `fk_order_user` FOREIGN KEY (`UserID`) REFERENCES `User` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Order`
--

LOCK TABLES `Order` WRITE;
/*!40000 ALTER TABLE `Order` DISABLE KEYS */;
INSERT INTO `Order` VALUES (1,'Завершен','2022-05-20 22:00:00','2016-05-20 22:00:00',1,801,47),(2,'Новый ','2022-05-20 22:00:00','2016-05-20 22:00:00',14,802,4),(3,'Новый ','2023-05-20 22:00:00','2017-05-20 22:00:00',2,803,8),(4,'Новый ','2023-05-20 22:00:00','2017-05-20 22:00:00',22,804,12),(5,'Новый ','2025-05-20 22:00:00','2019-05-20 22:00:00',2,805,18),(6,'Новый ','2026-05-20 22:00:00','2020-05-20 22:00:00',28,806,26),(7,'Новый ','2028-05-20 22:00:00','2022-05-20 22:00:00',3,807,35),(8,'Новый ','2028-05-20 22:00:00','2022-05-20 22:00:00',32,808,48),(9,'Новый ','2030-05-20 22:00:00','2024-05-20 22:00:00',5,809,5),(10,'Новый ','2030-05-20 22:00:00','2024-05-20 22:00:00',36,810,10);
/*!40000 ALTER TABLE `Order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `OrderProduct`
--

DROP TABLE IF EXISTS `OrderProduct`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `OrderProduct` (
  `OrderID` int NOT NULL,
  `ProductArticleNumber` varchar(6) NOT NULL,
  `ProductCount` int NOT NULL,
  PRIMARY KEY (`OrderID`,`ProductArticleNumber`),
  KEY `orderproduct_ibfk_2` (`ProductArticleNumber`),
  CONSTRAINT `orderproduct_ibfk_1` FOREIGN KEY (`OrderID`) REFERENCES `Order` (`OrderID`),
  CONSTRAINT `orderproduct_ibfk_2` FOREIGN KEY (`ProductArticleNumber`) REFERENCES `Product` (`ProductArticleNumber`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `OrderProduct`
--

LOCK TABLES `OrderProduct` WRITE;
/*!40000 ALTER TABLE `OrderProduct` DISABLE KEYS */;
INSERT INTO `OrderProduct` VALUES (1,'T793T4',3),(1,'А112Т4',2),(2,'F573T5',10),(2,'G387Y6',16),(3,'B736H6',20),(3,'D735T5',20),(4,'H384H3',2),(4,'K437E6',2),(5,'E732R7',4),(5,'R836H6',3),(6,'F839R6',4),(6,'G304H6',4),(7,'C430T4',10),(7,'C946Y6',3),(8,'B963H5',5),(8,'V403G6',5),(9,'V026J4',2),(9,'V727Y6',2),(10,'C635Y6',2),(10,'W405G6',2);
/*!40000 ALTER TABLE `OrderProduct` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PickupPoint`
--

DROP TABLE IF EXISTS `PickupPoint`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PickupPoint` (
  `PickupPointID` int NOT NULL AUTO_INCREMENT,
  `Address` text NOT NULL,
  PRIMARY KEY (`PickupPointID`)
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PickupPoint`
--

LOCK TABLES `PickupPoint` WRITE;
/*!40000 ALTER TABLE `PickupPoint` DISABLE KEYS */;
INSERT INTO `PickupPoint` VALUES (1,'344288, г. Талнах, ул. Чехова, 1'),(2,'614164, г.Талнах,  ул. Степная, 30'),(3,'394242, г. Талнах, ул. Коммунистическая, 43'),(4,'660540, г. Талнах, ул. Солнечная, 25'),(5,'125837, г. Талнах, ул. Шоссейная, 40'),(6,'125703, г. Талнах, ул. Партизанская, 49'),(7,'625283, г. Талнах, ул. Победы, 46'),(8,'614611, г. Талнах, ул. Молодежная, 50'),(9,'454311, г.Талнах, ул. Новая, 19'),(10,'660007, г.Талнах, ул. Октябрьская, 19'),(11,'603036, г. Талнах, ул. Садовая, 4'),(12,'450983, г.Талнах, ул. Комсомольская, 26'),(13,'394782, г. Талнах, ул. Чехова, 3'),(14,'603002, г. Талнах, ул. Дзержинского, 28'),(15,'450558, г. Талнах, ул. Набережная, 30'),(16,'394060, г.Талнах, ул. Фрунзе, 43'),(17,'410661, г. Талнах, ул. Школьная, 50'),(18,'625590, г. Талнах, ул. Коммунистическая, 20'),(19,'625683, г. Талнах, ул. 8 Марта'),(20,'400562, г. Талнах, ул. Зеленая, 32'),(21,'614510, г. Талнах, ул. Маяковского, 47'),(22,'410542, г. Талнах, ул. Светлая, 46'),(23,'620839, г. Талнах, ул. Цветочная, 8'),(24,'443890, г. Талнах, ул. Коммунистическая, 1'),(25,'603379, г. Талнах, ул. Спортивная, 46'),(26,'603721, г. Талнах, ул. Гоголя, 41'),(27,'410172, г. Талнах, ул. Северная, 13'),(28,'420151, г. Талнах, ул. Вишневая, 32'),(29,'125061, г. Талнах, ул. Подгорная, 8'),(30,'630370, г. Талнах, ул. Шоссейная, 24'),(31,'614753, г. Талнах, ул. Полевая, 35'),(32,'426030, г. Талнах, ул. Маяковского, 44'),(33,'450375, г. Талнах ул. Клубная, 44'),(34,'625560, г. Талнах, ул. Некрасова, 12'),(35,'630201, г. Талнах, ул. Комсомольская, 17'),(36,'190949, г. Талнах, ул. Мичурина, 26');
/*!40000 ALTER TABLE `PickupPoint` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Product`
--

DROP TABLE IF EXISTS `Product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Product` (
  `ProductArticleNumber` varchar(100) NOT NULL,
  `ProductName` text NOT NULL,
  `ProductDescription` text NOT NULL,
  `ProductCategory` text NOT NULL,
  `ProductPhoto` blob,
  `ProductManufacturer` text NOT NULL,
  `ProductCost` decimal(19,4) NOT NULL,
  `ProductDiscountAmount` tinyint DEFAULT NULL,
  `ProductQuantityInStock` int NOT NULL,
  `ProductUnit` varchar(50) NOT NULL,
  `ProductSupplier` int NOT NULL,
  `ProductCurrentDiscount` tinyint DEFAULT NULL,
  PRIMARY KEY (`ProductArticleNumber`),
  KEY `fk_product_supplier_idx` (`ProductSupplier`),
  CONSTRAINT `fk_product_supplier` FOREIGN KEY (`ProductSupplier`) REFERENCES `Supplier` (`SupplierID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Product`
--

LOCK TABLES `Product` WRITE;
/*!40000 ALTER TABLE `Product` DISABLE KEYS */;
INSERT INTO `Product` VALUES ('B736H6','Вилка столовая','Вилка столовая 21,2 см «Аляска бэйсик» сталь KunstWerk','Вилки',_binary 'B736H6.jpg','Alaska',220.0000,5,4,'шт.',2,3),('B963H5','Ложка','Ложка 21 мм металлическая (медная) (Упаковка 10 шт)','Ложки','','Smart Home',800.0000,5,8,'шт.',2,3),('C430T4','Набор на одну персону','Набор на одну персону (4 предмета) серия \"Bistro\"\", нерж. сталь, Was, Германия.\"','наборы','','Attribute',1600.0000,30,6,'шт.',2,3),('C635Y6','Детский столовый набор','Детский столовый набор Fissman «Зебра» ','наборы','','Apollo',1000.0000,15,25,'шт.',1,4),('C730R7','Ложка детская','Ложка детская столовая','Ложки','','Smart Home',300.0000,5,17,'шт.',2,3),('C943G5','Набор чайных ложек','Attribute Набор чайных ложек Baguette 3 предмета серебристый','наборы','','Attribute',200.0000,5,12,'шт.',1,4),('C946Y6','Вилка столовая','Вилка детская столовая','Вилки','','Apollo',300.0000,15,16,'шт.',2,2),('D735T5','Ложка чайная','Ложка чайная ALASKA Eternum','Ложки',_binary 'D735T5.jpg','Alaska',220.0000,5,13,'шт.',2,2),('E732R7','Набор столовых приборов','Набор столовых приборов Smart Home20 Black в подарочной коробке, 4 шт','наборы',_binary 'E732R7.jpg','Smart Home',990.0000,15,6,'шт.',1,5),('F392G6','Набор столовых приборов','Apollo Набор столовых приборов Chicago 4 предмета серебристый','наборы','','Apollo',490.0000,10,9,'шт.',2,4),('F573T5','Вилки столовые','Вилки столовые на блистере / 6 шт.','вилки',_binary 'F573T5.jpg','Davinci',650.0000,15,4,'шт.',1,3),('F745K4','Столовые приборы для салата','Столовые приборы для салата Orskov Lava, 2шт','наборы','','Mayer & Boch',2000.0000,10,2,'шт.',2,3),('F839R6','Ложка чайная','Ложка чайная DORIA Eternum','Ложки','','Doria',400.0000,15,6,'шт.',1,2),('G304H6','Набор ложек','Набор ложек столовых APOLLO \"Bohemica\"\" 3 пр.\"','Ложки','','Apollo',500.0000,5,12,'шт.',1,4),('G387Y6','Ложка столовая','Ложка столовая DORIA L=195/60 мм Eternum','Ложки',_binary 'G387Y6.jpg','Doria',441.0000,5,23,'шт.',1,4),('H384H3','Набор столовых приборов','Набор столовых приборов для торта Palette 7 предметов серебристый','наборы',_binary 'H384H3.jpg','Apollo',600.0000,15,9,'шт.',1,2),('H495H6','Набор стейковых ножей','Набор стейковых ножей 4 пр. в деревянной коробке','ножи','','Mayer & Boch',7000.0000,15,15,'шт.',2,2),('K437E6','Набор вилок','Набор вилок столовых APOLLO \"Aurora\"\" 3шт.\"','наборы',_binary 'K437E6.jpg','Apollo',530.0000,5,16,'шт.',1,3),('L593H5','набор ножей','Набор ножей Mayer & Boch, 4 шт','наборы','','Mayer & Boch',1300.0000,25,14,'шт.',1,5),('N493G6','Набор для серверовки','Набор для сервировки сыра Select','наборы','','Smart Home',2550.0000,15,6,'шт.',2,4),('R836H6','Набор  столовых ножей','Attribute Набор столовых ножей Baguette 2 предмета серебристый','ножи',_binary 'R836H6.jpg','Attribute',250.0000,5,16,'шт.',2,3),('S394J5','Набор чайных ложек','Attribute Набор чайных ложек Chaplet 3 предмета серебристый','наборы','','Attribute',170.0000,5,4,'шт.',2,3),('S395B5','Нож для стейка','Нож для стейка 11,5 см серебристый/черный','ножи','','Apollo',600.0000,10,15,'шт.',2,4),('T793T4','Набор ложек','Набор столовых ложек Baguette 3 предмета серебристый','Ложки',_binary 'T793T4.jpg','Attribute',250.0000,10,16,'шт.',2,3),('V026J4','набор ножей','абор ножей для стейка и пиццы Swiss classic 2 шт. желтый','наборы','','Apollo',700.0000,15,9,'шт.',1,3),('V403G6','Ложка чайная','Ложка чайная DORIA Eternum','Ложки','','Doria',600.0000,15,24,'шт.',1,5),('V727Y6','Набор десертных ложек','Набор десертных ложек на подставке Размер: 7*7*15 см','Ложки','','Mayer & Boch',3000.0000,10,8,'шт.',2,4),('W295Y5','Сервировочный набор для торта','Набор сервировочный для торта \"Розанна\"\"\"','наборы','','Attribute',1100.0000,15,16,'шт.',1,2),('W405G6','Набор столовых приборов','Набор сервировочных столовых вилок Цветы','наборы','','Attribute',1300.0000,25,4,'шт.',2,3),('А112Т4','Набор вилок','Набор столовых вилок Davinci, 20 см 6 шт.','Вилки',_binary 'А112Т4.jpg','Davinci',1600.0000,30,6,'шт.',1,2);
/*!40000 ALTER TABLE `Product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Role`
--

DROP TABLE IF EXISTS `Role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Role` (
  `RoleID` int NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(100) NOT NULL,
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Role`
--

LOCK TABLES `Role` WRITE;
/*!40000 ALTER TABLE `Role` DISABLE KEYS */;
INSERT INTO `Role` VALUES (1,'Администратор'),(2,'Менеджер'),(3,'Клиент');
/*!40000 ALTER TABLE `Role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Supplier`
--

DROP TABLE IF EXISTS `Supplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Supplier` (
  `SupplierID` int NOT NULL AUTO_INCREMENT,
  `SupplierName` varchar(100) NOT NULL,
  PRIMARY KEY (`SupplierID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Supplier`
--

LOCK TABLES `Supplier` WRITE;
/*!40000 ALTER TABLE `Supplier` DISABLE KEYS */;
INSERT INTO `Supplier` VALUES (1,'Максидом'),(2,'LeroiMerlin');
/*!40000 ALTER TABLE `Supplier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `User`
--

DROP TABLE IF EXISTS `User`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `User` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `UserSurname` varchar(100) NOT NULL,
  `UserName` varchar(100) NOT NULL,
  `UserPatronymic` varchar(100) NOT NULL,
  `UserLogin` text NOT NULL,
  `UserPassword` text NOT NULL,
  `UserRole` int NOT NULL,
  PRIMARY KEY (`UserID`),
  KEY `UserRole` (`UserRole`),
  CONSTRAINT `user_ibfk_1` FOREIGN KEY (`UserRole`) REFERENCES `Role` (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `User`
--

LOCK TABLES `User` WRITE;
/*!40000 ALTER TABLE `User` DISABLE KEYS */;
INSERT INTO `User` VALUES (1,'Ефремов ','Сергей','Пантелеймонович','loginDEppn2018','6}i+FD',3),(2,'Родионова ','Тамара','Валентиновна','loginDElqb2018','RNynil',3),(3,'Миронова ','Галина','Улебовна','loginDEydn2018','34I}X9',2),(4,'Сидоров ','Роман','Иринеевич','loginDEijg2018','4QlKJW',2),(5,'Ситников ','Парфений','Всеволодович','loginDEdpy2018','MJ0W|f',2),(6,'Никонов ','Роман','Геласьевич','loginDEwdm2018','&PynqU',2),(7,'Щербаков ','Владимир','Матвеевич','loginDEdup2018','JM+2{s',3),(8,'Кулаков ','Мартын','Михаилович','loginDEhbm2018','9aObu4',2),(9,'Сазонова ','Оксана','Лаврентьевна','loginDExvq2018','hX0wJz',1),(10,'Архипов ','Варлам','Мэлорович','loginDErks2018','LQNSjo',2),(11,'Устинова ','Ираида','Мэлоровна','loginDErvb2018','ceAf&R',1),(12,'Лукин ','Георгий','Альбертович','loginDEulo2018','zO5l}l',1),(13,'Кононов ','Эдуард','Валентинович','loginDEgfw2018','3c2Ic1',3),(14,'Орехова ','Клавдия','Альбертовна','loginDEmxb2018','ZPXcRS',2),(15,'Яковлев ','Яков','Эдуардович','loginDEgeq2018','&&Eim0',2),(16,'Воронов ','Мэлс','Семёнович','loginDEkhj2018','Pbc0t{',3),(17,'Вишнякова ','Ия','Данииловна','loginDEliu2018','32FyTl',3),(18,'Третьяков ','Фёдор','Вадимович','loginDEsmf2018','{{O2QG',3),(19,'Макаров ','Максим','Ильяович','loginDEutd2018','GbcJvC',2),(20,'Шубина ','Маргарита','Анатольевна','loginDEpgh2018','YV2lvh',2),(21,'Блинова ','Ангелина','Владленовна','loginDEvop2018','pBP8rO',2),(22,'Воробьёв ','Владлен','Фролович','loginDEwjo2018','EQaD|d',3),(23,'Сорокина ','Прасковья','Фёдоровна','loginDEbur2018','aZKGeI',2),(24,'Давыдов ','Яков','Антонович','loginDEszw2018','EGU{YE',3),(25,'Рыбакова ','Евдокия','Анатольевна','loginDExsu2018','*2RMsp',3),(26,'Маслов ','Геннадий','Фролович','loginDEztn2018','nJBZpU',3),(27,'Цветкова ','Элеонора','Аристарховна','loginDEtmn2018','UObB}N',3),(28,'Евдокимов ','Ростислав','Александрович','loginDEhep2018','SwRicr',3),(29,'Никонова ','Венера','Станиславовна','loginDEevr2018','zO5l}l',3),(30,'Громов ','Егор','Антонович','loginDEnpa2018','M*QLjf',3),(31,'Суворова ','Валерия','Борисовна','loginDEgyt2018','Pav+GP',1),(32,'Мишина ','Елизавета','Романовна','loginDEbrr2018','Z7L|+i',3),(33,'Зимина ','Ольга','Аркадьевна','loginDEyoo2018','UG1BjP',1),(34,'Игнатьев ','Игнатий','Антонинович','loginDEaob2018','3fy+3I',1),(35,'Пахомова ','Зинаида','Витальевна','loginDEwtz2018','&GxSST',3),(36,'Устинов ','Владимир','Федосеевич','loginDEctf2018','sjt*3N',1),(37,'Кулаков ','Мэлор','Вячеславович','loginDEipm2018','MAZl6|',2),(38,'Сазонов ','Авксентий','Брониславович','loginDEjoi2018','o}C4jv',3),(39,'Бурова ','Наина','Брониславовна','loginDEwap2018','4hny7k',2),(40,'Фадеев ','Демьян','Федосеевич','loginDEaxm2018','BEc3xq',3),(41,'Бобылёва ','Дарья','Якуновна','loginDEsmq2018','ATVmM7',3),(42,'Виноградов ','Созон','Арсеньевич','loginDEeur2018','n4V{wP',3),(43,'Гордеев ','Владлен','Ефимович','loginDEvke2018','WQLXSl',3),(44,'Иванова ','Зинаида','Валерьевна','loginDEvod2018','0EW93v',2),(45,'Гусев ','Руслан','Дамирович','loginDEjaw2018','h6z&Ky',3),(46,'Маслов ','Дмитрий','Иванович','loginDEpdp2018','8NvRfC',2),(47,'Антонова ','Ульяна','Семёновна','loginDEjpp2018','oMOQq3',3),(48,'Орехова ','Людмила','Владимировна','loginDEkiy2018','BQzsts',2),(49,'Авдеева ','Жанна','Куприяновна','loginDEhmn2018','a|Iz|7',2),(50,'Кузнецов ','Фрол','Варламович','loginDEfmn2018','cw3|03',3);
/*!40000 ALTER TABLE `User` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'db67'
--

--
-- Dumping routines for database 'db67'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-09-04 21:38:40
