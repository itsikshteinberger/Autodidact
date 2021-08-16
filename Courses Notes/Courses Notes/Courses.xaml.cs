using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Courses_Notes
{
    /// <summary>
    /// Interaction logic for Courses.xaml
    /// </summary>
    public partial class Courses : Page
    {
        
        public Courses()
        {
            InitializeComponent();
            loadData();
        }
        public void loadData()
        {
            
            DataTable Dt = Database.loadData("select * from Courses");
            foreach (DataRow dataRow in Dt.Rows)
            {
                Course course = new Course(Convert.ToInt32(dataRow.ItemArray[0]),
                                            (string)dataRow.ItemArray[1],
                                            (string)dataRow.ItemArray[2],
                                            (string)dataRow.ItemArray[3],
                                            (string)dataRow.ItemArray[4],
                                            Convert.ToInt32(dataRow.ItemArray[5]));
                MainPanel.Children.Add(course.AddCourse());
                //foreach (var item in dataRow.ItemArray)
            }
        }

        public void Add(object sender, RoutedEventArgs e)
        {
            
            ((MainWindow)System.Windows.Application.Current.MainWindow).Navigate(new Add());
        }

     
        private void Add_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Opacity = 0.8;
            ((Button)sender).Foreground = Brushes.GreenYellow;
        }
        private void Add_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Opacity = 0.4;
            ((Button)sender).Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF0C0C0C");
        }
    }
}
