using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ExzamenVS.Views;
using ExzamenVS.Services;
using ExzamenVS.Presenters;


namespace ExzamenVS
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region IOC setting
            IOC.Regisrter<FormVS, IFormVSView>();
            IOC.Regisrter<NewProjectForm, INewProjectView>();
            IOC.Regisrter<ProjectService, IProjectService>();
            IOC.Regisrter<FormVSPresenter>();
            IOC.Regisrter<NewProjectPresenter>();
            IOC.Build();
            #endregion
            FormVSPresenter formVS = IOC.Resolve<FormVSPresenter>();
            Application.Run((Form)formVS.View);
        }
    }
}
