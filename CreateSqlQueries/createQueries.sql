CREATE SCHEMA IF NOT EXISTS PARKING_LOT;
USE PARKING_LOT;

CREATE TABLE PARKING_LOT
    (	Lot_name	VARCHAR(10)	NOT NULL,
		Camera_id	CHAR(5)		NOT NULL,
		Street		VARCHAR(20)	NOT NULL,
		City		VARCHAR(20)	NOT NULL,
		Zip_code	CHAR(5)		NOT NULL,
    	PRIMARY KEY (Camera_id));

CREATE TABLE PARKING_SPACE
    (	Space_point_id		INT(10) 	NOT NULL AUTO_INCREMENT,
    	x_space				DOUBLE DEFAULT 0,
		y_space				DOUBLE DEFAULT 0,
		Space_width			DECIMAL(10,2),
    	Space_height		DECIMAL(10.2),
		PRIMARY KEY (Space_point_id)
	) ENGINE=INNODB;

CREATE TABLE VEHICLE
    (	Space_point_id		INT(10) NOT NULL AUTO_INCREMENT,
    	x_vehicle			DOUBLE DEFAULT 0,
		y_vehicle			DOUBLE DEFAULT 0,
    	Vehicle_width		DECIMAL(10,2),
    	Vehicle_height		DECIMAL(10.2),
		Parked_datetime		DATETIME,
		Vacated_datetime	DATETIME,
		Vehicle_type		VARCHAR(20) NOT NULL,
		PRIMARY KEY (Space_point_id),
		FOREIGN KEY (Space_point_id)	REFERENCES PARKING_SPACE(Space_point_id)
	) ENGINE=INNODB;