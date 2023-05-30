using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;
using WareHouseManagementSystemForClient.DbContext.Context;
using WareHouseManagementSystemForClient.Model.ReturnCargoModels;

namespace WareHouseManagementSystemForClient.Repositories.Repositories
{
    public class ReturnCargoRepository : IReturnCargoRepository
    {
        private readonly DapperContext _context;
        public ReturnCargoRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<(IEnumerable<ReturnCargo>,int)> GetReturnCargoByBookingId(int bookingId)
        {
            var procedureName = "GetReturnCargoByBookingId";
            var parameters = new DynamicParameters();
            parameters.Add("BookingId", bookingId, DbType.Int64, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var returnCargos = await connection.QueryAsync<ReturnCargo>(procedureName, parameters,
             commandType: CommandType.StoredProcedure);
                return (returnCargos.ToList(), returnCargos.Count());
            }
        }
    }
}
