using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Materia
    {
        public int IdMateria { get; set; }
        [Display(Name="Nombre de la materia:")]
        [Required(ErrorMessage = "El nombre de la materia es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Costo de la materia:")]
        [Required(ErrorMessage = "El costo de la materia es requerido")]
        public decimal Costo { get; set; }
        public List<object> Materias { get; set; }

    }
}
