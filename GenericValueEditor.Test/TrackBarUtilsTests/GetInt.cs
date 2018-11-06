using GenericValueEditor.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericValueEditor.Test.TrackBarUtilsTest
{
    [TestClass]
    public class GetInt
    {
        [TestMethod]
        public void MinValue0To10()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Minimum;
            Assert.AreEqual(0, TrackBarUtils.GetInt(trackBar, 0, 10));
        }

        [TestMethod]
        public void MidValue0To10()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Maximum / 2;
            Assert.AreEqual(5, TrackBarUtils.GetInt(trackBar, 0, 10));
        }

        [TestMethod]
        public void MaxValue0To10()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Maximum;
            Assert.AreEqual(10, TrackBarUtils.GetInt(trackBar, 0, 10));
        }

        [TestMethod]
        public void MinValueNeg5To5()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Minimum;
            Assert.AreEqual(-5, TrackBarUtils.GetInt(trackBar, -5, 5));
        }

        [TestMethod]
        public void MidValueNeg5To5()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Maximum / 2;
            Assert.AreEqual(0, TrackBarUtils.GetInt(trackBar, -5, 5));
        }

        [TestMethod]
        public void MaxValueNeg5To5()
        {
            var trackBar = new System.Windows.Forms.TrackBar();
            trackBar.Value = trackBar.Maximum;
            Assert.AreEqual(5, TrackBarUtils.GetInt(trackBar, -5, 5));
        }
    }
}
