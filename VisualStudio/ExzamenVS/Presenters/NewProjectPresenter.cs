using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExzamenVS.Views;
using ExzamenVS.Models;
using ExzamenVS.Services;

namespace ExzamenVS.Presenters
{
    class NewProjectPresenter
    {
        INewProjectView newProjectView;
        IProjectService projectService;
        public Project project = new Project();
      
        public IView View { get { return newProjectView; } }

        public NewProjectPresenter(INewProjectView newProjectView, IProjectService projectService)
        {
            this.newProjectView = newProjectView;
            this.projectService = projectService;
            
            EventSubscription();
        }

        private void EventSubscription()
        {
            newProjectView.OpenFolderEvent += NewProjectView_OpenFolderEvent;
            newProjectView.AddProjectEvent += NewProjectView_AddProjectEvent;
        }

        private void NewProjectView_AddProjectEvent(object sender, AddProjectEventArgs e)
        {
          
            projectService.CreatingProjectFolder(e.project);
           this.project= e.project;
        }

        private void NewProjectView_OpenFolderEvent(object sender, EventArgs e)
        {
            newProjectView.SetPathFolde(projectService.GetPahtFolder());
        }
    }
}
