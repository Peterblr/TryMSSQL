using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TryMSSQL
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlConnection northwinSqlConnection = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);
            sqlConnection.Open();

            northwinSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthwinDB"].ConnectionString);
            northwinSqlConnection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
                $"INSERT INTO Students (FirstName, LastName, Birthday, Adress, Phone, Mail) VALUES (@FirstName, @LastName, @Birthday, @Adress, @Phone, @Mail)",
                sqlConnection);

            DateTime date = DateTime.Parse(textBoxBirthday.Text);

            command.Parameters.AddWithValue("FirstName", textBoxFirstName.Text);
            command.Parameters.AddWithValue("LastName", textBoxLastName.Text);
            command.Parameters.AddWithValue("Birthday", $"{date.Month}/{date.Day}/{date.Year}");
            command.Parameters.AddWithValue("Adress", textBoxAdress.Text);
            command.Parameters.AddWithValue("Phone", textBoxPhone.Text);
            command.Parameters.AddWithValue("Mail", textBoxMail.Text);

            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                textBoxSelect.Text,
                northwinSqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

    }
}
