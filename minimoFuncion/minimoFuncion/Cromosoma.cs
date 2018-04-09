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
    class Cromosoma
    {
        public int[] gene; //Declaracion del arreglo de genes
        private int BITS_PER_GENE;/*Declaracion de la variable entera 
        la cual sera el numero de bits que tendra el gen*/
        private float min, max; //Variables del intervalo [-10,10]
        private int particiones; //variable para el numero de partes

        public Cromosoma[] gen;
        public Cromosoma(int Gen) {
            gen = new Cromosoma[Gen];
            float min = -10;
            float max = 10;
            int particiones = 300;
            BITS_PER_GENE = (int)(Math.Log(particiones) / Math.Log10(2)); //Declaracion del numero de bits por gene
            gene = new int[BITS_PER_GENE]; //aSIGNACION DE MEMORIA
            for (int i = 0; i < Gen; i++)
            {
                crearGenes(i,min,max,particiones);
            }
        }

        public void crearGenes(int numeroGen, float min, float max, int particiones) {
            gen[numeroGen]= new Cromosoma(numeroGen);
            gen[numeroGen].inicializa();
        }
        public void inicializa() //Con esta funcion se llena el gen con numeros aleatorios
        {
            Random random = new Random();
            int n;
            for (int i = 0; i < BITS_PER_GENE; i++)
            {
                n = random.Next(0, 2); //Se guarda temporalmente el numero generado
                if (n == 1) //Si es uno se agrega al gen en la posicion de i 
                {
                    gene[i] = 1;
                }
            }
        }

        public double GetValue() /*Con esta funcion se devuelve 
            el valor decimal del gen*/
        {
            double value = 0;
            float deltaX; //Declaracion de la variable a calcular
            int[] gene = new int[BITS_PER_GENE];
            gene = getGeneBin();
            deltaX = Math.Abs(min-max) / particiones; /*Se realiza la operacion:
            deltaX=|min-max|/num Particiones*/
            for (int i = 0; i < BITS_PER_GENE - 1; i++) /*Se recorre el tamaño 
                de los bits que tiene el gen*/
            {
                value += gene[BITS_PER_GENE - i - 1] * Math.Pow(2.0, i); /*Se eleva a la potencia correspondiente 
                cada uno de los valores del gen*/
            }
            value = min + (deltaX * value);
            return value; //Retorna el valor de gen en decimal
        }

        public string getGene() //Regresa el valor del gen
        {
            string genotipo = ""; /*Se declara una cadena donde se guardara el gen*/
            for (int i = 0; i < BITS_PER_GENE; i++) /*Se recorre todo el arreglo del gen */
            {
                genotipo += gene[i].ToString(); /*Se agregan los valores del gen*/
            }
            return genotipo; //Regresa la cadane con el valor del gen
        }

        public int[] getGeneBin() //Funcion para obtener el valor en binario del gen
        {
            int[] gen = new int[BITS_PER_GENE];
            gen[0] = gene[0];
            gen[1] = gene[1];
            for (int i = 1; i < BITS_PER_GENE - 1; i++)//Recorre todo el arreglo del gen
            {
                if (gen[i] != gene[i + 1]) /*El algoritmo dice que se aplique una XOR al bit y un bit 
                    posterior si el resultado es verdadero se agrega 1 al codigo gray*/
                    gen[i+1] = 1;
                else //sino se cumple la condicion entonces se  agrega un cero
                    gen[i+1] = 0;
            }
            return gen;  //Retorna la cadena  
        }

        //Metodo set de la variable gene
        public void setGene(int[] gen)
        {
            gene = gen;
        }
        //Metodo get de la variable de BITS_PER_GENE
        public int getBits_Per_Gene() 
        {
            return BITS_PER_GENE;
        }
        //Metodo para imprimir los datos que se necesitan
        public override string ToString() 
        {
            //Se retornar los datos que se mostraran en consola
            string cadena = "El Cromosoma es [";
            for (int i = 0; i < gen.Length; i++) {
                cadena += gen[i].getGene();        
            }
            cadena += "]";
            for (int i = 0; i < gen.Length; i++) {
                cadena += gen[i].GetValue();
                cadena += ",";        
            }
            return cadena;
        }

    }
}
