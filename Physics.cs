
namespace BluuMaths
{
    public static class Physics
    {
        public static double dispDueAcc(double initialPos, double initialVelocity, double acceleration, int timestep)
        {
            // x_0 + v_0*t + (0.5f)*a*square(t)
            return initialPos + initialVelocity * timestep + (0.5f) * acceleration * Trigonometry.square(timestep);
        }
    }
}
