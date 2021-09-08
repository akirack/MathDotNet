using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public static class Statistics
    {

        #region Metodos auxiliares
        /// <summary>
        /// Retorna um array  contendo o conjunto de dados do IEnumerable.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>Faz uma copia do conjunto de dados e retorna o conjunto data como array.</returns>
        private static double[] ConvertToArray(IEnumerable<double> data)
        {
            return data.ToArray();
        }

        /// <summary>
        /// Retorna um array contendo o conjunto de dados ordenados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna um array ordenados dos dados de entrada.</returns>
        private static double[] SortArray(IEnumerable<double> data)
        {
            double[] d = ConvertToArray(data);
            Array.Sort(d);

            return d;
        }
        #endregion

        #region Medidas de tendencia central
        /// <summary>
        /// Retorna a media aritimetica do conjunto de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna a media aritimetica dos dados de entrada.</returns>
        public static double Media(IEnumerable<double> data)
        {
            double m = 0;
            int t = 0;

            foreach (double i in data)
            {
                m += i;
                t++;
            }

            return m / t;
        }

        /// <summary>
        /// Retorna a media ponderada do conjunto de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <param name="weight">Conjunto pesos</param>
        /// <returns>retorna a media ponderada dos dados de entrada.</returns>
        public static double MediaPonderada(IEnumerable<double> data, IEnumerable<double> weight)
        {
            double m = 0;
            double t = 0;

            var zip = data.Zip(weight, (d, w) => new { D = d, W = w });

            foreach (var i in zip)
            {
                m += i.D*i.W;
                t += i.W;
            }

            return m / t;
        }

        /// <summary>
        /// Retorna a media geometrica do conjunto de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna a media geometrica dos dados de entrada.</returns>
        public static double MediaGeometrica(IEnumerable<double> data)
        {
            double m = 0;
            int t = 0;

            foreach (double i in data)
            {
                m *= i;
                t++;
            }

            return Math.Pow(m,1/t);
        }

        /// <summary>
        /// Retorna a media harmonica do conjunto de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna a media harmonica dos dados de entrada.</returns>
        public static double MediaHarmonica(IEnumerable<double> data)
        {
            double m = 0;
            int t = 0;

            foreach (double i in data)
            {
                m += 1/i;
                t++;
            }

            return t/m;
        }

        /// <summary>
        /// Retorna a mediana do conjunto de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna a mediana dos dados de entrada.</returns>
        public static double Mediana(IEnumerable<double> data)
        {
            double[] m = SortArray(data);
            int p = Convert.ToInt32(m.Length/2);

            if((m.Length % 2) != 0)
            {
                return p;
            }

            else //if ((m.Length % 2) == 0)
            {
                return (m[p - 1] + m[p])/2;
            }
        }
        
        /// <summary>
        /// Retorna a Moda  do conjunto de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna a moda dos dados de entrada.</returns>
        public static double Moda(IEnumerable<double> data)
        {
            int a = 0, b = 0;
            double m = 0;

            foreach(var i in data)
            {
                foreach (var j in data)
                {
                    if (j == i)
                    {
                        a++;
                    }

                    else if (a > b)
                    {
                        b = a;
                        m = j;
                    }
                }
            }

            return m;
        }

        #endregion

        #region Dispercao de dados

        /// <summary>
        /// Retorna a amplitude do conjunto de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna a amplitude dos dados de entrada.</returns>
        public static double Amplitude(IEnumerable<double> data)
        {
            double[] s = SortArray(data);

            return s[s.Length - 1] - s[0];
        }

        /// <summary>
        /// Retorna a variancia amostral do conjunto de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna a variancia amostral dos dados de entrada.</returns>
        public static double VarianciaAmostral(IEnumerable<double> data)
        {
            double m = 0;
            int t = 0;
            double x = Media(data);


            foreach (double i in data)
            {
                m += (i - x)* (i - x);
                t++;
            }

            return m / (t - 1);
        }

        /// <summary>
        /// Retorna a variancia populacional do conjunto de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna a variancia populacional dos dados de entrada.</returns>
        public static double VarianciaPopulacional(IEnumerable<double> data)
        {
            double m = 0;
            int t = 0;
            double x = Media(data);

            foreach (double i in data)
            {
                m += (i - x) * (i - x);
                t++;
            }

            return m / t;
        }

        /// <summary>
        /// Retorna a Desvio padrao amostral do conjunto de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna a Desvio padrao amostral dos dados de entrada.</returns>
        public static double DesvioPadraAmostral(IEnumerable<double> data)
        {

            double v = VarianciaAmostral(data);

            return Math.Sqrt(v);
        }

        /// <summary>
        /// Retorna a Desvio padrao Populacional do conjunto de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna a Desvio padrao Populacional dos dados de entrada.</returns>
        public static double DesvioPadraPopulacional(IEnumerable<double> data)
        {

            double v = VarianciaPopulacional(data);

            return Math.Sqrt(v);
        }

        /// <summary>
        /// Retorna a Desvio medio do conjunto de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna a Desvio medio dos dados de entrada.</returns>
        public static double DesvioMedio(IEnumerable<double> data)
        {
            double m = 0;
            int t = 0;
            double x = Media(data);

            foreach (double i in data)
            {
                m += Math.Abs((i - x));
                t++;
            }

            return m / t;
        }

        /// <summary>
        /// Retorna o coeficiete de variacao do conjunto de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna a coeficiete de variacao em percentual dos dados de entrada.</returns>
        public static double CoeficieteDeVariacao(IEnumerable<double> data)
        {
       
            double x = Media(data);
            double d = DesvioPadraPopulacional(data);

            return (x/d)*100;
        }

        #endregion



        /// <summary>
        /// Retorna a covariancia entre os cojuntos de dados.
        /// </summary>
        /// <param name="dataX">Conjunto de dados 1</param>
        /// <param name="dataY">Conjunto de dados 2</param>
        /// <returns>retorna covariancia dos conjuntos de dados de entrada.</returns>
        public static double Covariancia(IEnumerable<double> dataX, IEnumerable<double> dataY)
        {
            double m = 0;
            double t = 0;
            double mX = Media(dataX);
            double mY = Media(dataY);


            var zip = dataX.Zip(dataY, (x, y) => new { X = x, Y = y });

            foreach (var i in zip)
            {
                m += (i.X - mX) * (i.Y - mY);
                t++;
            }

            return m / (t - 1);
        }

        /// <summary>
        /// Retorna o valor maximo entre os cojuntos de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna o  valor maximo dos conjuntos de dados de entrada.</returns>
        public static double Max(IEnumerable<double> data)
        {
            double[] s = SortArray(data);
            return s[s.Length - 1];
        }

        /// <summary>
        /// Retorna o valor minimo absoluto entre os cojuntos de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna o  valor minimo absoluto dos conjuntos de dados de entrada.</returns>
        public static double MaxAbsoluto(IEnumerable<double> data)
        {
            double m = double.NegativeInfinity;
        
            foreach (double i in data)
            {
                Math.Abs(i);

                if(i > m)
                {
                    m = i;
                }
            }

            return m;
        }

        /// <summary>
        /// Retorna o valor minimo entre os cojuntos de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna o  valor minimo dos conjuntos de dados de entrada.</returns>
        public static double Min(IEnumerable<double> data)
        {
            double[] s = SortArray(data);
            return s[0];
        }

        /// <summary>
        /// Retorna o valor minimo absoluto entre os cojuntos de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna o  valor minimo absolutos dos conjuntos de dados de entrada.</returns>
        public static double MixAbsoluto(IEnumerable<double> data)
        {
            double m = double.PositiveInfinity;

            foreach (double i in data)
            {
                Math.Abs(i);

                if (i < m)
                {
                    m = i;
                }
            }

            return m;
        }

        /// <summary>
        /// Retorna o valor da raiz media quadrada entre os cojuntos de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna o  valor raiz media quadrada dos conjuntos de dados de entrada.</returns>
        public static double RMS(IEnumerable<double> data)
        {
            double m = 0;
            int t = 0;

            foreach (double i in data)
            {
                m += Math.Pow(i,2);
                t++; 
            }

            return Math.Sqrt(m/t);
        }

        /// <summary>
        /// Retorna o valor do momento dos cojuntos de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <param name="n">numero correspodente ao momento</param>
        /// <returns>retorna o  valor raiz media quadrada dos conjuntos de dados de entrada.</returns>
        public static double Momento(IEnumerable<double> data, int n)
        {

            double media = Media(data);
            double s = 0;
            int t = 0;

            foreach(double i in data)
            {
                s += Math.Pow(i, n);
                t++;
            }
            return s / t;


        }

        /// <summary>
        /// Retorna o valor da kurtose dos cojuntos de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna o valor da kurtose  dos conjuntos de dados de entrada.</returns>
        public static double Kurtose(IEnumerable<double> data, int n)
        {

            double m = Media(data);
            double std = DesvioPadraPopulacional(data);
            double s = 0;
            int t = 0;

            foreach (double i in data)
            {
                s += Math.Pow(((i - m) / std), 4);
                t++;
            }
            return s / t;
        }

        /// <summary>
        /// Retorna o valor da Assimetria dos cojuntos de dados.
        /// </summary>
        /// <param name="data">Conjunto de dados</param>
        /// <returns>retorna o valor da Assimetria dos conjuntos de dados de entrada.</returns>
        public static double Assimetria(IEnumerable<double> data, int n)
        {

            double m = Media(data);
            double std = DesvioPadraPopulacional(data);
            double s = 0;
            int t = 0;

            foreach (double i in data)
            {
                s += Math.Pow((i - m), 3);
                t++;
            }

            return s / (t * Math.Pow(std, 3));
            //return (3 * (this.Media(param) - this.Mediana(param)) / this.DesvioPadrao(param));          
        }


    }
}


