using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_Select__Insert__and_Update
{
    public partial class FrmClubRegistration : Form
    {
        public FrmClubRegistration()
        {
            InitializeComponent();
        }

       
        private ClubRegistrationQuery clubRegistrationQuery;
        private int ID, Age, count;
        private string FirstName, MiddleName, LastName, Gender, Program;
        private long StudentId;


        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListOfClubMembers();
        }

        private void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView1.DataSource = clubRegistrationQuery.bindingSource;
        }


        private int RegistrationID()
        {
            count++;
            return count;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            ID = RegistrationID();

            if (!long.TryParse(txtStudentId.Text, out StudentId))
            {
                MessageBox.Show("Please enter a valid Student ID.");
                return;
            }

            if (!int.TryParse(txtAge.Text, out Age))
            {
                MessageBox.Show("Please enter a valid Age.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtStudentId.Text) || string.IsNullOrWhiteSpace(txtAge.Text))
            {
                MessageBox.Show("Student ID and Age cannot be empty.");
                return;
            }


            FirstName = txtFirstName.Text;
            MiddleName = txtMiddleName.Text;
            LastName = txtLastName.Text;
            Gender = cmbGender.SelectedItem?.ToString() ?? ""; 
            Program = cmbProgram.SelectedItem?.ToString() ?? "";  



            bool result = clubRegistrationQuery.RegisterStudent(ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }
    }


}
