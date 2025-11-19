using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;

namespace SQL_Select__Insert__and_Update
{

    internal class ClubRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlReader;


        public DataTable dataTable;
        public BindingSource bindingSource;


        private string connectionString;


        public string _FirstName, 
                      _MiddleName, 
                      _LastName, 
                      _Gender, 
                      _Program;
        public int _Age;
        public ClubRegistrationQuery()
        {
            connectionString = @"Data Source=ROD\SQLEXPRESS;Initial Catalog=Club_DB;Integrated Security=True;TrustServerCertificate=True";

            sqlConnect = new SqlConnection(connectionString);

            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }

        public bool DisplayList()
        {
            
            string ViewClubMembers =
                "SELECT StudentId, FirstName, MiddleName, LastName, Age, Gender, Program " +
                "FROM ClubMembers";

          
                
                sqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);

                
                dataTable.Clear();                
                sqlAdapter.Fill(dataTable);        
                bindingSource.DataSource = dataTable;

                return true;
           

        }
        public bool RegisterStudent(int ID, long StudentID, string FirstName, string
        MiddleName, string LastName, int Age, string Gender, string Program)
        {
            sqlCommand = new SqlCommand("INSERT INTO ClubMembers VALUES(@ID, @StudentID, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)", sqlConnect);
            sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            sqlCommand.Parameters.Add("@RegistrationID", SqlDbType.BigInt).Value = StudentID;
            sqlCommand.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = StudentID;
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = MiddleName;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
            sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
            sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;
            sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar).Value = Program;
            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            return true;
        }

        public bool StudentIdExists(long studentId)
        {
            string query = "SELECT COUNT(*) FROM ClubMembers WHERE StudentID = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, sqlConnect))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentId);

                sqlConnect.Open();
                int count = (int)cmd.ExecuteScalar();
                sqlConnect.Close();

                return count > 0; // TRUE if duplicate
            }
        }

        public List<long> GetAllStudentIDs()
        {
            List<long> ids = new List<long>();

            string query = "SELECT StudentId FROM ClubMembers";

            sqlCommand = new SqlCommand(query, sqlConnect);
            sqlConnect.Open();
            sqlReader = sqlCommand.ExecuteReader();

            while (sqlReader.Read())
            {
                ids.Add(sqlReader.GetInt64(0));
            }

            sqlReader.Close();
            sqlConnect.Close();

            return ids;
        }

        public DataRow GetMemberByID(long studentID)
        {
            string query = "SELECT * FROM ClubMembers WHERE StudentId = @id";
            sqlAdapter = new SqlDataAdapter(query, sqlConnect);
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@id", studentID);

            DataTable dt = new DataTable();
            sqlAdapter.Fill(dt);

            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }

        public bool UpdateStudent(long studentID, string firstName, string middleName, string lastName, int age, string gender, string program)
        {
            string query = @"UPDATE ClubMembers 
                     SET FirstName = @FirstName, 
                         MiddleName = @MiddleName, 
                         LastName = @LastName, 
                         Age = @Age, 
                         Gender = @Gender, 
                         Program = @Program
                     WHERE StudentId = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, sqlConnect))
            {
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@MiddleName", middleName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Program", program);
                cmd.Parameters.AddWithValue("@StudentID", studentID);

                sqlConnect.Open();
                cmd.ExecuteNonQuery();
                sqlConnect.Close();
            }

            // Refresh DataTable for DataGridView
            DisplayList(); // This reloads the DataTable and updates the BindingSource
            return true;
        }


    }




}
