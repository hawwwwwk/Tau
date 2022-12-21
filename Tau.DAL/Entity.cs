using System.ComponentModel.DataAnnotations;

namespace Tau.DAL
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}