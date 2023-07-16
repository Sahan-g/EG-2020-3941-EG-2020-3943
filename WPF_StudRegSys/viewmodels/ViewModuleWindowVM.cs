using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_StudRegSys.win;
using System.Windows;

namespace WPF_StudRegSys.viewmodels
{
    public partial class ViewModuleWindowVM : ObservableObject
    {
        [ObservableProperty]
        public string moduleName;
        [ObservableProperty]
        public string moduleCode;
        [ObservableProperty]
        public int credits;

        [ObservableProperty]
        public int idm;
        [ObservableProperty]
        public Module selectedModule;



        [ObservableProperty]
        ObservableCollection<Module> modules;

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


        [RelayCommand]
        public void InsertModule()
        {
            Module m = new Module()
            {
                ModuleName = moduleName,
                ModuleCode = moduleCode,
                Credits = credits
            };


            using (var db = new DatabaseContext())
            {
                db.Modules.Add(m);
                db.SaveChanges();
                LoadModule();
                ViewModuleWindow stud = new ViewModuleWindow();
                var win = Application.Current.MainWindow;
                Application.Current.MainWindow = stud;
                win.Close();
                stud.Show();
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

        public ViewModuleWindowVM()
        {
            LoadModule();
        }

        [RelayCommand]
        public void AddNewModule()
        {
            AddModuleWindow addmodule = new AddModuleWindow();
            
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow= addmodule;
            addmodule.Show();
        }

        [RelayCommand]
        public void DeleteModule()
        {
            if (SelectedModule != null)
            {

                Idm = SelectedModule.Id;
                Modules.Remove(SelectedModule);

                using (var db = new DatabaseContext())
                {
                    Module m = db.Modules.Find(Idm);

                    db.Modules.Remove(m);
                    db.SaveChanges();
                    LoadModule();
                    MessageBoxResult result = MessageBox.Show("Module was deleted succesfully!", "Done");
                }
            }
            else
            {
                MessageBox.Show("Please select a module first", "ERROR!");
            }
        }

        [RelayCommand]
        public void EditModule()
        {
            if (SelectedModule != null)
            {

                EditModuleWindow edit1 = new EditModuleWindow();
                edit1.DataContext = new EditModuleWindowVM(SelectedModule);
                

                Application.Current.MainWindow.Close();
                Application.Current.MainWindow= edit1;
                edit1.Show();

            }
            else
            {
                MessageBox.Show("Please select a module first", "ERROR!");
            }
        }
    }
}
