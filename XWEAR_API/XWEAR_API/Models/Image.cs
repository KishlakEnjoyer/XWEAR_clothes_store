using System;
using System.Collections.Generic;

namespace XWEAR_API.Models;

public partial class Image
{
    public string GoodArticle { get; set; } = null!;

    public string Patrh { get; set; } = null!;

    public sbyte? Main { get; set; }

    public virtual Good GoodArticleNavigation { get; set; } = null!;
}
