using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRescate.BL
{
    public class form_adopcion
    {
        public int id_form_adopcion { get; set; }
        public mascotas? mascotas { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? direccion { get; set; }
        public SqlDateTime fecha_nac { get; set; }
        public string? celular { get; set; }
        public string? correo { get; set; }
        public string? razones_adopcion { get; set; }
        public instruccion? instruccion { get; set; }
        public compromiso? compromiso { get; set; }
        public estado_adopcion? estado_adopcion { get; set; }
        public string Transaccion { get; set; }
    }
}
