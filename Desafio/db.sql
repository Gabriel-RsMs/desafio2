CREATE DATABASE DESAFIO;

USE DESAFIO;

CREATE TABLE USERS(
    EMAIL VARCHAR(50) NOT NULL;
    PASSWD PASSWORD NOT NULL;
    ACCOUNTID INT IDENTITY(1,1);
    ACCOUNTTYPE INT DEFAULT 1;
    BALANCE INT NOT NULL;
)