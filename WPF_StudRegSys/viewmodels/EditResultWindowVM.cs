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
    public partial class EditResultWindowVM : ObservableObject
    {
        [ObservableProperty]
        public int studentId;
        [ObservableProperty]
        public string moduleCode;
        [ObservableProperty]
        public string grade;

        [ObservableProperty]
        public int ide;

        [ObservableProperty]
        ObservableCollection<string> resOptions = new ObservableCollection<string> { "A+", "A", "A-", "B+", "B", "B-", "C+", "C-", "E" };

        [ObservableProperty]
        ObservableCollection<Result> results;

        [ObservableProperty]
        public Result selectedResult;

        [RelayCommand]
        public void UpdateResult()
        {


            using (var db = new DatabaseContext())
            {
                Result r = db.Results.Find(ide);
                if (r != null)
                {
                    r.StudentId = studentId;
                    r.ModuleCode = moduleCode;
                    r.Grade = grade;
                    db.SaveChanges();
                    LoadResult();
                    CalcGpa(r.StudentId);
                    MessageBox.Show("Result edited successfully!", "Done");
                    ViewResultWindow stud = new ViewResultWindow();
                    var win = Application.Current.MainWindow;
                    Application.Current.MainWindow = stud;
                    win.Close();
                    stud.Show();

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
        public EditResultWindowVM(Result r)
        {
            StudentId = r.StudentId;
            ModuleCode = r.ModuleCode;
            Grade = r.Grade;

            Ide = r.Id;
            LoadResult();
        }

        public EditResultWindowVM()
        {

        }


        public void CalcGpa(int id)
        {
            Dictionary<string, double> gpv = new Dictionary<string, double>
            {
                {"A+",4.0 },{"A",4.0},{"A-",3.7},{ "B+" ,3.3 },{"B",3.3},{"B-",2.7 },{"C+",2.3},{"C",2.0},{"C-",1.7}
            };
            using (var db = new DatabaseContext())
            {
                int modulecount = 0;
                int creditCount = 0;
                double gpa = 0;

                Student stu = db.Students.Where(s => s.Id == id).First();
                List<Result> r = db.Results.Where(s => s.StudentId == id).ToList();
                List<Module> mod = db.Modules.ToList();

                foreach (var re in r)
                {
                    int credits = (int)re.ModuleCode[1];
                    creditCount += credits;

                    gpa = gpv[re.Grade] * credits;


                }
                gpa /= creditCount;

                Student s = db.Students.Find(id);
                if (s != null)
                {
                    s.GPA = gpa;
                    db.SaveChanges();


                }
            }
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
