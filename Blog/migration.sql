CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Blogs` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Title` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `ContentText1` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ContentText2` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ContentText3` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ContentText4` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ImageUrl1` longtext CHARACTER SET utf8mb4 NULL,
    `ImageFileName1` longtext CHARACTER SET utf8mb4 NULL,
    `ImageUrl2` longtext CHARACTER SET utf8mb4 NULL,
    `ImageFileName2` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `UpdatedAt` datetime(6) NOT NULL,
    `IsPublished` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Blogs` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Users` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Username` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `PasswordHash` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Email` varchar(100) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `Users` (`Id`, `Email`, `PasswordHash`, `Username`)
VALUES (1, 'admin@proequipment.sa', '$2a$11$9Acqq2VdOYcl7s.T4JizHOPgw0qjhnSQdbyxf7G0uEfZ7zly0mZpS', 'admin');

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20251101122638_InitialCreateMySql', '7.0.14');

COMMIT;

START TRANSACTION;

ALTER TABLE `Blogs` ADD `ContentText5` longtext CHARACTER SET utf8mb4 NOT NULL;

UPDATE `Users` SET `PasswordHash` = '$2a$11$LLdkDB6eHoXK91a.vf2tlu6bctjoe.FEJfiwI3Bqu0H7Zl8CqnNei'
WHERE `Id` = 1;
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20251102115426_AddingContentText5', '7.0.14');

COMMIT;

