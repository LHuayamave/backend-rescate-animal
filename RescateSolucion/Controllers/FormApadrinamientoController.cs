using Microsoft.AspNetCore.Mvc;
using ProyectoRescate.BL;
using RescateSolucion.CodeGeneral;
using System.Data;
using System.Xml.Linq;


namespace RescateSolucion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormApadrinamientoController : ControllerBase
    {
        [Route("[action]")]
        [HttpGet]

        public async Task<ActionResult<form_apadrinamiento>> GetFormApadrinamiento()
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = XDocument.Parse("<form_apadrinamiento></form_apadrinamiento>");
            Console.Write(NameStoredProcedure.GetFormApadrinamiento + "\n\n" + cadenaConexion + "\n\n" + xmlParam.ToString());
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.GetFormApadrinamiento, cadenaConexion, "CONSULTAR_FORMULARIO_APADRINAMIENTO", xmlParam.ToString());
            List<form_apadrinamiento> listData = new List<form_apadrinamiento>();
            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        form_apadrinamiento objResponse = new form_apadrinamiento
                        {

                            id_form_apadrinamiento = Convert.ToInt32(row["id_form_apadrinamiento"]),
                            id_mascotas = Convert.ToInt32(row["id_mascotas"]),
                            mascotas = new mascotas()
                            {
                                nombre = row["Nombre de la mascota"].ToString()
                            },
                            nombre = row["nombre"].ToString(),
                            apellido = row["apellido"].ToString(),
                            fecha_nac = Convert.ToDateTime(row["fecha_nac"]).ToString("dd-MM-yyyy"),
                            ciudad = row["ciudad"].ToString(),
                            correo = row["correo"].ToString(),
                            num_cuenta = row["num_cuenta"].ToString(),
                            titular_cuenta = row["titular_cuenta"].ToString(),
                            cant_colaboracion = Convert.ToInt32(row["cant_colaboracion"]),
                            id_periodicidad = Convert.ToInt32(row["id_periodicidad"]),
                            id_estado_apadrinamiento = Convert.ToInt32(row["id_estado_apadrinamiento"]),

                            periodicidad = new periodicidad()
                            {
                                descripcion = row["Periodicidad"].ToString()
                            },
                            estado_Apadrinamiento = new estado_apadrinamiento()
                            {
                                descripcion = row["Estado del Formulario"].ToString()
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

        public async Task<ActionResult<RespuestaSP>> SetFormApadrinamiento([FromBody] form_apadrinamiento form_Apadrinamiento)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(form_Apadrinamiento);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SetFormApadrinamiento, cadenaConexion, "INSERTAR_FORM_APADRINAMIENTO", xmlParam.ToString());
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
        public async Task<ActionResult<RespuestaSP>> PutFormApadrinamiento(int id, [FromBody] form_apadrinamiento form_Apadrinamiento)
        {

            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            form_Apadrinamiento.id_form_apadrinamiento = id;
            XDocument xmlParam = DBXmlMethods.GetXml(form_Apadrinamiento);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SetFormApadrinamiento, cadenaConexion, "MODIFICAR_ESTADO_FORM_APADRINAMIENTO", xmlParam.ToString());
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
