using GenericValueEditor.Utils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GenericValueEditor.ControlCreation
{
    internal static class ValueControlCreation
    {
        public static Control AddPropertyControls(string name, ValueEnums.ValueType type, Control parent, 
            Dictionary<string, EditorValue> valueByName)
        {
            var mainPanel = new Panel();

            var tableLayout = CreatePropertyTableLayout(3);
            tableLayout.Dock = DockStyle.Fill;

            AddPropertyControlsToTableLayout(name, type, tableLayout, valueByName);

            mainPanel.Controls.Add(tableLayout);

            // Resize the table layout to fill the form.
            mainPanel.Width = parent.Width;

            // Make sure the rows are densely packed in the parent layout.
            mainPanel.Height = 30;
            parent.Controls.Add(mainPanel);

            return mainPanel;
        }

        private static void AddPropertyControlsToTableLayout(string name, ValueEnums.ValueType type, 
            TableLayoutPanel tableLayout, Dictionary<string, EditorValue> valueByName)
        {
            switch (type)
            {
                case ValueEnums.ValueType.UintFlag:
                    LabelCreation.AddLabel(tableLayout, 0, 0, name);
                    TextBoxCreation.AddTextBox(tableLayout, type, 0, 1, name, valueByName);
                    break;
                case ValueEnums.ValueType.Float:
                    LabelCreation.AddLabel(tableLayout, 0, 0, name);
                    var floatTextBox = TextBoxCreation.AddTextBox(tableLayout, type, 0, 1, name, valueByName);
                    if (valueByName[name].TrackBarInfo != null)
                        SetUpFloatTrackBar(name, type, tableLayout, valueByName, floatTextBox);
                    break;
                case ValueEnums.ValueType.Double:
                    LabelCreation.AddLabel(tableLayout, 0, 0, name);
                    var doubleTextBox = TextBoxCreation.AddTextBox(tableLayout, type, 0, 1, name, valueByName);
                    if (valueByName[name].TrackBarInfo != null)
                        SetUpDoubleTrackBar(name, type, tableLayout, valueByName, doubleTextBox);
                    break;
                case ValueEnums.ValueType.Int:
                    LabelCreation.AddLabel(tableLayout, 0, 0, name);
                    var intTextBox = TextBoxCreation.AddTextBox(tableLayout, type, 0, 1, name, valueByName);
                    if (valueByName[name].TrackBarInfo != null)
                        SetUpIntTrackBar(name, type, tableLayout, valueByName, intTextBox);
                    break;
                case ValueEnums.ValueType.Bool:
                    CheckBoxCreation.AddCheckBox(tableLayout, 0, 0, name, valueByName);
                    break;
                case ValueEnums.ValueType.Enum:
                    LabelCreation.AddLabel(tableLayout, 0, 0, name);
                    ComboBoxCreation.AddComboBox(tableLayout, 0, 1, name, valueByName);
                    break;
                case ValueEnums.ValueType.String:
                    LabelCreation.AddLabel(tableLayout, 0, 0, name);
                    TextBoxCreation.AddTextBox(tableLayout, type, 0, 1, name, valueByName);
                    break;
                default:
                    break;
            }
        }

        private static void SetUpFloatTrackBar(string name, ValueEnums.ValueType type, TableLayoutPanel tableLayout, Dictionary<string, EditorValue> valueByName, TextBox floatTextBox)
        {
            var floatTrackBar = TrackBarCreation.AddTrackBar(tableLayout, type, 0, 2, name, valueByName);
            float min = (float)valueByName[name].TrackBarInfo.Min;
            float max = (float)valueByName[name].TrackBarInfo.Max;

            // Set track bar to update text box.
            floatTrackBar.Scroll += (sender, args) =>
            {
                floatTextBox.Text = TrackBarUtils.GetFloat(floatTrackBar, min, max).ToString();
            };

            // Set text box to update track bar.
            floatTextBox.TextChanged += (sender, args) =>
            {
                if (valueByName[name].EnableTrackBarUpdates)
                    TrackBarUtils.SetFloat((float)valueByName[name].Value, floatTrackBar, min, max);
            };
        }

        private static void SetUpDoubleTrackBar(string name, ValueEnums.ValueType type, TableLayoutPanel tableLayout, Dictionary<string, EditorValue> valueByName, TextBox doubleTextBox)
        {
            var doubleTrackBar = TrackBarCreation.AddTrackBar(tableLayout, type, 0, 2, name, valueByName);
            double min = valueByName[name].TrackBarInfo.Min;
            double max = valueByName[name].TrackBarInfo.Max;

            // Set track bar to update text box.
            doubleTrackBar.Scroll += (sender, args) =>
            {
                doubleTextBox.Text = TrackBarUtils.GetDouble(doubleTrackBar, min, max).ToString();
            };

            // Set text box to update track bar.
            doubleTextBox.TextChanged += (sender, args) =>
            {
                if (valueByName[name].EnableTrackBarUpdates)
                    TrackBarUtils.SetDouble((double)valueByName[name].Value, doubleTrackBar, min, max);
            };
        }

        private static void SetUpIntTrackBar(string name, ValueEnums.ValueType type, TableLayoutPanel tableLayout, Dictionary<string, EditorValue> valueByName, TextBox intTextBox)
        {
            var intTrackBar = TrackBarCreation.AddTrackBar(tableLayout, type, 0, 2, name, valueByName);
            int min = (int)valueByName[name].TrackBarInfo.Min;
            int max = (int)valueByName[name].TrackBarInfo.Max;
            // Set track bar to update text box.
            intTrackBar.Scroll += (sender, args) =>
            {
                intTextBox.Text = TrackBarUtils.GetInt(intTrackBar, min, max).ToString();
            };

            // Set text box to update track bar.
            intTextBox.TextChanged += (sender, args) =>
            {
                if (valueByName[name].EnableTrackBarUpdates)
                    TrackBarUtils.SetInt((int)valueByName[name].Value, intTrackBar, min, max);
            };
        }

        private static TableLayoutPanel CreatePropertyTableLayout(int columnCount)
        {
            var tableLayout = new TableLayoutPanel()
            {
                RowCount = 1,
                ColumnCount = columnCount,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            tableLayout.SuspendLayout();
            SetColumnStyles(tableLayout);
            //SetRowStyles(tableLayout);
            tableLayout.ResumeLayout();

            return tableLayout;
        }

        private static void SetColumnStyles(TableLayoutPanel tableLayout)
        {
            tableLayout.ColumnStyles.Clear();

            // Make the labels and text boxes small.
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));

            for (int i = 2; i < tableLayout.ColumnCount; i++)
            {
                var style = new ColumnStyle(SizeType.Percent, 100 / tableLayout.ColumnCount);
                tableLayout.ColumnStyles.Add(style);
            }
        }

        private static void SetRowStyles(TableLayoutPanel tableLayout)
        {
            tableLayout.RowStyles.Clear();

            for (int i = 0; i < tableLayout.RowCount; i++)
            {
                var style = new RowStyle(SizeType.AutoSize);
                tableLayout.RowStyles.Add(style);
            }
        }
    }
}
