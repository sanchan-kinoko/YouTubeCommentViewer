namespace YouTubeCommentViewer
{
    public struct Comment
    {
        public readonly List<Message> message;
        public readonly string commentId;
        public readonly string userName;
        public readonly string userId;
        public readonly UserType userType;
        public readonly string iconUrl;
        public readonly DateTime timeStump;
        public readonly string superChatAmount;
        public readonly CommentType commentType;
        /// <summary>
        /// nullならデフォルトカラー
        /// </summary>
        public readonly Color? foreColor;
        /// <summary>
        /// nullならデフォルトカラー
        /// </summary>
        public readonly Color? backColor;


        public Comment(List<Message> message, string commentId, string userName, string userId,
            UserType userType, string iconUrl, DateTime timeStump, string superChatAmount, CommentType commentType,
            Color? foreColor, Color? backColor)
        {
            this.message = message;
            this.commentId = commentId;
            this.userName = userName;
            this.userId = userId;
            this.userType = userType;
            this.iconUrl = iconUrl;
            this.timeStump = timeStump;
            this.superChatAmount = superChatAmount;
            this.commentType = commentType;
            this.foreColor = foreColor;
            this.backColor = backColor;

        }
    }
}
