CREATE DATABASE  IF NOT EXISTS `trade` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `trade`;
-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: trade
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
  `OrderPickupPoint` int NOT NULL,
  `OrderDate` datetime NOT NULL,
  `OrderCode` int NOT NULL,
  `UserID` int NOT NULL,
  PRIMARY KEY (`OrderID`),
  KEY `fk_order_user_idx` (`UserID`),
  KEY `fk_order_pickuppoint_idx` (`OrderPickupPoint`),
  CONSTRAINT `fk_order_pickuppoint` FOREIGN KEY (`OrderPickupPoint`) REFERENCES `PickupPoint` (`PickupPointID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_order_user` FOREIGN KEY (`UserID`) REFERENCES `User` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Order`
--

LOCK TABLES `Order` WRITE;
/*!40000 ALTER TABLE `Order` DISABLE KEYS */;
INSERT INTO `Order` VALUES (1,'Новый ','2022-05-09 00:00:00',1,'2022-05-03 00:00:00',211,6),(2,'Завершен','2022-05-10 00:00:00',3,'2022-05-04 00:00:00',212,4),(3,'Новый ','2022-05-11 00:00:00',5,'2022-05-05 00:00:00',213,1),(4,'Новый ','2022-05-12 00:00:00',6,'2022-05-06 00:00:00',214,12),(5,'Завершен','2022-05-13 00:00:00',7,'2022-05-07 00:00:00',215,21),(6,'Новый ','2022-05-14 00:00:00',10,'2022-05-08 00:00:00',216,32),(7,'Новый ','2022-05-15 00:00:00',11,'2022-05-09 00:00:00',217,33),(8,'Новый ','2022-05-16 00:00:00',20,'2022-05-10 00:00:00',218,26),(9,'Завершен','2022-05-17 00:00:00',30,'2022-05-11 00:00:00',219,8),(10,'Новый ','2022-05-18 00:00:00',33,'2022-05-12 00:00:00',220,17);
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
  KEY `orderproduct_ibfk_1_idx` (`OrderID`),
  KEY `orderproduct_ibfk_2_idx` (`ProductArticleNumber`),
  CONSTRAINT `orderproduct_ibfk_1` FOREIGN KEY (`OrderID`) REFERENCES `Order` (`OrderID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `orderproduct_ibfk_2` FOREIGN KEY (`ProductArticleNumber`) REFERENCES `Product` (`ProductArticleNumber`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `OrderProduct`
--

LOCK TABLES `OrderProduct` WRITE;
/*!40000 ALTER TABLE `OrderProduct` DISABLE KEYS */;
INSERT INTO `OrderProduct` VALUES (1,'D374E4',2),(1,'Z472R4',1),(2,'A782R4',1),(2,'K830R4',1),(3,'K849L6',1),(3,'O393R4',2),(4,'S983R4',3),(4,'Z469T7',1),(5,'F938T5',1),(5,'S037R9',1),(6,'D799T6',2),(6,'E679R3',2),(7,'P023T2',2),(7,'V892T6',2),(8,'K702L6',1),(8,'S625T5',1),(9,'L802Y5',3),(9,'P307T5',1),(10,'B702T6',1),(10,'M562Y7',1);
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
INSERT INTO `PickupPoint` VALUES (1,'344288, г. Вологда, ул. Чехова, 1'),(2,'614164, г.Вологда,  ул. Степная, 30'),(3,'394242, г. Вологда, ул. Коммунистическая, 43'),(4,'660540, г. Вологда, ул. Солнечная, 25'),(5,'125837, г. Вологда, ул. Шоссейная, 40'),(6,'125703, г. Вологда, ул. Партизанская, 49'),(7,'625283, г. Вологда, ул. Победы, 46'),(8,'614611, г. Вологда, ул. Молодежная, 50'),(9,'454311, г.Вологда, ул. Новая, 19'),(10,'660007, г.Вологда, ул. Октябрьская, 19'),(11,'603036, г. Вологда, ул. Садовая, 4'),(12,'450983, г.Вологда, ул. Комсомольская, 26'),(13,'394782, г. Вологда, ул. Чехова, 3'),(14,'603002, г. Вологда, ул. Дзержинского, 28'),(15,'450558, г. Вологда, ул. Набережная, 30'),(16,'394060, г.Вологда, ул. Фрунзе, 43'),(17,'410661, г. Вологда, ул. Школьная, 50'),(18,'625590, г. Вологда, ул. Коммунистическая, 20'),(19,'625683, г. Вологда, ул. 8 Марта'),(20,'400562, г. Вологда, ул. Зеленая, 32'),(21,'614510, г. Вологда, ул. Маяковского, 47'),(22,'410542, г. Вологда, ул. Светлая, 46'),(23,'620839, г. Вологда, ул. Цветочная, 8'),(24,'443890, г. Вологда, ул. Коммунистическая, 1'),(25,'603379, г. Вологда, ул. Спортивная, 46'),(26,'603721, г. Вологда, ул. Гоголя, 41'),(27,'410172, г. Вологда, ул. Северная, 13'),(28,'420151, г. Вологда, ул. Вишневая, 32'),(29,'125061, г. Вологда, ул. Подгорная, 8'),(30,'630370, г. Вологда, ул. Шоссейная, 24'),(31,'614753, г. Вологда, ул. Полевая, 35'),(32,'426030, г. Вологда, ул. Маяковского, 44'),(33,'450375, г. Вологда ул. Клубная, 44'),(34,'625560, г. Вологда, ул. Некрасова, 12'),(35,'630201, г. Вологда, ул. Комсомольская, 17'),(36,'190949, г. Вологда, ул. Мичурина, 26');
/*!40000 ALTER TABLE `PickupPoint` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Product`
--

DROP TABLE IF EXISTS `Product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Product` (
  `ProductArticleNumber` varchar(6) NOT NULL,
  `ProductName` text NOT NULL,
  `ProductDescription` text NOT NULL,
  `ProductCategory` text NOT NULL,
  `ProductPhoto` varchar(1000) DEFAULT NULL,
  `ProductManufacturer` text NOT NULL,
  `ProductCost` decimal(19,4) NOT NULL,
  `ProductDiscountAmount` tinyint DEFAULT NULL,
  `ProductQuantityInStock` int NOT NULL,
  `ProductUnit` varchar(50) NOT NULL,
  `ProductCurrentDiscount` tinyint DEFAULT NULL,
  `ProductSupplier` int NOT NULL,
  PRIMARY KEY (`ProductArticleNumber`),
  KEY `fk_product_supplier_idx` (`ProductSupplier`),
  CONSTRAINT `fk_product_supplier` FOREIGN KEY (`ProductSupplier`) REFERENCES `Supplier` (`SupplierID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Product`
--

LOCK TABLES `Product` WRITE;
/*!40000 ALTER TABLE `Product` DISABLE KEYS */;
INSERT INTO `Product` VALUES ('A782R4','Аккумулятор','Аккумулятор автомобильный BIG FIGHTER 55р','Автозапчасти','A782R4.jpg','BIG FIGHTER',4500.0000,30,24,'шт.',2,1),('B702T6','Домкрат','Домкрат ALCA 436000','Автосервис','','ALCA',2700.0000,10,3,'шт.',2,2),('D374E4','Съемник подшипников','Съемник AIRLINE AT-GP2-05','Съемники подшипников','D374E4.jpeg','AIRLINE',1400.0000,20,2,'шт.',3,2),('D799T6','Съемник подшипников','Съемник для подшипников JTC 9000','Съемники подшипников','','JTC',1800.0000,25,6,'шт.',2,2),('E679R3','Автошампунь','Автошампунь GRASS 800026 Active Foam Truck','Автосервис','','GRASS',4000.0000,15,14,'шт.',4,2),('E932T8','Полироль','Полироль GRASS 125101 Black Brilliance','Автосервис','','GRASS',2100.0000,25,23,'шт.',3,2),('F026R4','Антифриз','Антифриз сине-зеленый MOBIL ANTIFREEZE EXTRA','Автосервис','','MOBIL',530.0000,15,13,'шт.',2,2),('F938T5','Антифриз','Антифриз красный TCL LLC01212','Автосервис','','TCL',1200.0000,15,34,'шт.',4,2),('H572T6','Парктроник','Парктроник AIRLINE APS-8L-02','Автозапчасти','','AIRLINE',2900.0000,15,12,'шт.',5,2),('K702L6','Ключ','Ключ JONNESWAY W233032 (30 / 32 мм)','Ручные инструменты','','JONNESWAY',1600.0000,15,9,'шт.',3,2),('K830R4','Колпак для колеса','Колпак для колеса AIRLINE Супер Астра R16 серебристый 2шт','Автозапчасти','K830R4.jpg','AIRLINE',915.0000,20,14,'уп.',3,1),('K849L6','Набор ключей','Набор ключей накидных STV 00-00010990 6шт.','Ручные инструменты','K849L6.jpeg','STV',780.0000,15,23,'уп.',2,2),('L802Y5','Лопата','Лопата саперная AIRLINE AB-S-03','Аксессуары','','AIRLINE',870.0000,5,23,'шт.',4,2),('M562Y7','Мультиметр','Мультиметр JTC 1227A автомобильный','Аксессуары','','JTC',14200.0000,5,12,'шт.',3,1),('O393R4','Отвертка','Отвертка JONNESWAY D04P2100','Ручные инструменты','O393R4.jpeg','JONNESWAY',460.0000,15,14,'шт.',3,2),('P023T2','Провода прикуривания','Провода прикуривания в сумке SMART CABLE 700 4,5м','Автозапчасти','','SMART',3400.0000,20,6,'шт.',2,1),('P307T5','Провода прикуривания','Провода прикуривания в сумке EXPERT 400А 2,5м','Автозапчасти','','EXPERT',700.0000,10,2,'шт.',4,1),('P439R4','Пассатижи','Пассатижи HAMMER Flex 601-050 160 мм (6 дюймов)','Аксессуары','','HAMMER',310.0000,5,34,'шт.',3,1),('S021R4','Адаптер для щеток','Адаптер для щеток стеклоочистителя ALCA Top Lock A/C блистер 2 шт','Автозапчасти','','ALCA',200.0000,3,13,'уп.',2,1),('S037R9','Щетка','Щётка AIRLINE AB-H-03','Аксессуары','S037R9.jpeg','AIRLINE',740.0000,25,26,'шт.',2,2),('S625T5','Щетка','щетка стеклоочистителя ALCA Start 16\"/40см каркасная\"','Автозапчасти','','ALCA',249.0000,20,12,'шт.',3,1),('S826R4','Щетка','Щетка стеклоочистителя ALCA Super flat 19\"/48см бескаркасная\"','Автозапчасти','','ALCA',530.0000,2,28,'шт.',4,1),('S983R4','Щетка','Щетка с/о BOSCH ECO 65C 650мм каркасная','Автозапчасти','S983R4.jpg','BOSCH',500.0000,15,8,'шт.',4,1),('T589T6','Термометр','Термометр ALCA 577000','Аксессуары','','ALCA',1400.0000,10,3,'шт.',2,2),('V892T6','Свеча зажигания','Свеча зажигания CHAMPION IGP F7RTC','Автозапчасти','','CHAMPION',130.0000,5,21,'шт.',3,1),('Z326T9','Зарядное устройство','Устройство зарядное EXPERT ЗУ-300 6/12В 3,8А','Зарядные устройства','','EXPERT',2400.0000,15,14,'шт.',3,1),('Z374R3','Зарядное устройство','Зарядное устройство AIRLINE ACH-15A-08','Зарядные устройства','Z374R3.jpeg','AIRLINE',4600.0000,25,14,'шт.',2,2),('Z469T7','Устройство пуско-зарядное','Устройство пуско-зарядное AIRLINE 12В 8000мАч 350А','Зарядные устройства','Z469T7.jpg','AIRLINE',4000.0000,25,4,'шт.',2,1),('Z472R4','Зарядное устройство','Зарядное устройство KOLNER KBCН 4','Зарядные устройства','Z472R4.jpeg','KOLNER',1250.0000,30,6,'шт.',4,2),('Z782T5','Зажим','Зажим AIRLINE SA-400-P','Зарядные устройства','','AIRLINE',290.0000,25,6,'уп.',5,2);
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
INSERT INTO `Supplier` VALUES (1,'Максидом'),(2,'220-volt');
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
  `UserPatronymic` varchar(100) DEFAULT NULL,
  `UserLogin` text NOT NULL,
  `UserPassword` text NOT NULL,
  `UserRole` int NOT NULL,
  PRIMARY KEY (`UserID`),
  KEY `UserRole` (`UserRole`),
  CONSTRAINT `user_ibfk_1` FOREIGN KEY (`UserRole`) REFERENCES `Role` (`RoleID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `User`
--

LOCK TABLES `User` WRITE;
/*!40000 ALTER TABLE `User` DISABLE KEYS */;
INSERT INTO `User` VALUES (1,'Якушев ','Евсей','Лукьевич','loginDEdcx2018','TYlFkM',3),(2,'Фёдоров ','Святослав','Григорьевич','loginDEnsa2018','LdqH+T',3),(3,'Борисов ','Герман','Дамирович','loginDErxm2018','8EavEy',3),(4,'Ситников ','Серапион','Фролович','loginDEaic2018','X2adoa',2),(5,'Третьяков ','Валерьян','Иринеевич','loginDEwul2018','uK&3Zr',2),(6,'Комиссарова ','Мария','Владимировна','loginDEjgl2018','++04Tb',3),(7,'Меркушева ','Раиса','Владленовна','loginDEgtk2018','pNWXhi',3),(8,'Калашникова ','Венера','Якуновна','loginDEvrd2018','S7N9hz',3),(9,'Комиссаров ','Семён','Павлович','loginDExky2018','Kt9EAS',1),(10,'Денисов ','Митрофан','Егорович','loginDExnj2018','IJDdP0',2),(11,'Матвиенко ','Дамир','Богданович','loginDEnbu2018','86uDLd',3),(12,'Кириллов ','Константин','Алексеевич','loginDEiik2018','gKt2zV',3),(13,'Медведьев ','Фёдор','Мэлсович','loginDEwyi2018','9eskgK',3),(14,'Карпов ','Евгений','Лукьевич','loginDEpfr2018','mW1Q36',3),(15,'Маркова ','Евдокия','Артёмовна','loginDEjct2018','3WpoK9',3),(16,'Красильников ','Тихон','Богданович','loginDEsmg2018','ApH1By',3),(17,'Титов ','Семён','Иринеевич','loginDEexu2018','Nt44pG',2),(18,'Кудряшов ','Борис','Иринеевич','loginDEztr2018','MYCgB7',2),(19,'Гаврилова ','Нинель','Денисовна','loginDEwrc2018','SktJa|',2),(20,'Быков ','Дмитрий','Валерьянович','loginDEzjs2018','|x{s+X',2),(21,'Фомичёв ','Денис','Федосеевич','loginDEeka2018','mLZvLv',2),(22,'Белова ','Марфа','Матвеевна','loginDEepr2018','BG6tpN',1),(23,'Романова ','Марина','Лаврентьевна','loginDEsnq2018','hrD}}g',1),(24,'Беспалов ','Демьян','Витальевич','loginDEvqn2018','LPa|e3',3),(25,'Архипова ','Венера','Демьяновна','loginDEery2018','*I0Rdi',3),(26,'Носков ','Парфений','Георгьевич','loginDElqv2018','Hqfw17',3),(27,'Зыков ','Иван','Варламович','loginDEtuz2018','Yln7JW',2),(28,'Иван ','Протасьевна','','loginDEllr2018','hXtdCD',2),(29,'Рожков ','Протасий','Альвианович','loginDEisy2018','5k5dHN',2),(30,'Большакова ','Нинель','Протасьевна','loginDEqiv2018','h+N2uW',1),(31,'Наумова ','Лидия','Донатовна','loginDEmfu2018','{ZpDBn',3),(32,'Панова ','Ольга','Олеговна','loginDEgbd2018','+86Nf*',2),(33,'Комаров ','Аркадий','Иванович','loginDEkdg2018','R0tt07',1),(34,'Федосеева ','Тамара','Михаиловна','loginDEjrs2018','MVg{yd',1),(35,'Пестов ','Роман','Михаилович','loginDEmvd2018','wyLDa{',3),(36,'Блинов ','Артём','Ильяович','loginDEctc2018','B&dlx+',2),(37,'Владимирова ','Полина','Иринеевна','loginDEavf2018','oDTttg',3),(38,'Силин ','Игнатий','Яковович','loginDEako2018','tD8J5+',1),(39,'Кононов ','Геннадий','Созонович','loginDEzrg2018','WXIgGi',2),(40,'Дьячков ','Фрол','Арсеньевич','loginDEdwq2018','WkTaBP',2),(41,'Горбачёв ','Арсений','Григорьевич','loginDEszg2018','NWkAVP',3),(42,'Виноградов ','Яков','Онисимович','loginDEmeh2018','HQ+m4W',3),(43,'Лаврентьева ','Валентина','Васильевна','loginDEpwm2018','be7AT0',3),(44,'Брагин ','Лукьян','Мартынович','loginDEnaq2018','I8c5EB',3),(45,'Трофимов ','Кондрат','Игоревич','loginDEtuk2018','6aDAzV',2),(46,'Степанова ','Глафира','Авксентьевна','loginDEhsb2018','n|I6A0',3),(47,'Федосеев ','Пётр','Григорьевич','loginDEaoi2018','Dl58m|',2),(48,'Поляков ','Николай','Антонович','loginDErmk2018','D3GuIv',2),(49,'Медведев ','Владимир','Онисимович','loginDEfnd2018','74D9|d',2),(50,'Иван ','Мэлоровна','','loginDEtza2018','A7Qldh',2);
/*!40000 ALTER TABLE `User` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'trade'
--

--
-- Dumping routines for database 'trade'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-09-05 17:06:51
