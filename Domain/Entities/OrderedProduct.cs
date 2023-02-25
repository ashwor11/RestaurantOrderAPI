using Core.Persistence.Repositories;
using Domain.Entities.AbstractEntities;

namespace Domain.Entities;

public class OrderedProduct : Entity
{
    public int ProductId { get; set; }
    public bool IsPaid { get; set; } = false;
    public virtual Product Product { get; set; }

}