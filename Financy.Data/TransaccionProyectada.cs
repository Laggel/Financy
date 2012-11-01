using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Financy.Data
{
    public class TransaccionProyectada
    {
        [Key]
        public int TransaccionProyectada_Id { get; set; }

        public int Cuenta_Id { get; set; }

        public virtual Cuenta Cuenta { get; set; }
        
        [DisplayName("Nombre")]
        public string Descripcion { get; set; }

        [DataType(DataType.Currency)]
        public double Monto { get; set; }

        public int Tipo_Id { get; set; }

        public Tipo Tipo { get; set; }

        [DisplayName("Fecha")]
        [Range(typeof(int), "1", "27")] 
        public int? Dia { get; set; }

    }
}
