using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Financy.Data
{
    public class Tipo
    {
        [Key]
        public int Tipo_Id { get; set; }

        public string Descripcion { get; set; }

        public int Cantidad { get; set; }

        public string Legend { get; set; }

    }
}
