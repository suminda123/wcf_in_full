using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Data.Entities
{
	public sealed class Vehicle
	{
		public Guid Id { get; set; }
		public string Make { get; set; }
		public string Description { get; set; }
	}
}
