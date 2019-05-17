using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabII.Models;
using LabII.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private IExpenseService expenseService;
        public ExpensesController(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
        }

        // GET: api/Expenses
        [HttpGet]
        public IEnumerable<Expense> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to, [FromQuery]Models.Type? type)
        {
            return expenseService.GetAll(from, to, type);
        }

        // GET: api/Expenses/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var found = expenseService.GetById(id);
            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }


        // POST: api/Expenses
        [HttpPost]
        public void Post([FromBody] Expense expense)
        {
            expenseService.Create(expense);
        }



        // PUT: api/Expenses/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Expense expense)
        {

            var result = expenseService.Upsert(id, expense);
            return Ok(result);
        }



        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = expenseService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}