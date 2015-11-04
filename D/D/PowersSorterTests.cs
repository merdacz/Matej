namespace D
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using FluentAssertions;

    using Xunit;
    using Xunit.Abstractions;

    public class PowersSorterTests
    {
        private readonly ITestOutputHelper output;

        public PowersSorterTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void EmptySet_DoesNotFail()
        {
            var sut = new PowersSorter();
            sut.Sort(new Power[] { });
        }

        [Fact]
        public void SameValues_RemainIntact()
        {
            var input = GeneratePowers(100, () => 101, () => 101);
            var expected = (Power[])input.Clone();
            var sut = new PowersSorter();

            sut.Sort(input);

            input.Should().Equal(expected);
        }

        [Fact]
        public void SameBase_ExponentsGrow()
        {
            var random = new Random(12345);
            var input = GeneratePowers(100, () => 999, () => random.Next(100, 10000));
            var sut = new PowersSorter();

            sut.Sort(input);

            input.Select(x => x.Exponent).Should().BeInAscendingOrder();
        }

        [Fact]
        public void RandomSmallInput_VerifiesAccuratelly()
        {
            var random = new Random(12345);
            var input = GeneratePowers(100, () => random.Next(2, 8), () => random.Next(2, 8));
            var copy = input.ComputePowers().ToArray();
            Array.Sort(copy);

            var sut = new PowersSorter();
            sut.Sort(input);

            input.ComputePowers().ShouldAllBeEquivalentTo(copy);
        }


        [Fact]
        public void RandomMaxInput_PerformanceTests()
        {
            var random = new Random(12345);
            var input = GeneratePowers(10000, () => random.Next(100, 10000), () => random.Next(100, 10000));

            var sut = new PowersSorter();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            sut.Sort(input);
            stopwatch.Stop();

            this.output.WriteLine($"Max example took: {stopwatch.Elapsed.Milliseconds} ms. ");
        }

        private static Power[] GeneratePowers(int howMuch, Func<int> a, Func<int> b)
        {
            var list = new List<Power>();
            for (int i = 0; i < howMuch; i++)
            {
                list.Add(new Power(a(), b()));
            }
            return list.ToArray();
        }
    }
}
