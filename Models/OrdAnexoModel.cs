using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SisEngeman.Models
{
    public class OrdAnexoModel
    {
        [Column(TypeName = "varbinary(max)")]
        public byte[] anexo { get; set; }

        [Key, Column(Order = 0)]
        public decimal codEmp { get; set; }

        [Key, Column(Order = 1)]
        public decimal codOrd { get; set; }

        public DateTime? datAlt { get; set; }

        [Key, StringLength(100)]
        public string descricao { get; set; }

        public string descricaores { get; set; }

        //public int tag { get; set; }
    }
}
