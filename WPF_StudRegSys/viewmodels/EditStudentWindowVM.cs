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

namespace WPF_StudRegSys.viewmodels
{
    public partial class EditStudentWindowVM : ObservableObject
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
        public int ide;



        [ObservableProperty]
        ObservableCollection<Student> students;

        [ObservableProperty]
        public Student selectedStudent;

        [RelayCommand]
        public void UpdateStudent()
        {
           

            using (var db = new DatabaseContext())
            {
                Student s = db.Students.Find(Ide);
                if (s != null)
                {
                    s.FirstName = firstName;
                    s.LastName = lastName;
                    s.Age = age;
                    s.GPA = gPA;
                    db.SaveChanges();
                    LoadStudent();
                    MessageBox.Show("Student details edited successfully!", "Done");
                    ViewStudentWindow stud = new ViewStudentWindow();
                    var win = Application.Current.MainWindow;
                    Application.Current.MainWindow = stud;
                    win.Close();
                    stud.Show();



                }                
            }
        }
        public void LoadStudent()
        {
            using (var db = new DatabaseContext())
            {
                var list = db.Students.ToList();
                Students = new ObservableCollection<Student>(list);
            }
        }
        public EditStudentWindowVM(Student s)
        {
            FirstName = s.FirstName;
            LastName = s.LastName;
            Age = s.Age;
            GPA = s.GPA;
            Ide = s.Id;
            LoadStudent();
        }

        public EditStudentWindowVM()
        {

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
