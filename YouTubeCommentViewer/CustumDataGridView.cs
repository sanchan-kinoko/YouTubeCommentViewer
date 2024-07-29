namespace YouTubeCommentViewer
{
    internal class CustumDataGridView : DataGridView
    {
        private bool enableUserNameWidthLoad = false;
        private bool enableUserNameWidthSave = false;

        /// <summary>
        /// DataGridViewが開いたかどうか
        /// </summary>
        private bool IsOpen = false;
        /// <summary>
        /// ユーザー名列のロードの有効化
        /// </summary>
        public bool EnableUserNameWidthLoad
        {
            get => enableUserNameWidthLoad;
            set => enableUserNameWidthLoad = value;
        }
        /// <summary>
        /// ユーザー名列のセーブの有効化
        /// </summary>
        public bool EnableUserNameWidthSave
        {
            get => enableUserNameWidthSave;
            set => enableUserNameWidthSave = value;
        }

        public CustumDataGridView() : base()
        {
            DoubleBuffered = true;
        }
        /// <summary>
        /// DataGridViewColumnの初期設定
        /// </summary>
        /// <param name="column"></param>
        /// <param name="name"></param>
        private void ColumnSettings(DataGridViewColumn column, string name)
        {
            var cellStyle = new DataGridViewCellStyle();
            cellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            cellStyle.WrapMode = DataGridViewTriState.True;
            column.DefaultCellStyle = cellStyle;
            column.HeaderText = name;
            column.Name = name;
            column.ReadOnly = true;
            column.Width = 120;
        }

        /// <summary>
        /// 指定行のユーザー名のフォント色を変更
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="color"></param>
        public void UserNameForeColor(int rowIndex, Color color)
        {
            Rows[rowIndex].Cells["columnUserName"].Style.ForeColor = color;
        }

        /// <summary>
        /// 指定した列の表示設定
        /// </summary>
        /// <param name="columnType"></param>
        /// <param name="visible"></param>
        public void VisibleColumn(ColumnType columnType, bool visible) { ToColumn(columnType).Visible = visible; }

        /// <summary>
        /// 列タイプからDataGridViewColumnを取得
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private DataGridViewColumn ToColumn(ColumnType type)
        {
            switch (type)
            {
                case ColumnType.TimeStamp: return Columns["columnTimeStamp"];
                case ColumnType.UserIcon: return Columns["columnIcon"];
                case ColumnType.UserName: return Columns["columnUserName"];
                case ColumnType.Comment: return Columns["columnComment"];
                default: return new DataGridViewColumn();
            }
        }

        /// <summary>
        /// DataGridViewの幅と列の表示状況に応じて最終列の幅を調節
        /// </summary>
        public void ResizeColumnWidth()
        {
            int differenceWidth = 3;
            for(int i = 0; i < Columns.Count - 1; i++)
            {
                if (Columns[i].Visible) { differenceWidth += Columns[i].Width; }
            }
            Columns[Columns.Count - 1].Width = Width - differenceWidth;
        }

        /// <summary>
        /// 指定行の背景色を設定(選択中の背景色も設定)
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="color"></param>
        public void SetRowBackColor(int rowIndex, Color color)
        {
            Rows[rowIndex].DefaultCellStyle.BackColor = color;
            foreach (DataGridViewColumn column in Columns)
            {
                Rows[rowIndex].Cells[column.Name].Style.SelectionBackColor = color;
            }
        }

        /// <summary>
        /// 背景色を設定
        /// </summary>
        public void SetBackColor(Color color)
        {
            ToColumn(ColumnType.TimeStamp).DefaultCellStyle.BackColor = color;
            ToColumn(ColumnType.UserIcon).DefaultCellStyle.BackColor = color;
            ToColumn(ColumnType.UserName).DefaultCellStyle.BackColor = color;
            ToColumn(ColumnType.Comment).DefaultCellStyle.BackColor = color;

            ToColumn(ColumnType.TimeStamp).DefaultCellStyle.SelectionBackColor = color;
            ToColumn(ColumnType.UserIcon).DefaultCellStyle.SelectionBackColor = color;
            ToColumn(ColumnType.UserName).DefaultCellStyle.SelectionBackColor = color;
            ToColumn(ColumnType.Comment).DefaultCellStyle.SelectionBackColor = color;

            BackgroundColor = color;
        }

        /// <summary>
        /// 選択中の文字色を設定
        /// </summary>
        public void SetSelectionForeColor(Color color)
        {
            ToColumn(ColumnType.TimeStamp).DefaultCellStyle.SelectionForeColor = color;
            ToColumn(ColumnType.UserIcon).DefaultCellStyle.SelectionForeColor = color;
            ToColumn(ColumnType.UserName).DefaultCellStyle.SelectionForeColor = color;
            ToColumn(ColumnType.Comment).DefaultCellStyle.SelectionForeColor = color;
        }

        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            if (IsOpen)
            { 
                ResizeColumnWidth();
                Refresh();
                if (enableUserNameWidthSave && e.Column == ToColumn(ColumnType.UserName))
                {
                    Properties.Settings.Default.UserColumnWidth = e.Column.Width;
                    Properties.Settings.Default.Save();
                }
            }
        }

        protected override void InitLayout()
        {
            //列追加
            var columnTimeStamp = new DataGridViewTextBoxColumn();
            ColumnSettings(columnTimeStamp, "columnTimeStamp");
            columnTimeStamp.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            columnTimeStamp.Resizable = DataGridViewTriState.False;

            var columnIcon = new DataGridViewImageColumn();
            ColumnSettings(columnIcon, "columnIcon");
            columnIcon.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            columnIcon.Resizable = DataGridViewTriState.False;

            var columnUserName = new DataGridViewTextBoxColumn();
            ColumnSettings(columnUserName, "columnUserName");
            columnUserName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            columnUserName.Resizable = DataGridViewTriState.True;
            if (EnableUserNameWidthLoad)
            {
                columnUserName.Width = Properties.Settings.Default.UserColumnWidth;
            }

            var columnComment = new DataGridViewTextBoxColumn();
            ColumnSettings(columnComment, "columnComment");
            columnComment.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            columnComment.Resizable = DataGridViewTriState.False;

            Columns.AddRange(columnTimeStamp, columnIcon, columnUserName, columnComment);

            //プロパティの設定
            AllowUserToAddRows = false;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            BackgroundColor = Color.White;
            ColumnHeadersVisible = false;
            RowHeadersVisible = false;
            ScrollBars = ScrollBars.None;
            base.InitLayout();

            IsOpen = true;
        }
    }
}
