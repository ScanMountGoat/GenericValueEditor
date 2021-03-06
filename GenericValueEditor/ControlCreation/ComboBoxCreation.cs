﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GenericValueEditor.ControlCreation
{
    internal static class ComboBoxCreation
    {
        public static ComboBox AddComboBox(TableLayoutPanel tableLayout, int row, int col,
            string name, Dictionary<string, EditorValue> valueByName)
        {
            var control = CreateComboBox(tableLayout, row, col, name, valueByName);

            // Fill all remaining columns.
            tableLayout.SetColumnSpan(control, 2);

            return control;
        }

        private static ComboBox CreateComboBox(TableLayoutPanel tableLayout, int row, int col, string name, Dictionary<string, EditorValue> valueByName)
        {
            var control = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Anchor = AnchorStyles.Right | AnchorStyles.Left,
                Width = 150
            };

            // Throw a more meaningful error before combo box setup fails.
            var enumType = valueByName[name].Value.GetType();
            if (!enumType.IsEnum)
                throw new ArgumentException($"The Enum editor type is incompatible with member type {enumType.ToString()}.");

            InitializeComboBoxItems(valueByName, name, enumType, control);
            CreateComboBoxChangedEvent(name, enumType, valueByName, control);

            tableLayout.Controls.Add(control, col, row);
            return control;
        }

        private static void CreateComboBoxChangedEvent(string name, Type enumType, Dictionary<string, EditorValue> valueByName, ComboBox control)
        {
            control.SelectedIndexChanged += (sender, args) =>
            {
                valueByName[name].Value = Enum.Parse(enumType, control.SelectedItem.ToString());
            };
        }

        private static void InitializeComboBoxItems(Dictionary<string, EditorValue> valueByName, string name, Type enumType, ComboBox control)
        {
            control.Items.Clear();
            control.Items.AddRange(Enum.GetNames(enumType));

            control.SelectedItem = valueByName[name].Value.ToString();
        }
    }
}
