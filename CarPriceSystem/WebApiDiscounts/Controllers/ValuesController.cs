using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApiDiscounts.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values/5
        [HttpGet("{id}")]
        public Discount Get(int id)
        {
            return new Discount
            {
                ID = id,
                DiscountPercent = 11.95
            };
        }
    }
}
