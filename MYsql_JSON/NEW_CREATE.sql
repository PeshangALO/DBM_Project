-- MySQL Script generated by MySQL Workbench
-- Tue Dec 14 17:23:58 2021
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema ADB1
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema ADB1
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `ADB1` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
-- -----------------------------------------------------
-- Schema UiA_DB
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema UiA_DB
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `UiA_DB` ;
-- -----------------------------------------------------
-- Schema radius
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema radius
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `radius` ;
USE `ADB1` ;

-- -----------------------------------------------------
-- Table `ADB1`.`ApplicationUser_dim`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ADB1`.`ApplicationUser_dim` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `Email` VARCHAR(64) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `GroupMembership` INT NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
AUTO_INCREMENT = 1025
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ADB1`.`DeviceOwnership_dim`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ADB1`.`DeviceOwnership_dim` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `ActiveUntil` DATETIME NOT NULL,
  `DateRegistered` DATETIME NOT NULL,
  `State` INT NOT NULL,
  `DeviceName` VARCHAR(64) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL DEFAULT '',
  `OwnerEmail` VARCHAR(64) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL DEFAULT '',
  `MAC` VARCHAR(64) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL DEFAULT '',
  `Vlan` VARCHAR(64) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
AUTO_INCREMENT = 8192
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ADB1`.`Fact_table`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ADB1`.`Fact_table` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `radreply_dim` INT UNSIGNED NOT NULL,
  `radaact_dim` BIGINT NOT NULL,
  `ApplicationUser_dim` VARCHAR(253) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `DeviceOwnership_dim` INT UNSIGNED NOT NULL,
  `Vlan_dim` VARCHAR(64) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `username` (`radreply_dim` ASC) VISIBLE,
  INDEX `radacct_fk_idx` (`radaact_dim` ASC) VISIBLE,
  INDEX `applicationuser_fk_idx` (`ApplicationUser_dim` ASC) VISIBLE,
  INDEX `deviceownership_fk_idx` (`DeviceOwnership_dim` ASC) VISIBLE,
  INDEX `vlan_fk` (`Vlan_dim` ASC) VISIBLE,
  CONSTRAINT `applicationuser_fk`
    FOREIGN KEY (`ApplicationUser_dim`)
    REFERENCES `UiA_DB`.`ApplicationUser` (`Id`),
  CONSTRAINT `deviceownership_fk`
    FOREIGN KEY (`DeviceOwnership_dim`)
    REFERENCES `UiA_DB`.`DeviceOwnership` (`Id`),
  CONSTRAINT `radacct_fk`
    FOREIGN KEY (`radaact_dim`)
    REFERENCES `radius`.`radacct` (`radacctid`),
  CONSTRAINT `radreply_fk`
    FOREIGN KEY (`radreply_dim`)
    REFERENCES `radius`.`radreply` (`id`),
  CONSTRAINT `vlan_fk`
    FOREIGN KEY (`Vlan_dim`)
    REFERENCES `UiA_DB`.`Vlan` (`VlanAlias`))
ENGINE = InnoDB
AUTO_INCREMENT = 65536
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ADB1`.`radacct_dim`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ADB1`.`radacct_dim` (
  `radacctid` BIGINT NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(64) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL DEFAULT '',
  `acctstarttime` DATETIME NULL DEFAULT NULL,
  `acctstoptime` DATETIME NULL DEFAULT NULL,
  `acctinputoctets` BIGINT NULL DEFAULT NULL,
  PRIMARY KEY (`radacctid`),
  INDEX `acctstarttime` (`acctstarttime` ASC) VISIBLE,
  INDEX `acctstoptime` (`acctstoptime` ASC) VISIBLE,
  INDEX `username` (`username` ASC) VISIBLE,
  INDEX `bulk_close` (`acctstoptime` ASC, `acctstarttime` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 196607
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ADB1`.`radreply_dim`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ADB1`.`radreply_dim` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(64) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL DEFAULT '',
  `value` VARCHAR(253) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL DEFAULT '',
  PRIMARY KEY (`id`),
  INDEX `username` (`username` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 8199
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

USE `UiA_DB` ;
USE `radius` ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
