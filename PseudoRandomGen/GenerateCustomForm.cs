using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PseudoRandomGen
{
    public partial class GenerateCustomForm : Form
    {
        public GenerateCustomForm()
        {
            InitializeComponent();

            double expValueExp = 0.5, dispExp = 0.25, devExp = 0.5;
            double expValuePuass = 5, dispPuass = 5, devPuass = Math.Sqrt(5);

            Random rnd = new Random();
            // Теоретические значения мат. ожидания и среднеквадратического отклонения.
            var resListRnd = LinearTriggerGen.GenerateSystemRandom();
            // Генераторы по показательному распределению и распределению Пуассона.
            var resExp1 = resListRnd.ExpGenerate();
            var resExp2 = LinearTriggerGen.ExpFilterGenerate();
            var resPuass1 = resListRnd.PuassonGenerate();
            var tmp = LinearTriggerGen.GenerateSystemRandom(10000);
            var resPuass2 = tmp.PuassonLimGenerate();
            #region Получение основных характеристик последовательностей.
            var expValueExp1 = LinearTriggerGen.ExpValue(resExp1);
            var dispExp1 = LinearTriggerGen.Disp(resExp1);
            var devExp1 = LinearTriggerGen.StandardDeviation(resExp1);

            var expValueExp2 = LinearTriggerGen.ExpValue(resExp2);
            var dispExp2 = LinearTriggerGen.Disp(resExp2);
            var devExp2 = LinearTriggerGen.StandardDeviation(resExp2);

            var expValuePuass1 = LinearTriggerGen.ExpValue(resPuass1);
            var dispPuass1 = LinearTriggerGen.Disp(resPuass1);
            var devPuass1 = LinearTriggerGen.StandardDeviation(resPuass1);

            var expValuePuass2 = LinearTriggerGen.ExpValue(resPuass2);
            var dispPuass2 = LinearTriggerGen.Disp(resPuass2);
            var devPuass2 = LinearTriggerGen.StandardDeviation(resPuass2);
            #endregion

            var chi2cnt1 = LinearTriggerGen.ChiSqrCountExp(resExp1).PrintChiSqrCount();
            var chi2cnt2 = LinearTriggerGen.ChiSqrCountExp(resExp2).PrintChiSqrCount();
            var chi2cntpuass1 = LinearTriggerGen.ChiSqrCount(resPuass1).PrintChiSqrCount();
            var chi2cntpuass2 = LinearTriggerGen.ChiSqrCount(resPuass2).PrintChiSqrCount();

            var chi2exp1 = LinearTriggerGen.ChiSqrViewE(resExp1);
            var chi2exp2 = LinearTriggerGen.ChiSqrViewE(resExp2);
            var chi2puass1 = LinearTriggerGen.ChiSqrViewP(resPuass1);
            var chi2puass2 = LinearTriggerGen.ChiSqrViewP(resPuass2);

            var chi2exp1Sum = chi2exp1.Sum();
            var chi2exp2Sum = chi2exp2.Sum();
            var chi2puass1Sum = chi2puass1.Sum();
            var chi2puass2Sum = chi2puass2.Sum();

            var chi2crit = LinearTriggerGen.InvChiSqr();
            var chi2critExp1 = LinearTriggerGen.InvChiSqr(chi2exp1.Count);
            var chi2critExp2 = LinearTriggerGen.InvChiSqr(chi2exp2.Count);
            var chi2critPuass1 = LinearTriggerGen.InvChiSqr(chi2puass1.Count);
            var chi2critPuass2 = LinearTriggerGen.InvChiSqr(chi2puass2.Count);
            // Вывод на форму.
            ExpDistrChart.Series[0].ChartType = SeriesChartType.FastPoint;
            ExpDistrFNChart.Series[0].ChartType = SeriesChartType.FastPoint;
            PuasDistrChart.Series[0].ChartType = SeriesChartType.FastPoint;
            PuasDistrLimChart.Series[0].ChartType = SeriesChartType.FastPoint;

            for (int i = 0; i < resExp1.Count - 1; i++)
            {
                ExpDistrLB.Items.Add(resExp1[i]);
                ExpDistrChart.Series[0].Points.AddXY(Math.Round(resExp1[i], 2), LinearTriggerGen.getDensity(resExp1[i]));
            }
            for (int i = 0; i < resExp2.Count - 1; i++)
            {
                ExpDistrFNLB.Items.Add(resExp2[i]);
                ExpDistrFNChart.Series[0].Points.AddXY(Math.Round(resExp2[i], 2), LinearTriggerGen.getDensity(resExp2[i]));
            }
            for (int i = 0; i < resPuass1.Count - 1; i++)
            {
                PuasDistrLB.Items.Add(resPuass1[i]);
                PuasDistrChart.Series[0].Points.AddXY(resPuass1[i], resPuass1[i + 1]);
            }
            for (int i = 0; i < resPuass2.Count - 1; i++)
            {
                PuasDistrLimLB.Items.Add(resPuass2[i]);
                PuasDistrLimChart.Series[0].Points.AddXY(resPuass2[i], resPuass2[i + 1]);
            }
            ExpDistrTB.Text = string.Format(    "Мат. ожидание =\r\n{0}" +
                                                "\r\n(теор. значение = {1})" +
                                                "\r\nДисперсия = \r\n{2}" + "\r\n(теор. значение = {3})\r\n" +
                                                "Среднеквадратическое\r\nотклонение = \r\n{4}\r\n(теор. значение = {5})\r\n" +
                                                "Промежутки:\r\n{6}\r\n"+
                                                "\r\n\r\nХи-квадрат (набл.) =\r\n{7}\r\n" +
                                                "Хи-квадрат (крит.) =\r\n{8}",
                                                expValueExp1, expValueExp, dispExp1, dispExp, devExp1, devExp, chi2cnt1, chi2exp1Sum, chi2critExp1);
            ExpDistrFNTB.Text = string.Format(  "Мат. ожидание =\r\n{0}" +
                                                "\r\n(теор. значение = {1})" +
                                                "\r\nДисперсия = \r\n{2}" + "\r\n(теор. значение = {3})\r\n" +
                                                "Среднеквадратическое\r\nотклонение = \r\n{4}\r\n(теор. значение = {5})\r\n" +
                                                "Промежутки:\r\n{6}\r\n" +
                                                "\r\n\r\nХи-квадрат (набл.) =\r\n{7}\r\n" +
                                                "Хи-квадрат (крит.) =\r\n{8}",
                                                expValueExp2, expValueExp, dispExp2, dispExp, devExp2, devExp, chi2cnt2, chi2exp2Sum, chi2critExp2);
            PuasDistrTB.Text = string.Format(   "Мат. ожидание =\r\n{0}" +
                                                "\r\n(теор. значение = {1})" +
                                                "\r\nДисперсия = \r\n{2}" + "\r\n(теор. значение = {3})\r\n" +
                                                "Среднеквадратическое\r\nотклонение = \r\n{4}\r\n(теор. значение = {5})\r\n" +
                                                "Промежутки:\r\n{6}\r\n" +
                                                "\r\n\r\nХи-квадрат (набл.) =\r\n{7}\r\n" +
                                                "Хи-квадрат (крит.) =\r\n{8}",
                                                expValuePuass1, expValuePuass, dispPuass1, dispPuass, devPuass1, devPuass, chi2cntpuass1,
                                                chi2puass1Sum, chi2critPuass1);
            PuasDistrLimTB.Text = string.Format("Мат. ожидание =\r\n{0}" +
                                                "\r\n(теор. значение = {1})" +
                                                "\r\nДисперсия = \r\n{2}" + "\r\n(теор. значение = {3})\r\n" +
                                                "Среднеквадратическое\r\nотклонение = \r\n{4}\r\n(теор. значение = {5})\r\n" +
                                                 "Промежутки:\r\n{6}\r\n" +
                                                "\r\n\r\nХи-квадрат (набл.) =\r\n{7}\r\n" +
                                                "Хи-квадрат (крит.) =\r\n{8}",
                                                expValuePuass2, expValuePuass, dispPuass2, dispPuass, devPuass2, devPuass, chi2cntpuass2,
                                                chi2puass2Sum, chi2critPuass2);
        }

        private void ExpDistrChart_DoubleClick(object sender, EventArgs e)
        {
            ChartForm cf = new ChartForm();
            cf.TBInfo = ExpDistrTB.Text;
            cf.TempChart.Series[0] = ExpDistrChart.Series[0];
            cf.Text = cf.TempChart.Series[0].Name = ExpDistrLabel.Text;
            cf.ShowDialog();
        }

        private void ExpDistrFNChart_DoubleClick(object sender, EventArgs e)
        {
            ChartForm cf = new ChartForm();
            cf.TBInfo = ExpDistrFNTB.Text;
            cf.TempChart.Series[0] = ExpDistrFNChart.Series[0];
            cf.Text = cf.TempChart.Series[0].Name = ExpDistrFNLabel.Text;
            cf.ShowDialog();
        }

        private void PuasDistrChart_DoubleClick(object sender, EventArgs e)
        {
            ChartForm cf = new ChartForm();
            cf.TBInfo = PuasDistrTB.Text;
            cf.TempChart.Series[0] = PuasDistrChart.Series[0];
            cf.Text = cf.TempChart.Series[0].Name = PuasDistrLabel.Text;
            cf.ShowDialog();
        }

        private void PuasDistrLimChart_DoubleClick(object sender, EventArgs e)
        {
            ChartForm cf = new ChartForm();
            cf.TBInfo = PuasDistrLimTB.Text;
            cf.TempChart.Series[0] = PuasDistrLimChart.Series[0];
            cf.Text = cf.TempChart.Series[0].Name = PuasDistrLimLabel.Text;
            cf.ShowDialog();
        }
    }
}
