using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efiszkiProject.Models
{
    public class UserInformation
    {
        [SQLite.PrimaryKey]
        public int Id { get; set; }
        public int IloscOgolnychOdpowiedzi { get; set; }
        public int passa { get; set; }
        public int IloscDobrychOdpowiedzi { get; set; }
        public int IloscLogowan { get; set; }
        public int NauczucielTest { get; set; }
        public int NauczycielPin { get; set; }

    }
}
