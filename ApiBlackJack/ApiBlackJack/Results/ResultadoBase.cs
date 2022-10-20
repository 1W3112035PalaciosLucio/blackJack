using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlackJack.Results
{
    public class ResultadoBase
    {
        public string Error { set; get; }
        public int CodigoEstado { set; get; }
        public bool Ok { set; get; }

        public void setOk()
        {
            this.CodigoEstado = 200;
            this.Error = "No hay errores";
            this.Ok = true;
        }

        public void setError(string error)
        {
            this.CodigoEstado = 400;
            this.Error = error;
            this.Ok = false;
        }
    }
}
