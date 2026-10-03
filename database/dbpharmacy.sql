/*
SQLyog Ultimate - MySQL GUI v8.22 
MySQL - 5.5.27 : Database - dbpharmacy
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`dbpharmacy` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `dbpharmacy`;

/*Table structure for table `category` */

DROP TABLE IF EXISTS `category`;

CREATE TABLE `category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `categoryname` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `categoryname` (`categoryname`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Data for the table `category` */

insert  into `category`(`id`,`categoryname`) values (2,'Mobility Equipment'),(1,'Surgical Instruments'),(3,'Syringe');

/*Table structure for table `customer` */

DROP TABLE IF EXISTS `customer`;

CREATE TABLE `customer` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `contact` varchar(50) NOT NULL,
  `address` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

/*Data for the table `customer` */

insert  into `customer`(`id`,`name`,`contact`,`address`) values (1,'John Carter','1234','34 Elm St., Chicago, IL'),(2,'Sarah Thompson','9087','123 Oak Rd., Dallas, TX'),(3,'David Rodriguez','3210','456 Pine Ave, San Diego'),(4,'Emily David','6586','987 Maple St., Denver, CO');

/*Table structure for table `customer_delivery` */

DROP TABLE IF EXISTS `customer_delivery`;

CREATE TABLE `customer_delivery` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `customerid` int(11) NOT NULL,
  `orderid` int(11) NOT NULL,
  `deliveryid` int(11) NOT NULL,
  `status` enum('Pending','In Transit','Delivered','Cancelled') DEFAULT 'Pending',
  `dateassigned` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `customerid` (`customerid`),
  KEY `orderid` (`orderid`),
  KEY `deliveryid` (`deliveryid`),
  CONSTRAINT `customer_delivery_ibfk_1` FOREIGN KEY (`customerid`) REFERENCES `customer` (`id`) ON DELETE CASCADE,
  CONSTRAINT `customer_delivery_ibfk_2` FOREIGN KEY (`orderid`) REFERENCES `orders` (`id`) ON DELETE CASCADE,
  CONSTRAINT `customer_delivery_ibfk_3` FOREIGN KEY (`deliveryid`) REFERENCES `order_driver` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Data for the table `customer_delivery` */

insert  into `customer_delivery`(`id`,`customerid`,`orderid`,`deliveryid`,`status`,`dateassigned`) values (1,1,1,1,'Delivered','2025-03-01 06:27:48'),(2,2,6,2,'Delivered','2025-03-04 12:21:01'),(3,3,2,3,'Pending','2025-03-04 17:58:11');

/*Table structure for table `driver` */

DROP TABLE IF EXISTS `driver`;

CREATE TABLE `driver` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `license` varchar(50) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `license` (`license`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

/*Data for the table `driver` */

insert  into `driver`(`id`,`name`,`license`) values (1,'Michael Scott','D123456789'),(2,'Robert Harris','D763189045');

/*Table structure for table `order_driver` */

DROP TABLE IF EXISTS `order_driver`;

CREATE TABLE `order_driver` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `orderid` int(11) NOT NULL,
  `driverid` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `orderid` (`orderid`),
  KEY `driverid` (`driverid`),
  CONSTRAINT `order_driver_ibfk_1` FOREIGN KEY (`orderid`) REFERENCES `orders` (`id`) ON DELETE CASCADE,
  CONSTRAINT `order_driver_ibfk_2` FOREIGN KEY (`driverid`) REFERENCES `driver` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Data for the table `order_driver` */

insert  into `order_driver`(`id`,`orderid`,`driverid`) values (1,1,2),(2,6,1),(3,2,2);

/*Table structure for table `orderdetails` */

DROP TABLE IF EXISTS `orderdetails`;

CREATE TABLE `orderdetails` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `orderid` int(11) NOT NULL,
  `productid` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `price` decimal(10,2) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `orderid` (`orderid`),
  KEY `productid` (`productid`),
  CONSTRAINT `orderdetails_ibfk_1` FOREIGN KEY (`orderid`) REFERENCES `orders` (`id`) ON DELETE CASCADE,
  CONSTRAINT `orderdetails_ibfk_2` FOREIGN KEY (`productid`) REFERENCES `product` (`productid`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1;

/*Data for the table `orderdetails` */

insert  into `orderdetails`(`id`,`orderid`,`productid`,`quantity`,`price`) values (1,1,1,3,'100.00'),(2,1,3,1,'200.00'),(3,2,2,2,'65.00'),(4,2,4,1,'130.00'),(8,4,1,2,'100.00'),(9,5,5,5,'30.00'),(10,5,5,5,'30.00'),(11,6,6,3,'70.00'),(12,6,6,5,'70.00'),(13,6,3,5,'200.00');

/*Table structure for table `orders` */

DROP TABLE IF EXISTS `orders`;

CREATE TABLE `orders` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `customerid` int(11) NOT NULL,
  `orderdate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `customerid` (`customerid`),
  CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`customerid`) REFERENCES `customer` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

