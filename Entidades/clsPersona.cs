using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public abstract class clsPersona
    {
        public string idPersona { get; set; }
        public string DNI { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Nombres { get; set; }
        public string Tipo { get; set; }
        public string item { get; set; }
       

        public clsPersona()
        {

        }
    }
}
