using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WPF_StudRegSys
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        //upon startup, this will check whether the Database file is created and if it doesn't exist, the file will be created
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    DatabaseFacade facade = new DatabaseFacade(new UserDataContext());
        //    facade.EnsureCreated();
        //}
    }
}
