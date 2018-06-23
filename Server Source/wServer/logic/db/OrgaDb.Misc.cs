using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Misc = () => Behav() //Only misc behaviors like sheep, healing turrets and such.
        .Init("West Tutorial Gun",
                new State(
                    new Shoot(32, fixedAngle: 180, coolDown: new Cooldown(3000, 1000))
                    )
            )
            .Init("North Tutorial Gun",
                new State(
                    new Shoot(32, fixedAngle: 270, coolDown: new Cooldown(3000, 1000))
                    )
            )
            .Init("East Tutorial Gun",
                new State(
                    new Shoot(32, fixedAngle: 0, coolDown: new Cooldown(3000, 1000))
                    )
            )
            .Init("South Tutorial Gun",
                new State(
                    new Shoot(32, fixedAngle: 90, coolDown: new Cooldown(3000, 1000))
                    )
            )
            .Init("Evil Chicken",
                new State(
                    new Wander(0.3)
                    )
            )
            .Init("Evil Chicken Minion",
                new State(
                    new Wander(0.3),
                    new Protect(0.3, "Evil Chicken God")
                    )
            )
            .Init("Evil Chicken God",
                new State(
                    new Prioritize(
                        new Follow(0.4, range: 5),
                        new Wander(0.3)
                        ),
                    new Reproduce("Evil Chicken Minion", densityMax: 12)
                    )
            )
            .Init("Evil Hen",
                new State(
                    new Wander(0.3)
                    ),
                new ItemLoot("Minor Health Potion", 1)
            )
            .Init("Kitchen Guard",
                new State(
                    new Prioritize(
                        new Follow(0.6, range: 6),
                        new Wander(0.4)
                        ),
                    new Shoot(7)
                    )
            )
            .Init("Butcher",
                new State(
                    new Prioritize(
                        new Follow(0.8, range: 1),
                        new Wander(0.4)
                        ),
                    new Shoot(3)
                    ),
                new ItemLoot("Minor Health Potion", 0.1),
                new ItemLoot("Minor Magic Potion", 0.1)
            )
            .Init("Bonegrind the Butcher",
                new State(
                    new State("Begin",
                        new Wander(0.6),
                        new PlayerWithinTransition(10, "AttackX")
                        ),
                    new State("AttackX",
                        new Taunt("Ah, fresh meat for the minions!"),
                        new Shoot(6, coolDown: 1400),
                        new Prioritize(
                            new Follow(0.6, 9, 3),
                            new Wander(0.6)
                            ),
                        new TimedTransition(4500, "AttackY"),
                        new HpLessTransition(0.3, "Flee")
                        ),
                    new State("AttackY",
                        new Prioritize(
                            new Follow(0.6, 9, 3),
                            new Wander(0.6)
                            ),
                        new Sequence(
                            new Shoot(7, 4, fixedAngle: 25),
                            new Shoot(7, 4, fixedAngle: 50),
                            new Shoot(7, 4, fixedAngle: 75),
                            new Shoot(7, 4, fixedAngle: 100),
                            new Shoot(7, 4, fixedAngle: 125)
                            ),
                        new TimedTransition(5200, "AttackX"),
                        new HpLessTransition(0.3, "Flee")
                        ),
                    new State("Flee",
                        new Taunt("The meat ain't supposed to bite back! Waaaaa!!"),
                        new Flash(0xff000000, 10, 100),
                        new Prioritize(
                            new StayBack(0.5, 6),
                            new Wander(0.5)
                            )
                        )
                    ),
                new ItemLoot("Minor Health Potion", 1),
                new ItemLoot("Minor Magic Potion", 1)
            )
         .Init("White Fountain",
                new State(
                    new NexusHealHp(5, 100, 1000)
                    )
            )
            .Init("Winter Fountain Frozen",
                new State(
                    new NexusHealHp(5, 100, 1000)
                    )
            )
            .Init("Sheep",
                new State(
                    new PlayerWithinTransition(15, "player_nearby"),
                    new State("player_nearby",
                        new Prioritize(
                            new StayCloseToSpawn(0.1, 2),
                            new Wander(0.1)
                            ),
                        new Taunt(0.001, 1000, "baa", "baa baa")
                        )
                    )
                )
        .Init("Nexus Crier",
            new State(
                new State("Idle",
                    new Taunt(true, "Welcome to Discovered Realms!"),
                    new TimedTransition(30000, "Idle2")
                    ),
            new State("Idle2",
                new Taunt(true, "Join us on discord @ discord.gg/ZYhQxzG"),
                    new TimedTransition(30000, "Idle3")
                ),
            new State("Idle3",
                new test1(ConditionEffectIndex.Invulnerable, duration: 2000, coolDown: 5000),
                new Taunt(true, "Breaking the rules will result in a ban!"),
                    new TimedTransition(30000, "Idle4")
                ),
            new State("Idle4",
                new Taunt(true, "Hacking will result in an ip-ban!"),
                    new TimedTransition(30000, "Idle5")
                ),
            new State("Idle5",
                new Taunt(true, "Treat other people with respect!"),
                    new TimedTransition(30000, "Idle6")
                ),
            new State("Idle6",
                new Taunt(true, "Have a good time on Discovered Realms!"),
                    new TimedTransition(30000, "Idle7")
                ),
            new State("Idle7",
                new Taunt(true, "You can donate using the coin ingame! (If no staff members are online, you need to take a screenshot of the reciept and send it to either A/Arcanuo)"),
                    new TimedTransition(30000, "Idle")
                )
            )
        );
    }
}
