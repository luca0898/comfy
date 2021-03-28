using AutoMapper;
using Comfy.Product.Contracts.Services;
using Comfy.Product.Entities;
using Comfy.Product.ViewModel;
using Comfy.SystemObjects.Attributes;
using Comfy.SystemObjects.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.API.Controllers
{
    [Route("v1/user"), BearerAuthorize("Authenticated")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All users
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        [SwaggerOperation(OperationId = "{entity}GetAll")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int skip, [FromQuery] int take, CancellationToken cancellationToken)
        {
            take = (take <= 0) ? 50 : take;

            IEnumerable<User> user = await _userService.FindAllAsync(cancellationToken, skip, take);

            IEnumerable<UserViewModel> viewSchedules = _mapper.Map<IEnumerable<UserViewModel>>(user);

            return Ok(new SuccessResponseViewModel<IEnumerable<UserViewModel>>(viewSchedules));
        }

        /// <summary>
        /// Get one user
        /// </summary>
        /// <param name="id">user identifier</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The user record</returns>
        [HttpGet, Route("{id}")]
        [SwaggerOperation(OperationId = "{entity}GetById")]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            User user = await _userService.GetOneAsync(id, cancellationToken);

            UserViewModel userView = _mapper.Map<UserViewModel>(user);

            return Ok(new SuccessResponseViewModel<UserViewModel>(userView));
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="model">User item</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public async Task<IActionResult> CreateAsync([FromBody] UserViewModel model, CancellationToken cancellationToken)
        {
            User entity = _mapper.Map<User>(model);

            User newly = await _userService.CreateAsync(entity, cancellationToken);

            UserViewModel result = _mapper.Map<UserViewModel>(newly);

            return Ok(new SuccessResponseViewModel<UserViewModel>(result));
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="model">User item</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut, Route("")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserViewModel model, CancellationToken cancellationToken)
        {
            User entity = _mapper.Map<User>(model);

            User updated = await _userService.UpdateAsync(entity, cancellationToken);

            UserViewModel result = _mapper.Map<UserViewModel>(updated);

            return Ok(new SuccessResponseViewModel<UserViewModel>(result));
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">user key</param>
        /// <param name="cancellationToken"></param>
        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _userService.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
