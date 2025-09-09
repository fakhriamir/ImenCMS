namespace ClientServices
{
    partial class Form1
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
            if (disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MeetingMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EventMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AutomationMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SapMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.تنطیماتToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon2 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIcon3 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "ثامن ارتباط عصر";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MeetingMenu,
            this.EventMenu,
            this.AutomationMenu,
            this.SapMenu,
            this.AboutMenu,
            this.ExitMenu,
            this.تنطیماتToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(171, 158);
            // 
            // MeetingMenu
            // 
            this.MeetingMenu.Name = "MeetingMenu";
            this.MeetingMenu.Size = new System.Drawing.Size(170, 22);
            this.MeetingMenu.Text = "مدیریت جلسات";
            this.MeetingMenu.Click += new System.EventHandler(this.MeetingMenu_Click);
            // 
            // EventMenu
            // 
            this.EventMenu.Name = "EventMenu";
            this.EventMenu.Size = new System.Drawing.Size(170, 22);
            this.EventMenu.Text = "مدیریت رویدادها";
            this.EventMenu.Click += new System.EventHandler(this.EventMenu_Click);
            // 
            // AutomationMenu
            // 
            this.AutomationMenu.Name = "AutomationMenu";
            this.AutomationMenu.Size = new System.Drawing.Size(170, 22);
            this.AutomationMenu.Text = "اتوماسیون اداری";
            this.AutomationMenu.Click += new System.EventHandler(this.AutomationMenu_Click);
            // 
            // SapMenu
            // 
            this.SapMenu.Name = "SapMenu";
            this.SapMenu.Size = new System.Drawing.Size(170, 22);
            this.SapMenu.Text = "سیستم حضور غیاب";
            this.SapMenu.Click += new System.EventHandler(this.SapMenu_Click);
            // 
            // AboutMenu
            // 
            this.AboutMenu.Name = "AboutMenu";
            this.AboutMenu.Size = new System.Drawing.Size(170, 22);
            this.AboutMenu.Text = "درباره";
            this.AboutMenu.Click += new System.EventHandler(this.AboutMenu_Click);
            // 
            // ExitMenu
            // 
            this.ExitMenu.Name = "ExitMenu";
            this.ExitMenu.Size = new System.Drawing.Size(170, 22);
            this.ExitMenu.Text = "بستن";
            this.ExitMenu.Click += new System.EventHandler(this.ExitMenu_Click);
            // 
            // تنطیماتToolStripMenuItem
            // 
            this.تنطیماتToolStripMenuItem.Name = "تنطیماتToolStripMenuItem";
            this.تنطیماتToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.تنطیماتToolStripMenuItem.Text = "تنطیمات";
            this.تنطیماتToolStripMenuItem.Click += new System.EventHandler(this.تنطیماتToolStripMenuItem_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ExitBtn.Location = new System.Drawing.Point(545, 0);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(30, 27);
            this.ExitBtn.TabIndex = 1;
            this.ExitBtn.Text = "X";
            this.ExitBtn.UseVisualStyleBackColor = true;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // notifyIcon2
            // 
            this.notifyIcon2.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon2.Text = "notifyIcon2";
            this.notifyIcon2.Visible = true;
            // 
            // notifyIcon3
            // 
            this.notifyIcon3.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon3.Text = "notifyIcon3";
            this.notifyIcon3.Visible = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ClientServices.Properties.Resources.Splash;
            this.ClientSize = new System.Drawing.Size(578, 387);
            this.Controls.Add(this.ExitBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MeetingMenu;
        private System.Windows.Forms.ToolStripMenuItem EventMenu;
        private System.Windows.Forms.ToolStripMenuItem AutomationMenu;
        private System.Windows.Forms.ToolStripMenuItem SapMenu;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.ToolStripMenuItem ExitMenu;
        private System.Windows.Forms.ToolStripMenuItem AboutMenu;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem تنطیماتToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon2;
        private System.Windows.Forms.NotifyIcon notifyIcon3;
    }
}

