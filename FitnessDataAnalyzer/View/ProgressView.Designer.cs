namespace FitnessDataAnalyzer.View
{
    partial class ProgressView
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
         System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
         System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
         System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
         System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
         this.mainTLP = new System.Windows.Forms.TableLayoutPanel();
         this.buttonTLP = new System.Windows.Forms.TableLayoutPanel();
         this.btnLoadWatchData = new System.Windows.Forms.Button();
         this.btnLoadFitnotesData = new System.Windows.Forms.Button();
         this.TreeView = new System.Windows.Forms.TreeView();
         this.ExerciseChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
         this.statusStrip1 = new System.Windows.Forms.StatusStrip();
         this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
         this.mainTLP.SuspendLayout();
         this.buttonTLP.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.ExerciseChart)).BeginInit();
         this.statusStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // mainTLP
         // 
         this.mainTLP.ColumnCount = 2;
         this.mainTLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
         this.mainTLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
         this.mainTLP.Controls.Add(this.buttonTLP, 0, 0);
         this.mainTLP.Controls.Add(this.TreeView, 0, 1);
         this.mainTLP.Controls.Add(this.ExerciseChart, 1, 1);
         this.mainTLP.Dock = System.Windows.Forms.DockStyle.Fill;
         this.mainTLP.Location = new System.Drawing.Point(0, 0);
         this.mainTLP.Name = "mainTLP";
         this.mainTLP.RowCount = 2;
         this.mainTLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
         this.mainTLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
         this.mainTLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
         this.mainTLP.Size = new System.Drawing.Size(1484, 761);
         this.mainTLP.TabIndex = 0;
         // 
         // buttonTLP
         // 
         this.buttonTLP.ColumnCount = 2;
         this.mainTLP.SetColumnSpan(this.buttonTLP, 2);
         this.buttonTLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
         this.buttonTLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
         this.buttonTLP.Controls.Add(this.btnLoadWatchData, 0, 0);
         this.buttonTLP.Controls.Add(this.btnLoadFitnotesData, 1, 0);
         this.buttonTLP.Dock = System.Windows.Forms.DockStyle.Fill;
         this.buttonTLP.Location = new System.Drawing.Point(3, 3);
         this.buttonTLP.Name = "buttonTLP";
         this.buttonTLP.RowCount = 1;
         this.buttonTLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
         this.buttonTLP.Size = new System.Drawing.Size(1478, 27);
         this.buttonTLP.TabIndex = 1;
         // 
         // btnLoadWatchData
         // 
         this.btnLoadWatchData.Location = new System.Drawing.Point(3, 3);
         this.btnLoadWatchData.Name = "btnLoadWatchData";
         this.btnLoadWatchData.Size = new System.Drawing.Size(120, 23);
         this.btnLoadWatchData.TabIndex = 0;
         this.btnLoadWatchData.Text = "Load Watch Data...";
         this.btnLoadWatchData.UseVisualStyleBackColor = true;
         // 
         // btnLoadFitnotesData
         // 
         this.btnLoadFitnotesData.Location = new System.Drawing.Point(129, 3);
         this.btnLoadFitnotesData.Name = "btnLoadFitnotesData";
         this.btnLoadFitnotesData.Size = new System.Drawing.Size(120, 23);
         this.btnLoadFitnotesData.TabIndex = 1;
         this.btnLoadFitnotesData.Text = "Load FitNotes Data...";
         this.btnLoadFitnotesData.UseVisualStyleBackColor = true;
         // 
         // TreeView
         // 
         this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.TreeView.Location = new System.Drawing.Point(3, 36);
         this.TreeView.Name = "TreeView";
         this.TreeView.Size = new System.Drawing.Size(121, 722);
         this.TreeView.TabIndex = 2;
         // 
         // ExerciseChart
         // 
         chartArea4.Name = "ChartArea1";
         this.ExerciseChart.ChartAreas.Add(chartArea4);
         this.ExerciseChart.Dock = System.Windows.Forms.DockStyle.Fill;
         legend4.Name = "Legend1";
         this.ExerciseChart.Legends.Add(legend4);
         this.ExerciseChart.Location = new System.Drawing.Point(130, 36);
         this.ExerciseChart.Name = "ExerciseChart";
         series7.ChartArea = "ChartArea1";
         series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
         series7.Legend = "Legend1";
         series7.Name = "HighActivitySeries";
         series7.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
         series8.ChartArea = "ChartArea1";
         series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
         series8.Legend = "Legend1";
         series8.Name = "LowActivitySeries";
         series8.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
         series8.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
         this.ExerciseChart.Series.Add(series7);
         this.ExerciseChart.Series.Add(series8);
         this.ExerciseChart.Size = new System.Drawing.Size(1351, 722);
         this.ExerciseChart.TabIndex = 3;
         this.ExerciseChart.Text = "chart1";
         // 
         // statusStrip1
         // 
         this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
         this.statusStrip1.Location = new System.Drawing.Point(0, 739);
         this.statusStrip1.Name = "statusStrip1";
         this.statusStrip1.Size = new System.Drawing.Size(1484, 22);
         this.statusStrip1.TabIndex = 1;
         this.statusStrip1.Text = "statusStrip1";
         // 
         // toolStripStatusLabel1
         // 
         this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
         this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
         // 
         // ProgressView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1484, 761);
         this.Controls.Add(this.statusStrip1);
         this.Controls.Add(this.mainTLP);
         this.MinimumSize = new System.Drawing.Size(600, 800);
         this.Name = "ProgressView";
         this.Text = "Progress";
         this.mainTLP.ResumeLayout(false);
         this.buttonTLP.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.ExerciseChart)).EndInit();
         this.statusStrip1.ResumeLayout(false);
         this.statusStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

        }

      #endregion

      private System.Windows.Forms.TableLayoutPanel mainTLP;
      private System.Windows.Forms.TableLayoutPanel buttonTLP;
      private System.Windows.Forms.Button btnLoadWatchData;
      private System.Windows.Forms.Button btnLoadFitnotesData;
      private System.Windows.Forms.TreeView TreeView;
      private System.Windows.Forms.DataVisualization.Charting.Chart ExerciseChart;
      private System.Windows.Forms.StatusStrip statusStrip1;
      private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
   }
}

