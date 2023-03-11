using Microsoft.AspNetCore.Mvc;
using ProyectoRescate.BL;
using RescateSolucion.CodeGeneral;
using System.Data;
using System.Xml.Linq;

namespace RescateSolucion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormVoluntarioController : ControllerBase
    {
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<form_voluntario>> GetFormVoluntario()
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = XDocument.Parse("<form_voluntario></form_voluntario>");
            Console.Write(NameStoredProcedure.SPSetFormVoluntario + "\n\n" + cadenaConexion + "\n\n" + xmlParam.ToString());
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPGetFormVoluntario, cadenaConexion, "CONSULTAR_FORM_VOLUNTARIO", xmlParam.ToString());
            List<form_voluntario> listData = new List<form_voluntario>();
            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        form_voluntario objResponse = new form_voluntario
                        {
                            id_form_voluntario = Convert.ToInt32(row["id_form_voluntarios"]),
                            nombre = row["nombre"].ToString(),
                            apellido = row["apellido"].ToString(),
                            fecha_nac = Convert.ToDateTime(row["fecha_nac"]),
                            edad = Convert.ToInt32(row["edad"]),
                            ciudad = row["ciudad"].ToString(),
                            ocupacion = row["ocupacion"].ToString(),
                            correo = row["correo"].ToString(),
                            direccion = row["direccion"].ToString(),
                            razones_voluntario = row["razones_voluntario"].ToString(),
                            id_estado_voluntario = Convert.ToInt32(row["id_estado_voluntario"]),

                            estado_Voluntario = new estado_voluntario()
                            {
                                descripcion = row["descripcion"].ToString()
                            }
                        };
                        listData.Add(objResponse);
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }
            return Ok(listData);
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<RespuestaSP>> SetFormVoluntario([FromBody] form_voluntario form_Voluntario)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(form_Voluntario);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPSetFormVoluntario, cadenaConexion, "INSERTAR_FORM_VOLUNTARIO", xmlParam.ToString());
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
        public async Task<ActionResult<RespuestaSP>> PutFormVoluntario(int id, [FromBody] form_voluntario form_Voluntario)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            form_Voluntario.id_form_voluntario = id;
            XDocument xmlParam = DBXmlMethods.GetXml(form_Voluntario);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPSetFormVoluntario, cadenaConexion, "MODIFICAR_ESTADO_FORM_VOLUNTARIO", xmlParam.ToString());
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
