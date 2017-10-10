using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf.Pager.LazyLoading.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnAppStartUp(object sender, StartupEventArgs e)
        {
            var vw = new MainWindow();
            var vm = new MainWindowVm().LoadContent();
            vw.DataContext = vm;
            vw.ShowDialog();
        }
    }
}
