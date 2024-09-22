using Api.Bases.Controllers;
using Application.Bases.Interfaces.IServices;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : CrudController<Post, Post, Post>
    {
        public PostController(ICrudService<Post, Post, Post> crudService) : base(crudService)
        {
        }
    }
}
