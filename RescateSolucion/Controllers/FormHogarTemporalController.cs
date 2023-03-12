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
    public class FormHogarTemporalController : ControllerBase
    {
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<RespuestaSP>> SetFrmHogarTemporal([FromBody] Form_hogar_temporal Form_hogar_temporal)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(Form_hogar_temporal);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPSetFormHogarTemporal, cadenaConexion, "INSERTAR_FORMULARIO", xmlParam.ToString());
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
                    objResponse.Respuesta = "ERROR";
                    objResponse.Leyenda = "No se ha obtenido resultados de la transaccion";
                    return BadRequest(objResponse);
                }
            }
            return Ok(objResponse);
        }
    }
}
