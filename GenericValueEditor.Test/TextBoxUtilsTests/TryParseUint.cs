using GenericValueEditor.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericValueEditor.Test.TextBoxUtilsTests
{
    [TestClass]
    public class TryParseUint
    {
        [TestMethod]
        public void ValidUint()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "255" };
            Assert.AreEqual(0xFFu, TextBoxUtils.TryParseUint(textBox));
        }

        [TestMethod]
        public void ValidUintHex()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "FF" };
            Assert.AreEqual(0xFFu, TextBoxUtils.TryParseUint(textBox, true));
        }

        [TestMethod]
        public void ValidUintHexPrefix()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "0xFF" };
            Assert.AreEqual(0xFFu, TextBoxUtils.TryParseUint(textBox, true));
        }

        [TestMethod]
        public void ValidUintHexPrefixAndSuffix()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "0xFFu" };
            Assert.AreEqual(0xFFu, TextBoxUtils.TryParseUint(textBox, true));
        }

        [TestMethod]
        public void InvalidUint()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "-1" };
            Assert.AreEqual(0x0u, TextBoxUtils.TryParseUint(textBox));
            Assert.AreEqual(System.Drawing.Color.Red, textBox.BackColor);
        }

        [TestMethod]
        public void InvalidUintHex()
        {
            var textBox = new System.Windows.Forms.TextBox() { Text = "0xZZ" };
            Assert.AreEqual(0x0u, TextBoxUtils.TryParseUint(textBox, true));
            Assert.AreEqual(System.Drawing.Color.Red, textBox.BackColor);
        }
    }
}
