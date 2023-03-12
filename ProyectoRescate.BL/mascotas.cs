﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Buffers.Text;
using System.Runtime.CompilerServices;

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
        /*
        public int id_mascotas { get; set; }
        public string? nombre { get; set; }
        public tipo_mascota? tipo_mascota { get; set; }
        public estado_salud? estado_salud { get; set; }
        public estado_mascotas? estado_mascotas { get; set; }
        public sexo? sexo { get; set; }*/
        //public string Transaccion { get; set; }

        public int id_mascotas { get; set; }
        public string? nombre { get; set; }
        public int id_tipo_mascota { get; set; }
        public int id_estado_salud { get; set; }
        public int id_estado_mascotas { get; set; }
        public int id_sexo { get; set; }
        //public byte[] foto { get; set; }
        public string? foto { get; set; }
        public tipo_mascota? tipo_mascota { get; set; }
        public estado_salud? estado_salud { get; set; }
        public estado_mascotas? estado_mascotas { get; set; }
        public sexo? sexo { get; set; }
        
    }
}