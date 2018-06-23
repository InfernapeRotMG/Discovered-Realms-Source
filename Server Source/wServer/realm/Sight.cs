#region

using System;
using System.Collections.Generic;
using wServer.realm.entities.player;

#endregion

namespace wServer.realm
{
    internal static class Sight
    {
        private static readonly Dictionary<int, IntPoint[]> points = new Dictionary<int, IntPoint[]>();

        public static IntPoint[] GetSightCircle(int radius)
        {
            IntPoint[] ret;
            if (!points.TryGetValue(radius, out ret))
            {
                List<IntPoint> pts = new List<IntPoint>();
                for (int y = -radius; y <= radius; y++)
                    for (int x = -radius; x <= radius; x++)
                    {
                        if (x * x + y * y <= radius * radius)
                            pts.Add(new IntPoint(x, y));
                    }
                ret = points[radius] = pts.ToArray();
            }
            return ret;
        }

        public static IntPoint[] Cast(Player player, int radius = 15)
        {
            var ret = new List<IntPoint>();
            var angle = 0;
            while (angle < 360)
            {
                var distance = 0;
                while (distance < radius)
                {
                    var x = (int)(distance * Math.Cos(angle));
                    var y = (int)(distance * Math.Sin(angle));
                    if ((x * x + y * y) <= (radius * radius))
                    {

                        ret.Add(new IntPoint(x, y));

                        ObjectDesc desc;
                        player.Manager.GameData.ObjectDescs.TryGetValue(player.Owner.Map[(int)player.X + x, (int)player.Y + y].ObjType, out desc);
                        if (desc != null)
                            if (desc.BlocksSight)
                                break;

                        ret.Add(new IntPoint(x, y));
                    }
                    distance++;
                }
                angle++;
            }
            return ret.ToArray();
        }
    }
}