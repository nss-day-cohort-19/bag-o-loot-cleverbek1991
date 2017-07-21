using System;
using System.Collections.Generic;
using Xunit;

namespace BagOLoot.Tests
{
    public class SantasChildListShould
    {
        private readonly SantasChildList _santaslist;

        public SantasChildListShould()
        {
            _santaslist = new SantasChildList();
        }

        [Fact]
        public void GetSantasChildList()
        {
            // int toyId = _santaslist.GetToyList();
            int toyId = 32;
            List<string> child = _santaslist.GetChildList(toyId);

            Assert.IsType<List<string>>(child);
        }
    }
}