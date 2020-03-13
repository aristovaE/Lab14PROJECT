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

namespace WpfApp14
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Boolean XMovement = false; // для чередования хода

        CurrentCell[] cellStates = new CurrentCell[9]
            {
                CurrentCell.NotSelected, CurrentCell.NotSelected, CurrentCell.NotSelected,
                CurrentCell.NotSelected, CurrentCell.NotSelected, CurrentCell.NotSelected,
                CurrentCell.NotSelected, CurrentCell.NotSelected, CurrentCell.NotSelected
            };
        // для установки пустых клеток сначала
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < LayoutRoot.Children.Count; i++)
            {
                if (LayoutRoot.Children[i] is Button)
                {
                    Button btn = (Button)LayoutRoot.Children[i];
                    btn.Click += new RoutedEventHandler(Button_Click);
                }
            }
        }
        private void Button_Click(object sender, EventArgs e)
        { }

        }
}
