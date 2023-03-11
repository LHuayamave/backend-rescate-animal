namespace ProyectoRescate.BL
{
    public class form_voluntario
    {
        public int id_form_voluntario { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public DateTime fecha_nac { get; set; }
        public int edad { get; set; }
        public string? ciudad { get; set; }
        public string? ocupacion { get; set; }
        public string? correo { get; set; }
        public string? direccion { get; set; }
        public string? razones_voluntario { get; set; }
        public int id_estado_voluntario { get; set; }
        public estado_voluntario? estado_Voluntario { get; set; }

    }
}