/*Data for the table `orders` */

insert  into `orders`(`id`,`customerid`,`orderdate`) values (1,1,'2025-03-01 06:14:35'),(2,3,'2025-03-01 06:16:22'),(4,2,'2025-03-01 08:16:24'),(5,1,'2025-03-01 08:21:14'),(6,2,'2025-03-04 12:20:04');

/*Table structure for table `product` */

DROP TABLE IF EXISTS `product`;

CREATE TABLE `product` (
  `productid` int(11) NOT NULL AUTO_INCREMENT,
  `productname` varchar(255) NOT NULL,
  `categoryid` int(11) DEFAULT NULL,
  `stock` int(11) DEFAULT '0',
  `price` decimal(10,2) NOT NULL,
  PRIMARY KEY (`productid`),
  KEY `categoryid` (`categoryid`),
  CONSTRAINT `product_ibfk_1` FOREIGN KEY (`categoryid`) REFERENCES `category` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

/*Data for the table `product` */

insert  into `product`(`productid`,`productname`,`categoryid`,`stock`,`price`) values (1,'Scalpel',1,97,'100.00'),(2,'Forceps',1,40,'65.00'),(3,'Wheelchair',2,84,'200.00'),(4,'Walker',2,60,'130.00'),(5,'Scissor',1,35,'30.00'),(6,'3cc Syringe',3,72,'70.00');

/*Table structure for table `stockin` */

DROP TABLE IF EXISTS `stockin`;

CREATE TABLE `stockin` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `supplierid` int(11) NOT NULL,
  `transactiondate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `supplierid` (`supplierid`),
  CONSTRAINT `stockin_ibfk_1` FOREIGN KEY (`supplierid`) REFERENCES `supplier` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

/*Data for the table `stockin` */

insert  into `stockin`(`id`,`supplierid`,`transactiondate`) values (1,1,'2025-02-28 22:40:57'),(2,1,'2025-02-28 22:41:12'),(3,2,'2025-02-28 22:41:28'),(4,2,'2025-02-28 22:41:45'),(5,1,'2025-03-01 08:16:54'),(6,1,'2025-03-04 12:17:36'),(7,1,'2025-03-04 12:18:40');

/*Table structure for table `stockin_details` */

DROP TABLE IF EXISTS `stockin_details`;

CREATE TABLE `stockin_details` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `stockinid` int(11) NOT NULL,
  `productid` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `stockinid` (`stockinid`),
  KEY `productid` (`productid`),
  CONSTRAINT `stockin_details_ibfk_1` FOREIGN KEY (`stockinid`) REFERENCES `stockin` (`id`) ON DELETE CASCADE,
  CONSTRAINT `stockin_details_ibfk_2` FOREIGN KEY (`productid`) REFERENCES `product` (`productid`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

/*Data for the table `stockin_details` */

insert  into `stockin_details`(`id`,`stockinid`,`productid`,`quantity`) values (1,1,2,40),(2,2,1,100),(3,3,4,60),(4,4,3,90),(5,5,5,40),(6,6,6,40),(7,7,6,40);

/*Table structure for table `stockout` */

DROP TABLE IF EXISTS `stockout`;

CREATE TABLE `stockout` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `transactiondate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `customer_delivery_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `stockout_ibfk_2` (`customer_delivery_id`),
  CONSTRAINT `stockout_ibfk_2` FOREIGN KEY (`customer_delivery_id`) REFERENCES `customer_delivery` (`id`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Data for the table `stockout` */

insert  into `stockout`(`id`,`transactiondate`,`customer_delivery_id`) values (1,'2025-03-01 06:32:05',1),(3,'2025-03-04 12:21:20',2);

/*Table structure for table `stockout_details` */

DROP TABLE IF EXISTS `stockout_details`;

CREATE TABLE `stockout_details` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `stockoutid` int(11) NOT NULL,
  `productid` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `stockoutid` (`stockoutid`),
  KEY `productid` (`productid`),
  CONSTRAINT `stockout_details_ibfk_1` FOREIGN KEY (`stockoutid`) REFERENCES `stockout` (`id`) ON DELETE CASCADE,
  CONSTRAINT `stockout_details_ibfk_2` FOREIGN KEY (`productid`) REFERENCES `product` (`productid`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

/*Data for the table `stockout_details` */

insert  into `stockout_details`(`id`,`stockoutid`,`productid`,`quantity`) values (1,1,1,3),(2,1,3,1),(4,3,6,3),(5,3,6,5),(6,3,3,5);

/*Table structure for table `supplier` */

DROP TABLE IF EXISTS `supplier`;

CREATE TABLE `supplier` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `contact` varchar(50) NOT NULL,
  `address` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

/*Data for the table `supplier` */

insert  into `supplier`(`id`,`name`,`contact`,`address`) values (1,'MedTech Solutions','5557654','456 Industrial Park'),(2,'Lifeline Medical Supply','5553333','789 Wellness Rd');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
