using GenericValueEditor.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericValueEditor.Test.TrackBarUtilsTest
{
    [TestClass]
    public class GetFloat
    {
        [TestMethod]
        public void MinValue0To1()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Minimum;
            Assert.AreEqual(0f, TrackBarUtils.GetFloat(trackBar, 0, 1));
        }

        [TestMethod]
        public void MidValue0To1()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Maximum / 2;
            Assert.AreEqual(0.5f, TrackBarUtils.GetFloat(trackBar, 0, 1));
        }

        [TestMethod]
        public void MaxValue0To1()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Maximum;
            Assert.AreEqual(1f, TrackBarUtils.GetFloat(trackBar, 0, 1));
        }

        [TestMethod]
        public void MinValueNeg5To5()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Minimum;
            Assert.AreEqual(-5f, TrackBarUtils.GetFloat(trackBar, -5, 5));
        }

        [TestMethod]
        public void MidValueNeg5To5()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Maximum / 2;
            Assert.AreEqual(0f, TrackBarUtils.GetFloat(trackBar, -5, 5));
        }

        [TestMethod]
        public void MaxValueNeg5To5()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Maximum;
            Assert.AreEqual(5f, TrackBarUtils.GetFloat(trackBar, -5, 5));
        }
    }
}
