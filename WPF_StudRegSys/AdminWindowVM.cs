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
    public partial class AdminWindowVM : ObservableObject
    {
        [ObservableProperty]
        public string username;
        [ObservableProperty]
        public string password;

        [ObservableProperty]
        ObservableCollection<User> users;

        [RelayCommand]
        public void InsertUser() 
        {
            User u = new User()
            {
                Username = username,
               Role="User",
                Password = password
                
                
            };

            using (var db = new DatabaseContext())
            {
                db.Users.Add(u);
                db.SaveChanges();
            }
            LoadUser();

            MessageBox.Show("User Added Sccessfully", "Done");

        }

        public void LoadUser()
        {
            using (var db = new DatabaseContext())
            {
                var list = db.Users.ToList();
                Users = new ObservableCollection<User>(list);
            }
        }

        public AdminWindowVM()
        {
            LoadUser();
        }

        [RelayCommand]
        public void Minimize()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;

        }

        [RelayCommand]
        public static void Close()
        {
            AdminLand sec = new AdminLand();
            Application.Current.MainWindow.Close();
            
            Application.Current.MainWindow= sec;
            sec.Show();
        }

        //public void AddNewUser()
        //{
        //    AdminWindow admin = new AdminWindow();
        //    admin.Show();
        //    Application.Current.MainWindow.Close();

        //}

    }
}
