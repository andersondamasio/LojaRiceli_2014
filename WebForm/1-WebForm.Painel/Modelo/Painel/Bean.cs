using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo
{
    public class Retorno : MensagemSistema
    {
        private Int32 Id_registro;
        private Object Objeto;

        public Object objeto
        {
            get { return Objeto; }
            set { Objeto = value; }
        }

        public Int32 id_registro
        {
            get { return Id_registro; }
            set { Id_registro = value; }
        }



    }
}