namespace D
{
    public class Power
    {
        public Power(int @base, int exponent)
        {
            this.Base = @base;
            this.Exponent = exponent;
        }

        public int Base { get; }

        public int Exponent { get; }

        public override bool Equals(object obj)
        {
            var target = obj as Power;
            if (target == null)
            {
                return false;
            }

            return this.Base == target.Base && this.Exponent == target.Exponent;
        }

        public override int GetHashCode()
        {
            return this.Base + 37 * this.Exponent;
        }

        public override string ToString()
        {
            return $"{this.Base}^{this.Exponent}";
        }
    }
}