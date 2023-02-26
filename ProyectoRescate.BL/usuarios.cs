namespace ProyectoRescate.BL
{
    public class usuarios
    {
        public int id_usuario { get; set; }
        public string? cedula { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? edad { get; set; }
        public string? contrasenia { get; set; }
        public rol? rol { get; set; }
        public estado_usuario? estado_usuario { get; set; }
        public string Transaccion { get; set; }
    }
}
