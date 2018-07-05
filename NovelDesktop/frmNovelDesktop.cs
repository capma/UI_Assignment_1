using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NovelDesktop
{
    public partial class frmNovelDesktop : Form
    {

        #region Properties
        private Point MouseDownLocation;

        private int panMostUsed_Top;
        private int panMostUsed_Left;
        private int panGaming_Top;
        private int panGaming_Left;

        private Image mainFormBackGround;

        public Image MainFormBackGround { get => mainFormBackGround; private set => mainFormBackGround = value; }

        private Font fontGroup;

        #endregion

        #region DLL Libraries for displaying apps within parent form
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        private static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        // Define the FindWindow API function.
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        //private static int GWL_STYLE = -16;
        //private static int WS_CHILD = 0x40000000;


        #endregion

        #region Constructor
        public frmNovelDesktop()
        {
            InitializeComponent();

            // Event for buttons in [Most Used] group
            btnCalculator_MostUsed.DoubleClick += new EventHandler(btnCalculator_MostUsed_DoubleClick);
            btnDOS_MostUsed.DoubleClick += new EventHandler(btnDOS_MostUsed_DoubleClick);
            btnNotepad_MostUsed.DoubleClick += new EventHandler(btnNotepad_MostUsed_DoubleClick);
            btnCharmap_MostUsed.DoubleClick += new EventHandler(btnCharmap_MostUsed_DoubleClick);
            btnWrite_MostUsed.DoubleClick += new EventHandler(btnWrite_MostUsed_DoubleClick);
            btnSoundVol_MostUsed.DoubleClick += new EventHandler(btnSoundVol_MostUsed_DoubleClick);
            btnControl_MostUsed.DoubleClick += new EventHandler(btnControl_MostUsed_DoubleClick);
            btnPaint_MostUsed.DoubleClick += new EventHandler(btnPaint_MostUsed_DoubleClick);

            // Event for buttons in [Gaming] group
            btnSilentHill_Gaming.DoubleClick += new EventHandler(btnSilentHill_Gaming_DoubleClick);
            btnResidentEvil_Gaming.DoubleClick += new EventHandler(btnResidentEvil_Gaming_DoubleClick);
            btnDoom_Gaming.DoubleClick += new EventHandler(btnDoom_Gaming_DoubleClick);
            btnCounterStrike_Gaming.DoubleClick += new EventHandler(btnCounterStrike_Gaming_DoubleClick);
            btnStarcraft_Gaming.DoubleClick += new EventHandler(btnStarcraft_Gaming_DoubleClick);
            btnWarcraft_Gaming.DoubleClick += new EventHandler(btnWarcraft_Gaming_DoubleClick);
            btnAOE_Gaming.DoubleClick += new EventHandler(btnAOE_Gaming_DoubleClick);
            btnDota_Gaming.DoubleClick += new EventHandler(btnDota_Gaming_DoubleClick);
        }

        #endregion

        #region Form methods

        /// <summary>
        /// LoadApplication
        /// </summary>
        /// <param name="path"></param>
        /// <param name="handle"></param>
        private void LoadApplication(string path, IntPtr handle)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //int timeout = 10 * 1000;     // Timeout value (10s) in case we want to cancel the task if it's taking too long.

            try
            {
                panPopupWindowStart.Visible = false;

                Process p = Process.Start(path);
                //Thread.Sleep(500);
                //p.WaitForInputIdle();

                while (!IsWindowVisible(p.MainWindowHandle) || p.MainWindowHandle == IntPtr.Zero)
                {
                    System.Threading.Thread.Sleep(10);
                    p.Refresh();
                }
                // Set the process parent window to the window we want
                SetParent(p.MainWindowHandle, handle);

                //// Place the window in the top left of the parent window without resizing it
                //SetWindowPos(p.MainWindowHandle, 0, 0, 0, 0, 0, 0x0001 | 0x0040);

            }
            catch (Exception ex)
            {
                //throw;
                //MessageBox.Show(ex.Message);
                //return;
            }
              
        }


        /// <summary>
        /// PassBackgroundImage
        /// </summary>
        /// <param name="sender"></param>
        private void PassBackgroundImage(Image sender)
        {
            // Set de text of the textbox to the value of the textbox of form 2
            this.BackgroundImage = sender;
        }

        /// <summary>
        /// PassFontToAppBar
        /// </summary>
        /// <param name="sender"></param>
        private void PassFontToAppBar(Font sender)
        {
            lblDateTime.Font = sender;
        }

        /// <summary>
        /// SetFontToGroup
        /// </summary>
        /// <param name="sender"></param>
        private void SetFontToGroup(Font sender)
        {
            lblCalculator_MostUsed.Font = sender;
            lblDOS_MostUsed.Font = sender;
            lblNotepad_MostUsed.Font = sender;
            lblCharmap_MostUsed.Font = sender;
            lblWrite_MostUsed.Font = sender;
            lblSoundVol_MostUsed.Font = sender;
            lblControl_MostUsed.Font = sender;
            lblPaint_MostUsed.Font = sender;
            lblSilentHill_Gaming.Font = sender;
            lblResidentEvil_Gaming.Font = sender;
            lblDoom_Gaming.Font = sender;
            lblCounterStrike_Gaming.Font = sender;
            lblStarcraft_Gaming.Font = sender;
            lblWarcraft_Gaming.Font = sender;
            lblAOE_Gaming.Font = sender;
            lblDota_Gaming.Font = sender;
        }

        /// <summary>
        /// PassFontToGroup
        /// </summary>
        /// <param name="sender"></param>
        private void PassFontToGroup(Font sender)
        {
            fontGroup = sender;

            SetFontToGroup(sender);
        }

        /// <summary>
        /// PassColorToTopBar
        /// </summary>
        /// <param name="sender"></param>
        private void PassColorToTopBar(Color sender)
        {
            panTopBar.BackColor = Color.FromArgb(25, sender);
        }

        /// <summary>
        /// PassColorToAppBar
        /// </summary>
        /// <param name="sender"></param>
        private void PassColorToAppBar(Color sender)
        {
            tlpWindowBar.BackColor = Color.FromArgb(50, sender);
        }

        /// <summary>
        /// SetColorToGroup
        /// </summary>
        /// <param name="sender"></param>
        private void SetColorToGroup(Color sender)
        {
            // Most Used group
            lblMostUsed_Grp.BackColor = Color.FromArgb(100, sender);
            tlpMostUsed_Body.BackColor = Color.FromArgb(70, sender);

            // Gaming group
            lblGaming_Grp.BackColor = Color.FromArgb(100, sender);
            tlpGaming_Body.BackColor = Color.FromArgb(70, sender);
        }

        /// <summary>
        /// PassColorToGroup
        /// </summary>
        /// <param name="sender"></param>
        private void PassColorToGroup(Color sender)
        {
            SetColorToGroup(sender);
        }

        #endregion

        #region Form events

        /// <summary>
        /// frmNovelDesktop_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNovelDesktop_Load(object sender, EventArgs e)
        {
            // Display Date and time in Top bar
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            string s = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone).ToString("dd MMM yyyy hh:mm tt");
            lblDateTime.Text = s;

            // Save ccurrent location of group of Most Used and Gaming
            panMostUsed_Top = panMostUsed.Top;
            panMostUsed_Left = panMostUsed.Left;
            panGaming_Top = panGaming.Top;
            panGaming_Left = panGaming.Left;

            // Save current background image
            MainFormBackGround = this.BackgroundImage;

            // Popup app
            panPopupWindowStart.BackColor = Color.FromArgb(100, Color.FromArgb(0, 32, 56));

            // Top bar
            panTopBar.BackColor = Color.FromArgb(25, Color.FromArgb(0, 32, 56));

            // App bar
            tlpWindowBar.BackColor = Color.FromArgb(50, Color.FromArgb(0, 32, 56));

            // Group of Most Used
            lblMostUsed_Grp.BackColor = Color.FromArgb(100, Color.Black);
            tlpMostUsed_Body.BackColor = Color.FromArgb(70, Color.Black);

            // Group of Gaming
            lblGaming_Grp.BackColor = Color.FromArgb(100, Color.Black);
            tlpGaming_Body.BackColor = Color.FromArgb(70, Color.Black);

            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button at the Top bar
            toolTip1.SetToolTip(this.btnSetting, "Setting");

            // Set up the ToolTip text for the Button at the Bottom bar
            toolTip1.SetToolTip(this.btnCalculator_Bottom, "Calculator");
            toolTip1.SetToolTip(this.btnNotepad_Bottom, "Notepad");
            toolTip1.SetToolTip(this.btnDOS_Bottom, "Command DOS");
            toolTip1.SetToolTip(this.btnPaint_Bottom, "Ms Paint");
            toolTip1.SetToolTip(this.btnWrite_Bottom, "Write");
            toolTip1.SetToolTip(this.btnCharmap_Bottom, "Charmap");
            toolTip1.SetToolTip(this.btnSoundVol_Bottom, "Sound Volume");
            toolTip1.SetToolTip(this.btnControl_Bottom, "Control Panel");

            toolTip1.SetToolTip(this.btnSilentHill_Bottom, "Silent Hill");
            toolTip1.SetToolTip(this.btnResidentEvil_Bottom, "Resident Evil");
            toolTip1.SetToolTip(this.btnDoom_Bottom, "Doom 3");
            toolTip1.SetToolTip(this.btnCounterStrike_Bottom, "Counter Strike 1.6");
            toolTip1.SetToolTip(this.btnStarcraft_Bottom, "Starcraft");
            toolTip1.SetToolTip(this.btnWarcraft_Bottom, "Warcraft");
            toolTip1.SetToolTip(this.btnAOE_Bottom, "Age of Empire");
            toolTip1.SetToolTip(this.btnDota_Bottom, "Dota");

            toolTip1.SetToolTip(this.btnWindow, "Start");
            toolTip1.SetToolTip(this.btnShutdown, "Shut down");

        }

        /// <summary>
        /// btnWindow_Click_1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWindow_Click_1(object sender, EventArgs e)
        {

            panPopupWindowStart.SuspendLayout();

            //btnMostUsed.BackColor = Color.Red;
            panGaming.SendToBack();

            //// panPopupWindowStart1
            //panPopupWindowStart1.BringToFront();
            //panPopupWindowStart1.Left = tlpWindowBar.Left;
            //panPopupWindowStart1.Top = this.Height - 5 - panPopupWindowStart1.Height - panBottom.Height - tlpWindowBar.Height;
            //panPopupWindowStart1.Visible = !(panPopupWindowStart1.Visible);

            // panPopupWindowStart
            panPopupWindowStart.BringToFront();
            panPopupWindowStart.Left = tlpWindowBar.Left;
            panPopupWindowStart.Top = this.Height - 5 - panPopupWindowStart.Height - panBottom.Height - tlpWindowBar.Height;
            panPopupWindowStart.Visible = !(panPopupWindowStart.Visible);
            panPopupWindowStart.ResumeLayout();


            //string coord = string.Empty;
            //coord += "Main form" + Environment.NewLine;
            //coord += "  Top: " + this.Top + Environment.NewLine;
            //coord += "  Bottom: " + this.Bottom + Environment.NewLine;
            //coord += "  Height: " + this.Height + Environment.NewLine;
            //coord += "panPopupWindowStart" + Environment.NewLine;
            //coord += "  Top: " + panPopupWindowStart.Top + Environment.NewLine;
            //coord += "  Bottom: " + panPopupWindowStart.Bottom + Environment.NewLine;
            //coord += "  Height: " + panPopupWindowStart.Height + Environment.NewLine;
            //coord += "tlpWindowBar" + Environment.NewLine;
            //coord += "  Top: " + tlpWindowBar.Top + Environment.NewLine;
            //coord += "  Bottom: " + tlpWindowBar.Bottom + Environment.NewLine;
            //coord += "  Height: " + tlpWindowBar.Height + Environment.NewLine;
            //coord += "btnWindow" + Environment.NewLine;
            //coord += "  Top: " + btnWindow.Top + Environment.NewLine;
            //coord += "  Bottom: " + btnWindow.Bottom + Environment.NewLine;
            //coord += "  Height: " + btnWindow.Height + Environment.NewLine;
            //label1.Text = coord;
        }

        /// <summary>
        /// frmNovelDesktop_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNovelDesktop_Click(object sender, EventArgs e)
        {
            panPopupWindowStart.Visible = false;
        }

        /// <summary>
        /// frmNovelDesktop_MouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNovelDesktop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                panPopupWindowStart.Visible = false;
            }
        }


        private void tmrRefreshDateTime_Tick(object sender, EventArgs e)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            string s = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone).ToString("dd MMM yyyy hh:mm tt");
            lblDateTime.Text = s;
        }

        private void btnShutdown_Click_1(object sender, EventArgs e)
        {
            panPopupWindowStart.Visible = false;

            if (MessageBox.Show("Do you want to close the Novel Desktop?",
                                                    "Novel Desktop",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question,
                                                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Close();
            }

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            panPopupWindowStart.Visible = false;
            tlpMostUsed_Grp.Enabled = false;
            tlpMostUsed_Body.Enabled = false;
            tlpGaming_Grp.Enabled = false;
            tlpGaming_Body.Enabled = false;

            fontGroup = lblCalculator_MostUsed.Font;

            frmSetting frm = new frmSetting(this.BackgroundImage, 
                                                lblDateTime.Font, 
                                                fontGroup, 
                                                panTopBar.BackColor,
                                                tlpWindowBar.BackColor,
                                                lblMostUsed_Grp.BackColor
                                                );

            // Create an instance of the delegate
            frm.passBackGroundImage = new frmSetting.PassBackgroundImage(PassBackgroundImage);
            frm.passFontToTopbar = new frmSetting.PassFontToTopbar(PassFontToAppBar);
            frm.passFontToGroup = new frmSetting.PassFontToGroup(PassFontToGroup);
            frm.passColorToTopBar = new frmSetting.PassColorToTopBar(PassColorToTopBar);
            frm.passColorToAppBar = new frmSetting.PassColorToAppBar(PassColorToAppBar);
            frm.passColorToGroup = new frmSetting.PassColorToGroup(PassColorToGroup);

            frm.ShowDialog();

            this.BackgroundImage = frm.SelectedBackgroundImage;
            lblDateTime.Font= frm.SelectedFontTopBar;
            SetFontToGroup(frm.SelectedFontGroup);
            panTopBar.BackColor = Color.FromArgb(25, frm.SelectedColorTopBar);
            tlpWindowBar.BackColor = Color.FromArgb(50, frm.SelectedColorAppBar);
            SetColorToGroup(frm.SelectedColorGroup);

            tlpMostUsed_Grp.Enabled = true;
            tlpMostUsed_Body.Enabled = true;
            tlpGaming_Grp.Enabled = true;
            tlpGaming_Body.Enabled = true;
        }

        private void lblMostUsed_Grp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void lblMostUsed_Grp_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                panMostUsed.SuspendLayout();
                panMostUsed.Left = e.X + panMostUsed.Left - MouseDownLocation.X;
                panMostUsed.Top = e.Y + panMostUsed.Top - MouseDownLocation.Y;
                panMostUsed.SuspendLayout();
            }
        }

        private void lblGaming_Grp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void lblGaming_Grp_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                panGaming.SuspendLayout();
                panGaming.Left = e.X + panGaming.Left - MouseDownLocation.X;
                panGaming.Top = e.Y + panGaming.Top - MouseDownLocation.Y;
                panGaming.SuspendLayout();
            }
        }


        #region Apps at the Popup
        
        private void btnAOE_Popup_Click(object sender, EventArgs e)
        {
            panPopupWindowStart.Visible = false;
            LoadApplication(@"C:\Program Files (x86)\Windows Media Player\wmplayer.exe", this.Handle);
        }

        private void btnCalculator_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\calc", this.Handle);
        }

        private void btnCharmap_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\charmap", this.Handle);
        }

        private void btnControl_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\control", this.Handle);
        }

        private void btnCounterStrike_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\taskmgr", this.Handle);
        }

        private void btnDoom_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\xpsrchvw", this.Handle);
        }

        private void btnDOS_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\cmd", this.Handle);
        }

        private void btnDota_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"C:\Windows\System32\WindowsPowerShell\v1.0\powershell", this.Handle);
        }

        private void btnNotepad_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\notepad", this.Handle);
        }

        private void btnPaint_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\mspaint", this.Handle);
        }

        private void btnResidentEvil_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\psr", this.Handle);
        }

        private void btnSilentHill_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\mstsc", this.Handle);
        }

        private void btnSoundVol_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\SndVol", this.Handle);
        }

        private void btnStarcraft_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"C:\Windows\System32\Speech\SpeechUX", this.Handle);
        }

        private void btnWarcraft_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"C:\Program Files\windows nt\accessories\wordpad.exe", this.Handle);
        }

        private void btnWrite_Popup_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\write", this.Handle);
        }

        #endregion

        #region Apps at Bottom bar
        

        private void btnCalculator_Bottom_Click_1(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\calc.exe", this.Handle);
        }

        private void btnDoom_Bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\xpsrchvw", this.Handle);
        }

        private void btnCharmap_Bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\charmap", this.Handle);
        }

        private void btnCounterStrike_Bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\taskmgr", this.Handle);
        }

        private void btnControl_Bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\control", this.Handle);
        }

        private void btnStarcraft_Bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"C:\Windows\System32\Speech\SpeechUX", this.Handle);
        }

        private void btnNotepad_Bottom_Click_1(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\notepad", this.Handle);
        }

        private void btnWarcraft_Bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"C:\Program Files\windows nt\accessories\wordpad.exe", this.Handle);
        }

        private void btnPaint_Bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\mspaint", this.Handle);
        }

        private void btnAOE_Bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"C:\Program Files (x86)\Windows Media Player\wmplayer.exe", this.Handle);
        }

        private void btnSoundVol_Bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\SndVol", this.Handle);
        }

        private void btnDota_Bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"C:\Windows\System32\WindowsPowerShell\v1.0\powershell", this.Handle);
        }

        private void btnWrite_Bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\write", this.Handle);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panPopupWindowStart.Visible = false;

            panMostUsed.Top = panMostUsed_Top;
            panMostUsed.Left = panMostUsed_Left;

            panGaming.Top = panGaming_Top;
            panGaming.Left = panGaming_Left;

        }

        private void btnCalculator_Bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\mstsc", this.Handle);
        }

        private void btnNotepad_bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\psr", this.Handle);
        }

        private void btnDOS_Bottom_Click(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\cmd", this.Handle);
        }

        #endregion

        #region Apps in the [Most Used] and [Gaming] group

        // Event double click for buttons in the [Most Used] group
        void btnCalculator_MostUsed_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\calc.exe", this.Handle);
        }
        void btnDOS_MostUsed_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\cmd", this.Handle);
        }
        void btnNotepad_MostUsed_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\notepad", this.Handle);
        }
        void btnCharmap_MostUsed_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\charmap", this.Handle);
        }
        void btnWrite_MostUsed_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\write", this.Handle);
        }
        void btnSoundVol_MostUsed_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\SndVol", this.Handle);
        }
        void btnControl_MostUsed_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\control", this.Handle);
        }
        void btnPaint_MostUsed_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\mspaint", this.Handle);
        }

        // Event double click for buttons in the [Gaming] group
        void btnSilentHill_Gaming_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\mstsc", this.Handle);
        }
        void btnResidentEvil_Gaming_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\psr", this.Handle);
        }
        void btnDoom_Gaming_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\xpsrchvw", this.Handle);
        }
        void btnCounterStrike_Gaming_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"c:\windows\system32\taskmgr", this.Handle);
        }
        void btnStarcraft_Gaming_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"C:\Windows\System32\Speech\SpeechUX", this.Handle);
        }
        void btnWarcraft_Gaming_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"C:\Program Files\windows nt\accessories\wordpad.exe", this.Handle);
        }
        void btnAOE_Gaming_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"C:\Program Files (x86)\Windows Media Player\wmplayer.exe", this.Handle);
        }
        void btnDota_Gaming_DoubleClick(object sender, EventArgs e)
        {
            LoadApplication(@"C:\Windows\System32\WindowsPowerShell\v1.0\powershell", this.Handle);
        }
        #endregion

        #endregion

    }
}
