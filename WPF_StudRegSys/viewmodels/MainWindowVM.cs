using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_StudRegSys.win;

namespace WPF_StudRegSys
{
    public partial class MainWindowVM : ObservableObject
    {
        [ObservableProperty]
        public string firstName;
        [ObservableProperty]
        public string lastName;
        [ObservableProperty]
        public int age;
        [ObservableProperty]
        public double gPA;



        [ObservableProperty]
        ObservableCollection<Student> students;

        [RelayCommand]
        public void InsertStudent()
        {
            Student s = new Student() { 


                FirstName = firstName,
                LastName = lastName,
                Age = age,
                GPA = gPA

            };

            using (var db = new DatabaseContext())
            {
                db.Students.Add(s);
                db.SaveChanges();
                LoadStudent();
            }

            
            ViewStudentWindow back= new ViewStudentWindow();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = back;
            back.Show();
            

            
            
        }

        public void LoadStudent()
        {
            using (var db = new DatabaseContext())
            {
                var list = db.Students.ToList();
                Students = new ObservableCollection<Student>(list);
            }
        }

        public MainWindowVM()
        {
            LoadStudent();
        }

        [RelayCommand]
        public void Minimize()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;

        }

        [RelayCommand]
        public void Close()
        {
            ViewStudentWindow us = new ViewStudentWindow();
            us.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = us;


        }


    }
}
