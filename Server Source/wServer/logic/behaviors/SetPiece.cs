using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wServer.realm;
using wServer.realm.setpieces;

namespace wServer.logic.behaviors
{
    public class SetPiece : Behavior
    {
        private readonly string name;
        private readonly int newX;
        private readonly int newY;

        public SetPiece(string name, int newX, int newY)
        {
            this.name = name;
            this.newX = newX;
            this.newY = newY;
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            var piece = (ISetPiece)Activator.CreateInstance(Type.GetType(
                "wServer.realm.setpieces." + name, true, true));
            piece.RenderSetPiece(host.Owner, new IntPoint(newX, newY));
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state) { }
    }
}
