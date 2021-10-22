using System.ComponentModel.DataAnnotations.Schema;

namespace WikiAPI.Models{
    public class Rol{
        [Column("rol_id")]
        public int rolId {get; set;}
        [Column("rol_nombre")]
        public string rolNombre {get; set;}
    }
}