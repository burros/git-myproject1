using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Logic;

namespace ForTest
{
    [TestFixture]
    class DataServiceTest
    {
        [Test]
        public void AddItem_When_given_new_item_Then_return_count_increase()
        {
            //Arrange
            var dataService = new DataService(3);
            dataService.AddItem(5);
            dataService.AddItem(4);

            //Act
            var count = dataService.GetAllData().Count();

            //Assert
            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public void ItemsCount_When_given_new_item_Then_return_count_array()
        {
            //Arrange
            var dataService = new DataService(3);
            dataService.AddItem(5);
            dataService.AddItem(4);
            dataService.AddItem(2);

            //Act
            var count = dataService.ItemsCount;

            //Assert
            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public void GetElementAt_When_given_item_number_Then_return_element()
        {
            //Arrange
            var dataService = new DataService(3);
            dataService.AddItem(5);
            dataService.AddItem(4);
            dataService.AddItem(2);

            //Act
            var count = dataService.GetElementAt(2);

            //Assert
            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public void RemoveAt_When_given_item_number_Then_remove_this_element_from_array_Equal_next_element()
        {
            //Arrange
            var dataService = new DataService(3);
            dataService.AddItem(5);
            dataService.AddItem(4);
            dataService.AddItem(2);
            var secondElement = dataService.GetElementAt(2);

            //Act
            dataService.RemoveAt(1);

            //Assert
            Assert.That(secondElement, Is.EqualTo(dataService.GetElementAt(1)));
        }

        [Test]
        public void RemoveAt_When_given_item_number_Then_remove_this_element_from_array_Equal_count_decrease()
        {
            //Arrange
            var dataService = new DataService(3);
            dataService.AddItem(5);
            dataService.AddItem(4);
            dataService.AddItem(2);
            var initialCount = dataService.ItemsCount;
            
            //Act
            dataService.RemoveAt(1);

            //Assert
            Assert.That(dataService.ItemsCount, Is.EqualTo(initialCount - 1));
        }

        [Test]
        public void ClearAll_When_method_executed_Then_delete_all_element()
        {
            //Arrange
            var dataService = new DataService(3);
            dataService.AddItem(5);
            dataService.AddItem(4);
            dataService.AddItem(2);
            

            //Act
            dataService.ClearAll();

            //Assert
            Assert.That(dataService.ItemsCount, Is.EqualTo(0));
        }

        [Test]
        public void GetMax_When_method_executed_Then_return_max_value_from_array()
        {
            //Arrange
            var dataService = new DataService(3);
            dataService.AddItem(-5);
            dataService.AddItem(4);
            dataService.AddItem(2);


            //Act
            var max = dataService.GetMax();

            //Assert
            Assert.That(max, Is.EqualTo(4));
        }


    }
}
