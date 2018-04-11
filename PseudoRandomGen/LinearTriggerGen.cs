using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace PseudoRandomGen
{
    /// <summary>
    /// Класс, предназначенный для генерации последовательности, оценки её периода и объединения нескольких последовательностей.
    /// </summary>
    static class LinearTriggerGen
    {
        #region Генерация последовательности методом линейного конгруэнтного датчика (задание 1.1).
        /// <summary>
        ///  Генерация последовательности методом линейного конгруэнтного датчика.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="startSeed">Начальное значение (как правило, подаётся случайно).</param>
        /// <param name="a">Параметр "a" (множитель).</param>
        /// <param name="module">Модуль.</param>
        /// <param name="c">Параметр "c" (приращение).</param>
        /// <param name="itersCount">Кол-во итераций (стандартно - 500).</param>
        /// <returns>Возвращает последовательность.</returns>
        public static List<T> Generate<T>(T startSeed, T a, T module, T c = default(T), int itersCount = 500)
        {
            var result = new List<T>();
            result.Add((dynamic)startSeed);
            dynamic mult = 0;
            for (int i = 1; i < itersCount; i++)
            {
                mult = Multiply((dynamic)a, result[i - 1]);
                result.Add((mult + c) % module);
            }
            return result;
        }
        #endregion
        #region Объединение нескольких сгенерированных последовательностей в общую (задание 1.2).
        /// <summary>
        /// Объединение нескольких сгенерированных последовательностей
        /// (линейных конгруэнтных датчиков) в общий (мультипликативный).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lists">Набор последовательностей.</param>
        /// <param name="module">Модуль.</param>
        /// <returns>Возвращает объединённую последовательность.</returns>
        public static List<long> CombineMultiSeq(List<List<long>> lists, long module)
        {
            var result = new List<long>();
            var cnt = lists[0].Count;
            for (int i = 0; i < cnt; i++)
            {
                var decreasing = 0.0;
                var mod = 0.0;
                for (int k = 0; k < lists.Count; k++)
                {
                    decreasing += Math.Pow((-1), k) * lists[k][i];
                }
                decreasing = Abs(decreasing);
                mod = decreasing % (module - 1);
                result.Add(Convert.ToInt64(mod));
            }
            return result;
        }
        #endregion
        #region Генерация последовательности с помощью метода NextDouble() класса Random (задание 1.3).
        /// <summary>
        /// Генерация последовательности с помощью метода NextDouble() класса Random.
        /// Ссылка: http://msdn.microsoft.com/ru-ru/library/system.random.aspx
        /// </summary>
        /// <param name="itersCount">Кол-во итераций (стандартно - 500).</param>
        /// <returns>Возвращает последовательность с квазиравномерным распределением в промежутке (0;1).</returns>
        public static List<double> GenerateSystemRandom(int itersCount = 500)
        {
            var result = new List<double>();
            Random rnd = new Random();
            for (int i = 0; i < itersCount; i++)
                result.Add(rnd.NextDouble());
            return result;
        }
        #endregion
        #region Операции с generic-переменными.
        static T Decrease<T>(T number1, T number2) => (dynamic)number1 - (dynamic)number2;
        static T Abs<T>(T number) => Math.Abs((dynamic)number);
        static T Mod<T>(T number1, T number2) => (dynamic)number1 % (dynamic)number2;
        static T Multiply<T>(T number1, T number2) => (dynamic)number1 * (dynamic)number2;
        #endregion
        #region Преобразование последовательности в последовательность с квазиравномерным распределением в промежутке (0;1).
        /// <summary>
        /// Преобразование последовательности в последовательность с квазиравномерным распределением в промежутке (0;1).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seq">Исходная последовательность.</param>
        /// <param name="module">Модуль.</param>
        /// <returns>Возвращает последовательность с квазиравномерным распределением в промежутке (0;1).</returns>
        public static List<double> ConvertToQuasi<T>(this List<T> seq, long module)
            => seq.Select(x => x.Equals(default(T)) ? (1 - (1 / Convert.ToDouble(module))) :
                                                      Convert.ToDouble(x) / module).ToList();
        #endregion
        #region Основные характеристики последовательности.
        /// <summary>
        /// Математическое ожидание.
        /// </summary>
        /// <param name="seq">Исходная последовательность.</param>
        /// <returns>Возвращает математическое ожидание последовательности.</returns>
        public static double ExpValue(List<double> seq) => seq.Average();
        public static double ExpValue(List<long> seq) => seq.Average();

        /// <summary>
        /// Среднеквадратическое отклонение.
        /// </summary>
        /// <param name="seq">Исходная последовательность.</param>
        /// <returns>Возвращает среднеквадратическое отклонение последовательности.</returns>
        public static double StandardDeviation(List<double> seq)
        {
            var disp = 0.0;
            foreach (var elem in seq)
                disp += Math.Pow(elem - seq.Average(), 2);
            disp /= (seq.Count - 1);
            return Math.Sqrt(disp);
        }
        public static double StandardDeviation(List<long> seq)
        {
            var disp = 0.0;
            foreach (var elem in seq)
                disp += Math.Pow(elem - seq.Average(), 2);
            disp /= (seq.Count - 1);
            return Math.Sqrt(disp);
        }
        public static double Disp(List<double> seq) => Math.Pow(StandardDeviation(seq), 2);
        public static double Disp(List<long> seq) => Math.Pow(StandardDeviation(seq), 2);
        /// <summary>
        /// Период последовательности.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seq">Исходная последовательность.</param>
        /// <returns>Возвращает период последовательности.</returns>
        public static long GetPeriod<T>(List<T> seq)
        {
            long period = 1;
            T firstVal = seq[0];
            for (int i = 1; i < seq.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(seq[i], firstVal)) return i + 1;
                else period++;
            }
            return period;
        }
        #endregion
        #region Хи-квадраты (наблюдения и критическое).
        /// <summary>
        /// Хи-квадрат (значение наблюдения).
        /// </summary>
        /// <param name="seq">Исходная последовательность.</param>
        /// <param name="piecesCount">Число промежутков на последовательности.</param>
        /// <returns>Возвращает хи-квадрат наблюдения на каждом промежутке.</returns>
        public static List<double> ChiSqrView(this List<double> seq, long piecesCount = 10)
        {
            var result = new List<double>();
            var np = seq.Count / piecesCount;
            double chi_sqr = 0;
            var cntList = ChiSqrCount(seq, piecesCount);

            for (int i = 0; i < piecesCount; i++)
            {
                chi_sqr = Math.Pow(cntList[i] - np, 2) / np;
                result.Add(chi_sqr);
            }
            return result;
        }
        /// <summary>
        /// Хи-квадрат (значение наблюдения) для распределения Пуассона.
        /// </summary>
        /// <param name="seq">Исходная последовательность.</param>
        /// <returns>Возвращает хи-квадрат наблюдения на каждом промежутке.</returns>
        public static List<double> ChiSqrViewP(this List<long> seq)
        {
            var result = new List<double>();
            var np = 0.0;
            double chi_sqr = 0;
            var cntList = ChiSqrCount(seq).Select(x => x.Value).ToList();
            var firstList = ChiSqrCount(seq).Take(cntList.Count - 1)
                                            .Select(x => Convert.ToInt32(x.Key.Substring(x.Key.IndexOf("[")+1, x.Key.IndexOf(";") - x.Key.IndexOf("[") - 1)))
                                            .ToList();
            var secondList = ChiSqrCount(seq).Take(cntList.Count - 1)
                                             .Select(x => Convert.ToInt32(x.Key.Substring(x.Key.IndexOf(";") + 1, x.Key.IndexOf(")") - x.Key.IndexOf(";") - 1)))
                                             .ToList();

            for (int i = 0; i < cntList.Count; i++)
            {
                double puassCoeff = 0.0;
                if (i < cntList.Count - 1)
                {
                    for (int k = firstList[i]; k <= secondList[i] - 1; k++)
                        puassCoeff += PuassonP(k);
                }
                else puassCoeff = PuassonP(i);
                    np = puassCoeff * seq.Count;
                chi_sqr = Math.Pow(cntList[i] - np, 2) / np;
                result.Add(chi_sqr);
            }
            return result;
        }
        /// <summary>
        /// Хи-квадрат (значение наблюдения) для показательного распределения.
        /// </summary>
        /// <param name="seq">Исходная последовательность.</param>
        /// <param name="lambda">Лямбда.</param>
        /// <returns>Возвращает хи-квадрат наблюдения на каждом промежутке.</returns>
        public static List<double> ChiSqrViewE(this List<double> seq, int lambda = 2)
        {
            var result = new List<double>();
            var np = 0.0;
            double chi_sqr = 0;
            var cntList = ChiSqrCountExp(seq).Select(x => x.Value).ToList();

            for (int i = 0; i < cntList.Count; i++)
            {
                np = (Math.Exp(-lambda * i) - Math.Exp(-lambda * (i+1))) * seq.Count;
                chi_sqr = Math.Pow(cntList[i] - np, 2) / np;
                result.Add(chi_sqr);
            }
            return result;
        }
        /// <summary>
        /// Частоты для каждого промежутка при показательном распределении.
        /// </summary>
        /// <param name="seq">Исходная последовательность.</param>
        /// <returns>Возвращает частоты для каждого промежутка при показательном распределении.</returns>
        public static Dictionary<string, long> ChiSqrCountExp(this List<double> seq)
        {
            var result = new Dictionary<string, long>();
            int cnt_seq_i = 0;
            var max = Convert.ToInt32(seq.Max());
            var index = 0;
            for (int i = 0; i <= max; i++)
            {
                index = i + 1;
                cnt_seq_i = seq.Where(x => x >= i && x < index).Count();
                if(cnt_seq_i == 0)
                {
                    index++;
                    cnt_seq_i = seq.Where(x => x >= i && x < index).Count();
                }
                string formatText = index > max ? string.Format("[{0};{1})", i, "+inf") :
                                                  string.Format("[{0};{1})", i, index);
                result.Add(formatText, cnt_seq_i);
                i = index - 1;
            }
            return result;
        }
        /// <summary>
        /// Частоты для каждого промежутка при равномерном распределении.
        /// </summary>
        /// <param name="seq">Исходная последовательность.</param>
        /// <param name="piecesCount">Кол-во промежутков.</param>
        /// <returns>Возвращает частоты для каждого промежутка при равномерном распределении.</returns>
        public static List<long> ChiSqrCount(this List<double> seq, long piecesCount = 10)
        {
            var result = new List<long>();
            int cnt_seq_i = 0;
            for (int i = 0; i < piecesCount; i++)
            {
                cnt_seq_i = seq.Where(x => (dynamic)x > Convert.ToDouble(i) / Convert.ToDouble(piecesCount) &&
                                           (dynamic)x < Convert.ToDouble(i + 1) / Convert.ToDouble(piecesCount)).Count();
                result.Add(cnt_seq_i);
            }
            return result;
        }
        /// <summary>
        /// Частоты для каждого промежутка при распределении Пуассона.
        /// </summary>
        /// <param name="seq">Исходная последовательность.</param>
        /// <returns>Возвращает частоты для каждого промежутка при распределении Пуассона.</returns>
        public static Dictionary<string, long> ChiSqrCount(this List<long> seq)
        {
            var result = new Dictionary<string, long>();
            int cnt_seq_i = 0;
            var max = seq.Max();
            var index = 0;
            for (int i = 0; i <= max; i++)
            {
                index = i + 1;
                cnt_seq_i = seq.Where(x => x >= i && x < index).Count();
                while (seq.Where(x => x >= i * 1L && x < index * 1L).Count() < 10 && index <= max)
                {
                    index++;
                    cnt_seq_i = seq.Where(x => x >= i * 1L && x < index * 1L).Count();
                }
                string formatText = index > max ? string.Format("[{0};{1})", i, "+inf") :
                                                  string.Format("[{0};{1})", i, index);
                result.Add(formatText, cnt_seq_i);
                i = index - 1;
            }
            return result;
        }
        /// <summary>
        /// Текстовый вывод промежутков (равномерное распределение).
        /// </summary>
        /// <param name="seq">Исходная последовательность.</param>
        /// <returns>Возвращает значения частот на промежутках в текстовом виде.</returns>
        public static string PrintChiSqrCount(this List<long> seq)
        {
            string result = "";
            for (int i = 0; i < seq.Count; i++)
                result += string.Format("Промежуток ({0}; {1}) = {2}\r\n",
                    Convert.ToDouble(i) / seq.Count, Convert.ToDouble(i + 1) / seq.Count, seq[i]);
            return result;
        }
        /// <summary>
        /// Текстовый вывод промежутков (показательное распределение и распределение Пуассона).
        /// </summary>
        /// <param name="dict">Исходный словарь, где ключ - промежуток СВ, а значение - частота встречаемой СВ.</param>
        /// <returns>Возвращает значения частот на промежутках в текстовом виде.</returns>
        public static string PrintChiSqrCount(this Dictionary<string, long> dict)
        {
            string result = "";
            foreach(var pair in dict)
                result += string.Format("Промежуток {0} = {1}\r\n", pair.Key, pair.Value);
            return result;
        }
        /// <summary>
        /// Хи-квадрат (критическое значение). Источник - функция InvCDF из библиотеки MathNet.Numerics.Distributions.
        /// </summary>
        /// <param name="piecesCount">Кол-во промежутков на последовательности (степени свободы + 1).</param>
        /// <param name="meanLevel">Уровень значимости (1 - доверительная вероятность).</param>
        /// <returns></returns>
        public static double InvChiSqr(long piecesCount = 10, double meanLevel = 0.05)
            => ChiSquared.InvCDF(piecesCount - 1, 1 - meanLevel);

        #endregion
        #region Генерация показательной последовательности и последовательности Пуассона.
        /// <summary>
        /// Генерация последовательности с показательным законом распределения методом обратных функций.
        /// </summary>
        /// <param name="seq">Исходная последовательность.</param>
        /// <param name="lambda">Параметр "лямбда" (по заданию "лямбда" = 2).</param>
        /// <param name="count">Кол-во элементов для вывода (по заданию кол-во = 100).</param>
        /// <returns>Возвращает последовательность с показательным законом распределения.</returns>
        public static List<double> ExpGenerate(this List<double> seq, int lambda = 2, int count = 100) =>
            seq.Select(x => -Math.Log(x) / lambda).Skip(count).Take(count).ToList();
        /// <summary>
        /// Генерация последовательности с показательным законом распределения методом просеивания фон Неймана.
        /// </summary>
        /// <param name="seq">Исходная последовательность.</param>
        /// <param name="lambda">Параметр "лямбда" (по заданию "лямбда" = 2).</param>
        /// <param name="count">Кол-во элементов для вывода (по заданию кол-во = 100).</param>
        /// <returns>Возвращает последовательность с показательным законом распределения.</returns>
        public static List<double> ExpFilterGenerate(int lambda = 2, int count = 100)
        {
            double a = 0;
            double m = lambda;
            Random rnd = new Random();
            double b = CalculateB();
            List<double> result = new List<double>();
            while(result.Count < count)
            {
                double prev = rnd.NextDouble();
                double curr = rnd.NextDouble();
                if (curr <= (1 / m) * getDensity(a + (b - a) * prev))
                    result.Add(a + (b - a) * prev);
            }
            return result;
        }
        /// <summary>
        /// Генерация последовательности с законом распределения Пуассона прямым методом.
        /// </summary>
        /// <param name="seq">Исходная последовательность.</param>
        /// <param name="a">Параметр "a" (по заданию "a" = 5).</param>
        /// <param name="n">Кол-во элементов для вывода (по заданию кол-во = 100).</param>
        /// <returns>Возвращает последовательность с законом распределения Пуассона.</returns>
        public static List<long> PuassonGenerate(this List<double> seq, int a = 5, int n = 100)
        {
            double first = 0;
            double second = 0;
            double coeff = Math.Exp(-a);
            List<long> result = new List<long>();
            foreach (double elem in seq)
            {
                first = 0;
                second = 0;
                for (int k = 0; k < n; k++)
                {
                    if (k == 0)
                    {
                        first = 0;
                        second = Math.Pow(a, k) / Factorial(k);
                    }
                    else
                    {
                        first += Math.Pow(a, k - 1) / Factorial(k - 1);
                        second = first + Math.Pow(a, k) / Factorial(k);
                    }
                    if (coeff * first < elem && elem <= coeff * second)
                    {
                        result.Add(k);
                        break;
                    }
                }
            }
            return result.Skip(n).Take(n).ToList();
        }
        /// <summary>
        /// Генерация последовательности с законом распределения Пуассона на основе предельной теоремы Пуассона.
        /// </summary>
        /// <param name="seq">Исходная последовательность.</param>
        /// <param name="a">Параметр "a" (по заданию "a" = 5).</param>
        /// <param name="n">Кол-во элементов для вывода (по заданию кол-во = 100).</param>
        /// <returns>Возвращает последовательность с законом распределения Пуассона.</returns>
        public static List<long> PuassonLimGenerate(this List<double> seq, int a = 5, int n = 100)
        {
            double Pn = Convert.ToDouble(a) / Convert.ToDouble(n);
            long count = 0;
            List<long> result = new List<long>();
            for (int i = 0; i < seq.Count / n; i++)
            {
                count = seq.Skip(i * n).Take(n).Where(x => x < Pn).Count();
                result.Add(count);
            }
            return result;
        }
        #endregion
        #region Прочие функции.
        static double PuassonP(int k, int alpha = 5) => Math.Pow(alpha, k) * Math.Exp(-alpha) / Factorial(k);
        // Плотность показательной функции.
        public static double getDensity(double x, double lambda = 2) => lambda * Math.Exp(-lambda * x);
        // Факториал.
        static long Factorial(int k)
        {
            if (k == 0) return 1;
            long result = 1;
            for (int i = 1; i <= k; i++)
                result *= i;
            return result;
        }
        static double CalculateB()
        {
            double eps = Math.Pow(10, -3);
            double b = 1;
            double x = 0;

            while (b > eps)
            {
                b = getDensity(x);
                x += 0.1;
            }

            return x;
        }
        #endregion
    }
}