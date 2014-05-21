using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace efiszkiProject.Models
{
    public class UserDefaultDataBase
    {
        [SQLite.PrimaryKey]
        public int Id  { get; set; }
        public string SlowkoPl { get; set; }
        public string SlowkoEn { get; set; }
        public string Podpowiedz { get; set; }
        public string Kontekst { get; set; }
        public int IloscOdpowiedzi { get; set; }
        public int IloscPoprawnychOdpowiedzi { get; set; }
        public int passa { get; set; }
        public int kategoria { get; set; }
    }
}
