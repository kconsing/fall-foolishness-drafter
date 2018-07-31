using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace fall_foolishness_drafter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> players;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            LoadDraftBoard();
        }

        public string BackgroundPath
        {
            get
            {
                return Environment.CurrentDirectory + @"\bg1.jpg";
            }
        }

        public string PicturePath
        {
            get
            {
                return Environment.CurrentDirectory + @"\pic1.jpg";
            }
        }

        public void LoadDraftBoard()
        {
            try
            {
                string[] lines;
                string[] tokens;
                players = new List<string>();

                lines = File.ReadAllLines(Environment.CurrentDirectory + @"\players.txt");

                foreach (string line in lines)
                {
                    tokens = line.Split('|');
                    player player = new player(tokens[0], tokens[1]);

                    DraftPool.Items.Add(line);
                    //DraftPool.Items.Add(player.lastName + ", " + player.firstName);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }

    public class player
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        public player(string firstname = "fist", string lastname = "last")
        {
            firstName = firstname;
            lastName = lastname;
        }

    }
}
