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
        // GET: api/<MascotasSolicitudAdopcionController>
        /* [HttpGet]
         public IEnumerable<string> Get()
         {
             return new string[] { "value1", "value2" };
         }

         // GET api/<MascotasSolicitudAdopcionController>/5
         [HttpGet("{id}")]
         public string Get(int id)
         {
             return "value";
         }

         // POST api/<MascotasSolicitudAdopcionController>
         [HttpPost]
         public void Post([FromBody] string value)
         {
         }

         // PUT api/<MascotasSolicitudAdopcionController>/5
         [HttpPut("{id}")]
         public void Put(int id, [FromBody] string value)
         {
         }

         // DELETE api/<MascotasSolicitudAdopcionController>/5
         [HttpDelete("{id}")]
         public void Delete(int id)
         {
         }*/

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<RespuestaSP>> SetAdoptarMascotas([FromBody] form_adopcion form_adopt)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(form_adopt);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPSetMascotas2, cadenaConexion, form_adopt.Transaccion, xmlParam.ToString());
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
    }
}
