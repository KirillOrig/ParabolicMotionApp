namespace BaseFigureMotionApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar trackBarSpeed;
        private System.Windows.Forms.TrackBar trackBarRotation;
        private System.Windows.Forms.TrackBar trackBarScale;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnReset;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.trackBarSpeed = new System.Windows.Forms.TrackBar();
            this.trackBarRotation = new System.Windows.Forms.TrackBar();
            this.trackBarScale = new System.Windows.Forms.TrackBar();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarScale)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 400);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // trackBarSpeed
            // 
            this.trackBarSpeed.Location = new System.Drawing.Point(630, 30);
            this.trackBarSpeed.Maximum = 20;
            this.trackBarSpeed.Minimum = 1;
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Size = new System.Drawing.Size(200, 56);
            this.trackBarSpeed.TabIndex = 1;
            this.trackBarSpeed.Value = 5;
            this.trackBarSpeed.Scroll += new System.EventHandler(this.trackBarSpeed_Scroll);
            // 
            // trackBarRotation
            // 
            this.trackBarRotation.Location = new System.Drawing.Point(630, 90);
            this.trackBarRotation.Maximum = 20;
            this.trackBarRotation.Minimum = 1;
            this.trackBarRotation.Name = "trackBarRotation";
            this.trackBarRotation.Size = new System.Drawing.Size(200, 56);
            this.trackBarRotation.TabIndex = 2;
            this.trackBarRotation.Value = 5;
            this.trackBarRotation.Scroll += new System.EventHandler(this.trackBarRotation_Scroll);
            // 
            // trackBarScale
            // 
            this.trackBarScale.Location = new System.Drawing.Point(630, 150);
            this.trackBarScale.Maximum = 200;
            this.trackBarScale.Minimum = 50;
            this.trackBarScale.Name = "trackBarScale";
            this.trackBarScale.Size = new System.Drawing.Size(200, 56);
            this.trackBarScale.TabIndex = 3;
            this.trackBarScale.Value = 100;
            this.trackBarScale.Scroll += new System.EventHandler(this.trackBarScale_Scroll);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(630, 220);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(90, 30);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Запуск";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(740, 220);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(90, 30);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Стоп";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(630, 270);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(200, 30);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Сброс";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(630, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Скорость движения фигуры";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(630, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Скорость вращения фигуры";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(630, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Размер фигуры";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(850, 430);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.trackBarScale);
            this.Controls.Add(this.trackBarRotation);
            this.Controls.Add(this.trackBarSpeed);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Эмуляция движения базовой фигуры";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
