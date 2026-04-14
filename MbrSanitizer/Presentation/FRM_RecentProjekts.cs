using MbrSanitizer.Application;
using MbrSanitizer.Application.EventArguments;
using MbrSanitizer.Data;
using MbrSanitizer.Helper;
using MbrSanitizer.Services;

namespace MbrSanitizer.Presentation;

public partial class FRM_RecentProjekts : Form
{
    //
    // Constructor
    //
    public FRM_RecentProjekts()
    {
        InitializeComponent();

        // Fill the Ui
        FillUi();
    }

    //
    // Event Handlers
    //
    private void FileClicked(object? sender, ProjectEventArgs e)
    {
        // Check if file still exists, if not fill the Ui again to remove the non-existend file from the list of recent projects
        if(FileManagerService.DoesFileExist(e.Project.Path))
            LaunchMainForm(e.Project);
        else
            FillUi();
    }

    private void BTN_ImportTemplate_Click(object sender, EventArgs e)
    {
        // Create OpenFileDialog object
        OpenFileDialog openFileDialog = DialogCreator.CreateOpenFileDialog("Wähle eine Vorlage zum Importieren aus");

        // Abort if user cancels the OpenFileDialog
        if(openFileDialog.ShowDialog() != DialogResult.OK)
            return;

        try
        {
            // Import Template and create object
            Template tmpTemplate = TemplatesManagerService.ImportTemplate(openFileDialog.FileName);

            // Add imported template to recent projects
            Project project = new() { Path = openFileDialog.FileName, Template = tmpTemplate };
            RecentFilesManager.AddToRecentProjects(project);

            // Launch mainform with imported template
            LaunchMainForm(project);
        }
        catch(Exception ex)
        {
            _ = MessageBox.Show($"Vorlage kann nicht importiert werden: {ex.Message}", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BTN_NewTemplate_Click(object sender, EventArgs e)
    {
        LaunchMainForm(new Project() { Path = "", Template = TemplatesManagerService.GetDefaultTemplate() });
    }

    private void BTN_ExitApplication_Click(object sender, EventArgs e)
    {
        Environment.Exit(0);
    }

    //
    // Local helpers
    //
    private void FillUi()
    {
        // Remove non-existend files from recent projects list
        RecentFilesManager.ClearRecentProjects();

        // Get the current list of recent projects
        List<Project>? recentProjects = RecentFilesManager.GetRecentProjects();

        // If no recent projects are found, display a message and return Method
        if(recentProjects.Count == 0)
        {
            Label LBL_NoRecentProjects = new()
            {
                Text = "Keine kürzlich geöffneten Projekte gefunden",
                Font = new Font("Leelawadee UI", 11, FontStyle.Regular),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            PNL_RecentProjects.Controls.Add(LBL_NoRecentProjects);
            return;
        }

        // Clear the panel before filling it again
        PNL_RecentProjects.Controls.Clear();

        // Process each project
        foreach(Project project in recentProjects)
        {
            // Add a new RecentFile control for each project to the panel and subscribe to click events
            RecentFile recentFile = new(project) { Dock = DockStyle.Top };
            recentFile.FileClicked += FileClicked;
            PNL_RecentProjects.Controls.Add(recentFile);
        }
    }

    private void LaunchMainForm(Project project)
    {
        // Hide this window
        Hide();

        // Fill the Ui
        FillUi();

        // Launch mainform with the selected project
        FRM_Main mainForm = new(project);
        _ = mainForm.ShowDialog();

        Environment.Exit(0);
    }
}