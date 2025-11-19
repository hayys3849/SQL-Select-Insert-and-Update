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
            connectionString = @"Data Source = LAB - A - PC00; Initial Catalog = Club_DB; User ID = dulalia.r; Password = ***********; Trust Server Certificate = True";


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

    }


}
