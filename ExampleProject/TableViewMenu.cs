﻿using System;
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
        private List<SampleClass> objectsToEdit = new List<SampleClass>()
        {
            new SampleClass(),
            new SampleClass(),
            new SampleClass()
        };

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
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;

            // Add columns for each editable field.
            dataTable.Columns.Add("col1", typeof(float));
            dataTable.Columns.Add("col2", typeof(float));

            // These can be added in any order.
            AddEnumComboBoxColumn(typeof(TestEnum));

            int rowIndex = 0;
            foreach (var item in objectsToEdit)
            {
                dataTable.Rows.Add(item.FloatValue1, item.FloatValue2);
                // TODO: Add combo box enum values.
                rowIndex++;
            }
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"[{e.ColumnIndex},  {e.RowIndex}] = {dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()}");            
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Invalid values will be replaced with the old value when leaving the cell.
            e.Cancel = false;
        }

        private void AddEnumComboBoxColumn(Type enumType)
        {
            // Only allow enums to use combo boxes.
            if (!enumType.IsEnum)
                throw new ArgumentException($"{enumType.ToString()} is not an enum.");

            var comboBoxColumn = new DataGridViewComboBoxColumn();
            foreach (var enumName in Enum.GetNames(enumType))
            {
                comboBoxColumn.Items.Add(enumName.ToString());
            }

            dataGridView1.Columns.Add(comboBoxColumn);
        }
    }
}
