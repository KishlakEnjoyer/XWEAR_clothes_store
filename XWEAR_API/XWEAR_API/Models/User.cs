using System;
using System.Collections.Generic;

namespace XWEAR_API.Models;

public partial class User
{
    public string Email { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public string? Nickname { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<Good> GoodArticles { get; set; } = new List<Good>();
}
