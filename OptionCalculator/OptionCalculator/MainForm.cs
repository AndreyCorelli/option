using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using OptionCalculator.Model;

namespace OptionCalculator
{
    public partial class MainForm : Form
    {
        private List<double> prices = new List<double>();

        public MainForm()
        {
            InitializeComponent();
            cbSide.SelectedIndex = 1;
        }

        /// <summary>
        /// pick the source data file and read prices in "prices" list
        /// for future calculations
        /// </summary>
        private void btnExplore_Click(object sender, EventArgs e)
        {
            if (File.Exists(tbPath.Text))
                openFileDialog.FileName = tbPath.Text;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            tbPath.Text = openFileDialog.FileName;
            prices = SourceCandles.ReadCandles(tbPath.Text);
            if (prices.Count == 0)
                MessageBox.Show("Source data was not read");
        }

        /// <summary>
        /// calculate the option's premium
        /// </summary>
        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (prices.Count < 3) return;

            var contract = new OptionContract
            {
                Side = cbSide.SelectedIndex == 0 ? Side.Put : Side.Call,
                Price = tbPrice.Text.Replace(',', '.').Replace(" ", "").ToDoubleUniform(),
                Strike = tbStrike.Text.Replace(',', '.').Replace(" ", "").ToDoubleUniform(),
                Term = tbTerm.Text.Replace(',', '.').Replace(" ", "").ToDoubleUniform(),
                Volume = tbVolume.Text.Replace(',', '.').Replace(" ", "").ToDoubleUniform(),
                YearTradeDays = tbDaysInYear.Text.ToInt()
            };

            var calc = new Calculator
            {
                iterationsCount = tbIterations.Text.ToInt()
            };
            var prem = calc.CalcPremium(prices, cbDetrend.Checked, contract, ExecutablePath.ExecPath);
            tbPremium.Text = $"{prem:F4}";
            tbHv.Text = $"{calc.HV:F4}";
        }

        /// <summary>
        /// make a random timeseries
        /// </summary>
        private void btnMakeSeries_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            var maker = new PriceHistoryMaker();
            var startPrice = tbTestStart.Text.Replace(',', '.').Replace(" ", "").ToDoubleUniform();
            var days = tbTestTerm.Text.ToInt();
            var delta = tbTestDailyDeltaPercent.Text.Replace(',', '.').Replace(" ", "").ToDoubleUniform();
            maker.BuildTrack(startPrice, delta / 100, DateTime.Today.AddDays(-days), DateTime.Today);
            maker.StoreTrack(saveFileDialog.FileName);
        }
    }
}
