using Folivora.Scaffold;
using Microsoft.EntityFrameworkCore.Storage;

namespace Проект
{
    public class Zaiavka
    {
        public int? id_zv { get; set; }
        public string? status_zv { get; set; }
        public string? ip_printer { get; set; }
        public string? cartridg_model { get; set; }
        public string? parent_off { get; set; }
        public int? num_off { get; set; }

    }
}
