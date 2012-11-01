using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Financy.Data
{
    public class Cuenta
    {
        [Key]
        public int Cuenta_Id { get; set; }

        [DisplayName("Cuenta")]
        public string Descripcion { get; set; }

        [DisplayName("Monto Inicial")]
        [DataType(DataType.Currency)]
        public double Monto { get; set; }

        //[DisplayName("Balance Actual")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        [DataType(DataType.Currency)]
        public double Balanc { get; set; }

        public string User { get; set; }
    }
}
