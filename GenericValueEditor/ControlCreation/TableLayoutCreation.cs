using System.Windows.Forms;

namespace GenericValueEditor.ControlCreation
{
    internal static class TableLayoutCreation
    {

        public static TableLayoutPanel CreatePropertyTableLayout(int columnCount)
        {
            var tableLayout = new TableLayoutPanel()
            {
                RowCount = 1,
                ColumnCount = columnCount,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            tableLayout.SuspendLayout();
            SetColumnStyles(tableLayout);
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
    }
}
