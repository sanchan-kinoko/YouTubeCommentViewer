using System.ComponentModel;

namespace YouTubeCommentViewer
{
    /// <summary>
    /// フォントを選択するコンボボックス
    /// </summary>
    internal class CityComboBox : ComboBox
    {
        /// <summary>
        /// デフォルトの都市
        /// </summary>
        private const string DEFAULT_CITY = "Anchorage";
        private string selectedCity = DEFAULT_CITY;

        public CityComboBox() : base() { }
        /// <summary>
        /// 都市リストを取得
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<string, int>[] GetCities()
        {
            var cities = new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("Anchorage", -9),
                new KeyValuePair<string, int>("Athens", 2),
                new KeyValuePair<string, int>("Azores", -1),
                new KeyValuePair<string, int>("Baghdad", 3),
                new KeyValuePair<string, int>("Beijing", 8),
                new KeyValuePair<string, int>("Bangkok", 7),
                new KeyValuePair<string, int>("Berlin", 1),
                new KeyValuePair<string, int>("Cairo", 2),
                new KeyValuePair<string, int>("Chicago", -6),
                new KeyValuePair<string, int>("Dacca", 6),
                new KeyValuePair<string, int>("Denver", -7),
                new KeyValuePair<string, int>("Dubai", 4),
                new KeyValuePair<string, int>("Hong Kong", 8),
                new KeyValuePair<string, int>("Honolulu", -10),
                new KeyValuePair<string, int>("Jakarta", 7),
                new KeyValuePair<string, int>("Jeddah", 3),
                new KeyValuePair<string, int>("Karachi", 5),
                new KeyValuePair<string, int>("Lisbon", 0),
                new KeyValuePair<string, int>("London", 0),
                new KeyValuePair<string, int>("Los Angeles", -8),
                new KeyValuePair<string, int>("Mexico City", -6),
                new KeyValuePair<string, int>("Midway Island", -11),
                new KeyValuePair<string, int>("Montreal", -5),
                new KeyValuePair<string, int>("New York", -5),
                new KeyValuePair<string, int>("Paris", 1),
                new KeyValuePair<string, int>("Pyongyang", 9),
                new KeyValuePair<string, int>("Rio de Janeiro", -3),
                new KeyValuePair<string, int>("Rome", 1),
                new KeyValuePair<string, int>("Santo Domingo", -4),
                new KeyValuePair<string, int>("Seoul", 9),
                new KeyValuePair<string, int>("Singapore", 8),
                new KeyValuePair<string, int>("Tokyo", 9),
                new KeyValuePair<string, int>("Vancouver", -8),
            };
            return cities;
        }

        /// <summary>
        /// 選択中の都市
        /// </summary>
        [Browsable(true)]
        public string SelectedCity
        {
            get => selectedCity;
            set
            {
                if (value is null) { selectedCity = DEFAULT_CITY; }
                else { selectedCity = value.Split(',')[0].Trim('['); ; }
                for (int i = 0; i < Items.Count; i++)
                {
#nullable disable warnings
                    if (((KeyValuePair<string, int>)Items[i]).Key != selectedCity) { continue; }
#nullable restore
                    SelectedIndex = i;
                    break;
                }

            }
        }

        /// <summary>
        /// 選択中の都市の標準時間との時差(Hours)
        /// </summary>
        public int SelectedTimeDifference
        {
            get
            {
                if (SelectedValue is not null) { return (int)SelectedValue; }
                return 0;
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
#nullable disable warnings
            selectedCity = SelectedItem.ToString();
#nullable restore
            base.OnSelectedIndexChanged(e);
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            DataSource = GetCities();
            DisplayMember = "key";
            ValueMember = "value";
        }
    }
}
