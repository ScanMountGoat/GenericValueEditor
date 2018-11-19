using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExampleProject
{
    public partial class TableViewMenu : Form
    {
        enum TestEnum
        {
            A,
            B,
            C
        }

        public TableViewMenu()
        {
            InitializeComponent();

            var dataTable = new DataTable();
            dataGridView1.DataSource = dataTable;
            dataGridView1.DataError += DataGridView1_DataError;

            // Add columns for each editable field.
            dataTable.Columns.Add("col1", typeof(string));
            dataTable.Columns.Add("col2", typeof(int));

            // These can be added in any order.
            AddEnumComboBoxColumn(typeof(TestEnum));

            dataTable.Columns.Add("col3", typeof(int));
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Invalid values will be replaced with the old value when leaving the cell.
            e.Cancel = false;
        }

        private void AddEnumComboBoxColumn(Type enumType)
        {
            // TODO: Check for enum and throw exception.
            var comboBoxColumn = new DataGridViewComboBoxColumn();
            foreach (var enumName in Enum.GetNames(enumType))
            {
                comboBoxColumn.Items.Add(enumName.ToString());
            }

            dataGridView1.Columns.Add(comboBoxColumn);
        }
    }
}
