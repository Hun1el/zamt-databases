-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: 10.207.106.12    Database: db67
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
-- Table structure for table `agent`
--

DROP TABLE IF EXISTS `agent`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `agent` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AgentTypeID` int NOT NULL,
  `Address` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `INN` varchar(12) NOT NULL,
  `KPP` varchar(9) DEFAULT NULL,
  `DirectorName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Phone` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Logo` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Priority` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_Agent_AgentType` (`AgentTypeID`),
  CONSTRAINT `FK_Agent_AgentType` FOREIGN KEY (`AgentTypeID`) REFERENCES `agenttype` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `agent`
--

LOCK TABLES `agent` WRITE;
/*!40000 ALTER TABLE `agent` DISABLE KEYS */;
/*!40000 ALTER TABLE `agent` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `agentpriorityhistory`
--

DROP TABLE IF EXISTS `agentpriorityhistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `agentpriorityhistory` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `AgentID` int NOT NULL,
  `ChangeDate` datetime(6) NOT NULL,
  `PriorityValue` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_AgentPriorityHistory_Agent` (`AgentID`),
  CONSTRAINT `FK_AgentPriorityHistory_Agent` FOREIGN KEY (`AgentID`) REFERENCES `agent` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `agentpriorityhistory`
--

LOCK TABLES `agentpriorityhistory` WRITE;
/*!40000 ALTER TABLE `agentpriorityhistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `agentpriorityhistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `agenttype`
--

DROP TABLE IF EXISTS `agenttype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `agenttype` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Image` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `agenttype`
--

LOCK TABLES `agenttype` WRITE;
/*!40000 ALTER TABLE `agenttype` DISABLE KEYS */;
/*!40000 ALTER TABLE `agenttype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `material`
--

DROP TABLE IF EXISTS `material`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `material` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CountInPack` int NOT NULL,
  `Unit` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CountInStock` double DEFAULT NULL,
  `MinCount` double NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Cost` decimal(10,2) NOT NULL,
  `Image` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `MaterialTypeID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_Material_MaterialType` (`MaterialTypeID`),
  CONSTRAINT `FK_Material_MaterialType` FOREIGN KEY (`MaterialTypeID`) REFERENCES `materialtype` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=101 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `material`
--

LOCK TABLES `material` WRITE;
/*!40000 ALTER TABLE `material` DISABLE KEYS */;
INSERT INTO `material` VALUES (1,'Гранулы белый 2x2',7,'л',76,8,NULL,47680.00,'',1),(2,'Нить серый 1x0',1,'м',978,42,NULL,27456.00,'',2),(3,'Нить белый 1x3',8,'м',406,27,NULL,2191.00,'',3),(4,'Нить цветной 1x1',3,'г',424,10,NULL,8619.00,'\\materials\\image_5.jpeg',2),(5,'Нить цветной 2x0',2,'м',395,26,NULL,16856.00,'',2),(6,'Краска синий 2x2',6,'л',334,48,NULL,403.00,'',2),(7,'Нить синий 0x2',9,'м',654,10,NULL,7490.00,'',3),(8,'Гранулы серый 2x2',7,'л',648,17,NULL,15478.00,'',1),(9,'Краска синий 1x2',2,'л',640,50,NULL,44490.00,'',2),(10,'Нить зеленый 2x0',5,'м',535,45,NULL,28301.00,'\\materials\\image_10.jpeg',2),(11,'Гранулы синий 1x2',3,'кг',680,6,NULL,9242.00,'',1),(12,'Нить синий 3x2',1,'м',529,13,NULL,10878.00,'',3),(13,'Краска белый 2x2',1,'л',659,35,NULL,29906.00,'\\materials\\image_3.jpeg',2),(14,'Краска зеленый 0x3',2,'л',50,48,NULL,24073.00,'',2),(15,'Нить зеленый 2x3',7,'г',649,25,NULL,20057.00,'',3),(16,'Краска белый 2x1',2,'л',790,8,NULL,3353.00,'',2),(17,'Нить серый 2x3',1,'г',431,40,NULL,22452.00,'',3),(18,'Гранулы серый 3x2',5,'л',96,9,NULL,29943.00,'',1),(19,'Краска серый 3x2',3,'л',806,50,NULL,55064.00,'',2),(20,'Гранулы белый 0x3',3,'кг',538,11,NULL,7183.00,'',1),(21,'Краска цветной 1x1',3,'л',784,22,NULL,43466.00,'',2),(22,'Гранулы белый 1x0',3,'кг',980,41,NULL,27718.00,'',1),(23,'Краска серый 0x2',3,'кг',679,36,NULL,33227.00,'',2),(24,'Гранулы серый 3x3',5,'л',2,38,NULL,15170.00,'',1),(25,'Краска серый 3x0',7,'кг',341,38,NULL,19352.00,'',2),(26,'Гранулы синий 2x1',9,'л',273,17,NULL,231.00,'\\materials\\image_2.jpeg',1),(27,'Гранулы синий 0x2',9,'л',576,36,NULL,41646.00,'',1),(28,'Нить цветной 1x0',5,'г',91,38,NULL,24948.00,'',3),(29,'Краска зеленый 2x2',2,'кг',752,36,NULL,19014.00,'',2),(30,'Краска цветной 1x3',9,'кг',730,5,NULL,268.00,'',2),(31,'Краска серый 2x0',2,'л',131,22,NULL,35256.00,'',2),(32,'Нить зеленый 2x1',6,'м',802,16,NULL,34556.00,'',3),(33,'Краска цветной 0x3',10,'л',324,9,NULL,3322.00,'',2),(34,'Нить белый 2x3',3,'г',283,41,NULL,10823.00,'',3),(35,'Гранулы синий 3x0',1,'кг',411,8,NULL,16665.00,'',1),(36,'Гранулы синий 1x3',8,'л',41,30,NULL,5668.00,'',1),(37,'Нить цветной 2x1',3,'м',150,22,NULL,7615.00,'',2),(38,'Гранулы серый 3x0',4,'л',0,5,NULL,702.00,'\\materials\\image_7.jpeg',1),(39,'Краска синий 3x0',7,'л',523,42,NULL,38644.00,'',2),(40,'Нить зеленый 0x0',8,'м',288,43,NULL,41827.00,'',2),(41,'Гранулы белый 1x2',4,'л',77,46,NULL,8129.00,'',1),(42,'Краска белый 3x0',5,'кг',609,48,NULL,51471.00,'',2),(43,'Краска цветной 0x1',6,'л',43,8,NULL,54401.00,'',2),(44,'Нить серый 1x1',5,'м',372,22,NULL,14474.00,'',3),(45,'Краска синий 2x1',9,'л',642,29,NULL,46848.00,'',2),(46,'Нить серый 3x0',1,'м',409,19,NULL,29503.00,'',3),(47,'Краска зеленый 3x3',6,'л',601,32,NULL,27710.00,'',2),(48,'Краска синий 2x0',7,'л',135,50,NULL,40074.00,'',2),(49,'Гранулы синий 2x3',2,'л',749,45,NULL,53482.00,'',1),(50,'Нить синий 0x3',8,'м',615,22,NULL,32087.00,'',2),(51,'Нить синий 3x3',7,'г',140,12,NULL,45774.00,'',3),(52,'Краска зеленый 2x3',2,'л',485,8,NULL,44978.00,'',2),(53,'Нить синий 3x0',10,'м',67,23,NULL,44407.00,'',3),(54,'Гранулы серый 2x1',7,'кг',779,44,NULL,50339.00,'',1),(55,'Краска зеленый 0x1',2,'л',869,7,NULL,30581.00,'',2),(56,'Краска синий 0x0',8,'кг',796,29,NULL,18656.00,'',2),(57,'Краска серый 2x1',5,'л',706,45,NULL,46579.00,'',2),(58,'Нить белый 0x1',10,'м',101,43,NULL,36883.00,'',3),(59,'Гранулы зеленый 1x2',9,'л',575,15,NULL,45083.00,'',1),(60,'Краска серый 0x1',2,'л',768,27,NULL,35063.00,'',2),(61,'Гранулы цветной 0x1',3,'л',746,50,NULL,24488.00,'',1),(62,'Гранулы белый 3x1',8,'л',995,27,NULL,43711.00,'',1),(63,'Нить зеленый 0x2',2,'м',578,20,NULL,17429.00,'',3),(64,'Гранулы зеленый 0x2',4,'л',206,34,NULL,38217.00,'',1),(65,'Краска цветной 1x2',10,'л',299,50,NULL,47701.00,'',2),(66,'Краска зеленый 1x0',8,'кг',626,17,NULL,52189.00,'',2),(67,'Гранулы серый 0x0',5,'л',608,12,NULL,16715.00,'',1),(68,'Гранулы синий 0x3',5,'кг',953,48,NULL,45134.00,'',1),(69,'Краска цветной 2x1',1,'л',325,45,NULL,1846.00,'',2),(70,'Нить синий 2x3',5,'м',10,21,NULL,43659.00,'',2),(71,'Нить синий 2x1',9,'г',948,13,NULL,12283.00,'',2),(72,'Гранулы белый 1x1',4,'л',93,11,NULL,6557.00,'',1),(73,'Краска синий 1x3',6,'кг',265,17,NULL,38230.00,'',2),(74,'Краска зеленый 3x0',2,'л',261,7,NULL,20226.00,'\\materials\\image_1.jpeg',2),(75,'Нить зеленый 1x0',9,'г',304,43,NULL,8105.00,'',3),(76,'Краска цветной 0x2',7,'л',595,38,NULL,2600.00,'',2),(77,'Нить синий 3x1',7,'м',579,48,NULL,4920.00,'',2),(78,'Краска зеленый 0x2',9,'л',139,23,NULL,39809.00,'',2),(79,'Краска синий 3x3',6,'кг',740,24,NULL,46545.00,'',2),(80,'Краска зеленый 1x1',2,'кг',103,34,NULL,40583.00,'\\materials\\image_6.jpeg',2),(81,'Краска цветной 2x3',9,'л',443,46,NULL,46502.00,'',2),(82,'Нить цветной 3x0',1,'м',989,14,NULL,53651.00,'',3),(83,'Гранулы серый 2x3',6,'л',467,28,NULL,47757.00,'',1),(84,'Краска белый 1x0',6,'л',95,6,NULL,3543.00,'',2),(85,'Гранулы серый 3x1',10,'кг',762,6,NULL,10899.00,'',1),(86,'Гранулы серый 2x0',3,'кг',312,21,NULL,8939.00,'\\materials\\image_8.jpeg',1),(87,'Нить белый 0x2',4,'г',43,19,NULL,29271.00,'',3),(88,'Гранулы зеленый 1x1',4,'л',10,19,NULL,46455.00,'\\materials\\image_4.jpeg',1),(89,'Нить серый 0x2',3,'м',504,10,NULL,45744.00,'\\materials\\image_9.jpeg',2),(90,'Гранулы белый 0x2',2,'л',581,40,NULL,9330.00,'',1),(91,'Нить цветной 3x2',3,'м',831,46,NULL,2939.00,'',3),(92,'Гранулы белый 3x0',6,'л',208,7,NULL,50217.00,'',1),(93,'Нить серый 1x2',1,'м',292,30,NULL,30198.00,'',3),(94,'Краска белый 0x1',7,'л',423,47,NULL,19777.00,'',2),(95,'Гранулы цветной 0x3',7,'кг',723,44,NULL,1209.00,'',1),(96,'Нить серый 1x3',1,'г',489,25,NULL,32002.00,'',2),(97,'Гранулы белый 2x3',4,'л',274,8,NULL,32446.00,'',1),(98,'Краска зеленый 3x1',10,'л',657,19,NULL,32487.00,'',2),(99,'Гранулы цветной 3x2',1,'л',32,45,NULL,28596.00,'',1),(100,'Нить белый 2x0',2,'м',623,23,NULL,46684.00,'',2);
/*!40000 ALTER TABLE `material` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `materialcounthistory`
--

DROP TABLE IF EXISTS `materialcounthistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `materialcounthistory` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `MaterialID` int NOT NULL,
  `ChangeDate` datetime(6) NOT NULL,
  `CountValue` double NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_MaterialCountHistory_Material` (`MaterialID`),
  CONSTRAINT `FK_MaterialCountHistory_Material` FOREIGN KEY (`MaterialID`) REFERENCES `material` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `materialcounthistory`
--

LOCK TABLES `materialcounthistory` WRITE;
/*!40000 ALTER TABLE `materialcounthistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `materialcounthistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `materialsupplier`
--

DROP TABLE IF EXISTS `materialsupplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `materialsupplier` (
  `MaterialID` int NOT NULL,
  `SupplierID` int NOT NULL,
  PRIMARY KEY (`MaterialID`,`SupplierID`),
  KEY `FK_MaterialSupplier_Supplier` (`SupplierID`),
  CONSTRAINT `FK_MaterialSupplier_Material` FOREIGN KEY (`MaterialID`) REFERENCES `material` (`ID`),
  CONSTRAINT `FK_MaterialSupplier_Supplier` FOREIGN KEY (`SupplierID`) REFERENCES `supplier` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `materialsupplier`
--

LOCK TABLES `materialsupplier` WRITE;
/*!40000 ALTER TABLE `materialsupplier` DISABLE KEYS */;
INSERT INTO `materialsupplier` VALUES (80,1),(83,1),(67,3),(6,4),(40,4),(41,4),(67,4),(64,5),(93,5),(24,6),(54,6),(90,6),(15,7),(76,7),(40,8),(57,8),(96,8),(2,9),(30,9),(13,10),(64,10),(88,10),(30,11),(36,11),(64,11),(5,12),(15,12),(40,12),(42,12),(86,12),(6,13),(96,13),(65,14),(54,15),(5,16),(71,16),(2,17),(4,18),(26,18),(64,18),(21,19),(83,19),(90,19),(2,20),(13,20),(64,20),(69,20),(85,20),(3,22),(20,22),(86,22),(10,24),(13,27),(21,27),(73,28),(74,28),(74,29),(37,30),(41,30),(10,31),(61,31),(6,32),(67,32),(69,32),(71,32),(74,32),(85,32),(52,33),(5,34),(10,34),(69,34),(83,35),(85,35),(25,36),(30,38),(58,38),(64,38),(69,38),(71,38),(73,38),(74,38),(76,38),(58,39),(71,39),(88,40),(36,41),(93,41),(99,41),(21,42),(69,42),(85,42),(64,44),(90,44),(73,45),(37,46),(90,46),(74,48),(24,49),(77,49),(80,49);
/*!40000 ALTER TABLE `materialsupplier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `materialtype`
--

DROP TABLE IF EXISTS `materialtype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `materialtype` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DefectedPercent` double NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `materialtype`
--

LOCK TABLES `materialtype` WRITE;
/*!40000 ALTER TABLE `materialtype` DISABLE KEYS */;
INSERT INTO `materialtype` VALUES (1,'Гранулы',0),(2,'Краски',0),(3,'Нитки',0);
/*!40000 ALTER TABLE `materialtype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductTypeID` int DEFAULT NULL,
  `ArticleNumber` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Image` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ProductionPersonCount` int DEFAULT NULL,
  `ProductionWorkshopNumber` int DEFAULT NULL,
  `MinCostForAgent` decimal(10,2) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_Product_ProductType` (`ProductTypeID`),
  CONSTRAINT `FK_Product_ProductType` FOREIGN KEY (`ProductTypeID`) REFERENCES `producttype` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productcosthistory`
--

DROP TABLE IF EXISTS `productcosthistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productcosthistory` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ProductID` int NOT NULL,
  `ChangeDate` datetime(6) NOT NULL,
  `CostValue` decimal(10,2) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_ProductCostHistory_Product` (`ProductID`),
  CONSTRAINT `FK_ProductCostHistory_Product` FOREIGN KEY (`ProductID`) REFERENCES `product` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productcosthistory`
--

LOCK TABLES `productcosthistory` WRITE;
/*!40000 ALTER TABLE `productcosthistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `productcosthistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productmaterial`
--

DROP TABLE IF EXISTS `productmaterial`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productmaterial` (
  `ProductID` int NOT NULL,
  `MaterialID` int NOT NULL,
  `Count` double DEFAULT NULL,
  PRIMARY KEY (`ProductID`,`MaterialID`),
  KEY `FK_ProductMaterial_Material` (`MaterialID`),
  CONSTRAINT `FK_ProductMaterial_Material` FOREIGN KEY (`MaterialID`) REFERENCES `material` (`ID`),
  CONSTRAINT `FK_ProductMaterial_Product` FOREIGN KEY (`ProductID`) REFERENCES `product` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productmaterial`
--

LOCK TABLES `productmaterial` WRITE;
/*!40000 ALTER TABLE `productmaterial` DISABLE KEYS */;
/*!40000 ALTER TABLE `productmaterial` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productsale`
--

DROP TABLE IF EXISTS `productsale`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productsale` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `AgentID` int NOT NULL,
  `ProductID` int NOT NULL,
  `SaleDate` date NOT NULL,
  `ProductCount` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_ProductSale_Agent` (`AgentID`),
  KEY `FK_ProductSale_Product` (`ProductID`),
  CONSTRAINT `FK_ProductSale_Agent` FOREIGN KEY (`AgentID`) REFERENCES `agent` (`ID`),
  CONSTRAINT `FK_ProductSale_Product` FOREIGN KEY (`ProductID`) REFERENCES `product` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productsale`
--

LOCK TABLES `productsale` WRITE;
/*!40000 ALTER TABLE `productsale` DISABLE KEYS */;
/*!40000 ALTER TABLE `productsale` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `producttype`
--

DROP TABLE IF EXISTS `producttype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `producttype` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DefectedPercent` double NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `producttype`
--

LOCK TABLES `producttype` WRITE;
/*!40000 ALTER TABLE `producttype` DISABLE KEYS */;
/*!40000 ALTER TABLE `producttype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shop`
--

DROP TABLE IF EXISTS `shop`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shop` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Address` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `AgentID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_Shop_Agent` (`AgentID`),
  CONSTRAINT `FK_Shop_Agent` FOREIGN KEY (`AgentID`) REFERENCES `agent` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shop`
--

LOCK TABLES `shop` WRITE;
/*!40000 ALTER TABLE `shop` DISABLE KEYS */;
/*!40000 ALTER TABLE `shop` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supplier`
--

DROP TABLE IF EXISTS `supplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `supplier` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `INN` varchar(12) NOT NULL,
  `StartDate` date NOT NULL,
  `QualityRating` int DEFAULT NULL,
  `SupplierType` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supplier`
--

LOCK TABLES `supplier` WRITE;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
INSERT INTO `supplier` VALUES (1,'ГаражТелекомГор','1718185951','2011-12-20',36,'МКК'),(2,'Компания Омск','1878504395','2012-09-13',2,'ОАО'),(3,'ГорМонтаж','1564667734','2016-12-23',79,'ООО'),(4,'Микро','2293562756','2019-05-27',64,'МКК'),(5,'Электро','1755853973','2015-06-16',14,'ЗАО'),(6,'Компания Мотор','1429908355','2010-12-27',50,'ООО'),(7,'Асбоцем','1944834477','2011-04-10',20,'МФО'),(8,'ВостокМети','1488487851','2012-03-13',58,'ООО'),(9,'МясКрепТеле','2152486844','2018-11-11',59,'ПАО'),(10,'Софт','1036521658','2011-11-23',67,'МКК'),(11,'Компания СервисМикроО','1178826599','2012-07-07',5,'ООО'),(12,'ИнфоГазМотор','1954050214','2011-07-23',42,'ОАО'),(13,'Монтаж','1163880101','2016-05-23',10,'ОАО'),(14,'ЭлектроЦвет','1654184411','2015-06-25',3,'ОАО'),(15,'Компания НефтьITИнф','1685247455','2017-03-09',85,'ООО'),(16,'ТомскНефть','1002996016','2015-05-07',95,'ООО'),(17,'ТомскТяжРеч','1102143492','2014-12-22',36,'МФО'),(18,'УралХме','2291253256','2015-05-22',82,'ООО'),(19,'ВодРыб','1113468466','2011-11-25',21,'ЗАО'),(20,'УралСервисМон','1892306757','2016-12-20',26,'МКК'),(21,'Казань','1965011544','2015-03-16',51,'ОАО'),(22,'Cиб','1949139718','2011-11-28',95,'ОАО'),(23,'ГаражГазМ','1740623312','2011-11-20',86,'ОАО'),(24,'МобайлДизайнОмск','1014490629','2019-10-28',73,'ООО'),(25,'ЖелДорГаз','1255275062','2014-09-04',76,'МФО'),(26,'ТверьБухГаз','2167673760','2013-11-13',9,'ОАО'),(27,'ТелекомТранс','2200735978','2015-01-11',8,'ОАО'),(28,'ГаражГлав','1404774111','2013-06-28',89,'МКК'),(29,'Компания К','1468114280','2018-12-07',70,'ПАО'),(30,'ТяжЛифтВостокС','1032089879','2012-08-13',66,'ОАО'),(31,'Компания Во','2027005945','2016-06-22',11,'ПАО'),(32,'МоторКаз','1076279398','2015-08-23',37,'ОАО'),(33,'Сервис','2031832854','2011-11-25',25,'ОАО'),(34,'ЮпитерТомс','1551173338','2011-07-28',60,'ПАО'),(35,'Мор','1906157177','2011-03-06',82,'МКК'),(36,'СеверТехВостокЛизинг','1846812080','2011-02-26',30,'ООО'),(37,'ЦементОбл','2021607106','2015-10-03',42,'ООО'),(38,'Компания КазаньАвтоCиб','1371692583','2015-10-19',23,'МКК'),(39,'ГаражХозФлот','2164720385','2018-08-28',7,'ОАО'),(40,'Компания МорМетал','1947163072','2013-11-18',33,'ООО'),(41,'ГлавРыб','1426268088','2018-11-09',46,'МФО'),(42,'CибCибОрио','1988313615','2018-01-13',95,'ООО'),(43,'ТелеРыбХм','2299034130','2012-02-10',3,'ООО'),(44,'ГлавАвтоГазТрест','2059691335','2014-08-04',18,'МФО'),(45,'ТяжКазаньБашкир','1794419510','2015-12-22',85,'ПАО'),(46,'Асбоцемент','1650212184','2018-12-09',80,'МФО'),(47,'Мотор','1019917089','2017-04-24',19,'ПАО'),(48,'МорФинансФинансМаш','1549496316','2013-06-18',68,'ООО'),(49,'РыбВектор','2275526397','2011-06-20',92,'ОАО'),(50,'Теле','2170198203','2010-05-01',11,'ПАО');
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;
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

-- Dump completed on 2025-04-09 14:27:23
