namespace LeetCode
{
    public static class _819_Most_Common_Word
    {
        public static string MostCommonWord(string paragraph, string[] banned)
        {
            var words = paragraph.ToLower().Split(new char[] { ' ', '!', '?', ',', ';', '.', '\'' },StringSplitOptions.RemoveEmptyEntries);
            var dic = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (banned.Contains(word))
                {
                    continue;
                }

                if (!dic.ContainsKey(word))
                {
                    dic.TryAdd(word, 1);
                }
                else
                {
                    dic[word]++;
                }
            }

            return dic.MaxBy(x => x.Value).Key;
        }
    }
}
