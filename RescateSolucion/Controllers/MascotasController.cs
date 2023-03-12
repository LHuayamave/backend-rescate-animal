using Microsoft.AspNetCore.Mvc;
using ProyectoRescate.BL;
using RescateSolucion.CodeGeneral;
using System.Buffers.Text;
using System.Data;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RescateSolucion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotasController : Controller
    {
      
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<mascotas>> GetMascotas()
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = XDocument.Parse("<mascotas></mascotas>");
            Console.Write(NameStoredProcedure.SPGetMascotas + "\n\n" + cadenaConexion + "\n\n" + xmlParam.ToString());
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPGetMascotas, cadenaConexion, "CONSULTA_MASCOTAS_FOTO", xmlParam.ToString());
            List<mascotas> listData = new List<mascotas>();
            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        mascotas objResponse = new mascotas
                        {
                            id_mascotas = Convert.ToInt32(row["id_mascotas"]),
                            nombre = row["nombre"].ToString(),
                            id_tipo_mascota = Convert.ToInt32(row["id_tipo_mascota"]),
                            id_estado_salud = Convert.ToInt32(row["id_estado_salud"]),
                            id_estado_mascotas = Convert.ToInt32(row["id_estado_mascotas"]),
                            id_sexo = Convert.ToInt32(row["id_sexo"]),
                            foto = row["foto"].ToString(),
                            //foto = (byte[])row["foto"], //FOTO PRUEBA
                            tipo_mascota = new tipo_mascota()
                            {
                                descripcion = row["tipoMascota"].ToString()
                            },
                            estado_salud = new estado_salud()
                            {
                                descripcion = row["estadoSalud"].ToString()
                            },
                            estado_mascotas = new estado_mascotas()
                            {
                                descripcion = row["estadoMascota"].ToString()
                            },
                            sexo = new sexo()
                            {
                                descripcion = row["sexo"].ToString()
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
        public async Task<ActionResult<mascotas>> GetMascotaId(int id)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = XDocument.Parse("<mascotas><id_mascotas>"+id+"</id_mascotas></mascotas>");
            Console.Write(NameStoredProcedure.SPGetMascotas + "\n\n" + cadenaConexion + "\n\n" + xmlParam.ToString());
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPGetMascotas, cadenaConexion, "CONSULTA_MASCOTA_ID", xmlParam.ToString());
            List<mascotas> listData = new List<mascotas>();
            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        mascotas objResponse = new mascotas
                        {
                            id_mascotas = Convert.ToInt32(row["id_mascotas"]),
                            nombre = row["nombre"].ToString(),
                            tipo_mascota = new tipo_mascota()
                            {
                                descripcion = row["tipoMascota"].ToString()
                            },
                            estado_salud = new estado_salud()
                            {
                                descripcion = row["estadoSalud"].ToString()
                            },
                            estado_mascotas = new estado_mascotas()
                            {
                                descripcion = row["estadoMascota"].ToString()
                            },
                            sexo = new sexo()
                            {
                                descripcion = row["sexo"].ToString()
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
        public async Task<ActionResult<RespuestaSP>> SetMascotas([FromBody] mascotas mascotas)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(mascotas);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPSetMascotas, cadenaConexion, "INSERTAR_MASCOTA", xmlParam.ToString());
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
        public async Task<ActionResult<RespuestaSP>> PutMascota(int id,[FromBody] mascotas mascotas)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            mascotas.id_mascotas= id;
            XDocument xmlParam = DBXmlMethods.GetXml(mascotas);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoredProcedure.SPSetMascotas, cadenaConexion, "MODIFICAR_MASCOTA", xmlParam.ToString());
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
