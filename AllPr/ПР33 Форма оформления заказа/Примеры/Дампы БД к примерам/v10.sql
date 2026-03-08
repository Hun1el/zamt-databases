CREATE DATABASE  IF NOT EXISTS `db49` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `db49`;
-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
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
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` VALUES (1,'Зарядные устройства'),(2,'Съемники подшипников'),(3,'Автозапчасти'),(4,'Ручные инструменты'),(5,'Аксессуары'),(6,'Автосервис');
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
  `OrderDate` datetime NOT NULL,
  `OrderDeliveryDate` datetime NOT NULL,
  `OrderPickupPoint` int NOT NULL,
  `OrderUserID` int NOT NULL,
  `OrderCode` int NOT NULL,
  `OrderStatus` text NOT NULL,
  PRIMARY KEY (`OrderID`),
  KEY `OrderUserID` (`OrderUserID`),
  KEY `OrderPickupPoint` (`OrderPickupPoint`),
  CONSTRAINT `order_ibfk_1` FOREIGN KEY (`OrderUserID`) REFERENCES `user` (`UserID`),
  CONSTRAINT `order_ibfk_2` FOREIGN KEY (`OrderPickupPoint`) REFERENCES `pickuppoint` (`PickupPointID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES `order` WRITE;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
INSERT INTO `order` VALUES (1,'2022-05-03 00:00:00','2022-05-09 00:00:00',1,1,211,'Новый '),(2,'2022-05-04 00:00:00','2022-05-10 00:00:00',3,2,212,'Завершен'),(3,'2022-05-05 00:00:00','2022-05-11 00:00:00',5,3,213,'Новый '),(4,'2022-05-06 00:00:00','2022-05-12 00:00:00',6,6,214,'Новый '),(5,'2022-05-07 00:00:00','2022-05-13 00:00:00',7,7,215,'Завершен'),(6,'2022-05-08 00:00:00','2022-05-14 00:00:00',10,8,216,'Новый '),(7,'2022-05-09 00:00:00','2022-05-15 00:00:00',11,11,217,'Новый '),(8,'2022-05-10 00:00:00','2022-05-16 00:00:00',20,12,218,'Новый '),(9,'2022-05-11 00:00:00','2022-05-17 00:00:00',30,13,219,'Завершен'),(10,'2022-05-12 00:00:00','2022-05-18 00:00:00',33,14,220,'Новый ');
/*!40000 ALTER TABLE `order` ENABLE KEYS */;
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
INSERT INTO `pickuppoint` VALUES (1,'344288, г. Вологда, ул. Чехова, 1'),(2,'614164, г.Вологда,  ул. Степная, 30'),(3,'394242, г. Вологда, ул. Коммунистическая, 43'),(4,'660540, г. Вологда, ул. Солнечная, 25'),(5,'125837, г. Вологда, ул. Шоссейная, 40'),(6,'125703, г. Вологда, ул. Партизанская, 49'),(7,'625283, г. Вологда, ул. Победы, 46'),(8,'614611, г. Вологда, ул. Молодежная, 50'),(9,'454311, г.Вологда, ул. Новая, 19'),(10,'660007, г.Вологда, ул. Октябрьская, 19'),(11,'603036, г. Вологда, ул. Садовая, 4'),(12,'450983, г.Вологда, ул. Комсомольская, 26'),(13,'394782, г. Вологда, ул. Чехова, 3'),(14,'603002, г. Вологда, ул. Дзержинского, 28'),(15,'450558, г. Вологда, ул. Набережная, 30'),(16,'394060, г.Вологда, ул. Фрунзе, 43'),(17,'410661, г. Вологда, ул. Школьная, 50'),(18,'625590, г. Вологда, ул. Коммунистическая, 20'),(19,'625683, г. Вологда, ул. 8 Марта'),(20,'400562, г. Вологда, ул. Зеленая, 32'),(21,'614510, г. Вологда, ул. Маяковского, 47'),(22,'410542, г. Вологда, ул. Светлая, 46'),(23,'620839, г. Вологда, ул. Цветочная, 8'),(24,'443890, г. Вологда, ул. Коммунистическая, 1'),(25,'603379, г. Вологда, ул. Спортивная, 46'),(26,'603721, г. Вологда, ул. Гоголя, 41'),(27,'410172, г. Вологда, ул. Северная, 13'),(28,'420151, г. Вологда, ул. Вишневая, 32'),(29,'125061, г. Вологда, ул. Подгорная, 8'),(30,'630370, г. Вологда, ул. Шоссейная, 24'),(31,'614753, г. Вологда, ул. Полевая, 35'),(32,'426030, г. Вологда, ул. Маяковского, 44'),(33,'450375, г. Вологда ул. Клубная, 44'),(34,'625560, г. Вологда, ул. Некрасова, 12'),(35,'630201, г. Вологда, ул. Комсомольская, 17'),(36,'190949, г. Вологда, ул. Мичурина, 26');
/*!40000 ALTER TABLE `pickuppoint` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `ProductArticleNumber` varchar(100) NOT NULL,
  `ProductName` varchar(100) NOT NULL,
  `ProductUnit` varchar(10) NOT NULL,
  `ProductCost` decimal(19,2) NOT NULL,
  `ProductMaxDiscount` tinyint NOT NULL,
  `ProductManufacturer` varchar(100) NOT NULL,
  `ProductSupplier` int NOT NULL,
  `ProductCategory` int NOT NULL,
  `ProductDiscount` tinyint NOT NULL,
  `ProductQuantityInStock` int NOT NULL,
  `ProductDescription` varchar(100) NOT NULL,
  `ProductPhoto` varchar(100) NOT NULL,
  PRIMARY KEY (`ProductArticleNumber`),
  KEY `ProductSupplier` (`ProductSupplier`),
  KEY `ProductCategory` (`ProductCategory`),
  CONSTRAINT `product_ibfk_1` FOREIGN KEY (`ProductSupplier`) REFERENCES `supplier` (`SupplierID`),
  CONSTRAINT `product_ibfk_2` FOREIGN KEY (`ProductCategory`) REFERENCES `category` (`CategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES ('A782R4','Аккумулятор','шт.',4500.00,30,'BIG FIGHTER',2,3,2,24,'Аккумулятор автомобильный BIG FIGHTER 55р','A782R4.jpg'),('B702T6','Домкрат','шт.',2700.00,10,'ALCA',1,6,2,3,'Домкрат ALCA 436000',''),('D374E4','Съемник подшипников','шт.',1400.00,20,'AIRLINE',1,2,3,2,'Съемник AIRLINE AT-GP2-05','D374E4.jpeg'),('D799T6','Съемник подшипников','шт.',1800.00,25,'JTC',1,2,2,6,'Съемник для подшипников JTC 9000',''),('E679R3','Автошампунь','шт.',4000.00,15,'GRASS',1,6,4,14,'Автошампунь GRASS 800026 Active Foam Truck',''),('E932T8','Полироль','шт.',2100.00,25,'GRASS',1,6,3,23,'Полироль GRASS 125101 Black Brilliance',''),('F026R4','Антифриз','шт.',530.00,15,'MOBIL',1,6,2,13,'Антифриз сине-зеленый MOBIL ANTIFREEZE EXTRA',''),('F938T5','Антифриз','шт.',1200.00,15,'TCL',1,6,4,34,'Антифриз красный TCL LLC01212',''),('H572T6','Парктроник','шт.',2900.00,15,'AIRLINE',1,3,5,12,'Парктроник AIRLINE APS-8L-02',''),('K702L6','Ключ','шт.',1600.00,15,'JONNESWAY',1,4,3,9,'Ключ JONNESWAY W233032 (30 / 32 мм)',''),('K830R4','Колпак для колеса','уп.',915.00,20,'AIRLINE',2,3,3,14,'Колпак для колеса AIRLINE Супер Астра R16 серебристый 2шт','K830R4.jpg'),('K849L6','Набор ключей','уп.',780.00,15,'STV',1,4,2,23,'Набор ключей накидных STV 00-00010990 6шт.','K849L6.jpeg'),('L802Y5','Лопата','шт.',870.00,5,'AIRLINE',1,5,4,23,'Лопата саперная AIRLINE AB-S-03',''),('M562Y7','Мультиметр','шт.',14200.00,5,'JTC',2,5,3,12,'Мультиметр JTC 1227A автомобильный',''),('O393R4','Отвертка','шт.',460.00,15,'JONNESWAY',1,4,3,14,'Отвертка JONNESWAY D04P2100','O393R4.jpeg'),('P023T2','Провода прикуривания','шт.',3400.00,20,'SMART',2,3,2,6,'Провода прикуривания в сумке SMART CABLE 700 4,5м',''),('P307T5','Провода прикуривания','шт.',700.00,10,'EXPERT',2,3,4,2,'Провода прикуривания в сумке EXPERT 400А 2,5м',''),('P439R4','Пассатижи','шт.',310.00,5,'HAMMER',2,5,3,34,'Пассатижи HAMMER Flex 601-050 160 мм (6 дюймов)',''),('S021R4','Адаптер для щеток','уп.',200.00,3,'ALCA',2,3,2,13,'Адаптер для щеток стеклоочистителя ALCA Top Lock A/C блистер 2 шт',''),('S037R9','Щетка','шт.',740.00,25,'AIRLINE',1,5,2,26,'Щётка AIRLINE AB-H-03','S037R9.jpeg'),('S625T5','Щетка','шт.',249.00,20,'ALCA',2,3,3,12,'щетка стеклоочистителя ALCA Start 16/40см каркасная',''),('S826R4','Щетка','шт.',530.00,2,'ALCA',2,3,4,28,'Щетка стеклоочистителя ALCA Super flat 19/48см бескаркасная',''),('S983R4','Щетка','шт.',500.00,15,'BOSCH',2,3,4,8,'Щетка с/о BOSCH ECO 65C 650мм каркасная','S983R4.jpg'),('T589T6','Термометр','шт.',1400.00,10,'ALCA',1,5,2,3,'Термометр ALCA 577000',''),('V892T6','Свеча зажигания','шт.',130.00,5,'CHAMPION',2,3,3,21,'Свеча зажигания CHAMPION IGP F7RTC',''),('Z326T9','Зарядное устройство','шт.',2400.00,15,'EXPERT',2,1,3,14,'Устройство зарядное EXPERT ЗУ-300 6/12В 3,8А',''),('Z374R3','Зарядное устройство','шт.',4600.00,25,'AIRLINE',1,1,2,14,'Зарядное устройство AIRLINE ACH-15A-08','Z374R3.jpeg'),('Z469T7','Устройство пуско-зарядное','шт.',4000.00,25,'AIRLINE',2,1,2,4,'Устройство пуско-зарядное AIRLINE 12В 8000мАч 350А','Z469T7.jpg'),('Z472R4','Зарядное устройство','шт.',1250.00,30,'KOLNER',1,1,4,6,'Зарядное устройство KOLNER KBCН 4','Z472R4.jpeg'),('Z782T5','Зажим','уп.',290.00,25,'AIRLINE',1,1,5,6,'Зажим AIRLINE SA-400-P','');
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
  `SupplierName` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`SupplierID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supplier`
--

LOCK TABLES `supplier` WRITE;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
INSERT INTO `supplier` VALUES (1,'220-volt'),(2,'Максидом');
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;
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
  `UserLogin` varchar(100) NOT NULL,
  `UserPassword` varchar(100) NOT NULL,
  `UserRole` int NOT NULL,
  PRIMARY KEY (`UserID`),
  KEY `UserRole` (`UserRole`),
  CONSTRAINT `user_ibfk_1` FOREIGN KEY (`UserRole`) REFERENCES `role` (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'Якушев','Евсей','Лукьевич','loginDEdcx2018','TYlFkM',1),(2,'Фёдоров','Святослав','Григорьевич','loginDEnsa2018','LdqH+T',1),(3,'Борисов','Герман','Дамирович','loginDErxm2018','8EavEy',1),(4,'Ситников','Серапион','Фролович','loginDEaic2018','X2adoa',2),(5,'Третьяков','Валерьян','Иринеевич','loginDEwul2018','uK&3Zr',2),(6,'Комиссарова','Мария','Владимировна','loginDEjgl2018','++04Tb',1),(7,'Меркушева','Раиса','Владленовна','loginDEgtk2018','pNWXhi',1),(8,'Калашникова','Венера','Якуновна','loginDEvrd2018','S7N9hz',1),(9,'Комиссаров','Семён','Павлович','loginDExky2018','Kt9EAS',3),(10,'Денисов','Митрофан','Егорович','loginDExnj2018','IJDdP0',2),(11,'Матвиенко','Дамир','Богданович','loginDEnbu2018','86uDLd',1),(12,'Кириллов','Константин','Алексеевич','loginDEiik2018','gKt2zV',1),(13,'Медведьев','Фёдор','Мэлсович','loginDEwyi2018','9eskgK',1),(14,'Карпов','Евгений','Лукьевич','loginDEpfr2018','mW1Q36',1),(15,'Маркова','Евдокия','Артёмовна','loginDEjct2018','3WpoK9',1),(16,'Красильников','Тихон','Богданович','loginDEsmg2018','ApH1By',1),(17,'Титов','Семён','Иринеевич','loginDEexu2018','Nt44pG',2),(18,'Кудряшов','Борис','Иринеевич','loginDEztr2018','MYCgB7',2),(19,'Гаврилова','Нинель','Денисовна','loginDEwrc2018','SktJa|',2),(20,'Быков','Дмитрий','Валерьянович','loginDEzjs2018','|x{s+X',2),(21,'Фомичёв','Денис','Федосеевич','loginDEeka2018','mLZvLv',2),(22,'Белова','Марфа','Матвеевна','loginDEepr2018','BG6tpN',3),(23,'Романова','Марина','Лаврентьевна','loginDEsnq2018','hrD}}g',3),(24,'Беспалов','Демьян','Витальевич','loginDEvqn2018','LPa|e3',1),(25,'Архипова','Венера','Демьяновна','loginDEery2018','*I0Rdi',1),(26,'Носков','Парфений','Георгьевич','loginDElqv2018','Hqfw17',1),(27,'Зыков','Иван','Варламович','loginDEtuz2018','Yln7JW',2),(28,'Иванченко','Иван','Протасьевна','loginDEllr2018','hXtdCD',2),(29,'Рожков','Протасий','Альвианович','loginDEisy2018','5k5dHN',2),(30,'Большакова','Нинель','Протасьевна','loginDEqiv2018','h+N2uW',3),(31,'Наумова','Лидия','Донатовна','loginDEmfu2018','{ZpDBn',1),(32,'Панова','Ольга','Олеговна','loginDEgbd2018','+86Nf*',2),(33,'Комаров','Аркадий','Иванович','loginDEkdg2018','R0tt07',3),(34,'Федосеева','Тамара','Михаиловна','loginDEjrs2018','MVg{yd',3),(35,'Пестов','Роман','Михаилович','loginDEmvd2018','wyLDa{',1),(36,'Блинов','Артём','Ильяович','loginDEctc2018','B&dlx+',2),(37,'Владимирова','Полина','Иринеевна','loginDEavf2018','oDTttg',1),(38,'Силин','Игнатий','Яковович','loginDEako2018','tD8J5+',3),(39,'Кононов','Геннадий','Созонович','loginDEzrg2018','WXIgGi',2),(40,'Дьячков','Фрол','Арсеньевич','loginDEdwq2018','WkTaBP',2),(41,'Горбачёв','Арсений','Григорьевич','loginDEszg2018','NWkAVP',1),(42,'Виноградов','Яков','Онисимович','loginDEmeh2018','HQ+m4W',1),(43,'Лаврентьева','Валентина','Васильевна','loginDEpwm2018','be7AT0',1),(44,'Брагин','Лукьян','Мартынович','loginDEnaq2018','I8c5EB',1),(45,'Трофимов','Кондрат','Игоревич','loginDEtuk2018','6aDAzV',2),(46,'Степанова','Глафира','Авксентьевна','loginDEhsb2018','n|I6A0',1),(47,'Федосеев','Пётр','Григорьевич','loginDEaoi2018','Dl58m|',2),(48,'Поляков','Николай','Антонович','loginDErmk2018','D3GuIv',2),(49,'Медведев','Владимир','Онисимович','loginDEfnd2018','74D9|d',2),(50,'Иванов','Иван','Мэлоровна','loginDEtza2018','A7Qldh',2);
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

-- Dump completed on 2024-09-09  8:09:19
