using System.ComponentModel;
using System.Reflection;

namespace YouTubeCommentViewer
{
    /// <summary>
    /// 色を選択するコンボボックス
    /// </summary>
    internal class ColorComboBox : CustumComboBox
    {
        private Color selectedColor;
        private bool validTransparent;

        public ColorComboBox() : base() { }


        /// <summary>
        /// 選択中の色
        /// </summary>
        [Browsable(true)]
        public Color SelectedColor
        {
            get => selectedColor;
            set
            {
                selectedColor = value;
                for (int i = 0; i < Items.Count; ++i)
                {
#nullable disable warnings
                    if ((Color)Items[i] != value) { continue; }
#nullable restore
                    SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 透明色(Transparent)の有効化
        /// </summary>
        [Browsable(true)]
        public bool ValidTransparent
        {
            get => validTransparent;
            set
            {
                validTransparent = value;
                Items.Clear();
                foreach (var color in GetColors()) { Items.Add(color); }
                if (!value)
                {
                    Items.RemoveAt(TransparentIndex());
                }
                for (int i = 0; i < Items.Count; ++i)
                {
#nullable disable warnings
                    if (((Color)Items[i]) != selectedColor) { continue; }
#nullable restore
                    SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 色を取得
        /// </summary>
        /// <returns></returns>
        private List<Color> GetColors()
        {
            var colorList = new List<Color>();
            var colors = typeof(Color).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var c in colors)
            {
#nullable disable warnings
                var color = (Color)c.GetValue(null, null);
#nullable restore
                colorList.Add(color);
            }
            return colorList;
        }

        /// <summary>
        /// 透明色(Transparent)が存在するIndexを取得、なければ-1
        /// </summary>
        /// <returns></returns>
        private int TransparentIndex()
        {
            for (int i = 0; i < Items.Count; i++)
            {
#nullable disable warnings
                if (((Color)Items[i]).Name == "Transparent") { return i; }
#nullable restore
            }
            return -1;
        }

        /// <summary>
        /// ドロップダウンリスト内の色を描画
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index == -1) { return; }
            e.DrawBackground();
            var rectangle = e.Bounds;
#nullable disable warnings
            var color = (Color)Items[e.Index];
#nullable restore
            rectangle.Offset(2, 2);
            rectangle.Size = new Size(20, rectangle.Height - 4);
            e.Graphics.FillRectangle(new SolidBrush(color), rectangle);
            e.Graphics.DrawRectangle(new Pen(e.ForeColor), rectangle);
            e.Graphics.DrawString(color.Name, Font, new SolidBrush(e.ForeColor), e.Bounds.X + 30, e.Bounds.Y + 1);
            base.OnDrawItem(e);
        }

        protected override void OnSelectedItemChanged(EventArgs e)
        {
            int index = TransparentIndex();
            if (SelectedIndex == index)
            {
                MessageBox.Show("エラー");
                if (index == 0)
                {
                    SelectedIndex = 1;
                }
                else
                {
                    SelectedIndex = 0;
                }
            }
            base.OnSelectedItemChanged(e);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
#nullable disable warnings
            selectedColor = (Color)SelectedItem;
#nullable restore
            base.OnSelectedIndexChanged(e);
        }

        protected override void InitLayout()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            foreach (var color in GetColors()) { Items.Add(color); }
            base.InitLayout();
        }
    }
}
