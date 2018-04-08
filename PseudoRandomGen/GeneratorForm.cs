using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PseudoRandomGen
{
    public partial class GeneratorForm : Form
    {
        public GeneratorForm()
        {
            InitializeComponent();
            Random rnd = new Random();
            // Теоретические значения мат. ожидания и среднеквадратического отклонения.
            double expValue = 0.5;
            double deviation = 1 / (2 * Math.Sqrt(3));
            // Инициализация модулей.
            long module1 = (long)Math.Pow(10, 8) + 1;
            long module2 = 2147483399;
            // Инициализация множителей "а".
            long a1 = 23;
            long a2 = 40692;
            #region Процесс генерации последовательности.
            // Первый датчик.
            var resList1_long = LinearTriggerGen.Generate(rnd.Next(1, 20), a1, module1);
            var resList1 = resList1_long.ConvertToQuasi(module1);
            // Второй датчик.
            var resList2_long = LinearTriggerGen.Generate(rnd.Next(1, 20), a2, module2);
            var resList2 = resList2_long.ConvertToQuasi(module2);
            // Первый и второй датчик (логику слияния см. в методе CombineMultiSeq).
            var resCombList = LinearTriggerGen.CombineMultiSeq(new List<List<long>> { resList1_long, resList2_long }, module1)
                                              .ConvertToQuasi(module1);
            // Встроенный генератор C#.
            var resListRnd = LinearTriggerGen.GenerateSystemRandom();
            #endregion
            #region Оценка периода последовательностей.
            // Период первой последовательности.
            var period1 = LinearTriggerGen.GetPeriod(resList1);
            // Период второй последовательности.
            var period2 = LinearTriggerGen.GetPeriod(resList2);
            // Период последовательности, полученной слиянием первого и второго датчика.
            var periodComb = LinearTriggerGen.GetPeriod(resCombList);
            #endregion
            #region Получение основных характеристик последовательностей.
            // Мат. ожидание.
            var seq1exp = LinearTriggerGen.ExpValue(resList1);
            var seq2exp = LinearTriggerGen.ExpValue(resList2);
            var seqCombexp = LinearTriggerGen.ExpValue(resCombList);
            var seqRndexp = LinearTriggerGen.ExpValue(resListRnd);
            // Среднеквадратическое отклонение.
            var seq1dev = LinearTriggerGen.StandardDeviation(resList1);
            var seq2dev = LinearTriggerGen.StandardDeviation(resList2);
            var seqCombdev = LinearTriggerGen.StandardDeviation(resCombList);
            var seqRnddev = LinearTriggerGen.StandardDeviation(resListRnd);
            #endregion
            #region Хи-квадрат (набл. и крит.)
            // Кол-во
            var chi2cnt1 = LinearTriggerGen.ChiSqrCount(resList1).PrintChiSqrCount();
            var chi2cnt2 = LinearTriggerGen.ChiSqrCount(resList2).PrintChiSqrCount();
            var chi2cntComb = LinearTriggerGen.ChiSqrCount(resCombList).PrintChiSqrCount();
            var chi2cntRnd = LinearTriggerGen.ChiSqrCount(resListRnd).PrintChiSqrCount();
            // Хи-квадрат набл.
            var chi2view1 = LinearTriggerGen.ChiSqrView(resList1).Sum();
            var chi2view2 = LinearTriggerGen.ChiSqrView(resList2).Sum();
            var chi2viewComb = LinearTriggerGen.ChiSqrView(resCombList).Sum();
            var chi2viewRnd = LinearTriggerGen.ChiSqrView(resListRnd).Sum();

            var chi2crit = LinearTriggerGen.InvChiSqr();
            #endregion

            // Вывод на форму.
            FirstTriggerChart.Series[0].ChartType = SeriesChartType.FastPoint;
            SecondTriggerChart.Series[0].ChartType = SeriesChartType.FastPoint;
            ComboTriggerChart.Series[0].ChartType = SeriesChartType.FastPoint;
            SystemRandomChart.Series[0].ChartType = SeriesChartType.FastPoint;
            for (int i = 0; i < resList1.Count - 1; i++)
            {
                FirstTriggerLB.Items.Add(resList1[i]);
                FirstTriggerChart.Series[0].Points.AddXY(resList1[i], resList1[i + 1]);
            }
            for (int i = 0; i < resList2.Count - 1; i++)
            {
                SecondTriggerLB.Items.Add(resList2[i]);
                SecondTriggerChart.Series[0].Points.AddXY(resList2[i], resList2[i + 1]);
            }
            for(int i = 0; i < resCombList.Count - 1; i++)
            {
                ComboTriggerLB.Items.Add(resCombList[i]);
                ComboTriggerChart.Series[0].Points.AddXY(resCombList[i], resCombList[i + 1]);
            }
            for (int i = 0; i < resListRnd.Count - 1; i++)
            {
                SystemRandomLB.Items.Add(resListRnd[i]);
                SystemRandomChart.Series[0].Points.AddXY(resListRnd[i], resListRnd[i + 1]);
            }
            FirstTriggerTB.Text = string.Format("m = {0}\r\na = {1}\r\nПериод\r\nпоследовательности =\r\n{2}\r\nМат. ожидание =\r\n{3}" +
                                                "\r\n(теор. значение = {4})" +
                                                "\r\nСреднеквадратическое\r\nотклонение = \r\n{5}" + "\r\n(теор. значение = {6})" +
                                                "\r\n\r\nХи-квадрат (набл.) =\r\n{7}\r\n" +
                                                "Хи-квадрат (крит.) =\r\n{8}",
                                                module1, a1, period1, seq1exp, expValue, seq1dev, deviation, chi2view1, chi2crit);
            FirstTriggerTB.Text += "\r\n\r\n" + chi2cnt1;
            SecondTriggerTB.Text = string.Format("m = {0}\r\na = {1}\r\nПериод\r\nпоследовательности =\r\n{2}\r\nМат. ожидание =\r\n{3}" +
                                                "\r\n(теор. значение = {4})" +
                                                "\r\nСреднеквадратическое\r\nотклонение = \r\n{5}"+ "\r\n(теор. значение = {6})" +
                                                "\r\n\r\nХи-квадрат (набл.) =\r\n{7}\r\n" +
                                                "Хи-квадрат (крит.) =\r\n{8}",
                                                module2, a2, period2, seq2exp, expValue, seq2dev, deviation, chi2view2, chi2crit);
            SecondTriggerTB.Text += "\r\n\r\n" + chi2cnt2;
            ComboTriggerTB.Text = string.Format("m = {0}\r\n\r\nПериод\r\nпоследовательности =\r\n{1}\r\nМат. ожидание =\r\n{2}" +
                                                "\r\nСреднеквадратическое\r\nотклонение = \r\n{3}\r\n\r\nХи-квадрат (набл.) =\r\n{4}\r\n" +
                                                "Хи-квадрат (крит.) =\r\n{5}",
                                                module1, periodComb, seqCombexp, seqCombdev, chi2viewComb, chi2crit);
            ComboTriggerTB.Text += "\r\n\r\n" + chi2cntComb;
            SystemRandomTB.Text = string.Format("Мат. ожидание =\r\n{0}" +
                                                "\r\nСреднеквадратическое\r\nотклонение = \r\n{1}\r\n\r\nХи-квадрат (набл.) =\r\n{2}\r\n" +
                                                "Хи-квадрат (крит.) =\r\n{3}",
                                                seqRndexp, seqRnddev, chi2viewRnd, chi2crit);
            SystemRandomTB.Text += "\r\n\r\n" + chi2cntRnd;
        }

        private void FirstTriggerChart_DoubleClick(object sender, EventArgs e)
        {
            ChartForm cf = new ChartForm();
            cf.TempChart.Series[0] = FirstTriggerChart.Series[0];
            cf.Text = cf.TempChart.Series[0].Name = "Первый датчик";
            cf.ShowDialog();
        }

        private void SecondTriggerChart_Click(object sender, EventArgs e)
        {
            ChartForm cf = new ChartForm();
            cf.TempChart.Series[0] = SecondTriggerChart.Series[0];
            cf.Text = cf.TempChart.Series[0].Name = "Второй датчик";
            cf.ShowDialog();
        }

        private void ComboTriggerChart_Click(object sender, EventArgs e)
        {
            ChartForm cf = new ChartForm();
            cf.TempChart.Series[0] = ComboTriggerChart.Series[0];
            cf.Text = cf.TempChart.Series[0].Name = "Комбинированный датчик";
            cf.ShowDialog();
        }

        private void SystemRandomChart_Click(object sender, EventArgs e)
        {
            ChartForm cf = new ChartForm();
            cf.TempChart.Series[0] = SystemRandomChart.Series[0];
            cf.Text = cf.TempChart.Series[0].Name = "Встроенный генератор";
            cf.ShowDialog();
        }
    }
}
