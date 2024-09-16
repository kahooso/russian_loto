namespace RussianLoto
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.nextRoundButton = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.oneCardRadioButton = new System.Windows.Forms.RadioButton();
            this.twoCardRadioButton = new System.Windows.Forms.RadioButton();
            this.threeCardRadioButton = new System.Windows.Forms.RadioButton();
            this.fourthCardRadioButton = new System.Windows.Forms.RadioButton();
            this.cardsGroupBox = new System.Windows.Forms.GroupBox();
            this.gameSettingsPanel = new System.Windows.Forms.Panel();
            this.difficultyGroupBox = new System.Windows.Forms.GroupBox();
            this.hardRadioButton = new System.Windows.Forms.RadioButton();
            this.mediumRadioButton = new System.Windows.Forms.RadioButton();
            this.easyRadioButton = new System.Windows.Forms.RadioButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.nameToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.balanceToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.exitToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.appartmentButton = new System.Windows.Forms.Button();
            this.cardsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.drawnNumberLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cardsGroupBox.SuspendLayout();
            this.gameSettingsPanel.SuspendLayout();
            this.difficultyGroupBox.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nextRoundButton
            // 
            this.nextRoundButton.Location = new System.Drawing.Point(12, 116);
            this.nextRoundButton.Name = "nextRoundButton";
            this.nextRoundButton.Size = new System.Drawing.Size(268, 41);
            this.nextRoundButton.TabIndex = 3;
            this.nextRoundButton.Text = "Играть";
            this.nextRoundButton.UseVisualStyleBackColor = true;
            this.nextRoundButton.Click += new System.EventHandler(this.nextRoundButton_Click);
            // 
            // oneCardRadioButton
            // 
            this.oneCardRadioButton.AutoSize = true;
            this.oneCardRadioButton.Checked = true;
            this.oneCardRadioButton.Location = new System.Drawing.Point(6, 19);
            this.oneCardRadioButton.Name = "oneCardRadioButton";
            this.oneCardRadioButton.Size = new System.Drawing.Size(51, 17);
            this.oneCardRadioButton.TabIndex = 0;
            this.oneCardRadioButton.TabStop = true;
            this.oneCardRadioButton.Text = "Одна";
            this.oneCardRadioButton.UseVisualStyleBackColor = true;
            this.oneCardRadioButton.Click += new System.EventHandler(this.allChanges_RadioButton);
            // 
            // twoCardRadioButton
            // 
            this.twoCardRadioButton.AutoSize = true;
            this.twoCardRadioButton.Location = new System.Drawing.Point(63, 19);
            this.twoCardRadioButton.Name = "twoCardRadioButton";
            this.twoCardRadioButton.Size = new System.Drawing.Size(46, 17);
            this.twoCardRadioButton.TabIndex = 1;
            this.twoCardRadioButton.Text = "Две";
            this.twoCardRadioButton.UseVisualStyleBackColor = true;
            this.twoCardRadioButton.Click += new System.EventHandler(this.allChanges_RadioButton);
            // 
            // threeCardRadioButton
            // 
            this.threeCardRadioButton.AutoSize = true;
            this.threeCardRadioButton.Location = new System.Drawing.Point(6, 42);
            this.threeCardRadioButton.Name = "threeCardRadioButton";
            this.threeCardRadioButton.Size = new System.Drawing.Size(44, 17);
            this.threeCardRadioButton.TabIndex = 2;
            this.threeCardRadioButton.Text = "Три";
            this.threeCardRadioButton.UseVisualStyleBackColor = true;
            this.threeCardRadioButton.Click += new System.EventHandler(this.allChanges_RadioButton);
            // 
            // fourthCardRadioButton
            // 
            this.fourthCardRadioButton.AutoSize = true;
            this.fourthCardRadioButton.Location = new System.Drawing.Point(63, 42);
            this.fourthCardRadioButton.Name = "fourthCardRadioButton";
            this.fourthCardRadioButton.Size = new System.Drawing.Size(64, 17);
            this.fourthCardRadioButton.TabIndex = 3;
            this.fourthCardRadioButton.Text = "Четыре";
            this.fourthCardRadioButton.UseVisualStyleBackColor = true;
            this.fourthCardRadioButton.Click += new System.EventHandler(this.allChanges_RadioButton);
            // 
            // cardsGroupBox
            // 
            this.cardsGroupBox.Controls.Add(this.oneCardRadioButton);
            this.cardsGroupBox.Controls.Add(this.fourthCardRadioButton);
            this.cardsGroupBox.Controls.Add(this.twoCardRadioButton);
            this.cardsGroupBox.Controls.Add(this.threeCardRadioButton);
            this.cardsGroupBox.Location = new System.Drawing.Point(12, 9);
            this.cardsGroupBox.Name = "cardsGroupBox";
            this.cardsGroupBox.Size = new System.Drawing.Size(131, 101);
            this.cardsGroupBox.TabIndex = 5;
            this.cardsGroupBox.TabStop = false;
            this.cardsGroupBox.Text = "Количество карт";
            // 
            // gameSettingsPanel
            // 
            this.gameSettingsPanel.Controls.Add(this.difficultyGroupBox);
            this.gameSettingsPanel.Controls.Add(this.cardsGroupBox);
            this.gameSettingsPanel.Controls.Add(this.nextRoundButton);
            this.gameSettingsPanel.Location = new System.Drawing.Point(440, 80);
            this.gameSettingsPanel.Name = "gameSettingsPanel";
            this.gameSettingsPanel.Size = new System.Drawing.Size(291, 160);
            this.gameSettingsPanel.TabIndex = 6;
            // 
            // difficultyGroupBox
            // 
            this.difficultyGroupBox.Controls.Add(this.hardRadioButton);
            this.difficultyGroupBox.Controls.Add(this.mediumRadioButton);
            this.difficultyGroupBox.Controls.Add(this.easyRadioButton);
            this.difficultyGroupBox.Location = new System.Drawing.Point(149, 9);
            this.difficultyGroupBox.Name = "difficultyGroupBox";
            this.difficultyGroupBox.Size = new System.Drawing.Size(131, 101);
            this.difficultyGroupBox.TabIndex = 6;
            this.difficultyGroupBox.TabStop = false;
            this.difficultyGroupBox.Text = "Сложность";
            // 
            // hardRadioButton
            // 
            this.hardRadioButton.AutoSize = true;
            this.hardRadioButton.Location = new System.Drawing.Point(4, 65);
            this.hardRadioButton.Name = "hardRadioButton";
            this.hardRadioButton.Size = new System.Drawing.Size(70, 17);
            this.hardRadioButton.TabIndex = 2;
            this.hardRadioButton.Text = "Тяжёлая";
            this.hardRadioButton.UseVisualStyleBackColor = true;
            this.hardRadioButton.Click += new System.EventHandler(this.changeDifficulty_RadioButtons);
            // 
            // mediumRadioButton
            // 
            this.mediumRadioButton.AutoSize = true;
            this.mediumRadioButton.Location = new System.Drawing.Point(6, 42);
            this.mediumRadioButton.Name = "mediumRadioButton";
            this.mediumRadioButton.Size = new System.Drawing.Size(68, 17);
            this.mediumRadioButton.TabIndex = 1;
            this.mediumRadioButton.Text = "Средняя";
            this.mediumRadioButton.UseVisualStyleBackColor = true;
            this.mediumRadioButton.Click += new System.EventHandler(this.changeDifficulty_RadioButtons);
            // 
            // easyRadioButton
            // 
            this.easyRadioButton.AutoSize = true;
            this.easyRadioButton.Checked = true;
            this.easyRadioButton.Location = new System.Drawing.Point(6, 19);
            this.easyRadioButton.Name = "easyRadioButton";
            this.easyRadioButton.Size = new System.Drawing.Size(62, 17);
            this.easyRadioButton.TabIndex = 0;
            this.easyRadioButton.TabStop = true;
            this.easyRadioButton.Text = "Лёгкая";
            this.easyRadioButton.UseVisualStyleBackColor = true;
            this.easyRadioButton.Click += new System.EventHandler(this.changeDifficulty_RadioButtons);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameToolStripLabel,
            this.balanceToolStripLabel,
            this.exitToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(743, 53);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // nameToolStripLabel
            // 
            this.nameToolStripLabel.Name = "nameToolStripLabel";
            this.nameToolStripLabel.Size = new System.Drawing.Size(47, 50);
            this.nameToolStripLabel.Text = "Логин: ";
            // 
            // balanceToolStripLabel
            // 
            this.balanceToolStripLabel.Name = "balanceToolStripLabel";
            this.balanceToolStripLabel.Size = new System.Drawing.Size(66, 50);
            this.balanceToolStripLabel.Text = "Баланс: - €";
            // 
            // exitToolStripButton
            // 
            this.exitToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.exitToolStripButton.AutoSize = false;
            this.exitToolStripButton.BackgroundImage = global::RussianLoto.Properties.Resources.exit;
            this.exitToolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exitToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exitToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitToolStripButton.Name = "exitToolStripButton";
            this.exitToolStripButton.Size = new System.Drawing.Size(50, 50);
            this.exitToolStripButton.Text = "Выход";
            this.exitToolStripButton.Click += new System.EventHandler(this.exitToolStripButton_Click);
            // 
            // appartmentButton
            // 
            this.appartmentButton.Location = new System.Drawing.Point(452, 246);
            this.appartmentButton.Name = "appartmentButton";
            this.appartmentButton.Size = new System.Drawing.Size(268, 34);
            this.appartmentButton.TabIndex = 8;
            this.appartmentButton.Text = "Квартира";
            this.appartmentButton.UseVisualStyleBackColor = true;
            this.appartmentButton.Click += new System.EventHandler(this.appartmentButton_Click);
            // 
            // cardsFlowLayoutPanel
            // 
            this.cardsFlowLayoutPanel.Location = new System.Drawing.Point(8, 80);
            this.cardsFlowLayoutPanel.Name = "cardsFlowLayoutPanel";
            this.cardsFlowLayoutPanel.Size = new System.Drawing.Size(426, 200);
            this.cardsFlowLayoutPanel.TabIndex = 9;
            // 
            // drawnNumberLabel
            // 
            this.drawnNumberLabel.Location = new System.Drawing.Point(8, 283);
            this.drawnNumberLabel.Name = "drawnNumberLabel";
            this.drawnNumberLabel.Size = new System.Drawing.Size(215, 13);
            this.drawnNumberLabel.TabIndex = 2;
            this.drawnNumberLabel.Text = "-";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(743, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.informationToolStripMenuItem.Text = "Информация";
            this.informationToolStripMenuItem.Click += new System.EventHandler(this.informationToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 450);
            this.Controls.Add(this.gameSettingsPanel);
            this.Controls.Add(this.cardsFlowLayoutPanel);
            this.Controls.Add(this.appartmentButton);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.drawnNumberLabel);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.cardsGroupBox.ResumeLayout(false);
            this.cardsGroupBox.PerformLayout();
            this.gameSettingsPanel.ResumeLayout(false);
            this.difficultyGroupBox.ResumeLayout(false);
            this.difficultyGroupBox.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button nextRoundButton;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.RadioButton fourthCardRadioButton;
        private System.Windows.Forms.RadioButton threeCardRadioButton;
        private System.Windows.Forms.RadioButton twoCardRadioButton;
        private System.Windows.Forms.RadioButton oneCardRadioButton;
        private System.Windows.Forms.GroupBox cardsGroupBox;
        private System.Windows.Forms.Panel gameSettingsPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel balanceToolStripLabel;
        private System.Windows.Forms.ToolStripLabel nameToolStripLabel;
        private System.Windows.Forms.Button appartmentButton;
        private System.Windows.Forms.FlowLayoutPanel cardsFlowLayoutPanel;
        private System.Windows.Forms.GroupBox difficultyGroupBox;
        private System.Windows.Forms.RadioButton hardRadioButton;
        private System.Windows.Forms.RadioButton mediumRadioButton;
        private System.Windows.Forms.RadioButton easyRadioButton;
        private System.Windows.Forms.ToolStripButton exitToolStripButton;
        private System.Windows.Forms.Label drawnNumberLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
    }
}

