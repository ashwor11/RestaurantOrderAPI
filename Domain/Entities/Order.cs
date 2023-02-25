using Core.Persistence.Repositories;
using Domain.Entities.AbstractEntities;

namespace Domain.Entities;

public class Order : Entity
{
    public int? TableId { get; set; }
    public double TotalPrice { get; set; }
    public double PaidPrice { get; set; } = 0;
    public DateTime ClosedDate { get; set; }
    public virtual Table? Table { get; set; }
    public virtual ICollection<OrderedProduct> Products { get; set; }

    public Order()
    {
        Products = new HashSet<OrderedProduct>();
    }
}