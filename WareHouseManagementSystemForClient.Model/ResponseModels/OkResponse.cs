using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseManagementSystemForClient.Model.ResponseModels
{
    public class OkResponse
    {
        public object? Data { get; set; } = new object();
        public string Message { get; set; } = "Ok";
        public int Status { get; } = 200;
    }
}
