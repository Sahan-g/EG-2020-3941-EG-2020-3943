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
    public partial class EditModuleWindowVM : ObservableObject
    {
        [ObservableProperty]
        public string moduleName;
        [ObservableProperty]
        public string moduleCode;
        [ObservableProperty]
        public int credits;
        
        [ObservableProperty]
        public int ide;



        [ObservableProperty]
        ObservableCollection<Module> modules;

        [ObservableProperty]
        public Module selectedModule;

        [RelayCommand]
        public void UpdateModule()
        {


            using (var db = new DatabaseContext())
            {
                Module m = db.Modules.Find(Ide);
                if (m != null)
                {
                    m.ModuleName = moduleName;
                    m.ModuleCode = moduleCode;
                    m.Credits = credits;
                    db.SaveChanges();
                    LoadModule();
                    MessageBox.Show("Module details edited successfully!", "Done");
                    ViewModuleWindow stud = new ViewModuleWindow();
                    var win = Application.Current.MainWindow;
                    Application.Current.MainWindow = stud;
                    win.Close();
                    stud.Show();
                }
            }
        }
        public void LoadModule()
        {
            using (var db = new DatabaseContext())
            {
                var list = db.Modules.ToList();
                Modules = new ObservableCollection<Module>(list);
            }
        }
        public EditModuleWindowVM(Module m)
        {
            ModuleName = m.ModuleName;
            ModuleCode = m.ModuleCode;
            Credits = m.Credits;
            
            Ide = m.Id;
            LoadModule();
        }

        public EditModuleWindowVM()
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
            UserLand us = new UserLand();
            us.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = us;


        }
    }
}
