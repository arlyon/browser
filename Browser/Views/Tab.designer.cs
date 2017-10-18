namespace Browser.Views
{
    partial class Tab
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UrlBar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ResponseMessageBox = new System.Windows.Forms.TextBox();
            this.ResponseBodyBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.StatusCode = new System.Windows.Forms.Label();
            this.GoForwardButton = new System.Windows.Forms.Button();
            this.ReloadButton = new System.Windows.Forms.Button();
            this.GoHomeButton = new System.Windows.Forms.Button();
            this.FavoritesButton = new System.Windows.Forms.Button();
            this.GoBackButton = new System.Windows.Forms.Button();
            this.Render = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UrlBar
            // 
            this.UrlBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UrlBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.UrlBar.Location = new System.Drawing.Point(117, 3);
            this.UrlBar.Name = "UrlBar";
            this.UrlBar.Size = new System.Drawing.Size(511, 32);
            this.UrlBar.TabIndex = 1;
            this.UrlBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnUrlKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Response Message";
            // 
            // ResponseMessageBox
            // 
            this.ResponseMessageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResponseMessageBox.Font = new System.Drawing.Font("Hasklig", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResponseMessageBox.Location = new System.Drawing.Point(6, 60);
            this.ResponseMessageBox.Multiline = true;
            this.ResponseMessageBox.Name = "ResponseMessageBox";
            this.ResponseMessageBox.ReadOnly = true;
            this.ResponseMessageBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResponseMessageBox.Size = new System.Drawing.Size(698, 201);
            this.ResponseMessageBox.TabIndex = 4;
            // 
            // ResponseBodyBox
            // 
            this.ResponseBodyBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResponseBodyBox.BackColor = System.Drawing.SystemColors.Control;
            this.ResponseBodyBox.Font = new System.Drawing.Font("Hasklig", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResponseBodyBox.Location = new System.Drawing.Point(6, 280);
            this.ResponseBodyBox.Multiline = true;
            this.ResponseBodyBox.Name = "ResponseBodyBox";
            this.ResponseBodyBox.ReadOnly = true;
            this.ResponseBodyBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResponseBodyBox.Size = new System.Drawing.Size(698, 354);
            this.ResponseBodyBox.TabIndex = 6;
            this.ResponseBodyBox.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Response Body";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 645);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Status Code:";
            // 
            // StatusCode
            // 
            this.StatusCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusCode.AutoSize = true;
            this.StatusCode.Location = new System.Drawing.Point(67, 645);
            this.StatusCode.Name = "StatusCode";
            this.StatusCode.Size = new System.Drawing.Size(31, 13);
            this.StatusCode.TabIndex = 15;
            this.StatusCode.Text = "none";
            // 
            // GoForwardButton
            // 
            this.GoForwardButton.BackColor = System.Drawing.Color.Transparent;
            this.GoForwardButton.FlatAppearance.BorderSize = 0;
            this.GoForwardButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.GoForwardButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.GoForwardButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GoForwardButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GoForwardButton.Image = global::Browser.Properties.Resources.forward;
            this.GoForwardButton.Location = new System.Drawing.Point(79, 3);
            this.GoForwardButton.Name = "GoForwardButton";
            this.GoForwardButton.Size = new System.Drawing.Size(32, 32);
            this.GoForwardButton.TabIndex = 11;
            this.GoForwardButton.UseVisualStyleBackColor = false;
            this.GoForwardButton.Click += new System.EventHandler(this.ForwardButtonPressed);
            // 
            // ReloadButton
            // 
            this.ReloadButton.BackColor = System.Drawing.Color.Transparent;
            this.ReloadButton.FlatAppearance.BorderSize = 0;
            this.ReloadButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ReloadButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ReloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReloadButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ReloadButton.Image = global::Browser.Properties.Resources.reload;
            this.ReloadButton.Location = new System.Drawing.Point(41, 3);
            this.ReloadButton.Name = "ReloadButton";
            this.ReloadButton.Size = new System.Drawing.Size(32, 32);
            this.ReloadButton.TabIndex = 10;
            this.ReloadButton.UseVisualStyleBackColor = false;
            this.ReloadButton.Click += new System.EventHandler(this.ReloadButtonPressed);
            // 
            // GoHomeButton
            // 
            this.GoHomeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GoHomeButton.BackColor = System.Drawing.Color.Transparent;
            this.GoHomeButton.FlatAppearance.BorderSize = 0;
            this.GoHomeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.GoHomeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.GoHomeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GoHomeButton.Image = global::Browser.Properties.Resources.home;
            this.GoHomeButton.Location = new System.Drawing.Point(672, 3);
            this.GoHomeButton.Name = "GoHomeButton";
            this.GoHomeButton.Size = new System.Drawing.Size(32, 32);
            this.GoHomeButton.TabIndex = 9;
            this.GoHomeButton.UseVisualStyleBackColor = false;
            this.GoHomeButton.Click += new System.EventHandler(this.HomeButtonPressed);
            // 
            // FavoritesButton
            // 
            this.FavoritesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FavoritesButton.BackColor = System.Drawing.Color.Transparent;
            this.FavoritesButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FavoritesButton.FlatAppearance.BorderSize = 0;
            this.FavoritesButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.FavoritesButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.FavoritesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FavoritesButton.Image = global::Browser.Properties.Resources.star;
            this.FavoritesButton.Location = new System.Drawing.Point(634, 3);
            this.FavoritesButton.Name = "FavoritesButton";
            this.FavoritesButton.Size = new System.Drawing.Size(32, 32);
            this.FavoritesButton.TabIndex = 2;
            this.FavoritesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.FavoritesButton.UseVisualStyleBackColor = false;
            this.FavoritesButton.Click += new System.EventHandler(this.FavoritesButtonPressed);
            // 
            // GoBackButton
            // 
            this.GoBackButton.BackColor = System.Drawing.Color.Transparent;
            this.GoBackButton.FlatAppearance.BorderSize = 0;
            this.GoBackButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.GoBackButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.GoBackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GoBackButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GoBackButton.Image = global::Browser.Properties.Resources.back;
            this.GoBackButton.Location = new System.Drawing.Point(3, 3);
            this.GoBackButton.Name = "GoBackButton";
            this.GoBackButton.Size = new System.Drawing.Size(32, 32);
            this.GoBackButton.TabIndex = 0;
            this.GoBackButton.UseVisualStyleBackColor = false;
            this.GoBackButton.Click += new System.EventHandler(this.BackButtonPressed);
            // 
            // Render
            // 
            this.Render.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Render.Location = new System.Drawing.Point(624, 640);
            this.Render.Name = "Render";
            this.Render.Size = new System.Drawing.Size(80, 23);
            this.Render.TabIndex = 16;
            this.Render.Text = "Render Page";
            this.Render.UseVisualStyleBackColor = true;
            this.Render.Click += new System.EventHandler(this.Render_Click);
            // 
            // Tab
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.Render);
            this.Controls.Add(this.ResponseBodyBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GoForwardButton);
            this.Controls.Add(this.ResponseMessageBox);
            this.Controls.Add(this.ReloadButton);
            this.Controls.Add(this.GoHomeButton);
            this.Controls.Add(this.StatusCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FavoritesButton);
            this.Controls.Add(this.UrlBar);
            this.Controls.Add(this.GoBackButton);
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "Tab";
            this.Size = new System.Drawing.Size(707, 666);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GoBackButton;
        private System.Windows.Forms.TextBox UrlBar;
        private System.Windows.Forms.Button FavoritesButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ResponseMessageBox;
        private System.Windows.Forms.TextBox ResponseBodyBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label StatusCode;
        private System.Windows.Forms.Button GoHomeButton;
        private System.Windows.Forms.Button ReloadButton;
        private System.Windows.Forms.Button GoForwardButton;
        private System.Windows.Forms.Button Render;
    }
}
