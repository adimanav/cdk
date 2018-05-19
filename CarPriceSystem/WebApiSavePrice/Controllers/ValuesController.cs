using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApiSavePrice.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]SalePrice value)
        {
        }
    }
}
