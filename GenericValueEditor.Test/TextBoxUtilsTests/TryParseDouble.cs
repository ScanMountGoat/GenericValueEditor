using GenericValueEditor.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericValueEditor.Test.TextBoxUtilsTests
{
    [TestClass]
    public class TryParseDouble
    {
        [TestMethod]
        public void ValidDouble()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "-0.5" };
            Assert.AreEqual(-0.5, TextBoxUtils.TryParseDouble(textBox));
        }

        [TestMethod]
        public void InvalidDouble()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "0xFF" };
            Assert.AreEqual(0, TextBoxUtils.TryParseDouble(textBox));
            Assert.AreEqual(System.Drawing.Color.Red, textBox.BackColor);
        }
    }
}
