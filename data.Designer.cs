
namespace UnderwaterROV
{
    partial class data
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
            this.components = new System.ComponentModel.Container();
            this.tbSendData = new System.Windows.Forms.TextBox();
            this.tbReceivedata = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TmFresh = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tbSendData
            // 
            this.tbSendData.AllowDrop = true;
            this.tbSendData.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSendData.Location = new System.Drawing.Point(12, 39);
            this.tbSendData.MaxLength = 100;
            this.tbSendData.Multiline = true;
            this.tbSendData.Name = "tbSendData";
            this.tbSendData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSendData.Size = new System.Drawing.Size(692, 107);
            this.tbSendData.TabIndex = 520;
            // 
            // tbReceivedata
            // 
            this.tbReceivedata.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbReceivedata.Location = new System.Drawing.Point(12, 193);
            this.tbReceivedata.Multiline = true;
            this.tbReceivedata.Name = "tbReceivedata";
            this.tbReceivedata.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbReceivedata.Size = new System.Drawing.Size(692, 115);
            this.tbReceivedata.TabIndex = 521;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 19);
            this.label1.TabIndex = 522;
            this.label1.Text = "发送数据：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 19);
            this.label2.TabIndex = 523;
            this.label2.Text = "接收数据：";
            // 
            // TmFresh
            // 
            this.TmFresh.Enabled = true;
            this.TmFresh.Interval = 200;
            this.TmFresh.Tick += new System.EventHandler(this.TmFresh_Tick);
            // 
            // data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 320);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSendData);
            this.Controls.Add(this.tbReceivedata);
            this.Name = "data";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.data_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tbSendData;
        public System.Windows.Forms.TextBox tbReceivedata;
        private System.Windows.Forms.Timer TmFresh;
    }
}