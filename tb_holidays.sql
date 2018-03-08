/*
Navicat MySQL Data Transfer

Source Server         : 192.168.8.21
Source Server Version : 50713
Source Host           : 192.168.8.21:3306
Source Database       : test

Target Server Type    : MYSQL
Target Server Version : 50713
File Encoding         : 65001

Date: 2018-03-06 09:36:19
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `tb_holidays`
-- ----------------------------
DROP TABLE IF EXISTS `tb_holidays`;
CREATE TABLE `tb_holidays` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `StartDate` varchar(18) DEFAULT NULL,
  `EndDate` varchar(18) DEFAULT NULL,
  `Days` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_holidays
-- ----------------------------
