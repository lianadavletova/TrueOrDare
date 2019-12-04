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
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        Random rnd;
        SqlConnection connect1 = new SqlConnection(@"Data Source=ADMN-ПК;Initial Catalog=truth_Or_Desire;Integrated Security=True");
        int[] arr;

        public int[] RandomArr(Random rnd, int lenght)
        {
            int[]arr = new int[lenght];
            for (int i = 0; i < arr.Length; i++)
            {
                var _r = rnd.Next(1, lenght);
                if (!(arr.Contains(_r)))
                {
                    arr[i] = _r;
                }
                else
                {
                    i--;
                }
            }
            return arr;
        }
        int countarr;
        public Window4()
        {
            InitializeComponent();
            rnd = new Random();
            connect1.Open();
            SqlCommand cmd = connect1.CreateCommand();
            cmd.CommandText = "select count(id_p) as count from Players";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                countarr = Convert.ToInt32(reader["count"]);
            }
            connect1.Close();
            arr = RandomArr(rnd, countarr);
            connect1.Open();
            SqlCommand cmd1 = connect1.CreateCommand();
            cmd1.CommandText = "select id_p from Players where id_p = '"+arr[0]+"'";
            SqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                nameplayer.Content = reader1["name_p"].ToString();
            }
            connect1.Close();
        }
    }
}
