using System;
using System.Collections.Generic;
using System.Web.Http;
using VehicleApp.Proxies;
using VehicleDataApp.Contracts;
using VehicleDataApp.Contracts.Data;
using VehicleDataApp.Contracts.Extensions;

namespace VehicleApp.Web.Controllers
{
	[RoutePrefix("api/vehicles")]
	public class VehicleController : ApiController
	{
		private readonly IVehicleService _vehicleServiceClient;

		public VehicleController(IVehicleService vehicleServiceClient)
		{
			this._vehicleServiceClient = vehicleServiceClient;
		}

		// GET: Vehicle
		[HttpGet]
		[Route("")]
		public IEnumerable<VehicleData> GetVehicles()
		{
			return _vehicleServiceClient.GetAllVehicles();
		}

		[HttpPost]
		[Route("")]
		public VehicleData AddVehicle(VehicleData vehicleData)
		{
			vehicleData.Validate();

			vehicleData.Id = Guid.NewGuid();
			return _vehicleServiceClient.CreateVehicle(vehicleData);
		}

		[HttpPost]
		[Route("update")]
		public VehicleData UpdateVehicle(VehicleData vehicleData)
		{
			vehicleData.Validate();
			return _vehicleServiceClient.UpdateVehicle(vehicleData);
		}

		[HttpDelete]
		[Route("{id}")]
		public bool DeleteVehicle(Guid id)
		{
			return _vehicleServiceClient.DeleteVehicleById(id);
		}

	}
}