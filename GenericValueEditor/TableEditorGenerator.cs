using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace GenericValueEditor
{
    /// <summary>
    /// Contains methods to create controls for editing an object's member variables.
    /// Each object occupies a unique row.
    /// </summary>
    /// <typeparam name="T">The reference type of the editable object. The member attributes 
    /// determine what editor controls are used.</typeparam>
    public class TableEditorGenerator<T>
    {
        // TODO: This is kind of gross.
        // Store a separate dictionary for each object.
        private List<Dictionary<string, EditorValue>> valueByNameCollection = new List<Dictionary<string, EditorValue>>();

        /// <summary>
        /// Initializes the rows and columns for the <see cref="DataGridView"/>.
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="objectsToEdit"></param>
        public void InitializeDataGridView(DataGridView dataGridView, List<T> objectsToEdit)
        {
            // TODO: Return a new grid view rather than modifying an existing one.
            var dataTable = CreateDataTable(dataGridView);

            valueByNameCollection.Clear();
            for (int i = 0; i < objectsToEdit.Count; i++)
            {
                valueByNameCollection.Add(new Dictionary<string, EditorValue>());
                Utils.ValueEditingUtils.UpdateEditorValues(objectsToEdit[i], valueByNameCollection[i]);
            }

            CreateMemberColumns(dataTable, dataGridView);
            InitializeTableValues(dataTable, objectsToEdit);
        }

        private void InitializeTableValues(DataTable dataTable, List<T> objectsToEdit)
        {
            foreach (var dict in valueByNameCollection)
            {
                // The column names should be the same as the dictionary keys.
                var values = new List<object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    var editorValue = dict[col.ColumnName];
                    values.Add(editorValue.Value);
                }
                dataTable.Rows.Add(values.ToArray());
            }
        }

        private void CreateMemberColumns(DataTable dataTable, DataGridView dataGridView)
        {
            // TODO: Empty list?
            foreach (var pair in valueByNameCollection[0])
            {
                var type = pair.Value.Value.GetType();

                if (type.IsEnum)
                    AddEnumComboBoxColumn(pair.Key, type, dataGridView);
                else
                    dataTable.Columns.Add(new DataColumn(pair.Key, type));

                //if (type.IsEnum)
                //{
                //    // Only use the combo box column to avoid invalid values.
                //    dataGridView.Columns[pair.Key].Visible = false;
                //    AddEnumComboBoxColumn(pair.Key, type, dataGridView);
                //}
            }
        }

        private DataTable CreateDataTable(DataGridView dataGridView)
        {
            var dataTable = new DataTable();

            dataGridView.DataSource = dataTable;

            SetUpDataErrorEvent(dataGridView);

            dataGridView.CellEndEdit += (sender, args) =>
            {
                // Update the value.
                var cell = dataGridView.CurrentCell;
                var type = valueByNameCollection[args.RowIndex][cell.OwningColumn.Name].Value.GetType();

                if (type.IsEnum)
                    valueByNameCollection[args.RowIndex][cell.OwningColumn.Name].Value = Enum.Parse(type, cell.Value.ToString());
                else
                    valueByNameCollection[args.RowIndex][cell.OwningColumn.Name].Value = cell.Value; 
            };

            return dataTable;
        }

        private static void SetUpDataErrorEvent(DataGridView dataGridView)
        {
            // Invalid values will be replaced with the old value when leaving the cell.
            dataGridView.DataError += (sender, args) => args.Cancel = false;
        }

        private static void AddEnumComboBoxColumn(string columnName, Type enumType, DataGridView dataGridView)
        {
            // Only allow enums to use combo boxes.
            if (!enumType.IsEnum)
                throw new ArgumentException($"{enumType.ToString()} is not an enum.");

            var comboBoxColumn = new DataGridViewComboBoxColumn()
            {
                Name = columnName,
                DataSource = Enum.GetNames(enumType),
                DataPropertyName = columnName
            };

            dataGridView.Columns.Add(comboBoxColumn);
        }
    }
}
