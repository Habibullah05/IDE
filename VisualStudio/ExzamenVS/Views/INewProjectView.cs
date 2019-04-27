using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExzamenVS.Models;

namespace ExzamenVS.Views
{
    public interface INewProjectView: IView
    {
        event EventHandler<EventArgs> OpenFolderEvent;
        event EventHandler<AddProjectEventArgs> AddProjectEvent;
    
        void SetPathFolde(string path);
    }

    public class AddProjectEventArgs : EventArgs
    {
        public Project project { get; set; }


    }


}
