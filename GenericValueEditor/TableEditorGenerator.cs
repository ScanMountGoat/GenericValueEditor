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
        private class EditableObject
        {
            public Dictionary<string, EditorValue> ValueByName { get; } = new Dictionary<string, EditorValue>();
        }

        private readonly List<EditableObject> objectsToEdit = new List<EditableObject>();

        /// <summary>
        /// Initializes the rows and columns for the <see cref="DataGridView"/>.
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="objectsToEdit"></param>
        public void InitializeDataGridView(DataGridView dataGridView, List<T> objectsToEdit)
        {
            // TODO: Return a new grid view rather than modifying an existing one.
            var dataTable = CreateDataTable(dataGridView);

            this.objectsToEdit.Clear();
            for (int i = 0; i < objectsToEdit.Count; i++)
            {
                this.objectsToEdit.Add(new EditableObject());
                Utils.ValueEditingUtils.UpdateEditorValues(objectsToEdit[i], this.objectsToEdit[i].ValueByName);
            }

            CreateMemberColumns(dataTable, dataGridView);
            InitializeTableValues(dataTable);
        }

        private void InitializeTableValues(DataTable dataTable)
        {
            foreach (var editableObject in objectsToEdit)
            {
                var values = GetPropertyValues(dataTable, editableObject);
                dataTable.Rows.Add(values.ToArray());
            }
        }

        private static List<object> GetPropertyValues(DataTable dataTable, EditableObject editableObject)
        {
            var values = new List<object>();

            // The column names should be the same as the dictionary keys.
            foreach (DataColumn col in dataTable.Columns)
            {
                var editorValue = editableObject.ValueByName[col.ColumnName];
                values.Add(editorValue.Value);
            }

            return values;
        }

        private void CreateMemberColumns(DataTable dataTable, DataGridView dataGridView)
        {
            // TODO: Can columns be generated from an empty list?
            foreach (var pair in objectsToEdit[0].ValueByName)
            {
                var type = pair.Value.Value.GetType();

                if (type.IsEnum)
                    AddEnumComboBoxColumn(pair.Key, type, dataGridView);
                else
                    dataTable.Columns.Add(new DataColumn(pair.Key, type));
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
                var type = objectsToEdit[args.RowIndex].ValueByName[cell.OwningColumn.Name].Value.GetType();

                if (type.IsEnum)
                    objectsToEdit[args.RowIndex].ValueByName[cell.OwningColumn.Name].Value = Enum.Parse(type, cell.Value.ToString());
                else
                    objectsToEdit[args.RowIndex].ValueByName[cell.OwningColumn.Name].Value = cell.Value; 
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
