namespace YouTubeCommentViewer
{
    internal static class Settings
    {
        /// <summary>
        /// フォントサイズに加算してアイコンサイズにする値
        /// </summary>
        private const int ICON_FONT_SIZE_DIFFERENCE = 10;

        public static int IconSize(int fontSize) { return fontSize + ICON_FONT_SIZE_DIFFERENCE; }
    }
}
