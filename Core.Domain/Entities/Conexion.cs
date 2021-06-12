using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain.Entities
{
    public class Conexion:Entity
    {
        
        [Required (ErrorMessageResourceType = typeof(Language.Resources), ErrorMessageResourceName = "FieldRequired")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Language.Resources), ErrorMessageResourceName = "FieldRequired")]
        public string Servidor { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language.Resources), ErrorMessageResourceName = "FieldRequired")]
        [Range(1,99999,ErrorMessageResourceType = typeof(Language.Resources),ErrorMessageResourceName = "RangeError")]
        public int Puerto { get; set; }

        [Display(Name = "Base de Datos")]
        [Required(ErrorMessageResourceType = typeof(Language.Resources), ErrorMessageResourceName = "FieldRequired")]
        public string BaseDatos { get; set; }

        [Display(Name = "Directorio en el Servidor")]        
        [Required(ErrorMessageResourceType = typeof(Language.Resources), ErrorMessageResourceName = "FieldRequired")]
        public string Path { get; set; }

        public virtual List<Salvas> Salvas { get; set; } = new List<Salvas> { };
    }
}
