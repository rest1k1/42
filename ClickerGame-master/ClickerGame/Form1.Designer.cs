namespace ClickerGame
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
			this.components = new System.ComponentModel.Container();
			this.labelScore = new System.Windows.Forms.Label();
			this.listBoxUpgrades = new System.Windows.Forms.ListBox();
			this.labelTime = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.buttonClick = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelScore
			// 
			this.labelScore.AutoSize = true;
			this.labelScore.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelScore.Location = new System.Drawing.Point(240, 31);
			this.labelScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelScore.Name = "labelScore";
			this.labelScore.Size = new System.Drawing.Size(71, 26);
			this.labelScore.TabIndex = 0;
			this.labelScore.Text = "label1";
			// 
			// listBoxUpgrades
			// 
			this.listBoxUpgrades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.listBoxUpgrades.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listBoxUpgrades.ForeColor = System.Drawing.Color.Black;
			this.listBoxUpgrades.FormattingEnabled = true;
			this.listBoxUpgrades.Location = new System.Drawing.Point(200, 90);
			this.listBoxUpgrades.Margin = new System.Windows.Forms.Padding(2);
			this.listBoxUpgrades.Name = "listBoxUpgrades";
			this.listBoxUpgrades.Size = new System.Drawing.Size(232, 65);
			this.listBoxUpgrades.TabIndex = 2;
			this.listBoxUpgrades.Click += new System.EventHandler(this.buttonBuy_Click);
			// 
			// labelTime
			// 
			this.labelTime.AutoSize = true;
			this.labelTime.Location = new System.Drawing.Point(514, 429);
			this.labelTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelTime.Name = "labelTime";
			this.labelTime.Size = new System.Drawing.Size(0, 13);
			this.labelTime.TabIndex = 5;
			// 
			// buttonClick
			// 
			this.buttonClick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.buttonClick.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonClick.ForeColor = System.Drawing.Color.White;
			this.buttonClick.Image = global::ClickerGame.Properties.Resources.christmas_furret;
			this.buttonClick.Location = new System.Drawing.Point(110, 212);
			this.buttonClick.Margin = new System.Windows.Forms.Padding(2);
			this.buttonClick.Name = "buttonClick";
			this.buttonClick.Size = new System.Drawing.Size(381, 218);
			this.buttonClick.TabIndex = 1;
			this.buttonClick.Text = "КЛИК";
			this.buttonClick.UseVisualStyleBackColor = false;
			this.buttonClick.Click += new System.EventHandler(this.buttonClick_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(597, 431);
			this.Controls.Add(this.labelTime);
			this.Controls.Add(this.listBoxUpgrades);
			this.Controls.Add(this.buttonClick);
			this.Controls.Add(this.labelScore);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "Form1";
			this.Text = "Собачья лудка";
			this.Click += new System.EventHandler(this.buttonBuy_Click);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Button buttonClick;
        private System.Windows.Forms.ListBox listBoxUpgrades;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Timer timer1;
    }
}

