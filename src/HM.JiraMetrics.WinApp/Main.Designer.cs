namespace HM.JiraMetrics.WinApp
{
    partial class Main
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.generateReportButton = new System.Windows.Forms.Button();
            this.jqlTextBox = new System.Windows.Forms.TextBox();
            this.jqlLabel = new System.Windows.Forms.Label();
            this.queryHistoryCombobox = new System.Windows.Forms.ComboBox();
            this.saveQueryCheckBox = new System.Windows.Forms.CheckBox();
            this.clearHistoryButton = new System.Windows.Forms.Button();
            this.queryHistoryLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.maxStartDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.MaxStartDateLabel = new System.Windows.Forms.Label();
            this.minStartDateLabel = new System.Windows.Forms.Label();
            this.minStartDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.excludeIssuesThatAreNotDoneCheckBox = new System.Windows.Forms.CheckBox();
            this.setIntervalForDoneDateGroupBox = new System.Windows.Forms.GroupBox();
            this.maxDoneDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.minDoneDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.setIntervalForDoneDateCheckbox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chunkSizeInDaysTextbox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.postcycle2cycleButton = new System.Windows.Forms.Button();
            this.cycle2postcycleButton = new System.Windows.Forms.Button();
            this.postcycleListBox = new System.Windows.Forms.ListBox();
            this.cycle2precycleButton = new System.Windows.Forms.Button();
            this.precycle2cycleButton = new System.Windows.Forms.Button();
            this.cycleListBox = new System.Windows.Forms.ListBox();
            this.precycleListBox = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.setIntervalForDoneDateGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // generateReportButton
            // 
            this.generateReportButton.Location = new System.Drawing.Point(1293, 218);
            this.generateReportButton.Name = "generateReportButton";
            this.generateReportButton.Size = new System.Drawing.Size(98, 43);
            this.generateReportButton.TabIndex = 0;
            this.generateReportButton.Text = "Cycle Time Report";
            this.generateReportButton.UseVisualStyleBackColor = true;
            this.generateReportButton.Click += new System.EventHandler(this.GenerateReport_Click);
            // 
            // jqlTextBox
            // 
            this.jqlTextBox.Location = new System.Drawing.Point(12, 68);
            this.jqlTextBox.Multiline = true;
            this.jqlTextBox.Name = "jqlTextBox";
            this.jqlTextBox.Size = new System.Drawing.Size(1375, 49);
            this.jqlTextBox.TabIndex = 5;
            this.jqlTextBox.UseSystemPasswordChar = true;
            // 
            // jqlLabel
            // 
            this.jqlLabel.AutoSize = true;
            this.jqlLabel.Location = new System.Drawing.Point(9, 52);
            this.jqlLabel.Name = "jqlLabel";
            this.jqlLabel.Size = new System.Drawing.Size(82, 13);
            this.jqlLabel.TabIndex = 4;
            this.jqlLabel.Text = "Enter Jql Query:";
            // 
            // queryHistoryCombobox
            // 
            this.queryHistoryCombobox.FormattingEnabled = true;
            this.queryHistoryCombobox.Location = new System.Drawing.Point(12, 28);
            this.queryHistoryCombobox.Name = "queryHistoryCombobox";
            this.queryHistoryCombobox.Size = new System.Drawing.Size(1271, 21);
            this.queryHistoryCombobox.TabIndex = 6;
            this.queryHistoryCombobox.SelectedIndexChanged += new System.EventHandler(this.QueryHistoryComboBoxSelectedIndexChanged);
            // 
            // saveQueryCheckBox
            // 
            this.saveQueryCheckBox.AutoSize = true;
            this.saveQueryCheckBox.Location = new System.Drawing.Point(26, 29);
            this.saveQueryCheckBox.Name = "saveQueryCheckBox";
            this.saveQueryCheckBox.Size = new System.Drawing.Size(80, 17);
            this.saveQueryCheckBox.TabIndex = 7;
            this.saveQueryCheckBox.Text = "Save query";
            this.saveQueryCheckBox.UseVisualStyleBackColor = true;
            // 
            // clearHistoryButton
            // 
            this.clearHistoryButton.Location = new System.Drawing.Point(1293, 26);
            this.clearHistoryButton.Name = "clearHistoryButton";
            this.clearHistoryButton.Size = new System.Drawing.Size(98, 23);
            this.clearHistoryButton.TabIndex = 8;
            this.clearHistoryButton.Text = "Clear History";
            this.clearHistoryButton.UseVisualStyleBackColor = true;
            this.clearHistoryButton.Click += new System.EventHandler(this.ClearHistoryButtonClick);
            // 
            // queryHistoryLabel
            // 
            this.queryHistoryLabel.AutoSize = true;
            this.queryHistoryLabel.Location = new System.Drawing.Point(9, 12);
            this.queryHistoryLabel.Name = "queryHistoryLabel";
            this.queryHistoryLabel.Size = new System.Drawing.Size(39, 13);
            this.queryHistoryLabel.TabIndex = 9;
            this.queryHistoryLabel.Text = "History";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.maxStartDateTimePicker);
            this.groupBox1.Controls.Add(this.MaxStartDateLabel);
            this.groupBox1.Controls.Add(this.minStartDateLabel);
            this.groupBox1.Controls.Add(this.minStartDateTimePicker);
            this.groupBox1.Location = new System.Drawing.Point(214, 123);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 138);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter start interval of selected issues";
            // 
            // maxStartDateTimePicker
            // 
            this.maxStartDateTimePicker.Location = new System.Drawing.Point(18, 86);
            this.maxStartDateTimePicker.Name = "maxStartDateTimePicker";
            this.maxStartDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.maxStartDateTimePicker.TabIndex = 18;
            // 
            // MaxStartDateLabel
            // 
            this.MaxStartDateLabel.AutoSize = true;
            this.MaxStartDateLabel.Location = new System.Drawing.Point(15, 70);
            this.MaxStartDateLabel.Name = "MaxStartDateLabel";
            this.MaxStartDateLabel.Size = new System.Drawing.Size(74, 13);
            this.MaxStartDateLabel.TabIndex = 17;
            this.MaxStartDateLabel.Text = "Max start date";
            // 
            // minStartDateLabel
            // 
            this.minStartDateLabel.AutoSize = true;
            this.minStartDateLabel.Location = new System.Drawing.Point(15, 25);
            this.minStartDateLabel.Name = "minStartDateLabel";
            this.minStartDateLabel.Size = new System.Drawing.Size(71, 13);
            this.minStartDateLabel.TabIndex = 16;
            this.minStartDateLabel.Text = "Min start date";
            // 
            // minStartDateTimePicker
            // 
            this.minStartDateTimePicker.Location = new System.Drawing.Point(18, 40);
            this.minStartDateTimePicker.Name = "minStartDateTimePicker";
            this.minStartDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.minStartDateTimePicker.TabIndex = 15;
            this.minStartDateTimePicker.Value = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            // 
            // excludeIssuesThatAreNotDoneCheckBox
            // 
            this.excludeIssuesThatAreNotDoneCheckBox.AutoSize = true;
            this.excludeIssuesThatAreNotDoneCheckBox.Checked = true;
            this.excludeIssuesThatAreNotDoneCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.excludeIssuesThatAreNotDoneCheckBox.Location = new System.Drawing.Point(742, 123);
            this.excludeIssuesThatAreNotDoneCheckBox.Name = "excludeIssuesThatAreNotDoneCheckBox";
            this.excludeIssuesThatAreNotDoneCheckBox.Size = new System.Drawing.Size(180, 17);
            this.excludeIssuesThatAreNotDoneCheckBox.TabIndex = 18;
            this.excludeIssuesThatAreNotDoneCheckBox.Text = "Exclude issues that are not done";
            this.excludeIssuesThatAreNotDoneCheckBox.UseVisualStyleBackColor = true;
            // 
            // setIntervalForDoneDateGroupBox
            // 
            this.setIntervalForDoneDateGroupBox.Controls.Add(this.maxDoneDateTimePicker);
            this.setIntervalForDoneDateGroupBox.Controls.Add(this.label1);
            this.setIntervalForDoneDateGroupBox.Controls.Add(this.label2);
            this.setIntervalForDoneDateGroupBox.Controls.Add(this.minDoneDateTimePicker);
            this.setIntervalForDoneDateGroupBox.Location = new System.Drawing.Point(478, 148);
            this.setIntervalForDoneDateGroupBox.Name = "setIntervalForDoneDateGroupBox";
            this.setIntervalForDoneDateGroupBox.Size = new System.Drawing.Size(258, 122);
            this.setIntervalForDoneDateGroupBox.TabIndex = 19;
            this.setIntervalForDoneDateGroupBox.TabStop = false;
            this.setIntervalForDoneDateGroupBox.Text = "Filter end interval of selected issues";
            this.setIntervalForDoneDateGroupBox.Visible = false;
            // 
            // maxDoneDateTimePicker
            // 
            this.maxDoneDateTimePicker.Location = new System.Drawing.Point(18, 86);
            this.maxDoneDateTimePicker.Name = "maxDoneDateTimePicker";
            this.maxDoneDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.maxDoneDateTimePicker.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Max end date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Min end date";
            // 
            // minDoneDateTimePicker
            // 
            this.minDoneDateTimePicker.Location = new System.Drawing.Point(18, 40);
            this.minDoneDateTimePicker.Name = "minDoneDateTimePicker";
            this.minDoneDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.minDoneDateTimePicker.TabIndex = 15;
            // 
            // setIntervalForDoneDateCheckbox
            // 
            this.setIntervalForDoneDateCheckbox.AutoSize = true;
            this.setIntervalForDoneDateCheckbox.Location = new System.Drawing.Point(478, 123);
            this.setIntervalForDoneDateCheckbox.Name = "setIntervalForDoneDateCheckbox";
            this.setIntervalForDoneDateCheckbox.Size = new System.Drawing.Size(145, 17);
            this.setIntervalForDoneDateCheckbox.TabIndex = 20;
            this.setIntervalForDoneDateCheckbox.Text = "Set interval for done date";
            this.setIntervalForDoneDateCheckbox.UseVisualStyleBackColor = true;
            this.setIntervalForDoneDateCheckbox.CheckedChanged += new System.EventHandler(this.SetDoneDateIntervalCheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Chunk Size in Days";
            // 
            // chunkSizeInDaysTextbox
            // 
            this.chunkSizeInDaysTextbox.Location = new System.Drawing.Point(23, 89);
            this.chunkSizeInDaysTextbox.Name = "chunkSizeInDaysTextbox";
            this.chunkSizeInDaysTextbox.Size = new System.Drawing.Size(100, 20);
            this.chunkSizeInDaysTextbox.TabIndex = 22;
            this.chunkSizeInDaysTextbox.Text = "365";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.saveQueryCheckBox);
            this.groupBox2.Controls.Add(this.chunkSizeInDaysTextbox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(1083, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 126);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.postcycle2cycleButton);
            this.groupBox3.Controls.Add(this.cycle2postcycleButton);
            this.groupBox3.Controls.Add(this.postcycleListBox);
            this.groupBox3.Controls.Add(this.cycle2precycleButton);
            this.groupBox3.Controls.Add(this.precycle2cycleButton);
            this.groupBox3.Controls.Add(this.cycleListBox);
            this.groupBox3.Controls.Add(this.precycleListBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 276);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(737, 351);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Select statuses for Cycle";
            // 
            // postcycle2cycleButton
            // 
            this.postcycle2cycleButton.Location = new System.Drawing.Point(466, 188);
            this.postcycle2cycleButton.Name = "postcycle2cycleButton";
            this.postcycle2cycleButton.Size = new System.Drawing.Size(53, 23);
            this.postcycle2cycleButton.TabIndex = 35;
            this.postcycle2cycleButton.Text = "<--";
            this.postcycle2cycleButton.UseVisualStyleBackColor = true;
            this.postcycle2cycleButton.Click += new System.EventHandler(this.postcycle2cycleButton_Click);
            // 
            // cycle2postcycleButton
            // 
            this.cycle2postcycleButton.Location = new System.Drawing.Point(466, 159);
            this.cycle2postcycleButton.Name = "cycle2postcycleButton";
            this.cycle2postcycleButton.Size = new System.Drawing.Size(53, 23);
            this.cycle2postcycleButton.TabIndex = 34;
            this.cycle2postcycleButton.Text = "-->";
            this.cycle2postcycleButton.UseVisualStyleBackColor = true;
            this.cycle2postcycleButton.Click += new System.EventHandler(this.cycle2postcycleButton_Click);
            // 
            // postcycleListBox
            // 
            this.postcycleListBox.FormattingEnabled = true;
            this.postcycleListBox.Location = new System.Drawing.Point(525, 22);
            this.postcycleListBox.Name = "postcycleListBox";
            this.postcycleListBox.Size = new System.Drawing.Size(199, 329);
            this.postcycleListBox.TabIndex = 33;
            // 
            // cycle2precycleButton
            // 
            this.cycle2precycleButton.Location = new System.Drawing.Point(202, 188);
            this.cycle2precycleButton.Name = "cycle2precycleButton";
            this.cycle2precycleButton.Size = new System.Drawing.Size(53, 23);
            this.cycle2precycleButton.TabIndex = 32;
            this.cycle2precycleButton.Text = "<--";
            this.cycle2precycleButton.UseVisualStyleBackColor = true;
            this.cycle2precycleButton.Click += new System.EventHandler(this.cycle2precycleButton_Click);
            // 
            // precycle2cycleButton
            // 
            this.precycle2cycleButton.Location = new System.Drawing.Point(202, 159);
            this.precycle2cycleButton.Name = "precycle2cycleButton";
            this.precycle2cycleButton.Size = new System.Drawing.Size(53, 23);
            this.precycle2cycleButton.TabIndex = 31;
            this.precycle2cycleButton.Text = "-->";
            this.precycle2cycleButton.UseVisualStyleBackColor = true;
            this.precycle2cycleButton.Click += new System.EventHandler(this.precycle2cycleButton_Click);
            // 
            // cycleListBox
            // 
            this.cycleListBox.FormattingEnabled = true;
            this.cycleListBox.Location = new System.Drawing.Point(261, 22);
            this.cycleListBox.Name = "cycleListBox";
            this.cycleListBox.Size = new System.Drawing.Size(199, 329);
            this.cycleListBox.TabIndex = 30;
            // 
            // precycleListBox
            // 
            this.precycleListBox.FormattingEnabled = true;
            this.precycleListBox.Location = new System.Drawing.Point(0, 22);
            this.precycleListBox.Name = "precycleListBox";
            this.precycleListBox.Size = new System.Drawing.Size(196, 329);
            this.precycleListBox.TabIndex = 29;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1403, 657);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.setIntervalForDoneDateCheckbox);
            this.Controls.Add(this.setIntervalForDoneDateGroupBox);
            this.Controls.Add(this.excludeIssuesThatAreNotDoneCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.queryHistoryLabel);
            this.Controls.Add(this.clearHistoryButton);
            this.Controls.Add(this.queryHistoryCombobox);
            this.Controls.Add(this.jqlTextBox);
            this.Controls.Add(this.jqlLabel);
            this.Controls.Add(this.generateReportButton);
            this.Name = "Main";
            this.Text = "Jira Metrics";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.setIntervalForDoneDateGroupBox.ResumeLayout(false);
            this.setIntervalForDoneDateGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button generateReportButton;
        private System.Windows.Forms.TextBox jqlTextBox;
        private System.Windows.Forms.Label jqlLabel;
        private System.Windows.Forms.ComboBox queryHistoryCombobox;
        private System.Windows.Forms.CheckBox saveQueryCheckBox;
        private System.Windows.Forms.Button clearHistoryButton;
        private System.Windows.Forms.Label queryHistoryLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker maxStartDateTimePicker;
        private System.Windows.Forms.Label MaxStartDateLabel;
        private System.Windows.Forms.Label minStartDateLabel;
        private System.Windows.Forms.DateTimePicker minStartDateTimePicker;
        private System.Windows.Forms.CheckBox excludeIssuesThatAreNotDoneCheckBox;
        private System.Windows.Forms.GroupBox setIntervalForDoneDateGroupBox;
        private System.Windows.Forms.DateTimePicker maxDoneDateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker minDoneDateTimePicker;
        private System.Windows.Forms.CheckBox setIntervalForDoneDateCheckbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox chunkSizeInDaysTextbox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button postcycle2cycleButton;
        private System.Windows.Forms.Button cycle2postcycleButton;
        private System.Windows.Forms.ListBox postcycleListBox;
        private System.Windows.Forms.Button cycle2precycleButton;
        private System.Windows.Forms.Button precycle2cycleButton;
        private System.Windows.Forms.ListBox cycleListBox;
        private System.Windows.Forms.ListBox precycleListBox;
    }
}

