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
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Clients`
--

LOCK TABLES `Clients` WRITE;
/*!40000 ALTER TABLE `Clients` DISABLE KEYS */;
INSERT INTO `Clients` VALUES (1,'Попов','Михаил','Владимирович','mihail.popov@bk.ru','+7 (915) 234-56-01'),(2,'Лебедев','Илья','Андреевич','ilya.lebedev@yandex.ru','+7 (916) 845-12-13'),(3,'Белов','Роман','Сергеевич','roman.belov@gmail.com','+7 (903) 678-90-23'),(4,'Тимофеев','Артём','Павлович','artem.timofeev@mail.ru','+7 (912) 445-77-34'),(5,'Павлов','Виктор','Николаевич','viktor.pavlov@yandex.ru','+7 (917) 332-11-45'),(6,'Рыбаков','Константин','Игоревич','kosta.rybakov@gmail.com','+7 (909) 556-88-56'),(7,'Нестеров','Сергей','Петрович','sergey.nesterov@mail.ru','+7 (925) 101-23-67'),(8,'Фадеев','Алексей','Михайлович','aleksey.fadeev@yandex.ru','+7 (926) 214-09-78'),(9,'Громов','Денис','Викторович','denis.gromov@gmail.com','+7 (913) 777-44-19'),(10,'Кудрин','Евгений','Анатольевич','evgeny.kudrin@mail.ru','+7 (960) 998-21-20'),(11,'Снегов','Никита','Олегович','nikita.snegov@yandex.ru','+7 (985) 430-12-31'),(12,'Ларин','Павел','Дмитриевич','pavel.larin@gmail.com','+7 (962) 555-33-42'),(13,'Прокофьев','Георгий','Ильич','georgy.prokofiev@mail.ru','+7 (967) 241-54-53'),(14,'Савин','Игорь','Алексеевич','igor.savin@yandex.ru','+7 (903) 812-65-64'),(15,'Денисов','Максим','Романович','maxim.denisov@gmail.com','+7 (915) 329-76-75'),(16,'Пономарёв','Вадим','Сергеевич','vadim.ponomarev@yahoo.com','+7 (916) 218-87-86'),(17,'Акимов','Рустам','Павлович','rustam.akimov@yandex.ru','+7 (903) 640-98-97'),(18,'Веденин','Вячеслав','Иванович','vyacheslav.vedenin@gmail.com','+7 (925) 471-09-08'),(19,'Баранов','Фёдор','Никитич','fedor.baranov@mail.ru','+7 (909) 382-10-19'),(21,'Степанов','Роман','Андреевич','roman.stepanov@gmail.com','+7 (912) 668-32-31'),(22,'Архипов','Дмитрий','Владимирович','dmitriy.arkhipov@mail.ru','+7 (960) 774-43-42'),(23,'Марченко','Тимур','Олегович','timur.marchenko@yandex.ru','+7 (985) 891-54-53'),(24,'Зимин','Матвей','Александрович','matvey.zimin@gmail.com','+7 (926) 210-65-64'),(25,'Лосев','Валерий','Геннадьевич','valeriy.losev@mail.ru','+7 (903) 123-76-75'),(26,'Махов','Арсений','Михайлович','arsen.makhov@yandex.ru','+7 (915) 987-87-86'),(27,'Князев','Иван','Кириллович','ivan.knyazev@gmail.com','+7 (917) 654-98-97'),(29,'Черемисов','Анатолий','Сергеевич','anatoly.cheremisov@yandex.ru','+7 (960) 357-10-19'),(30,'Смоляков','Назар','Игоревич','nazar.smolyakov@gmail.com','+7 (962) 468-21-20'),(31,'Ульянов','Борис','Павлович','boris.ulyanov@mail.ru','+7 (985) 579-32-31'),(32,'Беляков','Олег','Николаевич','oleg.belyakov@yandex.ru','+7 (926) 680-43-42'),(33,'Юдин','Савелий','Викторович','saveliy.yudin@gmail.com','+7 (915) 791-54-53'),(34,'Хлопов','Родион','Григорьевич','rodion.khlopov@mail.ru','+7 (903) 802-65-64'),(35,'Поплавский','Юрий','Алексеевич','yuriy.poplavsky@yandex.ru','+7 (917) 213-76-75'),(36,'Кириллов','Николай','Дмитриевич','nikolay.kirillov@gmail.com','+7 (909) 124-87-86'),(37,'Бородин','Владислав','Петрович','vladislav.borodin@mail.ru','+7 (921) 335-98-97'),(38,'Суханов','Александр','Романович','alex.sukhanov@yandex.ru','+7 (960) 446-09-08'),(40,'Дьячков','Леонид','Сергеевич','leonid.dyachkov@mail.ru','+7 (915) 668-21-20'),(41,'Золотарёв','Раймонд','Андреевич','raymond.zolotarev@gmail.com','+7 (926) 779-32-31'),(42,'Коваленко','Платон','Никитич','platon.kovalenko@yandex.ru','+7 (903) 880-43-42'),(43,'Макеев','Григорий','Александрович','grigory.makeev@mail.ru','+7 (917) 991-54-53'),(44,'Назаров','Роман','Вадимович','roman.nazarov@gmail.com','+7 (960) 102-65-64'),(45,'Рожков','Семен','Павлович','semen.rozhkov@yandex.ru','+7 (962) 213-76-75'),(47,'Калинин','Артур','Олегович','artur.kalinin@gmail.com','+7 (915) 435-98-97'),(48,'Фоминский','Виталий','Денисович','vitaly.fominsky@mail.ru','+7 (903) 546-09-08');
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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Order`
--

