using GenericValueEditor.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericValueEditor.Test.TextBoxUtilsTests
{
    [TestClass]
    public class TryParseInt
    {
        [TestMethod]
        public void ValidInt()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "-1" };
            Assert.AreEqual(-1, TextBoxUtils.TryParseInt(textBox));
        }

        [TestMethod]
        public void ValidIntHex()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "FF" };
            Assert.AreEqual(255, TextBoxUtils.TryParseInt(textBox, true));
        }

        [TestMethod]
        public void ValidIntHexPrefix()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "0xFF" };
            Assert.AreEqual(255, TextBoxUtils.TryParseInt(textBox, true));
        }

        [TestMethod]
        public void InvalidInt()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "0.5f" };
            Assert.AreEqual(0, TextBoxUtils.TryParseInt(textBox));
            Assert.AreEqual(System.Drawing.Color.Red, textBox.BackColor);
        }

        [TestMethod]
        public void InvalidIntHex()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "0xZZ" };
            Assert.AreEqual(0, TextBoxUtils.TryParseInt(textBox, true));
            Assert.AreEqual(System.Drawing.Color.Red, textBox.BackColor);
        }
    }
}
