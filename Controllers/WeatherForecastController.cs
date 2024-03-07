using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using System.Data;

namespace ZoomanagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public WeatherForecastController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]



        public JsonResult Get()
        {
            try
            {
      
                string InsertDataTable = @"insert into gifts(Name , Price , Image) value ('Banana',5000,'hihi');insert into houses(Name ,onl , followers,num,Story,avatar,cover,video,species) values ('DogHouse',true,5,10,'hihi','haha','huhu','hoho','Dog'), ('CatHouse',true,5,10,'hihi','haha','huhu','hoho','Cat');";
                DataTable Table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                MySqlDataReader myReader;
                using (MySqlConnection myConnection = new MySqlConnection(sqlDataSource))
                {

                    myConnection.Open();

                    using (MySqlCommand myCommand = new MySqlCommand(InsertDataTable, myConnection))
                    {
                        myReader = myCommand.ExecuteReader();

                        Table.Load(myReader);
                        myReader.Close();
                        myConnection.Close();
                    }
                }
                
                return new JsonResult("insert data");
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }

        }
    }
}