LOCK TABLES `Order` WRITE;
/*!40000 ALTER TABLE `Order` DISABLE KEYS */;
INSERT INTO `Order` VALUES (1,2,17,'2025-12-09','2025-12-24',2,78500.30,0.00,0.00),(2,2,19,'2025-12-09','2025-12-13',1,15583.64,0.00,2032.65),(3,2,10,'2025-12-09','2025-12-14',4,123120.12,7980.01,17100.02),(4,2,3,'2025-12-09','2025-12-16',3,24550.00,0.00,0.00),(5,2,19,'2025-12-09','2026-01-14',1,44000.66,0.00,0.00),(6,2,10,'2025-12-09','2026-07-28',2,113925.89,8575.07,0.00);
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
  `ProductPrice` decimal(12,2) NOT NULL,
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
INSERT INTO `OrderProduct` VALUES (1,1,1,18500.30),(1,2,1,60000.00),(2,5,1,13550.99),(3,7,1,7000.00),(3,8,1,77000.11),(3,9,1,30000.00),(4,7,1,7000.00),(4,11,1,17550.00),(5,3,1,44000.66),(6,1,1,18500.30),(6,2,1,60000.00),(6,3,1,44000.66);
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
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Product`
--

LOCK TABLES `Product` WRITE;
/*!40000 ALTER TABLE `Product` DISABLE KEYS */;
INSERT INTO `Product` VALUES (1,'Digital Визитка Pro','Элегантная и современная одностраничная визитка вашей компании в цифровом пространстве. Включает профессиональное представление компании, детальное описание услуг и преимуществ, встроенную форму обратной связи, интерактивную карту расположения офиса, быстрые ссылки на все социальные сети и мессенджеры. Сайт оптимизирован для всех устройств и содержит счётчик посещений. Идеальное решение для стартапов, фрилансеров и малых предприятий, желающих произвести первое впечатление.','Сайт-визитка.png',1,18500.30),(2,'ShopMax – Премиум Магазин','Высокопроизводительный онлайн-магазин нового поколения с продвинутыми функциями электронной коммерции. Полноценный каталог товаров с неограниченным количеством позиций, многоуровневые фильтры по цене, характеристикам и отзывам, умная система рекомендаций похожих товаров. Интегрированы все популярные платёжные системы (Яндекс.Касса, Сбербанк, Paypal), система управления складом с отслеживанием остатков, личный кабинет клиента с историей заказов. Полная аналитика продаж в реальном времени и CRM для работы с клиентами.','Интернет-магазин.png',2,60000.00),(3,'CorporateHub Elite','Авторитетный корпоративный сайт, демонстрирующий профессионализм и масштаб вашей организации. Множество разделов: подробная история компании, полный каталог услуг и решений, команда сотрудников с фото и должностями, новостная лента с последними обновлениями, портфолио завершённых проектов, раздел с отзывами и кейсами клиентов. Встроена передовая CMS для управления контентом без технических знаний, интеграция с внутренними системами (1С, ERP), система документооборота. Мощная аналитика поведения посетителей.','Корпоративный сайт.png',3,44000.66),(4,'ConvertLand - Высокопродуктивный Лендинг','Специально разработанная целевая страница для максимального захвата внимания и преобразования посетителей в клиентов или лидов. Убедительный сценарий продаж с психологически проверенной структурой, минималистичный дизайн без отвлекающих элементов, привлекающие видеоролики и динамичные изображения. Оптимизированные формы заявок с минимальным количеством полей, счётчик отсчёта времени, социальное доказательство (отзывы, рейтинги). Полная интеграция с email-маркетингом и CRM-системами. Подробная аналитика конверсий каждого элемента.','Лендинг.png',4,10000.00),(5,'PortfolioShowcase Premium','Профессиональный сайт-портфолио для демонстрации вашего таланта и опыта. Красивая галерея работ с возможностью увеличения, фильтрации по категориям и датам выполнения. Детальные страницы для каждого проекта с описанием задачи, решения, использованных инструментов и результатов. Секция с информацией об авторе, квалификации, опыте и достижениях. Система отзывов от довольных клиентов с рейтингами и фотографиями. Встроённая форма для получения новых заказов и контактная информация.','Портфолио.png',5,13550.99),(6,'Интеллектуальный Блог','Мощная информационная платформа для публикации и распространения качественного контента. Интуитивная система создания статей с поддержкой богатого форматирования, встраивания видео и инфографики. Автоматическая категоризация и теги для удобной навигации, встроенный поиск по статьям с поддержкой фильтров. Система комментариев с модерацией и уведомлениями, возможность подписки читателей на обновления. Полная оптимизация для SEO, генерация RSS-ленты, интеграция с социальными сетями для автоматического шеринга.','Интелектуальный блог.png',6,11000.00),(7,'Личный Онлайн-Дневник','Уютное пространство для ведения личного дневника и творческого самовыражения в интернете. Приватные или полуоткрытые записи с контролем доступа, система закрепления важных заметок, галерея фото и видео к записям. Интерактивные элементы: опросы для читателей, стена комментариев, система лайков и рейтинга записей. Красивые темы оформления на выбор, возможность экспорта всех записей. Уведомления о новых комментариях и подписках. Полная конфиденциальность и безопасность данных.','Личный блог.png',6,7000.00),(8,'Портал Знаний','Масштабный информационный экосистем для размещения и организации большого объёма контента. Многоуровневая иерархия разделов и подразделов для логичной организации информации, динамическая система быстрого поиска по всему контенту, система рекомендений связанных материалов. Виджеты горячих новостей, рейтинга популярного контента и последних обновлений. Персонализация для каждого пользователя на основе его интересов, система уведомлений о новом контенте. Встроённая аналитика популярности материалов.','Информационный портал.png',7,77000.11),(9,'Динамичный Новостной Портал','Современная платформа для быстрого и эффективного распространения новостей и актуальной информации. Система горячих новостей с автоматическим выдвижением на первый план, категоризация новостей по темам с быстрым переходом между ними. Встроённая система лайков и комментариев для взаимодействия аудитории, возможность поделиться новостью в соцсетях одним кликом. Push-уведомления на мобильные устройства об важных новостях, RSS-лента для подписки. Полная оптимизация для поисковых систем и быстрой индексации.','Новостной сайт.png',8,30000.00),(10,'Cайт Форум','Живое интернет-сообщество для обсуждения интересующих вас тем и обмена ценным опытом. Удобная структура разделов и подразделов по различным категориям, древовидная структура тем и ответов для логичного диалога. Система репутации пользователей, значки и звания для активных участников, приватные сообщения между пользователями. Мощная модераторская панель для контроля качества контента, система автомодерации от спама и оскорблений. Интеграция с соцсетями для удобного входа, уведомления об ответах на ваши посты.','Форум.png',9,19080.99),(11,'Персональный Бренд','Веб-пространство для продвижения вашей личной марки и профессионального имиджа. Выразительная главная страница с кратким резюме и ключевыми компетенциями, детальный раздел опыта с описанием всех значимых проектов и достижений. Полный набор ваших навыков с визуализацией уровня владения, загружаемое резюме в PDF-формате. Портфолио завершённых проектов с описаниями и результатами, секция с благодарностями и рекомендациями от коллег. Встроённая контактная форма для приглашений на новые проекты и сотрудничество.','Персональный сайт.png',10,17550.00),(12,'Рекламная Платформа','Инновационный сайт для запуска и продвижения специальных рекламных кампаний и событий. Захватывающий дизайн с использованием современных визуальных эффектов, видеофон с автоматическим проигрыванием яркого видео. Интерактивные элементы: счётчик дней до события, таймер предложения, слайдеры с преимуществами. Встроённые формы регистрации и заказа предложения, интеграция с социальными сетями для массовой рассылки. Полная аналитика кликов, просмотров и конверсий кампании.','Промо-сайт.png',11,27700.00),(13,'Платформа Онлайн-Обучения','Профессиональная система управления обучением для создания и проведения онлайн-курсов. Структурированные курсы с модулями и уроками, видеолекции в высоком качестве с транскриптами, интерактивные задания и тесты для проверки знаний. Система отслеживания прогресса студентов с детальной статистикой, сертификаты об окончании курсов для резюме. Личные кабинеты студентов и преподавателей с удобным управлением, форум для обсуждения материалов курса. Встроённые платежи за курсы, аналитика популярности и эффективности контента.','Образовательный портал.png',12,80000.00);
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
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES (1,'Солоников','Антон','Сергеевич','1','6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b',1,'+7 (986) 769-19-66'),(2,'Муравьев','Максим','Алексеевич','2','d4735e3a265e16eee03f59718b9b5d03019c07d8b6c51f90da3a666eec13ab35',2,'+7 (930) 751-12-12'),(3,'Сидоркин','Даниил','Дмитриевич','Sidorkin1','8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918',1,'+7 (930) 112-12-65'),(5,'Беляева','Кристина','Олеговна','3','4e07408562bedb8b60ce05c1decfe3ad16b72230967de01f640b7e4729b49fce',3,'+7 (980) 222-21-31'),(6,'Иванов','Иван','Иванович','loginBDgfd2025','fdbabf061aadd0bc7f2eb9bb6c6588cb132addd6e97e046b8ccde7df51f923a3',2,'+7 (930) 451-55-51'),(13,'Беляев','Андрей','Андреевич','loginBDpga2025','6dfed725e346893774e1517b779e98e6bab42452bc09fe0a4cf8b2a7631645ca',2,'+7 (211) 769-11-12'),(19,'Ширяева','Валерия','Павловна','loginBDzzz2025','2d0a3d55e369ba93b5e21787ea0a1eb16430c2047298cb4dccc204a3a2ff2509',1,'+7 (930) 739-19-21'),(28,'Егорова','Виктория','Дмитриевна','loginBDpps2025','837415d9be86d98841e560e604a231c730ffe0913eca63676e6eb9a35f998530',2,'+7 (901) 722-19-22'),(31,'Шиханова','Дарья','Сергеевна','loginBDoiu2025','b390e506a89e4be4ea1be6ffd58e9d1dcbdd6d9fce55c368f4681c4de42662da',1,'+7 (333) 702-19-11'),(35,'Мартынова','Екатерина','Олеговна','loginEO2025','236ce6028a39cbca9311b198d004ac06d2f0e141cf8915ec012c129ab73828fb',2,'+7 (901) 100-22-35'),(38,'Егорова','Дарья','Павловна','loginDP2025','01bff976f3351845be753a796da87e0776b1409ec402090315e09290e0ad1aaf',3,'+7 (901) 100-22-38'),(40,'Богданова','Ирина','Александровна','loginIA2025','075e5ccfe524c85ec85bd50225f238c5b337d11b67753998abc3b41b62d691ec',3,'+7 (901) 100-22-40');
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

-- Dump completed on 2025-12-09 10:26:32
