namespace MemoryGameForWindows
{
    partial class SettingForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFirstPlayerName = new System.Windows.Forms.TextBox();
            this.textBoxFriend = new System.Windows.Forms.TextBox();
            this.buttonOpponent = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonBoardSize = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Player Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Second Player Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Board Size:";
            // 
            // textBoxFirstPlayerName
            // 
            this.textBoxFirstPlayerName.AccessibleName = "";
            this.textBoxFirstPlayerName.Location = new System.Drawing.Point(173, 19);
            this.textBoxFirstPlayerName.Name = "textBoxFirstPlayerName";
            this.textBoxFirstPlayerName.Size = new System.Drawing.Size(124, 22);
            this.textBoxFirstPlayerName.TabIndex = 3;
            // 
            // textBoxFriend
            // 
            this.textBoxFriend.Enabled = false;
            this.textBoxFriend.Location = new System.Drawing.Point(173, 51);
            this.textBoxFriend.Name = "textBoxFriend";
            this.textBoxFriend.Size = new System.Drawing.Size(124, 22);
            this.textBoxFriend.TabIndex = 4;
            this.textBoxFriend.Text = "- computer -";
            // 
            // buttonOpponent
            // 
            this.buttonOpponent.Location = new System.Drawing.Point(320, 44);
            this.buttonOpponent.Name = "buttonOpponent";
            this.buttonOpponent.Size = new System.Drawing.Size(137, 30);
            this.buttonOpponent.TabIndex = 5;
            this.buttonOpponent.Text = "Against a Friend";
            this.buttonOpponent.UseVisualStyleBackColor = true;
            this.buttonOpponent.Click += new System.EventHandler(this.buttonOpponent_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonStart.Location = new System.Drawing.Point(361, 147);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(96, 29);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "Start!";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonBoardSize
            // 
            this.buttonBoardSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonBoardSize.Location = new System.Drawing.Point(25, 104);
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.Size = new System.Drawing.Size(101, 72);
            this.buttonBoardSize.TabIndex = 7;
            this.buttonBoardSize.UseVisualStyleBackColor = false;
            this.buttonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // SettingForm
            // 
            this.AcceptButton = this.buttonStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 189);
            this.Controls.Add(this.buttonBoardSize);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonOpponent);
            this.Controls.Add(this.textBoxFriend);
            this.Controls.Add(this.textBoxFirstPlayerName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Memory Game - Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFirstPlayerName;
        private System.Windows.Forms.TextBox textBoxFriend;
        private System.Windows.Forms.Button buttonOpponent;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonBoardSize;
    }
}