using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using parking;

namespace SerializationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test()
        {
            var dto = new AutoParkingDto
            {
                Filled = DateTime.Now,
                AutoName = "Nissan X-Trail",
                BodyType = Body.Кроссовер,
                AutoNumber = "В339КУ",
                ParkingNumber = 9,
                TimeOut = DateTime.Now,
                Price = 1499,
                Owner = new OwnerDto
                {
                    FullName = "Slastnikov Kirill",
                    PhoneNumber = "88005553535"
                },
                Passport = new PassportDto
                {
                    Series = "6688",
                    Number = "522433"
                }

            };
            var tempFileName = Path.GetTempFileName();
            try
            {
                AutoParkingHelper.WriteToFile(tempFileName, dto);
                var readDto = AutoParkingHelper.LoadFromFile(tempFileName);
                Assert.AreEqual(dto.Filled, readDto.Filled);
            }
            finally
            {
                File.Delete(tempFileName);
            }

        }
    }
}
