using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GenericValueEditor.ControlCreation
{
    internal static class ComboBoxCreation
    {
        public static ComboBox AddComboBox(TableLayoutPanel tableLayout, int row, int col,
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
    }
}
