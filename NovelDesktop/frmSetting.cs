using System;
using System.Drawing;
using System.Windows.Forms;

namespace NovelDesktop
{
    public partial class frmSetting : Form
    {
        #region Properties
        private Image selectedBackgroundImage;
        private Image previousBackgroundImage;

        private Font selectedFontTopBar;
        private Font selectedFontGroup;
        private Font previousFontTopBar;
        private Font previousFontGroup;

        private Color selectedColorTopBar;
        private Color selectedColorGroup;
        private Color selectedColorAppBar;

        private Color previousColorTopBar;
        private Color previousColorGroup;
        private Color previousColorAppBar;

        public Image SelectedBackgroundImage { get => selectedBackgroundImage; private set => selectedBackgroundImage = value; }
        public Font SelectedFontTopBar { get => selectedFontTopBar; private set => selectedFontTopBar = value; }
        public Font SelectedFontGroup { get => selectedFontGroup; set => selectedFontGroup = value; }
        public Color SelectedColorTopBar { get => selectedColorTopBar; set => selectedColorTopBar = value; }
        public Color SelectedColorAppBar { get => selectedColorAppBar; set => selectedColorAppBar = value; }
        public Color SelectedColorGroup { get => selectedColorGroup; set => selectedColorGroup = value; }

        #endregion

        #region Delegate

        // BACKGROUND IMAGE

        // Define delegate for function setting background image
        public delegate void PassBackgroundImage(Image sender);
        // Create instance (null)
        public PassBackgroundImage passBackGroundImage;

        // FONT
        // Define delegate for function setting font to Topbar
        public delegate void PassFontToTopbar(Font sender);
        // Create instance (null)
        public PassFontToTopbar passFontToTopbar;

        // Define delegate for function setting font to Topbar
        public delegate void PassFontToGroup(Font sender);
        // Create instance (null)
        public PassFontToGroup passFontToGroup;

        // COLOR
        // Define delegate for function setting color to Top bar
        public delegate void PassColorToTopBar(Color sender);
        // Create instance (null)
        public PassColorToTopBar passColorToTopBar;

        // Define delegate for function setting color to App bar
        public delegate void PassColorToAppBar(Color sender);
        // Create instance (null)
        public PassColorToAppBar passColorToAppBar;

        // Define delegate for function setting color to Group
        public delegate void PassColorToGroup(Color sender);
        // Create instance (null)
        public PassColorToGroup passColorToGroup;

        #endregion

        #region DLL Libraries
        //Import libraries to hook form events
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public frmSetting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mainFormBackgroundImage"></param>
        /// <param name="mainFormFontTopBar"></param>
        /// <param name="mainFormFontGroup"></param>
        /// <param name="mainFormColorTopBar"></param>
        /// <param name="mainFormColorAppBar"></param>
        /// <param name="mainFormColorGroup"></param>
        public frmSetting(Image mainFormBackgroundImage,
                            Font mainFormFontTopBar,
                            Font mainFormFontGroup,
                            Color mainFormColorTopBar,
                            Color mainFormColorAppBar,
                            Color mainFormColorGroup
                        )
        {
            InitializeComponent();

            this.previousBackgroundImage = mainFormBackgroundImage;
            this.previousFontTopBar = mainFormFontTopBar;
            this.previousFontGroup = mainFormFontGroup;
            this.previousColorTopBar = mainFormColorTopBar;
            this.previousColorAppBar = mainFormColorAppBar;
            this.previousColorGroup = mainFormColorGroup;
        }

        #endregion

        #region Form events

        /// <summary>
        /// frmSetting_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSetting_Load(object sender, EventArgs e)
        {
            panTheme.Dock = DockStyle.Fill;
            panFont.Visible = false;
            //btnFont.BackColor = Color.Red;
            btnTheme.BackColor = Color.FromArgb(0, 32, 56);
        }

