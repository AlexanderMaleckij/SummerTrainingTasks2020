IF EXISTS (SELECT 1 FROM information_schema.tables where table_name = 'Exam')
	DROP TABLE Exam;

IF EXISTS (SELECT 1 FROM information_schema.tables where table_name = 'Credit')
	DROP TABLE Credit;

IF EXISTS (SELECT 1 FROM information_schema.tables where table_name = 'KnowledgeControl')
	DROP TABLE KnowledgeControl;

IF EXISTS (SELECT 1 FROM information_schema.tables where table_name = 'Student')
	DROP TABLE Student;

IF EXISTS (SELECT 1 FROM information_schema.tables where table_name = 'StudentGroup')
	DROP TABLE StudentGroup;

CREATE TABLE StudentGroup
	(Id INT IDENTITY(1,1) PRIMARY KEY,
	GroupName TEXT NOT NULL,
	TransitionYear INT NOT NULL);	--year of transition to the new course (or enrollment in a group)

CREATE TABLE Student
	(Id       INT IDENTITY(1,1) PRIMARY KEY,
	StudentGroupId INT   NOT NULL,
	FullName TEXT NOT NULL,
	Gender   NVARCHAR(1)  NOT NULL,
	DateOfBirth DATETIME  NOT NULL,
	FOREIGN KEY (StudentGroupId) REFERENCES StudentGroup(Id) ON DELETE CASCADE);	--When deleting a group, delete students of this group

CREATE TABLE KnowledgeControl
	(Id    INT IDENTITY(1,1) PRIMARY KEY,
	Semester  TINYINT CHECK (Term > 0 AND Term < 3),
	SubjectName TEXT NOT NULL,
	PassDate    DATETIME     NOT NULL,
	StudentGroupId  INT,
	FOREIGN KEY (StudentGroupId) REFERENCES StudentGroup(Id) ON DELETE CASCADE); --Remove all knowledge controls when deleting a group

CREATE TABLE Credit
	(Id INT IDENTITY(1,1) PRIMARY KEY,
	KnowledgeControlId INT,
	StudentId INT,
	IsPassed  BIT,
	FOREIGN KEY (StudentId) REFERENCES Student(Id),
	FOREIGN KEY (KnowledgeControlId) REFERENCES KnowledgeControl(Id)  ON DELETE CASCADE);

CREATE TABLE Exam
	(Id INT IDENTITY(1,1) PRIMARY KEY,
	KnowledgeControlId INT,
	StudentId INT,
	Mark TINYINT NOT NULL CHECK (Mark >= 0 AND Mark <= 10),
	FOREIGN KEY (StudentId) REFERENCES Student(Id),
	FOREIGN KEY (KnowledgeControlId) REFERENCES KnowledgeControl(Id)  ON DELETE CASCADE);

INSERT INTO StudentGroup (GroupName) VALUES 
  ('IP-11', 2019), -- 1
  ('IS-11', 2019); -- 2

INSERT INTO Student (StudentGroupId, FullName, Gender, DateOfBirth) VALUES
  (1, 'Patrick Black',  'm', CONVERT(VARCHAR, '02/03/2000', 103)),	--1
  (1, 'Charles Backer', 'm', CONVERT(VARCHAR, '01/07/2000', 103)),	--2
  (1, 'Frank Thomas',   'm', CONVERT(VARCHAR, '03/05/2001', 103)),	--3
  (2, 'Alisha Edwards', 'f', CONVERT(VARCHAR, '06/05/2000', 103)),	--4
  (2, 'Leticia Ayrton', 'f', CONVERT(VARCHAR, '03/05/2001', 103)),	--5
  (2, 'Megan Gilson',   'f', CONVERT(VARCHAR, '11/11/2000', 103));	--6

--data for two semesters, 1 exam and 1 credit per semester for each group
INSERT INTO KnowledgeControl (Semester, SubjectName, PassDate, StudentGroupId) VALUES
  (1, 'Philosophy', CONVERT(VARCHAR, '12/24/2018', 103), 1),	-- 1
  (1, 'English',    CONVERT(VARCHAR, '12/26/2018', 103), 2),	-- 2
  (1, 'OAIP',          CONVERT(VARCHAR, '01/19/2019', 103), 1),	-- 3
  (1, 'Math analysis', CONVERT(VARCHAR, '01/24/2019', 103), 2),	-- 4
  (2, 'Programming', CONVERT(VARCHAR, '12/26/2018', 103), 1),	-- 5
  (2, 'OAIP',        CONVERT(VARCHAR, '01/24/2019', 103), 2),	-- 6
  (2, 'Discrete math',     CONVERT(VARCHAR, '12/24/2018', 103), 1),	-- 7
  (2, 'Physical training', CONVERT(VARCHAR, '01/19/2019', 103), 2);	-- 8

INSERT INTO Credit (KnowledgeControlId, StudentId,  IsPassed) VALUES
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

INSERT INTO Exam (KnowledgeControlId, StudentId,  Mark) VALUES
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