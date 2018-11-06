using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace GenericValueEditor.Utils
{
    /// <summary>
    /// Contains methods for converting the text of a <see cref="TextBox"/> to numeric types.
    /// </summary>
    public static class TextBoxUtils
    {
        /// <summary>
        /// Returns the value of <paramref name="textBox"/> or <c>0</c> on error.
        /// </summary>
        /// <param name="textBox">The text box to parse</param>
        /// <param name="changeTextBoxColor">Sets the back color of <paramref name="textBox"/> when <c>true</c></param>
        /// <returns>The value of <paramref name="textBox"/> or <c>0</c> on error</returns>
        public static float TryParseFloat(TextBox textBox, bool changeTextBoxColor = true)
        {
            float result = 0;
            if (float.TryParse(textBox.Text, out result))
                textBox.BackColor = Color.White;
            else if (changeTextBoxColor)
                textBox.BackColor = Color.Red;

            return result;
        }

        /// <summary>
        /// Returns the value of <paramref name="textBox"/> or <c>0</c> on error.
        /// </summary>
        /// <param name="textBox">The text box to parse</param>
        /// <param name="changeTextBoxColor">Sets the back color of <paramref name="textBox"/> when <c>true</c></param>
        /// <returns>The value of <paramref name="textBox"/> or <c>0</c> on error</returns>
        public static double TryParseDouble(TextBox textBox, bool changeTextBoxColor = true)
        {
            // Sets the text box back color to red for invalid values.
            double result = 0;
            if (double.TryParse(textBox.Text, out result))
                textBox.BackColor = Color.White;
            else if (changeTextBoxColor)
                textBox.BackColor = Color.Red;

            return result;
        }

        /// <summary>
        /// Returns the value of <paramref name="textBox"/> or <c>0</c> on error.
        /// </summary>
        /// <param name="textBox">The text box to parse</param>
        /// <param name="useHex">Parses text of the form <c>FFFFFFFF</c> when <c>true</c></param>
        /// <param name="changeTextBoxColor">Sets the back color of <paramref name="textBox"/> when <c>true</c></param>
        /// <returns>The value of <paramref name="textBox"/> or <c>0</c> on error</returns>
        public static int TryParseInt(TextBox textBox, bool useHex = false, bool changeTextBoxColor = true)
        {
            // Ignore the hex prefix.
            string text = textBox.Text;
            if (text.StartsWith("0x"))
                text = text.Replace("0x", "");

            int result = 0;
            if (useHex && int.TryParse(text, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out result))
            {
                textBox.BackColor = Color.White;
            }
            else if (int.TryParse(text, out result))
            {
                textBox.BackColor = Color.White;
            }
            else if (changeTextBoxColor)
            {
                textBox.BackColor = Color.Red;
            }

            return result;
        }

        /// <summary>
        /// Returns the value of <paramref name="textBox"/> or <c>0</c> on error.
        /// </summary>
        /// <param name="textBox">The text box to parse</param>
        /// <param name="useHex">Parses text of the form <c>FFFFFFFF</c> when <c>true</c></param>
        /// <param name="changeTextBoxColor">Sets the back color of <paramref name="textBox"/> when <c>true</c></param>
        /// <returns>The value of <paramref name="textBox"/> or <c>0</c> on error</returns>
        public static uint TryParseUint(TextBox textBox, bool useHex = false, bool changeTextBoxColor = true)
        {
            // Ignore the hex prefix or suffix.
            string text = textBox.Text;
            if (text.StartsWith("0x"))
                text = text.Replace("0x", "");
            if (text.EndsWith("u"))
                text = text.Replace("u", "");

            uint result = 0;
            if (useHex && uint.TryParse(text, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out result))
            {
                textBox.BackColor = Color.White;
            }
            else if (uint.TryParse(text, out result))
            {
                textBox.BackColor = Color.White;
            }
            else if (changeTextBoxColor)
            {
                textBox.BackColor = Color.Red;
            }

            return result;
        }
    }
}
