namespace Week8;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
class DataTier
{
    public string connStr = "server=20.172.0.16;user=hskarr1;database=hskarr1;port=8080;password=hskarr1";

    // perform login check using Stored Procedure "LoginCount" in Database based on given user' studentID and Password
    public bool LoginCheck(User user)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            string procedure = "LoginCount";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure; // set the commandType as storedProcedure
            cmd.Parameters.AddWithValue("@inputUserID", user.userID);
            cmd.Parameters.AddWithValue("@inputUserPassword", user.userPassword);
            cmd.Parameters.Add("@userCount", MySqlDbType.Int32).Direction = ParameterDirection.Output;
            MySqlDataReader rdr = cmd.ExecuteReader();

            int returnCount = (int)cmd.Parameters["@userCount"].Value;
            rdr.Close();
            conn.Close();

            if (returnCount == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return false;
        }

    }
    public DataTable CheckEnrollment(User user)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        Console.WriteLine("Please input a semester in TermYear format, e.g: Fall2022, Spring2021");
        string semester = Console.ReadLine();
        try
        {
            conn.Open();
            string procedure = "CheckEnrollment";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inputStudentID", user.userID);
            cmd.Parameters["@inputStudentID"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@inputSemester", semester);
            cmd.Parameters["@inputSemester"].Direction = ParameterDirection.Input;

            MySqlDataReader rdr = cmd.ExecuteReader();

            DataTable tableEnrollment = new DataTable();
            tableEnrollment.Load(rdr);
            rdr.Close();
            conn.Close();
            return tableEnrollment;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return null;
        }
    }

    public void AddCourse(User user, string semester, int courseID)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
           conn.Open();
            string procedure = "AddCourse";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inputStudentID", user.userID);
            cmd.Parameters["@inputStudentID"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@inputCourseID", courseID);
            cmd.Parameters["@inputCourseID"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@inputSemester", semester);
            cmd.Parameters["@inputSemester"].Direction = ParameterDirection.Input;
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
        }
    }

    public void DropCourse(User user, string semester, int courseID){
        MySqlConnection conn = new MySqlConnection(connStr);
       
        try
        {  
            conn.Open();
            string procedure = "DropCourse";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inputStudentID", user.userID);
            cmd.Parameters["@inputStudentID"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@inputCourseID", courseID);
            cmd.Parameters["@inputCourseID"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@inputSemester", semester);
            cmd.Parameters["@inputSemester"].Direction = ParameterDirection.Input;
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
        }
    }

}
