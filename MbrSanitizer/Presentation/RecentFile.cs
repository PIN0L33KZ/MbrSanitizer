using MbrSanitizer.Application.EventArguments;
using MbrSanitizer.Data;

namespace MbrSanitizer.Presentation;

public partial class RecentFile : UserControl
{
    public EventHandler<ProjectEventArgs>? FileClicked;
    private readonly Project _project;

    public RecentFile(Project project)
    {
        InitializeComponent();

        RegisterEvents(this);

        _project = project;

        // Fill Ui
        LBL_FileName.Text = _project.Path.Split("\\")[^1];
        LBL_FilePath.Text = _project.Path;
    }

    private void FileClickedHandler(object? sender, EventArgs e)
    {
        FileClicked?.Invoke(this, new ProjectEventArgs(_project));
    }

    // Local helpers
    private void HoverStart(object? sender, EventArgs e)
    {
        // Update Ui to indicate active hover
        PNL_Background.BorderColor = Color.FromArgb(194, 165, 142);
    }

    private void HoverEnd(object? sender, EventArgs e)
    {
        // Update Ui to indicate inactive hover
        PNL_Background.BorderColor = Color.FromArgb(103, 99, 99);
    }

    private void RegisterEvents(Control Parent)
    {
        foreach(Control ctrl in Parent.Controls)
        {
            ctrl.MouseEnter += HoverStart;
            ctrl.MouseLeave += HoverEnd;
            ctrl.Click += FileClickedHandler;

            // Check if control as children
            if(ctrl.HasChildren)
                // Run recursively for children
                RegisterEvents(ctrl);
        }
    }
}