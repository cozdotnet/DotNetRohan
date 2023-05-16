CREATE TABLE credentials (
    [user_id] int IDENTITY(1,1)  NOT NULL,
    password varchar(50)  NOT NULL,
    role varchar(50)  NOT NULL,
    CONSTRAINT credentials_pk PRIMARY KEY  ([user_id])
);

CREATE TABLE Appointment(
	app_id int IDENTITY(1,1)  NOT NULL,
	[user_name] varchar(50) NOT NULL,
	created_date date NOT NULL,
	CONSTRAINT Appointent_pk PRIMARY KEY  ([app_id])
);


-- foreign keys
-- Reference: attendance_batch (table: attendance)
ALTER TABLE Appointment ADD CONSTRAINT Appointment_name
    FOREIGN KEY ([user_id])
    REFERENCES credentials ([user_id]);

