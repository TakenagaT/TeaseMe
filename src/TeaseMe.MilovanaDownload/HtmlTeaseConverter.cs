using System;
using System.Text.RegularExpressions;
using TeaseMe.Common;

namespace TeaseMe.MilovanaDownload
{
    public class HtmlTeaseConverter
    {
        public static TeasePage CreatePage(string id, string originalHtml)
        {
            var result = new TeasePage { Id = id };
            
            var text = originalHtml.Remove('\n', '\r', '\t');
            text = text.AfterFirst("<a name=\"t\"></a>");

            string nextPage;
            string imageUrl;

            if (text.Contains("<a name=\"rate\"></a>"))
            {
                // Last page.
                text = text.BeforeFirst("<div class=\"item\"><a name=\"rate\"></a>");

                Match m = Regex.Match(text, "<img src=\"(?<imageUrl>.*?)\"");
                nextPage = String.Empty;
                imageUrl = m.Success ? m.Groups["imageUrl"].Value : String.Empty;
            }
            else
            {
                text = text.BeforeFirst("<script type=\"text/javascript\">document.onkeypress=function(e)");

                Match m = Regex.Match(text, "<a href=\"webteases/showtease.php\\?id=(?<teaseId>\\d+)&p=(?<nextPage>\\d+)#t\"><img src=\"(?<imageUrl>.*?)\"");
                nextPage = m.Success ? m.Groups["nextPage"].Value : String.Empty;
                imageUrl = m.Success ? m.Groups["imageUrl"].Value : String.Empty;
            }

            text = text.AfterFirst("<div id=\"tease_content\">");

            if (text.StartsWith("<p class=\"breaker\"></p>"))
            {
                text = text.AfterFirst("<p class=\"breaker\"></p>");
            }
            if (text.Contains("<p class=\"link\"><a href=\"webteases/showtease.php?id="))
            {
                text = text.BeforeLast("<p class=\"link\"><a href=\"webteases/showtease.php?id=");
            }
            if (text.Contains("</div><h1>The End</h1>"))
            {
                text = text.Replace("</div><h1>The End</h1>", "<h1>The End</h1>");
            }
            
            result.Text = text;

            if (!String.IsNullOrEmpty(imageUrl))
            {
                result.ImageList.Add(new TeaseMedia { Id = imageUrl });
            }
            if (!String.IsNullOrEmpty(nextPage))
            {
                result.ButtonList.Add(new TeaseButton { Target = nextPage, Text = "Continue" });
            }

            return result;
        }
    }
}
