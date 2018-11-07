using System.Windows.Forms;

namespace ExampleProject
{
    public partial class MainForm : Form
    {
        SampleClass objectToEdit = new SampleClass();

        public MainForm()
        {
            InitializeComponent();

            var objectEditor = new GenericValueEditor.EditorGenerator(objectToEdit);
            objectEditor.AddEditorControls(flowLayoutPanel1);

        }

        private void flowLayoutPanel1_Resize(object sender, System.EventArgs e)
        {
            GenericValueEditor.Utils.GuiUtils.ScaleControlsHorizontallyToLayoutWidth(flowLayoutPanel1);
        }
    }
}
