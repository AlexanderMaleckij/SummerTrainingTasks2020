IF EXISTS (SELECT 1 FROM information_schema.tables where table_name = 'Exams')
	DROP TABLE Exams;

IF EXISTS (SELECT 1 FROM information_schema.tables where table_name = 'Credits')
	DROP TABLE Credits;

IF EXISTS (SELECT 1 FROM information_schema.tables where table_name = 'KnowledgeControls')
	DROP TABLE KnowledgeControls;

IF EXISTS (SELECT 1 FROM information_schema.tables where table_name = 'Teachers')
	DROP TABLE Teachers;

IF EXISTS (SELECT 1 FROM information_schema.tables where table_name = 'Subjects')
	DROP TABLE Subjects;

IF EXISTS (SELECT 1 FROM information_schema.tables where table_name = 'Students')
	DROP TABLE Students;

IF EXISTS (SELECT 1 FROM information_schema.tables where table_name = 'StudentGroups')
	DROP TABLE StudentGroups;

IF EXISTS (SELECT 1 FROM information_schema.tables where table_name = 'Specialties')
	DROP TABLE Specialties;

CREATE TABLE Specialties
	(Id INT IDENTITY(1,1) PRIMARY KEY,
	SpecialtyName NVARCHAR(MAX) NOT NULL,
	Description NVARCHAR(MAX));

CREATE TABLE StudentGroups
	(Id INT IDENTITY(1,1) PRIMARY KEY,
	SpecialtyId INT NOT NULL,
	GroupName NVARCHAR(MAX) NOT NULL,
	SeptemberYear INT NOT NULL,
	FOREIGN KEY (SpecialtyId) REFERENCES Specialties(Id) ON DELETE CASCADE);

CREATE TABLE Students
	(Id       INT IDENTITY(1,1) PRIMARY KEY,
	StudentGroupId INT   NOT NULL,
	FullName NVARCHAR(MAX) NOT NULL,
	Gender   NVARCHAR(1)  NOT NULL,
	DateOfBirth DATETIME  NOT NULL,
	FOREIGN KEY (StudentGroupId) REFERENCES StudentGroups(Id) ON DELETE CASCADE);

CREATE TABLE Teachers
	(Id INT IDENTITY(1,1) PRIMARY KEY,
	FullName NVARCHAR(MAX) NOT NULL);

CREATE TABLE Subjects
	(Id INT IDENTITY(1,1) PRIMARY KEY,
	SubjectName NVARCHAR(MAX) NOT NULL);

CREATE TABLE KnowledgeControls
	(Id    INT IDENTITY(1,1) PRIMARY KEY,
	TeacherId INT NOT NULL,
	StudentGroupId  INT,
	SubjectId INT NOT NULL,
	Semester  TINYINT CHECK (Semester > 0 AND Semester < 3),
	PassDate    DATETIME     NOT NULL,
	FOREIGN KEY (TeacherId) REFERENCES Teachers(Id) ON DELETE NO ACTION,
	FOREIGN KEY (StudentGroupId) REFERENCES StudentGroups(Id) ON DELETE CASCADE,
	FOREIGN KEY (SubjectId) REFERENCES Subjects(Id) ON DELETE NO ACTION);

CREATE TABLE Credits
	(Id INT IDENTITY(1,1) PRIMARY KEY,
	KnowledgeControlId INT,
	StudentId INT,
	IsPassed  BIT,
	FOREIGN KEY (StudentId) REFERENCES Students(Id),
	FOREIGN KEY (KnowledgeControlId) REFERENCES KnowledgeControls(Id)  ON DELETE CASCADE);

CREATE TABLE Exams
	(Id INT IDENTITY(1,1) PRIMARY KEY,
	KnowledgeControlId INT,
	StudentId INT,
	Mark TINYINT NOT NULL CHECK (Mark >= 0 AND Mark <= 10),
	FOREIGN KEY (StudentId) REFERENCES Students(Id),
	FOREIGN KEY (KnowledgeControlId) REFERENCES KnowledgeControls(Id)  ON DELETE CASCADE);

