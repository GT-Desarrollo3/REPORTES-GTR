using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccesoDatos;
using Entidades;

namespace Negocio
{
    public class clsFormularioBL
    {
        private clsFormularioBL()
        {

        }

        private readonly static clsFormularioBL instancia = new clsFormularioBL();

        public static clsFormularioBL Instancia
        {
            get { return instancia; }
        }

        public List<clsFormulario> consulta_Formularios_NoAsignados()
        {
            return clsFormularioDAO.Instancia.consulta_Formularios_NoAsignados();
        }

    }
}
