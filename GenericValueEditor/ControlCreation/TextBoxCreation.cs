using System.Collections.Generic;
using System.Windows.Forms;
using GenericValueEditor.Utils;
using System;

namespace GenericValueEditor.ControlCreation
{
    internal static class TextBoxCreation
    {
        // Use a dictionary to avoid a large switch statement and simplify arguments.
        private static readonly Dictionary<ValueEnums.ValueType, Func<TextBox, object>> parseFunctionByType = new Dictionary<ValueEnums.ValueType, Func<TextBox, object>>()
        {
            { ValueEnums.ValueType.Float,    textBox => TextBoxUtils.TryParseFloat(textBox) },
            { ValueEnums.ValueType.Double,   textBox => TextBoxUtils.TryParseDouble(textBox) },
            { ValueEnums.ValueType.Int,      textBox => TextBoxUtils.TryParseInt(textBox) },
            { ValueEnums.ValueType.UintFlag, textBox => TextBoxUtils.TryParseUint(textBox, true) },
            { ValueEnums.ValueType.String,   textBox => textBox.Text }
        };

        public static TextBox AddTextBox(TableLayoutPanel tableLayout, ValueEnums.ValueType type, int row, int col,
            string name, Dictionary<string, EditorValue> valueByName)
        {
            var textBox = new TextBox()
            {
                Text = valueByName[name].ToString(),
                Dock = DockStyle.Fill,
                //Anchor = AnchorStyles.Right | AnchorStyles.Left,
            };

            tableLayout.Controls.Add(textBox, col, row);

            CreateTextChangedEvent(type, name, textBox, valueByName);

            return textBox;
        }

        private static void CreateTextChangedEvent(ValueEnums.ValueType type, string name, TextBox control,
            Dictionary<string, EditorValue> valueByName)
        {
            if (!parseFunctionByType.ContainsKey(type))
                throw new NotSupportedException($"Conversion of text to type {type.ToString()} is not supported.");

            control.TextChanged += (sender, args) =>
            {
                valueByName[name].Value = parseFunctionByType[type](control);
            };
        }
    }
}
