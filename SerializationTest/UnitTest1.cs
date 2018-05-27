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
                Type = Body.Кроссовер,
                AutoNumber = "В339КУ",
                ParkingNumber = 9,
                TimeOut = DateTime.Now,
                Price = 1499,

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
