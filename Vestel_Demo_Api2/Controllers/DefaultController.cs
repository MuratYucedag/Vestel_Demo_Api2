using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vestel_Demo_Api2.DAL;

namespace Vestel_Demo_Api2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCategories()
        {
            var context = new Context();
            return Ok(context.Categories.ToList());
        }
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var context = new Context();
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(category);
            }
        }
        [HttpPost]
        public IActionResult CategoryAdd(Category p)
        {
            var context = new Context();
            context.Add(p);
            context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult CategoryDelete(int id)
        {
            var context = new Context();
            var deletedCategory = context.Categories.Find(id);
            if (deletedCategory == null)
            {
                return NotFound();
            }
            else
            {
                context.Remove(deletedCategory);
                context.SaveChanges();
                return Ok();
            }
        }
        [HttpPut]
        public IActionResult UpdateCategory(Category p)
        {
            var context = new Context();
            var updatedCategory = context.Find<Category>(p.ID);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            else
            {
                updatedCategory.Name = p.Name;
                context.Update(updatedCategory);
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
