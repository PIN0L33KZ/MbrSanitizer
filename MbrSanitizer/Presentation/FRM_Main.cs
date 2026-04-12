using MbrSanitizer.Data;
using MbrSanitizer.Services;
using System.Diagnostics;

namespace MbrSanitizer;

public partial class FRM_Main : Form
{
    private readonly string _appName = "Mobirise Sanitizer";
    private bool _projectSelected = false;

    public FRM_Main()
    {
        InitializeComponent();
    }

    private void BTN_ExportSettings_Click(object sender, EventArgs e)
    {
        // Check if project was selected
        if(!_projectSelected)
        {
            _ = MessageBox.Show("Bitte selektiere zu erst ein Projekt.", _appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // Sanitize Template
        tmpTemplate = InputSanitizer.SanitizeTemplate(tmpTemplate);

        // Check if Template is valid
        if(!InputSanitizer.IsValidTemplate(tmpTemplate))
        {
            _ = MessageBox.Show("Einige angaben sind nicht korrekt.", _appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Create SaveFileDialog object
        SaveFileDialog saveFileDialog = new()
        {
            FileName = "Vorlage",
            Filter = "JSON files (*.json)|*.json",
            Title = "Öffne deine Vorlagen Datei"
        };

        // Abort if user cancels the SaveFileDialog
        if(saveFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        // Export Template
        TemplateExporter.ExportTemplate(tmpTemplate, saveFileDialog.FileName);
    }

    private void BTN_ImportSettings_Click(object sender, EventArgs e)
    {
        // Check if project was selected
        if(_projectSelected)
        {
            DialogResult userInput = MessageBox.Show("Möchtest du die aktuellen Eingaben überschreiben?", _appName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(userInput == DialogResult.No)
            {
                return;
            }
        }

        // Create OpenFileDialog object
        OpenFileDialog openFileDialog = new()
        {
            Filter = "JSON files (*.json)|*.json",
            Title = "Öffne deine Vorlagen Datei"
        };

        // Abort if user cancels the OpenFileDialog
        if(openFileDialog.ShowDialog() != DialogResult.OK)
            return;

        // Import Template and create object
        Template tmpTemplate = TemplateImporter.ImportTemplate(openFileDialog.FileName);

        FillUi(tmpTemplate);
        _projectSelected = true;
    }

    private void FillUi(Template template)
    {
        LBL_ProjectPath.Text = template.ProjectPath;
        TBX_ValueShort.Text = template.ValueShort;
        TBX_ValueLong.Text = template.ValueLong;
        CHX_DeleteMbrFile.Checked = template.DeleteMbrFile;
        CHX_AntiDragImgs.Checked = template.AntiDragImgs;
        TBX_CustomComment.Text = template.CustomComment;
    }

    private void BTN_SelectProjectPath_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog folderBrowserDialog = new()
        {
            Description = "Wähle den Pfad zu deinem Mobirise Projektordner aus.",
            UseDescriptionForTitle = true
        };

        if(folderBrowserDialog.ShowDialog() != DialogResult.OK)
            return;

        LBL_ProjectPath.Text = folderBrowserDialog.SelectedPath;
        _projectSelected = true;
    }

    private void BTN_DeleteAllFiles_Click(object sender, EventArgs e)
    {
        if(!_projectSelected)
        {
            _ = MessageBox.Show("Bitte selektiere zu erst ein Projekt.", _appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if(MessageBox.Show("Möchtest du wirklich alle Dateien und Verzeichnise löschen?", _appName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        FilesManager.DeleteAllProjectFiles(LBL_ProjectPath.Text);

        _ = MessageBox.Show("Projekt Verzeichnis erfolgreich geleert.", _appName, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void BTN_OpenProjectPath_Click(object sender, EventArgs e)
    {
        if(!_projectSelected)
        {
            _ = MessageBox.Show("Bitte selektiere zu erst ein Projekt.", _appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            ProcessStartInfo processStartInfo = new()
            {
                FileName = LBL_ProjectPath.Text,
                UseShellExecute = true
            };

            _ = Process.Start(processStartInfo);
        }
        catch(Exception ex)
        {
            _ = MessageBox.Show($"Fehler beim Öffnen des Projektpfads: {ex.Message}", _appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BTN_StartSanitize_Click(object sender, EventArgs e)
    {
        if(!_projectSelected)
        {
            _ = MessageBox.Show("Bitte selektiere zu erst ein Projekt.", _appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if(MessageBox.Show("Möchtest du den Sanitisierungsprozess starten?", _appName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        try
        {
            var normalizedPath = Path.GetFullPath(LBL_ProjectPath.Text);
            var valueShort = InputSanitizer.SanitizeText(TBX_ValueShort.Text);
            var valueLong = InputSanitizer.SanitizeText(TBX_ValueLong.Text);

            // Sanitize project
            ProjectSanitizer.SanitizeProject(
                normalizedPath,
                valueShort,
                valueLong,
                deleteMbrFile: CHX_DeleteMbrFile.Checked,
                antiDragImages: CHX_AntiDragImgs.Checked,
                customComment: TBX_CustomComment.Text
                );

            _ = MessageBox.Show("Projekt erfolgreich gesäubert.", _appName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch(Exception ex)
        {
            _ = MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}", _appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}