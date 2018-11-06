using System.Collections.Generic;
using System.Windows.Forms;
using GenericValueEditor.Utils;

namespace GenericValueEditor.ControlCreation
{
    internal static class TextBoxCreation
    {
        public static TextBox AddTextBox(TableLayoutPanel tableLayout, ValueEnums.ValueType type, int row, int col,
            string name, Dictionary<string, EditorValue> valueByName)
        {
            var textBox = new TextBox()
            {
                Text = valueByName[name].ToString(),
                Anchor = AnchorStyles.Right | AnchorStyles.Left,
                Width = 150
            };

            tableLayout.Controls.Add(textBox, col, row);

            CreateTextChangedEvent(type, name, textBox, valueByName);

            return textBox;
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
    }
}
