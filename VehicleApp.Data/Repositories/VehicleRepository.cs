using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using VehicleApp.Data.Entities;

namespace VehicleApp.Data.Repositories
{
	public sealed class VehicleRepository : IVehicleRepository
	{
		public IEnumerable<Vehicle> GetAllVehicles()
		{
			//todo add paging
			IEnumerable<Vehicle> results;
			using (var cn = DbConnection)
			{
				cn.Open();
				results = cn.Query<Vehicle>("spGetAllVehicles",
					commandType: CommandType.StoredProcedure).AsList();
			}
			return results;
		}

		public Vehicle CreateVehicle(Vehicle vehicle)
		{
			Vehicle result;
			using (var cn = DbConnection)
			{
				cn.Open();
				result = cn.Query<Vehicle>("spCreateVehicle",
					vehicle,
					commandType: CommandType.StoredProcedure).FirstOrDefault();
			}
			return result;
		}

		public Vehicle UpdateVehicle(Vehicle vehicle)
		{
			Vehicle result;
			using (var cn = DbConnection)
			{
				cn.Open();
				result = cn.Query<Vehicle>("spUpdateVehicle",
					vehicle,
					commandType: CommandType.StoredProcedure).FirstOrDefault();
			}
			return result;
		}

		public int DeleteVehicle(Guid id)
		{
			int result;
			using (var cn = DbConnection)
			{
				cn.Open();
				result = cn.Query<int>("spDeleteVehicle",
					new {Id=id},
					commandType: CommandType.StoredProcedure).FirstOrDefault();
			}
			return result;

		}

		private IDbConnection DbConnection
		{
			get { return new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString); }
		}
	}
}
