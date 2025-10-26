namespace AStarVisualizer
{
    partial class Form1
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
            pathVisual = new PictureBox();
            clearButton = new Button();
            PathFindingButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pathVisual).BeginInit();
            SuspendLayout();
            // 
            // pathVisual
            // 
            pathVisual.Location = new Point(0, -1);
            pathVisual.Name = "pathVisual";
            pathVisual.Size = new Size(800, 375);
            pathVisual.TabIndex = 0;
            pathVisual.TabStop = false;
            pathVisual.Click += pathVisual_Click;
            pathVisual.MouseDown += pathVisual_MouseDown;
            pathVisual.MouseMove += pathVisual_MouseMove;
            pathVisual.MouseUp += pathVisual_MouseUp;
            // 
            // clearButton
            // 
            clearButton.Location = new Point(12, 391);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(75, 23);
            clearButton.TabIndex = 1;
            clearButton.Text = "CLEAR";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            // 
            // PathFindingButton
            // 
            PathFindingButton.Location = new Point(111, 391);
            PathFindingButton.Name = "PathFindingButton";
            PathFindingButton.Size = new Size(111, 23);
            PathFindingButton.TabIndex = 2;
            PathFindingButton.Text = "FIND PATH";
            PathFindingButton.UseVisualStyleBackColor = true;
            PathFindingButton.Click += PathFindingButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PathFindingButton);
            Controls.Add(clearButton);
            Controls.Add(pathVisual);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pathVisual).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pathVisual;
        private Button clearButton;
        private Button PathFindingButton;
    }
}
