using System.Drawing.Drawing2D;
using System.Net;

namespace YouTubeCommentViewer
{
    internal class ImageConvertor
    {
        /// <summary>
        /// 画像URLを円形のBitmapに変換
        /// </summary>
        /// <param name="url">画像URL</param>
        /// <returns>Bitmap</returns>
        internal static Bitmap UrlToBitmap(string url, int size)
        {
            var wc = new WebClient();
            var stream = wc.OpenRead(url);
            var bitmap = new Bitmap(stream);
            stream.Close();
            return CircleBitmap(bitmap, size);
        }

        /// <summary>
        /// Bitmapを円形のBitmapに変換
        /// </summary>
        /// <returns>Bitmap</returns>
        internal static Bitmap CircleBitmap(Bitmap bitmap, int size)
        {
            //サイズ調整と円形にトリミング
            var canvas = new Bitmap(size, size);
            var g = Graphics.FromImage(canvas);
            var gp = new GraphicsPath();
            gp.AddEllipse(g.VisibleClipBounds);
            var rgn = new Region(gp);
            g.Clip = rgn;
            var img = bitmap;
            g.DrawImage(img, g.VisibleClipBounds);
            img.Dispose();
            g.Dispose();

            return canvas;
        }
    }
}
