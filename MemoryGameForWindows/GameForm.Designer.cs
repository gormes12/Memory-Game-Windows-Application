namespace MemoryGameForWindows
{
    partial class GameForm
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
            this.components = new System.ComponentModel.Container();
            this.labelCurrentPlayer = new System.Windows.Forms.Label();
            this.labelSecondPlayerRes = new System.Windows.Forms.Label();
            this.labelFirstPlayerRes = new System.Windows.Forms.Label();
            this.buttonFirstOnBoard = new System.Windows.Forms.Button();
            this.timerShowCards = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelCurrentPlayer
            // 
            this.labelCurrentPlayer.AutoSize = true;
            this.labelCurrentPlayer.Location = new System.Drawing.Point(12, 368);
            this.labelCurrentPlayer.Name = "labelCurrentPlayer";
            this.labelCurrentPlayer.Size = new System.Drawing.Size(107, 17);
            this.labelCurrentPlayer.TabIndex = 0;
            this.labelCurrentPlayer.Text = "Current Player: ";
            // 
            // labelSecondPlayerRes
            // 
            this.labelSecondPlayerRes.AutoSize = true;
            this.labelSecondPlayerRes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.labelSecondPlayerRes.Location = new System.Drawing.Point(12, 424);
            this.labelSecondPlayerRes.Name = "labelSecondPlayerRes";
            this.labelSecondPlayerRes.Size = new System.Drawing.Size(107, 17);
            this.labelSecondPlayerRes.TabIndex = 1;
            this.labelSecondPlayerRes.Text = "Current Player: ";
            // 
            // labelFirstPlayerRes
            // 
            this.labelFirstPlayerRes.AutoSize = true;
            this.labelFirstPlayerRes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelFirstPlayerRes.Location = new System.Drawing.Point(12, 396);
            this.labelFirstPlayerRes.Name = "labelFirstPlayerRes";
            this.labelFirstPlayerRes.Size = new System.Drawing.Size(107, 17);
            this.labelFirstPlayerRes.TabIndex = 2;
            this.labelFirstPlayerRes.Text = "Current Player: ";
            // 
            // buttonFirstOnBoard
            // 
            this.buttonFirstOnBoard.Location = new System.Drawing.Point(15, 26);
            this.buttonFirstOnBoard.Name = "buttonFirstOnBoard";
            this.buttonFirstOnBoard.Size = new System.Drawing.Size(67, 68);
            this.buttonFirstOnBoard.TabIndex = 3;
            this.buttonFirstOnBoard.UseVisualStyleBackColor = true;
            // 
            // timerShowCards
            // 
            this.timerShowCards.Interval = 750;
            this.timerShowCards.Tick += new System.EventHandler(this.timerShowCard_Tick);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonFirstOnBoard);
            this.Controls.Add(this.labelFirstPlayerRes);
            this.Controls.Add(this.labelSecondPlayerRes);
            this.Controls.Add(this.labelCurrentPlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCurrentPlayer;
        private System.Windows.Forms.Label labelSecondPlayerRes;
        private System.Windows.Forms.Label labelFirstPlayerRes;
        private System.Windows.Forms.Button buttonFirstOnBoard;
        private System.Windows.Forms.Timer timerShowCards;
    }
}