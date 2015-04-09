using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Olifant.JiraMetrics.WinApp;
using Olifant.JiraMetrics.Lib;
using Olifant.JiraMetrics.Lib.Jira;
using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Lib.Metrics;
using Olifant.JiraMetrics.Lib.Metrics.Filters;
using Olifant.JiraMetrics.Lib.Metrics.TextReport;

namespace Olifant.JiraMetrics.WinApp
{
    public partial class Main : Form
    {
        private readonly JqlQueryManager jqlQueryManager;

        public Main()
        {
            InitializeComponent();

            this.jqlQueryManager = new JqlQueryManager();

            queryHistoryCombobox.DataSource = jqlQueryManager.JqlQueries;
            setIntervalForDoneDateGroupBox.Visible = setIntervalForDoneDateCheckbox.Checked;

            var defaultStartDateTime = new StartDateFilter();
            minStartDateTimePicker.Value = defaultStartDateTime.MinStartDateTime;
            maxStartDateTimePicker.Value = defaultStartDateTime.MaxStartDateTime;
            var statuses = new[]
                               {
                                   "open", "reopened",
                                   "Describe Requirement", "Describing Requirement", "Design Architecture",
                                   "Designing Architecture", "Implement", "Build & Configure", "Implementing",
                                   "Building & Configuring", "In Progress", "Review", "System Test", "Test",
                                   "Ready for Test", "Deployed To Test", "Testing", "System Testing", "System Test Done",
                                   "System Integration Test", "System Integration Testing", "Acceptance Test",
                                   "Acceptance Testing", "Release", "Resolved", "Closed"
                               };

            this.precycleListBox.Items.AddRange(statuses.Take(6).ToArray());
            this.precycleListBox.SelectedIndex = 0;

            cycleListBox.Items.AddRange(statuses.Skip(6).Take(12).ToArray());
            cycleListBox.SelectedIndex = 0;

            postcycleListBox.Items.AddRange(statuses.Skip(18).ToArray());
            postcycleListBox.SelectedIndex = 0;
        }

        private void GenerateReport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(jqlTextBox.Text))
            {
                MessageBox.Show("No jql?", "Input problems");
                return;
            }

            int chunkSize;
            if (!int.TryParse(chunkSizeInDaysTextbox.Text, out chunkSize))
            {
                MessageBox.Show("Chunk size not an integer?", "Input problems");
                return;
            }

            GenerateReport(chunkSize);
        }

        private void GenerateReport(int chunkSizeInDays)
        {
            var jiraClient = new ChunkedJiraRestClient(chunkSizeInDays);
            var controller = new JiraMetricsFacade(jiraClient);

            try
            {
                var statuses = Status.Create(this.cycleListBox.Items.Cast<string>().ToArray());
                var preCycleStatuses = Status.Create(this.precycleListBox.Items.Cast<string>().ToArray());

                var cycleTimeRule = new CycleTimeRule(
                    statuses,
                    preCycleStatuses,
                    "started");

                controller.GenerateCycleTimeReport(
                    this.jqlTextBox.Text,
                    cycleTimeRule,
                    this.SummonFilters(),
                    chunkSizeInDays,
                    new NotepadProxy());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Failure");
            }

            if (this.saveQueryCheckBox.Checked)
            {
                this.jqlQueryManager.SaveQuery(this.jqlTextBox.Text);
            }

            var oldJql = this.jqlTextBox.Text;
            this.queryHistoryCombobox.DataSource = this.jqlQueryManager.JqlQueries;
            this.jqlTextBox.Text = oldJql;
        }

        private List<IIssueFilter> SummonFilters()
        {
            var startDateIntervalFilter = new StartDateFilter(
                minStartDateTimePicker.Value.Date, 
                maxStartDateTimePicker.Value.Date);

            var filters = new List<IIssueFilter> { startDateIntervalFilter };

            if (setIntervalForDoneDateCheckbox.Checked)
            {
                var doneDateIntervalFilter = new DoneDateFilter(
                    minDoneDateTimePicker.Value.Date, 
                    maxDoneDateTimePicker.Value.Date);

                filters.Add(doneDateIntervalFilter);
            }

            if (excludeIssuesThatAreNotDoneCheckBox.Checked)
            {
                filters.Add(new WorkDoneFilter());
            }

            return filters;
        }

        private void QueryHistoryComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            jqlTextBox.Text = queryHistoryCombobox.SelectedItem.ToString();
        }

        private void ClearHistoryButtonClick(object sender, EventArgs e)
        {
            this.jqlQueryManager.JqlQueries = new List<string>();
            queryHistoryCombobox.DataSource = this.jqlQueryManager.JqlQueries;
        }

        private void SetDoneDateIntervalCheckedChanged(object sender, EventArgs e)
        {
            setIntervalForDoneDateGroupBox.Visible = setIntervalForDoneDateCheckbox.Checked;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void MoveStatus(ListBox fromListBox, ListBox toListBox)
        {
            var indexBeforeMove = fromListBox.SelectedIndex;
            var itemToMove = fromListBox.SelectedItem;
            fromListBox.Items.Remove(itemToMove);
            toListBox.Items.Add(itemToMove);

            if (fromListBox.Items.Count > indexBeforeMove)
            {
                fromListBox.SelectedIndex = indexBeforeMove;
            }
            else if (fromListBox.Items.Count > 0)
            {
                fromListBox.SelectedIndex = 0;
            }
        }

        private void precycle2cycleButton_Click(object sender, EventArgs e)
        {
            this.MoveStatus(this.precycleListBox, this.cycleListBox);
        }

        private void cycle2precycleButton_Click(object sender, EventArgs e)
        {
            this.MoveStatus(this.cycleListBox, this.precycleListBox);
        }

        private void cycle2postcycleButton_Click(object sender, EventArgs e)
        {
            this.MoveStatus(this.cycleListBox, this.postcycleListBox);
        }

        private void postcycle2cycleButton_Click(object sender, EventArgs e)
        {
            this.MoveStatus(this.postcycleListBox, this.cycleListBox);
        }
    }
}
