using Microsoft.AspNetCore.Mvc;
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
            Console.Write(NameStoredProcedure.SPGetUsuarios + "\n\n" + cadenaConexion + "\n\n" + xmlParam.ToString());
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPGetUsuarios, cadenaConexion, "CONSULTAR_USUARIOS", xmlParam.ToString());
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
            return Ok(listData);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<usuario>> GetUsuarioId(int id)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = XDocument.Parse("<usuario><id_usuario>" + id + "</id_usuario></usuario>");
            Console.Write(NameStoredProcedure.SPGetUsuarios + "\n\n" + cadenaConexion + "\n\n" + xmlParam.ToString());
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPGetUsuarios, cadenaConexion, "CONSULTA_USUARIO_ID", xmlParam.ToString());
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
            return Ok(listData);
        }

        [HttpPost]
        public async Task<ActionResult<usuario>> loginUsuario(string cedula, string contrasenia)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = XDocument.Parse("<usuario><cedula>" + cedula + "</cedula><contrasenia>"+contrasenia+"</contrasenia></usuario>");
            Console.Write(NameStoredProcedure.SPLoginUsuario + "\n\n" + cadenaConexion + "\n\n" + xmlParam.ToString());
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPLoginUsuario, cadenaConexion, "LOGIN_USUARIO", xmlParam.ToString());
            List<usuario> listData = new List<usuario>();
            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        usuario objResponse = new usuario
                        {
                            nombre = row["nombre"].ToString(),
                            apellido = row["apellido"].ToString(),
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
        public async Task<ActionResult<RespuestaSP>> SetUsuario([FromBody] usuario usuario)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(usuario);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPSetUsuario, cadenaConexion, "INSERTAR_USUARIO", xmlParam.ToString());
            RespuestaSP objResponse = new RespuestaSP();
            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    objResponse.Respuesta = dsResultado.Tables[0].Rows[0]["Respuesta"].ToString();
                    objResponse.Leyenda = dsResultado.Tables[0].Rows[0]["Leyenda"].ToString();
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                    objResponse.Respuesta = "ERROR";
                    objResponse.Leyenda = "No se ha obtenido resultados de la transaccion";
                    return BadRequest(objResponse);
                }
            }
            return Ok(objResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RespuestaSP>> PutUsuario(int id, [FromBody] usuario usuario)
        {

            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            usuario.id_usuario= id;
            XDocument xmlParam = DBXmlMethods.GetXml(usuario);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPSetUsuario, cadenaConexion, "MODIFICAR_USUARIO", xmlParam.ToString());
            RespuestaSP objResponse = new RespuestaSP();
            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    objResponse.Respuesta = dsResultado.Tables[0].Rows[0]["Respuesta"].ToString();
                    objResponse.Leyenda = dsResultado.Tables[0].Rows[0]["Leyenda"].ToString();
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                    objResponse.Respuesta = "ERROR";
                    objResponse.Leyenda = "No se ha obtenido resultados de la transaccion";
                    return BadRequest(objResponse);
                }
            }
            return Ok(objResponse);
        }
    }
}
