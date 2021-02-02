using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DAL
{
    public static class DBManager
    {
        public static readonly string connString = string.Empty;

        static DBManager()
        {
            connString = ConfigurationManager.ConnectionStrings["dbString"].ConnectionString;
        }

        public static Student GetByID(int rollNo)
        {
            Student theStudent = new Student();

            IDbConnection conn = new MySqlConnection();
            conn.ConnectionString = connString;
            string query = "Select * from student WHERE rollNo=" + rollNo;
            IDbCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd as MySqlCommand);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                DataRowCollection rows = ds.Tables[0].Rows;
                foreach (DataRow row in rows)
                {
                    theStudent.RollNo = int.Parse(row["rollNo"].ToString());
                    theStudent.Name = row["name"].ToString();
                    theStudent.Branch = row["branch"].ToString();
                    theStudent.FatherName = row["fatherName"].ToString();
                    theStudent.Dob = DateTime.Parse(row["dob"].ToString());
                    theStudent.Gender = row["gender"].ToString();

                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
            // implementation 
            return theStudent;
        }
        public static List<Student> GetAllStudents()
        {
            List<Student> allStudents = new List<Student>();
            IDbConnection conn = new MySqlConnection();
            conn.ConnectionString = connString;
            string query = "Select * from student";
            IDbCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd as MySqlCommand);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                DataRowCollection rows = ds.Tables[0].Rows;
                foreach (DataRow row in rows)
                {
                    Student theStudent = new Student();
                    theStudent.RollNo = int.Parse(row["rollNo"].ToString());
                    theStudent.Name = row["name"].ToString();
                    theStudent.Branch = row["branch"].ToString();
                    theStudent.FatherName = row["fatherName"].ToString();
                    theStudent.Dob = DateTime.Parse(row["dob"].ToString());
                    theStudent.Gender = row["gender"].ToString();
                    allStudents.Add(theStudent);
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
            return allStudents;
        }
        public static bool Insert(Student newStudent)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))   //DI via Constructor
                {
                    if (con.State == ConnectionState.Closed)        //if connection is closed?
                        con.Open();
                    string query = "INSERT INTO student (RollNO,Name ,Branch, FatherName, Dob, Gender) " +
                                                "VALUES (@RollNo, @Name, @Branch, @FatherName, @Dob,@Gender)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@RollNo", newStudent.RollNo));
                    cmd.Parameters.Add(new MySqlParameter("@Name", newStudent.Name));
                    cmd.Parameters.Add(new MySqlParameter("@Branch", newStudent.Branch));
                    cmd.Parameters.Add(new MySqlParameter("@FatherName", newStudent.FatherName));
                    cmd.Parameters.Add(new MySqlParameter("@Dob", newStudent.Dob));
                    cmd.Parameters.Add(new MySqlParameter("@Gender", newStudent.Gender));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status = true;
                }
            }
            catch (MySqlException exp)
            {
                string message = exp.Message;
            }
            return status;
        }
        public static bool Update(Student existingStudent)
        {
            bool status = false;
            IDbConnection conn = new MySqlConnection();
            conn.ConnectionString = connString;
            IDbCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from student";
            cmd.Connection = conn;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd as MySqlCommand);
            DataSet ds = new DataSet();
            try
            {
                MySqlCommandBuilder cmdbuilder = new MySqlCommandBuilder(da);
                da.Fill(ds);
                DataColumn[] keyColumns = new DataColumn[1];
                keyColumns[0] = ds.Tables[0].Columns["RollNo"];
                ds.Tables[0].PrimaryKey = keyColumns;
                DataRow datarow = ds.Tables[0].Rows.Find(existingStudent.RollNo);
                datarow["name"] = existingStudent.Name;
                datarow["fatherName"] = existingStudent.FatherName;
                datarow["branch"] = existingStudent.Branch;
                datarow["dob"] = existingStudent.Dob;
                datarow["gender"] = existingStudent.Gender;
                da.Update(ds);
                status = true;
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                string msg = e.Message;
            }
            return status;
        }
        public static bool Delete(int rollNO)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    string query = "DELETE from student  WHERE rollNO=@rollNO";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@rollNO", rollNO));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status = true;
                }
            }
            catch (MySqlException exp)
            {
                string message = exp.Message;
            }
            return status;
        }

        // result tables records-----------------------------------------------
        public static Result GetByRollno(int id)
        {
            Result result = new Result();

            IDbConnection conn = new MySqlConnection();
            conn.ConnectionString = connString;
            string query = "Select * from result WHERE RollNo=" + id;
            IDbCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd as MySqlCommand);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                DataRowCollection rows = ds.Tables[0].Rows;
                foreach (DataRow row in rows)
                {
                    result.RollNo = int.Parse(row["RollNo"].ToString());
                    result.English = int.Parse(row["English"].ToString());
                    result.Java = int.Parse(row["Java"].ToString());
                    result.Angular = int.Parse(row["Angular"].ToString());
                    result.DotNet = int.Parse((row["DotNet"].ToString()));
                    result.Html = int.Parse(row["Html"].ToString());

                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
            // implementation 
            return result;
        }
        public static List<Result> GetAllResult()
        {
            List<Result> allResult = new List<Result>();
            IDbConnection conn = new MySqlConnection();
            conn.ConnectionString = connString;
            string query = "Select * from result";
            IDbCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd as MySqlCommand);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                DataRowCollection rows = ds.Tables[0].Rows;
                foreach (DataRow row in rows)
                {
                    Result result = new Result();
                    result.RollNo = int.Parse(row["RollNo"].ToString());
                    result.English = int.Parse(row["English"].ToString());
                    result.Java = int.Parse(row["Java"].ToString());
                    result.Angular = int.Parse(row["Angular"].ToString());
                    result.DotNet = int.Parse((row["DotNet"].ToString()));
                    result.Html = int.Parse(row["Html"].ToString());
                    allResult.Add(result);
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
            return allResult;
        }


        public static bool InsertResult(Result newResult)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))   //DI via Constructor
                {
                    if (con.State == ConnectionState.Closed)        //if connection is closed?
                        con.Open();
                    string query = "INSERT INTO result (RollNO,English ,Java, Angular, DotNet, Html) " +
                                                "VALUES (@RollNo, @English, @Java, @Angular, @DotNet,@Html)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@RollNo", newResult.RollNo));
                    cmd.Parameters.Add(new MySqlParameter("@English", newResult.English));
                    cmd.Parameters.Add(new MySqlParameter("@Java", newResult.Java));
                    cmd.Parameters.Add(new MySqlParameter("@Angular", newResult.Angular));
                    cmd.Parameters.Add(new MySqlParameter("@DotNet", newResult.DotNet));
                    cmd.Parameters.Add(new MySqlParameter("@Html", newResult.Html));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status = true;
                }
            }
            catch (MySqlException exp)
            {
                string message = exp.Message;
            }
            return status;
        }
        public static bool UpdateResult(Result existingResult)
        {
            bool status = false;
            IDbConnection conn = new MySqlConnection();
            conn.ConnectionString = connString;
            IDbCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from result";
            cmd.Connection = conn;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd as MySqlCommand);
            DataSet ds = new DataSet();
            try
            {
                MySqlCommandBuilder cmdbuilder = new MySqlCommandBuilder(da);
                da.Fill(ds);
                DataColumn[] keyColumns = new DataColumn[1];
                keyColumns[0] = ds.Tables[0].Columns["RollNo"];
                ds.Tables[0].PrimaryKey = keyColumns;
                DataRow datarow = ds.Tables[0].Rows.Find(existingResult.RollNo);
                datarow["English"] = existingResult.English;
                datarow["Java"] = existingResult.Java;
                datarow["Angular"] = existingResult.Angular;
                datarow["DotNet"] = existingResult.DotNet;
                datarow["Html"] = existingResult.Html;
                da.Update(ds);
                status = true;
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                string msg = e.Message;
            }
            return status;
        }
        public static bool DeleteResult(int RollNO)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    string query = "DELETE from result  WHERE RollNO=@RollNO";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@rollNO", RollNO));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status = true;
                }
            }
            catch (MySqlException exp)
            {
                string message = exp.Message;
            }
            return status;
        }
    }
}
