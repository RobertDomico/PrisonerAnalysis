namespace PrisonerAnalysis
{
    public partial class Main : Form
    {
        private List<Prisoner> prisoners = new List<Prisoner>();

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

                prisoners = File.ReadAllLines(files[0])
                                           .Skip(3)
                                           .SkipLast(2)
                                           .Select(v => Prisoner.FromCsv(v))
                                           .ToList();

                MessageBox.Show(prisoners.Count().ToString());
            }
        }
    }
}