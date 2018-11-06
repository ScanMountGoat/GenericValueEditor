using GenericValueEditor.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericValueEditor.Test.TrackBarUtilsTest
{
    [TestClass]
    public class SetFloat
    {
        [TestMethod]
        public void MinValue0To1()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            TrackBarUtils.SetFloat(0, trackBar, 0, 1);
            Assert.AreEqual(trackBar.Minimum, trackBar.Value);
        }

        [TestMethod]
        public void MidValue0To1()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            TrackBarUtils.SetFloat(0.5f, trackBar, 0, 1);
            Assert.AreEqual(trackBar.Maximum / 2, trackBar.Value);
        }

        [TestMethod]
        public void MaxValue0To1()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            TrackBarUtils.SetFloat(1, trackBar, 0, 1);
            Assert.AreEqual(trackBar.Maximum, trackBar.Value);
        }

        [TestMethod]
        public void MinValueNeg5To5()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            TrackBarUtils.SetFloat(-5, trackBar, -5, 5);
            Assert.AreEqual(trackBar.Minimum, trackBar.Value);
        }

        [TestMethod]
        public void MidValueNeg5To5()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            TrackBarUtils.SetFloat(0, trackBar, -5, 5);
            Assert.AreEqual(trackBar.Maximum / 2, trackBar.Value);
        }

        [TestMethod]
        public void MaxValueNeg5To5()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            TrackBarUtils.SetFloat(5, trackBar, -5, 5);
            Assert.AreEqual(trackBar.Maximum, trackBar.Value);
        }
    }
}
