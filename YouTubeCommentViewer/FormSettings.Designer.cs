namespace YouTubeCommentViewer
{
    partial class FormSettings
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            groupBox2 = new GroupBox();
            buttonDefaultSetting = new Button();
            checkBoxSuperChatColor = new CustumCheckBox();
            checkBoxDisplayUserIcon = new CustumCheckBox();
            checkBoxDisplayUserName = new CustumCheckBox();
            checkBoxDisplayTimeStamp = new CustumCheckBox();
            groupBoxUserName = new GroupBox();
            panelUserType = new Panel();
            label3 = new Label();
            checkBoxDisplayGrid = new CustumCheckBox();
            nudFontSize = new NumericUpDown();
            labelDiscription = new Label();
            checkBoxValidUserType = new CustumCheckBox();
            buttonStart = new Button();
            label2 = new Label();
            groupBox1 = new GroupBox();
            textBoxLiveId = new TextBox();
            dgvExample = new CustumDataGridView();
            label4 = new Label();
            webView2HowTo = new Microsoft.Web.WebView2.WinForms.WebView2();
            groupBox2.SuspendLayout();
            groupBoxUserName.SuspendLayout();
            panelUserType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudFontSize).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExample).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView2HowTo).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label1.Location = new Point(8, 6);
            label1.Name = "label1";
            label1.Size = new Size(138, 15);
            label1.TabIndex = 0;
            label1.Text = "表示例(Display example)";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox2.Controls.Add(buttonDefaultSetting);
            groupBox2.Controls.Add(checkBoxSuperChatColor);
            groupBox2.Controls.Add(checkBoxDisplayUserIcon);
            groupBox2.Controls.Add(checkBoxDisplayUserName);
            groupBox2.Controls.Add(checkBoxDisplayTimeStamp);
            groupBox2.Controls.Add(groupBoxUserName);
            groupBox2.Controls.Add(checkBoxDisplayGrid);
            groupBox2.Controls.Add(nudFontSize);
            groupBox2.Controls.Add(labelDiscription);
            groupBox2.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            groupBox2.Location = new Point(528, 71);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(404, 462);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "表示設定(Diplay Settings)";
            // 
            // buttonDefaultSetting
            // 
            buttonDefaultSetting.Location = new Point(172, 433);
            buttonDefaultSetting.Name = "buttonDefaultSetting";
            buttonDefaultSetting.Size = new Size(226, 23);
            buttonDefaultSetting.TabIndex = 19;
            buttonDefaultSetting.Text = "デフォルト設定に戻す / Restore default settings";
            buttonDefaultSetting.UseVisualStyleBackColor = true;
            buttonDefaultSetting.Click += buttonDefaultSetting_Click;
            // 
            // checkBoxSuperChatColor
            // 
            checkBoxSuperChatColor.AutoSize = true;
            checkBoxSuperChatColor.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            checkBoxSuperChatColor.Location = new Point(15, 388);
            checkBoxSuperChatColor.Name = "checkBoxSuperChatColor";
            checkBoxSuperChatColor.Size = new Size(269, 19);
            checkBoxSuperChatColor.TabIndex = 18;
            checkBoxSuperChatColor.Text = "スーパーチャットに色を付ける / Set super chat color";
            checkBoxSuperChatColor.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayUserIcon
            // 
            checkBoxDisplayUserIcon.AutoSize = true;
            checkBoxDisplayUserIcon.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            checkBoxDisplayUserIcon.Location = new Point(15, 359);
            checkBoxDisplayUserIcon.Name = "checkBoxDisplayUserIcon";
            checkBoxDisplayUserIcon.Size = new Size(221, 19);
            checkBoxDisplayUserIcon.TabIndex = 17;
            checkBoxDisplayUserIcon.Text = "ユーザーアイコン表示 / Display user icon";
            checkBoxDisplayUserIcon.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayUserName
            // 
            checkBoxDisplayUserName.AutoSize = true;
            checkBoxDisplayUserName.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            checkBoxDisplayUserName.Location = new Point(12, 228);
            checkBoxDisplayUserName.Margin = new Padding(0);
            checkBoxDisplayUserName.Name = "checkBoxDisplayUserName";
            checkBoxDisplayUserName.Padding = new Padding(3);
            checkBoxDisplayUserName.Size = new Size(210, 25);
            checkBoxDisplayUserName.TabIndex = 16;
            checkBoxDisplayUserName.Text = "ユーザー名表示 / Display user name";
            checkBoxDisplayUserName.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayTimeStamp
            // 
            checkBoxDisplayTimeStamp.AutoSize = true;
            checkBoxDisplayTimeStamp.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            checkBoxDisplayTimeStamp.Location = new Point(15, 203);
            checkBoxDisplayTimeStamp.Name = "checkBoxDisplayTimeStamp";
            checkBoxDisplayTimeStamp.Size = new Size(217, 19);
            checkBoxDisplayTimeStamp.TabIndex = 15;
            checkBoxDisplayTimeStamp.Text = "コメント時間表示 / Display time stamp";
            checkBoxDisplayTimeStamp.UseVisualStyleBackColor = true;
            // 
            // groupBoxUserName
            // 
            groupBoxUserName.Controls.Add(panelUserType);
            groupBoxUserName.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            groupBoxUserName.Location = new Point(6, 232);
            groupBoxUserName.Name = "groupBoxUserName";
            groupBoxUserName.Size = new Size(392, 119);
            groupBoxUserName.TabIndex = 14;
            groupBoxUserName.TabStop = false;
            // 
            // panelUserType
            // 
            panelUserType.Controls.Add(label3);
            panelUserType.Location = new Point(9, 54);
            panelUserType.Name = "panelUserType";
            panelUserType.Size = new Size(380, 59);
            panelUserType.TabIndex = 19;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label3.Location = new Point(131, 6);
            label3.Name = "label3";
            label3.Size = new Size(123, 45);
            label3.TabIndex = 15;
            label3.Text = "モデレーター(Moderator)\r\n\r\nメンバー(Member)";
            // 
            // checkBoxDisplayGrid
            // 
            checkBoxDisplayGrid.AutoSize = true;
            checkBoxDisplayGrid.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            checkBoxDisplayGrid.Location = new Point(15, 173);
            checkBoxDisplayGrid.Name = "checkBoxDisplayGrid";
            checkBoxDisplayGrid.RightToLeft = RightToLeft.No;
            checkBoxDisplayGrid.Size = new Size(259, 19);
            checkBoxDisplayGrid.TabIndex = 7;
            checkBoxDisplayGrid.Text = "グリッド線表示＆色(Display Grid Lines＆color)";
            checkBoxDisplayGrid.UseVisualStyleBackColor = true;
            // 
            // nudFontSize
            // 
            nudFontSize.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            nudFontSize.Location = new Point(275, 52);
            nudFontSize.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            nudFontSize.Name = "nudFontSize";
            nudFontSize.Size = new Size(117, 23);
            nudFontSize.TabIndex = 3;
            nudFontSize.TextAlign = HorizontalAlignment.Right;
            nudFontSize.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // labelDiscription
            // 
            labelDiscription.AutoSize = true;
            labelDiscription.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            labelDiscription.Location = new Point(6, 25);
            labelDiscription.Name = "labelDiscription";
            labelDiscription.Size = new Size(218, 135);
            labelDiscription.TabIndex = 0;
            labelDiscription.Text = "フォント(Font)\r\n\r\nフォントサイズ(Font size) {0} - {1}\r\n\r\nフォントの色(Font Color)\r\n\r\n選択中のフォントの色(Selected Font Color)\r\n\r\n背景色(Background Color)";
            // 
            // checkBoxValidUserType
            // 
            checkBoxValidUserType.AutoSize = true;
            checkBoxValidUserType.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            checkBoxValidUserType.Location = new Point(29, 29);
            checkBoxValidUserType.Name = "checkBoxValidUserType";
            checkBoxValidUserType.Size = new Size(361, 19);
            checkBoxValidUserType.TabIndex = 18;
            checkBoxValidUserType.Text = "ユーザータイプで色を変える(Change colors depending on user type)";
            checkBoxValidUserType.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            buttonStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonStart.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
            buttonStart.Location = new Point(528, 539);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(404, 50);
            buttonStart.TabIndex = 3;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label2.Location = new Point(6, 25);
            label2.Name = "label2";
            label2.Size = new Size(202, 15);
            label2.TabIndex = 1;
            label2.Text = "https://www.youtube.com/watch?v=";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBoxLiveId);
            groupBox1.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            groupBox1.Location = new Point(528, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(404, 59);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Live ID 設定(Live ID Setting)";
            // 
            // textBoxLiveId
            // 
            textBoxLiveId.Location = new Point(275, 22);
            textBoxLiveId.Name = "textBoxLiveId";
            textBoxLiveId.Size = new Size(117, 23);
            textBoxLiveId.TabIndex = 1;
            // 
            // dgvExample
            // 
            dgvExample.EnableUserNameWidthLoad = false;
            dgvExample.EnableUserNameWidthSave = false;
            dgvExample.Location = new Point(0, 0);
            dgvExample.Name = "dgvExample";
            dgvExample.Size = new Size(240, 150);
            dgvExample.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label4.Location = new Point(12, 274);
            label4.Name = "label4";
            label4.Size = new Size(90, 15);
            label4.TabIndex = 4;
            label4.Text = "使い方(How To)";
            // 
            // webView2HowTo
            // 
            webView2HowTo.AllowExternalDrop = true;
            webView2HowTo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webView2HowTo.CreationProperties = null;
            webView2HowTo.DefaultBackgroundColor = Color.White;
            webView2HowTo.Location = new Point(12, 292);
            webView2HowTo.Name = "webView2HowTo";
            webView2HowTo.Size = new Size(510, 297);
            webView2HowTo.TabIndex = 5;
            webView2HowTo.ZoomFactor = 1D;
            // 
            // FormSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(944, 601);
            Controls.Add(webView2HowTo);
            Controls.Add(label4);
            Controls.Add(groupBox1);
            Controls.Add(buttonStart);
            Controls.Add(groupBox2);
            Controls.Add(label1);
            MinimumSize = new Size(960, 640);
            Name = "FormSettings";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Comment Viewer [Settings]";
            Load += FormSettings_Load;
            Resize += FormSettings_Resize;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBoxUserName.ResumeLayout(false);
            panelUserType.ResumeLayout(false);
            panelUserType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudFontSize).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExample).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView2HowTo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private CustumCheckBox checkBoxDisplayTimeStamp;
        private CustumCheckBox checkBoxDisplayGrid;
        private CustumCheckBox checkBoxDisplayUserName;
        private CustumCheckBox checkBoxValidUserType;
        private CustumCheckBox checkBoxSuperChatColor;
        private CustumCheckBox checkBoxDisplayUserIcon;
        private Label label1;
        private GroupBox groupBox2;
        private Label labelDiscription;
        private Button buttonStart;
        private GroupBox groupBox1;
        private Label label2;
        private NumericUpDown nudFontSize;
        private GroupBox groupBoxUserName;
        private Label label3;
        private Panel panelUserType;
        private TextBox textBoxLiveId;
        private Button buttonDefaultSetting;
        private Label label4;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2HowTo;
    }
}
