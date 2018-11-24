using System.Collections.Generic;
using System.Windows.Forms;

namespace ExampleProject
{
    public partial class TableViewMenu : Form
    {
        private List<SampleClass> objectsToEdit = new List<SampleClass>()
        {
            new SampleClass(),
            new SampleClass(),
            new SampleClass()
        };

        public TableViewMenu()
        {
            InitializeComponent();

            var tableGenerator = new GenericValueEditor.TableEditorGenerator<SampleClass>();
            tableGenerator.InitializeDataGridView(dataGridView1, objectsToEdit);
        }
    }
}
