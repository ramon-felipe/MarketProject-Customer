using System.ComponentModel.DataAnnotations;

namespace MarketProject.CustomerService.Domain.Entities
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; protected set; }

    }
}
