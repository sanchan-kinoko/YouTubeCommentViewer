using System.Drawing;

namespace YouTubeCommentViewer
{
    public partial class FormCommentViewer : Form
    {
        /// <summary>
        /// �|�[�����O�Ԋu(�~���b)
        /// </summary>
        private const int POLLING_TIMESPAN = 100;

        /// <summary>
        /// �R�����g����擾���ɑҋ@���鎞��(�~���b)
        /// </summary>
        private const int FIRST_WAIT_TIME = 10000;

        /// <summary>
        /// �G���[���Ɏ��ɏ�������܂őҋ@���鎞��(�~���b)
        /// </summary>
        private const int ERROR_WAIT_TIME = 5000;

        /// <summary>
        /// �������I������G���[�A����
        /// </summary>
        private const int DISPOSE_ERROR_COUNT = 3;

        /// <summary>
        /// �ő�\������
        /// </summary>
        private const int MAX_MESSAGE_COUNT = 1000;

        /// <summary>
        /// �������I�����邩�ǂ����H
        /// </summary>
        private bool IsClosing = false;

        /// <summary>
        /// �X�N���[����~�����ǂ����H
        /// </summary>
        private bool IsStopScroll = false;

        private readonly ChatFetch Chat;

        public FormCommentViewer(string liveId)
        {
            InitializeComponent();
            InitializeComponentCustum();

            Size = new Size(Properties.Settings.Default.ViewWidth, Properties.Settings.Default.ViewHeight);
            StartPosition = FormStartPosition.Manual;
            Location = new Point(Properties.Settings.Default.ViewLocationX, Properties.Settings.Default.ViewLocationY);

#nullable disable warnings
            dgvCommentViewer.VisibleColumn(ColumnType.TimeStamp, Properties.Settings.Default.VisibleTimeStamp);
#nullable restore
            dgvCommentViewer.VisibleColumn(ColumnType.UserIcon, Properties.Settings.Default.VisibleUserIcon);
            dgvCommentViewer.VisibleColumn(ColumnType.UserName, Properties.Settings.Default.VisibleUserName);

            dgvCommentViewer.ForeColor = Properties.Settings.Default.ForeColor;
            dgvCommentViewer.SetSelectionForeColor(Properties.Settings.Default.SelectionForeColor);
            dgvCommentViewer.SetBackColor(Properties.Settings.Default.BackColor);
            dgvCommentViewer.GridColor = Properties.Settings.Default.GridColor;

            dgvCommentViewer.DefaultCellStyle.Font = new Font(Properties.Settings.Default.FontName, Properties.Settings.Default.FontSize);

            DataGridViewCellBorderStyle borderStyle;
            if (Properties.Settings.Default.VisibleGrid) { borderStyle = DataGridViewCellBorderStyle.Single; }
            else { borderStyle = DataGridViewCellBorderStyle.None; }
            dgvCommentViewer.CellBorderStyle = borderStyle;
            dgvCommentViewer.ResizeColumnWidth();

            //DataGridView�̐ݒ肪�S�ďI����Ă��烆�[�U�[����̕��ݒ�̕ۑ���L����
            dgvCommentViewer.EnableUserNameWidthSave = true;
            Chat = new ChatFetch(liveId);
        }


        private void FormCommentViewer_Shown(object sender, EventArgs e) { PollingStart(); }

        private void FormCommentViewer_SizeChanged(object sender, EventArgs e)
        {
            dgvCommentViewer.ResizeColumnWidth();
            Properties.Settings.Default.ViewWidth = Width;
            Properties.Settings.Default.ViewHeight = Height;
            Properties.Settings.Default.Save();
        }

        private void FormCommentViewer_LocationChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ViewLocationX = Location.X;
            Properties.Settings.Default.ViewLocationY = Location.Y;
            Properties.Settings.Default.Save();
        }

