using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDbApp.Model;
using MongoDbApp.Service;
using MongoDbApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserService _userService;

        public UserController(IDbSettings settings)
        {
            _userService = new UserService(settings);
        }
        //Tümünü Listeleme
        [HttpGet]
        public ActionResult<List<User>> GetAll() => _userService.GetAll();

        //Tek bir kullanıcıyı getirme
        [HttpGet("{id:length(24)}")]
        public ActionResult<User> Get(string id) => _userService.GetSingle(id);

        //Kullanıcı ekleme
        [HttpPost]
        public ActionResult<User> Add(User user) => _userService.Add(user);

        //Kullanıcıyı güncelleme.
        [HttpPut]
        public ActionResult Update(User currentUser)
        {
            var user = _userService.GetSingle(currentUser.Id);
            if (user == null)
                return NotFound();

            _userService.Update(currentUser);
            return Ok();
        }

        //Kullanıcıyı silme
        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            var user = _userService.GetSingle(id);

            if (user == null)
                return NotFound();

            _userService.Delete(id);
            return Ok();
        }
    }
}
