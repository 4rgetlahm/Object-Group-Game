
namespace object_group_game


{
    partial class CreateItemMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameInput = new System.Windows.Forms.TextBox();
            this.effectInput = new System.Windows.Forms.ListBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.effectLabel = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.strInput = new System.Windows.Forms.NumericUpDown();
            this.dexInput = new System.Windows.Forms.NumericUpDown();
            this.intInput = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.strInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dexInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intInput)).BeginInit();
            this.SuspendLayout();
            // 
            // nameInput
            // 
            this.nameInput.Location = new System.Drawing.Point(180, 62);
            this.nameInput.Name = "nameInput";
            this.nameInput.Size = new System.Drawing.Size(231, 31);
            this.nameInput.TabIndex = 0;
            // 
            // effectInput
            // 
            this.effectInput.FormattingEnabled = true;
            this.effectInput.ItemHeight = 25;
            this.effectInput.Location = new System.Drawing.Point(180, 134);
            this.effectInput.Name = "effectInput";
            this.effectInput.Size = new System.Drawing.Size(231, 54);
            this.effectInput.TabIndex = 2;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(51, 65);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(104, 25);
            this.nameLabel.TabIndex = 3;
            this.nameLabel.Text = "Item Name:";
            // 
            // effectLabel
            // 
            this.effectLabel.AutoSize = true;
            this.effectLabel.Location = new System.Drawing.Point(95, 134);
            this.effectLabel.Name = "effectLabel";
            this.effectLabel.Size = new System.Drawing.Size(60, 25);
            this.effectLabel.TabIndex = 5;
            this.effectLabel.Text = "Effect:";
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(180, 315);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(126, 34);
            this.submitButton.TabIndex = 6;
            this.submitButton.Text = "Create";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // strInput
            // 
            this.strInput.Location = new System.Drawing.Point(60, 231);
            this.strInput.Name = "strInput";
            this.strInput.Size = new System.Drawing.Size(92, 31);
            this.strInput.TabIndex = 7;
            // 
            // dexInput
            // 
            this.dexInput.Location = new System.Drawing.Point(189, 231);
            this.dexInput.Name = "dexInput";
            this.dexInput.Size = new System.Drawing.Size(92, 31);
            this.dexInput.TabIndex = 8;
            // 
            // intInput
            // 
            this.intInput.Location = new System.Drawing.Point(327, 231);
            this.intInput.Name = "intInput";
            this.intInput.Size = new System.Drawing.Size(92, 31);
            this.intInput.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "STR";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "DEX";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "INT";
            // 
            // CreateItemMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(488, 378);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.intInput);
            this.Controls.Add(this.dexInput);
            this.Controls.Add(this.strInput);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.effectLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.effectInput);
            this.Controls.Add(this.nameInput);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CreateItemMenu";
            this.Text = "Create Item";
            this.Load += new System.EventHandler(this.CreateItemMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.strInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dexInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameInput;
        private System.Windows.Forms.ListBox effectInput;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label effectLabel;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.NumericUpDown strInput;
        private System.Windows.Forms.NumericUpDown dexInput;
        private System.Windows.Forms.NumericUpDown intInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

