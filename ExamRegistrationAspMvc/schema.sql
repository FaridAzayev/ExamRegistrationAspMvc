CREATE TABLE "SUBJECTS" (
    "CODE"                CHAR(3) PRIMARY KEY,
    "CLASS_NO"            NUMBER(2),
    "NAME"                VARCHAR(30),
    "TEACHER_FIRSTNAME"   VARCHAR(20),
    "TEACHER_LASTNAME"    VARCHAR(20)
);
/

CREATE TABLE "STUDENTS" (
    "NUM"          NUMBER(5) PRIMARY KEY,
    "FIRST_NAME"   VARCHAR(30),
    "LAST_NAME"    VARCHAR(30),
    "CLASS_NO"     NUMBER(2)
);
/

CREATE TABLE "EXAMS" (
    "SUBJECT_CODE"     CHAR(3)
        REFERENCES subjects ( code ),
    "STUDENT_NUMBER"   NUMBER(5)
        REFERENCES students ( num ),
    "EXAM_DATE"        DATE,
    "MARK"             NUMBER(1)
);
/