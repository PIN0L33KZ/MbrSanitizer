using MbrSanitizer.Application;
using MbrSanitizer.Data;
using MbrSanitizer.Helper;
using MbrSanitizer.Services;
using System.Diagnostics;

namespace MbrSanitizer;

public partial class FRM_Main : Form
{
    private bool _projectSelected = false;

    //
    // Constructors
    //
    public FRM_Main(Project project)
    {
        InitializeComponent();
        Text = $"{Const.AppName} v{Const.AppVersion}";

        // Check if last known good template is filled in application settings
        if(project == null || project.Path == string.Empty)
            return;

        // Chek if last known good template still exists -> load it, else -> otherwise clear settings
        if(File.Exists(project.Path))
        {
            // Import Template and create object
            Template tmpTemplate = TemplatesManagerService.ImportTemplate(project.Path);

            // Fill UI with Template values
            FillUi(tmpTemplate);
            _projectSelected = true;
        }
        else
        {
            // Clear settings if file does not exist
            RecentFilesManager.DeleteFromRecentProjects(project);
        }
    }

    //
    // Event Handlers
    //
    private void BTN_ExportSettings_Click(object sender, EventArgs e)
    {
        // Check if project was selected
        if(!_projectSelected)
        {
            _ = MessageBox.Show("Bitte selektiere zu erst ein Projekt.", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Create Template object
        Template tmpTemplate = new()
        {
            ProjectPath = LBL_ProjectPath.Text,
            ValueShort = TBX_ValueShort.Text,
            ValueLong = TBX_ValueLong.Text,
            DeleteMbrFile = CHX_DeleteMbrFile.Checked,
            AntiDragImgs = CHX_AntiDragImgs.Checked,
            CustomComment = TBX_CustomComment.Text
        };

        try
        {
            // Sanitize Template
            tmpTemplate = TemplatesManagerService.SanitizeTemplate(tmpTemplate);

            // Check if Template is valid
            if(!InputSanitizer.IsValidTemplate(tmpTemplate))
            {
                _ = MessageBox.Show("Einige angaben sind nicht korrekt.", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create SaveFileDialog object to save the Template
            SaveFileDialog saveFileDialog = DialogCreator.CreateSaveFileDialog("Speichere deine Vorlage");

            // Abort if user cancels the SaveFileDialog
            if(saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            TemplatesManagerService.ExportTemplate(tmpTemplate, saveFileDialog.FileName);
        }
        catch(Exception ex)
        {
            _ = MessageBox.Show($"Vorlage kann nicht exportiert werden: {ex.Message}", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BTN_ImportSettings_Click(object sender, EventArgs e)
    {
        // Check if project was selected
        if(_projectSelected)
        {
            DialogResult userInput = MessageBox.Show("Möchtest du die aktuellen Eingaben überschreiben?", Const.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(userInput == DialogResult.No)
                return;
        }

        // Create OpenFileDialog object
        OpenFileDialog openFileDialog = DialogCreator.CreateOpenFileDialog("Wähle eine Vorlage zum Importieren aus");

        // Abort if user cancels the OpenFileDialog
        if(openFileDialog.ShowDialog() != DialogResult.OK)
            return;

        try
        {
            // Import Template and create object
            Template tmpTemplate = TemplatesManagerService.ImportTemplate(openFileDialog.FileName);

            // Fill UI with Template values
            FillUi(tmpTemplate);
            _projectSelected = true;

            // Add imported template to recent projects
            Project project = new() { Path = openFileDialog.FileName, Template = tmpTemplate };
            RecentFilesManager.AddToRecentProjects(project);
        }
        catch(Exception ex)
        {
            _ = MessageBox.Show($"Vorlage kann nicht importiert werden: {ex.Message}", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BTN_SelectProjectPath_Click(object sender, EventArgs e)
    {
        // Create FolderBrowserDialog object to select the project path
        FolderBrowserDialog folderBrowserDialog = DialogCreator.CreateFolderBrowserDialog("Wähle den Projektordner aus");

        if(folderBrowserDialog.ShowDialog() != DialogResult.OK)
            return;

        LBL_ProjectPath.Text = folderBrowserDialog.SelectedPath;

        _projectSelected = true;
    }

    private void BTN_DeleteAllFiles_Click(object sender, EventArgs e)
    {
        // Check if project was selected
        if(!_projectSelected)
        {
            _ = MessageBox.Show("Bitte selektiere zu erst ein Projekt.", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Ask user for confirmation
        if(MessageBox.Show("Möchtest du wirklich alle Dateien und Verzeichnise löschen?", Const.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        try
        {
            // Delete all files in the project directory
            FileManagerService.DeleteAllProjectFiles(LBL_ProjectPath.Text);

            _ = MessageBox.Show("Projekt Verzeichnis erfolgreich geleert.", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch(Exception ex)
        {
            _ = MessageBox.Show($"Verzeichnis konnte nicht geleert werden: {ex.Message}", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BTN_OpenProjectPath_Click(object sender, EventArgs e)
    {
        // Check if project was selected
        if(!_projectSelected)
        {
            _ = MessageBox.Show("Bitte selektiere zu erst ein Projekt.", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            // Create ProcessStartInfo object to start the file explorer with the project path
            ProcessStartInfo processStartInfo = new()
            {
                FileName = LBL_ProjectPath.Text,
                UseShellExecute = true
            };

            // Start the file explorer with the project path
            _ = Process.Start(processStartInfo);
        }
        catch(Exception ex)
        {
            _ = MessageBox.Show($"Projektpfad konnte nicht geöffnet werden: {ex.Message}", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BTN_StartSanitize_Click(object sender, EventArgs e)
    {
        // Check if project was selected
        if(!_projectSelected)
        {
            _ = MessageBox.Show("Bitte selektiere zu erst ein Projekt.", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Ask user for confirmation
        if(MessageBox.Show("Möchtest du den Sanitisierungsprozess starten?", Const.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        try
        {
            // Disable UI to prevent user interaction during sanitization
            EnableUi(false);

            // Normalize project path and sanitize input values
            var normalizedPath = Path.GetFullPath(LBL_ProjectPath.Text);
            var valueShort = InputSanitizer.SanitizeText(TBX_ValueShort.Text);
            var valueLong = InputSanitizer.SanitizeText(TBX_ValueLong.Text);

            // Sanitize project
            ProjectSanitizerService.SanitizeProject(
                normalizedPath,
                valueShort,
                valueLong,
                deleteMbrFile: CHX_DeleteMbrFile.Checked,
                antiDragImages: CHX_AntiDragImgs.Checked,
                customComment: TBX_CustomComment.Text
                );

            _ = MessageBox.Show("Projekt erfolgreich gesäubert.", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch(MobiriseProjectFileNotFoundException ex)
        {
            _ = MessageBox.Show($"{ex.Message}\nDeaktivere ggf. den Haken bei \"{CHX_DeleteMbrFile.Text}\"", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch(Exception ex)
        {
            _ = MessageBox.Show($"Projekt konnte nicht gesäubert werden: {ex.Message}", Const.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            // Enable UI
            EnableUi();
        }
    }

    //
    // Local helpers
    //
    private void FillUi(Template template)
    {
        LBL_ProjectPath.Text = template.ProjectPath;
        TBX_ValueShort.Text = template.ValueShort;
        TBX_ValueLong.Text = template.ValueLong;
        CHX_DeleteMbrFile.Checked = template.DeleteMbrFile;
        CHX_AntiDragImgs.Checked = template.AntiDragImgs;
        TBX_CustomComment.Text = template.CustomComment;
    }

    private void EnableUi(bool enable = true)
    {
        Enabled = enable;
    }
}