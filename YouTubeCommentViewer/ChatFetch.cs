using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace YouTubeCommentViewer
{
    public class ChatFetch
    {
        public readonly string liveId;
        private ChatData? chatData = null;
        private readonly HttpClient client;
        private int statusCode = 200;

        public ChatFetch(string liveId)
        {
            this.liveId = liveId;
            client = new HttpClient();
        }

        /// <summary>
        /// ステータスコード
        /// </summary>
        public int StatusCode
        {
            get => statusCode;
            set => statusCode = value;
        }

        public async Task<Comment[]> FetchAsync(int firstWaitTime)
        {
            // 初回時にchatDataを初期化する
            if (chatData == null) await FirstFetch(firstWaitTime).ConfigureAwait(false);

            // Postする
            HttpResponseMessage chat = await FetchChat().ConfigureAwait(false);
            if (chat == null) return new Comment[0];
            string response = chat.Content.ReadAsStringAsync().Result;

            // パースする
            List<Comment> comments = Parse(response);

            // continuationをアップデート
#nullable disable warnings
            chatData.UpdateContinuation(response);
#nullable restore
            return comments.ToArray();
        }

        public Comment[] Fetch(int firstWaitTime) => FetchAsync(firstWaitTime).Result;

        private List<Comment> Parse(string response)
        {
            List<Comment> comments = new List<Comment>();

            var node = JsonNode.Parse(response);
            var a = node?["continuationContents"]?["liveChatContinuation"]?["actions"];
            if (a == null) return comments;

            foreach (var item in a.AsArray())
            {
                JsonNode? chatsItem = null;
#nullable disable warnings
                if (item["addChatItemAction"]?["item"] != null)
#nullable restore
                {
                    chatsItem = item["addChatItemAction"]?["item"];
                }
                else if (item["addLiveChatTickerItemAction"]?
                    ["item"]?
                    ["liveChatTickerSponsorItemRenderer"]?
                    ["showItemEndpoint"]?
                    ["showLiveChatItemEndpoint"]?
                    ["renderer"]?
                    ["liveChatSponsorshipsGiftPurchaseAnnouncementRenderer"]?
                    ["header"] != null)
                {
                    chatsItem = item["addLiveChatTickerItemAction"]?["item"];
                }

                if (chatsItem == null) continue;

                var commentType = CommentType.Normal;
                JsonNode? chats = null;

                if (chatsItem?["liveChatTextMessageRenderer"] != null)
                {
                    commentType = CommentType.Normal;
                    chats = chatsItem?["liveChatTextMessageRenderer"];
                }
                else if (chatsItem?["liveChatMembershipItemRenderer"] != null)
                {
                    commentType = CommentType.Membership;
                    chats = chatsItem?["liveChatMembershipItemRenderer"];
                }
                else if (chatsItem?["liveChatPaidMessageRenderer"] != null)
                {
                    commentType = CommentType.SuperChat;
                    chats = chatsItem?["liveChatPaidMessageRenderer"];
                }
                else if (chatsItem?["liveChatPaidStickerRenderer"] != null)
                {
                    commentType = CommentType.SuperSticker;
                    chats = chatsItem?["liveChatPaidStickerRenderer"];
                }
                else if (chatsItem?["liveChatSponsorshipsHeaderRenderer"] != null)
                {
                    commentType = CommentType.MembershipGift;
                    chats = chatsItem?["liveChatSponsorshipsHeaderRenderer"];
                }
                else
                {
                    continue;
                }

                string superChatAmount = chats?["purchaseAmountText"]?["simpleText"]?.ToString() ?? "";
                var message = GetMessage(chats, superChatAmount);
                string iconUrl = GetIconUrl(chats);
                string commentId = chats?["id"]?.ToString() ?? "";
                string userName = chats?["authorName"]?["simpleText"]?.ToString() ?? "";
                string userId = chats?["authorExternalChannelId"]?.ToString() ?? "";
                var userType = GetUserType(chats);
                var timeStamp = (new DateTime(1970, 1, 1, 0, 0, 0)).AddSeconds(long.Parse(chats?["timestampUsec"]?.ToString() ?? "0") / 1000000);
#nullable disable warnings
                Color? foreColor = ToColor(chats?["bodyTextColor"]?.ToString());
                Color? backColor = ToColor(chats?["bodyBackgroundColor"]?.ToString());
#nullable restore
                if (commentType == CommentType.SuperSticker)
                {
#nullable disable warnings
                    foreColor = ToColor(chats?["moneyChipTextColor"]?.ToString());
                    backColor = ToColor(chats?["moneyChipBackgroundColor"]?.ToString());
#nullable restore
                }
                else if (commentType == CommentType.Membership || commentType == CommentType.MembershipGift)
                {
                    foreColor = Color.White;
                    backColor = Color.FromArgb(255, 15, 157, 88);
                }

                comments.Add(
                    new Comment(
                        message, commentId, userName, userId, userType, iconUrl, timeStamp, superChatAmount, commentType,
                        foreColor, backColor
                        )
                    );
            }

            return comments;
        }

        /// <summary>
        /// ユーザータイプを取得
        /// </summary>
        /// <param name="chats"></param>
        /// <returns></returns>
        private UserType GetUserType(JsonNode? chats)
        {
            if (chats == null) return UserType.None;
            if (chats?["authorBadges"] == null) return UserType.None;
            var userType = UserType.None;
#nullable disable warnings
            foreach (var chat in chats["authorBadges"]?.AsArray())
#nullable restore
            {
                if (chat?["liveChatAuthorBadgeRenderer"] == null) continue;
                if (chat?["liveChatAuthorBadgeRenderer"]?["tooltip"] == null) continue;
#nullable disable warnings
                string tooltip = chat?["liveChatAuthorBadgeRenderer"]?["tooltip"]?.ToString();
                if (tooltip.Contains("Member"))
#nullable restore
                {
                    userType = UserType.Member;
                }
                else if (tooltip.Contains("Moderator"))
                {
                    userType = UserType.Moderator;
                    break;
                }
            }
            return userType;
        }

        /// <summary>
        /// メッセージを取得
        /// </summary>
        /// <param name="chats"></param>
        /// <returns></returns>
        private List<Message> GetMessage(JsonNode? chats, string superChatAmount)
        {
            var message = new List<Message>();

            if (superChatAmount != "")
            {
                message.Add(new Message(MessageType.Text, superChatAmount));
            }
#nullable disable warnings
            if (chats?["header"] != null)
            {
                if (message.Count > 0) { message.Add(new Message(MessageType.Text, "\n")); }
                message.AddRange(GetMessage(chats["header"]?["primaryText"]?.AsArray()));
            }
            if (chats?["headerPrimaryText"]?["runs"] != null)
            {
                if (message.Count > 0) { message.Add(new Message(MessageType.Text, "\n")); }
                message.AddRange(GetMessage(chats["headerPrimaryText"]?["runs"]?.AsArray()));
            }
            if (chats?["primaryText"]?["runs"] != null)
            {
                if (message.Count > 0) { message.Add(new Message(MessageType.Text, "\n")); }
                message.AddRange(GetMessage(chats["primaryText"]?["runs"]?["text"]?.AsArray()));
            }
            if (chats?["headerSubtext"]?["simpleText"] != null)
            {
                if (message.Count > 0) { message.Add(new Message(MessageType.Text, "\n")); }
                if (!string.IsNullOrEmpty(superChatAmount))
                {
                    //新規メンバー
                    message.Add(
                        new Message(
                            MessageType.Text,
                            string.Format("Welcome to {0}", chats["headerSubtext"]?["simpleText"]?.ToString() ?? "")
                            )
                        );
                }
                else
                {
                    //メンバー歴
                    message.Add(
                        new Message(
                            MessageType.Text, 
                            chats["headerSubtext"]?["simpleText"]?.ToString() ?? ""
                            )
                        );
                }
            }
            if (chats?["headerSubtext"]?["runs"] != null)
            {
                if (message.Count > 0) { message.Add(new Message(MessageType.Text, "\n")); }
                message.AddRange(GetMessage(chats["headerSubtext"]?["runs"]?.AsArray()));
            }
            if (chats?["message"]?["runs"] != null)
            {
                if (message.Count > 0) { message.Add(new Message(MessageType.Text, "\n")); }
                message.AddRange(GetMessage(chats["message"]?["runs"]?.AsArray()));
            }
            if (chats?["sticker"]?["accessibility"]?["accessibilityData"]?["label"] != null)
            {
                if (message.Count > 0) { message.Add(new Message(MessageType.Text, "\n")); }
                message.Add(
                    new Message(
                        MessageType.Text, 
                        string.Format("Sticker「{0}」", chats?["sticker"]?["accessibility"]?["accessibilityData"]?["label"]?.ToString() ?? "")
                        )
                    );
            }
#nullable restore
            return message;
        }

        private List<Message> GetMessage(JsonArray chatArray)
        {
            var list = new List<Message>();
            foreach (var chat in chatArray)
            {
#nullable disable warnings
                if (chat?["text"] != null)
                {
                    list.Add(new Message(MessageType.Text, chat["text"].ToString()));
                }
                if (chat?["emoji"] != null)
                {
                    string emojiId = chat["emoji"]["emojiId"]?.ToString();

                    if (emojiId == null) continue;
                    if (emojiId.Length < 25)
                    {
                        list.Add(new Message(MessageType.Text, chat?["emoji"]?["emojiId"]?.ToString()));
#nullable restore
                    }
                    else
                    {
                        list.Add(new Message(MessageType.Text, "⍰"));
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// YouTubeチャット欄のカラー値から色を取得
        /// </summary>
        /// <param name="ytColorValue"></param>
        /// <returns></returns>
        private Color? ToColor(string ytColorValue)
        {
            if (ytColorValue == null) return null;
            long colorValue = long.Parse(ytColorValue);
            string colorCode = string.Format("#{0}", colorValue.ToString("x8").Substring(2, 6));
            return ColorTranslator.FromHtml(colorCode);
        }

        /// <summary>
        /// アイコンURLを取得
        /// </summary>
        /// <param name="chats"></param>
        /// <returns></returns>
        private string GetIconUrl(JsonNode? chats)
        {
            if (chats == null) return "";
            string iconUrl = "";
#nullable disable warnings
            foreach (var chat in chats["authorPhoto"]?["thumbnails"]?.AsArray())
            {
                if (chat?["url"] == null) continue;
                iconUrl = chat["url"].ToString();
#nullable restore
            }
            return iconUrl;
        }

        private async Task<HttpResponseMessage> FetchChat()
        {
            // paramを作る
            var param = new Dictionary<string, string>()
            {
#nullable disable warnings
                ["key"] = chatData.key
#nullable restore
            };

            // データを実際に取ってくる
            var response = await client.PostAsync(
                "https://www.youtube.com/youtubei/v1/live_chat/get_live_chat?" + "key=" + param["key"],
                new StringContent(chatData.Build())).ConfigureAwait(false);

            // OKじゃない場合はnullを返す
            StatusCode = (int)response.StatusCode;
#nullable disable warnings
            if (response.StatusCode != System.Net.HttpStatusCode.OK) return null;
#nullable restore
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            return response;
        }

        private async Task FirstFetch(int firstWaitTime)
        {
            client.DefaultRequestHeaders.Add(
        "User-Agent",
        "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36");

            var param = new Dictionary<string, string>()
            {
                ["v"] = liveId
            };
            var content = await new FormUrlEncodedContent(param).ReadAsStringAsync();
            var result = await client.GetAsync("https://www.youtube.com/live_chat?" + content).ConfigureAwait(false);
            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Thread.Sleep(firstWaitTime);
            Match matchedKey = Regex.Match(resultContent, "\"INNERTUBE_API_KEY\":\"(.+?)\"");
            Match matchedContinuation = Regex.Match(resultContent, "\"continuation\":\"(.+?)\"");
            Match matchedVisitor = Regex.Match(resultContent, "\"visitorData\":\"(.+?)\"");
            Match matchedClient = Regex.Match(resultContent, "\"clientVersion\":\"(.+?)\"");

            chatData = new ChatData(
                matchedKey.Groups[1].Value,
                matchedContinuation.Groups[1].Value,
                matchedVisitor.Groups[1].Value,
                matchedClient.Groups[1].Value);
        }
    }
}
