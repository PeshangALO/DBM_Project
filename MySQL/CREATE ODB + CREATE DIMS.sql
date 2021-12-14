-- MySQL Script generated by MySQL Workbench
-- Mon Nov 29 12:59:50 2021
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';


-- -- -----------------------------------------------------
-- CREATE SCHEMA IF NOT EXISTS `ADB1` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;

USE `ADB1` ;
-- -----------------------------------------------------
-- Schema ADB1
-- -----------------------------------------------------

CREATE TABLE IF NOT EXISTS `ADB1`.`Fact_table` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `radreply_dim` INT UNSIGNED NOT NULL,
  `radaact_dim` BIGINT(255) NOT NULL,
  `ApplicationUser_dim` VARCHAR(253) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `DeviceOwnership_dim` INT UNSIGNED NOT NULL,
  `Vlan_dim` VARCHAR(64) CHARACTER SET 'utf8mb4' NOT NULL,
  -- `Time_dim` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `username` (`radreply_dim` ASC) VISIBLE,
  INDEX `radacct_fk_idx` (`radaact_dim` ASC) VISIBLE,
  INDEX `applicationuser_fk_idx` (`ApplicationUser_dim` ASC) VISIBLE,
  INDEX `deviceownership_fk_idx` (`DeviceOwnership_dim` ASC) VISIBLE,
  CONSTRAINT `radreply_fk`
    FOREIGN KEY (`radreply_dim`)
    REFERENCES `radius`.`radreply` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `radacct_fk`
    FOREIGN KEY (`radaact_dim`)
    REFERENCES `radius`.`radacct` (`radacctid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `applicationuser_fk`
    FOREIGN KEY (`ApplicationUser_dim`)
    REFERENCES `UiA_DB`.`ApplicationUser` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `deviceownership_fk`
    FOREIGN KEY (`DeviceOwnership_dim`)
    REFERENCES `UiA_DB`.`DeviceOwnership` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `vlan_fk`
    FOREIGN KEY (`vlan_dim`)
    REFERENCES `UiA_DB`.`Vlan` (`VlanAlias`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

USE `ADB1` ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;


-- -----------------------------------------------------
-- Table `ADB1`.`ApplicationUser_dim`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ADB1`.`ApplicationUser_dim` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `Email` VARCHAR(64) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `GroupMembership` INT NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;



USE `ADB1` ;
DROP TABLE DeviceOwnership_dim;
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
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `ADB1`.`Vlan`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ADB1`.`Vlan_dim` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `VlanName` VARCHAR(64) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
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
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;




-- -----------------------------------------------------
-- Table `ADB1`.`radreply`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ADB1`.`radreply_dim` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(64) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL DEFAULT '',
  `value` VARCHAR(253) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL DEFAULT '',
  PRIMARY KEY (`id`),
  INDEX `username` (`username` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


