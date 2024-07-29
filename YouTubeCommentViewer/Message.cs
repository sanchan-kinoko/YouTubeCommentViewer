namespace YouTubeCommentViewer
{
    /// <summary>
    /// メッセージ
    /// </summary>
    public class Message
    {
        public MessageType type = MessageType.Text;
        public string text = "";

        public Message(MessageType type, string text)
        {
            this.type = type;
            this.text = text;
        }
    }
}
