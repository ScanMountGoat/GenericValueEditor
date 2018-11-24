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
                    values.Add(dict[col.ColumnName].Value);
                }
                dataTable.Rows.Add(values.ToArray());
            }
        }

        private void CreateMemberColumns(DataTable dataTable, DataGridView dataGridView)
        {
            // TODO: How to handle order?
            // TODO: Skip enum and use combo box.
            // TODO: Custom types?
            // TODO: Only use members with the proper attribute.
            // TODO: Empty list?
            foreach (var pair in valueByNameCollection[0])
            {
                dataTable.Columns.Add(new DataColumn(pair.Key, pair.Value.Value.GetType()));
            }
        }

        private static DataTable CreateDataTable(DataGridView dataGridView)
        {
            var dataTable = new DataTable();

            dataGridView.DataSource = dataTable;

            dataGridView.DataError += (sender, args) =>
            {
                // Invalid values will be replaced with the old value when leaving the cell.
                args.Cancel = false;
            };

            dataGridView.CellEndEdit += (sender, args) =>
            {
                System.Diagnostics.Debug.WriteLine($"[{args.ColumnIndex},  {args.RowIndex}] = {dataGridView[args.ColumnIndex, args.RowIndex].Value.ToString()}");
            };

            return dataTable;
        }

        private static void AddEnumComboBoxColumn(Type enumType, DataGridView dataGridView)
        {
            // Only allow enums to use combo boxes.
            if (!enumType.IsEnum)
                throw new ArgumentException($"{enumType.ToString()} is not an enum.");

            var comboBoxColumn = new DataGridViewComboBoxColumn();
            foreach (var enumName in Enum.GetNames(enumType))
            {
                comboBoxColumn.Items.Add(enumName.ToString());
            }

            dataGridView.Columns.Add(comboBoxColumn);
        }
    }
}
