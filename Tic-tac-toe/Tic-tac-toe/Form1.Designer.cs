namespace Tic_tac_toe
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.startGameBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.computerFirstMove = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.clearPanelBtn = new System.Windows.Forms.Button();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.playerFirstMove = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.statsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startGameBtn
            // 
            this.startGameBtn.Location = new System.Drawing.Point(338, 98);
            this.startGameBtn.Name = "startGameBtn";
            this.startGameBtn.Size = new System.Drawing.Size(75, 23);
            this.startGameBtn.TabIndex = 1;
            this.startGameBtn.Text = "Start";
            this.startGameBtn.UseVisualStyleBackColor = true;
            this.startGameBtn.Click += new System.EventHandler(this.startGameBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 302);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // computerFirstMove
            // 
            this.computerFirstMove.AutoSize = true;
            this.computerFirstMove.Location = new System.Drawing.Point(338, 51);
            this.computerFirstMove.Name = "computerFirstMove";
            this.computerFirstMove.Size = new System.Drawing.Size(70, 17);
            this.computerFirstMove.TabIndex = 3;
            this.computerFirstMove.TabStop = true;
            this.computerFirstMove.Text = "Computer";
            this.computerFirstMove.UseVisualStyleBackColor = true;
            this.computerFirstMove.CheckedChanged += new System.EventHandler(this.computerFirstMove_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(320, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 157);
            this.label1.TabIndex = 5;
            // 
            // clearPanelBtn
            // 
            this.clearPanelBtn.Location = new System.Drawing.Point(338, 269);
            this.clearPanelBtn.Name = "clearPanelBtn";
            this.clearPanelBtn.Size = new System.Drawing.Size(75, 23);
            this.clearPanelBtn.TabIndex = 6;
            this.clearPanelBtn.Text = "Clear";
            this.clearPanelBtn.UseVisualStyleBackColor = true;
            this.clearPanelBtn.Click += new System.EventHandler(this.clearPanelBtn_Click);
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(337, 24);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(89, 20);
            this.userNameTextBox.TabIndex = 7;
            // 
            // playerFirstMove
            // 
            this.playerFirstMove.AutoSize = true;
            this.playerFirstMove.Location = new System.Drawing.Point(338, 70);
            this.playerFirstMove.Name = "playerFirstMove";
            this.playerFirstMove.Size = new System.Drawing.Size(47, 17);
            this.playerFirstMove.TabIndex = 4;
            this.playerFirstMove.TabStop = true;
            this.playerFirstMove.Text = "User";
            this.playerFirstMove.UseVisualStyleBackColor = true;
            this.playerFirstMove.CheckedChanged += new System.EventHandler(this.playerFirstMove_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(337, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "User Name";
            // 
            // statsButton
            // 
            this.statsButton.Location = new System.Drawing.Point(338, 295);
            this.statsButton.Name = "statsButton";
            this.statsButton.Size = new System.Drawing.Size(75, 23);
            this.statsButton.TabIndex = 9;
            this.statsButton.Text = "Show stats";
            this.statsButton.UseVisualStyleBackColor = true;
            this.statsButton.Click += new System.EventHandler(this.statsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 325);
            this.Controls.Add(this.statsButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.clearPanelBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.playerFirstMove);
            this.Controls.Add(this.computerFirstMove);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.startGameBtn);
            this.Name = "Form1";
            this.Text = "Tic tac toe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startGameBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton computerFirstMove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clearPanelBtn;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.RadioButton playerFirstMove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button statsButton;
    }
}

