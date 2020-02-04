using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.beep
{
    public class RangeAttackState : EnemyBaseState
    {
        private Drone drone;
        public RangeAttackState(Drone _drone) : base(_drone.gameObject)
        {
            drone = _drone;
        }
        public override Type Tick()
        {
            
            return null;
        }
        
    }
}
