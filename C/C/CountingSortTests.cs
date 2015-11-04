namespace C
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;

    using Xunit;
    using Xunit.Abstractions;

    public class CountingSortTests
    {
        private readonly ITestOutputHelper output;

        private static byte[] OrderedArray => new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        public CountingSortTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TooLongArray()
        {
            var input = new byte[byte.MaxValue + 1];
            var sut = new CountingSort(11);
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Sort(input));
        }

        [Fact]
        public void ArrayOfOnes()
        {
            var input = Enumerable.Repeat((byte)1, 11).ToArray();
            var sut = new CountingSort(11);
            var result = sut.Sort(input);
            Assert.Equal(input, result);
        }

        [Fact]
        public void ArrayOfOrderedValues()
        {
            var input = OrderedArray;
            var sut = new CountingSort(11);
            var result = sut.Sort(input);
            Assert.Equal(OrderedArray, result);
        }

        [Fact]
        public void ArrayOfReverseOrderValues()
        {
            var input = OrderedArray.Reverse().ToArray();
            var sut = new CountingSort(11);
            var result = sut.Sort(input);
            Assert.Equal(OrderedArray, result);
        }

        [Fact]
        public void ArrayOfRandomValues()
        {
            var random = new Random(12345);
            var input = new byte[255];
            random.NextBytes(input);
            var expected = (byte[])input.Clone();
            Array.Sort(expected);

            var sut = new CountingSort(byte.MaxValue);
            var result = sut.Sort(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ArrayWithDuplicatedValues()
        {
            var random = new Random(12345);
            var input = new byte[] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 6 };
            var expected = (byte[])input.Clone();
            Array.Sort(expected);

            var sut = new CountingSort(11);
            var result = sut.Sort(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ArrayWithManyBigValues()
        {
            var random = new Random(12345);
            var input = Enumerable.Repeat((byte)255, 255).ToArray();
            var expected = (byte[])input.Clone();
            Array.Sort(expected);

            var sut = new CountingSort(255);
            var result = sut.Sort(input);

            Assert.Equal(expected, result);
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

            this.output.WriteLine("Counting sort...");
            using (new MeassureTime(this.output))
            {
                for (int i = 0; i < numOfPasses; i++)
                {
                    var counting = new CountingSort(byte.MaxValue);
                    counting.Sort((byte[])input.Clone());
                }
            }
        }
    }
}