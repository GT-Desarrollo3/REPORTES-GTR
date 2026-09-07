using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class clsEmpleado : clsPersona
    {        
        public String cargo { get; set; }
        public String empresa { get; set; }
        public String fechaIngreso { get; set; }
        public String fechaCese { get; set; }
    }
}
