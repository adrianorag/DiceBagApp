using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Domain.Entities;
using WebApi.Domain.Services.Interfaces;

namespace WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userAppService;

        public UsersController(IUserService userAppService)
        {
            _userAppService = userAppService;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userAppService.GetAll();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            User user = _userAppService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserID)
            {
                return BadRequest();
            }
            
            try
            {
                _userAppService.Update(user);
            }
            catch (Exception ex)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        [HttpPost]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.DateRegister = DateTime.Now;
            user.DateRegisterLastUpdate = DateTime.Now;


            _userAppService.Add(user);

            return CreatedAtRoute("DefaultApi", new { id = user.UserID }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = _userAppService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userAppService.Remove(user);

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userAppService.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return _userAppService.GetById(id) != null;
        }
    }
}