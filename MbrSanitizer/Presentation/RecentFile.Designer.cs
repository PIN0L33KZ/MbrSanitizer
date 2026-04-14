namespace MbrSanitizer.Presentation;

partial class RecentFile
{
    /// <summary> 
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
        if(disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Vom Komponenten-Designer generierter Code

    /// <summary> 
    /// Erforderliche Methode für die Designerunterstützung. 
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        PNL_Background = new Guna.UI2.WinForms.Guna2Panel();
        LBL_FilePath = new Label();
        LBL_FileName = new Label();
        PBX_FileIcon = new PictureBox();
        PNL_Background.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)PBX_FileIcon).BeginInit();
        SuspendLayout();
        // 
        // PNL_Background
        // 
        PNL_Background.Anchor = AnchorStyles.None;
        PNL_Background.BorderColor = Color.FromArgb(103, 99, 99);
        PNL_Background.BorderRadius = 15;
        PNL_Background.BorderThickness = 2;
        PNL_Background.Controls.Add(LBL_FilePath);
        PNL_Background.Controls.Add(LBL_FileName);
        PNL_Background.Controls.Add(PBX_FileIcon);
        PNL_Background.CustomizableEdges = customizableEdges1;
        PNL_Background.FillColor = Color.FromArgb(58, 55, 55);
        PNL_Background.Font = new Font("Leelawadee UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        PNL_Background.ForeColor = Color.FromArgb(235, 234, 234);
        PNL_Background.Location = new Point(40, 4);
        PNL_Background.Name = "PNL_Background";
        PNL_Background.ShadowDecoration.CustomizableEdges = customizableEdges2;
        PNL_Background.Size = new Size(383, 66);
        PNL_Background.TabIndex = 0;
        // 
        // LBL_FilePath
        // 
        LBL_FilePath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        LBL_FilePath.AutoEllipsis = true;
        LBL_FilePath.Font = new Font("Leelawadee UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        LBL_FilePath.ForeColor = Color.FromArgb(235, 234, 234);
        LBL_FilePath.Location = new Point(48, 30);
        LBL_FilePath.Name = "LBL_FilePath";
        LBL_FilePath.Size = new Size(319, 21);
        LBL_FilePath.TabIndex = 1;
        LBL_FilePath.Text = "File Path";
        // 
        // LBL_FileName
        // 
        LBL_FileName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        LBL_FileName.AutoEllipsis = true;
        LBL_FileName.Font = new Font("Leelawadee UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
        LBL_FileName.ForeColor = Color.FromArgb(194, 165, 142);
        LBL_FileName.Location = new Point(48, 9);
        LBL_FileName.Name = "LBL_FileName";
        LBL_FileName.Size = new Size(319, 21);
        LBL_FileName.TabIndex = 1;
        LBL_FileName.Text = "File Name";
        // 
        // PBX_FileIcon
        // 
        PBX_FileIcon.Anchor = AnchorStyles.Left;
        PBX_FileIcon.Image = Properties.Resources.json_file;
        PBX_FileIcon.Location = new Point(12, 18);
        PBX_FileIcon.Name = "PBX_FileIcon";
        PBX_FileIcon.Size = new Size(30, 30);
        PBX_FileIcon.SizeMode = PictureBoxSizeMode.AutoSize;
        PBX_FileIcon.TabIndex = 0;
        PBX_FileIcon.TabStop = false;
        // 
        // RecentFile
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Transparent;
        Controls.Add(PNL_Background);
        Cursor = Cursors.Hand;
        Name = "RecentFile";
        Size = new Size(463, 74);
        PNL_Background.ResumeLayout(false);
        PNL_Background.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)PBX_FileIcon).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Guna.UI2.WinForms.Guna2Panel PNL_Background;
    private PictureBox PBX_FileIcon;
    private Label LBL_FileName;
    private Label LBL_FilePath;
}
