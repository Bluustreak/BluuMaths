using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluuMaths.PointMassRelated
{
    internal static class PointmassPhysics
    {
        public static void moveAllPointmassesOneStepMT(ref List<Pointmass> everything)
        {            //return p.StartingVelocity.initVelX + initialVelocity * timestep + (0.5f) * acceleration * Trigonometry.square(timestep);
            int timestep = 1;
            for (int targetPointmass = 0; targetPointmass < everything.Count; targetPointmass++)
            {
                //var thread1 = new Thread(nudgeOnePointDueOthers(targetPointmass, ref everything, timestep));
                //send nudgeOnePointDueOthers(targetPointmass+1, ref everything, timestep) to thread 1
                //send nudgeOnePointDueOthers(targetPointmass+2, ref everything, timestep) to thread 2
                //send nudgeOnePointDueOthers(targetPointmass+3, ref everything, timestep) to thread 3
                //etc...

                nudgeOnePointDueOthers(targetPointmass, ref everything, timestep);
            }
        }
        public static void nudgeOnePointDueOthers(int targetPointmass, ref List<Pointmass> otherPointmasses, int timestep)
        {
            Pointmass p =  otherPointmasses[targetPointmass];
            double sumAccX = 0, sumAccY = 0;
            foreach (var other in otherPointmasses)
            {
                if(p != other)
                {
                    var cdist = cartDist(p, other);
                    var absD = absDist(p, other);
                    var absAcc = absAcceleration(p, other);

                    var xAcc = absAcc * (cdist.dx/absD);
                    var yAcc = absAcc * (cdist.dy/absD);

                    sumAccX += xAcc;
                    sumAccY += yAcc;
                }
            }
            (double locX, double locY) = p.Location;
            locX += sumAccX * timestep;
            locY += sumAccY * timestep;
            p.Location = (locX, locY);
        }

        public static double absAcceleration(Pointmass p, Pointmass p_other)
        {
            double G = 0.0000000000667408;
            var force = (p.Mass * p_other.Mass * G) / Trigonometry.square(absDist(p, p_other));
            return force/p.Mass;
        }
        public static double absDist(Pointmass p, Pointmass p_other)
        {
            double dx= p.Location.posX - p_other.Location.posX;
            double dy = p.Location.posY - p_other.Location.posY;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        public static (double dx, double dy) cartDist(Pointmass p, Pointmass p_other)
        {
            double dx = p.Location.posX - p_other.Location.posX;
            double dy = p.Location.posY - p_other.Location.posY;
            return (dx, dy);
        }

    }
}
