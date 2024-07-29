using System.ComponentModel;

namespace YouTubeCommentViewer
{
    /// <summary>
    /// フォントを選択するコンボボックス
    /// </summary>
    internal class FontComboBox : CustumComboBox
    {
        private string selectedFont;

#nullable disable warnings
        public FontComboBox() : base()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            Items.AddRange(GetFonts().ToArray());
            SelectedIndex = 0;
            selectedFont = (string)Items[SelectedIndex];
        }
#nullable restore

        /// <summary>
        /// 選択中のフォント
        /// </summary>
        [Browsable(true)]
        public string SelectedFont
        {
            get => selectedFont;
            set
            {
                selectedFont = value;
                for (int i = 0; i < Items.Count; i++)
                {
#nullable disable warnings
                    if ((string)Items[i] != value) { continue; }
#nullable restore
                    SelectedIndex = i;
                    break;
                }

            }
        }

        /// <summary>
        /// フォントを取得
        /// </summary>
        /// <returns></returns>
        private List<string> GetFonts()
        {
            var fontList = new List<string>();
            foreach (var ff in FontFamily.Families)
            {
                if (!ff.IsStyleAvailable(FontStyle.Regular)) { continue; }
                fontList.Add(ff.Name);
            }
            return fontList;
        }

        /// <summary>
        /// ドロップダウンリスト内のフォントを描画
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
#nullable disable warnings
            string txt = e.Index > -1 ? Items[e.Index].ToString() : Text;
            var f = new Font(txt, Font.Size);
#nullable restore
            var b = new SolidBrush(e.ForeColor);
            float ym = (e.Bounds.Height - e.Graphics.MeasureString(txt, f).Height) / 2;
            e.Graphics.DrawString(txt, f, b, e.Bounds.X, e.Bounds.Y + ym);
            f.Dispose();
            b.Dispose();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
#nullable disable warnings
            selectedFont = SelectedItem.ToString();
#nullable restore
            base.OnSelectedIndexChanged(e);
        }

    }
}
