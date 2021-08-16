using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Page
    {
        private Course course;
        public Details()
        {
            InitializeComponent();
            course = Database.Current_course;
            datemassege.Content = course.DateMessage();
            name.Content = course.name;
            days.Content = Days();
            Strike.Content = course.strike;
            Complete.IsEnabled = !AlreadyMade();

            daysimg.Source = new BitmapImage(new Uri(@"C:\Users\User\source\repos\Courses Notes\Courses Notes\Images\calendar.png"));
            if (!AlreadyMade())
            {
                Strikeimg.Source = new BitmapImage(new Uri(@"C:\Users\User\source\repos\Courses Notes\Courses Notes\Images\fireColor.png"));
            }
            AddCalendar();

        }
        public void AddCalendar()
        {   
            if (!course.IsStartNull())
            {
                StackPanel stackPanel = new StackPanel();
                Label label = new Label();
                label.Content = "Start:";
                Calendar calendar = new Calendar();
                calendar.IsTodayHighlighted = false;
                calendar.SelectedDate = course.start;
                calendar.DisplayDate = course.start;
                calendar.SelectedDatesChanged += freezestart;
                stackPanel.Children.Add(label);
                stackPanel.Children.Add(calendar);
                dates.Children.Add(stackPanel);
            }
            if (!course.IsEndNull())
            {
                StackPanel stackPanel = new StackPanel();
                
                Label label = new Label();
                
                label.Content = "End:";
                Calendar calendar = new Calendar();
                calendar.IsTodayHighlighted = false;
                calendar.SelectedDate = course.end;
                calendar.DisplayDate = course.end;
                calendar.SelectedDatesChanged += freezeend;
                stackPanel.Children.Add(label);
                stackPanel.Children.Add(calendar);
                dates.Children.Add(stackPanel);
            }
            
        }
        private void freezeend(object sender, SelectionChangedEventArgs e) {
            ((Calendar)sender).SelectedDate = course.end;
            ((Calendar)sender).DisplayDate = course.end;
        }
        private void freezestart(object sender, SelectionChangedEventArgs e)
        {
            ((Calendar)sender).SelectedDate = course.start;
            ((Calendar)sender).DisplayDate = course.start;
        }


        public bool IsYesterday()
        {
            DataTable dataTable = Database.loadData(String.Format("select count(*) from learn where Date = '{0}' and CourseId = {1} GROUP BY CourseId", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), course.id));
            return dataTable.Rows.Count != 0;
        }
        public bool AlreadyMade()
        {
            DataTable dataTable = Database.loadData(String.Format("select count(*) from learn where Date = '{0}' and CourseId = {1} GROUP BY CourseId", DateTime.Now.ToString("yyyy-MM-dd") , course.id));
            return dataTable.Rows.Count != 0;
        }
        public int Days()
        {
            DataTable dataTable = Database.loadData("SELECT * FROM learn WHERE CourseId = "+course.id);
            return dataTable.Rows.Count;
        }
        public void GoToCourse(object sender, RoutedEventArgs e)
        {
            try {
            System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = course.link,
                    UseShellExecute = true
                }); }
            catch { }
            
        }
        public void SetLearn(object sender, RoutedEventArgs e)
        {
            Strikeimg.Source = new BitmapImage(new Uri(@"C:\Users\User\source\repos\Courses Notes\Courses Notes\Images\fire.png"));
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            Database.ExecuteQuery(String.Format("INSERT INTO learn (CourseId ,Date) VALUES ({0},'{1}')",course.id,date));

            if (IsYesterday())
            {
                Database.ExecuteQuery(String.Format("UPDATE Courses SET Strike = {0} WHERE Id = {1}", course.strike + 1, course.id));
                course.strike += 1;
            }
            else
            {
                Database.ExecuteQuery(String.Format("UPDATE Courses SET Strike = {0} WHERE Id = {1}", 1, course.id));
                course.strike = 1;
            }

            Strike.Content = course.strike;
            days.Content = Days();
            Complete.IsEnabled = false;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Database.ExecuteQuery(String.Format("DELETE FROM learn WHERE CourseId = {0}",course.id));
            Database.ExecuteQuery(String.Format("DELETE FROM Courses WHERE Id = {0}", course.id));
            ((MainWindow)System.Windows.Application.Current.MainWindow).Navigate(new Courses());
        }
    }
}
