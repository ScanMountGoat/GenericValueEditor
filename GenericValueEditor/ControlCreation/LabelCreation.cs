using System.Windows.Forms;

namespace GenericValueEditor.ControlCreation
{
    internal static class LabelCreation
    {
        public static Label AddLabel(TableLayoutPanel tableLayout, int row, int col, string text)
        {
            // Setting auto size is necessary for anchoring.
            var label = new Label()
            {
                Text = text,
                AutoSize = true,
                Anchor = AnchorStyles.Right,
            };

            tableLayout.Controls.Add(label, col, row);
            return label;
        }
    }
}
