using System.Text.RegularExpressions;

public static class StringExtensions
{
    public static string WithSpacesBetweenWords(this string str)
    {
        return Regex.Replace(str, "((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", " $1").Trim();
    }
}