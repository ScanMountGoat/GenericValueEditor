using System.Windows.Forms;

namespace GenericValueEditor.Utils
{
    /// <summary>
    /// Contains utility methods for working with <see cref="Control"/> objects.
    /// </summary>
    public static class GuiUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerControl"></param>
        public static void ScaleControlsHorizontallyToLayoutWidth(Control containerControl)
        {
            foreach (Control control in containerControl.Controls)
            {
                control.Width = containerControl.Width - containerControl.Margin.Horizontal;
            }
        }
    }
}
