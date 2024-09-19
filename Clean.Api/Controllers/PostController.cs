using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : CrudController<Post, Post, Post, Guid>
    {
        public PostController(IPostRepository repository) : base(repository)
        {
        }
    }
}
