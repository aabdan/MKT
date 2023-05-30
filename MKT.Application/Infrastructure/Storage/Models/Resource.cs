using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;


namespace MKT.Application.Infrastructure.Storage.Models
{
    public class Resource
    {
        public IFormFile File { get; set; }
        public ResourceType ResourceType { get; set; }

    }

    public class InvoiceResource
    {
        public byte[]? Content { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public ResourceType ResourceType { get; set; }
    }

    public enum ResourceType
    {
        MenuItem,
        Offer,
        Article,
        Invoice,
        BuildAppGuid
    }
}
