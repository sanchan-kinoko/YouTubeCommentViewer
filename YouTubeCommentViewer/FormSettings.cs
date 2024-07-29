using System.Reflection;
using Microsoft.Web.WebView2.Core;
using YouTubeCommentViewer.Properties;

namespace YouTubeCommentViewer
{
    public partial class FormSettings : Form
    {
        Form CloseForm;

#nullable disable warnings
        public FormSettings(Form closeForm)
#nullable restore
        {
            InitializeComponent();
            InitializeComponentCustum();

            Text += string.Format(" [Ver.{0}]", Assembly.GetExecutingAssembly().GetName().Version);

#nullable disable warnings
            webView2HowTo.CoreWebView2InitializationCompleted += WebView2InitializationCompleted;
#nullable restore
            webView2HowTo.EnsureCoreWebView2Async(null);

            CloseForm = closeForm;
        }
        private void WebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                webView2HowTo.CoreWebView2.Navigate("https://sanchanoffice.com/youtubecommentviewer/#howto");
            }
        }
        private void FormSettings_Load(object sender, EventArgs e)
        {
            SettingControls();
            SettingCommonEvent();
            InitializeControls();
            CloseForm.Close();
        }
        private void buttonDefaultSetting_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            RemoveCommonEvent();
            SettingControls();
            SettingCommonEvent();
            InitializeControls();
            Properties.Settings.Default.Save();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string liveId = textBoxLiveId.Text.Trim();
            if (string.IsNullOrEmpty(liveId))
            {
                MessageBox.Show("Live ID を入力してください。\nPlease enter your Live ID.", "警告/Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Visible = false;
            new FormCommentViewer(liveId).ShowDialog(this);
            Visible = true;
        }

        private void FormSettings_Resize(object sender, EventArgs e) { dgvExample.ResizeColumnWidth(); }

        /// <summary>
        /// 各コントロールの共通イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Common_Event(object sender, EventArgs e)
        {
            SetSettings();
            InitializeControls();
            SettingControls();
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 各コントロールに共通イベントを紐づけ
        /// </summary>
        private void SettingCommonEvent()
        {
#nullable disable warnings
            fontComboBox.SelectedIndexChanged += Common_Event;
            nudFontSize.ValueChanged += Common_Event;
            colorComboBoxFore.SelectedIndexChanged += Common_Event;
            colorComboBoxSelectionFore.SelectedIndexChanged += Common_Event;
            colorComboBoxBack.SelectedIndexChanged += Common_Event;
            checkBoxDisplayGrid.CheckStateChanged += Common_Event;
            colorComboBoxGrid.SelectedIndexChanged += Common_Event;
            checkBoxDisplayTimeStamp.CheckStateChanged += Common_Event;
            cityComboBox.SelectedIndexChanged += Common_Event;
            checkBoxDisplayUserName.CheckStateChanged += Common_Event;
            checkBoxValidUserType.CheckStateChanged += Common_Event;
            colorComboBoxModeratorFore.SelectedIndexChanged += Common_Event;
            colorComboBoxMemberFore.SelectedIndexChanged += Common_Event;
            checkBoxDisplayUserIcon.CheckStateChanged += Common_Event;
            checkBoxSuperChatColor.CheckStateChanged += Common_Event;
#nullable restore
        }

        private void RemoveCommonEvent()
        {
#nullable disable warnings
            fontComboBox.SelectedIndexChanged -= Common_Event;
            nudFontSize.ValueChanged -= Common_Event;
            colorComboBoxFore.SelectedIndexChanged -= Common_Event;
            colorComboBoxSelectionFore.SelectedIndexChanged -= Common_Event;
            colorComboBoxBack.SelectedIndexChanged -= Common_Event;
            checkBoxDisplayGrid.CheckStateChanged -= Common_Event;
            colorComboBoxGrid.SelectedIndexChanged -= Common_Event;
            checkBoxDisplayTimeStamp.CheckStateChanged -= Common_Event;
            cityComboBox.SelectedIndexChanged -= Common_Event;
            checkBoxDisplayUserName.CheckStateChanged -= Common_Event;
            checkBoxValidUserType.CheckStateChanged -= Common_Event;
            colorComboBoxModeratorFore.SelectedIndexChanged -= Common_Event;
            colorComboBoxMemberFore.SelectedIndexChanged -= Common_Event;
            checkBoxDisplayUserIcon.CheckStateChanged -= Common_Event;
            checkBoxSuperChatColor.CheckStateChanged -= Common_Event;
#nullable restore
        }
        /// <summary>
        /// 設定をコントロールに反映＆チェックボックスと使用可否子コントロールの紐づけ
        /// </summary>
        /// <param name="s"></param>
        private void SettingControls()
        {
            //設定読み込み
            fontComboBox.SelectedFont = Properties.Settings.Default.FontName;
            nudFontSize.Value = Properties.Settings.Default.FontSize;
            colorComboBoxFore.SelectedColor = Properties.Settings.Default.ForeColor;
            colorComboBoxSelectionFore.SelectedColor = Properties.Settings.Default.SelectionForeColor;
            colorComboBoxBack.SelectedColor = Properties.Settings.Default.BackColor;
            checkBoxDisplayGrid.Checked = Properties.Settings.Default.VisibleGrid;
            colorComboBoxGrid.SelectedColor = Properties.Settings.Default.GridColor;
            checkBoxDisplayTimeStamp.Checked = Properties.Settings.Default.VisibleTimeStamp;
            cityComboBox.SelectedCity = Properties.Settings.Default.City;
            checkBoxDisplayUserName.Checked = Properties.Settings.Default.VisibleUserName;
            checkBoxValidUserType.Checked = Properties.Settings.Default.EnableChangeUserName;
            colorComboBoxModeratorFore.SelectedColor = Properties.Settings.Default.ModeratorColor;
            colorComboBoxMemberFore.SelectedColor = Properties.Settings.Default.MemberColor;
            checkBoxDisplayUserIcon.Checked = Properties.Settings.Default.VisibleUserIcon;
            checkBoxSuperChatColor.Checked = Properties.Settings.Default.EnableSuperChatColor;

            //チェックボックスと使用可否子コントロールの紐づけ
            checkBoxDisplayGrid.EnableRelationControl = colorComboBoxGrid;
            checkBoxDisplayTimeStamp.EnableRelationControl = cityComboBox;
            checkBoxDisplayUserName.EnableRelationControl = groupBoxUserName;
            checkBoxValidUserType.EnableRelationControl = panelUserType;
        }
        private void SetSettings()
        {
            Properties.Settings.Default.FontName = fontComboBox.SelectedFont;
            Properties.Settings.Default.FontSize = (int)nudFontSize.Value;
            Properties.Settings.Default.ForeColor = colorComboBoxFore.SelectedColor;
            Properties.Settings.Default.SelectionForeColor = colorComboBoxSelectionFore.SelectedColor;
            Properties.Settings.Default.BackColor = colorComboBoxBack.SelectedColor;
            Properties.Settings.Default.VisibleGrid = checkBoxDisplayGrid.Checked;
            Properties.Settings.Default.GridColor = colorComboBoxGrid.SelectedColor;
            Properties.Settings.Default.VisibleTimeStamp = checkBoxDisplayTimeStamp.Checked;
            Properties.Settings.Default.City = cityComboBox.SelectedCity;
            Properties.Settings.Default.SelectedTimeDifference = cityComboBox.SelectedTimeDifference;
            Properties.Settings.Default.VisibleUserName = checkBoxDisplayUserName.Checked;
            Properties.Settings.Default.EnableChangeUserName = checkBoxValidUserType.Checked;
            Properties.Settings.Default.ModeratorColor = colorComboBoxModeratorFore.SelectedColor;
            Properties.Settings.Default.MemberColor = colorComboBoxMemberFore.SelectedColor;
            Properties.Settings.Default.VisibleUserIcon = checkBoxDisplayUserIcon.Checked;
            Properties.Settings.Default.EnableSuperChatColor = checkBoxSuperChatColor.Checked;
        }
        private void InitializeControls()
        {
            dgvExample.VisibleColumn(ColumnType.TimeStamp, Properties.Settings.Default.VisibleTimeStamp);
            dgvExample.VisibleColumn(ColumnType.UserIcon, Properties.Settings.Default.VisibleUserIcon);
            dgvExample.VisibleColumn(ColumnType.UserName, Properties.Settings.Default.VisibleUserName);

            DataGridViewCellBorderStyle borderStyle;
            if (Properties.Settings.Default.VisibleGrid) { borderStyle = DataGridViewCellBorderStyle.Single; }
            else { borderStyle = DataGridViewCellBorderStyle.None; }
            dgvExample.CellBorderStyle = borderStyle;

            dgvExample.DefaultCellStyle.Font = new Font(Properties.Settings.Default.FontName, Properties.Settings.Default.FontSize);

            //色設定
            dgvExample.ForeColor = Properties.Settings.Default.ForeColor;
            dgvExample.SetSelectionForeColor(Properties.Settings.Default.SelectionForeColor);
            dgvExample.SetBackColor(Properties.Settings.Default.BackColor);

            //グリッド線の色設定
            dgvExample.GridColor = Properties.Settings.Default.GridColor;

            InitializeDataGridView();
            dgvExample.ResizeColumnWidth();
        }
        private void InitializeDataGridView()
        {
            dgvExample.Rows.Clear();
            var now = DateTime.UtcNow.AddHours(Properties.Settings.Default.SelectedTimeDifference);
            int iconSize = Properties.Settings.Default.FontSize + 10;
            var icon1 = ImageConvertor.CircleBitmap(Resources.ex1, iconSize);
            var icon2 = ImageConvertor.CircleBitmap(Resources.ex2, iconSize);

            int newIndex = 0;
            newIndex = dgvExample.Rows.Add(now.ToString("H:mm"), icon1, "User A", "Hi:)");
            if (Properties.Settings.Default.EnableChangeUserName)
            {
                dgvExample.UserNameForeColor(newIndex, Properties.Settings.Default.MemberColor);
            }
            newIndex = dgvExample.Rows.Add(now.AddMinutes(5).ToString("H:mm"), icon2, "村人B", "こんにちは～最近どうですか？");
            if (Properties.Settings.Default.EnableChangeUserName)
            {
                dgvExample.UserNameForeColor(newIndex, Properties.Settings.Default.ModeratorColor);
            }
            newIndex = dgvExample.Rows.Add(now.AddMinutes(10).ToString("H:mm"), icon1, "User A", "i'm fine thank you");
            if (Properties.Settings.Default.EnableChangeUserName)
            {
                dgvExample.UserNameForeColor(newIndex, Properties.Settings.Default.MemberColor);
            }
            newIndex = dgvExample.Rows.Add(now.AddMinutes(15).ToString("H:mm"), icon2, "村人B", "￥320\nお疲れ様でした");
            if (Properties.Settings.Default.EnableChangeUserName)
            {
                dgvExample.UserNameForeColor(newIndex, Properties.Settings.Default.ModeratorColor);
            }
            if (Properties.Settings.Default.EnableSuperChatColor)
            {
                dgvExample.Rows[newIndex].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#000000");
                dgvExample.UserNameForeColor(newIndex, ColorTranslator.FromHtml("#000000"));
                dgvExample.SetRowBackColor(dgvExample.Rows.Count - 1, ColorTranslator.FromHtml("#00e5ff"));
            }
            newIndex = dgvExample.Rows.Add(now.AddMinutes(20).ToString("H:mm"), icon1, "User A", "See You;)");
            if (checkBoxValidUserType.Checked)
            {
                dgvExample.UserNameForeColor(newIndex, Properties.Settings.Default.MemberColor);
            }
            newIndex = dgvExample.Rows.Add(now.AddMinutes(25).ToString("H:mm"), icon2, "村人B", "初見です");
            if (checkBoxValidUserType.Checked)
            {
                dgvExample.UserNameForeColor(newIndex, Properties.Settings.Default.ModeratorColor);
            }
        }

        #region 自作デザイナー
        private ColorComboBox colorComboBoxMemberFore;
        private ColorComboBox colorComboBoxGrid;
        private ColorComboBox colorComboBoxBack;
        private ColorComboBox colorComboBoxSelectionFore;
        private ColorComboBox colorComboBoxFore;
        private ColorComboBox colorComboBoxModeratorFore;
        private CityComboBox cityComboBox;
        private FontComboBox fontComboBox;
        private CustumDataGridView dgvExample;
        private void InitializeComponentCustum()
        {
            colorComboBoxFore = new ColorComboBox();
            colorComboBoxSelectionFore = new ColorComboBox();
            colorComboBoxBack = new ColorComboBox();
            colorComboBoxGrid = new ColorComboBox();
            colorComboBoxModeratorFore = new ColorComboBox();
            colorComboBoxMemberFore = new ColorComboBox();
            cityComboBox = new CityComboBox();
            fontComboBox = new FontComboBox();
            dgvExample = new CustumDataGridView();
            // 
            // colorComboBoxFore
            // 
            colorComboBoxFore.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            colorComboBoxFore.FormattingEnabled = true;
            colorComboBoxFore.IntegralHeight = false;
            colorComboBoxFore.Location = new Point(275, 82);
            colorComboBoxFore.Name = "colorComboBoxFore";
            colorComboBoxFore.SelectedColor = Color.Black;
            colorComboBoxFore.Size = new Size(117, 24);
            colorComboBoxFore.TabIndex = 15;
            colorComboBoxFore.ValidTransparent = true;
            // 
            // colorComboBoxSelectionFore
            // 
            colorComboBoxSelectionFore.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            colorComboBoxSelectionFore.FormattingEnabled = true;
            colorComboBoxSelectionFore.IntegralHeight = false;
            colorComboBoxSelectionFore.Location = new Point(275, 112);
            colorComboBoxSelectionFore.Name = "colorComboBoxSelectionFore";
            colorComboBoxSelectionFore.SelectedColor = Color.Red;
            colorComboBoxSelectionFore.Size = new Size(117, 24);
            colorComboBoxSelectionFore.TabIndex = 15;
            colorComboBoxSelectionFore.ValidTransparent = true;
            // 
            // colorComboBoxBack
            // 
            colorComboBoxBack.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            colorComboBoxBack.FormattingEnabled = true;
            colorComboBoxBack.IntegralHeight = false;
            colorComboBoxBack.Location = new Point(275, 142);
            colorComboBoxBack.Name = "colorComboBoxBack";
            colorComboBoxBack.SelectedColor = Color.White;
            colorComboBoxBack.Size = new Size(117, 24);
            colorComboBoxBack.TabIndex = 15;
            colorComboBoxBack.ValidTransparent = false;
            // 
            // colorComboBoxGrid
            // 
            colorComboBoxGrid.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            colorComboBoxGrid.FormattingEnabled = true;
            colorComboBoxGrid.IntegralHeight = false;
            colorComboBoxGrid.Location = new Point(275, 172);
            colorComboBoxGrid.Name = "colorComboBoxGrid";
            colorComboBoxGrid.SelectedColor = Color.DimGray;
            colorComboBoxGrid.Size = new Size(117, 24);
            colorComboBoxGrid.TabIndex = 15;
            colorComboBoxGrid.ValidTransparent = false;

            // 
            // colorComboBoxModeratorFore
            // 
            colorComboBoxModeratorFore.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            colorComboBoxModeratorFore.FormattingEnabled = true;
            colorComboBoxModeratorFore.IntegralHeight = false;
            colorComboBoxModeratorFore.Location = new Point(260, 3);
            colorComboBoxModeratorFore.Name = "colorComboBoxModeratorFore";
            colorComboBoxModeratorFore.SelectedColor = Color.RoyalBlue;
            colorComboBoxModeratorFore.Size = new Size(117, 24);
            colorComboBoxModeratorFore.TabIndex = 15;
            colorComboBoxModeratorFore.ValidTransparent = true;
            // 
            // colorComboBoxMemberFore
            // 
            colorComboBoxMemberFore.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            colorComboBoxMemberFore.FormattingEnabled = true;
            colorComboBoxMemberFore.IntegralHeight = false;
            colorComboBoxMemberFore.Location = new Point(260, 33);
            colorComboBoxMemberFore.Name = "colorComboBoxMemberFore";
            colorComboBoxMemberFore.SelectedColor = Color.Green;
            colorComboBoxMemberFore.Size = new Size(117, 24);
            colorComboBoxMemberFore.TabIndex = 16;
            colorComboBoxMemberFore.ValidTransparent = true;
            // 
            // cityComboBox
            // 
            cityComboBox.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            cityComboBox.FormattingEnabled = true;
            cityComboBox.Location = new Point(275, 201);
            cityComboBox.Name = "cityComboBox";
            cityComboBox.SelectedCity = "Tokyo";
            cityComboBox.Size = new Size(117, 23);
            cityComboBox.TabIndex = 9;
            // 
            // fontComboBox
            // 
            fontComboBox.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            fontComboBox.FormattingEnabled = true;
            fontComboBox.IntegralHeight = false;
            fontComboBox.Location = new Point(275, 22);
            fontComboBox.Name = "fontComboBox";
            fontComboBox.SelectedFont = "Agency FB";
            fontComboBox.Size = new Size(117, 24);
            fontComboBox.TabIndex = 1;
            // 
            // dgvExample
            // 
            dgvExample.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvExample.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExample.Location = new Point(12, 24);
            dgvExample.Name = "dgvExample";
            dgvExample.Size = new Size(510, 240);
            dgvExample.TabIndex = 4;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cityComboBox);
            groupBox2.Controls.Add(colorComboBoxGrid);
            groupBox2.Controls.Add(colorComboBoxBack);
            groupBox2.Controls.Add(colorComboBoxSelectionFore);
            groupBox2.Controls.Add(colorComboBoxFore);
            groupBox2.Controls.Add(fontComboBox);
            // 
            // groupBoxUserName
            // 
            groupBoxUserName.Controls.Add(checkBoxValidUserType);
            // 
            // panelUserType
            // 
            panelUserType.Controls.Add(colorComboBoxModeratorFore);
            panelUserType.Controls.Add(colorComboBoxMemberFore);
            // 
            // FormSettings
            // 
            Controls.Add(dgvExample);
            ((System.ComponentModel.ISupportInitialize)dgvExample).EndInit();
        }
        #endregion
    }
}
