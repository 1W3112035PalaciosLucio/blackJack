using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiBlackJack.Models
{
    public partial class Cartas
    {
        public Cartas()
        {

        }

        public int Id { get; set; }
        public int Valor { get; set; }
        public string Palo { get; set; }



    }
}
