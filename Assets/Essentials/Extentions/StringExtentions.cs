
public static class StringExtentions
{
    public static string Reverse(this string s)
    {
        string reversed = "";

        for (int i = s.Length; i-- > 0;)
            reversed += s[i];

        return reversed;
    }
}
