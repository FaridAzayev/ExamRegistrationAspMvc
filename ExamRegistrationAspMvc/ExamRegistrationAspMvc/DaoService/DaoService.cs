using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Utils.Data.Access;
using ExamRegistrationAspMvc.Models;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Globalization;

namespace ExamRegistrationAspMvc.Repository
{
    public class DaoService
    {
        private readonly string GET_SUBJECTS_SQL 
            = "SELECT * FROM SUBJECTS";
        private readonly string DELETE_SUBJECT_SQL 
            = "DELETE FROM SUBJECTS WHERE TRIM(CODE) = :P_CODE";
        private readonly string INSERT_SUBJECT_SQL 
            = "INSERT INTO SUBJECTS (CODE, CLASS_NO, NAME, TEACHER_FIRSTNAME, TEACHER_LASTNAME) VALUES (:P_CODE, :P_NO, :P_NAME, :P_TFNAME, :P_TSNAME)";
        private readonly string UPDATE_SUBJECT_SQL 
            = "UPDATE SUBJECTS SET CLASS_NO = :P_NO, NAME = :P_NAME, TEACHER_FIRSTNAME = :P_TFNAME, TEACHER_LASTNAME = :P_TSNAME WHERE TRIM(CODE) = :P_CODE";

        private readonly string GET_STUDENTS_SQL 
            = "SELECT * FROM STUDENTS";
        private readonly string DELETE_STUDENT_SQL 
            = "DELETE FROM STUDENTS WHERE NUM = :P_NUM";
        private readonly string INSERT_STUDENT_SQL 
            = "INSERT INTO STUDENTS (NUM, FIRST_NAME, LAST_NAME, CLASS_NO) VALUES (:P_NUM, :P_FNAME, :P_SNAME, :P_NO)";
        private readonly string UPDATE_STUDENT_SQL 
            = "UPDATE STUDENTS SET CLASS_NO = :P_NO, FIRST_NAME = :P_FNAME, LAST_NAME = :P_SNAME WHERE NUM = :P_NUM";

        private readonly string GET_EXAMS_SQL
            = "SELECT * FROM EXAMS";
        private readonly string DELETE_EXAM_SQL
            = "DELETE FROM EXAMS WHERE TRIM(SUBJECT_CODE) = :P_CODE AND STUDENT_NUMBER = :P_NUM";
        private readonly string INSERT_EXAM_SQL
            = "INSERT INTO EXAMS (SUBJECT_CODE, STUDENT_NUMBER, EXAM_DATE, MARK) VALUES (:P_CODE, :P_NUM, :P_DATE, :P_MARK)";

        private string connStr;

        public DaoService()
        {
            connStr = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
        }

        public List<Subject> GetSubjects()
        {
            var subjects = new List<Subject>();
            var _DataTable = new DataTable();

            using (OracleDataAdapter dataAdapter = new OracleDataAdapter(GET_SUBJECTS_SQL, new OracleConnection(connStr)))
            {
                int rowNumsImported = dataAdapter.Fill(_DataTable);
            }

            foreach (DataRow dataRow in _DataTable.Rows)
            {
                subjects.Add( new Subject()
                {
                    Code = dataRow["CODE"].ToString()
                    , ClassNo = Convert.ToInt32(dataRow["CLASS_NO"])
                    , Name = dataRow["NAME"].ToString()
                    , TeacherFirstName = dataRow["TEACHER_FIRSTNAME"].ToString()
                    , TeacherLastName = dataRow["TEACHER_LASTNAME"].ToString()
                });
            }
            
            return subjects;
        }

