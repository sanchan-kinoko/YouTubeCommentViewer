namespace YouTubeCommentViewer
{
    internal class CustumCheckBox : CheckBox
    {
        private Control enableRelationControl;

        public CustumCheckBox() : base() { enableRelationControl = new Control(); }

        /// <summary>
        /// 使用可否連動コントロール
        /// </summary>
        public Control EnableRelationControl
        {
            get => enableRelationControl;
            set
            {
                enableRelationControl = value;
                enableRelationControl.Enabled = Checked;
            }
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
#nullable disable warnings
            enableRelationControl.Enabled = Checked;
#nullable restore
        }
    }
}
