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
    internal class AlgoServiceTests
    {
        [Test]
        public void DoubleSum_When_given_array_Then_return_double_summ_this_array()
        {
            //Arrange
            var algoService = new AlgoService();
            var arguments = new[] {1, 5, 6, -8};

            //Act
            var doubleSum = algoService.DoubleSum(arguments);

            //Assert
            Assert.That(doubleSum, Is.EqualTo(8));
        }

        [Test]
        public void MinValue_When_given_array_Then_return_min_element_this_array()
        {
            //Arrange
            var algoService = new AlgoService();
            var arguments = new[] {1, 5, 6, -8};

            //Act
            var min = algoService.MinValue(arguments);

            //Assert
            Assert.That(min, Is.EqualTo(-8));
        }

        [Test]
        public void Function_When_given_four_argument_Then_return_result_function()
        {
            //Arrange
            var algoService = new AlgoService();
            var arg1 = 2;
            var arg2 = 3;
            var arg3 = 3;
            var arg4 = 2.1;

            //Act
            var result = algoService.Function(arg1, arg2, arg3, arg4);

            //Assert
            Assert.That(result, Is.EqualTo(9.8196019072973471));
        }

        [Test]
        public void GetAverage_When_added_array_Then_return_average_value_this_array()
        {
            //Arrange
            var algoService = new AlgoService();
            var arguments = new[] {-10, 6, 4, -8};

            //Act
            var avarege = algoService.GetAverage(arguments);

            //Assert
            Assert.That(avarege, Is.EqualTo(-2));
        }

        [Test]
        public void Sqr_When_given_number_Then_return_sqr_this_number()
        {
            //Arrange
            var algoService = new AlgoService();
            var arguments = 5;

            //Act
            var avarege = algoService.Sqr(arguments);

            //Assert
            Assert.That(avarege, Is.EqualTo(25));
        }
    }
}
