namespace pcontrol2._0
{
    partial class pcontrol
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.qrBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.qrBox)).BeginInit();
            this.SuspendLayout();
            // 
            // qrBox
            // 
            this.qrBox.Location = new System.Drawing.Point(0, 0);
            this.qrBox.Name = "qrBox";
            this.qrBox.Size = new System.Drawing.Size(230, 230);
            this.qrBox.TabIndex = 0;
            this.qrBox.TabStop = false;
            // 
            // pcontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 230);
            this.Controls.Add(this.qrBox);
            this.Name = "pcontrol";
            this.Text = "pcontrol";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.pcontrol_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.qrBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox qrBox;
    }
}

