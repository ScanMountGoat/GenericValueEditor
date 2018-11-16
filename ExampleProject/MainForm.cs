using System.Windows.Forms;

namespace ExampleProject
{
    public partial class MainForm : Form
    {
        SampleClass object1 = new SampleClass();
        SampleClass object2 = new SampleClass();

        public MainForm()
        {
            InitializeComponent();

            var objectEditor = new GenericValueEditor.EditorGenerator<SampleClass>(object1);
            objectEditor.AddEditorControls(flowLayoutPanel1);

            objectEditor.ObjectToEdit = object2;

            var menu = new TableViewMenu();
            menu.Show();
        }

        private void flowLayoutPanel1_Resize(object sender, System.EventArgs e)
        {
            GenericValueEditor.Utils.GuiUtils.ScaleControlsHorizontallyToLayoutWidth(flowLayoutPanel1);
            System.Diagnostics.Debug.WriteLine(object2.TextValue);
        }
    }
}
