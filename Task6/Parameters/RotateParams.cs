using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Interfaces;

namespace Task6.Parameters
{
    public class RotateParams
    {
        public readonly Vector3 Direction;
        public readonly float Angle;
        public RotateParams(float angle, Vector3 direction)
        {
            Direction = direction;
            Angle = angle;
        }

        public RotateParams GetParameters()
        {
            return this;
        }
    }
}
