namespace ATTestCF
{
	partial class Form1
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
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.txtRecipient = new System.Windows.Forms.TextBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.txtResponse = new System.Windows.Forms.TextBox();
			this.cbStoreLocation = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 20);
			this.label1.Text = "Port:";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(68, 4);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(100, 25);
			this.numericUpDown1.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 20);
			this.label2.Text = "Adresat:";
			// 
			// txtMessage
			// 
			this.txtMessage.Location = new System.Drawing.Point(3, 62);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(234, 69);
			this.txtMessage.TabIndex = 4;
			// 
			// txtRecipient
			// 
			this.txtRecipient.Location = new System.Drawing.Point(68, 32);
			this.txtRecipient.Name = "txtRecipient";
			this.txtRecipient.Size = new System.Drawing.Size(169, 24);
			this.txtRecipient.TabIndex = 5;
			this.txtRecipient.Text = "+420";
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(3, 137);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(72, 20);
			this.btnSend.TabIndex = 6;
			this.btnSend.Text = "Odeslat";
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(81, 137);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(72, 20);
			this.btnSave.TabIndex = 7;
			this.btnSave.Text = "Ulozit";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// txtResponse
			// 
			this.txtResponse.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtResponse.Location = new System.Drawing.Point(0, 166);
			this.txtResponse.Multiline = true;
			this.txtResponse.Name = "txtResponse";
			this.txtResponse.ReadOnly = true;
			this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtResponse.Size = new System.Drawing.Size(240, 128);
			this.txtResponse.TabIndex = 10;
			// 
			// cbStoreLocation
			// 
			this.cbStoreLocation.Location = new System.Drawing.Point(159, 133);
			this.cbStoreLocation.Name = "cbStoreLocation";
			this.cbStoreLocation.Size = new System.Drawing.Size(78, 24);
			this.cbStoreLocation.TabIndex = 11;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240, 294);
			this.Controls.Add(this.cbStoreLocation);
			this.Controls.Add(this.txtResponse);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.txtRecipient);
			this.Controls.Add(this.txtMessage);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.label1);
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.TextBox txtRecipient;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox txtResponse;
		private System.Windows.Forms.ComboBox cbStoreLocation;
	}
}