        /// <summary>
        /// lblCaption_MouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblCaption_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        /// <summary>
        /// btnFont_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFont_Click(object sender, EventArgs e)
        {
            btnFont.BackColor = Color.FromArgb(0, 32, 56);
            btnTheme.BackColor = Color.Transparent;

            panTheme.Visible = false;
            panFont.Visible = true;
            panFont.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// btnTheme_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTheme_Click(object sender, EventArgs e)
        {
            btnFont.BackColor = Color.Transparent;
            btnTheme.BackColor = Color.FromArgb(0, 32, 56);

            panTheme.Visible = true;
            panFont.Visible = false;
        }

        /// <summary>
        /// btnTheme1_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTheme1_Click(object sender, EventArgs e)
        {
            this.SelectedBackgroundImage = btnTheme1.BackgroundImage;
            if (passBackGroundImage != null)
            {
                passBackGroundImage(this.SelectedBackgroundImage);
            }
        }

        /// <summary>
        /// btnTheme2_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTheme2_Click(object sender, EventArgs e)
        {
            this.SelectedBackgroundImage = btnTheme2.BackgroundImage;
            if (passBackGroundImage != null)
            {
                passBackGroundImage(this.SelectedBackgroundImage);
            }
        }

        /// <summary>
        /// btnTheme3_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTheme3_Click(object sender, EventArgs e)
        {
            this.SelectedBackgroundImage = btnTheme3.BackgroundImage;
            if (passBackGroundImage != null)
            {
                passBackGroundImage(this.SelectedBackgroundImage);
            }
        }

        /// <summary>
        /// btnTheme4_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTheme4_Click(object sender, EventArgs e)
        {
            this.SelectedBackgroundImage = btnTheme4.BackgroundImage;
            if (passBackGroundImage != null)
            {
                passBackGroundImage(this.SelectedBackgroundImage);
            }
        }

        /// <summary>
        /// btnOK_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // Background image
            if (this.selectedBackgroundImage == null)
            {
                this.selectedBackgroundImage = previousBackgroundImage;
            }

            // Font
            if (this.selectedFontTopBar == null)
            {
                this.selectedFontTopBar = previousFontTopBar;
            }
            if (this.selectedFontGroup == null)
            {
                this.selectedFontGroup = previousFontGroup;
            }

            // Color
            if (this.selectedColorTopBar == null)
            {
                this.selectedColorTopBar = previousColorTopBar;
            }
            if (this.selectedColorAppBar == null)
            {
                this.selectedColorAppBar = previousColorAppBar;
            }
            if (this.selectedColorGroup == null)
            {
                this.selectedColorGroup = previousColorGroup;
            }

            this.Close();
        }

        /// <summary>
        /// btnCancel_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.selectedBackgroundImage = previousBackgroundImage;
            this.selectedFontTopBar = previousFontTopBar;
            this.selectedFontGroup = previousFontGroup;

            this.selectedColorTopBar = previousColorTopBar;
            this.selectedColorAppBar = previousColorAppBar;
            this.selectedColorGroup = previousColorGroup;

            this.Close();
        }

        /// <summary>
        /// btnFontTopbar_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFontTopbar_Click(object sender, EventArgs e)
        {
            // Show the dialog.
            DialogResult result = fontDialog1.ShowDialog();

            // See if OK was pressed.
            if (result == DialogResult.OK)
            {
                // Get Font.
                this.selectedFontTopBar = fontDialog1.Font;
                if (passFontToTopbar != null)
                {
                    passFontToTopbar(this.selectedFontTopBar);
                }
            }
        }

        /// <summary>
        /// btnFontGroup_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFontGroup_Click(object sender, EventArgs e)
        {
            // Show the dialog.
            DialogResult result = fontDialog1.ShowDialog();

            // See if OK was pressed.
            if (result == DialogResult.OK)
            {
                // Get Font.
                this.selectedFontGroup = fontDialog1.Font;
                if (passFontToGroup != null)
                {
                    passFontToGroup(this.selectedFontGroup);
                }
            }
        }

        /// <summary>
        /// btnColorCaption_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColorCaption_Click(object sender, EventArgs e)
        {
            // Show the dialog.
            DialogResult result = colorDialog1.ShowDialog();

            // See if OK was pressed.
            if (result == DialogResult.OK)
            {
                // Get Font.
                this.selectedColorTopBar = colorDialog1.Color;
                if (this.passColorToTopBar != null)
                {
                    passColorToTopBar(this.selectedColorTopBar);
                }
            }
        }

        /// <summary>
        /// btnColorAppBar_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColorAppBar_Click(object sender, EventArgs e)
        {
            // Show the dialog.
            DialogResult result = colorDialog1.ShowDialog();

            // See if OK was pressed.
            if (result == DialogResult.OK)
            {
                // Get Font.
                this.selectedColorAppBar = colorDialog1.Color;
                if (this.passColorToAppBar != null)
                {
                    passColorToAppBar(this.selectedColorAppBar);
                }
            }

        }

        /// <summary>
        /// btnColorGroup_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColorGroup_Click(object sender, EventArgs e)
        {
            // Show the dialog.
            DialogResult result = colorDialog1.ShowDialog();

            // See if OK was pressed.
            if (result == DialogResult.OK)
            {
                // Get Font.
                this.selectedColorGroup = colorDialog1.Color;
                if (this.passColorToGroup != null)
                {
                    passColorToGroup(this.selectedColorGroup);
                }
            }
        }

        #endregion

    }
}
