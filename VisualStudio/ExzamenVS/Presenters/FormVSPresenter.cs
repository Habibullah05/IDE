using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExzamenVS.Views;
using ExzamenVS.Services;

namespace ExzamenVS.Presenters
{
    class FormVSPresenter
    {
        private IFormVSView form_VS;
       // private INewProjectView newProject;
        private IProjectService projectService;
        public IView View { get { return form_VS; } }

        public FormVSPresenter(IFormVSView form_VS, IProjectService projectService)
        {
            this.form_VS = form_VS;
            this.projectService = projectService;
          
            EventSubscription();
        }

        private void EventSubscription()
        {
            form_VS.NewProjectEvent += Form_VS_NewProjectEvent;
            form_VS.AddPageEvent += Form_VS_AddPageEvent;
            form_VS.RunEvent += Form_VS_RunEvent;
            form_VS.OpenProjectEvent += Form_VS_OpenProjectEvent1;
            form_VS.AddCSEvent += Form_VS_AddCSEvent;
            form_VS.SerealizationEvent += Form_VS_SerealizationEvent;
            form_VS.OpenFileEvent += Form_VS_OpenFileEvent;
            form_VS.SaveFileEvent += Form_VS_SaveFileEvent;
            form_VS.BuildEvent += Form_VS_BuildEvent1;
            form_VS.RemoveFileEvent += Form_VS_RemoveFileEvent;
        }

        private void Form_VS_RemoveFileEvent(object sender, RemoveFileEventArgs e)
        {
            var tmp = projectService.RemoveFile(e.project, e.cS);
            projectService.ProjectSerialization(tmp);
            form_VS.SetProject(tmp);
            
        }

        private void Form_VS_BuildEvent1(object sender, CSEventArgs e)
        {
          form_VS.SetErrors(projectService.Build(e.cS));
        }

       

        private void Form_VS_SaveFileEvent(object sender, CSEventArgs e)
        {
            projectService.SaveFile(e.cS);
        }

        private void Form_VS_OpenFileEvent(object sender, OpenFileEventArgs e)
        {
            form_VS.AddCS(projectService.OpenFile(e.path));
        }

        private void Form_VS_SerealizationEvent(object sender, SerealizationEventArgs e)
        {
            projectService.ProjectSerialization(e.project);
        }

        private void Form_VS_OpenProjectEvent1(object sender, OpenFileEventArgs e)
        {
            form_VS.SetProject(projectService.OpenProject(e.path));
        }

      

        private void Form_VS_AddCSEvent(object sender, EventArgs e)
        {
           
            form_VS.AddCS(projectService.CreateCS());
        }

        private void Form_VS_RunEvent(object sender, CSEventArgs e)
        {
            projectService.Run(e.cS);
        }

        private void Form_VS_AddPageEvent(object sender, CSEventArgs e)
        {
            form_VS.AddPage(projectService.AddPage(e.cS));
        }

        private void Form_VS_NewProjectEvent(object sender, EventArgs e)
        {
            NewProjectPresenter newProject = IOC.Resolve<NewProjectPresenter>();
            if (newProject.View.ShowDialog())
            {
                form_VS.SetProject(newProject.project);
            }
        }
    }
}
