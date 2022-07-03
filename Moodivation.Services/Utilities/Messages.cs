using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodivation.Services.Utilities
{
    public static class Messages
    {
        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Category not found.";
                return "No such category found.";
            }
            public static string Add(string Title)
            {
                return $"The category named {Title} has been successfully added.";
            }

            public static string Update(string Title)
            {
                return $"The category named {Title} has been successfully updated.";
            }
            public static string Delete(string Title)
            {
                return $"The category named {Title} has been successfully deleted.";
            }
        }

        public static class Product
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Product not found.";
                return "No such product found.";
            }
            public static string Add(string Title)
            {
                return $"The product named {Title} has been successfully added.";
            }

            public static string Update(string Title)
            {
                return $"The product named {Title} has been successfully updated.";
            }
            public static string Delete(string Title)
            {
                return $"The product named {Title} has been successfully deleted.";
            }
        }
    }
}
