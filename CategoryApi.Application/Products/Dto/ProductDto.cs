﻿namespace CategoryApi.Application.Products.Dto
{
    public class ProductDto
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}