using System;
using System.Collections.Generic;
using Xunit;

namespace BagOLoot.Tests
{
    public class SantaHelperShould
    {
        private readonly SantaHelper _helper;

        public SantaHelperShould()
        {
            _helper = new SantaHelper();
        }

        // [Fact]

        // public void AddToyToChildsBag()
        // {
        //     string toyName = "Skateboard";
        //     int childId = 715;
        //     int toyId = _helper.AddToyToBag(toyName, childId);
        //     List<int> toys = _helper.GetChildsToys(childId);

        //     Assert.Contains(toyId, toys);
        // }

        // [Fact]
        // public void RemoveToyFromChildsBag()
        // {
        //     int toyId = 34;
        //     int childNameId = 134;
        //     _helper.RemoveToyFromBag(toyId, childNameId);
        //     List<int> toys = _helper.GetChildsToys(childNameId);

        //     Assert.DoesNotContain(toyId, toys);
        // }
    }
}