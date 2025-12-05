using Folivora.Scaffold;
using Microsoft.EntityFrameworkCore.Storage;

namespace Проект
{
    public class Office
    {
        public int id_off { get; set; }
        public string parent { get; set; }
        public int number_off { get; set; } 
    }
}
