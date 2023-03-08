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
    public class FormRescateAnimalController : ControllerBase
    {
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<RespuestaSP>> SetFormRescateAnimal([FromBody] Form_rescate_animal Form_rescate_animal)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(Form_rescate_animal);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPSetRescateAnimal, cadenaConexion, "INSERTAR_FORMULARIO", xmlParam.ToString());
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
