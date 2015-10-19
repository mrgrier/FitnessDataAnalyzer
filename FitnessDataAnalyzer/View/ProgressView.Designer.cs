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
         this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
         this.SuspendLayout();
         // 
         // TableLayoutPanel
         // 
         this.TableLayoutPanel.ColumnCount = 1;
         this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
         this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
         this.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.TableLayoutPanel.Location = new System.Drawing.Point(0, 0);
         this.TableLayoutPanel.Name = "TableLayoutPanel";
         this.TableLayoutPanel.RowCount = 1;
         this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
         this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
         this.TableLayoutPanel.Size = new System.Drawing.Size(284, 261);
         this.TableLayoutPanel.TabIndex = 0;
         // 
         // ProgressView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(284, 261);
         this.Controls.Add(this.TableLayoutPanel);
         this.Name = "ProgressView";
         this.Text = "Progress";
         this.ResumeLayout(false);

        }

      #endregion

      private System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
   }
}

