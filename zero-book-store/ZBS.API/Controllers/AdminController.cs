using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ZBS.Application.Services.BookServ;
using ZBS.Application.Services.BookServ.Dtoes;
using ZBS.Application.Services.RoleControlServ;
using ZBS.Application.Services.RoleControlServ.Dtoes;
using ZBS.Application.Services.UserServ;
using ZBS.Infrastructure.Models.Enum;
using ZBS.Infrastructure.Repositories.Books.CRUDmodels;
using ZBS.Infrastructure.Repositories.Orders;
using ZBS.Infrastructure.Repositories.User;

namespace ZBS.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller, BaseController
    {
        private IBookService _bookService;
        private IOrderRepository _orderRepository;
        private IRoleControlService _roleControlService;
        private IUserService _userService;
        private IUserRepository _userRepository;

        public AdminController(IBookService bookService, IUserService userService,
            IOrderRepository orderRepository, IRoleControlService roleControlService,
            IUserRepository userRepository)
        {
            _bookService = bookService;
            _orderRepository = orderRepository;
            _roleControlService = roleControlService;
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpPost("Book/Create")]
        public async Task<ActionResult> CreateBook(CreateBookWithAuthorDto createBookWithAuthorDto)
        {
            if (createBookWithAuthorDto != null)
            {
                await _bookService.Create(createBookWithAuthorDto);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("Book/Delete")]
        public async Task<ActionResult> DeleteBookbyId( int id)
        {
            if (id != 0 && id!= -1)
            {
                await _bookService.DeleteBook(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("Book/EditQuantity")]
        public async Task<ActionResult> EditBookQuantity(EditBookQuantityDto editBookQuantityDto)
        {
            if (editBookQuantityDto != null)
            {
                await _bookService.EditBookQuantity(editBookQuantityDto);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        
        [HttpGet("Book/{id}")]
        public async Task<ActionResult<GetBookModel>> GetBookById([Required] int Id)
        {
            if (Id != 0 && Id != -1)
            {
                var book = await _bookService.GetBookById(Id);
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("User/GelAll")]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetAllUsers(int currentPageNumber, int pageSize)
        {
            return Ok(await _userRepository.GetAllUsersAsync(currentPageNumber, pageSize));
        }

        [HttpGet("User/{id}")]
        public async Task<ActionResult<UserEntity>> GetUserById(int id)
        {
            if(id != 0 && id!= -1) { 
            return Ok(await _userService.GetUserByIdAsync(id));
            }else
            {
                return NotFound();
            }
        }

        [HttpDelete("User/{id}")]
        public async Task<ActionResult> DeleteUserById( int id)
        {
            
                if (id != 0 && id != -1)
                {
                return Ok(await _userService.DeleteUserByIdAsync(id));
                }
                else
                {
                return NotFound();
                }
        }

        [HttpPut("User/UpdateRole")]
        public async Task<RoleUpdateDetailsDto> UpdateUserRole(int Id, Infrastructure.Models.Enum.Role role)
        {
            return await _roleControlService.UpdateUserRoleAsync(Id, role);
        }
    
        [HttpGet("Order/{id}")]
        public async Task<ActionResult<IEnumerable<OrderEntity>>> GetOrdersByUserId(int id)
        {
            if (id != 0 && id !=-1) { 
            return Ok(await _orderRepository.GetByUserIdAsync(id));
            }
            else
            {
                return NotFound();
            }
        }

        
    }
}
