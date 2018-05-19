
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApiVehicle.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Vehicle> Get()
        {
            return new Vehicle[]
            {
                new Vehicle
                {
                    ID = 1,
                    Make = "Nissan",
                    Model = "GTR",
                    Year = "2018",
                    Trim = "Sports",
                    msrp = 69123.26
                }
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Vehicle Get(int id)
        {
            return new Vehicle
            {
                ID = id,
                Make = "Nissan",
                Model = "GTR",
                Year = "2018",
                Trim = "Sports",
                msrp = 69123.26
            };
        }
    }
}
