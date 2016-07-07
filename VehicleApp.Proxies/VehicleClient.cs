using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using VehicleDataApp.Contracts;
using VehicleDataApp.Contracts.Data;

namespace VehicleApp.Proxies
{
	public class VehicleClient:ClientBase<IVehicleService>, IVehicleService
	{
	
		public IEnumerable<VehicleData> GetAllVehicles()
		{
			return Channel.GetAllVehicles();
		}

		public VehicleData CreateVehicle(VehicleData vehicleData)
		{
			return Channel.CreateVehicle(vehicleData);
		}

		public VehicleData UpdateVehicle(VehicleData vehicleData)
		{
			return Channel.UpdateVehicle(vehicleData);
		}

		public bool DeleteVehicleById(Guid id)
		{
			return Channel.DeleteVehicleById(id);
		}
	}
}
