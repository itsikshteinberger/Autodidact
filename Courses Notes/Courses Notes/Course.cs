using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Courses_Notes
{
    class Course
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public int strike { get; set; }

        public DateTime start;
        public DateTime end;

        public Course(int id, string name, string start, string end, string link, int strike)
        {
            this.id = id;
            this.name = name;
            this.start = StringToDate(start);
            this.end = StringToDate(end);
            this.link = link;
            this.strike = strike;
        }
        public bool IsStartNull()
        {
            return start == DateTime.MinValue;
        }
        public bool IsEndNull()
        {
            return end == DateTime.MinValue;
        }
        public string DateToString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        public DateTime StringToDate(string date)
        {

            if (date == "null") return DateTime.MinValue;

            string[] dateArray = date.Split('-'); 

            DateTime dateTime = new DateTime(
                Int32.Parse(dateArray[0]), 
                Int32.Parse(dateArray[1]), 
                Int32.Parse(dateArray[2])
            );

            return dateTime;
        }
        public ImageBrush Background()
        {
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(
                new Uri(
                   "C:\\Users\\User\\source\\repos\\Courses Notes\\Courses Notes\\Images\\courseBackground.png"));
            myBrush.ImageSource = image.Source;
            return myBrush;
        }



        public StackPanel AddCourse()
        {
            StackPanel CoursePanel = new StackPanel();

            Button Coursebutton = new Button();
            Coursebutton.Content = this.name;
            Coursebutton.Click += new RoutedEventHandler(OnClick);
            Coursebutton.Background = Background();
            Label DateMassage = new Label();
            DateMassage.Content = DateMessage();
            
            CoursePanel.Children.Add(Coursebutton);
            CoursePanel.Children.Add(DateMassage);

            Style CourseStyle = Application.Current.FindResource("Course") as Style;
            CoursePanel.Style = CourseStyle; 
            return CoursePanel;
        }
        public void OnClick(object sender, RoutedEventArgs e)
        {
            Database.Current_course = this;
            ((MainWindow)System.Windows.Application.Current.MainWindow).Navigate(new Details());
        } 

        public string DateMessage()
        {
            string DateMessage;

            if (this.end == DateTime.MinValue)
            {
                if (this.start == DateTime.MinValue)
                {
                    DateMessage = "There is no time limit";
                }
                else
                {
                    if (Convert.ToInt32((this.start - DateTime.Now).TotalDays) > 0)
                    {
                        DateMessage = "Will start in " + Convert.ToInt32((this.start - DateTime.Now).TotalDays) + " days";
                    }
                    else
                    {
                        DateMessage = "Started before " + Convert.ToInt32((DateTime.Now - this.start).TotalDays) + " days";
                    }
                    //DateMessage = Convert.ToInt32((this.end - DateTime.Now).TotalDays).ToString();
                }    
            } 
            else
            {
                if (Convert.ToInt32((this.end - DateTime.Now).TotalDays) < 0)
                {
                    DateMessage = "Was supposed to end before " + Convert.ToInt32((DateTime.Now - this.end).TotalDays);
                }
                else
                {
                    if(this.start != DateTime.MinValue && Convert.ToInt32((this.start - DateTime.Now).TotalDays) >= 0)
                    {
                        DateMessage = "Will start in " + Convert.ToInt32((this.start - DateTime.Now).TotalDays) + " days";
                    }
                    else
                    {
                        DateMessage = "There are "+ Convert.ToInt32((this.end - DateTime.Now).TotalDays) + " days left";
                    }
                }
            }
            return DateMessage;
        }

    }
}
