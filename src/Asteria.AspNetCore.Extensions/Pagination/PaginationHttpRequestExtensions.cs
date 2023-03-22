using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace Asteria.AspNetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class PaginationHttpRequestExtensions
    {

        private static Range Parse(string formula)
        {
            Regex validate = new(@"\s*items\s*\d+-\d+", RegexOptions.IgnoreCase);
            if (validate.IsMatch(formula))
            {
                Regex numbsRegex = new(@"(\d+)");
                MatchCollection numbsMatch = numbsRegex.Matches(formula);
                if (numbsMatch.Count >= 2)
                {
                    var start = int.Parse(numbsMatch[0].Value);
                    var end = int.Parse(numbsMatch[1].Value);
                    if (end >= start)
                    {
                        return start..end;
                    }
                }
            }

            return Range.All;
        }

        /// <summary>
        /// 获取请求的分页范围
        /// </summary>
        /// <returns></returns>
        public static Range GetRange(this HttpRequest request)
        {
            if (request.Headers.TryGetValue("Range", out var val))
            {
                return Parse(val);
            }

            return Range.All;
        }

    }
}
