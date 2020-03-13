using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp14
{
    public enum ResultOfGame : byte
    {
        //результаты игры:
        XWin, //победили крестики
        OWin, //победили нолики
        Nor, //ничья
        Continue //продолжить игру
    }
}
