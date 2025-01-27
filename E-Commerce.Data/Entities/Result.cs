using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Entities
{
    public class Result
    {
        public string Text { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; }
        public object GenericObject { get; set; }
    }
}
