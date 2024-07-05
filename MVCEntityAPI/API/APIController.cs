using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace MVCEntityAPI.API
{
    public class APIController : ApiController
    {
        // GET: api/API
        Employee1Entities dbobj = new Employee1Entities();
        [HttpGet]
        [Route("api/API/getwebapitabs")]
        public IHttpActionResult Get()
        {
            return Ok(dbobj.WebApiTabs.ToList());
        }

        // GET: api/API/5
        [HttpGet]
        [Route("api/API/getwebapideatiltab/{id}")]
        public IHttpActionResult Get(int id)
        {
            WebApiTab employee = dbobj.WebApiTabs.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    

        // POST: api/API
        [HttpPost]
        [Route("api/API/postwebapitab")]
        public IHttpActionResult Post(WebApiTab obj)//(1)
        {
            if (ModelState.IsValid)
            {
                dbobj.WebApiTabs.Add(obj);// add deatils from (1)
                dbobj.SaveChanges();

            }
            else
            {
                return BadRequest(ModelState);
            }
            return Ok(200);
        }

        // PUT: api/API/5
        [HttpPut]
        [Route("api/API/putwebapitab/{id}")]
        public IHttpActionResult Put(int id, WebApiTab ob)
        {
            dbobj.Entry(ob).State = EntityState.Modified;
            try
            {
                dbobj.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/API/5
        [HttpDelete]
        [Route("api/API/deletewebapitab/{id}")]
        
        public IHttpActionResult Delete(int id)
        {

            WebApiTab webApiTab = dbobj.WebApiTabs.Find(id);
            if (webApiTab == null)
            {
                return NotFound();
            }
            dbobj.WebApiTabs.Remove(webApiTab);
            dbobj.SaveChanges();
            return Ok(webApiTab);


        }
    }
}
