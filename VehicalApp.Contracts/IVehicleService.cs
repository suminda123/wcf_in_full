using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using VehicleDataApp.Contracts.Data;

namespace VehicleDataApp.Contracts
{
	[ServiceContract]
	public interface IVehicleService
	{
		[OperationContract]
		IEnumerable<VehicleData> GetAllVehicles();

		[OperationContract]
		VehicleData CreateVehicle(VehicleData vehicleData);

		[OperationContract]
		VehicleData UpdateVehicle(VehicleData vehicleData);

		[OperationContract]
		bool DeleteVehicleById(Guid id);
	}
	// Use a data contract as illustrated in the sample below to add composite types to service operations.
	// You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "VehicleApp.Contracts.ContractType".
}
