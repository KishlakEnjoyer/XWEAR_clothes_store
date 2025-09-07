CREATE DATABASE  IF NOT EXISTS `xwear_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `xwear_db`;
-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: xwear_db
-- ------------------------------------------------------
-- Server version	9.0.1

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
-- Table structure for table `brands`
--

DROP TABLE IF EXISTS `brands`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `brands` (
  `brand_id` int NOT NULL AUTO_INCREMENT,
  `brand_name` varchar(70) DEFAULT NULL,
  PRIMARY KEY (`brand_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `brands`
--

LOCK TABLES `brands` WRITE;
/*!40000 ALTER TABLE `brands` DISABLE KEYS */;
INSERT INTO `brands` VALUES (1,'Nike'),(2,'Adidas'),(3,'Kappa'),(4,'Puma'),(5,'Tommy Hilfiger'),(6,'GAP'),(7,'CalvinKlein'),(8,'Lacoste');
/*!40000 ALTER TABLE `brands` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cart`
--

DROP TABLE IF EXISTS `cart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cart` (
  `user_email` varchar(120) NOT NULL,
  `good_article` varchar(8) NOT NULL,
  `good_count` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`user_email`,`good_article`),
  KEY `fk_cart_good_idx` (`good_article`),
  CONSTRAINT `fk_cart_good` FOREIGN KEY (`good_article`) REFERENCES `goods` (`good_article`),
  CONSTRAINT `fk_cart_user` FOREIGN KEY (`user_email`) REFERENCES `users` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cart`
--

LOCK TABLES `cart` WRITE;
/*!40000 ALTER TABLE `cart` DISABLE KEYS */;
/*!40000 ALTER TABLE `cart` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categories` (
  `category_id` int NOT NULL AUTO_INCREMENT,
  `category_name` varchar(70) DEFAULT NULL,
  PRIMARY KEY (`category_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (1,'Обувь'),(2,'Одежда');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `good_size`
--

DROP TABLE IF EXISTS `good_size`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `good_size` (
  `good_article` varchar(8) NOT NULL,
  `size` float NOT NULL,
  `price` float DEFAULT NULL,
  PRIMARY KEY (`good_article`,`size`),
  CONSTRAINT `fk_good_size` FOREIGN KEY (`good_article`) REFERENCES `goods` (`good_article`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `good_size`
--

LOCK TABLES `good_size` WRITE;
/*!40000 ALTER TABLE `good_size` DISABLE KEYS */;
INSERT INTO `good_size` VALUES ('00000001',39,4099),('00000001',40,4199),('00000001',41,4299),('00000001',42,4599),('00000001',43,4599),('00000002',38,2999),('00000002',39,3199),('00000002',40,3299),('00000002',41,3499),('00000002',42,3499),('00000002',43,3599);
/*!40000 ALTER TABLE `good_size` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `goods`
--

DROP TABLE IF EXISTS `goods`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `goods` (
  `good_article` varchar(8) NOT NULL,
  `brand_id` int NOT NULL,
  `model_id` int NOT NULL,
  `category_id` int NOT NULL,
  PRIMARY KEY (`good_article`,`model_id`,`category_id`,`brand_id`),
  UNIQUE KEY `good_article_UNIQUE` (`good_article`),
  KEY `fk_brand_good_idx` (`brand_id`),
  KEY `fk_category_good_idx` (`category_id`),
  KEY `fk_model_idx` (`model_id`),
  CONSTRAINT `fk_brand_good` FOREIGN KEY (`brand_id`) REFERENCES `brands` (`brand_id`),
  CONSTRAINT `fk_category_good` FOREIGN KEY (`category_id`) REFERENCES `categories` (`category_id`),
  CONSTRAINT `fk_model` FOREIGN KEY (`model_id`) REFERENCES `models` (`model_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `goods`
--

LOCK TABLES `goods` WRITE;
/*!40000 ALTER TABLE `goods` DISABLE KEYS */;
INSERT INTO `goods` VALUES ('00000001',1,1,1),('00000002',1,2,1),('00000003',1,3,1);
/*!40000 ALTER TABLE `goods` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `images`
--

DROP TABLE IF EXISTS `images`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `images` (
  `good_article` varchar(8) NOT NULL,
  `patrh` varchar(100) NOT NULL,
  `main` tinyint DEFAULT NULL,
  PRIMARY KEY (`good_article`,`patrh`),
  UNIQUE KEY `good_id_UNIQUE` (`good_article`),
  CONSTRAINT `fk_good_image` FOREIGN KEY (`good_article`) REFERENCES `goods` (`good_article`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `images`
--

LOCK TABLES `images` WRITE;
/*!40000 ALTER TABLE `images` DISABLE KEYS */;
/*!40000 ALTER TABLE `images` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `models`
--

DROP TABLE IF EXISTS `models`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `models` (
  `model_id` int NOT NULL AUTO_INCREMENT,
  `model_name` varchar(70) DEFAULT NULL,
  PRIMARY KEY (`model_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `models`
--

LOCK TABLES `models` WRITE;
/*!40000 ALTER TABLE `models` DISABLE KEYS */;
INSERT INTO `models` VALUES (1,'Air Force Ultra 1'),(2,'Air Force Flynkit'),(3,'Court Zoom Cage 2');
/*!40000 ALTER TABLE `models` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transactions`
--

DROP TABLE IF EXISTS `transactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transactions` (
  `transaction_id` int NOT NULL AUTO_INCREMENT,
  `user_email` varchar(120) NOT NULL,
  `user_name` varchar(70) DEFAULT NULL,
  `user_surname` varchar(70) DEFAULT NULL,
  `company_name` varchar(70) DEFAULT NULL,
  `user_country` varchar(70) DEFAULT NULL,
  `user_street` varchar(70) DEFAULT NULL,
  `user_num_house` varchar(5) DEFAULT NULL,
  `user_city` varchar(70) DEFAULT NULL,
  `user_state` varchar(70) DEFAULT NULL,
  `user_index` varchar(6) DEFAULT NULL,
  `transaction_total_amount` float DEFAULT NULL,
  `transaction_date` datetime DEFAULT NULL,
  `transactions_status` enum('Не оплачен','В обработке','Оплачено') DEFAULT NULL,
  PRIMARY KEY (`transaction_id`,`user_email`),
  UNIQUE KEY `transaction_id_UNIQUE` (`transaction_id`),
  KEY `fk_user_transaction_idx` (`user_email`),
  CONSTRAINT `fk_user_transaction` FOREIGN KEY (`user_email`) REFERENCES `users` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transactions`
--

LOCK TABLES `transactions` WRITE;
/*!40000 ALTER TABLE `transactions` DISABLE KEYS */;
/*!40000 ALTER TABLE `transactions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transactions_details`
--

DROP TABLE IF EXISTS `transactions_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transactions_details` (
  `transaction_id` int NOT NULL,
  `good_article` varchar(8) NOT NULL,
  `count_good` int DEFAULT NULL,
  `good_amount` float DEFAULT NULL,
  PRIMARY KEY (`transaction_id`,`good_article`),
  KEY `fk_trans_good_idx` (`good_article`),
  CONSTRAINT `fk_trans_good` FOREIGN KEY (`good_article`) REFERENCES `goods` (`good_article`),
  CONSTRAINT `fk_trans_trans` FOREIGN KEY (`transaction_id`) REFERENCES `transactions` (`transaction_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transactions_details`
--

LOCK TABLES `transactions_details` WRITE;
/*!40000 ALTER TABLE `transactions_details` DISABLE KEYS */;
/*!40000 ALTER TABLE `transactions_details` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `email` varchar(120) NOT NULL,
  `password_hash` varchar(255) DEFAULT NULL,
  `nickname` varchar(70) DEFAULT NULL,
  `phone` varchar(18) DEFAULT NULL,
  PRIMARY KEY (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES ('erik.fayzullin.2007@mail.ru','hash228','apfsnormtema','+7 (987) 494-06-37'),('reginasafina0227@gmail.com','hash228','reggsf','+7 (961) 360-51-11');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-09-08  0:17:54
