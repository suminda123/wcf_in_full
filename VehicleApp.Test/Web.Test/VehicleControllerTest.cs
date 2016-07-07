using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using VehicleApp.Web.Controllers;
using VehicleDataApp.Contracts;
using VehicleDataApp.Contracts.Data;
using Xunit;

namespace VehicleApp.Test.Web.Test
{
	public class VehicleControllerTest
	{
		[Fact]
		public void Should_Throw_InvalidOperation_if_Make_Empty()
		{
			var moqVehicleServiceClient = new Mock<IVehicleService>();

			var vehicleController = new VehicleController(moqVehicleServiceClient.Object);

			var data = new VehicleData() {Id = Guid.NewGuid(), Description = "IST"};

			Assert.Throws<InvalidOperationException>(()=> vehicleController.AddVehicle(data));
		}

		[Fact]
		public void Should_Throw_InvalidOperation_if_Description_Empty()
		{
			var moqVehicleServiceClient = new Mock<IVehicleService>();

			var vehicleController = new VehicleController(moqVehicleServiceClient.Object);

			var data = new VehicleData() { Id = Guid.NewGuid(), Make = "Toyota" };

			Assert.Throws<InvalidOperationException>(() => vehicleController.AddVehicle(data));
		}

		[Fact]
		public void Should_AddVehicle()
		{
			//arrange
			var data = new VehicleData() { Id = Guid.NewGuid(), Make = "Toyota", Description = "IST" };
			var moqVehicleServiceClient = new Mock<IVehicleService>();
			moqVehicleServiceClient.Setup(x => x.CreateVehicle(data)).Returns(data);

			var vehicleController = new VehicleController(moqVehicleServiceClient.Object);

			//act
			var result = vehicleController.AddVehicle(data);

			//assert
			Assert.Equal(data.Id,result.Id);
		}
	}
}
