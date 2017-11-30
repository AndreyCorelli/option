using System;
using System.IO;
using System.Windows.Forms;
using OptionCalculator.Model;

namespace OptionCalculator
{
    public partial class MainForm : Form
    {
        private SourceCandles candles = new SourceCandles();

        public MainForm()
        {
            InitializeComponent();
            cbSide.SelectedIndex = 1;
        }

        private void btnExplore_Click(object sender, EventArgs e)
        {
            if (File.Exists(tbPath.Text))
                openFileDialog.FileName = tbPath.Text;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            tbPath.Text = openFileDialog.FileName;
            if (!candles.ReadCandles(tbPath.Text))
                MessageBox.Show("Source data was not read");
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (candles.sourceCandles.Count == 0) return;

            var side = cbSide.SelectedIndex == 0 ? Side.Put : Side.Call;
            var price = tbPrice.Text.Replace(',', '.').Replace(" ", "").ToDoubleUniform();
            var strike = tbStrike.Text.Replace(',', '.').Replace(" ", "").ToDoubleUniform();
            var term = tbTerm.Text.Replace(',', '.').Replace(" ", "").ToDoubleUniform();
            var volume = tbVolume.Text.Replace(',', '.').Replace(" ", "").ToDoubleUniform();

            var calc = new Calculator();
            var prem = calc.CalcPremium(candles, cbDetrend.Checked, side, volume, term, 
                price, strike, ExecutablePath.Combine("option_test.txt"));
            tbPremium.Text = $"{prem:F4}";
        }

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
