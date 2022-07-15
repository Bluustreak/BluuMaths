
namespace BluuMaths
{
    public static class Physics
    {
        public const double G = 0.0000000000667408;

        public static double dispDueAcc(double initialPos, double initialVelocity, double acceleration, int timestep)
        {
            // x_0 + v_0*t + (0.5f)*a*square(t)
            return initialPos + initialVelocity * timestep + (0.5f) * acceleration * Trigonometry.square(timestep);
        }

        public static double orbitalVelocity(double massOfParent, double radiusOfMotion)
        {
            return Math.Sqrt((G*massOfParent)/radiusOfMotion);
        }
    }
}
