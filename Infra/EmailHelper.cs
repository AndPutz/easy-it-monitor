using Infra.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class EmailHelper
    {
        public const string HTML_BASE = "HtmlBase";
        public const string DEFAULT_SUBJECT = SystemInfo.MONITOR_DESCRIPTION;

        public static string CreateHtmlBody(string title, string name, string content)
        {
            return Resources.ResourceManager.GetString(HTML_BASE)
                .Replace("{title}", title)
                .Replace("{conteudo}", name)
                .Replace("{nome}", content);
        }

        public static string AddContent(string content)
        {
            return "<p> Hi! <b>{name}</b>,<br/>" + content;
        }
    }
}
