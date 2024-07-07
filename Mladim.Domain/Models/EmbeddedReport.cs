using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class EmbeddedReport
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string EmbedUrl {  get; set; }
    public string Token { get; set; }

    public EmbeddedReport(string id, string name, string embedUrl, string token)
    {
        this.Id = id;
        this.Name = name;
        this.EmbedUrl = embedUrl;
        this.Token = token;
    }
}
