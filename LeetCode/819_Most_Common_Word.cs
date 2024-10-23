namespace LeetCode
{
    public static class _819_Most_Common_Word
    {
        public static string MostCommonWord(string paragraph, string[] banned)
        {
            var para = banned.Select(a => paragraph.Replace(a, "")).First().ToLower();

            for (int i = 0; i < para.Length; i++)
            {
                if (char.IsPunctuation(para[i]))
                {
                   // para='j';
                }
            }

            return "";
        }
    }
}
