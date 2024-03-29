﻿namespace Anti.LockScreen
{
    partial class fmSetting
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
            if (disposing && (components != null)) {
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
            this.components = new System.ComponentModel.Container();
            this.tmIdleCheck = new System.Windows.Forms.Timer(this.components);
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.lblIdle = new System.Windows.Forms.Label();
            this.lblSend = new System.Windows.Forms.Label();
            this.btnExist = new System.Windows.Forms.Button();
            this.lblMsgs = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbInterval = new System.Windows.Forms.ComboBox();
            this.cbRepeatCount = new System.Windows.Forms.ComboBox();
            this.cbStartup = new System.Windows.Forms.CheckBox();
            this.cbMinimize = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tmIdleCheck
            // 
            this.tmIdleCheck.Enabled = true;
            this.tmIdleCheck.Interval = 1000;
            this.tmIdleCheck.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // niTray
            // 
            this.niTray.Visible = true;
            this.niTray.Click += new System.EventHandler(this.niTray_Click);
            // 
            // lblIdle
            // 
            this.lblIdle.AutoSize = true;
            this.lblIdle.BackColor = System.Drawing.Color.Transparent;
            this.lblIdle.Location = new System.Drawing.Point(12, 18);
            this.lblIdle.Name = "lblIdle";
            this.lblIdle.Size = new System.Drawing.Size(154, 22);
            this.lblIdle.TabIndex = 3;
            this.lblIdle.Text = "Interval (minutes)";
            this.lblIdle.Click += new System.EventHandler(this.fmSetting_Click);
            // 
            // lblSend
            // 
            this.lblSend.AutoSize = true;
            this.lblSend.BackColor = System.Drawing.Color.Transparent;
            this.lblSend.Location = new System.Drawing.Point(12, 58);
            this.lblSend.Name = "lblSend";
            this.lblSend.Size = new System.Drawing.Size(110, 22);
            this.lblSend.TabIndex = 6;
            this.lblSend.Text = "Send Count";
            this.lblSend.Click += new System.EventHandler(this.fmSetting_Click);
            // 
            // btnExist
            // 
            this.btnExist.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnExist.Location = new System.Drawing.Point(239, 89);
            this.btnExist.Name = "btnExist";
            this.btnExist.Size = new System.Drawing.Size(84, 30);
            this.btnExist.TabIndex = 10;
            this.btnExist.Text = "E&xit";
            this.btnExist.UseVisualStyleBackColor = true;
            this.btnExist.Click += new System.EventHandler(this.miExit_Click);
            // 
            // lblMsgs
            // 
            this.lblMsgs.AutoSize = true;
            this.lblMsgs.Location = new System.Drawing.Point(12, 136);
            this.lblMsgs.Name = "lblMsgs";
            this.lblMsgs.Size = new System.Drawing.Size(205, 22);
            this.lblMsgs.TabIndex = 11;
            this.lblMsgs.Text = "Send <Num Lock> Key";
            this.lblMsgs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMsgs.Click += new System.EventHandler(this.fmSetting_Click);
            this.lblMsgs.DoubleClick += new System.EventHandler(this.lblSendTimes_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(16, 125);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 1);
            this.panel1.TabIndex = 12;
            // 
            // cbInterval
            // 
            this.cbInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterval.FormattingEnabled = true;
            this.cbInterval.Location = new System.Drawing.Point(233, 15);
            this.cbInterval.Name = "cbInterval";
            this.cbInterval.Size = new System.Drawing.Size(90, 29);
            this.cbInterval.TabIndex = 1;
            this.cbInterval.Click += new System.EventHandler(this.fmSetting_Click);
            // 
            // cbRepeatCount
            // 
            this.cbRepeatCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRepeatCount.FormattingEnabled = true;
            this.cbRepeatCount.Location = new System.Drawing.Point(233, 55);
            this.cbRepeatCount.Name = "cbRepeatCount";
            this.cbRepeatCount.Size = new System.Drawing.Size(90, 29);
            this.cbRepeatCount.TabIndex = 2;
            this.cbRepeatCount.Click += new System.EventHandler(this.fmSetting_Click);
            // 
            // cbStartup
            // 
            this.cbStartup.AutoSize = true;
            this.cbStartup.Location = new System.Drawing.Point(30, 93);
            this.cbStartup.Name = "cbStartup";
            this.cbStartup.Size = new System.Drawing.Size(92, 26);
            this.cbStartup.TabIndex = 13;
            this.cbStartup.Text = "Startup";
            this.cbStartup.UseVisualStyleBackColor = true;
            this.cbStartup.CheckedChanged += new System.EventHandler(this.cbStartup_CheckedChanged);
            // 
            // cbMinimize
            // 
            this.cbMinimize.AutoSize = true;
            this.cbMinimize.Location = new System.Drawing.Point(128, 93);
            this.cbMinimize.Name = "cbMinimize";
            this.cbMinimize.Size = new System.Drawing.Size(105, 26);
            this.cbMinimize.TabIndex = 14;
            this.cbMinimize.Text = "Minimize";
            this.cbMinimize.UseVisualStyleBackColor = true;
            this.cbMinimize.CheckedChanged += new System.EventHandler(this.cbMinimize_CheckedChanged);
            // 
            // fmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 189);
            this.Controls.Add(this.cbMinimize);
            this.Controls.Add(this.cbStartup);
            this.Controls.Add(this.cbRepeatCount);
            this.Controls.Add(this.cbInterval);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblMsgs);
            this.Controls.Add(this.btnExist);
            this.Controls.Add(this.lblSend);
            this.Controls.Add(this.lblIdle);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.Name = "fmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anti Lock Screen";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Click += new System.EventHandler(this.fmSetting_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmIdleCheck;
        private System.Windows.Forms.NotifyIcon niTray;
        private System.Windows.Forms.Label lblIdle;
        private System.Windows.Forms.Label lblSend;
        private System.Windows.Forms.Button btnExist;
        private System.Windows.Forms.Label lblMsgs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbInterval;
        private System.Windows.Forms.ComboBox cbRepeatCount;
        private System.Windows.Forms.CheckBox cbStartup;
        private System.Windows.Forms.CheckBox cbMinimize;
    }
}

