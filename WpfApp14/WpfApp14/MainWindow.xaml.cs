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
        Boolean X = false; // для чередования хода
        BitmapImage bmpImgX;
        BitmapImage bmpImg0;

        CurrentCell[] curCell = new CurrentCell[9]
            {
                CurrentCell.NotSelected, CurrentCell.NotSelected, CurrentCell.NotSelected,
                CurrentCell.NotSelected, CurrentCell.NotSelected, CurrentCell.NotSelected,
                CurrentCell.NotSelected, CurrentCell.NotSelected, CurrentCell.NotSelected
            };
        // для установки пустых клеток сначала
        public MainWindow()
        {
            InitializeComponent();
            Uri uriImg0 = new Uri(@"pack://application:,,,/Resources/o.png", UriKind.Absolute);
            Uri uriImgX = new Uri(@"pack://application:,,,/Resources/x.png", UriKind.Absolute);
            bmpImg0 = GetBitmapImage(uriImg0);
            bmpImgX = GetBitmapImage(uriImgX);
            for (int i = 0; i < LayoutRoot.Children.Count; i++)
            {
                if (LayoutRoot.Children[i] is Button)
                {
                    Button btn = (Button)LayoutRoot.Children[i];
                    btn.Click += new RoutedEventHandler(Button_Click);
                }
            }
        }
        BitmapImage GetBitmapImage(Uri uri)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = uri;
            bitmapImage.EndInit();
            return bitmapImage;
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Image img = (Image)btn.Content;
            int btnNumber = Convert.ToInt32((string)btn.Tag);

            if (X == true && curCell[btnNumber] == CurrentCell.NotSelected)
            {
                img.Source = bmpImgX;
                curCell[btnNumber] = CurrentCell.X;
                X = false;

            }
            else if (curCell[btnNumber] == CurrentCell.NotSelected)
            {
                img.Source = bmpImg0;
                curCell[btnNumber] = CurrentCell.O;
                X = true;
            }

            switch (GetGameResult())
            {
                case ResultOfGame.XWin:
                    MessageBox.Show(this, "Выиграли крестики");
                    break;
                case ResultOfGame.OWin:
                    MessageBox.Show(this, "Выиграли нолики");
                    break;
                case ResultOfGame.Nor:
                    MessageBox.Show(this, "Ничья");
                    break;
                case ResultOfGame.Continue:
                    break;
            }

        }
        private ResultOfGame GetGameResult()
        {
            if (curCell.All<CurrentCell>(c => c != CurrentCell.NotSelected)) //Проверить каждый элемент массива cellStates не равен ли он CellState.NotSelected. Если все неравны, то ничья.
                return ResultOfGame.Nor;
            if (CheckCellState(CurrentCell.O))
                return ResultOfGame.OWin;
            if (CheckCellState(CurrentCell.X))
                return ResultOfGame.XWin;
            return ResultOfGame.Continue;
        }
        private Boolean CheckCellState(CurrentCell curCell)
        {
            if (this.curCell[0] == curCell && this.curCell[1] == curCell && this.curCell[2] == curCell)
                return true;
            else
                if (this.curCell[3] == curCell && this.curCell[4] == curCell && this.curCell[5] == curCell)
                return true;
            else
                    if (this.curCell[6] == curCell && this.curCell[7] == curCell && this.curCell[8] == curCell)
                return true;
            else
                        if (this.curCell[0] == curCell && this.curCell[3] == curCell && this.curCell[6] == curCell)
                return true;
            else
                            if (this.curCell[1] == curCell && this.curCell[4] == curCell && this.curCell[7] == curCell)
                return true;
            else
                                if (this.curCell[2] == curCell && this.curCell[5] == curCell && this.curCell[8] == curCell)
                return true;
            else
                                    if (this.curCell[0] == curCell && this.curCell[4] == curCell && this.curCell[8] == curCell)
                return true;
            else
                                        if (this.curCell[2] == curCell && this.curCell[4] == curCell && this.curCell[6] == curCell)
                return true;
            return false;
        }
    }

}

