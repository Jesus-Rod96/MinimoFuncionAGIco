/////UNIVERSIDAD AUTÓNOMA DEL ESTADO DE MÉXICO/////
/////CENTRO UNIVERSITARIO UAEM ZUMPANGO//////
//------INGENIERÍA EN COMPUTACIÓN-------//
//------UA: ALGORITMOS GENETICOS 2018A-----//
//******ALUMNO: JESÚS RODOLFO RODRÍGUEZ BARRERA*******//
//******PROFESOR: ASDRÚBAL LÓPEZ CHAU*********//
//------FECHA: 06/ABRIL/2018
/*DESCRIPCION:Encuentra el mínimo de la función f(x)=cos(x)*e^{-pi*x/100} en el intervalo de x [-10 a 10]
Usa la representación real con codificación Gray.
Implementa todo en C#.
Parámetros del AG:
N = 100, Total de generaciones = 500, Mecanismo de selección: Ruleta, Mutación 10%, Elitismo.*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minimoFuncion
{
    class Program
    {
        public static void Main(string[] args)
        {
            int TAMN = 100;
            int TAM2 = 2;
            int GENERACIONES = 500;
            //int iteraciones;
            double r; //Variable para generar un numero aleatorio r entre 0 y T(suma de valores esperados)
            double sumaAptitud, sumaAptitud2; //Variables para la suma de aptitudes
            Individuo[] individuo = new Individuo[TAMN]; //Se declara el arreglo de los primeros 100 individuos
            Individuo[] individuos2 = new Individuo[TAMN]; //Se declaran los segundos individuos para las nuevas generaciones
            Individuo[] individuoHijos = new Individuo[TAM2]; //Declaracion de los individuos que seran resultado de la cruza 
            //IndividuoGray indGray = new IndividuoGray(7); //Se declara el objeto del segundo individuo Gray
            for (int i = 0; i < TAMN; i++)
            { //Creacion de los primeros 100 individuos
                individuo[i] = new Individuo();
            }
            int aux;
            for (int iteraciones = 0; iteraciones < GENERACIONES; iteraciones++) //Realizar las iteraciones para las 500 generaciones
            {
                sumaAptitud = 0;
                for (int i = 0; i < TAMN; i++)
                { //Calculo de la suma de aptitudes
                    individuo[i].calculaAptitud();
                    sumaAptitud += individuo[i].getAptitud();
                }
                for (int i = 0; i < TAMN; i++) //Se eligen los mejores candidatos para la cruza
                {
                    sumaAptitud2 = 0;
                    aux = -1;
                    r = new Random(Guid.NewGuid().GetHashCode()).NextDouble() * (sumaAptitud);
                    while (sumaAptitud2 < r) /*Se realiza la condicion para para que el programa se detenga
                        hasta que la suma de las aptitudes sea mayor a r y se elige ese individuo*/ 
                    {
                        aux++;
                        sumaAptitud2 += individuo[aux].getAptitud();
                    }
                    individuos2[i] = individuo[aux];
                }
                aux = 0;

                for (int i = 0; i < TAMN; i++) /*Si alguno de los individuos es optimo seguira a la
                    siguiente generacion*/
                {
                    individuos2[i].calculaAptitud();
                    if (individuos2[i].getAptitud() <= 1 && individuos2[i].getAptitud() >= -1)
                    {
                        individuo[aux] = individuos2[i];
                        aux++;
                    }
                }
                //Se llena la siguiente generacion con la cruza de los individuos seleccionados
                while (aux < 99)
                {
                    individuoHijos = individuos2[new Random(Guid.NewGuid().GetHashCode()).Next(0, 100)].Cross(individuos2[new Random(Guid.NewGuid().GetHashCode()).Next(0, 100)]);
                    //Los hijos obtenidos pasan a la siguiente generacion
                    individuo[aux] = individuoHijos[0];
                    aux++;
                    individuo[aux] = individuoHijos[1];
                    aux++;
                }
                //Elitismo al 10% de la poblacion
                for (int i = 0; i < 10; i++)
                {
                    individuo[new Random(Guid.NewGuid().GetHashCode()).Next(0, 100)].mutacion();
                }
                //Console.WriteLine(individuo.ToString()); //Imprime en consola los datos del primer individuo
                //Console.WriteLine(indGray.ToString()); //Imprime en consola los datos del individuo Gray
            }
            //Se calculan las aptitudes finales
            for (int i =0; i<TAMN;i++) {
                individuo[i].calculaAptitud();
            }
            //Se muestran los resultados obtenidos
            for (int i=0;i<TAMN;i++) {
                Console.WriteLine("Individuo: " + individuo[i].ToString() + " Aptitud: " + individuo[i].getAptitud());
            }

            for (int i = 0; true;) { } // Se utiliza para pausar la consola y que no se cierre
        }
    }
}
