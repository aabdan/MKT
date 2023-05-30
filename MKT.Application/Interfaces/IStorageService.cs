using MKT.Application.Infrastructure.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKT.Application.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadResourceAsync(Resource resource);
        Task<string> UploadResourceAsync(InvoiceResource invoiceResource);
    }
}
