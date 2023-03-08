namespace ProyectoRescate.BL
{
    public class Form_rescate_animal
    {
        public int id_form_rescate_animal { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public int id_tipo_mascota { get; set; }
        public int id_estado_rescate_animal { get; set; }
        /*
        public tipo_mascota tipo_mascota { get; set; }
        public Estado_rescate_animal estado_rescate_animal { get; set; }
        public int? croquis { get; set; }*/
    }
}
