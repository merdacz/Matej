namespace C
{
    using System;
    using System.Linq;

    using Xunit;
    using Xunit.Abstractions;

    public class SortingNetwork11Tests
    {
        private readonly ITestOutputHelper output;

        private static byte[] OrderedArray => new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        public SortingNetwork11Tests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TooShortArray()
        {
            var input = new byte[10];
            var sut = new SortingNetwork11();
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Sort(input));
        }

        [Fact]
        public void TooLongArray()
        {
            var input = new byte[12];
            var sut = new SortingNetwork11();
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Sort(input));
        }

        [Fact]
        public void ArrayOfOnes()
        {
            var input = Enumerable.Repeat((byte)1, 11).ToArray();
            var expected = (byte[])input.Clone();
            var sut = new SortingNetwork11();

            sut.Sort(input);

            Assert.Equal(expected, input);
        }

        [Fact]
        public void ArrayOfOrderedValues()
        {
            var input = (byte[])OrderedArray.Clone();
            var sut = new SortingNetwork11();
            sut.Sort(input);
            Assert.Equal(OrderedArray, input);
        }

        [Fact]
        public void ArrayOfReverseOrderValues()
        {
            var input = OrderedArray.Reverse().ToArray();
            var sut = new SortingNetwork11();
            sut.Sort(input);
            Assert.Equal(OrderedArray, input);
        }

        [Fact]
        public void ArrayOfRandomValues()
        {
            var random = new Random(12345);
            var input = new byte[11];
            random.NextBytes(input);
            var expected = (byte[])input.Clone();
            Array.Sort(expected);

            var sut = new SortingNetwork11();
            sut.Sort(input);

            Assert.Equal(expected, input);
        }

        [Fact]
        public void ArrayWithDuplicatedValues()
        {
            var random = new Random(12345);
            var input = new byte[] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 6 };
            var expected = (byte[])input.Clone();
            Array.Sort(expected);

            var sut = new SortingNetwork11();
            sut.Sort(input);

            Assert.Equal(expected, input);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        [InlineData(100000)]
        [InlineData(1000000)]
        [InlineData(10000000)]
        public void PerformanceTest(int numOfPasses)
        {
            var random = new Random(12345);
            var input = new byte[11];
            random.NextBytes(input);

            this.output.WriteLine("Sorting network...");
            using (new MeassureTime(this.output))
            {
                for (int i = 0; i < numOfPasses; i++)
                {
                    var network = new SortingNetwork11();
                    network.Sort((byte[])input.Clone());
                }
            }
        }
    }
}