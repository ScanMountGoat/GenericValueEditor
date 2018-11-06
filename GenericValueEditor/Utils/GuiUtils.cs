using System.Windows.Forms;

namespace GenericValueEditor.Utils
{
    /// <summary>
    /// Contains utility methods for working with <see cref="Control"/> objects.
    /// </summary>
    public static class GuiUtils
    {
        /// <summary>
        /// Scales all child controls to match the parent width, accounting for horizontal margins.
        /// </summary>
        /// <param name="containerControl">The parent control</param>
        public static void ScaleControlsHorizontallyToLayoutWidth(Control containerControl)
        {
            foreach (Control control in containerControl.Controls)
            {
                control.Width = containerControl.Width - containerControl.Margin.Horizontal;
            }
        }
    }
}
