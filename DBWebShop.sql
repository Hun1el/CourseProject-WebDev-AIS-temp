-- MySQL dump 10.13  Distrib 8.0.39, for Win64 (x86_64)
--
-- Host: localhost    Database: DBWebShop
-- ------------------------------------------------------
-- Server version	8.0.30

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `DBWebShop`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `DBWebShop` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `DBWebShop`;

--
-- Table structure for table `Category`
--

DROP TABLE IF EXISTS `Category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Category` (
  `CategoryID` int NOT NULL AUTO_INCREMENT,
  `CategoryName` varchar(45) NOT NULL,
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Category`
--

LOCK TABLES `Category` WRITE;
/*!40000 ALTER TABLE `Category` DISABLE KEYS */;
INSERT INTO `Category` VALUES (1,'Сайт-визитка'),(2,'Интернет-магазин'),(3,'Корпоративный сайт'),(4,'Лендинг (Landing Page)'),(5,'Портфолио'),(6,'Блог'),(7,'Информационный портал'),(8,'Новостной сайт'),(9,'Форум'),(10,'Персональный сайт'),(11,'Промо-сайт'),(12,'Образовательный портал');
/*!40000 ALTER TABLE `Category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Clients`
--

DROP TABLE IF EXISTS `Clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Clients` (
  `ClientID` int NOT NULL AUTO_INCREMENT,
  `Surname` varchar(90) NOT NULL,
  `FirstName` varchar(75) NOT NULL,
  `MiddleName` varchar(90) DEFAULT NULL,
  `Email` varchar(100) NOT NULL,
  `PhoneNumber` varchar(20) NOT NULL,
  PRIMARY KEY (`ClientID`)
) ENGINE=InnoDB AUTO_INCREMENT=54 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Clients`
--

LOCK TABLES `Clients` WRITE;
/*!40000 ALTER TABLE `Clients` DISABLE KEYS */;
INSERT INTO `Clients` VALUES (1,'Попов','Михаил','Владимирович','mihail.popov@mail.ru','+7 (915) 234-56-01'),(2,'Лебедев','Илья','Андреевич','ilya.lebedev@yandex.ru','+7 (916) 845-12-13'),(3,'Белов','Роман','Сергеевич','roman.belov@gmail.com','+7 (903) 678-90-23'),(4,'Тимофеев','Артём','Павлович','artem.timofeev@mail.ru','+7 (912) 445-77-34'),(5,'Павлов','Виктор','Николаевич','viktor.pavlov@yandex.ru','+7 (917) 332-11-45'),(6,'Рыбаков','Константин','Игоревич','kosta.rybakov@gmail.com','+7 (909) 556-88-56'),(7,'Нестеров','Сергей','Петрович','sergey.nesterov@mail.ru','+7 (925) 101-23-67'),(8,'Фадеев','Алексей','Михайлович','aleksey.fadeev@yandex.ru','+7 (926) 214-09-78'),(9,'Громов','Денис','Викторович','denis.gromov@gmail.com','+7 (913) 777-44-19'),(10,'Кудрин','Евгений','Анатольевич','evgeny.kudrin@mail.ru','+7 (960) 998-21-20'),(11,'Снегов','Никита','Олегович','nikita.snegov@yandex.ru','+7 (985) 430-12-31'),(12,'Ларин','Павел','Дмитриевич','pavel.larin@gmail.com','+7 (962) 555-33-42'),(13,'Прокофьев','Георгий','Ильич','georgy.prokofiev@mail.ru','+7 (967) 241-54-53'),(14,'Савин','Игорь','Алексеевич','igor.savin@yandex.ru','+7 (903) 812-65-64'),(15,'Денисов','Максим','Романович','maxim.denisov@gmail.com','+7 (915) 329-76-75'),(16,'Пономарёв','Вадим','Сергеевич','vadim.ponomarev@yahoo.com','+7 (916) 218-87-86'),(17,'Акимов','Рустам','Павлович','rustam.akimov@yandex.ru','+7 (903) 640-98-97'),(18,'Веденин','Вячеслав','Иванович','vyacheslav.vedenin@gmail.com','+7 (925) 471-09-08'),(19,'Баранов','Фёдор','Никитич','fedor.baranov@mail.ru','+7 (909) 382-10-19'),(20,'Коровин','Степан','Петрович','stepan.korovin@yandex.ru','+7 (921) 507-21-20'),(21,'Степанов','Роман','Андреевич','roman.stepanov@gmail.com','+7 (912) 668-32-31'),(22,'Архипов','Дмитрий','Владимирович','dmitriy.arkhipov@mail.ru','+7 (960) 774-43-42'),(23,'Марченко','Тимур','Олегович','timur.marchenko@yandex.ru','+7 (985) 891-54-53'),(24,'Зимин','Матвей','Александрович','matvey.zimin@gmail.com','+7 (926) 210-65-64'),(25,'Лосев','Валерий','Геннадьевич','valeriy.losev@mail.ru','+7 (903) 123-76-75'),(26,'Махов','Арсений','Михайлович','arsen.makhov@yandex.ru','+7 (915) 987-87-86'),(27,'Князев','Иван','Кириллович','ivan.knyazev@gmail.com','+7 (917) 654-98-97'),(28,'Шаповалов','Пётр','Евгеньевич','petr.shapovalov@mail.ru','+7 (909) 246-09-08'),(29,'Черемисов','Анатолий','Сергеевич','anatoly.cheremisov@yandex.ru','+7 (960) 357-10-19'),(30,'Смоляков','Назар','Игоревич','nazar.smolyakov@gmail.com','+7 (962) 468-21-20'),(31,'Ульянов','Борис','Павлович','boris.ulyanov@mail.ru','+7 (985) 579-32-31'),(32,'Беляков','Олег','Николаевич','oleg.belyakov@yandex.ru','+7 (926) 680-43-42'),(33,'Юдин','Савелий','Викторович','saveliy.yudin@gmail.com','+7 (915) 791-54-53'),(34,'Хлопов','Родион','Григорьевич','rodion.khlopov@mail.ru','+7 (903) 802-65-64'),(35,'Поплавский','Юрий','Алексеевич','yuriy.poplavsky@yandex.ru','+7 (917) 213-76-75'),(36,'Кириллов','Николай','Дмитриевич','nikolay.kirillov@gmail.com','+7 (909) 124-87-86'),(37,'Бородин','Владислав','Петрович','vladislav.borodin@mail.ru','+7 (921) 335-98-97'),(38,'Суханов','Александр','Романович','alex.sukhanov@yandex.ru','+7 (960) 446-09-08'),(39,'Крыловский','Егор','Иванович','egor.krylovsky@gmail.com','+7 (962) 557-10-19'),(40,'Дьячков','Леонид','Сергеевич','leonid.dyachkov@mail.ru','+7 (915) 668-21-20'),(41,'Золотарёв','Раймонд','Андреевич','raymond.zolotarev@gmail.com','+7 (926) 779-32-31'),(42,'Коваленко','Платон','Никитич','platon.kovalenko@yandex.ru','+7 (903) 880-43-42'),(43,'Макеев','Григорий','Александрович','grigory.makeev@mail.ru','+7 (917) 991-54-53'),(44,'Назаров','Роман','Вадимович','roman.nazarov@gmail.com','+7 (960) 102-65-64'),(45,'Рожков','Семен','Павлович','semen.rozhkov@yandex.ru','+7 (962) 213-76-75'),(46,'Прохоров','Даниил','Викторович','daniil.prokhorov@mail.ru','+7 (921) 324-87-86'),(47,'Калинин','Артур','Олегович','artur.kalinin@gmail.com','+7 (915) 435-98-97'),(48,'Фоминский','Виталий','Денисович','vitaly.fominsky@mail.ru','+7 (903) 546-09-08'),(49,'Райков','Евгений','Михайлович','evgeny.raikov@yandex.ru','+7 (960) 657-10-19');
/*!40000 ALTER TABLE `Clients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Order`
--

DROP TABLE IF EXISTS `Order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Order` (
  `OrderID` int NOT NULL AUTO_INCREMENT,
  `UserID` int NOT NULL,
  `ClientID` int NOT NULL,
  `OrderDate` date NOT NULL,
  `OrderCompDate` date NOT NULL,
  `StatusID` int NOT NULL,
  `OrderCost` decimal(12,2) NOT NULL,
  `Discount` decimal(12,2) DEFAULT '0.00',
  `Surcharge` decimal(12,2) DEFAULT '0.00',
  PRIMARY KEY (`OrderID`),
  KEY `fk_order_user_idx` (`UserID`),
  KEY `fk_order_client_idx` (`ClientID`),
  KEY `fk_order_status_idx` (`StatusID`),
  CONSTRAINT `fk_order_client` FOREIGN KEY (`ClientID`) REFERENCES `Clients` (`ClientID`),
  CONSTRAINT `fk_order_status` FOREIGN KEY (`StatusID`) REFERENCES `Status` (`StatusID`),
  CONSTRAINT `fk_order_user` FOREIGN KEY (`UserID`) REFERENCES `Users` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=85 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Order`
--

LOCK TABLES `Order` WRITE;
/*!40000 ALTER TABLE `Order` DISABLE KEYS */;
INSERT INTO `Order` VALUES (1,2,5,'2025-01-05','2025-01-12',3,1200.00,0.00,0.00),(2,2,8,'2025-01-10','2025-01-17',3,1000.00,0.00,0.00),(3,6,12,'2025-01-15','2025-01-22',3,1500.00,0.00,0.00),(4,7,20,'2025-01-18','2025-01-25',3,900.00,0.00,0.00),(5,7,2,'2025-01-20','2025-01-27',3,1100.00,0.00,0.00),(6,8,7,'2025-01-22','2025-01-30',3,4500.00,0.00,0.00),(7,7,15,'2025-01-25','2025-02-01',3,3500.00,0.00,0.00),(8,8,19,'2025-01-28','2025-02-04',3,5000.00,0.00,0.00),(9,11,22,'2025-01-30','2025-02-06',3,4800.00,0.00,0.00),(10,13,1,'2025-02-01','2025-02-08',3,4200.00,0.00,0.00),(11,13,4,'2025-02-03','2025-02-10',3,3000.00,0.00,0.00),(12,14,9,'2025-02-05','2025-02-12',3,3200.00,0.00,0.00),(13,15,13,'2025-02-07','2025-02-14',3,3500.00,0.00,0.00),(14,17,17,'2025-02-09','2025-02-16',3,3300.00,0.00,0.00),(15,21,21,'2025-02-11','2025-02-18',3,3400.00,0.00,0.00),(16,21,25,'2025-02-13','2025-02-20',3,1500.00,0.00,0.00),(17,22,28,'2025-02-15','2025-02-22',3,1400.00,0.00,0.00),(18,22,31,'2025-02-17','2025-02-24',3,1300.00,0.00,0.00),(19,23,34,'2025-02-19','2025-02-26',3,1600.00,0.00,0.00),(20,26,37,'2025-02-21','2025-02-28',3,1700.00,0.00,0.00),(21,27,40,'2025-02-23','2025-03-02',3,900.00,0.00,0.00),(22,28,43,'2025-02-25','2025-03-04',3,1000.00,0.00,0.00),(23,29,46,'2025-02-27','2025-03-06',3,1100.00,0.00,0.00),(24,29,49,'2025-03-01','2025-03-08',3,950.00,0.00,0.00),(25,27,3,'2025-03-03','2025-03-10',3,1050.00,0.00,0.00),(26,26,6,'2025-03-05','2025-03-12',3,800.00,0.00,0.00),(27,27,9,'2025-03-07','2025-03-14',3,900.00,0.00,0.00),(28,28,12,'2025-03-09','2025-03-16',3,850.00,0.00,0.00),(29,29,15,'2025-03-11','2025-03-18',3,750.00,0.00,0.00),(30,32,18,'2025-03-13','2025-03-20',3,700.00,0.00,0.00),(31,32,21,'2025-03-15','2025-03-22',3,4000.00,0.00,0.00),(32,32,24,'2025-03-17','2025-03-24',3,3800.00,0.00,0.00),(33,35,27,'2025-03-19','2025-03-26',3,4200.00,0.00,0.00),(34,38,30,'2025-03-21','2025-03-28',3,3900.00,0.00,0.00),(35,35,33,'2025-03-23','2025-03-30',3,3700.00,0.00,0.00),(36,40,36,'2025-03-25','2025-04-01',3,2500.00,0.00,0.00),(37,40,39,'2025-03-27','2025-04-03',3,2700.00,0.00,0.00),(38,44,42,'2025-03-29','2025-04-05',3,2600.00,0.00,0.00),(39,44,45,'2025-03-31','2025-04-07',3,2800.00,0.00,0.00),(40,46,48,'2025-04-02','2025-04-09',3,2400.00,0.00,0.00),(41,49,1,'2025-04-04','2025-04-11',3,1800.00,0.00,0.00),(42,46,4,'2025-04-06','2025-04-13',3,1500.00,0.00,0.00),(43,27,7,'2025-04-08','2025-04-15',3,1600.00,0.00,0.00),(44,44,10,'2025-04-10','2025-04-17',3,1700.00,0.00,0.00),(45,2,13,'2025-04-12','2025-04-19',3,1400.00,0.00,0.00),(46,46,16,'2025-04-14','2025-04-21',3,700.00,0.00,0.00),(47,6,19,'2025-04-16','2025-04-23',3,750.00,0.00,0.00),(48,11,22,'2025-04-18','2025-04-25',3,800.00,0.00,0.00),(49,49,25,'2025-04-20','2025-04-27',3,1300.00,0.00,0.00),(50,14,28,'2025-04-22','2025-04-29',3,4500.00,0.00,0.00),(51,13,22,'2025-10-28','2025-10-28',1,1200.00,0.00,0.00),(52,47,17,'2025-10-28','2025-10-31',1,3400.00,0.00,0.00),(53,2,22,'2025-10-28','2025-10-28',1,1200.00,0.00,0.00),(54,2,22,'2025-10-28','2025-10-28',1,1200.00,0.00,0.00),(55,2,32,'2025-10-28','2025-10-28',1,1200.00,0.00,0.00),(56,2,3,'2025-10-28','2025-10-28',1,1200.00,0.00,0.00),(57,2,22,'2025-10-28','2025-10-28',1,8200.00,0.00,0.00),(58,2,22,'2025-10-28','2025-10-28',1,1200.00,0.00,0.00),(59,2,19,'2025-10-28','2025-10-28',1,1200.00,0.00,0.00),(60,2,3,'2025-10-28','2025-10-28',1,1200.00,0.00,0.00),(61,2,32,'2025-10-28','2025-10-28',1,1200.00,0.00,0.00),(62,2,3,'2025-10-28','2025-10-28',1,1200.00,0.00,0.00),(63,2,22,'2025-10-28','2025-10-28',1,1200.00,0.00,0.00),(64,2,22,'2025-10-28','2025-10-28',1,4200.00,0.00,0.00),(65,2,37,'2025-10-28','2025-12-28',1,14400.00,0.00,0.00),(66,2,19,'2025-10-29','2025-10-28',1,1200.00,0.00,0.00),(67,2,17,'2025-10-29','2025-11-05',1,1200.00,0.00,0.00),(68,2,17,'2025-10-29','2025-11-06',1,1000.00,0.00,0.00),(69,2,3,'2025-10-29','2025-11-05',1,1200.00,0.00,0.00),(70,2,22,'2025-10-29','2025-11-05',1,3256.00,0.00,0.00),(71,2,22,'2025-10-29','2025-11-04',1,3811.00,0.00,0.00),(72,2,22,'2025-10-29','2025-11-05',1,5105.00,696.12,0.00),(73,2,22,'2025-10-29','2025-11-18',2,3811.00,444.00,555.00),(74,2,3,'2025-11-17','2025-11-24',4,5766.00,434.00,0.00),(75,2,17,'2025-11-17','2025-11-25',1,6000.00,0.00,0.00),(76,2,43,'2025-11-17','2025-11-21',1,6265.00,406.07,870.15),(77,2,38,'2025-11-17','2025-11-24',1,1200.00,0.00,0.00),(78,2,3,'2025-11-19','2025-11-26',1,2090.00,110.00,0.00),(79,2,17,'2025-11-19','2025-11-26',1,3534.00,266.00,0.00),(80,2,22,'2025-11-19','2025-11-26',1,1140.00,60.00,0.00),(81,2,22,'2025-11-23','2025-11-30',1,1140.00,60.00,0.00),(82,2,3,'2025-11-23','2025-11-30',1,2090.00,110.00,0.00),(83,2,24,'2025-11-24','2025-11-27',1,13504811.00,875311.85,1875668.25),(84,2,33,'2025-11-24','2025-12-01',1,2000.00,0.00,0.00);
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
  `ProductID` int NOT NULL,
  `ProductCount` int NOT NULL,
  PRIMARY KEY (`OrderID`,`ProductID`),
  KEY `fk_orderproduct_product_idx` (`ProductID`),
  KEY `fk_orderproduct_order_idx` (`OrderID`),
  CONSTRAINT `fk_orderproduct_order` FOREIGN KEY (`OrderID`) REFERENCES `Order` (`OrderID`),
  CONSTRAINT `fk_orderproduct_product` FOREIGN KEY (`ProductID`) REFERENCES `Product` (`ProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `OrderProduct`
--

LOCK TABLES `OrderProduct` WRITE;
/*!40000 ALTER TABLE `OrderProduct` DISABLE KEYS */;
INSERT INTO `OrderProduct` VALUES (74,1,1),(74,3,1),(74,7,1),(75,1,1),(75,9,1),(76,5,1),(76,14,1),(76,17,1),(76,52,1),(77,1,1),(78,1,1),(78,2,1),(79,1,1),(79,3,1),(79,5,1),(80,1,1),(81,1,1),(82,1,1),(82,2,1),(83,1,1),(83,2,1),(83,3,1),(84,4,1),(84,5,1);
/*!40000 ALTER TABLE `OrderProduct` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Product`
--

DROP TABLE IF EXISTS `Product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Product` (
  `ProductID` int NOT NULL AUTO_INCREMENT,
  `ProductName` varchar(75) NOT NULL,
  `ProductDescription` text NOT NULL,
  `ProductPhoto` text,
  `CategoryID` int NOT NULL,
  `BasePrice` decimal(9,2) NOT NULL,
  PRIMARY KEY (`ProductID`),
  KEY `fk_product_category_idx` (`CategoryID`),
  CONSTRAINT `fk_product_category` FOREIGN KEY (`CategoryID`) REFERENCES `Category` (`CategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Product`
--

LOCK TABLES `Product` WRITE;
/*!40000 ALTER TABLE `Product` DISABLE KEYS */;
INSERT INTO `Product` VALUES (1,'Визитка \"Tech Startup\"','Мини-сайт для презентации IT-стартапа и его услуг уыолплрывлпрывошрпшдыдфддддддdsGHHHHHHHHHHHHDKJSGHDSJOGHLSDJGHSDLGHDSJGHLSDJGHDSHGDSJIGHSDJIGDSJGHDSJHGDJGHDSJHKGDHDSGJHDGSJHDSGZJHKDSGHJKDSZGJHDHSJFвыпвцыпывпвпвыпывSGDGSDGsdgdsgsdghdfhdfhffdGDSGAHSHEARHERHERDSHGSFSФыафыаsafdasgAAAABfdsdd','2.png',1,9999999.98),(2,'Визитка \"Creative Studio\"','Визитка для творческой студии с портфолио и контактами','',1,9999999.51),(3,'Визитка \"Law & Partners\"','Простой сайт-визитка для юридической фирмы с информацией о специалистах','',1,9999999.50),(4,'Визитка \"Café Aroma\"','Сайт-визитка для кофейни с меню и контактами','',1,9999999.50),(5,'Визитка \"FitLife Club\"','Презентация фитнес-клуба, расписание занятий и контакты','',1,9999999.51),(6,'Интернет-магазин \"ShopEasy\"','Полнофункциональный онлайн-магазин бытовой техники с корзиной и оплатой','',2,9999999.00),(7,'Интернет-магазин \"BookWorld\"','Платформа для продажи книг с каталогом и системой отзывов','',2,9999999.50),(8,'Интернет-магазин \"FashionHub\"','Магазин одежды с фильтрацией, каталогом и онлайн-оплатой','',2,9999999.00),(9,'Интернет-магазин \"Gadget Store\"','Продажа электроники с описаниями, фото и онлайн-оплатой','',2,9999999.00),(10,'Интернет-магазин \"EcoMarket\"','Эко-магазин с каталогом товаров и оформлением заказов онлайн','',2,9999999.00),(11,'Корпоративный сайт \"IT Solutions\"','Сайт компании IT-сферы с информацией о проектах и командой','',3,9999999.00),(12,'Корпоративный сайт \"GreenEnergy\"','Сайт экологической компании с описанием услуг и кейсами','',3,3200.00),(13,'Корпоративный сайт \"FinancePro\"','Сайт финансовой компании с услугами и формой обратной связи','',3,3500.00),(14,'Корпоративный сайт \"Consulting Co\"','Сайт консалтинговой компании с портфолио и контактами','',3,3300.00),(15,'Корпоративный сайт \"Medical Center\"','Сайт медицинского центра с расписанием и записью на прием','',3,3400.00),(16,'Лендинг \"Startup Launch\"','Одностраничный сайт для презентации стартапа и сбора лидов','',4,1500.00),(17,'Лендинг \"App Promo\"','Лендинг для продвижения мобильного приложения с формой загрузки','',4,1400.00),(18,'Лендинг \"Online Course\"','Страница для онлайн-курса с описанием программы и регистрацией','',4,1300.00),(19,'Лендинг \"Event Promo\"','Одностраничный сайт мероприятия с программой и регистрацией','',4,1600.00),(20,'Лендинг \"Product Launch\"','Лендинг для презентации нового продукта с формой заказа','',4,1700.00),(21,'Портфолио \"Alex Designer\"','Персональное портфолио веб-дизайнера с примерами работ','',5,900.00),(22,'Портфолио \"Photographer Pro\"','Портфолио фотографа с галереей и контактной информацией','',5,1000.00),(23,'Портфолио \"Web Developer\"','Портфолио веб-разработчика с кейсами проектов','',5,1100.00),(24,'Портфолио \"Illustrator Studio\"','Галерея иллюстратора с контактами для заказов','',5,950.00),(25,'Портфолио \"Architect Portfolio\"','Портфолио архитектора с проектами и описаниями','',5,1050.00),(26,'Блог \"Travel Diaries\"','Блог о путешествиях с фото, советами и маршрутами','',6,800.00),(27,'Блог \"Healthy Life\"','Блог о здоровье, фитнесе и правильном питании','',6,900.00),(28,'Блог \"Tech Insights\"','Технологический блог с новостями и обзорами гаджетов','',6,850.00),(29,'Блог \"Food Lovers\"','Кулинарный блог с рецептами и советами по готовке','',6,750.00),(30,'Блог \"Parenting Tips\"','Блог для родителей с советами по воспитанию и развитию детей','',6,700.00),(31,'Информационный портал \"EduPortal\"','Образовательный портал с материалами для школ и учеников','',7,4000.00),(32,'Информационный портал \"KnowledgeBase\"','Портал знаний с библиотекой статей и инструкций','',7,3800.00),(33,'Информационный портал \"Science Hub\"','Научный портал с публикациями и исследованиями','',7,4200.00),(34,'Информационный портал \"Health Info\"','Медицинский портал с актуальной информацией и статьями','',7,3900.00),(35,'Информационный портал \"City Guide\"','Портал города с афишей событий, картой и справкой','',7,3700.00),(36,'Новостной сайт \"Daily News\"','Сайт с актуальными локальными новостями и событиями','',8,2500.00),(37,'Новостной сайт \"Tech News\"','Новости технологий и стартапов с аналитикой','',8,2700.00),(38,'Новостной сайт \"Sports Arena\"','Новости спорта, обзоры матчей и интервью','',8,2600.00),(39,'Новостной сайт \"Finance Daily\"','Новости финансов, рынков и экономики','',8,2800.00),(40,'Новостной сайт \"Culture Beat\"','Новости культуры, искусства и мероприятий','',8,2400.00),(41,'Форум \"CodeTalk\"','Форум для программистов с обсуждением технологий и проектов','',9,1800.00),(42,'Форум \"GamersHub\"','Форум для геймеров с обсуждением игр и турниров','',9,1500.00),(43,'Форум \"TravelersForum\"','Форум путешественников для обмена советами и маршрутами','',9,1600.00),(44,'Форум \"HealthForum\"','Форум о здоровье, фитнесе и правильном питании','',9,1700.00),(45,'Форум \"BookClub\"','Форум любителей книг с рецензиями и обсуждениями','',9,1400.00),(46,'Персональный сайт \"John Doe\"','Персональный сайт с биографией и контактами','',10,700.00),(47,'Персональный сайт \"Jane Smith\"','Сайт специалиста с резюме и портфолио','',10,750.00),(48,'Персональный сайт \"Freelancer Portfolio\"','Сайт фрилансера с кейсами и отзывами клиентов','',10,800.00),(49,'Промо-сайт \"Event Promo\"','Одностраничный сайт для промо-акции или мероприятия','',11,1300.00),(51,'2  5 23            4 242','12','Диграмма вариантов использования.jpg',2,3253.00),(52,'Уцевп','3452135trurtutru','2025-10-16_19-27-01.png',2,9999999.00);
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
  `RoleName` varchar(30) NOT NULL,
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Role`
--

LOCK TABLES `Role` WRITE;
/*!40000 ALTER TABLE `Role` DISABLE KEYS */;
INSERT INTO `Role` VALUES (1,'Администратор'),(2,'Менеджер'),(3,'Директор');
/*!40000 ALTER TABLE `Role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Status`
--

DROP TABLE IF EXISTS `Status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Status` (
  `StatusID` int NOT NULL AUTO_INCREMENT,
  `StatusName` varchar(30) NOT NULL,
  PRIMARY KEY (`StatusID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Status`
--

LOCK TABLES `Status` WRITE;
/*!40000 ALTER TABLE `Status` DISABLE KEYS */;
INSERT INTO `Status` VALUES (1,'Новый'),(2,'В работе'),(3,'Завершён'),(4,'Отменён');
/*!40000 ALTER TABLE `Status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Users` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `Surname` varchar(90) NOT NULL,
  `FirstName` varchar(75) NOT NULL,
  `MiddleName` varchar(90) DEFAULT NULL,
  `UserLogin` varchar(20) NOT NULL,
  `UserPassword` varchar(255) NOT NULL,
  `RoleID` int NOT NULL,
  `PhoneNumber` varchar(20) NOT NULL,
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `UserLogin_UNIQUE` (`UserLogin`),
  UNIQUE KEY `UserPassword_UNIQUE` (`UserPassword`),
  KEY `fk_user_role_idx` (`RoleID`),
  CONSTRAINT `fk_user_role` FOREIGN KEY (`RoleID`) REFERENCES `Role` (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES (1,'Солоников','Антон','Сергеевич','1','6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b',1,'+7 (986) 769-19-66'),(2,'Муравьев','Максим','Алексеевич','2','d4735e3a265e16eee03f59718b9b5d03019c07d8b6c51f90da3a666eec13ab35',2,'+7 (930) 751-12-12'),(3,'Сидоркин','Даниил','Дмитриевич','Sidorkin','1cf6c028454bc66cc79e4d41e3ad3cebfa18764bee6c0c64c5730c92952f66bc',1,'+7 (930) 112-12-65'),(4,'Муравьева','Мария','Викторовна','loginBDqsa2025','85b561e18b2e8168319a366e185758610aac0e3449a657320ab9106cbc3046af',3,'+7 (920) 115-29-16'),(5,'Беляева','Кристина','Олеговна','3','4e07408562bedb8b60ce05c1decfe3ad16b72230967de01f640b7e4729b49fce',3,'+7 (980) 222-21-31'),(6,'Иванов','Иван','Иванович','loginBDgfd2025','fdbabf061aadd0bc7f2eb9bb6c6588cb132addd6e97e046b8ccde7df51f923a3',2,'+7 (930) 451-55-51'),(7,'Панчин','Кирилл','Валерьевич','loginBDqag2025','03689bedb212f99be38b079e81f2d8f4bd261787559722ae84ea9495cb0c93ff',2,'+7 (980) 742-32-52'),(8,'Сидоров','Дмитрий','Алексеевич','loginBDqss2025','9d6d060afba95d92cf7d55348b40103d6506e89162f95b84a7a02ea84aba55f7',2,'+7 (222) 122-62-87'),(11,'Комаров','Арсений','Владимирович','loginBDhff2025','1409821fa43ffcb5c02db67ea087e532008394e79339ff3f673d95eab6cd6a48',2,'+7 (930) 551-64-72'),(13,'Беляев','Андрей','Андреевич','loginBDpga2025','6dfed725e346893774e1517b779e98e6bab42452bc09fe0a4cf8b2a7631645ca',2,'+7 (211) 769-11-12'),(14,'Титов','Валентин','Петрович','4','bd6734503e241e4d9add72c8dd2c9bb0856d1106e94fd6bb9c4efb37daf54117',2,'+7 (986) 769-19-66'),(15,'Сергеев','Александр','Сильвестрович','loginBDnbg2025','a50f1cc23e4f358ae22385ae7675bb199d24139158cf6c6acd2193cae94c3142',2,'+7 (777) 755-12-11'),(17,'Максимов','Денис','Владимирович','loginBDzvl2025','fa01c6d725742afb81172790f3f3bce180051742ff326c9155cbc385ca936b57',2,'+7 (930) 719-88-99'),(19,'Ширяева','Валерия','Павловна','loginBDzzz2025','2d0a3d55e369ba93b5e21787ea0a1eb16430c2047298cb4dccc204a3a2ff2509',1,'+7 (930) 739-19-21'),(21,'Виноградова','Ирина','Андреевна','loginBDlyl2025','352dcc815afefb9aeb689b51a95ad6350655792d4932fb22c01bb8fa2136a7b2',2,'+7 (922) 729-13-41'),(22,'Осипова','Наталья','Викторовна','loginBDlol2025','494015d120f20e88a3595918f44ea99b7e0b0747dee370cbbdbefac898586952',2,'+7 (930) 522-64-31'),(23,'Борисов','Николай','Максимович','loginBDdnd2025','b38a193e265e059cf28bd22af33cbaad26ec55dbab35b5090b8e02e72f896c03',2,'+7 (142) 742-42-42'),(26,'Макарова','Надежда','Ивановна','loginBDaud2025','47870d79e0a29c4756831dbe0ac7f875ddec9656101ac288d43027663e4e70e1',2,'+7 (120) 769-12-11'),(27,'Миронов','Антон',NULL,'loginBDaux2025','4aeb59a5d1c179141d591186d96c232b1389aeb44893ec7776cbb845f47f74e9',2,'+7 (900) 769-11-00'),(28,'Егорова','Виктория','Дмитриевна','loginBDpps2025','837415d9be86d98841e560e604a231c730ffe0913eca63676e6eb9a35f998530',2,'+7 (901) 722-19-22'),(29,'Фомин','Виктор','','loginBDlkj2025','85c8e3b3fa305eb938ce39f78cb9f4ace538257aef38f98afb740b94e29146ec',2,'+7 (980) 769-10-90'),(31,'Шиханова','Дарья','Сергеевна','loginBDoiu2025','b390e506a89e4be4ea1be6ffd58e9d1dcbdd6d9fce55c368f4681c4de42662da',1,'+7 (333) 702-19-11'),(32,'Орлова','Мария','Владимировна','loginMV2025','b54c93f6e2d7573fe51f2e91ac14cd38a58566a4c23aa5e548911047b2b368b2',2,'+7 (901) 100-22-32'),(33,'Тарасов','Денис','Сергеевич','loginDS2025','d775b07e2cde2cb491a668066a730b72f663f77f93746f4fba8ab74b864a3789',1,'+7 (901) 100-22-33'),(35,'Мартынова','Екатерина','Олеговна','loginEO2025','236ce6028a39cbca9311b198d004ac06d2f0e141cf8915ec012c129ab73828fb',2,'+7 (901) 100-22-35'),(36,'Фролова','Софья','Дмитриевна','loginSD2025','0e37e84e820812abca771cbe20261703f97863913958ea22588490a895360fa5',1,'+7 (901) 100-22-36'),(38,'Егорова','Дарья','Павловна','loginDP2025','01bff976f3351845be753a796da87e0776b1409ec402090315e09290e0ad1aaf',2,'+7 (901) 100-22-38'),(39,'Матвеева','Валерия','Ивановна','loginVI2025','409eefe687aed3f45b30ca0ecb74bdf0f282f4cfd0587bf6b6833c8b7749f791',1,'+7 (901) 100-22-39'),(40,'Богданова','Ирина','Александровна','loginIA2025','075e5ccfe524c85ec85bd50225f238c5b337d11b67753998abc3b41b62d691ec',2,'+7 (901) 100-22-40'),(42,'Власова','Ксения','Григорьевна','loginKG2025','acac7f7a0c93fddd4e558ed2645b24ced385cb4469b1b4c077d187862a9255db',1,'+7 (901) 100-22-42'),(44,'Миронова','Алиса','Константиновна','loginAK2025','1421cbff315854c2a749a53bfe807ed9d2bbcd60e824ac75dca6b95f2cdeccb5',2,'+7 (901) 100-22-44'),(45,'Фомина','Виктория','Игоревна','loginVIc2025','20e09ea4411e1e052a6dc394048ab6981494cf2efa62dce03bb9e92fe4872b2a',1,'+7 (901) 100-22-45'),(46,'Ширяева','Алина','Романовна','loginAR2025','f6c985f534b286147ca2ef5fc6c610d0d6aa60067aa6c51265a5d845ee30b9f3',2,'+7 (901) 100-22-46'),(47,'Анисимова','Кристина','Владимировна','loginKV2025','14cb6b8e457deca3cfa677dda8844717bf5201f6285d8cf9bd5ae9e71b06eb9a',3,'+7 (901) 100-22-47'),(48,'Осипова','Маргарита','Сергеевна','loginMS2025','1c5976ca4d7c82294d42903aa1fdf4791cffa25896ac771fca064bdd636ffd82',1,'+7 (901) 100-22-48'),(49,'Трофимова','Наталья','Ильинична','loginNI2025','00ecf881eff87565f0080ebf5e86cf37031b9c32b06483cef1d833a45f010a97',2,'+7 (901) 100-22-49'),(51,'Комарова','Юлия','Михайловна','loginYM2025','8a45d81cc30a91c66ea6f3e89eb3788226538a67d09f857c8c8bc7d5bff54c05',1,'+7 (901) 100-22-51');
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-11-24 18:34:25
