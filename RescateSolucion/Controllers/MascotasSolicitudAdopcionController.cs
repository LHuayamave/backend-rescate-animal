using Microsoft.AspNetCore.Mvc;
using ProyectoRescate.BL;
using RescateSolucion.CodeGeneral;
using System.Data;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RescateSolucion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotasSolicitudAdopcionController : ControllerBase
    {
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<form_adopcion>> GetAdopciones()
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = XDocument.Parse("<form_adopcion></form_adopcion>");
            Console.Write(NameStoredProcedure.SPGetFormAdopcion + "\n\n" + cadenaConexion + "\n\n" + xmlParam.ToString());
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPGetFormAdopcion, cadenaConexion, "CONSULTAR_ADOPCION_MASCOTAS", xmlParam.ToString());
            List<form_adopcion> listData = new List<form_adopcion>();
            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        form_adopcion objResponse = new form_adopcion
                        {
                            id_form_adopcion = Convert.ToInt32(row["id_form_adopcion"]),
                            id_mascotas = Convert.ToInt32(row["id_mascotas"]),
                            nombre = row["nombre"].ToString(),
                            apellido = row["apellido"].ToString(),
                            direccion = row["direccion"].ToString(),
                            fecha_nac = Convert.ToDateTime(row["fecha_nac"]).ToString("dd-MM-yyyy"),
                            celular = row["celular"].ToString(),
                            correo = row["correo"].ToString(),
                            razones_adopcion = row["razones_adopcion"].ToString(),
                            id_instruccion = Convert.ToInt32(row["id_instruccion"]),
                            id_compromiso = Convert.ToInt32(row["id_compromiso"]),
                            id_estado_adopcion = Convert.ToInt32(row["id_estado_adopcion"]),

                            mascotas = new mascotas()
                            {
                                nombre = row["Nombre de la mascota"].ToString()
                            },

                            instruccion = new instruccion()
                            {
                                descripcion = row["Nivel de instruccion"].ToString()
                            },
                            compromiso = new compromiso()
                            {
                                descripcion = row["Compromiso"].ToString()
                            },
                            estado_Adopcion = new estado_adopcion()
                            {
                                descripcion = row["Estado de la adopcion"].ToString()
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
        public async Task<ActionResult<RespuestaSP>> SetAdoptarMascotas([FromBody] form_adopcion form_adopt)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(form_adopt);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPSetMascotas2, cadenaConexion, "INGRESAR_SOLICITUD_ADOPCION", xmlParam.ToString());
            RespuestaSP objResponse = new RespuestaSP();
            //List<Mascotas> listData = new List<Mascotas>();
            if (dsResultado.Tables.Count > 0)
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
        [HttpPut("{id}")]
        public async Task<ActionResult<RespuestaSP>> PutFormAdopcion(int id, [FromBody] form_adopcion form_Adopcion)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            form_Adopcion.id_form_adopcion= id;
            XDocument xmlParam = DBXmlMethods.GetXml(form_Adopcion);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPSetMascotas2, cadenaConexion, "MODIFICAR_ESTADO_ADOPCION", xmlParam.ToString());
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
