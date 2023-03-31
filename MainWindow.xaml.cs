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

namespace Ovosh
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conect = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                conect = new SqlConnection(@"Data Source = DESKTOP-H6EAR4O\SQLEXPRESS; Initial Catalog = OiF;
                Integrated Security = True;");
                conect.Open();
                MessageBox.Show("Соединение установлено", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch 
            {
                MessageBox.Show("Ошибка подключения к Базе Данных!","Оибка соединения",MessageBoxButton.OK,MessageBoxImage.Error);
                conect = null;
            }
            
        }

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            
           
            if (conect != null)
            {
                
               int index = TAB_CONTROL.SelectedIndex;
                SqlDataReader rdr = null;
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from Table_O_F", conect);
                    rdr = cmd.ExecuteReader();
                    
                    if (Tab1.IsSelected)
                    {
                        listB1.Items.Clear();
                        
                        listB1.Items.Add(string.Format("{0,-5} {1,-15} {2,-15} {3,-25} {4,-17}", rdr.GetName(0), rdr.GetName(1) ,rdr.GetName(2) ,rdr.GetName(3) , rdr.GetName(4)));
                        while (rdr.Read())
                        {
                           
                            listB1.Items.Add(string.Format("{0,-5} {1,-15} {2,-15} {3,-25} {4,-17}", rdr[0], rdr[1], rdr[2], rdr[3], rdr[4]));

                        }
                        
                    }

                    if (Tab2.IsSelected)
                    {
                        
                        listB2.Items.Clear();
                        listB2.Items.Add(string.Format("{0,-5} {1,-15} {2,-15} {3,-25} {4,-17}", rdr.GetName(0), rdr.GetName(1), rdr.GetName(2), rdr.GetName(3), rdr.GetName(4)));
                        while (rdr.Read())
                        {

                            listB2.Items.Add(string.Format("{0,-5} {1,-15} {2,-15} {3,-25} {4,-17}", rdr[0], rdr[1], rdr[2], rdr[3], rdr[4]));

                        }
                    }
                    rdr.Close();
                }
                catch
                {

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

        private void ButtonName_Click(object sender, RoutedEventArgs e)
        {
            if (conect != null)
            {

                int index = TAB_CONTROL.SelectedIndex;
                SqlDataReader rdr = null;
                try
                {
                    SqlCommand cmd = new SqlCommand("select id ,Название from Table_O_F", conect);
                    rdr = cmd.ExecuteReader();

                    if (Tab1.IsSelected)
                    {
                        listB1.Items.Clear();
                        
                        listB1.Items.Add(string.Format("{0,-5} {1,-15}", rdr.GetName(0), rdr.GetName(1)));
                        while (rdr.Read())
                        {
                            listB1.Items.Add(string.Format("{0,-5} {1,-15}", rdr[0], rdr[1]));
                        }

                    }

                    if (Tab2.IsSelected)
                    {
                        
                        listB2.Items.Clear();
                        listB2.Items.Add(string.Format("{0,-5} {1,-15}", rdr.GetName(0), rdr.GetName(1)));
                        while (rdr.Read())
                        {
                            listB2.Items.Add(string.Format("{0,-5} {1,-15}", rdr[0], rdr[1]));
                        }
                    }
                    rdr.Close();
                }
                catch
                {

                }

            }

        }

        private void ButtonColor_Click(object sender, RoutedEventArgs e)
        {
            if (conect != null)
            {

                int index = TAB_CONTROL.SelectedIndex;
                SqlDataReader rdr = null;
                try
                {
                    SqlCommand cmd = new SqlCommand("select id ,Цвет from Table_O_F", conect);
                    rdr = cmd.ExecuteReader();

                    if (Tab1.IsSelected)
                    {
                        listB1.Items.Clear();
                        
                        listB1.Items.Add(string.Format("{0,-5} {1,-15}", rdr.GetName(0), rdr.GetName(1)));
                        while (rdr.Read())
                        {
                            listB1.Items.Add(string.Format("{0,-5} {1,-15}", rdr[0], rdr[1]));
                        }

                    }

                    if (Tab2.IsSelected)
                    {
                        
                        listB2.Items.Clear();
                        listB2.Items.Add(string.Format("{0,-5} {1,-15}", rdr.GetName(0), rdr.GetName(1)));
                        while (rdr.Read())
                        {
                            listB2.Items.Add(string.Format("{0,-5} {1,-15}", rdr[0], rdr[1]));
                        }
                    }
                    rdr.Close();
                }
                catch
                {

                }

            }
        }

        private void ButtonMAX_Click(object sender, RoutedEventArgs e)
        {
            if (conect != null)
            {

                int index = TAB_CONTROL.SelectedIndex;
                SqlDataReader rdr = null;
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT Название, Колорийность FROM Table_O_F WHERE Колорийность = (SELECT MAX(Колорийность) FROM Table_O_F)", conect);
                    rdr = cmd.ExecuteReader();

                    if (Tab1.IsSelected)
                    {
                        listB1.Items.Clear();
                        listB1.Items.Add("Максимальная колорийность");
                        while (rdr.Read())
                        {
                            listB1.Items.Add(string.Format("{0,-15}{1,-5}", rdr[0], rdr[1]));
                        }

                    }

                    if (Tab2.IsSelected)
                    {
                        
                        listB2.Items.Clear();
                        listB2.Items.Add("Максимальная колорийность"); ;
                        while (rdr.Read())
                        {
                            listB2.Items.Add(string.Format("{0,-15}{1,-5}", rdr[0], rdr[1]));
                        }
                    }
                    rdr.Close();
                }
                catch
                {

                }
            }
        }

        private void ButtonMIN_Click(object sender, RoutedEventArgs e)
        {
            if (conect != null)
            {

                int index = TAB_CONTROL.SelectedIndex;
                SqlDataReader rdr = null;
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT Название, Колорийность FROM Table_O_F WHERE Колорийность = (SELECT MIN(Колорийность) FROM Table_O_F)", conect);
                    rdr = cmd.ExecuteReader();

                    if (Tab1.IsSelected)
                    {
                        listB1.Items.Clear();
                        listB1.Items.Add("Минимальная колорийность");
                        while (rdr.Read())
                        {
                            listB1.Items.Add(string.Format("{0,-15}{1,-5}", rdr[0], rdr[1]));
                        }

                    }

                    if (Tab2.IsSelected)
                    {

                        listB2.Items.Clear();
                        listB2.Items.Add("Минимальная колорийность"); ;
                        while (rdr.Read())
                        {
                            listB2.Items.Add(string.Format("{0,-15}{1,-5}", rdr[0], rdr[1]));
                        }
                    }
                    rdr.Close();
                }
                catch
                {

                }
            }
        }

        private void ButtonAVG_Click(object sender, RoutedEventArgs e)
        {
            if (conect != null)
            {

                int index = TAB_CONTROL.SelectedIndex;
                SqlDataReader rdr = null;
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT AVG(Колорийность) FROM Table_O_F", conect);
                    rdr = cmd.ExecuteReader();

                    if (Tab1.IsSelected)
                    {
                        listB1.Items.Clear();
                        listB1.Items.Add("Средняя колорийность");
                        while (rdr.Read())
                        {
                            listB1.Items.Add(string.Format("{0,-15}", rdr[0]));
                        }

                    }

                    if (Tab2.IsSelected)
                    {

                        listB2.Items.Clear();
                        listB2.Items.Add("Средняя колорийность"); ;
                        while (rdr.Read())
                        {
                            listB2.Items.Add(string.Format("{0,-15}", rdr[0])); ;
                        }
                    }
                    rdr.Close();
                }
                catch
                {

                }
            }
        }
    }
}
