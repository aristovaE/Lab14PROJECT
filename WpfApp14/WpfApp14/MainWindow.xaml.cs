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
        BitmapImage bmpImgX; //Для изображения крестика
        BitmapImage bmpImg0; //Для изображения нолика

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

            Turn.Content = "Ход: О";         
        }

        BitmapImage GetBitmapImage(Uri uri)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = uri;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        /// <summary>
        /// Установка крестика или нолика на поле
        /// </summary>
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
                Turn.Content = "Ход: О";
            }
            else if (curCell[btnNumber] == CurrentCell.NotSelected)
            {
                img.Source = bmpImg0;
                curCell[btnNumber] = CurrentCell.O;
                X = true;
                Turn.Content = "Ход: Х";
            }

            //Вывод сообщени в зависимости от результатов игры(или продолжение игры)
            switch (GetGameResult())
            {
                case ResultOfGame.XWin: 
                    MessageBox.Show(this, "Выиграли крестики", "Победа!");
                    Restart();
                    break;
                case ResultOfGame.OWin:
                    MessageBox.Show(this, "Выиграли нолики", "Победа!");
                    Restart();
                    break;
                case ResultOfGame.Nor:
                    MessageBox.Show(this, "Ничья", "Ничья!");
                    Restart();
                    break;
                case ResultOfGame.Continue:
                    break;
            }

        }

        /// <summary>
        /// Рестарт игры при победе или заполнении всех ячеек
        /// </summary>
        private void Restart()
        {
            for (int i = 0; i < curCell.Length; i++)
                curCell[i] = CurrentCell.NotSelected;
            for (int i = 0; i < LayoutRoot.Children.Count; i++)
            {
                if (LayoutRoot.Children[i] is Button)
                {
                    Button btn = (Button)LayoutRoot.Children[i];
                    ((Image)btn.Content).Source = null;
                }
            }
        }

        /// <summary>
        /// Проверка состояния игры
        /// </summary>
        /// <returns>Установка результата игры: 
        /// Победа крестиков или ноликов
        /// Ничья - если все ячейки заполнены и не было сообщения о победе
        /// Иначе -> продолжение игры
        /// </returns>
        private ResultOfGame GetGameResult()
        {
            if (CheckCellState(CurrentCell.O))
                return ResultOfGame.OWin;
            if (CheckCellState(CurrentCell.X))
                return ResultOfGame.XWin;
            if (curCell.All<CurrentCell>(c => c != CurrentCell.NotSelected)) //Проверить каждый элемент массива cellStates не равен ли он CellState.NotSelected. Если все неравны, то ничья.
                return ResultOfGame.Nor;
            return ResultOfGame.Continue;
        }

        /// <summary>
        /// Проверка всех победных комбинаций
        /// </summary>
        /// <param name="curCell">Крестик или нолик</param>
        /// <returns>true - если победа достигнута, иначе - false</returns>
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

