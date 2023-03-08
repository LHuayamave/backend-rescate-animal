namespace ProyectoRescate.BL
{
    public class Form_hogar_temporal
    {
        public int id_form_hogar_temporal { get; set; }
        public int id_mascota { get; set; }
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public int id_tiempo { get; set; }
        public int id_estado_hogar_animal { get; set; }
        public mascotas? mascota { get; set; }
        public Tiempo? tiempo { get; set; }
        public Estado_hogar_animal? estado_hogar_animal { get; set; }



    }
}
