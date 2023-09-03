using Pustok.Database.Base;

namespace Pustok.Database.Models;

public class Basket : BaseEntity<int>, IAuditable
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<BasketItem> BasketItems { get; set; }
}
