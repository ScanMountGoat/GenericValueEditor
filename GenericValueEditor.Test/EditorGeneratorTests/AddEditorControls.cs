using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericValueEditor.Test.EditorGeneratorTests
{
    [TestClass]
    public class AddEditorControls
    {
        private class NoMembers
        {
        }

        private class ValidClass
        {
            [EditInfo("edit me", ValueEnums.ValueType.Bool)]
            public bool ReadonlyFlag { get; set; }
        }

        [TestMethod]
        public void NoValidAttributes()
        {
            var generator = new EditorGenerator<object>(new object());

            var parent = new System.Windows.Forms.Control();
            generator.AddEditorControls(parent);
            Assert.AreEqual(0, parent.Controls.Count);
        }

        [TestMethod]
        public void NoMembersInClass()
        {
            var generator = new EditorGenerator<NoMembers>(new NoMembers());

            var parent = new System.Windows.Forms.Control();
            generator.AddEditorControls(parent);
            Assert.AreEqual(0, parent.Controls.Count);
        }

        [TestMethod]
        public void ValidMembersInClass()
        {
            var generator = new EditorGenerator<ValidClass>(new ValidClass());

            var parent = new System.Windows.Forms.Control();
            generator.AddEditorControls(parent);
            Assert.AreNotEqual(0, parent.Controls.Count);
        }
    }
}
