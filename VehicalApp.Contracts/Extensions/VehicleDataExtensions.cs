using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDataApp.Contracts.Data;

namespace VehicleDataApp.Contracts.Extensions
{
	public static class VehicleDataExtensions
	{
		public static void Validate(this VehicleData vehicleData)
		{
			if (string.IsNullOrEmpty(vehicleData.Make))
				throw new InvalidOperationException("Make is required field");

			if (string.IsNullOrEmpty(vehicleData.Description))
				throw new InvalidOperationException("Description is required field");
		}
	}
}
