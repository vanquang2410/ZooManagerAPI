using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Linq;
using ZoomanagerAPI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ZoomanagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GiftController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public ActionResult<IEnumerable<Houses>> Get()
        {
            try
            {
                string query = @"select Name , Price ,Image from gifts;";

                DataTable Table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                MySqlDataReader myReader;
                using (MySqlConnection myConnection = new MySqlConnection(sqlDataSource))
                {

                    myConnection.Open();

                    using (MySqlCommand myCommand = new MySqlCommand(query, myConnection))
                    {
                        myReader = myCommand.ExecuteReader();

                        Table.Load(myReader);
                        myReader.Close();
                        myConnection.Close();
                    }
                }


                List<Gift> giftList= new List<Gift>();
                foreach (DataRow row in Table.Rows)
                {
                    giftList.Add(new Gift
                    {
                        Name = row["Name"].ToString(),
                        Price = Convert.ToInt32(row["Price"]),
                        Image = row["Image"].ToString()
                    });
                }
                return Ok(giftList);
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }

        }
    }
}
