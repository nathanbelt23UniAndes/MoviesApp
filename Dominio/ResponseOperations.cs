using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio
{
   [NotMapped]
   public  class ResponseOperations
    {

        public bool Ok { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }

    }
}
