using MbrSanitizer.Data;

namespace MbrSanitizer.Application.EventArguments;

public class ProjectEventArgs : EventArgs
{
    public Project Project { get; }

    public ProjectEventArgs(Project project)
    {
        Project = project;
    }
}