using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluuMaths.PointMassRelated
{
    public class Pointmass
    {
        /*
        A pointmass will be more of a rock or a planet rather than a speck of dust or an atom

        properties: 
        mass
        location tuple
        startingVelocity tuple
        velocity tuple

        methods:
        updateLocation(), which checks all other particles in the system and lets itself be affected by them, causing a movement
        setNewLocation() because input parsing might be needed to avoid invalid values
        setNewVelocity() because input parsing might be needed to avoid invalid values

         */
        public double Mass { get; set; }
        public (double posX, double posY) Location { get; set; }
        public (double initVelX, double initVelY) StartingVelocity { get; set; }
        public (double velX, double velY) Velocity { get; set; }

        public Pointmass(double mass, (double posX, double posY) location, (double initVelX, double initVelY) startingVelocity)
        {
            this.Mass = mass;
            this.Location = location;
            this.StartingVelocity = startingVelocity;
            this.Velocity = startingVelocity;
        }
    }
}
