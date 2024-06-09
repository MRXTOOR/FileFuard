using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Volo.Abp.Data;

namespace FileFuardSetup.Page
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public string connectionString = @"Data Source=DESKTOP-N9D0PE1;Initial Catalog=FileGuard;Integrated Security=True;";

        private void button1_Click(object sender, EventArgs e)
        {
            
            string login = Login1.Text;
            string password = Password.Text;

            if ( string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

           

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Проверка на существование пользователя с таким логином
                    string checkUserQuery = "SELECT COUNT(*) FROM administration WHERE login = @login";
                    SqlCommand checkUserCommand = new SqlCommand(checkUserQuery, connection);
                    checkUserCommand.Parameters.AddWithValue("@login", login);

                    int userCount = (int)checkUserCommand.ExecuteScalar();

                    if (userCount > 0)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует.");
                        return;
                    }

                    // Регистрация нового пользователя
                    string query = "INSERT INTO administration (login, password) VALUES (@login, @password)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Регистрация прошла успешно!");
                        Autarishation form6 = new Autarishation();
                        form6.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при регистрации.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
            //Autarishation autarishation = new Autarishation();
            //this.Hide();
            //autarishation.Show();
        }
    }
}
