using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using System.Data;
using System.Runtime.Remoting.Contexts;
using System.Configuration;
using System.Management.Instrumentation;

namespace Ovosh
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        string conf = null;
        string conf2 = null;
        SqlConnection conect = null;
        public MainWindow()
        {
            InitializeComponent();

        }
        public string W_i_n(string cofig = "Введите значение")
        {
            string c = null;
            Window1 window = new Window1();
            window.Color.Text = cofig;
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (window.ShowDialog() == true)
            {
                c = window.Color.Text;
            }
            return c;
        }
        public void Tabler(SqlCommand com)
        {
            SqlDataReader rdr = null;
            try
            {

                SqlCommand cmd = com;
                //cmd.Parameters.Add("@p1", SqlDbType.NVarChar).Value = "Тип";
                //cmd.Parameters.Add("@p2", SqlDbType.NVarChar).Value = "Цвет";
                //cmd.Parameters.Add("@p3", SqlDbType.NVarChar).Value = "Название";
                //cmd.Parameters.Add("@p4", SqlDbType.NVarChar).Value = "Колорийность";
                //cmd.Parameters.Add("@p5", SqlDbType.NVarChar).Value = "id";

                DataTable table = new DataTable();
                if (Tab1.IsSelected)
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(table);
                    DATA_TABLE.ItemsSource = table.DefaultView;
                }

                if (Tab2.IsSelected)
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(table);
                    DATA_TABLE2.ItemsSource = table.DefaultView;
                }
                DATA_TABLE.DataContext = table;
               
            }
            catch
            {

            }
        }
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (conect == null) 
            { 
            try
                {
                //@"Data Source = DESKTOP-H6EAR4O\SQLEXPRESS; Initial Catalog = OiF;
                //Integrated Security = True;"
                conect = new SqlConnection();
                conect.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                conect.Open();
                MessageBox.Show("Соединение установлено", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            catch
                {
                MessageBox.Show("Ошибка подключения к Базе Данных!", "Ошбка соединения", MessageBoxButton.OK, MessageBoxImage.Error);
                conect = null;
                }
             }

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (conect != null)
            {
                conect.Close();
                MessageBox.Show("Соединение разорвано", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            Tabler(new SqlCommand("select * from Table_O_F", conect));
        }



        private void ButtonName_Click(object sender, RoutedEventArgs e)
        {
            Tabler(new SqlCommand("select id ,Название from Table_O_F", conect));

        }

        private void ButtonColor_Click(object sender, RoutedEventArgs e)
        {

            Tabler(new SqlCommand("select id ,Цвет from Table_O_F", conect));

        }

        private void ButtonMAX_Click(object sender, RoutedEventArgs e)
        {

            Tabler(new SqlCommand("SELECT Название, Колорийность FROM Table_O_F WHERE Колорийность = (SELECT MAX(Колорийность) FROM Table_O_F)", conect));  }

        private void ButtonMIN_Click(object sender, RoutedEventArgs e)
        {

            Tabler(new SqlCommand("SELECT Название, Колорийность FROM Table_O_F WHERE Колорийность = (SELECT MIN(Колорийность) FROM Table_O_F)", conect));

        }

        private void ButtonAVG_Click(object sender, RoutedEventArgs e)
        {


            Tabler(new SqlCommand("SELECT AVG(Колорийность) FROM Table_O_F", conect));

        }

        private void ButtonCout_Ovosh_Click(object sender, RoutedEventArgs e)
        {

            Tabler(new SqlCommand("SELECT COUNT(*) AS Количество_Овощей FROM Table_O_F WHERE Тип = 'Овощь'", conect));

        }
        private void ButtonCout_Fruts_Click(object sender, RoutedEventArgs e)
        {

            Tabler(new SqlCommand("SELECT COUNT(*) AS Количество_Фруктов FROM Table_O_F WHERE Тип = 'Фрукт'", conect));

        }

        private void ButtonColor_Chose_Click(object sender, RoutedEventArgs e)
        {
            conf = W_i_n("Введите цвет");
            Tabler(new SqlCommand($"SELECT COUNT(*) AS Выбранного_Цвета FROM Table_O_F WHERE Цвет = '{conf}'", conect));

        }

        private void Button_Count_all_Color_Click(object sender, RoutedEventArgs e)
        {


            Tabler(new SqlCommand("SELECT Цвет, COUNT(*) as Количество FROM Table_O_F GROUP BY Цвет", conect));


        }

        private void Button_Klori_Menshe_Click(object sender, RoutedEventArgs e)
        {


            conf = W_i_n("Задайте максимальную колорийность");
            int d;
            if (int.TryParse(conf, out d))
            {
                Tabler(new SqlCommand($"SELECT Название, Колорийность FROM Table_O_F WHERE Колорийность < {conf}", conect));



            }
            else
            {
                MessageBox.Show("Введено не числовое значение!!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private void Button_Kolori_bolshe(object sender, RoutedEventArgs e)
        {

            conf = W_i_n("Задайте минимальню колорийность");
            int d;
            if (int.TryParse(conf, out d))
            {
                Tabler(new SqlCommand($"SELECT Название, Колорийность FROM Table_O_F WHERE Колорийность > {conf}", conect));

            }
            else
            {
                MessageBox.Show("Введено не числовое значение!!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Kolor_Diapazon_Click(object sender, RoutedEventArgs e)
        {
                    Window2 window = new Window2();
                    window.Owner = this;
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    if (window.ShowDialog() == true)
                    {
                        conf = window.Menshe.Text;
                        conf2 = window.Bolshe.Text;
                    }

                    int d, c;
                    if (int.TryParse(conf, out d) & int.TryParse(conf2, out c))
                    {
                        
                        Tabler(new SqlCommand($"SELECT Название, Колорийность FROM Table_O_F WHERE Колорийность < {conf} AND Колорийность > {conf2}", conect));

                    }
                    else
                    {
                        MessageBox.Show("Введено не числовое значение!!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }        
        }

        private void Button_Red_Jelow_Click(object sender, RoutedEventArgs e)
        {
            Tabler(new SqlCommand("SELECT Название, Цвет FROM Table_O_F WHERE Цвет = 'Красный' OR Цвет = 'Желтый'", conect));
        }
    }
}

  


