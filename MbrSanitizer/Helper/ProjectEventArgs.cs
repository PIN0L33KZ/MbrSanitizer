using MbrSanitizer.Data;

namespace MbrSanitizer.Helper;

public class ProjectEventArgs : EventArgs
{
    public Project Project { get; }

    public ProjectEventArgs(Project project)
    {
        Project = project;
    }
}