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
    public class Individuo
    {
        Cromosoma cromosoma; //Se declara un objeto de cromosoma 
        //int particiones;//variable para el numero de particiones
        private double aptitud, valorEsperado;//varibales para el minimo,maximo y el numero de bits

        public Individuo()//Funcion en donde se piden los datos a ingresar
        {
            cromosoma = new Cromosoma(1); //Se declara un objeto de tipo cromosoma
            //cromosoma.Cromosoma(1);//Se inicializa el cromosoma
        }

        //Esta funcion realizara la suma de las aptitudes
        public void calculaAptitud()
        {
            double x = this.cromosoma.GetValue();//obtenemos el valor del cromosoma
            aptitud = Math.Cos(x / 180) * Math.Pow(Math.E, ((-Math.PI * x) / 100));//funcion de aptitud
        }

        public void mutacion() //Esta funcion realizara la mutacion
        {
            int[] aux2;
            aux2 =this.cromosoma.getGeneBin();
            aux2[new Random(Guid.NewGuid().GetHashCode()).Next(0, this.cromosoma.getBits_Per_Gene())] =1;
            this.cromosoma.setGene(aux2);
        }
        //Metodo get de la variable aptitud
        public double getAptitud()
        {
            return aptitud;
        }
        //Metodo Set de la variable valorEsperado
        public void setValorEsperado(double valorE)
        {
            valorEsperado = valorE;
        }
        //Metodo get de la variable valorEsperado
        public double getValorEsperado()
        {
            return valorEsperado;
        }


        public Individuo[] Cross(Individuo madre)
        {
            Console.WriteLine(madre);
            Individuo[] hijos = new Individuo[2];//Se declara el arreglo de los  individuos hijos
            hijos[0] = new Individuo(); //Se crea el primer individuo hijo 0
            int[] gen = new int[madre.cromosoma.getBits_Per_Gene()];
            hijos[1] = new Individuo(); //Se crea el segundo individuo hijo 1
            int[] gen2 = new int[madre.cromosoma.getBits_Per_Gene()];
            //El arreglo madre se encargara de llamar al metodo
            int crossPoint = new Random(Guid.NewGuid().GetHashCode()).Next(3, 5); /*Se indica el punto en el
            cual se dividiran los arreglos para hacer la cruza*/
            Console.WriteLine("\n\tLa cruza es\n  ");
            for (int i = 0; i < madre.cromosoma.getBits_Per_Gene(); i++)
            {
                if (i <= crossPoint)
                {//Si el indice es <= al punto entonces se asignan las posiciones de cada hijo
                    gen[i]=Int32.Parse(this.cromosoma.getGene().ElementAt(i).ToString());
                    gen2[i]=Int32.Parse(madre.cromosoma.getGene().ElementAt(i).ToString());
                }
                else
                {//si no entonces las posiciones seguiran de la misma manera
                    gen[i]=Int32.Parse(madre.cromosoma.getGene().ElementAt(i).ToString());
                    gen2[i]=Int32.Parse(this.cromosoma.getGene().ElementAt(i).ToString());
                }
            }
            hijos[0].cromosoma.setGene(gen);
            hijos[1].cromosoma.setGene(gen2);
            return hijos; //Se retornan los hijos obtenidos
        }
        public override string ToString() //Metodo para imprimir los datos que se necesitan
        {
            //Se retornar los datos que se mostraran en consola
            return string.Format(cromosoma.ToString());
        }
    }
}
