﻿using Microsoft.AspNetCore.Mvc;
using ProyectoRescate.BL;
using RescateSolucion.CodeGeneral;
using System.Data;
using System.Xml.Linq;

namespace RescateSolucion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<usuario>> GetUsuarios()
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = XDocument.Parse("<usuario></usuario>");
            Console.Write(NameStoredProcedure.SPGetUsuarios+"\n\n"+cadenaConexion+ "\n\n" + xmlParam.ToString());
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPGetUsuarios, cadenaConexion, "CONSULTAR_USUARIOS", xmlParam.ToString());
            List<usuario> listData = new List<usuario>();
            if(dsResultado.Tables.Count> 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        usuario objResponse = new usuario
                        {
                            id_usuario = Convert.ToInt32(row["id_usuario"]),
                            cedula = row["cedula"].ToString(),
                            nombre = row["nombre"].ToString(),
                            apellido = row["apellido"].ToString(),
                            telefono = row["telefono"].ToString(),
                            edad = Convert.ToInt32(row["edad"]),
                            contrasenia = row["contrasenia"].ToString(),
                            rol = new rol()
                            {
                                id_rol = Convert.ToInt32(row["id_rol"]),
                                descripcion = row["descripcion"].ToString()
                            },
                            estado_usuario = new estado_usuario()
                            {
                                id_estado_usuario = Convert.ToInt32(row["id_estado_usuario"]),
                                descripcion = row["estDesc"].ToString()
                            }
                        };
                        listData.Add(objResponse);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return Ok(listData);
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<RespuestaSP>> SetUsuario([FromBody] usuario usuarios)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(usuarios);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPSetUsuarios, cadenaConexion, "", xmlParam.ToString());
            RespuestaSP objResponse = new RespuestaSP();
            if(dsResultado.Tables.Count > 0)
            {
                try
                {
                    objResponse.Respuesta = dsResultado.Tables[0].Rows[0]["Respuesta"].ToString();
                    objResponse.Leyenda = dsResultado.Tables[0].Rows[0]["Leyenda"].ToString();
                }
                catch (Exception e)
                {
                    objResponse.Respuesta = "ERROR";
                    objResponse.Leyenda = "No se ha obtenido resultados de la transaccion";
                    return BadRequest(objResponse);
                }
            }
            return Ok(objResponse);
        }
    }
}
