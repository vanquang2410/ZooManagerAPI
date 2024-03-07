using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using ZoomanagerAPI.Models;

namespace ZoomanagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HousesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Houses>> Get()
        {
            try
            {
                string query = @"select Name , onl ,followers,num,Story,avatar,cover,video,species from houses;";

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

                List<Houses>ListHouse = new List<Houses>();
                foreach (DataRow row in Table.Rows)
                {
                    ListHouse.Add(new Houses
                    {
                        Name = row["Name"].ToString(),
                        onl = Convert.ToBoolean(row["onl"]),
                        followers = Convert.ToInt32(row["followers"]),
                        num = Convert.ToInt32(row["num"]),
                        story = row["story"].ToString(),
                        avatar = row["avatar"].ToString(),
                        cover = row["cover"].ToString(),
                        video = row["video"].ToString(),
                        species = row["species"].ToString()
                    });
                }
                  return Ok(ListHouse);
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }

        }
    }
}
