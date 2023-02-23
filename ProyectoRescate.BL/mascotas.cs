namespace ProyectoRescate.BL
{
    public class mascotas
    {/*
        public int id_mascotas { get; set; }
        public string? nombre { get; set; }
        public TipoMascota? TipoMascota { get; set; }
        public EstadoSalud? EstadoSalud { get; set; }
        public EstadoMascotas? EstadoMascotas { get; set; }
        public Sexo? Sexo { get; set; }
        public string Transaccion { get; set; }*/

        public int id_mascotas { get; set; }
        public string? nombre { get; set; }
        public tipo_mascota? tipo_mascota { get; set; }
        public estado_salud? estado_salud { get; set; }
        public estado_mascotas? estado_mascotas { get; set; }
        public sexo? sexo { get; set; }
        public string Transaccion { get; set; }
    }
}
