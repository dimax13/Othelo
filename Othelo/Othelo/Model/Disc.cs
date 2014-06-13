using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othelo.Model
{
    class Disc
    {
        public int ID { get; set; }
        public DiscColor Color { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
    }

    public enum DiscColor
    {
        NONE, BLACK, WHITE, PLAYABLE
    }
}
