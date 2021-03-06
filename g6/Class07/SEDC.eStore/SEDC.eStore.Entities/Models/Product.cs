﻿using SEDC.eStore.Entities.BaseModels;
using SEDC.eStore.Entities.Enums;

namespace SEDC.eStore.Entities.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Category Category { get; set; }

        public Product()
            : base()
        {
        }

        public string Info()
        {
            return $"Id: {Id}, Name: {Name}, Price: {Price} MKD";
        }
    }
}