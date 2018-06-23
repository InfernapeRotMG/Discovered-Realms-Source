#region

using wServer.realm;
using wServer.realm.entities;

#endregion

namespace wServer.logic.behaviors
{
    public class SwapTexture : Behavior
    {
        private readonly int index1;
        private readonly int index2;
        protected Cooldown coolDown;

        public SwapTexture(int index1, int index2, Cooldown coolDown = new Cooldown())
        {
            this.index1 = index1;
            this.index2 = index2;
            this.coolDown = coolDown.Normalize();
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            state = 0;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            int cool = (int)state;

            if (cool <= 0)
            {
                if ((host as Enemy).AltTextureIndex != index1)
                {
                    (host as Enemy).AltTextureIndex = index1;
                    host.UpdateCount++;
                }
                else
                {
                    (host as Enemy).AltTextureIndex = index2;
                    host.UpdateCount++;
                }
                cool = coolDown.Next(Random);
            }
            else
                cool -= time.thisTickTimes;

            state = cool;
        }
    }
}