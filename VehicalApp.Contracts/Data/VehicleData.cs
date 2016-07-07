using System;
using System.Runtime.Serialization;

namespace VehicleDataApp.Contracts.Data
{
	[DataContract]
	public sealed class VehicleData
	{
		[DataMember]
		public Guid Id { get; set; }

		[DataMember]
		public string Make { get; set; }

		[DataMember]
		public string Description { get; set; }
	}
}
