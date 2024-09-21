﻿using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Bases.Data;
using Infrastructure.Bases.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
