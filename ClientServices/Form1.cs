using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using ClientServices;
using System.Collections;

namespace ClientServices
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer mytim = new Timer();
            mytim.Tick += new EventHandler(DoCheckEvent);
            mytim.Interval = 1000;
            mytim.Enabled = true;
            mytim.Start();         
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
           // this.Hide();
        }
        private void DoCheckEvent(object sender, EventArgs e)
        {
            com.b.www.Services mys = new com.b.www.Services();
            try
            {
            object[] aa = mys.GetMeeting("4513", "nem");
           
                //if (aa[0].ToString() != "")
                //{
                    notifyIcon1.BalloonTipText = aa[0].ToString();
                    notifyIcon1.BalloonTipTitle = "یادآوری جلسه";
                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon1.ShowBalloonTip(1000);
                //}

                //if (!string.IsNullOrEmpty(aa[1].ToString()))
                //{
                    notifyIcon2.BalloonTipText = aa[1].ToString();
                    notifyIcon2.BalloonTipTitle = "یادآوری رویداد";
                    notifyIcon2.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon2.ShowBalloonTip(1000);
                //}
                //if (!string.IsNullOrEmpty(aa[2].ToString()))
                //{
                    notifyIcon3.BalloonTipText = aa[2].ToString();
                    notifyIcon3.BalloonTipTitle = "یادآوری یادداشت";
                    notifyIcon3.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon3.ShowBalloonTip(1000);
                //}
               // else { 
                    notifyIcon1.Dispose(); notifyIcon2.Dispose(); notifyIcon3.Dispose();
            //}
            }
            catch (System.IndexOutOfRangeException ex)  // CS0168
            {
                System.Console.WriteLine(ex.Message);
                // Set IndexOutOfRangeException to the new exception's InnerException. 
                throw new System.ArgumentOutOfRangeException("index parameter is out of range.", ex);
            }
            
        }
        string GetDefaultBrowserPath()
        {
            string key = @"http\shell\open\command";
            RegistryKey registryKey =
            Registry.ClassesRoot.OpenSubKey(key, false);
            return ((string)registryKey.GetValue(null, null)).Split('"')[1];
        }

        private void EventMenu_Click(object sender, EventArgs e)
        {
            Process.Start(GetDefaultBrowserPath(), "http://portal.samenea.com/Meeting/EventCalendar.aspx");
        }

        private void MeetingMenu_Click(object sender, EventArgs e)
        {
            Process.Start(GetDefaultBrowserPath(), "http://portal.samenea.com/Meeting/MeetingCalendar.aspx");
        }

        private void AutomationMenu_Click(object sender, EventArgs e)
        {
            Process.Start("iExplore.exe", " http://automation.samenea.com/menu/");
           
        }

        private void SapMenu_Click(object sender, EventArgs e)
        {
            Process.Start("iExplore.exe", " http://erp.samenea.com:8000/nwbc/?sap-nwbc-node=root&sap-client=109&sap-language=AR&sap-rtl=X");
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            
            this.Visible = !this.Visible; 
           
        }
        private void AboutMenu_Click(object sender, EventArgs e)
        {
            this.Opacity = 100;
            this.Show();
        }
        private void ExitMenu_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void تنطیماتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting MyS = new Setting();
            MyS.Show();
        }

    }
}
