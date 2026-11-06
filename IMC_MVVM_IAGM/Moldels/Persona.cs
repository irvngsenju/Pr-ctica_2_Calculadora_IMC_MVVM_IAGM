using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMC_MVVM_IAGM.Moldels
{
    public class Persona
    {
        //datos numerocos necesesarios para el calculo

        public double peso { get; set; }

        public double estatura { get; set; }



        public double calcularIMC()
        {
            return peso / (estatura * estatura);
        }
            
    }
}
