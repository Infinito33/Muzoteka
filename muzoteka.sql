CREATE DATABASE  IF NOT EXISTS `muzoteka` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `muzoteka`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win64 (x86_64)
--
-- Host: localhost    Database: muzoteka
-- ------------------------------------------------------
-- Server version	5.6.23-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `administrator`
--

DROP TABLE IF EXISTS `administrator`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `administrator` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `email` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `username_UNIQUE` (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `administrator`
--

LOCK TABLES `administrator` WRITE;
/*!40000 ALTER TABLE `administrator` DISABLE KEYS */;
/*!40000 ALTER TABLE `administrator` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `album`
--

DROP TABLE IF EXISTS `album`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `album` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nazwa` varchar(45) NOT NULL,
  `data_wydania` date NOT NULL,
  `dddb` date NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `album`
--

LOCK TABLES `album` WRITE;
/*!40000 ALTER TABLE `album` DISABLE KEYS */;
INSERT INTO `album` VALUES (1,'Let\'s Talk About Love','1985-10-14','2015-11-12'),(2,'Ready for Romance','1986-05-26','2015-11-12'),(3,'Waking Up the Neighbours','1991-09-24','2015-11-12'),(4,'18 til I Die','1996-04-04','2015-11-12'),(5,'Reckless','1984-11-05','2015-11-12'),(9,'Shatter Me','2015-01-13','2015-12-25'),(15,'komponując siebie','2013-06-21','2016-01-03'),(16,'dsfsdf','2016-01-13','2016-01-10'),(17,'dgdfgdf','2016-01-14','2016-01-10');
/*!40000 ALTER TABLE `album` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `album_has_wykonawca`
--

DROP TABLE IF EXISTS `album_has_wykonawca`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `album_has_wykonawca` (
  `album_id` int(11) NOT NULL,
  `wykonawca_id` int(11) NOT NULL,
  PRIMARY KEY (`album_id`,`wykonawca_id`),
  KEY `fk_album_has_wykonawca_wykonawca1_idx` (`wykonawca_id`),
  KEY `fk_album_has_wykonawca_album1_idx` (`album_id`),
  CONSTRAINT `fk_album_has_wykonawca_album1` FOREIGN KEY (`album_id`) REFERENCES `album` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_album_has_wykonawca_wykonawca1` FOREIGN KEY (`wykonawca_id`) REFERENCES `wykonawca` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `album_has_wykonawca`
--

LOCK TABLES `album_has_wykonawca` WRITE;
/*!40000 ALTER TABLE `album_has_wykonawca` DISABLE KEYS */;
INSERT INTO `album_has_wykonawca` VALUES (1,1),(2,1),(17,1),(3,2),(4,2),(5,2),(16,6),(15,7);
/*!40000 ALTER TABLE `album_has_wykonawca` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ocena_utwor`
--

DROP TABLE IF EXISTS `ocena_utwor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ocena_utwor` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ocena` int(11) NOT NULL,
  `idutwor` int(11) NOT NULL,
  `iduzytkownik` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `idutwor_INDEX` (`idutwor`),
  KEY `iduzytkownik_INDEX` (`iduzytkownik`),
  CONSTRAINT `ocena_utwor` FOREIGN KEY (`idutwor`) REFERENCES `utwor` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `ocena_uzytkownik` FOREIGN KEY (`iduzytkownik`) REFERENCES `uzytkownik` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ocena_utwor`
--

LOCK TABLES `ocena_utwor` WRITE;
/*!40000 ALTER TABLE `ocena_utwor` DISABLE KEYS */;
INSERT INTO `ocena_utwor` VALUES (1,5,1,3);
/*!40000 ALTER TABLE `ocena_utwor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `playlista`
--

DROP TABLE IF EXISTS `playlista`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `playlista` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nazwa` varchar(45) NOT NULL,
  `data_utworzenia` date NOT NULL,
  `opis` varchar(255) DEFAULT NULL,
  `iduzytkownik` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `iduzytkownik_INDEX` (`iduzytkownik`),
  CONSTRAINT `uzytkownikPlaylista` FOREIGN KEY (`iduzytkownik`) REFERENCES `uzytkownik` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `playlista`
--

LOCK TABLES `playlista` WRITE;
/*!40000 ALTER TABLE `playlista` DISABLE KEYS */;
INSERT INTO `playlista` VALUES (3,'TestPlaylista','2015-12-29','OPIS',4),(5,'playlista admina','2016-01-02','fajna super tajna',3),(6,'hip hop','2016-01-02','<3',3),(7,'aaaaa','2016-01-10','1',4),(8,'aaa','2016-01-10','aaa',7),(9,'bbb','2016-01-10','bbb',7),(10,'ccc','2016-01-10','ccc',7);
/*!40000 ALTER TABLE `playlista` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `utwor`
--

DROP TABLE IF EXISTS `utwor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `utwor` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nazwa` varchar(45) NOT NULL,
  `gatunek` varchar(45) NOT NULL,
  `dlugosc` int(11) DEFAULT NULL,
  `link` varchar(200) NOT NULL,
  `dddb` date NOT NULL,
  `idalbum` int(11) NOT NULL,
  `idwyk` int(11) NOT NULL,
  `idplaylista` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `utwor_album` (`idalbum`),
  KEY `utwor_wyk` (`idwyk`),
  KEY `utwor_playlista` (`idplaylista`),
  CONSTRAINT `utw_alb` FOREIGN KEY (`idalbum`) REFERENCES `album` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `utw_playlista` FOREIGN KEY (`idplaylista`) REFERENCES `playlista` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `utw_wyk` FOREIGN KEY (`idwyk`) REFERENCES `wykonawca` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `utwor`
--

LOCK TABLES `utwor` WRITE;
/*!40000 ALTER TABLE `utwor` DISABLE KEYS */;
INSERT INTO `utwor` VALUES (1,'Heavenn','Rock',230,'','2015-11-12',5,2,NULL),(2,'Somebody','Rock',NULL,'link YT2','2015-11-12',5,2,NULL),(3,'Summer of \'69','Rock',NULL,'link YT3','2015-11-12',5,2,NULL),(4,'Brother Louie','Synthpop',NULL,'link YT4','2015-11-12',2,1,NULL),(5,'Lady Lai','Synthpop',NULL,'link YT5','2015-11-12',2,1,NULL),(10,'flagi serc serc','pop',220,'https://www.youtube.com/embed/jOOq8uKEXmQ?list=PLX4GS4BM317YuZaDMz_XizxEcoAg48Zse','2016-01-03',15,7,NULL),(11,'Flirt','pop',221,'https://www.youtube.com/embed/ORikRIu7s1o?list=PLX4GS4BM317YuZaDMz_XizxEcoAg48Zse','2016-01-03',15,7,NULL),(12,'Hotel chwili','pop',218,'https://www.youtube.com/embed/DQyo_RZMktg?list=PLX4GS4BM317YuZaDMz_XizxEcoAg48Zse','2016-01-03',15,7,NULL),(13,'ksiezniczka','pop',211,'https://www.youtube.com/embed/r_ADtyQqTSc?list=PLX4GS4BM317YuZaDMz_XizxEcoAg48Zse','2016-01-03',15,7,NULL),(14,'When Youre Go','Rock & Roll',0,'/embed/','2016-01-10',5,2,NULL),(15,'xxxxx','sexsex',0,'sex/embed/sex','2016-01-10',5,6,NULL),(16,'sexsex','xesxes',0,'sex/embed/xes','2016-01-10',5,6,NULL),(17,'adasdasdas','sdfsdfsdfsdf',3433,'asdasdas/embed/','2016-01-10',5,3,NULL);
/*!40000 ALTER TABLE `utwor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `utwor_w_playlista`
--

DROP TABLE IF EXISTS `utwor_w_playlista`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `utwor_w_playlista` (
  `utwor_id` int(11) NOT NULL,
  `playlista_id` int(11) NOT NULL,
  PRIMARY KEY (`utwor_id`,`playlista_id`),
  KEY `fk_utwor_id` (`utwor_id`),
  KEY `utwor_playlista_idx` (`playlista_id`),
  CONSTRAINT `utwor_playlista` FOREIGN KEY (`playlista_id`) REFERENCES `playlista` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `utwor_w_playlista`
--

LOCK TABLES `utwor_w_playlista` WRITE;
/*!40000 ALTER TABLE `utwor_w_playlista` DISABLE KEYS */;
INSERT INTO `utwor_w_playlista` VALUES (1,5),(2,3),(2,5),(3,3),(8,6);
/*!40000 ALTER TABLE `utwor_w_playlista` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uzytkownik`
--

DROP TABLE IF EXISTS `uzytkownik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `uzytkownik` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `uprawnienia` varchar(45) NOT NULL,
  `email` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `uzytkownik`
--

LOCK TABLES `uzytkownik` WRITE;
/*!40000 ALTER TABLE `uzytkownik` DISABLE KEYS */;
INSERT INTO `uzytkownik` VALUES (3,'admin1','1pass','full',''),(4,'tester','tester','standard',''),(7,'admin','admin','full','admin@admin.com');
/*!40000 ALTER TABLE `uzytkownik` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `wykonawca`
--

DROP TABLE IF EXISTS `wykonawca`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `wykonawca` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `imie` varchar(45) DEFAULT NULL,
  `nazwisko` varchar(45) DEFAULT NULL,
  `pseudonim` varchar(45) NOT NULL,
  `data_ur` date DEFAULT NULL,
  `dddb` date NOT NULL,
  `opis` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `wykonawca`
--

LOCK TABLES `wykonawca` WRITE;
/*!40000 ALTER TABLE `wykonawca` DISABLE KEYS */;
INSERT INTO `wykonawca` VALUES (1,NULL,NULL,'Modern Talking','2015-01-01','2015-11-12','Opis...'),(2,'Bryan','Adams','Bryan Adams','1959-11-05','2015-11-12','Bryan Adams opis...'),(3,'Lindsey','Stirling','Lindsey Stirling','1985-11-05','2015-12-25','Lindsey Stirling opis...'),(6,'','','Popek','2015-12-31','2016-01-02','hue'),(7,'Sylwia','Grzeszczak','Sylwia Grzeszczak','2015-01-01','2016-01-03','');
/*!40000 ALTER TABLE `wykonawca` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'muzoteka'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-12-04 16:07:04
