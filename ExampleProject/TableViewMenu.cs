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

            // TODO: Data error event?
            var dataTable = new DataTable();
            dataGridView1.DataSource = dataTable;

            // Add columns for each editable field.
            dataTable.Columns.Add("col1", typeof(string));
            dataTable.Columns.Add("col2", typeof(int));

            // These can be added in any order.
            AddEnumComboBoxColumn(typeof(TestEnum));

            dataTable.Columns.Add("col3", typeof(int));
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
