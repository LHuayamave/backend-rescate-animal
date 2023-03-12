using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoRescate.BL;
using RescateSolucion.CodeGeneral;
using System.Data;
using System.Xml.Linq;

namespace RescateSolucion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /*[HttpGet("{cedula}")]
        public async Task<ActionResult<string>> GetUsuarioCedula(string cedula)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = XDocument.Parse("<usuario><cedula>" + cedula + "</cedula></usuario>");
            Console.Write("verificar resultado" + NameStoredProcedure.SPGetUsuarios + "\n\n" + cadenaConexion + "\n\n" + xmlParam.ToString());
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPGetUsuarios, cadenaConexion, "OBTENER_CONTRASENIA", xmlParam.ToString());
            //List<usuario> listData = new List<usuario>();
            string contraseniaR="not";

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        contraseniaR = row["contrasenia"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return (contraseniaR);
        }*/

        
        [HttpGet("{cedula}")]
        public async Task<ActionResult<usuario>> GetUsuarioCedula(string cedula)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = XDocument.Parse("<usuario><cedula>" + cedula + "</cedula></usuario>");
            Console.Write("verificar resultado" + NameStoredProcedure.SPGetUsuarios + "\n\n" + cadenaConexion + "\n\n" + xmlParam.ToString());
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPGetUsuarios, cadenaConexion, "CONSULTA_USUARIO_CEDULA", xmlParam.ToString());
            //List<usuario> listData = new List<usuario>();
            usuario usuarioResp = new usuario();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        usuarioResp.cedula  = row["cedula"].ToString();
                        usuarioResp.contrasenia = row["contrasenia"].ToString();
                        usuarioResp.nombre = row["nombre"].ToString();
                        usuarioResp.apellido = row["apellido"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return Ok(usuarioResp);
        }
        /*
        public async Task<ActionResult<usuario>> GetUsuarioCedula(string cedula)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = XDocument.Parse("<usuario><cedula>" + cedula + "</cedula></usuario>");
            Console.Write("verificar resultado" + NameStoredProcedure.SPGetUsuarios + "\n\n" + cadenaConexion + "\n\n" + xmlParam.ToString());
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPGetUsuarios, cadenaConexion, "CONSULTA_USUARIO_CEDULA", xmlParam.ToString());
            List<usuario> listData = new List<usuario>();
            if (dsResultado.Tables.Count > 0)
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
                            id_rol = Convert.ToInt32(row["id_rol"]),
                            id_estado_usuario = Convert.ToInt32(row["id_estado_usuario"]),
                            rol = new rol()
                            {
                                descripcion = row["descripcion"].ToString()
                            },
                            estado_usuario = new estado_usuario()
                            {
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
            return Ok(listData[0]);
        }*/


    }
}
