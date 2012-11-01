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

        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public double Monto { get; set; }
        
        public string EntradaProyectada_Id { get; set; }

        public virtual EntradaProyectada EntradaProyectada { get; set; }

        public string Estado { get; set; }

    }
}
