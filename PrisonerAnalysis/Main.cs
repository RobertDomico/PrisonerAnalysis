using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.ComponentModel;
using System.Windows.Forms;

namespace PrisonerAnalysis
{
    public partial class Main : Form
    {        
        private int newSortColumn;
        private ListSortDirection newColumnDirection = ListSortDirection.Ascending;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                List<Prisoner> prisoners = new List<Prisoner>();
                prisoners = File.ReadAllLines(files[0])
                                           .Skip(3)
                                           .SkipLast(2)
                                           .Select(v => Prisoner.FromCsv(v))
                                           .ToList();
              
                dataGridView.DataSource = new SortableBindingList<Prisoner>(prisoners);


                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].SortMode != DataGridViewColumnSortMode.NotSortable)
            {
                if (e.ColumnIndex == newSortColumn)
                {
                    if (newColumnDirection == ListSortDirection.Ascending)
                        newColumnDirection = ListSortDirection.Descending;
                    else
                        newColumnDirection = ListSortDirection.Ascending;
                }

                newSortColumn = e.ColumnIndex;

                switch (newColumnDirection)
                {
                    case ListSortDirection.Ascending:
                        dataGridView.Sort(dataGridView.Columns[newSortColumn], ListSortDirection.Ascending);
                        break;
                    case ListSortDirection.Descending:
                        dataGridView.Sort(dataGridView.Columns[newSortColumn], ListSortDirection.Descending);
                        break;
                }

                if (newColumnDirection == ListSortDirection.Ascending)
                {
                    dataGridView.Columns[newSortColumn].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                }
                else
                {
                    dataGridView.Columns[newSortColumn].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                }
            }
        }
    }
    
}