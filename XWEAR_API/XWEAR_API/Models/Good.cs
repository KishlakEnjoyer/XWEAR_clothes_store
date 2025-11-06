using System;
using System.Collections.Generic;

namespace XWEAR_API.Models;

public partial class Good
{
    public string GoodArticle { get; set; } = null!;

    public int BrandId { get; set; }

    public int ModelId { get; set; }

    public int CategoryId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<GoodSize> GoodSizes { get; set; } = new List<GoodSize>();

    public virtual Image? Image { get; set; }

    public virtual Model Model { get; set; } = null!;

    public virtual ICollection<TransactionsDetail> TransactionsDetails { get; set; } = new List<TransactionsDetail>();

    public virtual ICollection<User> UserEmails { get; set; } = new List<User>();
}
