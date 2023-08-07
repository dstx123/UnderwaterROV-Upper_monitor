
namespace UnderwaterROV
{
    partial class fmConnection
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
            this.btnPortCheck2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbPort1 = new System.Windows.Forms.TextBox();
            this.tbIPadress1 = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPortCheck2
            // 
            this.btnPortCheck2.Location = new System.Drawing.Point(244, 100);
            this.btnPortCheck2.Name = "btnPortCheck2";
            this.btnPortCheck2.Size = new System.Drawing.Size(143, 27);
            this.btnPortCheck2.TabIndex = 22;
            this.btnPortCheck2.Text = "确认";
            this.btnPortCheck2.UseVisualStyleBackColor = true;
            this.btnPortCheck2.Click += new System.EventHandler(this.btnPortCheck2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbPort1);
            this.groupBox1.Controls.Add(this.tbIPadress1);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.label32);
            this.groupBox1.Location = new System.Drawing.Point(12, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 51);
            this.groupBox1.TabIndex = 521;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ROV网址";
            // 
            // tbPort1
            // 
            this.tbPort1.Location = new System.Drawing.Point(253, 21);
            this.tbPort1.Name = "tbPort1";
            this.tbPort1.Size = new System.Drawing.Size(94, 21);
            this.tbPort1.TabIndex = 24;
            // 
            // tbIPadress1
            // 
            this.tbIPadress1.Location = new System.Drawing.Point(73, 21);
            this.tbIPadress1.Name = "tbIPadress1";
            this.tbIPadress1.Size = new System.Drawing.Size(94, 21);
            this.tbIPadress1.TabIndex = 23;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(194, 25);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(53, 12);
            this.label31.TabIndex = 20;
            this.label31.Text = "端口号：";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(20, 25);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(53, 12);
            this.label32.TabIndex = 19;
            this.label32.Text = "IP地址：";
            // 
            // fmConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 155);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPortCheck2);
            this.Name = "fmConnection";
            this.Text = "连接设置";
            this.Load += new System.EventHandler(this.fmConnection_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnPortCheck2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbPort1;
        private System.Windows.Forms.TextBox tbIPadress1;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
    }
}