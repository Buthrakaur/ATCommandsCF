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
			this.txtResponse = new System.Windows.Forms.TextBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cbStoreLocation = new System.Windows.Forms.ComboBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnSend = new System.Windows.Forms.Button();
			this.txtRecipient = new System.Windows.Forms.TextBox();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtResponse
			// 
			this.txtResponse.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtResponse.Location = new System.Drawing.Point(0, 161);
			this.txtResponse.Multiline = true;
			this.txtResponse.Name = "txtResponse";
			this.txtResponse.ReadOnly = true;
			this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtResponse.Size = new System.Drawing.Size(240, 107);
			this.txtResponse.TabIndex = 10;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem2);
			this.menuItem1.Text = "Akce";
			// 
			// menuItem2
			// 
			this.menuItem2.Text = "Adresar";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cbStoreLocation);
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Controls.Add(this.btnSend);
			this.panel1.Controls.Add(this.txtRecipient);
			this.panel1.Controls.Add(this.txtMessage);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.numericUpDown1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(240, 161);
			// 
			// cbStoreLocation
			// 
			this.cbStoreLocation.Location = new System.Drawing.Point(159, 132);
			this.cbStoreLocation.Name = "cbStoreLocation";
			this.cbStoreLocation.Size = new System.Drawing.Size(78, 24);
			this.cbStoreLocation.TabIndex = 19;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(81, 136);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(72, 20);
			this.btnSave.TabIndex = 18;
			this.btnSave.Text = "Ulozit";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(3, 136);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(72, 20);
			this.btnSend.TabIndex = 17;
			this.btnSend.Text = "Odeslat";
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// txtRecipient
			// 
			this.txtRecipient.Location = new System.Drawing.Point(68, 31);
			this.txtRecipient.Name = "txtRecipient";
			this.txtRecipient.Size = new System.Drawing.Size(169, 24);
			this.txtRecipient.TabIndex = 16;
			this.txtRecipient.Text = "+420";
			// 
			// txtMessage
			// 
			this.txtMessage.Location = new System.Drawing.Point(3, 61);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(234, 69);
			this.txtMessage.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 20);
			this.label2.Text = "Adresat:";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(68, 3);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(100, 25);
			this.numericUpDown1.TabIndex = 14;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 20);
			this.label1.Text = "Port:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240, 268);
			this.Controls.Add(this.txtResponse);
			this.Controls.Add(this.panel1);
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "Form1";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox txtResponse;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox cbStoreLocation;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.TextBox txtRecipient;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label1;
	}
}

