using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace ПравдаИлиДействие
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        //string connectionString;
        SqlDataAdapter adapter;
        bool newGame;

        public Window1()
        {
            InitializeComponent();
            //connectionString = "Data Source=ADMN-ПК;Initial Catalog=truth_Or_Desire;Integrated Security=True";
            newGame = false;
            connect1.Open();
            SqlCommand cmd = connect1.CreateCommand();
            cmd.CommandText = "delete from Players";
            cmd.ExecuteNonQuery();
            connect1.Close();
        }

        public void UpdateListBox(SqlConnection con, ListView listBox1)
        {
            listBox1.Items.Clear();
            con.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            string query = "SELECT name_p FROM Players";
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                listBox1.Items.Add(reader["name_p"].ToString());
            }
            con.Close();
        }
        SqlConnection connect1 = new SqlConnection(@"Data Source=ADMN-ПК;Initial Catalog=truth_Or_Desire;Integrated Security=True");
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            connect1.Open();
            SqlCommand cmd = connect1.CreateCommand();
            // cmd.CommandType = CommandType.Text;
            if (newGame == false)
            {
                cmd.CommandText = "insert into Players values(1,'" + namePlayer.Text + "')";
                newGame = true;
            }
            else
            {
                cmd.CommandText = "insert into Players values((select max(id_p) from Players)+1,'" + namePlayer.Text + "')";
            }
            cmd.ExecuteNonQuery();
            connect1.Close();
            UpdateListBox(connect1, listPlayers);
        }
        //int count;
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            //count = Convert.ToInt32(listPlayers.Items.Count);
            Window4 mw = new Window4();
            this.Close();
            mw.Show();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }
    }
}
