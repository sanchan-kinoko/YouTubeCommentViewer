﻿namespace YouTubeCommentViewer
{
    partial class FormCommentViewer
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
            SuspendLayout();
            // 
            // FormCommentViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(524, 681);
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormCommentViewer";
            ShowIcon = false;
            Text = "Booting up...";
            FormClosing += FormCommentViewer_FormClosing;
            Shown += FormCommentViewer_Shown;
            LocationChanged += FormCommentViewer_LocationChanged;
            SizeChanged += FormCommentViewer_SizeChanged;
            KeyDown += FormCommentViewer_KeyDown;
            ResumeLayout(false);
        }

        #endregion
    }
}