        private void FormCommentViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                IsStopScroll = !IsStopScroll;
            }
        }

        private void FormCommentViewer_FormClosing(object sender, EventArgs e) { IsClosing = true; }

        private Color UserTypeToColor(UserType userType)
        {
            switch (userType)
            {
                case UserType.None: return Properties.Settings.Default.ForeColor;
                case UserType.Member: return Properties.Settings.Default.MemberColor;
                case UserType.Moderator: return Properties.Settings.Default.ModeratorColor;
                default: return Properties.Settings.Default.ForeColor;
            }
        }

        private void PollingStart()
        {
            int errorCount = 0;
            long chatCount = 0;
            List<string> commentIds = new List<string>();
            bool isFirstTime = true;
            while (!IsClosing)
            {
                try
                {
                    while (!IsClosing)
                    {
                        if (isFirstTime) { isFirstTime = false; }
                        else
                        {
                            SetFormText(IsStopScroll, chatCount, Chat.StatusCode);
                        }
                        Application.DoEvents();

                        var comments = Chat.Fetch(FIRST_WAIT_TIME);
                        if (comments == null) { return; }
                        foreach (var comment in comments)
                        {
                            if (commentIds.Contains(comment.commentId))
                            {
                                continue;
                            }
                            commentIds.Add(comment.commentId);

                            string time = "";
                            if (comment.timeStump.Year > 2020)
                            {
                                //2020�N(�ߋ�)���̂̓����ɐݒ肳��Ă����玞�Ԃ�\�����Ȃ�
                                time = comment.timeStump.AddHours(Properties.Settings.Default.SelectedTimeDifference).ToString("H:mm");
                            }

                            int index = dgvCommentViewer.Rows.Add(
                                time,
                                ImageConvertor.UrlToBitmap(comment.iconUrl, Settings.IconSize(Properties.Settings.Default.FontSize)),
                                comment.userName,
                                GetComment(comment.message)
                                );
                            chatCount++;
                            SetFormText(IsStopScroll, chatCount, Chat.StatusCode);
                            if (Properties.Settings.Default.EnableChangeUserName)
                            {
                                dgvCommentViewer.UserNameForeColor(index, UserTypeToColor(comment.userType));
                            }
                            if (Properties.Settings.Default.EnableSuperChatColor)
                            {
                                if (comment.foreColor.HasValue && comment.backColor.HasValue)
                                {
                                    dgvCommentViewer.Rows[index].DefaultCellStyle.ForeColor = comment.foreColor.Value;
                                    dgvCommentViewer.UserNameForeColor(index, comment.foreColor.Value);
                                    dgvCommentViewer.SetRowBackColor(index, comment.backColor.Value);
                                }
                            }
                            if (commentIds.Count >= MAX_MESSAGE_COUNT)
                            {
                                commentIds.RemoveAt(0);
                                dgvCommentViewer.Rows.RemoveAt(0);
                            }
                        }
                        while (IsStopScroll)
                        {
                            SetFormText(IsStopScroll, chatCount, Chat.StatusCode);
                            Application.DoEvents();
                            Thread.Sleep(POLLING_TIMESPAN);
                        }
                        if (dgvCommentViewer.Rows.Count > 0)
                        {
                            dgvCommentViewer.FirstDisplayedScrollingRowIndex = dgvCommentViewer.Rows.Count - 1;
                        }
                        errorCount = 0;
                        Application.DoEvents();
                        Thread.Sleep(POLLING_TIMESPAN);
                    }
                }
                catch (Exception ex)
                {
                    errorCount++;
                }
                if (!IsClosing)
                {
                    if (errorCount <= DISPOSE_ERROR_COUNT)
                    {
                        Text = "Recovering due to error... " + errorCount + "/" + DISPOSE_ERROR_COUNT;
                        Application.DoEvents();
                        Thread.Sleep(ERROR_WAIT_TIME);
                    }
                    else
                    {
                        Close();
                    }
                }
            }
        }

        /// <summary>
        /// �t�H�[���̃e�L�X�g�ݒ�
        /// </summary>
        /// <param name="isStopScroll"></param>
        /// <returns></returns>
        private void SetFormText(bool isStopScroll, long chatCount, int statusCode)
        {
            string statusJudge = "NG";
            if (statusCode == (int)System.Net.HttpStatusCode.OK) { statusJudge = "OK"; }
            string viewerStatus = "Stop";
            if (IsStopScroll) { viewerStatus = "Restart"; }
            string format = "Comment Viewer [Status Code : {0}({1})] [{2} : Space] [Chat Count : {3}]";
            Text = string.Format(format, statusCode, statusJudge, viewerStatus, chatCount);
        }

        private string GetComment(List<Message> message)
        {
            string comment = "";
            foreach (var item in message)
            {
                comment += item.text;
            }
            return comment.Trim('\n');
        }


        #region ����f�U�C�i�[
        private CustumDataGridView dgvCommentViewer;
        private void InitializeComponentCustum()
        {
            dgvCommentViewer = new CustumDataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvCommentViewer).BeginInit();
            // 
            // dgvCommentViewer
            // 
            dgvCommentViewer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCommentViewer.Dock = DockStyle.Fill;
            dgvCommentViewer.Location = new Point(0, 0);
            dgvCommentViewer.Name = "dgvCommentViewer";
            dgvCommentViewer.TabIndex = 2;
            dgvCommentViewer.EnableUserNameWidthLoad = true;
            // 
            // FormCommentViewer
            // 
            Controls.Add(dgvCommentViewer);
            ((System.ComponentModel.ISupportInitialize)dgvCommentViewer).EndInit();
        }
        #endregion

    }
}
