using System;
using System.Collections.Generic;
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
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Add()
        {
            InitializeComponent();
            end.SelectedDate = DateTime.Now.AddDays(7);
            start.SelectedDate = DateTime.Now;
        }

        private void NoEnd_Click(object sender, RoutedEventArgs e)
        {
            end.IsEnabled = !NoEnd.IsChecked.Value;
        }

        private void NoStart_Click(object sender, RoutedEventArgs e)
        {
            start.IsEnabled = !NoStart.IsChecked.Value;
        }

        private void AddCourse(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(Link.Text) && !String.IsNullOrWhiteSpace(Name.Text))
            {
                string startD = "null", endD = "null";

                if (!NoStart.IsChecked.Value) { startD = start.SelectedDate.Value.ToString("yyyy-MM-dd"); }
                if (!NoEnd.IsChecked.Value) { endD = end.SelectedDate.Value.ToString("yyyy-MM-dd"); }
                
                string query = String.Format("insert into Courses(Name, StartDate, EndDate, Link, Strike) values ('{0}','{1}','{2}','{3}',0)",
                                            Name.Text, startD, endD, Link.Text);
                
                Database.ExecuteQuery(query);
                
                ((MainWindow)System.Windows.Application.Current.MainWindow).Navigate(new Courses());
            }
        }//bool isUri = Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);

        private void start_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            start.DisplayDateStart = DateTime.Now;
            end.DisplayDateStart = start.SelectedDate;
        }

        private void end_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
           
            start.DisplayDateEnd = end.SelectedDate;
        }
    }
}
