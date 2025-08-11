namespace B2S_SetUp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            
            // Main TableLayoutPanel
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.AutoSize = true;
            this.mainLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainLayout.ColumnCount = 4;
            this.mainLayout.ColumnStyles.Clear();
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.mainLayout.Padding = new System.Windows.Forms.Padding(8);

            // Initialize Controls
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBrowseBackground = new System.Windows.Forms.Button();

            this.lblPlayfield = new System.Windows.Forms.Label();
            this.txtPlayfieldWidth = new System.Windows.Forms.TextBox();
            this.txtPlayfieldHeight = new System.Windows.Forms.TextBox();

            this.lblBackglass = new System.Windows.Forms.Label();
            this.txtBackglassWidth = new System.Windows.Forms.TextBox();
            this.txtBackglassHeight = new System.Windows.Forms.TextBox();

            this.lblDisplay = new System.Windows.Forms.Label();
            this.txtDisplayDevice = new System.Windows.Forms.TextBox();

            this.lblBackglassPos = new System.Windows.Forms.Label();
            this.txtBackglassX = new System.Windows.Forms.TextBox();
            this.txtBackglassY = new System.Windows.Forms.TextBox();

            // DMD Size Controls
            this.lblDMDSize = new System.Windows.Forms.Label();
            this.txtDMDWidth = new System.Windows.Forms.TextBox();
            this.txtDMDHeight = new System.Windows.Forms.TextBox();

            // DMD Position Controls
            this.lblDMDPosition = new System.Windows.Forms.Label();
            this.txtDMDX = new System.Windows.Forms.TextBox();
            this.txtDMDY = new System.Windows.Forms.TextBox();

            this.chkYFlip = new System.Windows.Forms.CheckBox();

            this.lblBackground = new System.Windows.Forms.Label();
            this.txtBackgroundX = new System.Windows.Forms.TextBox();
            this.txtBackgroundY = new System.Windows.Forms.TextBox();

            this.lblBackgroundSize = new System.Windows.Forms.Label();
            this.txtBackgroundWidth = new System.Windows.Forms.TextBox();
            this.txtBackgroundHeight = new System.Windows.Forms.TextBox();

            // Background Image Controls
            this.lblBackgroundImage = new System.Windows.Forms.Label();
            this.txtBackgroundImage = new System.Windows.Forms.TextBox();

            // Background Image Panel
            this.backgroundImagePanel = new System.Windows.Forms.TableLayoutPanel();
            this.backgroundImagePanel.ColumnCount = 2;
            this.backgroundImagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundImagePanel.Margin = new System.Windows.Forms.Padding(0);
            this.backgroundImagePanel.AutoSize = false;
            this.backgroundImagePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.backgroundImagePanel.Height = 23;
            this.backgroundImagePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.backgroundImagePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));

            // Background Image Controls
            this.txtBackgroundImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBackgroundImage.Margin = new System.Windows.Forms.Padding(0);

            this.btnBrowseBackground.Text = "...";
            this.btnBrowseBackground.Size = new System.Drawing.Size(25, 23);
            this.btnBrowseBackground.MinimumSize = new System.Drawing.Size(25, 23);
            this.btnBrowseBackground.MaximumSize = new System.Drawing.Size(25, 23);
            this.btnBrowseBackground.UseVisualStyleBackColor = true;
            this.btnBrowseBackground.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBrowseBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBrowseBackground.Margin = new System.Windows.Forms.Padding(0);

            // Add controls to background image panel
            this.backgroundImagePanel.Controls.Add(this.txtBackgroundImage, 0, 0);
            this.backgroundImagePanel.Controls.Add(this.btnBrowseBackground, 1, 0);

            // Configure Controls
            this.btnLoad.Text = "Load";
            this.btnLoad.Dock = System.Windows.Forms.DockStyle.Fill;

            this.btnSave.Text = "Save";
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;

            // Configure Labels
            this.lblPlayfield.Text = "Playfield Width/Height:";
            this.lblPlayfield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlayfield.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblBackglass.Text = "Backglass Width/Height:";
            this.lblBackglass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBackglass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblDisplay.Text = "Display Device:";
            this.lblDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblBackglassPos.Text = "Backglass Position (X/Y):";
            this.lblBackglassPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBackglassPos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblDMDSize.Text = "DMD Size (Width/Height):";
            this.lblDMDSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDMDSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblDMDPosition.Text = "DMD Position (X/Y):";
            this.lblDMDPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDMDPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.chkYFlip.Text = "Y-Flip";
            this.chkYFlip.AutoSize = true;
            this.chkYFlip.Dock = System.Windows.Forms.DockStyle.Fill;

            this.lblBackground.Text = "Background Position (X/Y):";
            this.lblBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBackground.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblBackgroundSize.Text = "Background Size:";
            this.lblBackgroundSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBackgroundSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblBackgroundImage.Text = "Background Image:";
            this.lblBackgroundImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBackgroundImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // Configure TextBoxes
            foreach (System.Windows.Forms.Control control in new System.Windows.Forms.Control[] {
                txtPlayfieldWidth, txtPlayfieldHeight,
                txtBackglassWidth, txtBackglassHeight,
                txtDisplayDevice,
                txtBackglassX, txtBackglassY,
                txtDMDWidth, txtDMDHeight,
                txtDMDX, txtDMDY,
                txtBackgroundX, txtBackgroundY,
                txtBackgroundWidth, txtBackgroundHeight
            })
            {
                control.Dock = System.Windows.Forms.DockStyle.Fill;
            }

            // Add Controls to TableLayoutPanel
            int row = 0;

            // Playfield row
            this.mainLayout.Controls.Add(this.lblPlayfield, 0, row);
            this.mainLayout.Controls.Add(this.txtPlayfieldWidth, 1, row);
            this.mainLayout.Controls.Add(this.txtPlayfieldHeight, 2, row);
            row++;

            // Backglass row
            this.mainLayout.Controls.Add(this.lblBackglass, 0, row);
            this.mainLayout.Controls.Add(this.txtBackglassWidth, 1, row);
            this.mainLayout.Controls.Add(this.txtBackglassHeight, 2, row);
            row++;

            // Display Device row
            this.mainLayout.Controls.Add(this.lblDisplay, 0, row);
            this.mainLayout.Controls.Add(this.txtDisplayDevice, 1, row);
            this.mainLayout.SetColumnSpan(this.txtDisplayDevice, 2);
            row++;

            // Backglass Position row
            this.mainLayout.Controls.Add(this.lblBackglassPos, 0, row);
            this.mainLayout.Controls.Add(this.txtBackglassX, 1, row);
            this.mainLayout.Controls.Add(this.txtBackglassY, 2, row);
            row++;

            // DMD Size row
            this.mainLayout.Controls.Add(this.lblDMDSize, 0, row);
            this.mainLayout.Controls.Add(this.txtDMDWidth, 1, row);
            this.mainLayout.Controls.Add(this.txtDMDHeight, 2, row);
            row++;

            // DMD Position row
            this.mainLayout.Controls.Add(this.lblDMDPosition, 0, row);
            this.mainLayout.Controls.Add(this.txtDMDX, 1, row);
            this.mainLayout.Controls.Add(this.txtDMDY, 2, row);
            row++;

            // Y-Flip row
            this.mainLayout.Controls.Add(this.chkYFlip, 0, row);
            row++;

            // Background Position row
            this.mainLayout.Controls.Add(this.lblBackground, 0, row);
            this.mainLayout.Controls.Add(this.txtBackgroundX, 1, row);
            this.mainLayout.Controls.Add(this.txtBackgroundY, 2, row);
            row++;

            // Background Size row
            this.mainLayout.Controls.Add(this.lblBackgroundSize, 0, row);
            this.mainLayout.Controls.Add(this.txtBackgroundWidth, 1, row);
            this.mainLayout.Controls.Add(this.txtBackgroundHeight, 2, row);
            row++;

            // Background Image row
            this.mainLayout.Controls.Add(this.lblBackgroundImage, 0, row);
            this.mainLayout.Controls.Add(this.backgroundImagePanel, 1, row);
            this.mainLayout.SetColumnSpan(this.backgroundImagePanel, 3);
            row++;

            // Buttons row at bottom
            this.mainLayout.Controls.Add(this.btnLoad, 1, row);
            this.mainLayout.Controls.Add(this.btnSave, 2, row);

            // Configure button styles
            this.btnLoad.Text = "Load";
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnLoad.AutoSize = true;
            this.btnLoad.MinimumSize = new System.Drawing.Size(75, 23);

            this.btnSave.Text = "Save";
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnSave.AutoSize = true;
            this.btnSave.MinimumSize = new System.Drawing.Size(75, 23);

            // Configure the browse button
            this.btnBrowseBackground.Text = "...";
            this.btnBrowseBackground.Size = new System.Drawing.Size(25, 23);
            this.btnBrowseBackground.MinimumSize = new System.Drawing.Size(25, 23);
            this.btnBrowseBackground.MaximumSize = new System.Drawing.Size(25, 23);
            this.btnBrowseBackground.UseVisualStyleBackColor = true;
            this.btnBrowseBackground.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBrowseBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBrowseBackground.Margin = new System.Windows.Forms.Padding(0);

            // Form settings
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MinimumSize = new System.Drawing.Size(500, 420);
            this.Size = new System.Drawing.Size(500, 420);
            this.Text = "B2S ScreenRes Editor";

            // Add TableLayoutPanel to form
            this.Controls.Add(this.mainLayout);
        }

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel backgroundImagePanel;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnBrowseBackground;
        private System.Windows.Forms.Label lblPlayfield;
        private System.Windows.Forms.TextBox txtPlayfieldWidth;
        private System.Windows.Forms.TextBox txtPlayfieldHeight;
        private System.Windows.Forms.Label lblBackglass;
        private System.Windows.Forms.TextBox txtBackglassWidth;
        private System.Windows.Forms.TextBox txtBackglassHeight;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.TextBox txtDisplayDevice;
        private System.Windows.Forms.Label lblBackglassPos;
        private System.Windows.Forms.TextBox txtBackglassX;
        private System.Windows.Forms.TextBox txtBackglassY;
        private System.Windows.Forms.Label lblDMDSize;
        private System.Windows.Forms.Label lblDMDPosition;
        private System.Windows.Forms.TextBox txtDMDWidth;
        private System.Windows.Forms.TextBox txtDMDHeight;
        private System.Windows.Forms.TextBox txtDMDX;
        private System.Windows.Forms.TextBox txtDMDY;
        private System.Windows.Forms.CheckBox chkYFlip;
        private System.Windows.Forms.Label lblBackground;
        private System.Windows.Forms.Label lblBackgroundSize;
        private System.Windows.Forms.TextBox txtBackgroundX;
        private System.Windows.Forms.TextBox txtBackgroundY;
        private System.Windows.Forms.TextBox txtBackgroundWidth;
        private System.Windows.Forms.TextBox txtBackgroundHeight;
        private System.Windows.Forms.Label lblBackgroundImage;
        private System.Windows.Forms.TextBox txtBackgroundImage;
    }
}