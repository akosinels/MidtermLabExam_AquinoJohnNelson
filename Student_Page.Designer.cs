namespace MidtermLabExam_AquinoJohnNelson
{
    partial class Student_Page
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
            StudentsFlowPanel = new FlowLayoutPanel();
            label1 = new Label();
            StudentsFlowPanel.SuspendLayout();
            SuspendLayout();
            // 
            // StudentsFlowPanel
            // 
            StudentsFlowPanel.AutoScroll = true;
            StudentsFlowPanel.Controls.Add(label1);
            StudentsFlowPanel.Dock = DockStyle.Fill;
            StudentsFlowPanel.FlowDirection = FlowDirection.TopDown;
            StudentsFlowPanel.Location = new Point(0, 0);
            StudentsFlowPanel.Name = "StudentsFlowPanel";
            StudentsFlowPanel.Size = new Size(800, 450);
            StudentsFlowPanel.TabIndex = 0;
            StudentsFlowPanel.WrapContents = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(171, 15);
            label1.TabIndex = 0;
            label1.Text = "Select a Student to View Details";
            // 
            // Student_Page
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(StudentsFlowPanel);
            Name = "Student_Page";
            Text = "Student List";
            Load += Student_Page_Load;
            StudentsFlowPanel.ResumeLayout(false);
            StudentsFlowPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel StudentsFlowPanel;
        private Label label1;
    }
}
