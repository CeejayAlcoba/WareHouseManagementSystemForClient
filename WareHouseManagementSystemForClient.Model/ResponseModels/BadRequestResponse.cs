using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.ResponseModels
{
    public class BadRequestResponse
    {
        public object? Data { get;} = new object();
        public string? Message { get; set; }
        public int Status { get; } = 400;
    }
}

