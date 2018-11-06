using GenericValueEditor.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericValueEditor.Test.TextBoxUtilsTests
{
    [TestClass]
    public class TryParseFloat
    {
        [TestMethod]
        public void ValidFloat()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "-0.5" };
            Assert.AreEqual(-0.5f, TextBoxUtils.TryParseFloat(textBox));
        }

        [TestMethod]
        public void InvalidFloat()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "0xFF" };
            Assert.AreEqual(0, TextBoxUtils.TryParseFloat(textBox));
            Assert.AreEqual(System.Drawing.Color.Red, textBox.BackColor);
        }
    }
}
