using System.Windows.Forms;

namespace ExampleProject
{
    public partial class MainForm : Form
    {
        SampleClass objectToEdit = new SampleClass();

        public MainForm()
        {
            InitializeComponent();

            var objectEditor = new GenericValueEditor.ObjectEditor(objectToEdit);
            objectEditor.AddEditorControls(flowLayoutPanel1);
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(objectToEdit.ToString());
        }

        private void flowLayoutPanel1_Resize(object sender, System.EventArgs e)
        {
            GenericValueEditor.Utils.GuiUtils.ScaleControlsHorizontallyToLayoutWidth(flowLayoutPanel1);
        }
    }
}
