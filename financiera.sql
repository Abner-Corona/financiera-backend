
SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

Create database financiera;
use financiera;
-- ----------------------------
-- Table structure for cat_user_type
-- ----------------------------
DROP TABLE IF EXISTS `cat_user_type`;
CREATE TABLE `cat_user_type`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` text CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `permission` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL,
  `nameTranslation` text CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of cat_user_type
-- ----------------------------
INSERT INTO `cat_user_type` VALUES (1, 'Administrator', '[]', 'ADMIN');
INSERT INTO `cat_user_type` VALUES (2, 'Client', '[]', 'CLIENT');

-- ----------------------------
-- Table structure for tab_account
-- ----------------------------
DROP TABLE IF EXISTS `tab_account`;
CREATE TABLE `tab_account`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` text CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `amount` decimal(13, 4) NULL DEFAULT NULL,
  `idUser` int(255) UNSIGNED NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `user_account_fk`(`idUser`) USING BTREE,
  CONSTRAINT `user_account_fk` FOREIGN KEY (`idUser`) REFERENCES `tab_user` (`id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE = InnoDB AUTO_INCREMENT = 31 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tab_account
-- ----------------------------
INSERT INTO `tab_account` VALUES (29, 'casa', 10.0000, 2);
INSERT INTO `tab_account` VALUES (30, 'para mi perrito', 20.0000, 3);

-- ----------------------------
-- Table structure for tab_record
-- ----------------------------
DROP TABLE IF EXISTS `tab_record`;
CREATE TABLE `tab_record`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `IdAccount` int(255) UNSIGNED NULL DEFAULT NULL,
  `operation` enum('deposit','withdrawal') CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `amount` decimal(13, 4) NULL DEFAULT NULL,
  `date` datetime(0) NULL DEFAULT current_timestamp ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `account_record_fk`(`IdAccount`) USING BTREE,
  CONSTRAINT `account_record_fk` FOREIGN KEY (`IdAccount`) REFERENCES `tab_account` (`id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE = InnoDB AUTO_INCREMENT = 75 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tab_record
-- ----------------------------
INSERT INTO `tab_record` VALUES (71, 29, 'deposit', 100.0000, '2021-03-18 11:40:22');
INSERT INTO `tab_record` VALUES (72, 29, 'withdrawal', 90.0000, '2021-03-18 11:40:57');
INSERT INTO `tab_record` VALUES (74, 30, 'deposit', 20.0000, '2021-03-18 11:49:11');

-- ----------------------------
-- Table structure for tab_user
-- ----------------------------
DROP TABLE IF EXISTS `tab_user`;
CREATE TABLE `tab_user`  (
  `id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` text CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `idUserType` int(10) UNSIGNED NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `user_typet_fl`(`idUserType`) USING BTREE,
  CONSTRAINT `user_typet_fl` FOREIGN KEY (`idUserType`) REFERENCES `cat_user_type` (`id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tab_user
-- ----------------------------
INSERT INTO `tab_user` VALUES (1, 'Admin', 1);
INSERT INTO `tab_user` VALUES (2, 'Client 1', 2);
INSERT INTO `tab_user` VALUES (3, 'Client 2', 2);

-- ----------------------------
-- Procedure structure for addNewAccount
-- ----------------------------
DROP PROCEDURE IF EXISTS `addNewAccount`;
delimiter ;;
CREATE PROCEDURE `addNewAccount`(_name text, _amount DECIMAL, _idUser INT)
BEGIN
	START TRANSACTION;
	INSERT INTO `financiera`.`tab_account` ( `name`, `amount`, `idUser` )
	VALUES
		( _name, _amount, _idUser );
	INSERT INTO `financiera`.`tab_record` ( `IdAccount`, `operation`, `amount` )
	VALUES
		( LAST_INSERT_ID(), 'deposit', _amount );
	COMMIT;
  select true;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for depositAccount
-- ----------------------------
DROP PROCEDURE IF EXISTS `depositAccount`;
delimiter ;;
CREATE PROCEDURE `depositAccount`(_idAccount text, _amount DECIMAL)
BEGIN
DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;  Select false ;
    END;
	START TRANSACTION;
	UPDATE `financiera`.`tab_account` SET `amount` = (`amount` +_amount) WHERE `id` = _idAccount;
	
	INSERT INTO `financiera`.`tab_record` ( `IdAccount`, `operation`, `amount` )
	VALUES
		( _idAccount, 'deposit', _amount );
	COMMIT;Select true ;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for getAccount
-- ----------------------------
DROP PROCEDURE IF EXISTS `getAccount`;
delimiter ;;
CREATE PROCEDURE `getAccount`(_idUser Int)
BEGIN
select * from tab_account  where idUser=_idUser;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for getRecords
-- ----------------------------
DROP PROCEDURE IF EXISTS `getRecords`;
delimiter ;;
CREATE PROCEDURE `getRecords`()
BEGIN
SELECT
	tu.NAME as user,
	ta.NAME as account,
	tr.date,
	tr.amount,
	tr.operation 
FROM
	tab_record tr,
	 tab_user tu,
	tab_account ta 
WHERE
	ta.id LIKE tr.IdAccount 
	AND tu.id = ta.idUser;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for getUsers
-- ----------------------------
DROP PROCEDURE IF EXISTS `getUsers`;
delimiter ;;
CREATE PROCEDURE `getUsers`()
BEGIN
	select * from tab_user;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for withdrawalAccount
-- ----------------------------
DROP PROCEDURE IF EXISTS `withdrawalAccount`;
delimiter ;;
CREATE PROCEDURE `withdrawalAccount`(_idAccount int, _amount DECIMAL)
BEGIN
	START TRANSACTION;
	UPDATE `financiera`.`tab_account` 
	SET `amount` = ( `amount` - _amount ) 
	WHERE
		`id` = _idAccount;
	INSERT INTO `financiera`.`tab_record` ( `IdAccount`, `operation`, `amount` )
	VALUES
		( _idAccount, 'withdrawal', _amount );
	IF
		(( SELECT amount FROM `financiera`.`tab_account` WHERE
		`id` = _idAccount ) < 0 ) THEN
		SELECT FALSE;
		ROLLBACK;
		ELSE 
				COMMIT;
			SELECT TRUE;
			
		END IF;
	
END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
