using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_StudRegSys.win;

namespace WPF_StudRegSys.viewmodels
{
    public partial class AdminLandVM : ObservableObject
    {
        [RelayCommand]
        public void Minimize()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;

        }

        [RelayCommand]
        public void Close()
        {
           Application.Current.Shutdown();
        }

        [RelayCommand]
        public void AddNewUser()
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow= admin;

        }


        [RelayCommand]
        public void ViewALlUsers()
        {
            ViewAllUsers viewA= new ViewAllUsers();
            viewA.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow= viewA;
           
        }

    }
}
