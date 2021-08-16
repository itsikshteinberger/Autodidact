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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;

namespace Courses_Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            
            InitializeComponent();  //learn(CourseId, Date) , Courses(Name,StartDate,EndDate,Link,Strike)
            Database.CreateTabels();
            
            string query = "insert into Courses(Name, StartDate, EndDate, Link, Strike) values ('Machine Learning','null','null','https://www.coursera.org/learn/machine-learning/home/welcome',0)";
            Navigate(new Courses());  
        } 
        public void Navigate(Page page)
        {
            if(page == new Courses())
            {
                
            }
            else if(page == new Add())
            {
               
            }
            frame.NavigationService.Navigate(page);
        }
        private void myFrame_ContentRendered(object sender, EventArgs e)
        {
            frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }

        private void home(object sender, RoutedEventArgs e) { Navigate(new Courses()); }
        private void add(object sender, RoutedEventArgs e) { Navigate(new Add()); }
    }
}
