﻿using Moodivation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodivation.Entities.Dtos
{
    public class CategoryListDto
    {
        public IList<Category> Categories { get; set; }
    }
}
