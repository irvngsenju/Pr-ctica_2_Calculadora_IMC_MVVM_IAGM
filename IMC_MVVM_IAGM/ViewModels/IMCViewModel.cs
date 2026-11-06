using IMC_MVVM_IAGM.Moldels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IMC_MVVM_IAGM.ViewModels
{
    public class IMCViewModel : INotifyPropertyChanged
    {
        //los entri captura es texto e incluso si son numeros

        public string ptext { get; set; } = "";

        public string estaturatext { get; set; } = "";

        public string resultado = "ingresa tus datos";

        public string Resultado
        {
            get { return resultado; }

            set
            {
                resultado = value;

                //avisamos al label que el resultado cambio

                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(
                        nameof(Resultado)));
            }
        }

        private string clasificacion = "";

        public string Clasificacion
        {
            get { return clasificacion; }
            set
            {
                clasificacion = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(
                        nameof(Clasificacion)));
            }
        }

        // El botón usará este comando mediante binding.
        public ICommand CalcularCommand { get; }
        public IMCViewModel()
        {
            // Al ejecutar el comando se llama a Calcular.
            CalcularCommand = new Command(Calcular);
        }

        // El binding escucha este evento.
        public event PropertyChangedEventHandler? PropertyChanged;

        private void Calcular()
        {
            // Validamos cada campo por separado.
            double peso;
            bool pesoValido = double.TryParse(ptext, out peso);
            if (pesoValido == false || !double.IsFinite(peso)
            || peso <= 0)
            {
                Resultado = "Escribe un peso válido mayor que cero.";
                clasificacion = "";
                return; // Termina sin hacer el cálculo.
            }
            double estatura;
            bool estaturaValida =
            double.TryParse(estaturatext, out estatura);
            if (estaturaValida == false
            || !double.IsFinite(estatura) || estatura <= 0)
            {
                Resultado = "Escribe una estatura válida en metros.";
                clasificacion = "";
                return;
            }

            // Creamos el modelo y le asignamos los datos.
            Persona persona = new Persona();
            persona.peso = peso;
            persona.estatura = estatura;
            // El modelo realiza la operación.
            double imc = persona.calcularIMC();
            // Evitamos mostrar un resultado numérico inválido.
            if (!double.IsFinite(imc))
            {
                Resultado = "Revisa el peso y la estatura.";
                return;
            }
            if (imc < 18.5)
            {
                Clasificacion = "Clasificación: Bajo peso";
            }
            else if (imc < 25)
            {
                Clasificacion = "Clasificación: Peso normal";
            }
            else if (imc < 30)
            {
                Clasificacion = "Clasificación: Sobrepeso";
            }
            else
            {
                Clasificacion = "Clasificación: Obesidad";
            }

            // F2 muestra el número con dos decimales.
            Resultado = "Tu IMC es: " + imc.ToString("F2");

           
        }

    }
}
