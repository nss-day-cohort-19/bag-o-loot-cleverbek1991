using System;
using System.Collections.Generic;
using Xunit;

namespace BagOLoot.Tests
{
    public class DeliveryReportShould
    {
        private readonly DeliveryReport _report;

        public DeliveryReportShould()
        {
            _report = new DeliveryReport();
        }

        [Fact]
        public void GetDeliveyReport()
        {
            int childId = 54;
            bool GetReport = _report.Delivered(childId);

            Assert.True(GetReport);
        }
    }
}