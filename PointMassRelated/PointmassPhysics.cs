namespace BluuMaths.PointMassRelated
{
    public static class PointmassPhysics
    {
        public static void moveAllPointmassesOneStepMT(ref List<Pointmass> everything, double timestep)
        {           
            for (int targetPointmass = 0; targetPointmass < everything.Count; targetPointmass++)
            {
                //the idea here was to make it multithreaded, but that turned out to be really weird in C#
                //furthermore, making this threaddable may actually be WORSE in performance  due to overhead
                //send nudgeOnePointDueOthers(targetPointmass, ref everything, timestep) to thread 1
                //send nudgeOnePointDueOthers(targetPointmass+1, ref everything, timestep) to thread 2
                //send nudgeOnePointDueOthers(targetPointmass+2, ref everything, timestep) to thread 3
                //etc...


                nudgeOnePointDueOthers(targetPointmass, ref everything, timestep);
            }
        }
        public static void nudgeOnePointDueOthers(int targetPointmass, ref List<Pointmass> otherPointmasses, double timestep)
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

            // update the new location based on speed and acceleration
            double timestepSquare = Trigonometry.square(timestep);
            (double locX, double locY) = p.Location;
            locX +=  p.Velocity.velX * timestep + (0.5f) * sumAccX * timestepSquare;
            locY +=  p.Velocity.velY * timestep + (0.5f) * sumAccY * timestepSquare;
            //locX += sumAccX * timestep;
            //locY += sumAccY * timestep;
            p.Location = (locX, locY);
            p.PositionHistoryX.Add(locX );
            p.PositionHistoryY.Add(locY );

            // update the new speed based on acceleration over time
            (double velX, double velY) = p.Velocity;
            velX = velX + sumAccX * timestep;
            velY = velY + sumAccY * timestep;
            p.Velocity = (velX, velY);
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
            return (-dx, -dy);
        }

    }
}
