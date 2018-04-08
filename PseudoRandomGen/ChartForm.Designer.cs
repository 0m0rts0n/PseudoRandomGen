namespace PseudoRandomGen
{
    partial class ChartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.TempChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.InfoTB = new System.Windows.Forms.TextBox();
            this.InfoLB = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.TempChart)).BeginInit();
            this.SuspendLayout();
            // 
            // TempChart
            // 
            chartArea1.Name = "ChartArea1";
            this.TempChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.TempChart.Legends.Add(legend1);
            this.TempChart.Location = new System.Drawing.Point(12, 12);
            this.TempChart.Name = "TempChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.TempChart.Series.Add(series1);
            this.TempChart.Size = new System.Drawing.Size(599, 580);
            this.TempChart.TabIndex = 0;
            this.TempChart.Text = "chart1";
            // 
            // InfoTB
            // 
            this.InfoTB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InfoTB.Location = new System.Drawing.Point(942, 13);
            this.InfoTB.Multiline = true;
            this.InfoTB.Name = "InfoTB";
            this.InfoTB.ReadOnly = true;
            this.InfoTB.Size = new System.Drawing.Size(297, 579);
            this.InfoTB.TabIndex = 1;
            this.InfoTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // InfoLB
            // 
            this.InfoLB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InfoLB.FormattingEnabled = true;
            this.InfoLB.ItemHeight = 15;
            this.InfoLB.Location = new System.Drawing.Point(617, 16);
            this.InfoLB.Name = "InfoLB";
            this.InfoLB.Size = new System.Drawing.Size(318, 574);
            this.InfoLB.TabIndex = 2;
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 604);
            this.Controls.Add(this.InfoLB);
            this.Controls.Add(this.InfoTB);
            this.Controls.Add(this.TempChart);
            this.Name = "ChartForm";
            this.Text = "ChartForm";
            ((System.ComponentModel.ISupportInitialize)(this.TempChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart TempChart;
        private System.Windows.Forms.TextBox InfoTB;
        private System.Windows.Forms.ListBox InfoLB;
    }
}