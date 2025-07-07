namespace PA5_Draft
{
    partial class Apples
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
            this.Apple = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.countApple = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Apple
            // 
            this.Apple.Location = new System.Drawing.Point(345, 127);
            this.Apple.Name = "Apple";
            this.Apple.Size = new System.Drawing.Size(112, 54);
            this.Apple.TabIndex = 0;
            this.Apple.Text = "Apple";
            this.Apple.UseVisualStyleBackColor = true;
            this.Apple.Click += new System.EventHandler(this.Apple_Click);
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(345, 307);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(112, 63);
            this.OK.TabIndex = 1;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number of Apples";
            // 
            // countApple
            // 
            this.countApple.AutoSize = true;
            this.countApple.Location = new System.Drawing.Point(123, 199);
            this.countApple.Name = "countApple";
            this.countApple.Size = new System.Drawing.Size(0, 25);
            this.countApple.TabIndex = 3;
            // 
            // Apples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.countApple);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Apple);
            this.Name = "Apples";
            this.Text = "Apples";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Apple;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label countApple;
    }
}