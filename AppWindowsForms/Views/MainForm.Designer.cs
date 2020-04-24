namespace FurAffinityClassifier.AppWindowsForms.Views
{
    partial class MainForm
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
            this.FolderSettingGroupBox = new System.Windows.Forms.GroupBox();
            this.ToFolderButton = new System.Windows.Forms.Button();
            this.ToFolderTextBox = new System.Windows.Forms.TextBox();
            this.ToFolderLabel = new System.Windows.Forms.Label();
            this.FromFolderButton = new System.Windows.Forms.Button();
            this.FromFolderTextBox = new System.Windows.Forms.TextBox();
            this.FromFolderLabel = new System.Windows.Forms.Label();
            this.ClassificationSettingGroupBox = new System.Windows.Forms.GroupBox();
            this.ClassifyAsLabel = new System.Windows.Forms.Label();
            this.CreateFolderIfNotExistCheckBox = new System.Windows.Forms.CheckBox();
            this.ClassifyAsDataGridView = new System.Windows.Forms.DataGridView();
            this.ButtonExecute = new System.Windows.Forms.Button();
            this.FolderSettingGroupBox.SuspendLayout();
            this.ClassificationSettingGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClassifyAsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FolderSettingGroupBox
            // 
            this.FolderSettingGroupBox.Controls.Add(this.ToFolderButton);
            this.FolderSettingGroupBox.Controls.Add(this.ToFolderTextBox);
            this.FolderSettingGroupBox.Controls.Add(this.ToFolderLabel);
            this.FolderSettingGroupBox.Controls.Add(this.FromFolderButton);
            this.FolderSettingGroupBox.Controls.Add(this.FromFolderTextBox);
            this.FolderSettingGroupBox.Controls.Add(this.FromFolderLabel);
            this.FolderSettingGroupBox.Location = new System.Drawing.Point(12, 12);
            this.FolderSettingGroupBox.Name = "FolderSettingGroupBox";
            this.FolderSettingGroupBox.Size = new System.Drawing.Size(286, 110);
            this.FolderSettingGroupBox.TabIndex = 0;
            this.FolderSettingGroupBox.TabStop = false;
            this.FolderSettingGroupBox.Text = "フォルダー設定";
            // 
            // ToFolderButton
            // 
            this.ToFolderButton.Location = new System.Drawing.Point(112, 81);
            this.ToFolderButton.Name = "ToFolderButton";
            this.ToFolderButton.Size = new System.Drawing.Size(75, 23);
            this.ToFolderButton.TabIndex = 5;
            this.ToFolderButton.Text = "選択...";
            this.ToFolderButton.UseVisualStyleBackColor = true;
            // 
            // ToFolderTextBox
            // 
            this.ToFolderTextBox.Location = new System.Drawing.Point(6, 81);
            this.ToFolderTextBox.Name = "ToFolderTextBox";
            this.ToFolderTextBox.Size = new System.Drawing.Size(100, 23);
            this.ToFolderTextBox.TabIndex = 4;
            // 
            // ToFolderLabel
            // 
            this.ToFolderLabel.AutoSize = true;
            this.ToFolderLabel.Location = new System.Drawing.Point(6, 63);
            this.ToFolderLabel.Name = "ToFolderLabel";
            this.ToFolderLabel.Size = new System.Drawing.Size(46, 15);
            this.ToFolderLabel.TabIndex = 3;
            this.ToFolderLabel.Text = "移動先:";
            // 
            // FromFolderButton
            // 
            this.FromFolderButton.Location = new System.Drawing.Point(112, 37);
            this.FromFolderButton.Name = "FromFolderButton";
            this.FromFolderButton.Size = new System.Drawing.Size(75, 23);
            this.FromFolderButton.TabIndex = 2;
            this.FromFolderButton.Text = "選択...";
            this.FromFolderButton.UseVisualStyleBackColor = true;
            this.FromFolderButton.Click += new System.EventHandler(this.FromFolderButton_Click);
            // 
            // FromFolderTextBox
            // 
            this.FromFolderTextBox.BackColor = System.Drawing.Color.White;
            this.FromFolderTextBox.ForeColor = System.Drawing.Color.Black;
            this.FromFolderTextBox.Location = new System.Drawing.Point(6, 37);
            this.FromFolderTextBox.Name = "FromFolderTextBox";
            this.FromFolderTextBox.Size = new System.Drawing.Size(100, 23);
            this.FromFolderTextBox.TabIndex = 1;
            // 
            // FromFolderLabel
            // 
            this.FromFolderLabel.AutoSize = true;
            this.FromFolderLabel.Location = new System.Drawing.Point(6, 19);
            this.FromFolderLabel.Name = "FromFolderLabel";
            this.FromFolderLabel.Size = new System.Drawing.Size(46, 15);
            this.FromFolderLabel.TabIndex = 0;
            this.FromFolderLabel.Text = "移動元:";
            // 
            // ClassificationSettingGroupBox
            // 
            this.ClassificationSettingGroupBox.Controls.Add(this.ClassifyAsLabel);
            this.ClassificationSettingGroupBox.Controls.Add(this.CreateFolderIfNotExistCheckBox);
            this.ClassificationSettingGroupBox.Controls.Add(this.ClassifyAsDataGridView);
            this.ClassificationSettingGroupBox.Location = new System.Drawing.Point(12, 128);
            this.ClassificationSettingGroupBox.Name = "ClassificationSettingGroupBox";
            this.ClassificationSettingGroupBox.Size = new System.Drawing.Size(286, 320);
            this.ClassificationSettingGroupBox.TabIndex = 1;
            this.ClassificationSettingGroupBox.TabStop = false;
            this.ClassificationSettingGroupBox.Text = "振り分け設定";
            // 
            // ClassifyAsLabel
            // 
            this.ClassifyAsLabel.AutoSize = true;
            this.ClassifyAsLabel.Location = new System.Drawing.Point(6, 44);
            this.ClassifyAsLabel.Name = "ClassifyAsLabel";
            this.ClassifyAsLabel.Size = new System.Drawing.Size(160, 15);
            this.ClassifyAsLabel.TabIndex = 2;
            this.ClassifyAsLabel.Text = "IDと異なるフォルダーに振り分ける";
            // 
            // CreateFolderIfNotExistCheckBox
            // 
            this.CreateFolderIfNotExistCheckBox.AutoSize = true;
            this.CreateFolderIfNotExistCheckBox.Location = new System.Drawing.Point(6, 22);
            this.CreateFolderIfNotExistCheckBox.Name = "CreateFolderIfNotExistCheckBox";
            this.CreateFolderIfNotExistCheckBox.Size = new System.Drawing.Size(219, 19);
            this.CreateFolderIfNotExistCheckBox.TabIndex = 1;
            this.CreateFolderIfNotExistCheckBox.Text = "対応するフォルダーがない場合は作成する";
            this.CreateFolderIfNotExistCheckBox.UseVisualStyleBackColor = true;
            // 
            // ClassifyAsDataGridView
            // 
            this.ClassifyAsDataGridView.BackgroundColor = System.Drawing.Color.DarkGray;
            this.ClassifyAsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ClassifyAsDataGridView.Location = new System.Drawing.Point(6, 62);
            this.ClassifyAsDataGridView.Name = "ClassifyAsDataGridView";
            this.ClassifyAsDataGridView.RowTemplate.Height = 21;
            this.ClassifyAsDataGridView.Size = new System.Drawing.Size(240, 150);
            this.ClassifyAsDataGridView.TabIndex = 0;
            // 
            // ButtonExecute
            // 
            this.ButtonExecute.Location = new System.Drawing.Point(223, 454);
            this.ButtonExecute.Name = "ButtonExecute";
            this.ButtonExecute.Size = new System.Drawing.Size(75, 23);
            this.ButtonExecute.TabIndex = 2;
            this.ButtonExecute.Text = "実行";
            this.ButtonExecute.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.ButtonExecute);
            this.Controls.Add(this.ClassificationSettingGroupBox);
            this.Controls.Add(this.FolderSettingGroupBox);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Fur Affinity Classifier";
            this.FolderSettingGroupBox.ResumeLayout(false);
            this.FolderSettingGroupBox.PerformLayout();
            this.ClassificationSettingGroupBox.ResumeLayout(false);
            this.ClassificationSettingGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClassifyAsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FolderSettingGroupBox;
        private System.Windows.Forms.Button ToFolderButton;
        private System.Windows.Forms.TextBox ToFolderTextBox;
        private System.Windows.Forms.Label ToFolderLabel;
        private System.Windows.Forms.Button FromFolderButton;
        private System.Windows.Forms.TextBox FromFolderTextBox;
        private System.Windows.Forms.Label FromFolderLabel;
        private System.Windows.Forms.GroupBox ClassificationSettingGroupBox;
        private System.Windows.Forms.DataGridView ClassifyAsDataGridView;
        private System.Windows.Forms.CheckBox CreateFolderIfNotExistCheckBox;
        private System.Windows.Forms.Button ButtonExecute;
        private System.Windows.Forms.Label ClassifyAsLabel;
    }
}