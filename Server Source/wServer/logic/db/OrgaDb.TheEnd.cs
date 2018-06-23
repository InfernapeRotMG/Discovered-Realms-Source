#region

using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

#endregion

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ TheEnd = () => Behav()
       .Init("arrow queen of darkness",
            new State(
                new StayCloseToSpawn(0.3, 4),
                //new ConditionalBehavior(ConditionEffectIndex.Invincible, new Taunt("Hero.. You cannot paralyze me, TAKE THE SHOT BACK!"), new Shoot(50, 1, 1, 2, coolDown: 9999999), new UnsetConditionalEffect(ConditionEffectIndex.Invulnerable)),
                new State("idle",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(10, "Speak")
                    ),
                new State("Speak",
                    //new SetPiece("HotLava", 116, 48),
                    new BringPlayer(newX: 116, newY: 48, range: 20, coolDown: 1),
                    new Taunt(true, "Hero.. You do not know what you have done, prepare to die..."),
                    new TimedTransition(5000, "GetOwnedm8")
                    ),
                new State("GetOwnedm8",
                    new HpLessTransition(0.30, "NearDeath"),
                    new UnsetConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new test1(ConditionEffectIndex.ArmorBroken, duration: 5000, coolDown: 15000),
                    new Wander(0.3),
                    new Shoot(20, 5, 75 / 5, 0, coolDown: 3000),
                    new Shoot(20, 2, 50 / 2, 1, coolDown: 1750),
                    new TimedTransition(10000, "DeathArrows")
                    ),
                new State("DeathArrows",
                    new HpLessTransition(0.30, "NearDeath"),
                    new Taunt(true, "FEEL THE WRATH OF THE ARROWS OF DEATH!"),
                    new Shoot(50, 10, 100 / 10, 1, coolDown: 10000),
                    new TimedTransition(100, "GetOwnedm8")
                    ),
                new State("NearDeath",
                    new ReturnToSpawn(true, 5),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new Taunt(true, "You hero, have nearly beaten me. But i will end you here.."),
                    new TimedTransition(5000, "KillEm")
                    ),
                new State("KillEm",
                    new Taunt(1, 7500, "Ha.. haaaaa... Hero you cannot defeat me!"),
                    new UnsetConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Wander(0.5),
                    new Shoot(50, 20, 100/20, 0, coolDown: 5000),
                    new Shoot(50, 8, 360/8, 1, 0, coolDown: 1000),
                    new HpLessTransition(0.05, "PreDeath")
                    ),
                new State("PreDeath",
                    new ReturnToSpawn(true, 5),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new Taunt(true, "You have killed me.. You release a great honor upon this realm..."),
                    new TimedTransition(5000, "Death")
                    ),
                new State("Death",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new Suicide()
                    )
                )
        );
    }
}