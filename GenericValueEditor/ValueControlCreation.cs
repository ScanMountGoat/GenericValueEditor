using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GenericValueEditor.Utils;

namespace GenericValueEditor
{
    static class ValueControlCreation
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
                    AddLabel(tableLayout, 0, 0, name);
                    AddTextBox(tableLayout, type, 0, 1, name, valueByName);
                    break;
                case ValueEnums.ValueType.Float:
                    AddLabel(tableLayout, 0, 0, name);
                    var floatTextBox = AddTextBox(tableLayout, type, 0, 1, name, valueByName);
                    var floatTrackBar = AddTrackBar(tableLayout, type, 0, 2, name, valueByName);

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
                    AddLabel(tableLayout, 0, 0, name);
                    var intTextBox = AddTextBox(tableLayout, type, 0, 1, name, valueByName);
                    var intTrackBar = AddTrackBar(tableLayout, type, 0, 2, name, valueByName);

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
                    AddCheckBox(tableLayout, 0, 0, name, valueByName);
                    break;
                case ValueEnums.ValueType.Enum:
                    AddLabel(tableLayout, 0, 0, name);
                    AddComboBox(tableLayout, 0, 1, name, valueByName);
                    break;
                case ValueEnums.ValueType.String:
                    AddLabel(tableLayout, 0, 0, name);
                    AddTextBox(tableLayout, type, 0, 1, name, valueByName);
                    break;
                default:
                    break;
            }
        }

        private static void AddLabel(TableLayoutPanel tableLayout, int row, int col, string text)
        {
            // Setting auto size is necessary for anchoring.
            Label label = new Label()
            {
                Text = text,
                AutoSize = true,
                Anchor = AnchorStyles.Right
            };

            tableLayout.Controls.Add(label, col, row);
        }

        private static TextBox AddTextBox(TableLayoutPanel tableLayout, ValueEnums.ValueType type, int row, int col, 
            string name, Dictionary<string, EditorValue> valueByName)
        {
            TextBox control = new TextBox()
            {
                Text = valueByName[name].ToString(),
                Anchor = AnchorStyles.Right | AnchorStyles.Left
            };

            tableLayout.Controls.Add(control, col, row);

            CreateTextChangedEvent(type, name, control, valueByName);

            return control;
        }

        private static ComboBox AddComboBox(TableLayoutPanel tableLayout, int row, int col, 
            string name, Dictionary<string, EditorValue> valueByName)
        {
            var control = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Anchor = AnchorStyles.Right | AnchorStyles.Left
            };

            // TODO: Throw exception if it isn't an enum.
            Type enumType = valueByName[name].Value.GetType();

            InitializeComboBoxItems(valueByName, name, enumType, control);
            CreateComboBoxChangedEvent(name, enumType, valueByName, control);

            tableLayout.Controls.Add(control, col, row);

            return control;
        }

        private static void CreateComboBoxChangedEvent(string name, Type enumType, Dictionary<string, EditorValue> valueByName, ComboBox control)
        {
            control.SelectedIndexChanged += (sender, args) =>
            {
                object value = Enum.Parse(enumType, control.SelectedItem.ToString());
                valueByName[name].Value = value;
            };
        }

        private static void InitializeComboBoxItems(Dictionary<string, EditorValue> valueByName, string name, Type enumType, ComboBox control)
        {
            control.Items.Clear();
            foreach (var enumName in Enum.GetNames(enumType))
            {
                control.Items.Add(enumName.ToString());
            }

            control.SelectedItem = valueByName[name].Value.ToString();
        }

        private static void CreateTextChangedEvent(ValueEnums.ValueType type, string name, TextBox control, 
            Dictionary<string, EditorValue> valueByName)
        {
            switch (type)
            {
                case ValueEnums.ValueType.UintFlag:
                    control.TextChanged += (sender, args) =>
                    {
                        valueByName[name].Value = TextBoxUtils.TryParseUint((TextBox)sender, true);
                    };
                    break;
                case ValueEnums.ValueType.Float:
                    control.TextChanged += (sender, args) =>
                    {
                        valueByName[name].Value = TextBoxUtils.TryParseFloat((TextBox)sender);
                    };
                    break;
                case ValueEnums.ValueType.Int:
                    control.TextChanged += (sender, args) =>
                    {
                        valueByName[name].Value = TextBoxUtils.TryParseInt((TextBox)sender);
                    };
                    break;
                case ValueEnums.ValueType.String:
                    control.TextChanged += (sender, args) =>
                    {
                        valueByName[name].Value = ((TextBox)sender).Text;
                    };
                    break;
                default:
                    break;
            }
        }

        private static void CreateScrollEvent(ValueEnums.ValueType type, string name, TrackBar control, Dictionary<string, EditorValue> valueByName)
        {
            switch (type)
            {
                case ValueEnums.ValueType.Float:
                    control.Scroll += (sender, args) =>
                    {
                        valueByName[name].EnableTrackBarUpdates = false;
                        float newValue = TrackBarUtils.GetFloat(control, 0, 1);
                        valueByName[name].Value = newValue;
                    };
                    break;
                case ValueEnums.ValueType.Int:
                    control.Scroll += (sender, args) =>
                    {
                        valueByName[name].EnableTrackBarUpdates = false;
                        int newIntValue = TrackBarUtils.GetInt(control, -128, 128);
                        valueByName[name].Value = newIntValue;
                    };
                    break;
                default:
                    break;
            }

            control.Leave += (sender, args) => { valueByName[name].EnableTrackBarUpdates = true; };
        }

        private static CheckBox AddCheckBox(TableLayoutPanel tableLayout, int row, int col, 
            string name, Dictionary<string, EditorValue> valueByName)
        {
            // Setting auto size is necessary for anchoring.
            CheckBox control = new CheckBox()
            {
                Checked = true,
                Text = name,
                AutoSize = true,
                Anchor = AnchorStyles.Right
            };

            tableLayout.Controls.Add(control, col, row);

            control.CheckedChanged += (sender, args) =>
            {
                valueByName[name].Value = ((CheckBox)sender).Checked;
            };

            return control;
        }

        private static TrackBar AddTrackBar(TableLayoutPanel tableLayout, ValueEnums.ValueType type, int row, int col, 
            string name, Dictionary<string, EditorValue> valueByName)
        {
            TrackBar trackBar = new TrackBar()
            {
                TickStyle = TickStyle.None,
                Anchor = AnchorStyles.Right | AnchorStyles.Left,
                Maximum = 100,
            };

            tableLayout.Controls.Add(trackBar, col, row);

            CreateScrollEvent(type, name, trackBar, valueByName);
            return trackBar;
        }

        private static TableLayoutPanel CreatePropertyTableLayout(int columnCount)
        {
            TableLayoutPanel tableLayout = new TableLayoutPanel()
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
