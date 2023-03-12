namespace ProyectoRescate.BL
{
    public class usuario
    {
        public int id_usuario { get; set; }
        public string? cedula { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? telefono { get; set; }
        public int edad { get; set; }
        public string? contrasenia { get; set; }
        public int id_rol { get; set; }
        public int id_estado_usuario { get; set; }
        public rol? rol { get; set; }
        public estado_usuario? estado_usuario { get; set; }
    }
}
