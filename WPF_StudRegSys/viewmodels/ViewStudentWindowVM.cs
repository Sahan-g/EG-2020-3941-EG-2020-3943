using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Controls;
using WPF_StudRegSys.win;
using System.Windows.Automation.Peers;

namespace WPF_StudRegSys.viewmodels
{
    public partial class ViewStudentWindowVM : ObservableObject
    {
        [ObservableProperty]
        public int id1;

        [ObservableProperty] 
        public string firstname;

        [ObservableProperty] 
        public string lastname;

        [ObservableProperty] 
        public int age;

        [ObservableProperty] 
        public double gPA;
        
        [ObservableProperty] 
        public Student selectedStudent ;


        [RelayCommand]
        public void Minimize()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;

        }

        [RelayCommand]
        public void Close()
        {
            UserLand us= new UserLand();
            us.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = us;


        }


        [RelayCommand]
        public void AddNewStudent()
        {
            MainWindow main = new MainWindow();
            main.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = main;
        }

        [ObservableProperty]
        ObservableCollection<Student> students;

        public void LoadStudent()
        {
            using (var db = new DatabaseContext())
            {
                var list = db.Students.ToList();
                Students = new ObservableCollection<Student>(list);
            }
        }

        public ViewStudentWindowVM()
        {
            LoadStudent();
        }

        [RelayCommand]
        public void DeleteStudent()
        {
            if (SelectedStudent != null)
            {
                
                Id1 = SelectedStudent.Id;
                Students.Remove(SelectedStudent);
                
                using (var db = new DatabaseContext())
                {
                    Student s = db.Students.Find(Id1);

                    db.Students.Remove(s);
                    db.SaveChanges();
                    LoadStudent();
                    MessageBoxResult result = MessageBox.Show("Student was deleted succesfully!", "Done");
                }
            }
            else
            {
                MessageBox.Show("Plese select a student first", "ERROR!");
            }
        }

        [RelayCommand]
        public void EditStudent()
        {
            if (SelectedStudent != null)
            {

                EditStudentWindow edit1 = new EditStudentWindow();
                edit1.DataContext = new EditStudentWindowVM(SelectedStudent);
                edit1.Show();

                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = edit1;

            }
            else
            {
                MessageBox.Show("Plese select a student first", "ERROR!");
            }
        }

    }   
}
