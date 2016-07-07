using System;
using System.Collections.Generic;
using VehicleApp.Data.Entities;

namespace VehicleApp.Data.Repositories
{
	public interface IVehicleRepository
	{
		IEnumerable<Vehicle> GetAllVehicles();
		Vehicle CreateVehicle(Vehicle vehicle);
		Vehicle UpdateVehicle(Vehicle vehicle);
		int DeleteVehicle(Guid id);
	}
}