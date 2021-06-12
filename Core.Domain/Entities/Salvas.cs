using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain.Entities
{
    public class Salvas:Entity
    {
        [Required(ErrorMessageResourceType = typeof(Language.Resources), ErrorMessageResourceName = "FieldRequired")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Language.Resources), ErrorMessageResourceName = "FieldRequired")]
        public DateTime Fecha { get; set; }


        [Display(Name = "Conexión")]
        [Required(ErrorMessageResourceType = typeof(Language.Resources), ErrorMessageResourceName = "FieldRequired")]
        public virtual Guid ConexionId { get; set; }
        public virtual Conexion Conexion { get; set; }


        public string FullName => $"{string.Format("{0:0000}", Fecha.Year)} {string.Format("{0:00}", Fecha.Month)} {string.Format("{0:00}", Fecha.Day)} - " +
            $"{string.Format("{0:00}",Fecha.Hour)} {string.Format("{0:00}",Fecha.Minute)} - {Nombre}.bak";
    }
}
