using Moodivation.Entities.Concrete;
using Moodivation.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodivation.Data.Abstract
{
    public interface IProductRepository: IEntityRepository<Product>
    {
    }
}
