using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarium.Classes
{
    class AppData
    {
        public static DataBase.DB_For_LibrariumEntities3 Context { get; } = new DataBase.DB_For_LibrariumEntities3();
    }
}
