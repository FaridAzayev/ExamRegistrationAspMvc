using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Utils.Data.Access;
using ExamRegistrationAspMvc.Models;

namespace ExamRegistrationAspMvc.Repository
{
    public class DaoService
    {
        public List<Subject> GetSubjects()
        {
            var subjects = new List<Subject>();
            using (OracleDataAdapter dataAdapter = new OracleDataAdapter(_QueryText.Text,
                new OracleConnection(ConnectionStrings.getConnectionString(DbTypes.oracle))))
            {
                int rowNumsImported = dataAdapter.Fill(_DataTable);
            }
            return subjects;
        }
    }
}