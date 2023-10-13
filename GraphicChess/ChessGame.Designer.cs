namespace GraphicChess
{
    partial class ChessGame
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Game = new System.Windows.Forms.ToolStripMenuItem();
            this.undoMoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoMoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.radioButtonResign = new System.Windows.Forms.RadioButton();
            this.radioButtonComputerDecides = new System.Windows.Forms.RadioButton();
            this.radioButtonUserDecides = new System.Windows.Forms.RadioButton();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.startNewGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Game});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Game
            // 
            this.Game.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoMoveToolStripMenuItem,
            this.redoMoveToolStripMenuItem,
            this.startNewGameToolStripMenuItem});
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(50, 20);
            this.Game.Text = "Game";
            // 
            // undoMoveToolStripMenuItem
            // 
            this.undoMoveToolStripMenuItem.Name = "undoMoveToolStripMenuItem";
            this.undoMoveToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.undoMoveToolStripMenuItem.Text = "Undo Last Move";
            this.undoMoveToolStripMenuItem.Click += new System.EventHandler(this.undoMoveToolStripMenuItem_Click);
            // 
            // redoMoveToolStripMenuItem
            // 
            this.redoMoveToolStripMenuItem.Name = "redoMoveToolStripMenuItem";
            this.redoMoveToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.redoMoveToolStripMenuItem.Text = "Redo Move";
            this.redoMoveToolStripMenuItem.Click += new System.EventHandler(this.redoMoveToolStripMenuItem_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(702, 339);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 16;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // radioButtonResign
            // 
            this.radioButtonResign.AutoSize = true;
            this.radioButtonResign.Location = new System.Drawing.Point(556, 281);
            this.radioButtonResign.Name = "radioButtonResign";
            this.radioButtonResign.Size = new System.Drawing.Size(58, 17);
            this.radioButtonResign.TabIndex = 15;
            this.radioButtonResign.TabStop = true;
            this.radioButtonResign.Text = "Resign";
            this.radioButtonResign.UseVisualStyleBackColor = true;
            this.radioButtonResign.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButtonComputerDecides
            // 
            this.radioButtonComputerDecides.AutoSize = true;
            this.radioButtonComputerDecides.Location = new System.Drawing.Point(556, 258);
            this.radioButtonComputerDecides.Name = "radioButtonComputerDecides";
            this.radioButtonComputerDecides.Size = new System.Drawing.Size(174, 17);
            this.radioButtonComputerDecides.TabIndex = 14;
            this.radioButtonComputerDecides.TabStop = true;
            this.radioButtonComputerDecides.Text = "Let computer decide next move";
            this.radioButtonComputerDecides.UseVisualStyleBackColor = true;
            this.radioButtonComputerDecides.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButtonUserDecides
            // 
            this.radioButtonUserDecides.AutoSize = true;
            this.radioButtonUserDecides.Location = new System.Drawing.Point(556, 235);
            this.radioButtonUserDecides.Name = "radioButtonUserDecides";
            this.radioButtonUserDecides.Size = new System.Drawing.Size(100, 17);
            this.radioButtonUserDecides.TabIndex = 13;
            this.radioButtonUserDecides.TabStop = true;
            this.radioButtonUserDecides.Text = "This is my move";
            this.radioButtonUserDecides.UseVisualStyleBackColor = true;
            this.radioButtonUserDecides.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(556, 164);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 11;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(556, 190);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 12;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(527, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "To:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(517, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "From:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(497, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Message:";
            // 
            // txtMessage
            // 
            this.txtMessage.CausesValidation = false;
            this.txtMessage.Enabled = false;
            this.txtMessage.Location = new System.Drawing.Point(556, 43);
            this.txtMessage.MaximumSize = new System.Drawing.Size(221, 100);
            this.txtMessage.MinimumSize = new System.Drawing.Size(221, 100);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(221, 100);
            this.txtMessage.TabIndex = 18;
            this.txtMessage.TabStop = false;
            // 
            // startNewGameToolStripMenuItem
            // 
            this.startNewGameToolStripMenuItem.Name = "startNewGameToolStripMenuItem";
            this.startNewGameToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.startNewGameToolStripMenuItem.Text = "Start New Game";
            this.startNewGameToolStripMenuItem.Click += new System.EventHandler(this.startNewGameToolStripMenuItem_Click);
            // 
            // ChessGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 370);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.radioButtonResign);
            this.Controls.Add(this.radioButtonComputerDecides);
            this.Controls.Add(this.radioButtonUserDecides);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ChessGame";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ChessGame_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Game;
        private System.Windows.Forms.ToolStripMenuItem undoMoveToolStripMenuItem;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.RadioButton radioButtonResign;
        private System.Windows.Forms.RadioButton radioButtonComputerDecides;
        private System.Windows.Forms.RadioButton radioButtonUserDecides;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ToolStripMenuItem redoMoveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startNewGameToolStripMenuItem;
    }
}

