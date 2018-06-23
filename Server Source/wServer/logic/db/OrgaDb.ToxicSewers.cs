#region

using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

#endregion

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ ToxicSewers = () => Behav()
        .Init("ds goblin sorcerer",
            new State(
            new Wander(0.3),
                new State("idle",
                    new Shoot(6, 5, 12, 0, coolDown: 1500),
                    new Grenade(3, 50, 6, coolDown: 1800)
                    )
                )
            )
        .Init("DS Boss Minion",
            new State(
                new State("idle",
                    new Wander(0.6),
                    new Grenade(3, 50, 10, coolDown: 5000)
                    )
                )
            )
        .Init("DS Gulpord the Slime God M",
            new State(
                new State("idle",
                    new Orbit(0.6, 3, target: "ds gulpord the slime god"),
                    new Shoot(10, 8, 45, 1, coolDown: 1500),
                    new Shoot(10, 4, 60, 0, coolDown: 4500)
                    )
                )
            )
        .Init("ds gulpord the slime god s",
            new State(
                new State("idle",
                    new Orbit(0.6, 3, target: "ds gulpord the slime god"),
                    new Shoot(10, 4, 20, 1, coolDown: 2000)
                    )
                )
            )
        .Init("ds Gulpord the Slime God",
            new State(
                new RealmPortalDrop(),
                new State("idle",
                    new PlayerWithinTransition(12, "begin")
                    ),
                new State("begin",
                    new TimedTransition(500, "shoot")
                    ),
                new State("shoot",
                    new HpLessTransition(0.90, "randomshooting"),
                    new Shoot(10, 8, 45, 1, coolDown: 2000),
                    new Shoot(10, 5, 72, 0, 0, coolDown: 1000),
                    new Shoot(10, 5, 72, 0, 6, coolDown: 1000),
                    new TimedTransition(400, "shoot1")
                    ),
                new State("shoot1",
                    new HpLessTransition(0.90, "randomshooting"),
                    new Shoot(10, 5, 72, 0, 15, coolDown: 1000),
                    new Shoot(10, 5, 72, 0, 21, coolDown: 1000),
                    new TimedTransition(400, "shoot2")
                    ),
                new State("shoot2",
                    new HpLessTransition(0.90, "randomshooting"),
                    new Shoot(10, 5, 72, 0, 30, coolDown: 1000),
                    new Shoot(10, 5, 72, 0, 36, coolDown: 1000),
                    new TimedTransition(400, "shoot3")
                    ),
                new State("shoot3",
                    new HpLessTransition(0.90, "randomshooting"),
                    new Shoot(10, 5, 72, 0, 45, coolDown: 1000),
                    new Shoot(10, 5, 72, 0, 51, coolDown: 1000),
                    new TimedTransition(400, "shoot4")
                    ),
                new State("shoot4",
                    new HpLessTransition(0.90, "randomshooting"),
                    new Shoot(10, 5, 72, 0, 60, coolDown: 1000),
                    new Shoot(10, 5, 72, 0, 66, coolDown: 1000),
                    new TimedTransition(400, "shoot")
                    ),
                new State("randomshooting",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ReturnToSpawn(true, 1),
                    new TimedTransition(1500, "randomshooting1")
                    ),
                new State("randomshooting1",
                    new HpLessTransition(0.70, "tossnoobs"),
                    new Shoot(10, 8, 45, 1, coolDown: 2000),
                    new Shoot(10, 9, 40, 0, 0, coolDown: 1000),
                    new TimedTransition(200, "randomshooting2")
                    ),
                new State("randomshooting2",
                    new HpLessTransition(0.70, "tossnoobs"),
                    new Shoot(10, 9, 40, 0, 30, coolDown: 1000),
                    new TimedTransition(200, "randomshooting3")
                    ),
                new State("randomshooting3",
                    new HpLessTransition(0.70, "tossnoobs"),
                    new Shoot(10, 9, 40, 0, 3, coolDown: 1000),
                    new TimedTransition(200, "randomshooting4")
                    ),
                new State("randomshooting4",
                    new HpLessTransition(0.70, "tossnoobs"),
                    new Shoot(10, 9, 40, 0, 81, coolDown: 1000),
                    new TimedTransition(200, "randomshooting5")
                    ),
                new State("randomshooting5",
                    new HpLessTransition(0.70, "tossnoobs"),
                    new Shoot(10, 9, 40, 0, 250, coolDown: 1000),
                    new TimedTransition(200, "randomshooting6")
                    ),
                new State("randomshooting6",
                    new HpLessTransition(0.70, "tossnoobs"),
                    new Shoot(10, 9, 40, 0, 172, coolDown: 1000),
                    new TimedTransition(200, "randomshooting7")
                    ),
                new State("randomshooting7",
                    new HpLessTransition(0.70, "tossnoobs"),
                    new Shoot(10, 9, 40, 0, 183, coolDown: 1000),
                    new TimedTransition(200, "randomshooting1")
                    ),
                new State("tossnoobs",
                    new TossObject("DS Boss Minion", 3, 0, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 45, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 90, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 135, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 180, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 225, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 270, coolDown: 99999999, randomToss: false),
                    new TossObject("DS Boss Minion", 3, 315, coolDown: 99999999, randomToss: false),
                    new TimedTransition(100, "derp")
                    ),
                new State("derp",
                    new HpLessTransition(0.50, "baibaiscrubs"),
                    new Shoot(10, 6, 12, 0, coolDown: 3000),
                    new Wander(0.5),
                    new StayCloseToSpawn(0.5, 7)
                    ),
                new State("baibaiscrubs",
                    new ReturnToSpawn(speed: 2),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ChangeSize(20, 100),
                    new TimedTransition(200, "seclol")
                    ),
                new State("seclol",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ChangeSize(20, 80),
                    new TimedTransition(1, "seclol2")
                    ),
                new State("seclol2",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ChangeSize(20, 60),
                    new TimedTransition(1, "seclol3")
                    ),
                new State("seclol3",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ChangeSize(20, 40),
                    new TimedTransition(1, "seclol4")
                    ),
                new State("seclol4",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ChangeSize(20, 20),
                    new TimedTransition(1, "seclol5")
                    ),
                new State("seclol5",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ChangeSize(20, 0),
                    new TimedTransition(1, "nubs")
                    ),
                new State("nubs",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new InvisiToss("DS Gulpord the Slime God M", 3, 32, coolDown: 9999999),
                    new InvisiToss("DS Gulpord the Slime God M", 3, 15, coolDown: 9999999),
                    new TimedTransition(100, "idleeeee")
                    ),
                new State("idleeeee",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new EntityNotExistsTransition("ds gulpord the slime god m", 10, "nubs2")
                    ),
                new State("nubs2",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new InvisiToss("DS Gulpord the Slime God s", 3, 32, coolDown: 9999999),
                    new InvisiToss("DS Gulpord the Slime God s", 3, 15, coolDown: 9999999),
                    new InvisiToss("DS Gulpord the Slime God s", 3, 26, coolDown: 9999999),
                    new InvisiToss("DS Gulpord the Slime God s", 3, 21, coolDown: 9999999),
                    new TimedTransition(100, "idleeeeee")
                    ),
                new State("idleeeeee",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new EntityNotExistsTransition("ds gulpord the slime god s", 10, "seclolagain")
                    ),
                new State("seclolagain",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ChangeSize(0, 0),
                    new TimedTransition(1, "seclolagain1")
                    ),
                new State("seclolagain1",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ChangeSize(20, 20),
                    new TimedTransition(1, "seclolagain2")
                    ),
                new State("seclolagain2",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ChangeSize(40, 40),
                    new TimedTransition(1, "seclolagain3")
                    ),
                new State("seclolagain3",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ChangeSize(60, 60),
                    new TimedTransition(1, "seclolagain4")
                    ),
                new State("seclolagain4",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ChangeSize(80, 80),
                    new TimedTransition(1, "seclolagain5")
                    ),
                new State("seclolagain5",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ChangeSize(100, 100),
                    new TimedTransition(1, "GO ANGRY!!!!111!!11")
                    ),
                new State("GO ANGRY!!!!111!!11",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Flash(0xFF0000, 1, 1),
                    new TimedTransition(1000, "FOLLOW")
                    ),
                new State("FOLLOW",
                new RealmPortalDrop(),
                new ConditionalEffect(ConditionEffectIndex.ParalyzeImmune),
                new ConditionalEffect(ConditionEffectIndex.StunImmune),
                new Shoot(10, 8, 45, 2, coolDown: 2000),
                new Shoot(3, 1, 0, 1, coolDown: 1000),
                new Follow(0.6, 10, 0),
                    new State("xdshoot",
                        new Shoot(10, 2, 5, 0, coolDown: 150),
                        new TimedTransition(1750, "xdshoot1")
                        ),
                    new State("xdshoot1",
                        new Shoot(10, 2, 10, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot2")
                        ),
                    new State("xdshoot2",
                        new Shoot(10, 2, 15, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot3")
                        ),
                    new State("xdshoot3",
                        new Shoot(10, 2, 20, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot4")
                        ),
                    new State("xdshoot4",
                        new Shoot(10, 2, 25, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot5")
                        ),
                    new State("xdshoot5",
                        new Shoot(10, 2, 30, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot6")
                        ),
                    new State("xdshoot6",
                        new Shoot(10, 2, 35, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot7")
                        ),
                    new State("xdshoot7",
                        new Shoot(10, 2, 40, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot8")
                        ),
                    new State("xdshoot8",
                        new Shoot(10, 2, 45, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot")
                        ),
                    new State("xdshoot",
                        new Shoot(10, 2, 50, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot9")
                        ),
                    new State("xdshoot9",
                        new Shoot(10, 2, 55, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot0")
                        ),
                    new State("xdshoot0",
                        new Shoot(10, 2, 60, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot00")
                        ),
                    new State("xdshoot00",
                        new Shoot(10, 2, 65, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot000")
                        ),
                    new State("xdshoot000",
                        new Shoot(10, 2, 70, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot0000")
                        ),
                    new State("xdshoot0000",
                        new Shoot(10, 2, 75, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshootxd")
                        ),
                    new State("xdshootxd",
                        new Shoot(10, 2, 80, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshootxdd")
                        ),
                    new State("xdshootxdd",
                        new Shoot(10, 2, 85, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshootxddd")
                        ),
                    new State("xdshootxddd",
                        new Shoot(10, 2, 90, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshootfff")
                        ),
                    new State("xdshootfff",
                        new Shoot(10, 2, 95, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshooteee")
                        ),
                    new State("xdshooteee",
                        new Shoot(10, 2, 100, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshootyyy")
                        ),
                    new State("xdshootyyy",
                        new Shoot(10, 2, 105, 0, coolDown: 1500),
                        new TimedTransition(100, "xdshoot")
                        )
                    )
                ),
            new Threshold(0.1,
                new ItemLoot("Greater Potion of Defense", 1),
                new ItemLoot("Void Blade", 0.02),
                new ItemLoot("Murky Toxin", 0.02),
                new ItemLoot("Wine Cellar Incantation", 0.01),
                new ItemLoot("Slurp Knight Skin", 0.01)
                )
            );
    }
}