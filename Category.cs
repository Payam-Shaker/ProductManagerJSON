using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager
{
    public class Category
    {
        public string Title { get; set; }

        public Category(string title)
        {
            this.Title = title;
        }
    }
}