        public void DeleteSubject(string code)
        {
            using (OracleConnection con = new OracleConnection(connStr))
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = DELETE_SUBJECT_SQL;
                cmd.Parameters.Add("P_CODE", code);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertSubject(Subject subject)
        {
            using (OracleConnection con = new OracleConnection(connStr))
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = INSERT_SUBJECT_SQL;
                cmd.Parameters.Add("P_CODE", subject.Code);
                cmd.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Int32, ParameterName = "P_NO", Value = subject.ClassNo.ToString() });
                    //"P_NO", subject.ClassNo);
                cmd.Parameters.Add("P_NAME", subject.Name);
                cmd.Parameters.Add("P_TFNAME", subject.TeacherFirstName);
                cmd.Parameters.Add("P_TSNAME", subject.TeacherLastName);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateSubject(Subject subject)
        {
            using (OracleConnection con = new OracleConnection(connStr))
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = UPDATE_SUBJECT_SQL;
                cmd.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Int32, ParameterName = "P_NO", Value = subject.ClassNo.ToString() });
                //"P_NO", subject.ClassNo);
                cmd.Parameters.Add("P_NAME", subject.Name);
                cmd.Parameters.Add("P_TFNAME", subject.TeacherFirstName);
                cmd.Parameters.Add("P_TSNAME", subject.TeacherLastName);
                cmd.Parameters.Add("P_CODE", subject.Code);
                cmd.ExecuteNonQuery();
            }
        }

        

        public List<Student> GetStudents()
        {
            var students = new List<Student>();
            var _DataTable = new DataTable();

            using (OracleDataAdapter dataAdapter = new OracleDataAdapter(GET_STUDENTS_SQL, new OracleConnection(connStr)))
            {
                int rowNumsImported = dataAdapter.Fill(_DataTable);
            }

            foreach (DataRow dataRow in _DataTable.Rows)
            {
                students.Add(new Student()
                {
                    Number = Convert.ToInt32(dataRow["NUM"])
                    , ClassNo = Convert.ToInt32(dataRow["CLASS_NO"])
                    , FirstName = dataRow["FIRST_NAME"].ToString()
                    , LastName = dataRow["LAST_NAME"].ToString()
                });
            }

            return students;
        }

        public void DeleteStudent(int number)
        {
            using (OracleConnection con = new OracleConnection(connStr))
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = DELETE_STUDENT_SQL;
                cmd.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Int32, ParameterName = "P_NUM", Value = number.ToString()});
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertStudent(Student student)
        {
            using (OracleConnection con = new OracleConnection(connStr))
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = INSERT_STUDENT_SQL;
                cmd.Parameters.Add(new OracleParameter() {OracleDbType = OracleDbType.Int32, ParameterName = "P_NUM", Value = student.Number.ToString()});
                cmd.Parameters.Add("P_FNAME", student.FirstName);
                cmd.Parameters.Add("P_SNAME", student.LastName);
                cmd.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Int32, ParameterName = "P_NO", Value = student.ClassNo.ToString() });
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (OracleConnection con = new OracleConnection(connStr))
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = UPDATE_STUDENT_SQL;
                cmd.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Int32, ParameterName = "P_NO", Value = student.ClassNo.ToString() });
                cmd.Parameters.Add("P_FNAME", student.FirstName);
                cmd.Parameters.Add("P_SNAME", student.LastName);
                cmd.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Int32, ParameterName = "P_NUM", Value = student.Number.ToString() });
                cmd.ExecuteNonQuery();
            }
        }


        public List<Exam> GetExams()
        {
            var exams = new List<Exam>();
            var _DataTable = new DataTable();

            using (OracleDataAdapter dataAdapter = new OracleDataAdapter(GET_EXAMS_SQL, new OracleConnection(connStr)))
            {
                int rowNumsImported = dataAdapter.Fill(_DataTable);
            }

            foreach (DataRow dataRow in _DataTable.Rows)
            {
                exams.Add(new Exam()
                {
                    SubjectCode = dataRow["SUBJECT_CODE"].ToString()
                    , StudentNo = Convert.ToInt32(dataRow["STUDENT_NUMBER"])
                    , ExamDate = DateTime.ParseExact(dataRow["EXAM_DATE"].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                    , Mark= Convert.ToInt32(dataRow["MARK"])
                });
            }

            return exams;
        }

        public void DeleteExam(int num, string code)
        {
            using (OracleConnection con = new OracleConnection(connStr))
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = DELETE_EXAM_SQL;
                cmd.Parameters.Add(new OracleParameter() { ParameterName = "P_CODE", Value = code});
                cmd.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Int32, ParameterName = "P_NUM", Value = num.ToString()});
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertExam(Exam exam)
        {
            using (OracleConnection con = new OracleConnection(connStr))
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = INSERT_EXAM_SQL;
                cmd.Parameters.Add("P_CODE", exam.SubjectCode);
                cmd.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Int32, ParameterName = "P_NUM", Value = exam.StudentNo.ToString() });
                cmd.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Date, ParameterName = "P_DATE", Value = exam.ExamDate});
                cmd.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Int32, ParameterName = "P_MARK", Value = exam.Mark.ToString() });
                cmd.ExecuteNonQuery();
            }
        }
    }
}