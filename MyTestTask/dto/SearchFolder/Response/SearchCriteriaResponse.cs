﻿namespace MyTestTask.dto.SearchFolder.Response
{
    public class SearchCriteriaResponse
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Number { get; set; }
        public string? City { get; set; }
        public string? Category { get; set; }

        public string? Subcategory { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset PublicationDate { get; set; }
    }
}