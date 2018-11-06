using GenericValueEditor.Utils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GenericValueEditor.ControlCreation
{
    internal static class ValueControlCreation
    {
        public static void AddPropertyControls(string name, ValueEnums.ValueType type, Control parent, 
            Dictionary<string, EditorValue> valueByName)
        {
            Panel panel = new Panel();

            TableLayoutPanel tableLayout = CreatePropertyTableLayout(3);
            tableLayout.ColumnStyles[0].Width = 25;
            tableLayout.ColumnStyles[1].Width = 75;

            AddPropertyControlsToTableLayout(name, type, tableLayout, valueByName);

            panel.Controls.Add(tableLayout);

            // Resize the table layout to fill the form.
            panel.Width = parent.Width;
            parent.Controls.Add(panel);
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
                    var floatTrackBar = TrackBarCreation.AddTrackBar(tableLayout, type, 0, 2, name, valueByName);

                    // Set track bar to update text box.
                    floatTrackBar.Scroll += (sender, args) =>
                    {
                        floatTextBox.Text = TrackBarUtils.GetFloat(floatTrackBar, 0, 1).ToString();
                    };

                    // Set text box to update track bar.
                    floatTextBox.TextChanged += (sender, args) =>
                    {
                        if (valueByName[name].EnableTrackBarUpdates)
                            TrackBarUtils.SetFloat((float)valueByName[name].Value, floatTrackBar, 0, 1);
                    };
                    break;
                case ValueEnums.ValueType.Int:
                    LabelCreation.AddLabel(tableLayout, 0, 0, name);
                    var intTextBox = TextBoxCreation.AddTextBox(tableLayout, type, 0, 1, name, valueByName);
                    var intTrackBar = TrackBarCreation.AddTrackBar(tableLayout, type, 0, 2, name, valueByName);

                    // Set track bar to update text box.
                    intTrackBar.Scroll += (sender, args) =>
                    {
                        intTextBox.Text = TrackBarUtils.GetInt(intTrackBar, -128, 128).ToString();
                    };

                    // Set text box to update track bar.
                    intTextBox.TextChanged += (sender, args) =>
                    {
                        if (valueByName[name].EnableTrackBarUpdates)
                            TrackBarUtils.SetInt((int)valueByName[name].Value, intTrackBar, -128, 128);
                    };
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

        private static TableLayoutPanel CreatePropertyTableLayout(int columnCount)
        {
            var tableLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                RowCount = 1,
                ColumnCount = columnCount
            };
 
            tableLayout.SuspendLayout();

            // TODO: This scaling only sort of works.
            tableLayout.ColumnStyles.Clear();
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            for (int i = 1; i < tableLayout.ColumnCount; i++)
            {
                ColumnStyle style = new ColumnStyle(SizeType.Percent, 100 / tableLayout.ColumnCount);
                tableLayout.ColumnStyles.Add(style);
            }

            tableLayout.ResumeLayout();
            return tableLayout;
        }

    }
}
