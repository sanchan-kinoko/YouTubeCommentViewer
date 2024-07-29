namespace YouTubeCommentViewer
{
    /// <summary>
    /// カスタムコンボボックス
    /// </summary>
    internal class CustumComboBox : ComboBox
    {
        /// <summary>
        /// コンボボックスのドロップダウンリストを表示した時の高さ
        /// </summary>
        internal const int DROP_DOWN_HEIGHT = 300;
        public CustumComboBox() : base()
        {
            DropDownHeight = DROP_DOWN_HEIGHT;
            DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
