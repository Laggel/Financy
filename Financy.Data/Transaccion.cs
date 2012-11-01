using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Financy.Data
{
    public class Transaccion
    {
        [Key]
        public int Transaccion_Id { get; set; }

        public int Cuenta_Id { get; set; }

        public virtual Cuenta Cuenta { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [DataType(DataType.Currency)]
        public double Monto { get; set; }

        public int TransaccionProyectada_Id { get; set; }

        public virtual TransaccionProyectada TransaccionProyectada { get; set; }

        public string Estado { get; set; }

    }
}