INSERT INTO Specialties (SpecialtyName, Description) VALUES
	('Computer science and programming technologies', 	'Information technology is a set of methods and techniques for processing information in all types of human activity using communications, printing, computer technology and software.

Engineer-system programmer - qualification of a specialist with a higher education in the field of information technology software development.'),	-- 1
	('Information systems and technologies', 'Information systems are a field of science and technology, which includes a set of means, methods and methods of human activity aimed at creating and using systems for collecting, transferring, processing, storing and accumulating information.');	-- 2

INSERT INTO StudentGroups (GroupName, SpecialtyId, SeptemberYear) VALUES 
	('IP-11', 1, 2019), -- 1
	('IS-11', 2, 2019); -- 2

INSERT INTO Students (StudentGroupId, FullName, Gender, DateOfBirth) VALUES
	(1, 'Patrick Black',  'm', CONVERT(VARCHAR, '02/03/2000', 103)),	-- 1
	(1, 'Charles Backer', 'm', CONVERT(VARCHAR, '01/07/2000', 103)),	-- 2
	(1, 'Frank Thomas',   'm', CONVERT(VARCHAR, '03/05/2001', 103)),	-- 3
	(2, 'Alisha Edwards', 'f', CONVERT(VARCHAR, '06/05/2000', 103)),	-- 4
	(2, 'Leticia Ayrton', 'f', CONVERT(VARCHAR, '03/05/2001', 103)),	-- 5
	(2, 'Megan Gilson',   'f', CONVERT(VARCHAR, '11/11/2000', 103));	-- 6

INSERT INTO Teachers (FullName) VALUES
	('Boretskaya Victoria Kazimirovna'),	-- 1
	('Chudilovskaya Svetlana Alexandrovna'),	-- 2
	('Kravchenko Olga Alekseevna'),	-- 3
	('Avakyan Elena Zinovievna'),	-- 4
	('Shibeko Victor Nikolaevich'),	-- 5
	('Zadorozhnyuk Maria Viktorovna'),	-- 6
	('Torba Tatiana Fedorovna');	-- 7

INSERT INTO Subjects (SubjectName) VALUES
	('Philosophy'),	-- 1
	('OAIP'),	-- 2
	('English'),	-- 3
	('Math analysis'),	-- 4
	('Programming'),	-- 5
	('Discrete math'),	-- 6
	('Physical training');	-- 7

--data for two semesters, 1 exam and 1 credit per semester for each group
INSERT INTO KnowledgeControls (Semester, SubjectId, TeacherId, PassDate, StudentGroupId) VALUES
	(1, 1, 1, CONVERT(VARCHAR, '12/24/2018', 103), 1),	-- 1
	(1, 3, 2, CONVERT(VARCHAR, '12/26/2018', 103), 2),	-- 2
	(1, 2, 3, CONVERT(VARCHAR, '01/19/2019', 103), 1),	-- 3
	(1, 4, 4, CONVERT(VARCHAR, '01/24/2019', 103), 2),	-- 4
	(2, 5, 5, CONVERT(VARCHAR, '12/26/2018', 103), 1),	-- 5
	(2, 2, 3, CONVERT(VARCHAR, '01/24/2019', 103), 2),	-- 6
	(2, 6, 6, CONVERT(VARCHAR, '12/24/2018', 103), 1),	-- 7
	(2, 7, 7, CONVERT(VARCHAR, '01/19/2019', 103), 2);	-- 8

INSERT INTO Credits (KnowledgeControlId, StudentId,  IsPassed) VALUES
  -- IP11 Credits --
	-- 1st term --
	(1, 1, 1),			-- Patrick Black
	(1, 2, 1),			-- Charles Backer
	(1, 3, 0),		-- Frank Thomas
	-- 2nd term --
	(7, 1, 1),			-- Patrick Black
	(7, 2, 1),			-- Charles Backer
	(7, 3, 1),			-- Frank Thomas
  -- IS11 Credits --
	-- 1st term --
	(2, 4, 1),			-- Alisha Edwards
	(2, 5, 0),		-- Leticia Ayrton
	(2, 6, 0),		-- Megan Gilson
	-- 2nd term --
	(8, 4, 1),			-- Alisha Edwards
	(8, 5, 0),		-- Leticia Ayrton
	(8, 6, 1);			-- Megan Gilson

INSERT INTO Exams (KnowledgeControlId, StudentId,  Mark) VALUES
  -- IP11 Exams --
	-- 1st term --
	(3, 1, 8),			-- Patrick Black
	(3, 2, 6),			-- Charles Backer
	(3, 3, 3),			-- Frank Thomas
	-- 2nd term --
	(5, 1, 7),			-- Patrick Black
	(5, 2, 4),			-- Charles Backer
	(5, 3, 8),			-- Frank Thomas
  -- IS11 Exams --
	-- 1st term --
	(4, 4, 9),			-- Alisha Edwards
	(4, 5, 2),			-- Leticia Ayrton
	(4, 6, 8),			-- Megan Gilson
	-- 2nd term --
	(6, 4, 8),			-- Alisha Edwards
	(6, 5, 3),			-- Leticia Ayrton
	(6, 6, 2);			-- Megan Gilson