using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;

namespace Core.Domain.Entities
{
    public class Entity
    {
        [Key] public Guid Id { get; set; }

        
        //public virtual string SearcheableField =>
        //    this.GetType().GetProperties().Aggregate("", (a, b) => a + " " + b.GetValue(this).ToString());
    }
}
