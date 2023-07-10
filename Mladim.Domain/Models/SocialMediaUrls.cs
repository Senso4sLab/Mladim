using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class SocialMediaUrls
{

    private SocialMediaUrls()
    {

    }
    private  SocialMediaUrls(string? twiter, string? facebook, string? instagram, string? tikTok)
    {
        Twiter = twiter;
        Facebook = facebook;
        Instagram = instagram;
        TikTok = tikTok;
    }

    public string? Twiter { get; private set; }
    public string? Facebook { get; private set; }
    public string? Instagram { get; private set; }
    public string? TikTok { get; private set; }

    public static SocialMediaUrls Create(string? twiter, string? facebook, string? instagram, string? tikTok) =>
        new SocialMediaUrls(twiter, facebook, instagram, tikTok);

}
