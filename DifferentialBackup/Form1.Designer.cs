namespace DifferentialBackup
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.папкаХраненияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFBup = new System.Windows.Forms.Button();
            this.btnPBup = new System.Windows.Forms.Button();
            this.btnLOG = new System.Windows.Forms.Button();
            this.brnRe = new System.Windows.Forms.Button();
            this.RoundChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.brnRst = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RoundChart)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFBup
            // 
            this.btnFBup.Location = new System.Drawing.Point(597, 4);
            this.btnFBup.Name = "btnFBup";
            this.btnFBup.Size = new System.Drawing.Size(125, 45);
            this.btnFBup.TabIndex = 4;
            this.btnFBup.Text = "Полная резервная копия";
            this.btnFBup.UseVisualStyleBackColor = true;
            this.btnFBup.Click += new System.EventHandler(this.btnFBup_Click);
            // 
            // btnPBup
            // 
            this.btnPBup.Location = new System.Drawing.Point(597, 55);
            this.btnPBup.Name = "btnPBup";
            this.btnPBup.Size = new System.Drawing.Size(125, 45);
            this.btnPBup.TabIndex = 5;
            this.btnPBup.Text = "Разностная резервная копия";
            this.btnPBup.UseVisualStyleBackColor = true;
            this.btnPBup.Click += new System.EventHandler(this.btnPBup_Click);
            // 
            // btnLOG
            // 
            this.btnLOG.Location = new System.Drawing.Point(597, 106);
            this.btnLOG.Name = "btnLOG";
            this.btnLOG.Size = new System.Drawing.Size(125, 45);
            this.btnLOG.TabIndex = 6;
            this.btnLOG.Text = "Журнал транзакций";
            this.btnLOG.UseVisualStyleBackColor = true;
            this.btnLOG.Click += new System.EventHandler(this.btnLOG_Click);
            // 
            // brnRe
            // 
            this.brnRe.Location = new System.Drawing.Point(597, 157);
            this.brnRe.Name = "brnRe";
            this.brnRe.Size = new System.Drawing.Size(125, 45);
            this.brnRe.TabIndex = 7;
            this.brnRe.Text = "Полное восстановление";
            this.brnRe.UseVisualStyleBackColor = true;
            this.brnRe.Click += new System.EventHandler(this.brnRe_Click);
            // 
            // RoundChart
            // 
            this.RoundChart.BackColor = System.Drawing.Color.Transparent;
            this.RoundChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.RoundChart.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            this.RoundChart.BorderSkin.PageColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.RoundChart.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.BackSecondaryColor = System.Drawing.Color.Transparent;
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            legend1.TitleBackColor = System.Drawing.Color.Transparent;
            this.RoundChart.Legends.Add(legend1);
            this.RoundChart.Location = new System.Drawing.Point(1, 0);
            this.RoundChart.Name = "RoundChart";
            series1.BackImageTransparentColor = System.Drawing.Color.Transparent;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series1.CustomProperties = "PieLabelStyle=Disabled";
            series1.EmptyPointStyle.CustomProperties = "PieLabelStyle=Outside";
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.SmartLabelStyle.Enabled = false;
            series1.SmartLabelStyle.MaxMovingDistance = 60D;
            this.RoundChart.Series.Add(series1);
            this.RoundChart.Size = new System.Drawing.Size(385, 261);
            this.RoundChart.TabIndex = 1;
            this.RoundChart.Text = "chart1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(315, 208);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 45);
            this.button1.TabIndex = 13;
            this.button1.Text = "Обновить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(392, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(199, 21);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // brnRst
            // 
            this.brnRst.Location = new System.Drawing.Point(597, 208);
            this.brnRst.Name = "brnRst";
            this.brnRst.Size = new System.Drawing.Size(125, 45);
            this.brnRst.TabIndex = 15;
            this.brnRst.Text = "Откатить БД";
            this.brnRst.UseVisualStyleBackColor = true;
            this.brnRst.Click += new System.EventHandler(this.brnRst_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 261);
            this.Controls.Add(this.brnRst);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.brnRe);
            this.Controls.Add(this.btnLOG);
            this.Controls.Add(this.btnPBup);
            this.Controls.Add(this.btnFBup);
            this.Controls.Add(this.RoundChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Приложение для автоматического резервного копирования";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RoundChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
		private System.Windows.Forms.ToolStripMenuItem папкаХраненияToolStripMenuItem;
        private System.Windows.Forms.Button btnFBup;
        private System.Windows.Forms.Button btnPBup;
        private System.Windows.Forms.Button btnLOG;
        private System.Windows.Forms.Button brnRe;
        private System.Windows.Forms.DataVisualization.Charting.Chart RoundChart;
        private System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button brnRst;
    }
}

