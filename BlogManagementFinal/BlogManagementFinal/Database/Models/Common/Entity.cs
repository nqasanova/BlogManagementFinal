using System;
namespace BlogManagementFinal.Database.Models.Common
{
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
