﻿namespace Entity.Concrete
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
