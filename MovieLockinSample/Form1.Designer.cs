namespace MovieLockinSample
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_TestMovieCreate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_TestProcess = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_TestMovieCreate
            // 
            this.button_TestMovieCreate.Location = new System.Drawing.Point(12, 12);
            this.button_TestMovieCreate.Name = "button_TestMovieCreate";
            this.button_TestMovieCreate.Size = new System.Drawing.Size(100, 32);
            this.button_TestMovieCreate.TabIndex = 0;
            this.button_TestMovieCreate.Text = "CreateMovie";
            this.button_TestMovieCreate.UseVisualStyleBackColor = true;
            this.button_TestMovieCreate.Click += new System.EventHandler(this.button_TestMovieCreate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 59);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 480);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // button_TestProcess
            // 
            this.button_TestProcess.Location = new System.Drawing.Point(118, 12);
            this.button_TestProcess.Name = "button_TestProcess";
            this.button_TestProcess.Size = new System.Drawing.Size(100, 32);
            this.button_TestProcess.TabIndex = 2;
            this.button_TestProcess.Text = "Test";
            this.button_TestProcess.UseVisualStyleBackColor = true;
            this.button_TestProcess.Click += new System.EventHandler(this.button_TestProcess_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 553);
            this.Controls.Add(this.button_TestProcess);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_TestMovieCreate);
            this.Name = "Form1";
            this.Text = "MovieLockinSample";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_TestMovieCreate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_TestProcess;
    }
}

