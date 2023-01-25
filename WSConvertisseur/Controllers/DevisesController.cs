using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WSConvertisseur.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    [Route("api/Devise")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        private List<Devise> listDevises;

        // GET: api/<DevisesController>
        [HttpGet]
        public IEnumerable<Devise> GetAll()
        {
            return ListDevises;

        }


        public DevisesController() {
            listDevises = new List<Devise>();
            listDevises.Add(new Devise(1, "Dollar", 1.08));
            listDevises.Add(new Devise(2, "Franc Suisse", 1.07));
            listDevises.Add(new Devise(3, "Yen", 120));
        }
       


        public List<Devise> ListDevises
        {
            get { return listDevises; }
            set { listDevises = value; }
        }


        /// <summary>
        /// Get a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // GET api/<DevisesController>/5
        [HttpGet("{id}", Name = "GetDevise")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Devise> GetById(int id)
        {
            Devise? devise = listDevises.FirstOrDefault((d) => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            return devise;
        }


        /// <summary>
        /// Delete a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is delete</response>
        /// <response code="404">When the currency id is not found</response>
        [HttpDelete("{id}", Name = "DeleteDevise")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Devise> DeleteById(int id)
        {
            Devise? devise = listDevises.FirstOrDefault((d) => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            listDevises.Remove(devise);
            return devise;
        }


        /// <summary>
        /// Create a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="devise">The currency you want to create</param>
        /// <response code="200">When the currency id is create</response>
        /// <response code="400">When the currency the model is not valid</response>
        // POST api/<DevisesController>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            listDevises.Add(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }



        /// <summary>
        /// Update a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <param name="devise">The currency you want to update</param>
        /// <response code="200">When the currency id is updated</response>
        /// <response code="404">When the currency id is not found</response>
        /// <response code="400">When the currency is not valid</response>
        // PUT api/<DevisesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != devise.Id)
            {
                return BadRequest();
            }
            int index = listDevises.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            listDevises[index] = devise;
            return NoContent();
        }


    }
}
