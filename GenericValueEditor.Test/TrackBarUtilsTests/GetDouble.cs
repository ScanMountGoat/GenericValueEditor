using GenericValueEditor.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericValueEditor.Test.TrackBarUtilsTest
{
    [TestClass]
    public class GetDouble
    {
        [TestMethod]
        public void MinValue0To1()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Minimum;
            Assert.AreEqual(0, TrackBarUtils.GetDouble(trackBar, 0, 1));
        }

        [TestMethod]
        public void MidValue0To1()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Maximum / 2;
            Assert.AreEqual(0.5, TrackBarUtils.GetDouble(trackBar, 0, 1));
        }

        [TestMethod]
        public void MaxValue0To1()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Maximum;
            Assert.AreEqual(1, TrackBarUtils.GetDouble(trackBar, 0, 1));
        }

        [TestMethod]
        public void MinValueNeg5To5()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Minimum;
            Assert.AreEqual(-5, TrackBarUtils.GetDouble(trackBar, -5, 5));
        }

        [TestMethod]
        public void MidValueNeg5To5()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Maximum / 2;
            Assert.AreEqual(0, TrackBarUtils.GetDouble(trackBar, -5, 5));
        }

        [TestMethod]
        public void MaxValueNeg5To5()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Maximum;
            Assert.AreEqual(5, TrackBarUtils.GetDouble(trackBar, -5, 5));
        }
    }
}
