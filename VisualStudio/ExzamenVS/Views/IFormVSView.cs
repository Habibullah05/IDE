using ExzamenVS.Models;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExzamenVS.Views
{
    public interface IFormVSView: IView
    {
        event EventHandler<EventArgs> NewProjectEvent;
        event EventHandler<CSEventArgs> AddPageEvent;
        event EventHandler<CSEventArgs> RunEvent;
        event EventHandler<CSEventArgs> BuildEvent;
        event EventHandler<EventArgs> AddCSEvent;
        event EventHandler<OpenFileEventArgs> OpenProjectEvent;
        event EventHandler<OpenFileEventArgs> OpenFileEvent;
        event EventHandler<SerealizationEventArgs> SerealizationEvent;
        event EventHandler<CSEventArgs> SaveFileEvent;
        event EventHandler<RemoveFileEventArgs> RemoveFileEvent;

        void AddPage(System.Windows.Forms.TabPage tabPage);
        void SetProject(Project project);
        void AddCS(CS cS);
        void SetErrors(CompilerErrorCollection errorCollection);
    }
    public class CSEventArgs: EventArgs
    {
        public CS cS { get; set; }


    }

    public class RemoveFileEventArgs  : EventArgs
    {
        public Project project { get; set; } = new Project();
        public CS cS { get; set; }
    }

    public class OpenFileEventArgs : EventArgs
    {
        public string path { get; set; }
    }

    public class SerealizationEventArgs: EventArgs
    {
        public Project project { get; set; } = new Project();
    }
}
