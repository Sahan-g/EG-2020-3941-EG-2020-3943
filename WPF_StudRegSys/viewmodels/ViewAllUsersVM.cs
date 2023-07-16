using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_StudRegSys.win;

namespace WPF_StudRegSys.viewmodels
{

    public partial class ViewAllUsersVM :ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<User> users;




        public void LoadUsers()
        {
            using (var db = new DatabaseContext())
            {
                var list = db.Users.ToList();
                Users = new ObservableCollection<User>(list);
            }
        }

        public ViewAllUsersVM()
        {
            LoadUsers();
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

            Application.Current.MainWindow = sec;
            sec.Show();
        }


    }
}
