using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using NUnit.Framework;

namespace ForTest
{
    [TestFixture]
    class MasterServiceTest
    {
        [Test]
        public void GetDoubleSum_When_create_empty_class_Then_throws_exception()
        {
            //Arrange
            var masterService = new MasterService(new AlgoService(), new DataService(3));

            //Act
            
            //Assert
            Assert.Throws<InvalidOperationException>(() => masterService.GetDoubleSum());
        }

        [Test]
        public void GetDoubleSum_When_executed_method_Then_return_double_sum_array()
        {
            //Arrange
            var dataService = new DataService(3);
            dataService.AddItem(3);
            dataService.AddItem(-2);
            dataService.AddItem(8);
            var masterService = new MasterService(new AlgoService(), dataService);

            //Act
            var avarege = masterService.GetDoubleSum();

            //Assert
            Assert.That(avarege, Is.EqualTo(18));
        }

        [Test]
        public void GetAverage_When_added_item_array_Then_return_average_value_this_array()
        {
            //Arrange
            var dataService  = new DataService(3);
            dataService.AddItem(3);
            dataService.AddItem(-2);
            dataService.AddItem(8);
            var masterService = new MasterService(new AlgoService(), dataService);

            //Act
            var avarege = masterService.GetAverage();

            //Assert
            Assert.That(avarege, Is.EqualTo(3));
        }

        [Test]
        public void GetMaxSquare_When_executed_method_Then_return_sqr_max_element()
        {
            //Arrange
            var dataService = new DataService(3);
            dataService.AddItem(3);
            dataService.AddItem(-2);
            dataService.AddItem(8);
            var masterService = new MasterService(new AlgoService(), dataService);

            //Act
            var avarege = masterService.GetMaxSquare();

            //Assert
            Assert.That(avarege, Is.EqualTo(64));
        }
    }
}
