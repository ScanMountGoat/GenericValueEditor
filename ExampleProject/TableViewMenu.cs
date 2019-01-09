using System.Collections.Generic;
using System.Windows.Forms;

namespace ExampleProject
{
    public partial class TableViewMenu : Form
    {
        private List<SampleClass> objectsToEdit = new List<SampleClass>()
        {
            new SampleClass() { enumValue = SampleClass.SampleEnum.A },
            new SampleClass(),
            new SampleClass() { EnableFeature = true, TextValue = "test" }
        };

        public TableViewMenu()
        {
            InitializeComponent();

            var tableGenerator = new GenericValueEditor.TableEditorGenerator<SampleClass>();
            tableGenerator.InitializeDataGridView(dataGridView1, objectsToEdit);

            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("New Edit:");
            foreach (var item in objectsToEdit)
            {
                System.Diagnostics.Debug.WriteLine(item.ToString());
            }
            System.Diagnostics.Debug.WriteLine("");
        }
    }
}
