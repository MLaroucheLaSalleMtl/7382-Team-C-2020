using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.beep
{
    class Boss0RangeAttackState : RangeAttackState
    {
        public Boss0RangeAttackState(Drone _drone) : base(_drone)
        {
        }

        //public Boss0RangeAttackState(Drone _drone) : base(_drone)
        //{

        //}

        //public override Type Tick()
        //{
        //    return base.Tick();
        //}
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Type Tick()
        {
            return base.Tick();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
