using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRescate.BL
{
    public class form_apadrinamiento
    {
        public int id_form_apadrinamiento {  get; set; }
        public int id_mascotas { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public DateTime fecha_nac { get; set; }
        public string? ciudad { get; set; }
        public string? correo { get; set; }
        public string? num_cuenta { get; set; }
        public string? titular_cuenta { get; set; }
        public int cant_colaboracion { get; set; }
        public int id_periodicidad { get; set; }
        public int id_estado_apadrinamiento { get; set; }
        public mascotas? mascotas { get; set; }
        public periodicidad? periodicidad { get; set; }
        public estado_apadrinamiento? estado_Apadrinamiento { get; set; }
    }
}
