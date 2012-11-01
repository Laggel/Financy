using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Financy.Data
{
    public class EntradaProyectada
    {
        [Key]
        public int EntradaProyectada_Id { get; set; }

        public int Cuenta_Id { get; set; }

        public virtual Cuenta Cuenta { get; set; }

        public string Descripcion { get; set; }

        public double Monto { get; set; }

        public string Tipo { get; set; }

        public int? Dia { get; set; }

    }
}
