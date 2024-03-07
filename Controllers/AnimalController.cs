using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using System.Data;

namespace ZoomanagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AnimalController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
            public JsonResult Get() {
                try
                {
                    string query = @"select Name , NameAnimal ,Age from Animals;";

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
                string jsonResult = JsonConvert.SerializeObject(Table);
                return new JsonResult(jsonResult);
                }
                catch (Exception e)
                {
                    return new JsonResult(e.Message);
                }
            
            }
    }
}
    