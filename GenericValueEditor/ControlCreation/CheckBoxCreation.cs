using System.Collections.Generic;
using System.Windows.Forms;

namespace GenericValueEditor.ControlCreation
{
    internal static class CheckBoxCreation
    {
        public static CheckBox AddCheckBox(TableLayoutPanel tableLayout, int row, int col,
            string name, Dictionary<string, EditorValue> valueByName)
        {
            // Setting auto size is necessary for anchoring.
            CheckBox control = new CheckBox()
            {
                Checked = true,
                Text = name,
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };

            tableLayout.Controls.Add(control, col, row);

            // Fill all columns, so the text isn't cut off.
            tableLayout.SetColumnSpan(control, tableLayout.ColumnCount);

            control.Checked = (bool)valueByName[name].Value;

            control.CheckedChanged += (sender, args) =>
            {
                valueByName[name].Value = ((CheckBox)sender).Checked;
            };

            return control;
        }
    }
}
