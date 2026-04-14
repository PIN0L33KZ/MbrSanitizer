namespace MbrSanitizer.Presentation;

partial class FRM_RecentProjekts
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if(disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_RecentProjekts));
        PNL_Top = new Panel();
        LBL_FormHeading = new Label();
        BTN_ExitApplication = new Guna.UI2.WinForms.Guna2Button();
        BTN_ImportTemplate = new Guna.UI2.WinForms.Guna2Button();
        BTN_NewTemplate = new Guna.UI2.WinForms.Guna2Button();
        guna2vScrollBar1 = new Guna.UI2.WinForms.Guna2VScrollBar();
        PNL_RecentProjects = new Panel();
        PNL_Top.SuspendLayout();
        SuspendLayout();
        // 
        // PNL_Top
        // 
        PNL_Top.Controls.Add(LBL_FormHeading);
        PNL_Top.Dock = DockStyle.Top;
        PNL_Top.Location = new Point(0, 0);
        PNL_Top.Name = "PNL_Top";
        PNL_Top.Size = new Size(465, 91);
        PNL_Top.TabIndex = 0;
        // 
        // LBL_FormHeading
        // 
        LBL_FormHeading.AutoSize = true;
        LBL_FormHeading.Font = new Font("Leelawadee UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        LBL_FormHeading.Location = new Point(152, 30);
        LBL_FormHeading.Name = "LBL_FormHeading";
        LBL_FormHeading.Size = new Size(160, 30);
        LBL_FormHeading.TabIndex = 1;
        LBL_FormHeading.Text = "Letzte Projekte";
        // 
        // BTN_ExitApplication
        // 
        BTN_ExitApplication.Animated = true;
        BTN_ExitApplication.AutoRoundedCorners = true;
        BTN_ExitApplication.BackColor = Color.Transparent;
        BTN_ExitApplication.BorderColor = Color.Transparent;
        BTN_ExitApplication.Cursor = Cursors.Hand;
        BTN_ExitApplication.CustomizableEdges = customizableEdges1;
        BTN_ExitApplication.DisabledState.BorderColor = Color.DarkGray;
        BTN_ExitApplication.DisabledState.CustomBorderColor = Color.DarkGray;
        BTN_ExitApplication.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
        BTN_ExitApplication.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
        BTN_ExitApplication.FillColor = Color.FromArgb(162, 123, 90);
        BTN_ExitApplication.Font = new Font("Leelawadee UI", 11.25F, FontStyle.Bold);
        BTN_ExitApplication.ForeColor = Color.White;
        BTN_ExitApplication.Image = Properties.Resources.exit;
        BTN_ExitApplication.Location = new Point(291, 434);
        BTN_ExitApplication.Name = "BTN_ExitApplication";
        BTN_ExitApplication.ShadowDecoration.CustomizableEdges = customizableEdges2;
        BTN_ExitApplication.Size = new Size(109, 32);
        BTN_ExitApplication.TabIndex = 11;
        BTN_ExitApplication.Text = "Beenden";
        BTN_ExitApplication.Click += BTN_ExitApplication_Click;
        // 
        // BTN_ImportTemplate
        // 
        BTN_ImportTemplate.Animated = true;
        BTN_ImportTemplate.AutoRoundedCorners = true;
        BTN_ImportTemplate.BackColor = Color.Transparent;
        BTN_ImportTemplate.BorderColor = Color.Transparent;
        BTN_ImportTemplate.Cursor = Cursors.Hand;
        BTN_ImportTemplate.CustomizableEdges = customizableEdges3;
        BTN_ImportTemplate.DisabledState.BorderColor = Color.DarkGray;
        BTN_ImportTemplate.DisabledState.CustomBorderColor = Color.DarkGray;
        BTN_ImportTemplate.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
        BTN_ImportTemplate.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
        BTN_ImportTemplate.FillColor = Color.FromArgb(162, 123, 90);
        BTN_ImportTemplate.Font = new Font("Leelawadee UI", 11.25F, FontStyle.Bold);
        BTN_ImportTemplate.ForeColor = Color.White;
        BTN_ImportTemplate.Image = Properties.Resources.import;
        BTN_ImportTemplate.Location = new Point(64, 434);
        BTN_ImportTemplate.Name = "BTN_ImportTemplate";
        BTN_ImportTemplate.ShadowDecoration.CustomizableEdges = customizableEdges4;
        BTN_ImportTemplate.Size = new Size(130, 32);
        BTN_ImportTemplate.TabIndex = 11;
        BTN_ImportTemplate.Text = "Importieren";
        BTN_ImportTemplate.Click += BTN_ImportTemplate_Click;
        // 
        // BTN_NewTemplate
        // 
        BTN_NewTemplate.Animated = true;
        BTN_NewTemplate.AutoRoundedCorners = true;
        BTN_NewTemplate.BackColor = Color.Transparent;
        BTN_NewTemplate.BorderColor = Color.Transparent;
        BTN_NewTemplate.Cursor = Cursors.Hand;
        BTN_NewTemplate.CustomizableEdges = customizableEdges5;
        BTN_NewTemplate.DisabledState.BorderColor = Color.DarkGray;
        BTN_NewTemplate.DisabledState.CustomBorderColor = Color.DarkGray;
        BTN_NewTemplate.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
        BTN_NewTemplate.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
        BTN_NewTemplate.FillColor = Color.FromArgb(162, 123, 90);
        BTN_NewTemplate.Font = new Font("Leelawadee UI", 11.25F, FontStyle.Bold);
        BTN_NewTemplate.ForeColor = Color.White;
        BTN_NewTemplate.Image = Properties.Resources.link;
        BTN_NewTemplate.Location = new Point(200, 434);
        BTN_NewTemplate.Name = "BTN_NewTemplate";
        BTN_NewTemplate.ShadowDecoration.CustomizableEdges = customizableEdges6;
        BTN_NewTemplate.Size = new Size(85, 32);
        BTN_NewTemplate.TabIndex = 11;
        BTN_NewTemplate.Text = "Neu";
        BTN_NewTemplate.Click += BTN_NewTemplate_Click;
        // 
        // guna2vScrollBar1
        // 
        guna2vScrollBar1.BackColor = Color.Transparent;
        guna2vScrollBar1.BindingContainer = PNL_RecentProjects;
        guna2vScrollBar1.BorderRadius = 5;
        guna2vScrollBar1.FillColor = Color.FromArgb(58, 55, 55);
        guna2vScrollBar1.InUpdate = false;
        guna2vScrollBar1.LargeChange = 10;
        guna2vScrollBar1.Location = new Point(435, 97);
        guna2vScrollBar1.Name = "guna2vScrollBar1";
        guna2vScrollBar1.ScrollbarSize = 18;
        guna2vScrollBar1.Size = new Size(18, 331);
        guna2vScrollBar1.TabIndex = 0;
        guna2vScrollBar1.ThumbColor = Color.FromArgb(80, 76, 76);
        // 
        // PNL_RecentProjects
        // 
        PNL_RecentProjects.AutoScroll = true;
        PNL_RecentProjects.Location = new Point(12, 97);
        PNL_RecentProjects.Name = "PNL_RecentProjects";
        PNL_RecentProjects.Size = new Size(441, 331);
        PNL_RecentProjects.TabIndex = 12;
        // 
        // FRM_RecentProjekts
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(38, 34, 34);
        ClientSize = new Size(465, 478);
        Controls.Add(guna2vScrollBar1);
        Controls.Add(PNL_RecentProjects);
        Controls.Add(BTN_NewTemplate);
        Controls.Add(BTN_ImportTemplate);
        Controls.Add(BTN_ExitApplication);
        Controls.Add(PNL_Top);
        Font = new Font("Lexend", 12F);
        ForeColor = Color.FromArgb(235, 234, 234);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        FormScreenCaptureMode = ScreenCaptureMode.HideContent;
        Icon = (Icon)resources.GetObject("$this.Icon");
        Margin = new Padding(4, 5, 4, 5);
        MaximizeBox = false;
        Name = "FRM_RecentProjekts";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Zuletzt genutzte Projekte";
        PNL_Top.ResumeLayout(false);
        PNL_Top.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel PNL_Top;
    private Label LBL_FormHeading;
    private Guna.UI2.WinForms.Guna2Button BTN_ExitApplication;
    private Guna.UI2.WinForms.Guna2Button BTN_ImportTemplate;
    private Guna.UI2.WinForms.Guna2Button BTN_NewTemplate;
    private Guna.UI2.WinForms.Guna2VScrollBar guna2vScrollBar1;
    private Panel PNL_RecentProjects;
}