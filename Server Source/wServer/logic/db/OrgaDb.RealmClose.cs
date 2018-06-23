#region

using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

#endregion

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ RealmCloses = () => Behav()
        #region GHOSTKING
        .Init("Ghost King",
                new State(
                    new State("Idle",
                        new BackAndForth(0.3, 3),
                        new HpLessTransition(0.99999, "EvaluationStart1")
                        ),
                    new State("EvaluationStart1",
                        new Taunt("No corporeal creature can kill my sorrow"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Prioritize(
                            new StayCloseToSpawn(0.4, 3),
                            new Wander(0.4)
                            ),
                        new TimedTransition(2500, "EvaluationStart2")
                        ),
                    new State("EvaluationStart2",
                        new Flash(0x0000ff, 0.1, 60),
                        new ChangeSize(20, 140),
                        new Shoot(10, 4, 30, defaultAngle: 0, coolDown: 1000),
                        new Prioritize(
                            new StayCloseToSpawn(0.4, 3),
                            new Wander(0.4)
                            ),
                        new HpLessTransition(0.87, "EvaluationEnd"),
                        new TimedTransition(6000, "EvaluationEnd")
                        ),
                    new State("EvaluationEnd",
                        new Taunt(0.5, "Aye, let's be miserable together"),
                        new HpLessTransition(0.875, "HugeMob"),
                        new HpLessTransition(0.952, "Mob"),
                        new HpLessTransition(0.985, "SmallGroup"),
                        new HpLessTransition(0.99999, "Solo")
                        ),
                    new State("HugeMob",
                        new Taunt("What a HUGE MOB!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0x00ff00, 0.2, 300),
                        new TossObject("Small Ghost", 4, 0, 100000),
                        new TossObject("Small Ghost", 4, 60, 100000),
                        new TossObject("Small Ghost", 4, 120, 100000),
                        new TossObject("Large Ghost", 4, 180, 100000),
                        new TossObject("Large Ghost", 4, 240, 100000),
                        new TossObject("Large Ghost", 4, 300, 100000),
                        new TimedTransition(30000, "HugeMob2")
                        ),
                    new State("HugeMob2",
                        new Taunt("I feel almost manic!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0x00ff00, 0.2, 300),
                        new TossObject("Small Ghost", 4, 0, 100000),
                        new TossObject("Small Ghost", 4, 60, 100000),
                        new TossObject("Small Ghost", 4, 120, 100000),
                        new TossObject("Large Ghost", 4, 180, 100000),
                        new TossObject("Large Ghost", 4, 240, 100000),
                        new TossObject("Large Ghost", 4, 300, 100000),
                        new TimedTransition(30000, "Company")
                        ),
                    new State("Mob",
                        new Taunt("There's a MOB of you."),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0x00ff00, 0.2, 300),
                        new TossObject("Small Ghost", 4, 0, 100000),
                        new TossObject("Small Ghost", 4, 60, 100000),
                        new TossObject("Small Ghost", 4, 120, 100000),
                        new TossObject("Large Ghost", 4, 180, 100000),
                        new TossObject("Large Ghost", 4, 240, 100000),
                        new TossObject("Large Ghost", 4, 300, 100000),
                        new TimedTransition(30000, "Company")
                        ),
                    new State("Company",
                        new Taunt("Misery loves company!"),
                        new TossObject("Ghost Master", 4, 0, 100000),
                        new TossObject("Medium Ghost", 4, 60, 100000),
                        new TossObject("Medium Ghost", 4, 120, 100000),
                        new TossObject("Large Ghost", 4, 180, 100000),
                        new TossObject("Large Ghost", 4, 240, 100000),
                        new TossObject("Large Ghost", 4, 300, 100000),
                        new TimedTransition(2000, "Wait")
                        ),
                    new State("SmallGroup",
                        new Taunt("Such a small party."),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0x00ff00, 0.2, 300),
                        new TossObject("Small Ghost", 4, 0, 100000),
                        new TossObject("Small Ghost", 4, 60, 100000),
                        new TossObject("Small Ghost", 4, 120, 100000),
                        new TossObject("Medium Ghost", 4, 180, 100000),
                        new TossObject("Medium Ghost", 4, 240, 100000),
                        new TossObject("Medium Ghost", 4, 300, 100000),
                        new TimedTransition(30000, "SmallGroup2")
                        ),
                    new State("SmallGroup2",
                        new Taunt("Misery loves company!"),
                        new TossObject("Ghost Master", 4, 0, 100000),
                        new TossObject("Small Ghost", 4, 60, 100000),
                        new TossObject("Small Ghost", 4, 120, 100000),
                        new TossObject("Medium Ghost", 4, 180, 100000),
                        new TossObject("Medium Ghost", 4, 240, 100000),
                        new TossObject("Medium Ghost", 4, 300, 100000),
                        new TimedTransition(2000, "Wait")
                        ),
                    new State("Solo",
                        new Taunt("Just you?  I guess you don't have any friends to play with."),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0x00ff00, 0.2, 10),
                        new TossObject("Ghost Master", 4, 0, 100000),
                        new TossObject("Small Ghost", 4, 70, 100000),
                        new TossObject("Small Ghost", 4, 140, 100000),
                        new TossObject("Small Ghost", 4, 210, 100000),
                        new TossObject("Small Ghost", 4, 280, 100000),
                        new TimedTransition(1000, "Wait")
                        ),
                    new State("Wait",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0x00ff00, 0.2, 10000),
                        new Prioritize(
                            new StayCloseToSpawn(1, 8),
                            new Follow(0.6, range: 2, duration: 2000, coolDown: 2000)
                            ),
                        new Shoot(10, coolDown: 1000),
                        new State("Speak",
                            new Taunt("I cannot be defeated while my loyal subjects sustain me!"),
                            new TimedTransition(1000, "Quiet")
                            ),
                        new State("Quiet",
                            new TimedTransition(22000, "Speak")
                            ),
                        new TimedTransition(140000, "Overly_long_combat")
                        ),
                    new State("Overly_long_combat",
                        new Taunt("You have sapped my energy. A curse on you!"),
                        new Prioritize(
                            new StayCloseToSpawn(1, 8),
                            new Follow(0.6, range: 2, duration: 2000, coolDown: 2000)
                            ),
                        new Shoot(10, coolDown: 1000),
                        new Order(30, "Ghost Master", "Decay"),
                        new Order(30, "Small Ghost", "Decay"),
                        new Order(30, "Medium Ghost", "Decay"),
                        new Order(30, "Large Ghost", "Decay"),
                        new Transform("Actual Ghost King")
                        ),
                    new State("Killed",
                        new Taunt("I feel my flesh again! For the first time in a 1000 years I LIVE!"),
                        new Taunt(0.5, "Will you release me?"),
                        new Transform("Actual Ghost King")
                        )
                    ),
                new Threshold(0.01,
                    new ItemLoot("Dagger of Apparitions", 0.02)
                    )
            )
            .Init("Ghost Master",
                new State(
                    new State("Attack1",
                        new State("NewLocation1",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Flash(0xff00ff00, 0.2, 10),
                            new Prioritize(
                                new StayCloseToSpawn(2, 7),
                                new Wander(2)
                                ),
                            new TimedTransition(1000, "Att1")
                            ),
                        new State("Att1",
                            new Shoot(10, 4, 90, fixedAngle: 0, coolDown: 400),
                            new TimedTransition(9000, "NewLocation1")
                            ),
                        new HpLessTransition(0.99, "Attack2")
                        ),
                    new State("Attack2",
                        new State("Intro",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Flash(0xff00ff00, 0.2, 10),
                            new ChangeSize(20, 140),
                            new TimedTransition(1000, "NewLocation2")
                            ),
                        new State("NewLocation2",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Flash(0xff00ff00, 0.2, 10),
                            new Prioritize(
                                new StayCloseToSpawn(2, 7),
                                new Wander(2)
                                ),
                            new TimedTransition(1000, "Att2")
                            ),
                        new State("Att2",
                            new Shoot(10, 4, 90, fixedAngle: 45, coolDown: 400),
                            new TimedTransition(6000, "NewLocation2")
                            ),
                        new HpLessTransition(0.98, "Attack3")
                        ),
                    new State("Attack3",
                        new State("Intro",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Flash(0xff00ff00, 0.2, 10),
                            new ChangeSize(20, 180),
                            new TimedTransition(1000, "NewLocation3")
                            ),
                        new State("NewLocation3",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Flash(0xff00ff00, 0.2, 10),
                            new Prioritize(
                                new StayCloseToSpawn(2, 7),
                                new Wander(2)
                                ),
                            new TimedTransition(1000, "Att3")
                            ),
                        new State("Att3",
                            new Shoot(10, 4, 90, fixedAngle: 22.5, coolDown: 400),
                            new TimedTransition(3000, "NewLocation3")
                            ),
                        new HpLessTransition(0.94, "KillKing")
                        ),
                    new State("KillKing",
                        new Taunt("Your secret soul master is dying, Your Majesty"),
                        new Order(30, "Ghost King", "Killed"),
                        new TimedTransition(3000, "Suicide")
                        ),
                    new State("Suicide",
                        new Taunt("I cannot live with my betrayal..."),
                        new Shoot(0, 8, 45, fixedAngle: 22.5),
                        new Decay(0)
                        ),
                    new State("Decay",
                        new Decay(0)
                        )
                    ),
                new ItemLoot("Purple Drake Egg", 0.03),
                new ItemLoot("White Drake Egg", 0.001),
                new ItemLoot("Tincture of Dexterity", 0.02)
            )
            .Init("Actual Ghost King",
                new State(
                    new Taunt(0.9, "I am still so very alone"),
                    new ChangeSize(-20, 95),
                    new Flash(0xff000000, 0.4, 100),
                    new BackAndForth(0.5, 3)
                    ),
                new TierLoot(2, ItemType.Ring, 0.25),
                new TierLoot(3, ItemType.Ring, 0.08),
                new TierLoot(7, ItemType.Weapon, 0.3),
                new TierLoot(8, ItemType.Weapon, 0.1),
                new TierLoot(7, ItemType.Armor, 0.3),
                new TierLoot(8, ItemType.Armor, 0.1),
                new TierLoot(2, ItemType.Ability, 0.7),
                new TierLoot(3, ItemType.Ability, 0.16),
                new TierLoot(4, ItemType.Ability, 0.02),
                new ItemLoot("Health Potion", 0.7),
                new ItemLoot("Magic Potion", 0.7)
            )
            .Init("Small Ghost",
                new State(
                    new TransformOnDeath("Medium Ghost"),
                    new State("NewLocation",
                        new Taunt(0.1, "Switch!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0xff00ff00, 0.2, 10),
                        new Prioritize(
                            new StayCloseToSpawn(2, 7),
                            new Wander(2)
                            ),
                        new TimedTransition(1000, "Attack")
                        ),
                    new State("Attack",
                        new Taunt(0.1, "Save the King's Soul!"),
                        new Shoot(10, 4, 90, fixedAngle: 0, coolDown: 400),
                        new TimedTransition(9000, "NewLocation")
                        ),
                    new State("Decay",
                        new Decay(0)
                        ),
                    new Decay(160000)
                    ),
                new ItemLoot("Magic Potion", 0.02),
                new ItemLoot("Ring of Magic", 0.02),
                new ItemLoot("Ring of Attack", 0.02)
            )
            .Init("Medium Ghost",
                new State(
                    new TransformOnDeath("Large Ghost"),
                    new State("Intro",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0xff00ff00, 0.2, 10),
                        new ChangeSize(20, 140),
                        new TimedTransition(1000, "NewLocation")
                        ),
                    new State("NewLocation",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0xff00ff00, 0.2, 10),
                        new Prioritize(
                            new StayCloseToSpawn(2, 7),
                            new Wander(2)
                            ),
                        new TimedTransition(1000, "Attack")
                        ),
                    new State("Attack",
                        new Taunt(0.02, "I come back more powerful than you could ever imagine"),
                        new Shoot(10, 4, 90, fixedAngle: 45, coolDown: 800),
                        new TimedTransition(6000, "NewLocation")
                        ),
                    new State("Decay",
                        new Decay(0)
                        ),
                    new Decay(160000)
                    ),
                new ItemLoot("Magic Potion", 0.02),
                new ItemLoot("Ring of Speed", 0.02),
                new ItemLoot("Ring of Attack", 0.02),
                new ItemLoot("Iron Quiver", 0.02)
            )
            .Init("Large Ghost",
                new State(
                    new State("Intro",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0xff00ff00, 0.2, 10),
                        new ChangeSize(20, 180),
                        new TimedTransition(1000, "NewLocation")
                        ),
                    new State("NewLocation",
                        new Taunt(0.01,
                            "The Ghost King protects this sacred ground",
                            "The Ghost King gave his heart to the Ghost Master.  He cannot die.",
                            "Only the Secret Ghost Master can kill the King."),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0xff00ff00, 0.2, 10),
                        new Prioritize(
                            new StayCloseToSpawn(2, 7),
                            new Wander(2)
                            ),
                        new TimedTransition(1000, "Attack")
                        ),
                    new State("Attack",
                        new Taunt(0.01, "The King's wife died here.  For her memory."),
                        new Shoot(10, 8, 45, fixedAngle: 22.5, coolDown: 800),
                        new TimedTransition(3000, "NewLocation"),
                        new EntityNotExistsTransition("Ghost King", 30, "AttackKingGone")
                        ),
                    new State("AttackKingGone",
                        new Taunt(0.01, "The King's wife died here.  For her memory."),
                        new Shoot(10, 8, 45, fixedAngle: 22.5, coolDown: 800, coolDownOffset: 800),
                        new TransformOnDeath("Imp", 2, 3),
                        new TimedTransition(3000, "NewLocation")
                        ),
                    new State("Decay",
                        new Decay(0)
                        ),
                    new Decay(160000)
                    ),
                new ItemLoot("Magic Potion", 0.02),
                new ItemLoot("Tincture of Defense", 0.02),
                new ItemLoot("Blue Drake Egg", 0.02),
                new ItemLoot("Yellow Drake Egg", 0.02)
            )


        #endregion

        #region LICH
        .Init("Lich",
                new State(
                    new State("Idle",
                        new StayCloseToSpawn(0.5, range: 5),
                        new Wander(0.5),
                        new HpLessTransition(0.99999, "EvaluationStart1")
                        ),
                    new State("EvaluationStart1",
                        new Taunt("New recruits for my undead army? How delightful!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Prioritize(
                            new StayCloseToSpawn(0.35, range: 5),
                            new Wander(0.35)
                            ),
                        new TimedTransition(2500, "EvaluationStart2")
                        ),
                    new State("EvaluationStart2",
                        new Flash(0x0000ff, 0.1, 60),
                        new Prioritize(
                            new StayCloseToSpawn(0.35, range: 5),
                            new Wander(0.35)
                            ),
                        new Shoot(10, projectileIndex: 1, count: 3, shootAngle: 120, coolDown: 100000,
                            coolDownOffset: 200),
                        new Shoot(10, projectileIndex: 1, count: 3, shootAngle: 120, coolDown: 100000,
                            coolDownOffset: 400),
                        new Shoot(10, projectileIndex: 1, count: 3, shootAngle: 120, coolDown: 100000,
                            coolDownOffset: 2200),
                        new Shoot(10, projectileIndex: 1, count: 3, shootAngle: 120, coolDown: 100000,
                            coolDownOffset: 2400),
                        new Shoot(10, projectileIndex: 1, count: 3, shootAngle: 120, coolDown: 100000,
                            coolDownOffset: 4200),
                        new Shoot(10, projectileIndex: 1, count: 3, shootAngle: 120, coolDown: 100000,
                            coolDownOffset: 4400),
                        new HpLessTransition(0.87, "EvaluationEnd"),
                        new TimedTransition(6000, "EvaluationEnd")
                        ),
                    new State("EvaluationEnd",
                        new Taunt("Time to meet your future brothers and sisters..."),
                        new HpLessTransition(0.875, "HugeMob"),
                        new HpLessTransition(0.952, "Mob"),
                        new HpLessTransition(0.985, "SmallGroup"),
                        new HpLessTransition(0.99999, "Solo")
                        ),
                    new State("HugeMob",
                        new Taunt("...there's an ARMY of them! HahaHahaaaa!!!"),
                        new Flash(0x00ff00, 0.2, 300),
                        new Spawn("Haunted Spirit", 5, 0, 3000),
                        new TossObject("Phylactery Bearer", 5.5, 0, 100000),
                        new TossObject("Phylactery Bearer", 5.5, 120, 100000),
                        new TossObject("Phylactery Bearer", 5.5, 240, 100000),
                        new TossObject("Phylactery Bearer", 3, 60, 100000),
                        new TossObject("Phylactery Bearer", 3, 180, 100000),
                        new Prioritize(
                            new Protect(0.9, "Phylactery Bearer", 15, 2, 2),
                            new Wander(0.9)
                            ),
                        new TimedTransition(25000, "HugeMob2")
                        ),
                    new State("HugeMob2",
                        new Taunt("My minions have stolen your life force and fed it to me!"),
                        new Flash(0x00ff00, 0.2, 300),
                        new Spawn("Haunted Spirit", 5, 0, 3000),
                        new TossObject("Phylactery Bearer", 5.5, 0, 100000),
                        new TossObject("Phylactery Bearer", 5.5, 120, 100000),
                        new TossObject("Phylactery Bearer", 5.5, 240, 100000),
                        new Prioritize(
                            new Protect(0.9, "Phylactery Bearer", 15, 2, 2),
                            new Wander(0.9)
                            ),
                        new TimedTransition(5000, "Wait")
                        ),
                    new State("Mob",
                        new Taunt("...there's a lot of them! Hahaha!!"),
                        new Flash(0x00ff00, 0.2, 300),
                        new Spawn("Haunted Spirit", 2, 0, 2000),
                        new TossObject("Phylactery Bearer", 5.5, 0, 100000),
                        new TossObject("Phylactery Bearer", 5.5, 120, 100000),
                        new TossObject("Phylactery Bearer", 5.5, 240, 100000),
                        new Prioritize(
                            new Protect(0.9, "Phylactery Bearer", 15, 2, 2),
                            new Wander(0.9)
                            ),
                        new TimedTransition(22000, "Mob2")
                        ),
                    new State("Mob2",
                        new Taunt("My minions have stolen your life force and fed it to me!"),
                        new Spawn("Haunted Spirit", 2, 0, 2000),
                        new TossObject("Phylactery Bearer", 5.5, 0, 100000),
                        new TossObject("Phylactery Bearer", 5.5, 120, 100000),
                        new TossObject("Phylactery Bearer", 5.5, 240, 100000),
                        new Prioritize(
                            new Protect(0.9, "Phylactery Bearer", 15, 2, 2),
                            new Wander(0.9)
                            ),
                        new TimedTransition(5000, "Wait")
                        ),
                    new State("SmallGroup",
                        new Taunt("...and there's more where they came from!"),
                        new Flash(0x00ff00, 0.2, 300),
                        new TossObject("Phylactery Bearer", 5.5, 0, 100000),
                        new TossObject("Phylactery Bearer", 5.5, 240, 100000),
                        new Prioritize(
                            new Protect(0.9, "Phylactery Bearer", 15, 2, 2),
                            new Wander(0.9)
                            ),
                        new TimedTransition(15000, "SmallGroup2")
                        ),
                    new State("SmallGroup2",
                        new Taunt("My minions have stolen your life force and fed it to me!"),
                        new Spawn("Haunted Spirit", 1, 1, 9000),
                        new Prioritize(
                            new Protect(0.9, "Phylactery Bearer", 15, 2, 2),
                            new Wander(0.9)
                            ),
                        new TimedTransition(5000, "Wait")
                        ),
                    new State("Solo",
                        new Taunt("...it's a small family, but you'll enjoy being part of it!"),
                        new Flash(0x00ff00, 0.2, 10),
                        new Wander(0.5),
                        new TimedTransition(3000, "Wait")
                        ),
                    new State("Wait",
                        new Taunt("Kneel before me! I am the master of life and death!"),
                        new Transform("Actual Lich")
                        )
                    )
            )
            .Init("Actual Lich",
                new State(
                    new Prioritize(
                        new Protect(0.9, "Phylactery Bearer", 15, 2, 2),
                        new Wander(0.5)
                        ),
                    new Spawn("Mummy", 4, coolDown: 4000),
                    new Spawn("Mummy King", 2, coolDown: 4000),
                    new Spawn("Mummy Pharaoh", 1, coolDown: 4000),
                    new State("typeA",
                        new Shoot(10, projectileIndex: 0, count: 2, shootAngle: 7, coolDown: 800),
                        new TimedTransition(8000, "typeB")
                        ),
                    new State("typeB",
                        new Taunt(0.7, "All that I touch turns to dust!",
                            "You will drown in a sea of undead!",
                            "ya gotta eat the booty like groceries!"
                            ),
                        new Shoot(10, projectileIndex: 1, count: 4, shootAngle: 7, coolDown: 1000),
                        new Shoot(10, projectileIndex: 0, count: 2, shootAngle: 7, coolDown: 800),
                        new TimedTransition(6000, "typeA")
                        )
                    ),
                new Threshold(0.15,
                new TierLoot(2, ItemType.Ring, 0.11),
                new TierLoot(3, ItemType.Ring, 0.01),
                new TierLoot(5, ItemType.Weapon, 0.3),
                new TierLoot(6, ItemType.Weapon, 0.2),
                new TierLoot(7, ItemType.Weapon, 0.05),
                new TierLoot(5, ItemType.Armor, 0.3),
                new TierLoot(6, ItemType.Armor, 0.2),
                new TierLoot(7, ItemType.Armor, 0.05),
                new TierLoot(1, ItemType.Ability, 0.9),
                new TierLoot(2, ItemType.Ability, 0.15),
                new TierLoot(3, ItemType.Ability, 0.02),
                new ItemLoot("Health Potion", 0.4),
                new ItemLoot("Magic Potion", 0.4)
                    )
            )
            .Init("Phylactery Bearer",
                new State(
                    new Heal(15, "Heros", 200),
                    new State("Attack1",
                        new Shoot(10, projectileIndex: 0, count: 3, shootAngle: 120, coolDown: 900, coolDownOffset: 400),
                        new State("AttackX",
                            new Prioritize(
                                new StayCloseToSpawn(0.55, range: 5),
                                new Orbit(0.55, 4, 5)
                                ),
                            new TimedTransition(1500, "AttackY")
                            ),
                        new State("AttackY",
                            new Taunt(0.05, "We feed the master!"),
                            new Prioritize(
                                new StayCloseToSpawn(0.55, range: 5),
                                new StayBack(0.55, 2),
                                new Wander(0.55)
                                ),
                            new TimedTransition(1500, "AttackX")
                            ),
                        new HpLessTransition(0.65, "Attack2")
                        ),
                    new State("Attack2",
                        new Shoot(10, projectileIndex: 0, count: 3, shootAngle: 15, predictive: 0.1, coolDown: 600,
                            coolDownOffset: 200),
                        new State("AttackX",
                            new Prioritize(
                                new StayCloseToSpawn(0.65, range: 5),
                                new Orbit(0.65, 4, acquireRange: 10)
                                ),
                            new TimedTransition(1500, "AttackY")
                            ),
                        new State("AttackY",
                            new Taunt(0.05, "We feed the master!"),
                            new Prioritize(
                                new StayCloseToSpawn(0.65, range: 5),
                                new Buzz(),
                                new Wander(0.65)
                                ),
                            new TimedTransition(1500, "AttackX")
                            ),
                        new HpLessTransition(0.3, "Attack3")
                        ),
                    new State("Attack3",
                        new Shoot(10, projectileIndex: 1, coolDown: 800),
                        new State("AttackX",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Prioritize(
                                new StayCloseToSpawn(1.3, range: 5),
                                new Wander(1.3)
                                ),
                            new TimedTransition(2500, "AttackY")
                            ),
                        new State("AttackY",
                            new Taunt(0.02, "We feed the master!"),
                            new Prioritize(
                                new StayCloseToSpawn(1, range: 5),
                                new Wander(1)
                                ),
                            new TimedTransition(2500, "AttackX")
                            )
                        ),
                    new Decay(130000)
                    ),
                new ItemLoot("Magic Potion", 0.03),
                new Threshold(0.15,

                new ItemLoot("The Phylactery", 0.001),
                new ItemLoot("Soul of the Bearer", 0.001),
                new ItemLoot("Soulless Robe", 0.001),
                new ItemLoot("Ring of the Covetous Heart", 0.01),
                new ItemLoot("Tincture of Defense", 0.02),
                new ItemLoot("Orange Drake Egg", 0.06)
                    )

            )
            .Init("Haunted Spirit",
                new State(
                    new State("NewLocation",
                        new Taunt(0.1, "XxxXxxxXxXxXxxx..."),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(10, predictive: 0.2, coolDown: 700),
                        new Prioritize(
                            new StayCloseToSpawn(1, 11),
                            new Wander(1)
                            ),
                        new TimedTransition(7000, "Attack")
                        ),
                    new State("Attack",
                        new Taunt(0.1, "Hungry..."),
                        new Shoot(10, predictive: 0.3, coolDown: 700),
                        new Shoot(10, 2, 70, coolDown: 700, coolDownOffset: 200),
                        new TimedTransition(3000, "NewLocation")
                        ),
                    new Decay(90000)
                    ),
                new TierLoot(8, ItemType.Weapon, 0.02),
                new ItemLoot("Magic Potion", 0.02),
                new ItemLoot("Ring of Magic", 0.02),
                new ItemLoot("Ring of Attack", 0.02),
                new ItemLoot("Tincture of Dexterity", 0.06),
                new ItemLoot("Tincture of Mana", 0.09),
                new ItemLoot("Tincture of Life", 0.04)
            )
            .Init("Mummy",
                new State(
                    new Prioritize(
                        new Protect(1, "Lich", protectionRange: 10),
                        new Follow(1.2, range: 7),
                        new Wander(0.4)
                        ),
                    new Shoot(10)
                    ),
                new ItemLoot("Magic Potion", 0.02),
                new ItemLoot("Spirit Salve Tome", 0.02)
            )
            .Init("Mummy King",
                new State(
                    new Prioritize(
                        new Protect(1, "Lich", protectionRange: 10),
                        new Follow(1.2, range: 7),
                        new Wander(0.4)
                        ),
                    new Shoot(10)
                    ),
                new ItemLoot("Magic Potion", 0.02),
                new ItemLoot("Spirit Salve Tome", 0.02)
            )
            .Init("Mummy Pharaoh",
                new State(
                    new Prioritize(
                        new Protect(1, "Lich", protectionRange: 10),
                        new Follow(1.2, range: 7),
                        new Wander(0.4)
                        ),
                    new Shoot(10)
                    ),
                new ItemLoot("Hell's Fire Wand", 0.02),
                new ItemLoot("Slayer Staff", 0.02),
                new ItemLoot("Golden Sword", 0.02),
                new ItemLoot("Golden Dagger", 0.02)
            )

        #endregion

        #region OASIS
        .Init("Oasis Giant",
                new State(
                    new Shoot(10, 4, 7, predictive: 1),
                    new Prioritize(
                        new StayCloseToSpawn(0.3, 2),
                        new Wander(0.4)
                        ),
                    new SpawnGroup("Oasis", 20, coolDown: 5000),
                    new Taunt(0.7, 10000,
                        "Come closer, {PLAYER}! Yes, closer!",
                        "I rule this place, {PLAYER}!",
                        "Surrender to my aquatic army, {PLAYER}!",
                        "You must be thirsty, {PLAYER}. Enter my waters!",
                        "Minions! We shall have {PLAYER} for dinner!"
                        )
                    ),
                new Threshold(0.01,
                    new ItemLoot("Water Drop Orb", 0.02)
                    )
            )
            .Init("Oasis Ruler",
                new State(
                    new Prioritize(
                        new Protect(0.5, "Oasis Giant", 15, 10, 3),
                        new Follow(1, range: 9),
                        new Wander(0.5)
                        ),
                    new Shoot(10)
                    ),
                new ItemLoot("Magic Potion", 0.05)
            )
            .Init("Oasis Soldier",
                new State(
                    new Prioritize(
                        new Protect(0.5, "Oasis Giant", 15, 11, 3),
                        new Follow(1, range: 7),
                        new Wander(0.5)
                        ),
                    new Shoot(10, predictive: 0.5)
                    ),
                new ItemLoot("Health Potion", 0.05)
            )
            .Init("Oasis Creature",
                new State(
                    new Prioritize(
                        new Protect(0.5, "Oasis Giant", 15, 12, 3),
                        new Follow(1, range: 5),
                        new Wander(0.5)
                        ),
                    new Shoot(10, coolDown: 400)
                    ),
                new ItemLoot("Health Potion", 0.05)
            )
            .Init("Oasis Monster",
                new State(
                    new Prioritize(
                        new Protect(0.5, "Oasis Giant", 15, 13, 3),
                        new Follow(1, range: 3),
                        new Wander(0.5)
                        ),
                    new Shoot(10, predictive: 0.5)
                    ),
                new ItemLoot("Magic Potion", 0.05)
            )
        #endregion

        #region PHOENIX
        .Init("Phoenix Lord",
                new State(
                    new Shoot(10, 4, 7, predictive: 0.5, coolDown: 600),
                    new Prioritize(
                        new StayCloseToSpawn(0.3, 2),
                        new Wander(0.4)
                        ),
                    new SpawnGroup("Pyre", 16, coolDown: 5000),
                    new Taunt(0.7, 10000,
                        "Alas, {PLAYER}, you will taste death but once!",
                        "I have met many like you, {PLAYER}, in my thrice thousand years!",
                        "Purge yourself, {PLAYER}, in the heat of my flames!",
                        "The ashes of past heroes cover my plains!",
                        "Some die and are ashes, but I am ever reborn!"
                        ),
                    new TransformOnDeath("Phoenix Egg")
                    )
            )
            .Init("Birdman Chief",
                new State(
                    new Prioritize(
                        new Protect(0.5, "Phoenix Lord", 15, 10, 3),
                        new Follow(1, range: 9),
                        new Wander(0.5)
                        ),
                    new Shoot(10)
                    ),
                new ItemLoot("Magic Potion", 0.05)
            )
            .Init("Birdman",
                new State(
                    new Prioritize(
                        new Protect(0.5, "Phoenix Lord", 15, 11, 3),
                        new Follow(1, range: 7),
                        new Wander(0.5)
                        ),
                    new Shoot(10, predictive: 0.5)
                    ),
                new ItemLoot("Health Potion", 0.05)
            )
            .Init("Phoenix Egg",
                new State(
                    new State("shielded",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TimedTransition(2000, "unshielded")
                        ),
                    new State("unshielded",
                        new Flash(0xff0000, 1, 5000),
                        new State("grow",
                            new ChangeSize(20, 150),
                            new TimedTransition(800, "shrink")
                            ),
                        new State("shrink",
                            new ChangeSize(-20, 130),
                            new TimedTransition(800, "grow")
                            )
                        ),
                    new TransformOnDeath("Phoenix Reborn")
                    )
            )
            .Init("Phoenix Reborn",
                new State(
                    new Prioritize(
                        new StayCloseToSpawn(0.9, 5),
                        new Wander(0.9)
                        ),
                    new SpawnGroup("Pyre", 4, coolDown: 1000),
                    new State("born_anew",
                        new Shoot(10, projectileIndex: 0, count: 12, shootAngle: 30, fixedAngle: 10, coolDown: 100000,
                            coolDownOffset: 500),
                        new Shoot(10, projectileIndex: 0, count: 12, shootAngle: 30, fixedAngle: 25, coolDown: 100000,
                            coolDownOffset: 1000),
                        new TimedTransition(1800, "xxx")
                        ),
                    new State("xxx",
                        new Shoot(10, projectileIndex: 1, count: 4, shootAngle: 7, predictive: 0.5, coolDown: 600),
                        new TimedTransition(2800, "yyy")
                        ),
                    new State("yyy",
                        new Shoot(10, projectileIndex: 0, count: 12, shootAngle: 30, fixedAngle: 10, coolDown: 100000,
                            coolDownOffset: 500),
                        new Shoot(10, projectileIndex: 0, count: 12, shootAngle: 30, fixedAngle: 25, coolDown: 100000,
                            coolDownOffset: 1000),
                        new Shoot(10, projectileIndex: 1, predictive: 0.5, coolDown: 350),
                        new TimedTransition(4500, "xxx")
                        )
                    ),
                new ItemLoot("Wine Cellar Incantation", 0.002)
            )

        #endregion

        #region ENTANCIENT
        .Init("Ent Ancient",
                new State(
                    new State("Idle",
                        new StayCloseToSpawn(0.1, 6),
                        new Wander(0.1),
                        new HpLessTransition(0.99999, "EvaluationStart1")
                        ),
                    new State("EvaluationStart1",
                        new Taunt("Uhh. So... sleepy..."),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Prioritize(
                            new StayCloseToSpawn(0.2, 3),
                            new Wander(0.2)
                            ),
                        new TimedTransition(2500, "EvaluationStart2")
                        ),
                    new State("EvaluationStart2",
                        new Flash(0x0000ff, 0.1, 60),
                        new Shoot(10, 2, 180, coolDown: 800),
                        new Prioritize(
                            new StayCloseToSpawn(0.3, range: 5),
                            new Wander(0.3)
                            ),
                        new HpLessTransition(0.87, "EvaluationEnd"),
                        new TimedTransition(6000, "EvaluationEnd")
                        ),
                    new State("EvaluationEnd",
                        new HpLessTransition(0.875, "HugeMob"),
                        new HpLessTransition(0.952, "Mob"),
                        new HpLessTransition(0.985, "SmallGroup"),
                        new HpLessTransition(0.99999, "Solo")
                        ),
                    new State("HugeMob",
                        new Taunt("You are many, yet the sum of your years is nothing."),
                        new Spawn("Greater Nature Sprite", 6, 0, 400),
                        new TossObject("Ent", 3, 0, 100000),
                        new TossObject("Ent", 3, 180, 100000),
                        new TossObject("Ent", 5, 10, 100000),
                        new TossObject("Ent", 5, 190, 100000),
                        new TossObject("Ent", 5, 70, 100000),
                        new TossObject("Ent", 7, 20, 100000),
                        new TossObject("Ent", 7, 200, 100000),
                        new TossObject("Ent", 7, 80, 100000),
                        new TossObject("Ent", 10, 30, 100000),
                        new TossObject("Ent", 10, 210, 100000),
                        new TossObject("Ent", 10, 90, 100000),
                        new TimedTransition(5000, "Wait")
                        ),
                    new State("Mob",
                        new Taunt("Little flies, little flies... we will swat you."),
                        new Spawn("Greater Nature Sprite", 3, 0, 1000),
                        new TossObject("Ent", 3, 0, 100000),
                        new TossObject("Ent", 4, 180, 100000),
                        new TossObject("Ent", 5, 10, 100000),
                        new TossObject("Ent", 6, 190, 100000),
                        new TossObject("Ent", 7, 20, 100000),
                        new TimedTransition(5000, "Wait")
                        ),
                    new State("SmallGroup",
                        new Taunt("It will be trivial to dispose of you."),
                        new Spawn("Greater Nature Sprite", 1, 1, 100000),
                        new TossObject("Ent", 3, 0, 100000),
                        new TossObject("Ent", 4.5, 180, 100000),
                        new TimedTransition(3000, "Wait")
                        ),
                    new State("Solo",
                        new Taunt("Mmm? Did you say something, mortal?"),
                        new TimedTransition(3000, "Wait")
                        ),
                    new State("Wait",
                        new Transform("Actual Ent Ancient")
                        )
                    )
            )
            .Init("Actual Ent Ancient",
                new State(
                    new Prioritize(
                        new StayCloseToSpawn(0.2, 6),
                        new Wander(0.2)
                        ),
                    new Spawn("Ent Sapling", 3, 0, 3000),
                    new State("Start",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ChangeSize(11, 160),
                        new Shoot(10, projectileIndex: 0, count: 1),
                        new TimedTransition(1600, "Growing1"),
                        new HpLessTransition(0.9, "Growing1")
                        ),
                    new State("Growing1",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ChangeSize(11, 180),
                        new Shoot(10, projectileIndex: 1, count: 3, shootAngle: 120),
                        new TimedTransition(1600, "Growing2"),
                        new HpLessTransition(0.8, "Growing2")
                        ),
                    new State("Growing2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ChangeSize(11, 200),
                        new Taunt(0.35, "Little mortals, your years are my minutes."),
                        new Shoot(10, projectileIndex: 2, count: 1),
                        new TimedTransition(1600, "Growing3"),
                        new HpLessTransition(0.7, "Growing3")
                        ),
                    new State("Growing3",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ChangeSize(11, 220),
                        new Shoot(10, projectileIndex: 3, count: 1),
                        new TimedTransition(1600, "Growing4"),
                        new HpLessTransition(0.6, "Growing4")
                        ),
                    new State("Growing4",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ChangeSize(11, 240),
                        new Taunt(0.35, "No axe can fell me!"),
                        new Shoot(10, projectileIndex: 4, count: 3, shootAngle: 120),
                        new TimedTransition(1600, "Growing5"),
                        new HpLessTransition(0.5, "Growing5")
                        ),
                    new State("Growing5",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ChangeSize(11, 260),
                        new Shoot(10, projectileIndex: 5, count: 1),
                        new TimedTransition(1600, "Growing6"),
                        new HpLessTransition(0.45, "Growing6")
                        ),
                    new State("Growing6",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ChangeSize(11, 280),
                        new Taunt(0.35, "Yes, YES..."),
                        new Shoot(10, projectileIndex: 6, count: 1),
                        new TimedTransition(1600, "Growing7"),
                        new HpLessTransition(0.4, "Growing7")
                        ),
                    new State("Growing7",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ChangeSize(11, 300),
                        new Shoot(10, projectileIndex: 7, count: 3, shootAngle: 120),
                        new TimedTransition(1600, "Growing8"),
                        new HpLessTransition(0.36, "Growing8")
                        ),
                    new State("Growing8",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ChangeSize(11, 320),
                        new Taunt(0.35, "I am the FOREST!!"),
                        new Shoot(10, projectileIndex: 8, count: 1),
                        new TimedTransition(1600, "Growing9"),
                        new HpLessTransition(0.32, "Growing9")
                        ),
                    new State("Growing9",
                        new ChangeSize(11, 340),
                        new Taunt(1.0, "YOU WILL DIE!!!"),
                        new Shoot(10, projectileIndex: 9, count: 1),
                        new State("convert_sprites",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Order(50, "Greater Nature Sprite", "Transform"),
                            new TimedTransition(2000, "shielded")
                            ),
                        new State("received_armor",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new ConditionalEffect(ConditionEffectIndex.Armored, true),
                            new TimedTransition(1000, "shielded")
                            ),
                        new State("shielded",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new TimedTransition(4000, "unshielded")
                            ),
                        new State("unshielded",
                            new Shoot(10, projectileIndex: 3, count: 3, shootAngle: 120, coolDown: 700),
                            new TimedTransition(4000, "shielded")
                            )
                        )
                    ),
                new Threshold(0.15,
                new TierLoot(2, ItemType.Ring, 0.15),
                new TierLoot(3, ItemType.Ring, 0.04),
                new TierLoot(6, ItemType.Weapon, 0.3),
                new TierLoot(7, ItemType.Weapon, 0.1),
                new TierLoot(6, ItemType.Armor, 0.3),
                new TierLoot(7, ItemType.Armor, 0.1),
                new TierLoot(1, ItemType.Ability, 0.95),
                new TierLoot(2, ItemType.Ability, 0.25),
                new TierLoot(3, ItemType.Ability, 0.05),
                new ItemLoot("Health Potion", 0.7),
                new ItemLoot("Magic Potion", 0.7)
                    )
            )
            .Init("Ent",
                new State(
                    new Prioritize(
                        new Protect(0.25, "Ent Ancient", 12, 7, 7),
                        new Follow(0.25, range: 1, acquireRange: 9),
                        new Shoot(10, 5, 72, fixedAngle: 30, coolDown: 1600, coolDownOffset: 800)
                        ),
                    new Shoot(10, predictive: 0.4, coolDown: 600),
                    new Decay(90000)
                    ),
                new ItemLoot("Tincture of Dexterity", 0.02)
            )
            .Init("Ent Sapling",
                new State(
                    new Prioritize(
                        new Protect(0.55, "Ent Ancient", 10, 4, 4),
                        new Wander(0.55)
                        ),
                    new Shoot(10, coolDown: 1000)
                    )
            )
            .Init("Greater Nature Sprite",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Shoot(10, 4, 10),
                    new Prioritize(
                        new StayCloseToSpawn(1.5, 11),
                        new Orbit(1.5, 4, 7),
                        new Follow(200, 7, 2),
                        new Follow(0.3, 7, 0.2)
                        ),
                    new State("Idle"),
                    new State("Transform",
                        new Transform("Actual Greater Nature Sprite")
                        ),
                    new Decay(90000)
                    )
            )
            .Init("Actual Greater Nature Sprite",
                new State(
                    new Flash(0xff484848, 0.6, 1000),
                    new Spawn("Ent", 2, 0, 3000),
                    new Heal(15, "Heros", 200),
                    new State("armor_ent_ancient",
                        new Order(30, "Actual Ent Ancient", "received_armor"),
                        new TimedTransition(1000, "last_fight")
                        ),
                    new State("last_fight",
                        new Shoot(10, 4, 10),
                        new Prioritize(
                            new StayCloseToSpawn(1.5, 11),
                            new Orbit(1.5, 4, 7),
                            new Follow(200, 7, 2),
                            new Follow(0.3, 7, 0.2)
                            )
                        ),
                    new Decay(60000)
                    ),
                new Threshold(0.15,
                new ItemLoot("Magic Potion", 0.25),
                new ItemLoot("Tincture of Life", 0.06),
                new ItemLoot("Green Drake Egg", 0.08),
                new ItemLoot("Quiver of Thunder", 0.007),
                new TierLoot(8, ItemType.Armor, 0.3)
                    )
            )
        #endregion

        #region REDDEMON
        .Init("Red Demon",
                new State(
                    new DropPortalOnDeath("Abyss of Demons Portal", 20),
                    new Shoot(10, projectileIndex: 0, count: 5, shootAngle: 5, predictive: 1, coolDown: 1200),
                    new Shoot(11, projectileIndex: 1, coolDown: 1400),
                    new Prioritize(
                        new StayCloseToSpawn(0.8, 5),
                        new Follow(1, range: 7),
                        new Wander(0.4)
                        ),
                    new Spawn("Imp", 5, coolDown: 10000),
                    new Spawn("Demon", 3, coolDown: 14000),
                    new Spawn("Demon Warrior", 3, coolDown: 18000),
                    new Taunt(0.7, 10000,
                        "I will deliver your soul to Oryx, {PLAYER}!",
                        "Oryx will not end our pain. We can only share it... with you!",
                        "Our anguish is endless, unlike your lives!",
                        "There can be no forgiveness!",
                        "What do you know of suffering? I can teach you much, {PLAYER}",
                        "Would you attempt to destroy us? I know your name, {PLAYER}!",
                        "You cannot hurt us. You cannot help us. You will feed us.",
                        "Your life is an affront to Oryx. You will die."
                        )
                    ),
                new ItemLoot("Golden Sword", 0.04),
                new ItemLoot("Ring of Greater Defense", 0.04),
                new ItemLoot("Potion of Speed", 0.03),
                new ItemLoot("Lava Helmet", 0.01),
                new ItemLoot("Steel Helm", 0.04)
            )
            .Init("Imp",
                new State(
                    new Prioritize(
                        new StayCloseToSpawn(1.4, 15),
                        new Wander(0.8)
                        ),
                    new Shoot(10, predictive: 0.5, coolDown: 200)
                    ),
                new ItemLoot("Missile Wand", 0.02),
                new ItemLoot("Serpentine Staff", 0.02),
                new ItemLoot("Fire Bow", 0.02)
            )
            .Init("Demon",
                new State(
                    new Prioritize(
                        new StayCloseToSpawn(1.4, 15),
                        new Follow(1.4, range: 7),
                        new Wander(0.4)
                        ),
                    new Shoot(10, 2, 7, predictive: 0.5)
                    ),
                new ItemLoot("Fire Bow", 0.03)
            )
            .Init("Demon Warrior",
                new State(
                    new Prioritize(
                        new StayCloseToSpawn(1.4, 15),
                        new Follow(1, range: 2.8),
                        new Wander(0.4)
                        ),
                    new Shoot(10, 3, 7, predictive: 0.5)
                    ),
                new ItemLoot("Obsidian Dagger", 0.03),
                new ItemLoot("Steel Shield", 0.02)
        #endregion

            );
    }
}