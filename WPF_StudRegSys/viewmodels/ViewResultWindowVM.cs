using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_StudRegSys.win;

namespace WPF_StudRegSys.viewmodels
{
    public partial class ViewResultWindowVM : ObservableObject
    {
        [ObservableProperty]
        public int studentId;
        [ObservableProperty]
        public string moduleCode;
        [ObservableProperty]
        public string grade;

        [ObservableProperty]
        public int idr;
        [ObservableProperty]
        public Result selectedResult;

        [ObservableProperty]
        ObservableCollection<Result> results;

        [ObservableProperty]
        ObservableCollection<string> resOptions = new ObservableCollection<string> { "A+", "A", "A-", "B+", "B", "B-", "C+", "C-", "E" };


        

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
        public void InsertResult()

        {
            Result r = new Result()
            {
                StudentId = studentId,
                ModuleCode = moduleCode,
                Grade = grade
            };
            using (var db = new DatabaseContext()) {
                if (db.Students.Any(s => s.Id == StudentId) && db.Modules.Any(m=>m.ModuleCode==ModuleCode) )
                {


                    db.Results.Add(r);
                    db.SaveChanges();
                    CalcGpa(r.StudentId);
                    ViewResultWindow us = new ViewResultWindow();
                    us.Show();
                    Application.Current.MainWindow.Close();
                    Application.Current.MainWindow = us;
                }
                else
                {

                    MessageBox.Show("Invalid Student Id Or Module Code", "Error");
                }
            }
            
        }

        public void LoadResult()
        {
            using (var db = new DatabaseContext())
            {
                var list = db.Results.ToList();
                Results = new ObservableCollection<Result>(list);
            }
        }

        public ViewResultWindowVM()
        {
           
           
            LoadResult();

        }

        [RelayCommand]
        public void AddNewResult()
        {
            AddResultWindow addresult = new AddResultWindow();
            
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow= addresult;
            addresult.Show();
        }

        [RelayCommand]
        public void DeleteResult()
        {
            if (SelectedResult != null)
            {

                Idr = SelectedResult.Id;
                Results.Remove(SelectedResult);

                using (var db = new DatabaseContext())
                {
                    Result r = db.Results.Find(Idr);

                    db.Results.Remove(r);
                    CalcGpa(r.StudentId);
                    db.SaveChanges();
                    LoadResult();
                    
                    MessageBoxResult result = MessageBox.Show("Result was deleted succesfully!", "Done");
                }
            }
            else
            {
                MessageBox.Show("Please select a result first", "ERROR!");
            }
        }

        [RelayCommand]
        public void EditResult()
        {
            if (SelectedResult != null)
            {

                EditResultWindow edit1 = new EditResultWindow();
                edit1.DataContext = new EditResultWindowVM(SelectedResult);
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow= edit1;
                edit1.Show();

            }
            else
            {
                MessageBox.Show("Please select a result first", "ERROR!");
            }
        }


        public void CalcGpa(int id)
        {
            Dictionary<string, double> gpv = new Dictionary<string, double>
            {
                {"A+",4.0 },{"A",4.0},{"A-",3.7},{ "B+" ,3.3 },{"B",3.3},{"B-",2.7 },{"C+",2.3},{"C",2.0},{"C-",1.7}
            };
            using( var db = new DatabaseContext())
            {
                int modulecount = 0;
                int creditCount = 0;
                double gpa = 0; 

                Student stu =db.Students.Where(s=>s.Id==id).First();
                List<Result>r=db.Results.Where(s=>s.StudentId==id).ToList();
                List<Module> mod = db.Modules.ToList();
                
                foreach( var re in r)
                {
                    int credits = (int)re.ModuleCode[1];
                    creditCount += credits;

                    gpa = gpv[re.Grade] * credits;


                }
                 gpa/=creditCount;

                Student s = db.Students.Find(id);
                if(s != null){
                s.GPA= gpa;
                db.SaveChanges();

                
                }
            }

        }
    }
}
