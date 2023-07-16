using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WPF_StudRegSys.win;

namespace WPF_StudRegSys.viewmodels
{
    public partial class UserLandVM : ObservableObject
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
        public void ViewStudentInfo()
        {

            ViewStudentWindow view = new ViewStudentWindow();
            view.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = view;

            

        }

        [RelayCommand]
        public void ViewModuleInfo()
        {
            ViewModuleWindow view = new ViewModuleWindow();
            view.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = view;
            
        }

        [RelayCommand]
        public void ViewResults()
        {
            ViewResultWindow view = new ViewResultWindow();
            view.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = view;
            
        }

        
        


        //[RelayCommand]
        //public void AddNewStudent()
        //{
        //    MainWindow main = new MainWindow();
        //    main.Show();
        //    Application.Current.MainWindow.Close();
        //}
    }
}
