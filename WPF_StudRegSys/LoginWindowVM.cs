using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_StudRegSys.win;

namespace WPF_StudRegSys
{
    public partial class LoginWindowVM : ObservableObject

    {
        [ObservableProperty]
        public string userName;

        [ObservableProperty]
        public String password;

        [ObservableProperty]
        public SecureString pass;

        [RelayCommand]
        public void login()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                bool adminUserFound = context.Users.Any(user => user.Username == userName && password == user.Password && user.Role == "Admin");
                bool userFound = context.Users.Any(user => user.Username == userName && password == user.Password && user.Role == "User");

                if (adminUserFound)
                {
                   
                    AdminLand admin = new AdminLand();
                    admin.Show();
                    Application.Current.MainWindow.Close();
                    Application.Current.MainWindow = admin;



                }
                else if (userFound)
                {
                    UserLand admin = new UserLand();
                    admin.Show();
                    Application.Current.MainWindow.Close();
                    Application.Current.MainWindow = admin;

                    //MainWindow main = new MainWindow();
                    //main.Show();
                    //Application.Current.MainWindow.Close();

                }
                else
                {
                    MessageBox.Show("Username or PassWord was incorrect", "error ");
                }
            }
        }


        [RelayCommand]
        public void minimize()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
            
        }

        [RelayCommand]
        public void close()
        {
            Application.Current.MainWindow.Close();
        }


       


    }





}
