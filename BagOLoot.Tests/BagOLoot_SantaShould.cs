using System;
using System.Collections.Generic;
using Xunit;

namespace BagOLoot.Tests
{
    public class SantaShould
    {
        private readonly Santa _santa;

        public SantaShould()
        {
            _santa = new Santa();
        }

        [Fact]

        public void GetChildsToyList()
        {
            int childId = 321;
            List<string> toys = _santa.GetChildsToys(childId);

            Assert.IsType<List<string>>(toys);
        }

    }
}