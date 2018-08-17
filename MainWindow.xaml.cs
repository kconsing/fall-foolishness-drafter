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
using System.Collections.ObjectModel;
using fall_foolishness_drafter.Models;
using System.Windows.Threading;

namespace fall_foolishness_drafter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<player> players { get; set; }
        public string picturepath = Environment.CurrentDirectory + @"\pic1.jpg";
        DispatcherTimer _timer;
        TimeSpan _time;
        public MainWindow()
        {
            InitializeComponent();

            _time = TimeSpan.FromSeconds(90);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString(@"mm\:ss");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            this.DataContext = this;
            players = new ObservableCollection<player>();
            LoadDraftBoard();
            players = new ObservableCollection<player>(players.OrderBy(p => p.firstName));

            DraftPool.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
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
                return picturepath;
            }
        }

        public void LoadDraftBoard()
        {
            try
            {
                string[] lines;
                string[] tokens;

                lines = File.ReadAllLines(Environment.CurrentDirectory + @"\players.txt");

                foreach (string line in lines)
                {
                    tokens = line.Split('|');
                    players.Add(new player { firstName = tokens[0], lastName = tokens[1] , fullName=tokens[0] + " " + tokens[1]}); //resvisit I shouldnt have to set the full name like that >:(
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if(DraftPool.SelectedItem == null)
            {
                MessageBox.Show("Select a player to draft.");
            }
            else
            {
                if (radioTeam1.IsChecked == true)
                {
                    Team1.Items.Add(DraftPool.SelectedItem.ToString());
                }
                if (radioTeam2.IsChecked == true)
                {
                    Team2.Items.Add(DraftPool.SelectedItem.ToString());
                }
                if (radioTeam3.IsChecked == true)
                {
                    Team3.Items.Add(DraftPool.SelectedItem.ToString());
                }
                if (radioTeam4.IsChecked == true)
                {
                    Team4.Items.Add(DraftPool.SelectedItem.ToString());
                }

                players.Remove((player)DraftPool.SelectedItem);

                _time = TimeSpan.FromSeconds(90);

                _timer.Start();
            }
        }
    }
}
