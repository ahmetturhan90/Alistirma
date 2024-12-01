using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRabbit
{
    public class Operation
    {
        public Guid? OrderEntryById { get; set; }
    }

    public class ProductValidation
    {
        public Guid? ProductId { get; set; }
    }
}
