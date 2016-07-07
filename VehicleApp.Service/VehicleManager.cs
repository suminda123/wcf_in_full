using System;
using System.Collections.Generic;
using System.Linq;
using VehicleApp.Data.Entities;
using VehicleApp.Data.Repositories;
using VehicleDataApp.Contracts;
using VehicleDataApp.Contracts.Data;
using VehicleDataApp.Contracts.Extensions;

namespace VehicleApp.Services
{
	public class VehicleManager : IVehicleService
	{
		//todo add autofac, so service can unit testable
		public IEnumerable<VehicleData> GetAllVehicles()
		{
			var vehicles = new List<VehicleData>();
			var vehicleRepository = new VehicleRepository();

			//can use automapper to map this data
			vehicleRepository.GetAllVehicles()
				.ToList()
				.ForEach(vehicle =>
				{
					vehicles.Add(
						new VehicleData
						{
							Id = vehicle.Id,
							Make = vehicle.Make,
							Description = vehicle.Description
						});
				}
				);

			return vehicles;
		}

		public VehicleData CreateVehicle(VehicleData vehicleData)
		{
			vehicleData.Validate();

			var vehicleRepository = new VehicleRepository();
			var result = vehicleRepository.CreateVehicle(
				new Vehicle
				{
					Id = vehicleData.Id,
					Make = vehicleData.Make,
					Description = vehicleData.Description
				}
				);

			return vehicleData;
		}

		public VehicleData UpdateVehicle(VehicleData vehicleData)
		{
			vehicleData.Validate();

			var vehicleRepository = new VehicleRepository();
			var result = vehicleRepository.UpdateVehicle(
				new Vehicle
				{
					Id = vehicleData.Id,
					Make = vehicleData.Make,
					Description = vehicleData.Description
				}
				);

			return vehicleData;
		}

		public bool DeleteVehicleById(Guid id)
		{
			var vehicleRepository = new VehicleRepository();
			return vehicleRepository.DeleteVehicle(id) > 0;
		}
	}
}
