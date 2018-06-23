#region

using System;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

#endregion

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Events = () => Behav()
        #region CRYSTAL
            .Init("Mysterious Crystal",
                new State(
                    //new DropPortalOnDeath("Deadwater Docks", 100),
                    new State("Idle",
                        new Taunt(0.1, "Break the crystal for great rewards..."),
                        new Taunt(0.1, "Help me..."),
                        new HpLessTransition(0.9999, "Instructions"),
                        new TimedTransition(10000, "Idle")
                        ),
                    new State("Instructions",
                        new Flash(0xffffffff, 2, 100),
                        new Taunt(0.8, "Fire upon this crystal with all your might for 5 seconds"),
                        new Taunt(0.8, "If your attacks are weak, the crystal magically heals"),
                        new Taunt(0.8, "Gather a large group to smash it open"),
                        new Taunt(0.8, "Strike this encasement down to let me free!"),
                        new HpLessTransition(0.998, "Evaluation")
                        ),
                    new State("Evaluation",
                        new State("Comment1",
                            new Taunt(true, "Sweet treasure awaits for powerful adventurers!"),
                            new Taunt(0.4, "Yes!  Smash my prison for great rewards!", "I can taste freedom like it was a popsicle"),
                            new TimedTransition(5000, "Comment2")
                            ),
                        new State("Comment2",
                            new Taunt(0.3, "If you are not very strong, this could kill you",
                                "If you are not yet powerful, stay away from the Crystal",
                                "New adventurers should stay away",
                                "That's the spirit. Lay your fire upon me.",
                                "So close...",
                                "I was almost free..."
                                ),
                            new TimedTransition(5000, "Comment3")
                            ),
                        new State("Comment3",
                            new Taunt(0.4, "I think you need more people...",
                                "Call all your friends to help you break the crystal!"
                                ),
                            new TimedTransition(10000, "Comment2")
                            ),
                        new Heal(1, "Crystals", 5000),
                        new HpLessTransition(0.95, "StartBreak"),
                        new TimedTransition(60000, "Fail")
                        ),
                    new State("Fail",
                        new Taunt("Perhaps you need a bigger group. Ask others to join you!"),
                        new Flash(0xff000000, 5, 1),
                        new Shoot(10, 16, 22.5, fixedAngle: 0, coolDown: 100000),
                        new Heal(1, "Crystals", 1000),
                        new TimedTransition(5000, "Idle")
                        ),
                    new State("StartBreak",
                        new Taunt("You cracked the crystal! Soon we shall emerge!"),
                        new ChangeSize(-2, 80),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0xff000000, 2, 10),
                        new TimedTransition(4000, "BreakCrystal")
                        ),
                    new State("BreakCrystal",
                        new Taunt("This your reward! Imagine what evil even Oryx needs to keep locked up!"),
                        new Shoot(0, 16, 22.5, fixedAngle: 0, coolDown: 100000),
                        new Spawn("Crystal Prisoner", 1, 1, 100000),
                        new Decay(0)
                        )
                    )
            )
            .Init("Crystal Prisoner",
                new State(
                    new DropPortalOnDeath("Deadwater Docks", 100),
                    new Spawn("Crystal Prisoner Steed", 5, 0, 200),
                    new State("pause",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TimedTransition(2000, "start_the_fun")
                        ),
                    new State("start_the_fun",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Taunt("I'm finally free! Yesss!!!"),
                        new TimedTransition(1500, "Daisy_attack")
                        ),
                    new State("Daisy_attack",
                        new Prioritize(
                            new StayCloseToSpawn(0.3, 7),
                            new Wander(0.3)
                            ),
                        new State("Quadforce1",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 0, coolDown: 300),
                            new TimedTransition(200, "Quadforce2")
                            ),
                        new State("Quadforce2",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 15, coolDown: 300),
                            new TimedTransition(200, "Quadforce3")
                            ),
                        new State("Quadforce3",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 30, coolDown: 300),
                            new TimedTransition(200, "Quadforce4")
                            ),
                        new State("Quadforce4",
                            new Shoot(10, projectileIndex: 3, coolDown: 1000),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 45, coolDown: 300),
                            new TimedTransition(200, "Quadforce5")
                            ),
                        new State("Quadforce5",
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 60, coolDown: 300),
                            new TimedTransition(200, "Quadforce6")
                            ),
                        new State("Quadforce6",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 75, coolDown: 300),
                            new TimedTransition(200, "Quadforce7")
                            ),
                        new State("Quadforce7",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 90, coolDown: 300),
                            new TimedTransition(200, "Quadforce8")
                            ),
                        new State("Quadforce8",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Shoot(10, projectileIndex: 3, coolDown: 1000),
                            new Shoot(0, projectileIndex: 0, count: 4, shootAngle: 90, fixedAngle: 105, coolDown: 300),
                            new TimedTransition(200, "Quadforce1")
                            ),
                        new HpLessTransition(0.3, "Whoa_nelly"),
                        new TimedTransition(18000, "Warning")
                        ),
                    new State("Warning",
                        new Prioritize(
                            new StayCloseToSpawn(0.5, 7),
                            new Wander(0.5)
                            ),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0xff00ff00, 0.2, 15),
                        new Follow(0.4, 9, 2),
                        new TimedTransition(3000, "Summon_the_clones")
                        ),
                    new State("Summon_the_clones",
                        new Prioritize(
                            new StayCloseToSpawn(0.85, 7),
                            new Wander(0.85)
                            ),
                        new Shoot(10, projectileIndex: 0, coolDown: 1000),
                        new Spawn("Crystal Prisoner Clone", 4, 0, 200),
                        new TossObject("Crystal Prisoner Clone", 5, 0, 100000),
                        new TossObject("Crystal Prisoner Clone", 5, 240, 100000),
                        new TossObject("Crystal Prisoner Clone", 7, 60, 100000),
                        new TossObject("Crystal Prisoner Clone", 7, 300, 100000),
                        new State("invulnerable_clone",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new TimedTransition(3000, "vulnerable_clone")
                            ),
                        new State("vulnerable_clone",
                            new TimedTransition(1200, "invulnerable_clone")
                            ),
                        new TimedTransition(16000, "Warning2")
                        ),
                    new State("Warning2",
                        new Prioritize(
                            new StayCloseToSpawn(0.85, 7),
                            new Wander(0.85)
                            ),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0xff00ff00, 0.2, 25),
                        new TimedTransition(5000, "Whoa_nelly")
                        ),
                    new State("Whoa_nelly",
                        new Prioritize(
                            new StayCloseToSpawn(0.6, 7),
                            new Wander(0.6)
                            ),
                        new Shoot(10, projectileIndex: 3, count: 3, shootAngle: 120, coolDown: 900),
                        new Shoot(10, projectileIndex: 2, count: 3, shootAngle: 15, fixedAngle: 40, coolDown: 1600,
                            coolDownOffset: 0),
                        new Shoot(10, projectileIndex: 2, count: 3, shootAngle: 15, fixedAngle: 220, coolDown: 1600,
                            coolDownOffset: 0),
                        new Shoot(10, projectileIndex: 2, count: 3, shootAngle: 15, fixedAngle: 130, coolDown: 1600,
                            coolDownOffset: 800),
                        new Shoot(10, projectileIndex: 2, count: 3, shootAngle: 15, fixedAngle: 310, coolDown: 1600,
                            coolDownOffset: 800),
                        new State("invulnerable_whoa",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new TimedTransition(2600, "vulnerable_whoa")
                            ),
                        new State("vulnerable_whoa",
                            new TimedTransition(1200, "invulnerable_whoa")
                            ),
                        new TimedTransition(10000, "Absolutely_Massive")
                        ),
                    new State("Absolutely_Massive",
                        new ChangeSize(13, 260),
                        new Prioritize(
                            new StayCloseToSpawn(0.2, 7),
                            new Wander(0.2)
                            ),
                        new Shoot(10, projectileIndex: 1, count: 9, shootAngle: 40, fixedAngle: 40, coolDown: 2000,
                            coolDownOffset: 400),
                        new Shoot(10, projectileIndex: 1, count: 9, shootAngle: 40, fixedAngle: 60, coolDown: 2000,
                            coolDownOffset: 800),
                        new Shoot(10, projectileIndex: 1, count: 9, shootAngle: 40, fixedAngle: 50, coolDown: 2000,
                            coolDownOffset: 1200),
                        new Shoot(10, projectileIndex: 1, count: 9, shootAngle: 40, fixedAngle: 70, coolDown: 2000,
                            coolDownOffset: 1600),
                        new State("invulnerable_mass",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new TimedTransition(2600, "vulnerable_mass")
                            ),
                        new State("vulnerable_mass",
                            new TimedTransition(1000, "invulnerable_mass")
                            ),
                        new TimedTransition(14000, "Start_over_again")
                        ),
                    new State("Start_over_again",
                        new ChangeSize(-20, 100),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0xff00ff00, 0.2, 15),
                        new TimedTransition(3000, "Daisy_attack")
                        )
                    ),
                new MostDamagers(3,
                    LootTemplates.StatIncreasePotionsLoot()
                ),
                new Threshold(0.05,
                    new ItemLoot("Crystal Wand", 0.05),
                    new ItemLoot("Crystal Sword", 0.05)
                    )
            )
            .Init("Crystal Prisoner Clone",
                new State(
                    new Prioritize(
                        new StayCloseToSpawn(0.85, range: 5),
                        new Wander(0.85)
                        ),
                    new Shoot(10, coolDown: 1400),
                    new State("taunt",
                        new Taunt(0.09, "I am everywhere and nowhere!"),
                        new TimedTransition(1000, "no_taunt")
                        ),
                    new State("no_taunt",
                        new TimedTransition(1000, "taunt")
                        ),
                    new Decay(17000)
                    )
            )
            .Init("Crystal Prisoner Steed",
                new State(
                    new State("change_position_fast",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Prioritize(
                            new StayCloseToSpawn(3.6, 12),
                            new Wander(3.6)
                            ),
                        new TimedTransition(800, "attack")
                        ),
                    new State("attack",
                        new Shoot(10, predictive: 0.3, coolDown: 500),
                        new State("keep_distance",
                            new Prioritize(
                                new StayCloseToSpawn(1, 12),
                                new Orbit(1, 9, target: "Crystal Prisoner", radiusVariance: 0)
                                ),
                            new TimedTransition(2000, "go_anywhere")
                            ),
                        new State("go_anywhere",
                            new Prioritize(
                                new StayCloseToSpawn(1, 12),
                                new Wander(1)
                                ),
                            new TimedTransition(2000, "keep_distance")
                            )
                        )
                    )
            )
        #endregion CRYSTAL

        #region CUBEGOD
            .Init("Cube God",
                new State(
                    new StayCloseToSpawn(0.3, range: 7),
                           new Wander(0.5),
                             new Shoot(10, count: 9, predictive: 0.9, shootAngle: 6.5, coolDown: 1000),
                             new Shoot(10, count: 6, predictive: 0.9, shootAngle: 6.5, projectileIndex: 1, coolDown: 1000, coolDownOffset: 200),
                             new Spawn("Cube Overseer", maxChildren: 5, initialSpawn: 3, coolDown: 100000),
                             new Spawn("Cube Defender", maxChildren: 5, initialSpawn: 5, coolDown: 100000),
                             new Spawn("Cube Blaster", maxChildren: 5, initialSpawn: 5, coolDown: 100000)
                ),
                                new MostDamagers(3,
                    new ItemLoot("Greater Potion of Vitality", 1)
                ),
                new Threshold(0.15,
                    new TierLoot(3, ItemType.Ring, 0.2),
                    new TierLoot(7, ItemType.Armor, 0.2),
                    new TierLoot(8, ItemType.Weapon, 0.2),
                    new TierLoot(4, ItemType.Ability, 0.1),
                    new TierLoot(8, ItemType.Armor, 0.1),
                    new TierLoot(4, ItemType.Ring, 0.05),
                    new TierLoot(9, ItemType.Armor, 0.03),
                    new TierLoot(5, ItemType.Ability, 0.03),
                    new TierLoot(9, ItemType.Weapon, 0.03),
                    new TierLoot(10, ItemType.Armor, 0.02),
                    new TierLoot(10, ItemType.Weapon, 0.02),
                    new TierLoot(11, ItemType.Armor, 0.01),
                    new TierLoot(11, ItemType.Weapon, 0.01),
                    new TierLoot(5, ItemType.Ring, 0.01),
                    new ItemLoot("Dirk of Cronus", 0.01),
                    new ItemLoot("Gladius of the Cubes", 0.005),
                    new ItemLoot("Greater Potion of Speed", 1)
                )
            )
            .Init("Cube Overseer",
                new State(
                    new StayCloseToSpawn(0.3, range: 7),
                             new Wander(1),
                             new Shoot(10, count: 4, predictive: 0.9, projectileIndex: 0, coolDown: 1250)
                )
            )
            .Init("Cube Defender",
                new State(
                    new Wander(0.5),
                             new StayCloseToSpawn(0.03, range: 7),
                             new Follow(0.4, acquireRange: 9, range: 2),
                             new Shoot(10, count: 1, coolDown: 1000, predictive: 0.9, projectileIndex: 0)
                )
            )
            .Init("Cube Blaster",
                new State(
                    new Wander(0.5),
                             new StayCloseToSpawn(0.03, range: 7),
                             new Follow(0.4, acquireRange: 9, range: 2),
                             new Shoot(10, count: 2, predictive: 0.9, projectileIndex: 0, coolDown: 1500),
                             new Shoot(10, count: 1, predictive: 0.9, projectileIndex: 0, coolDown: 1500)
                )
            )

        #endregion CUBEGOD

        #region SKULLSHRINE
        .Init("Skull Shrine",
                new State(
                    new Shoot(25, 9, 10, predictive: 1),
                    new Spawn("Red Flaming Skull", 8, coolDown: 5000),
                    new Spawn("Blue Flaming Skull", 10, coolDown: 1000),
                    new Reproduce("Red Flaming Skull", 10, 8, 5000),
                    new Reproduce("Blue Flaming Skull", 10, 10, 1000)
                ),
                new MostDamagers(3,
                    LootTemplates.StatIncreasePotionsLoot()
                ),
                new Threshold(0.05,
                    new TierLoot(8, ItemType.Weapon, 0.2),
                    new TierLoot(9, ItemType.Weapon, 0.03),
                    new TierLoot(10, ItemType.Weapon, 0.02),
                    new TierLoot(11, ItemType.Weapon, 0.01),
                    new TierLoot(3, ItemType.Ring, 0.2),
                    new TierLoot(4, ItemType.Ring, 0.05),
                    new TierLoot(5, ItemType.Ring, 0.01),
                    new TierLoot(7, ItemType.Armor, 0.2),
                    new TierLoot(8, ItemType.Armor, 0.1),
                    new TierLoot(9, ItemType.Armor, 0.03),
                    new TierLoot(10, ItemType.Armor, 0.02),
                    new TierLoot(11, ItemType.Armor, 0.01),
                    new TierLoot(4, ItemType.Ability, 0.1),
                    new TierLoot(5, ItemType.Ability, 0.03),
                    new ItemLoot("Sword Of The Shrines", 0.025),
                    new ItemLoot("Orb of Conflict", 0.025)
                )
            )
            .Init("Red Flaming Skull",
                new State(
                    new Prioritize(
                        new Wander(.6),
                        new Follow(.6, 20, 3)
                        ),
                    new Shoot(15, 2, 5, 0, predictive: 1, coolDown: 750)
                    )
            )
            .Init("Blue Flaming Skull",
                new State(
                    new Prioritize(
                        new Orbit(1, 20, target: "Skull Shrine", radiusVariance: 0.5),
                        new Wander(.6)
                        ),
                    new Shoot(15, 2, 5, 0, predictive: 1, coolDown: 750)
                    )
            )

        #endregion

        #region HERMITGOD

           /* .Init("Hermit God",
                new State(
                    new InvisiToss("Hermit God Drop", 6, 0, 90000001, coolDownOffset: 0),
                    new CopyDamageOnDeath("Hermit God Drop"),
                    new DropPortalOnDeath("Ocean Trench Portal", 100, XAdjustment: 5, YAdjustment: 5),
                    new State("invis",
                        new SetAltTexture(3),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new InvisiToss("Hermit Minion", 9, 0, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 45, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 90, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 135, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 180, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 225, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 270, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 315, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 15, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 30, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 90, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 120, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 150, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 180, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 210, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 240, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 50, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 100, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 150, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 200, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 250, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 300, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 45, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 90, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 135, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 180, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 225, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 270, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 315, 90000001, coolDownOffset: 0),
                        new TimedTransition(1000, "check")
                        ),
                    new State("check",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new EntityNotExistsTransition("Hermit God Tentacle", 20, "active")
                        ),
                    new State("active",
                        new SetAltTexture(2),
                        new TimedTransition(500, "active2")
                        ),
                    new State("active2",
                        new SetAltTexture(0),
                        new Shoot(25, 3, 10, 0, coolDown: 200),
                        new Wander(.2),
                        new TossObject("Whirlpool", 6, 0, 90000001, 100),
                        new TossObject("Whirlpool", 6, 45, 90000001, 100),
                        new TossObject("Whirlpool", 6, 90, 90000001, 100),
                        new TossObject("Whirlpool", 6, 135, 90000001, 100),
                        new TossObject("Whirlpool", 6, 180, 90000001, 100),
                        new TossObject("Whirlpool", 6, 225, 90000001, 100),
                        new TossObject("Whirlpool", 6, 270, 90000001, 100),
                        new TossObject("Whirlpool", 6, 315, 90000001, 100),
                        new TimedTransition(10000, "rage")
                        ),
                    new State("rage",
                        new SetAltTexture(4),
                        new Order(20, "Whirlpool", "despawn"),
                        new Flash(0xfFF0000, .8, 9000001),
                        new Shoot(25, 8, projectileIndex: 1, coolDown: 2000),
                        new Shoot(25, 20, projectileIndex: 2, coolDown: 3000, coolDownOffset: 5000),
                        new TimedTransition(17000, "invis")
                        )
                    )
            )
            .Init("Whirlpool",
                new State(
                    new State("active",
                        new Shoot(25, 8, projectileIndex: 0, coolDown: 1000),
                        new Orbit(.5, 4, target: "Hermit God", radiusVariance: 0),
                        new EntityNotExistsTransition("Hermit God", 50, "despawn")
                        ),
                    new State("despawn",
                        new Suicide()
                        )
                    )
            )
            .Init("Hermit God Tentacle",
                new State(
                    new Prioritize(
                        new Orbit(.5, 5, target: "Hermit God", radiusVariance: 0.5),
                        new Follow(0.85, range: 1, duration: 2000, coolDown: 0)
                        ),
                    new Shoot(4, 8, projectileIndex: 0, coolDown: 1000)
                    ),
                new Threshold(0.05,
                    new ItemLoot("Ring of the Knowledge Seeker", 0.01)
                    )
            )
            .Init("Hermit Minion",
                new State(
                    new Prioritize(
                        new Wander(.5),
                        new Follow(0.85, 3, 1, 2000, 0)
                        ),
                    new Shoot(5, 1, 1, 1, coolDown: 2300),
                    new Shoot(5, 3, 1, 0, coolDown: 1000)
                    )
            )
            .Init("Hermit God Drop",
                new State(
                    new State("idle",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new EntityNotExistsTransition("Hermit God", 10, "despawn")
                        ),
                    new State("despawn",
                        new Suicide()
                        )
                    ),
                new MostDamagers(3,
                    new OnlyOne(
                        new ItemLoot("Greater Potion of Dexterity", 1),
                        new ItemLoot("Greater Potion of Vitality", 1)
                    )
                ),
                new Threshold(0.05,
                    new ItemLoot("Hide of Guidance", 0.01),
                    new ItemLoot("Quiver of Guidance", 0.01),
                    new ItemLoot("Bow of Guidance", 0.01),
                    new ItemLoot("Helm of the Juggernaut", 0.01)
                )
            )*/
        #endregion

        #region GHOSTSHIP

                        //made by omni the greatest rapper ever
                        .Init("Vengeful Spirit",
                    new State(
                        new State("Start",
                            new ChangeSize(50, 120),
                            new Prioritize(
                                new Follow(0.48, 8, 1),
                                new Wander(0.45)
                                ),
                            new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 16, coolDown: 1000),
                            new TimedTransition(1000, "Vengeful")
                            ),
                        new State("Vengeful",
                            new Prioritize(
                                new Follow(1, 8, 1),
                                new Wander(0.45)
                                ),
                            new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 16, coolDown: 1645),
                            new TimedTransition(3000, "Vengeful2")
                            ),
                            new State("Vengeful2",
                            new ReturnToSpawn(once: false, speed: 0.6),
                            new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 16, coolDown: 1500),
                            new TimedTransition(1500, "Vengeful")
                            )))
                       .Init("Water Mine",
                        new State(
                           new State("Seek",
                            new Prioritize(
                                new Follow(0.45, 8, 1),
                                new Wander(0.55)
                                ),
                            new TimedTransition(3750, "Boom")
                            ),
                            new State("Boom",
                            new Shoot(8.4, count: 10, projectileIndex: 0, coolDown: 1000),
                            new Suicide()
                     )))
                     .Init("Beach Spectre",
                        new State(
                           new State("Fight",
                               new Wander(0.03),
                           new ChangeSize(10, 120),
                           new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 14, coolDown: 1750)
                     )))

                     .Init("Beach Spectre Spawner",
                        new State(
                           new ConditionalEffect(ConditionEffectIndex.Invincible),
                           new State("Spawn",
                           new Reproduce("Beach Spectre", densityMax: 1, densityRadius: 3, spawnRadius: 1, coolDown: 1250)
                     )))
                      .Init("Tempest Cloud",
                        new State(
                            new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new State("Start1",
                           new ChangeSize(70, 130),
                           new TimedTransition(3000, "Start2")
                     ),
                        new State("Start2",
                           new SetAltTexture(1),
                           new TimedTransition(1, "Start3")
                     ),
                        new State("Start3",
                           new SetAltTexture(2),
                           new TimedTransition(1, "Start4")
                     ),
                         new State("Start4",
                           new SetAltTexture(3),
                           new TimedTransition(1, "Start5")
                     ),
                         new State("Start5",
                           new SetAltTexture(4),
                           new TimedTransition(1, "Start6")
                     ),
                         new State("Start6",
                           new SetAltTexture(5),
                           new TimedTransition(1, "Start7")
                     ),
                         new State("Start7",
                           new SetAltTexture(6),
                           new TimedTransition(1, "Start8")
                     ),
                         new State("Start8",
                           new SetAltTexture(7),
                           new TimedTransition(1, "Start9")
                     ),
                         new State("Start9",
                           new SetAltTexture(8),
                           new TimedTransition(1, "Final")
                     ),
                         new State("Final",
                           new SetAltTexture(9),
                           new TimedTransition(1, "CircleAndStorm")
                     ),
                         new State("CircleAndStorm",
                           new Orbit(0.25, 9, 20, "Ghost Ship Anchor", speedVariance: 0.1),
                           new Shoot(8.4, count: 7, projectileIndex: 0, coolDown: 1000)
                     )))
                    .Init("Ghost Ship Anchor",
                        new State(
                           new State("idle",
                           new ConditionalEffect(ConditionEffectIndex.Invincible)
                     ),
                        new State("tempestcloud",
                            new InvisiToss("Tempest Cloud", 9, 0, coolDown: 9999999),
                            new InvisiToss("Tempest Cloud", 9, 45, coolDown: 9999999),
                            new InvisiToss("Tempest Cloud", 9, 90, coolDown: 9999999),
                            new InvisiToss("Tempest Cloud", 9, 135, coolDown: 9999999),
                            new InvisiToss("Tempest Cloud", 9, 180, coolDown: 9999999),
                            new InvisiToss("Tempest Cloud", 9, 225, coolDown: 9999999),
                            new InvisiToss("Tempest Cloud", 9, 270, coolDown: 9999999),
                            new InvisiToss("Tempest Cloud", 9, 315, coolDown: 9999999),
                            new InvisiToss("Tempest Cloud", 9, 350, coolDown: 9999999),
                            new InvisiToss("Tempest Cloud", 9, 250, coolDown: 9999999),
                            new InvisiToss("Tempest Cloud", 9, 110, coolDown: 9999999),
                            new InvisiToss("Tempest Cloud", 9, 200, coolDown: 9999999),
                            new InvisiToss("Tempest Cloud", 9, 10, coolDown: 9999999),
                            new InvisiToss("Tempest Cloud", 9, 290, coolDown: 9999999),

                            //Spectre Spawner
                            new InvisiToss("Beach Spectre Spawner", 17, 0, coolDown: 9999999),
                            new InvisiToss("Beach Spectre Spawner", 17, 45, coolDown: 9999999),
                            new InvisiToss("Beach Spectre Spawner", 17, 90, coolDown: 9999999),
                            new InvisiToss("Beach Spectre Spawner", 17, 135, coolDown: 9999999),
                            new InvisiToss("Beach Spectre Spawner", 17, 180, coolDown: 9999999),
                            new InvisiToss("Beach Spectre Spawner", 17, 225, coolDown: 9999999),
                            new InvisiToss("Beach Spectre Spawner", 17, 270, coolDown: 9999999),
                            new InvisiToss("Beach Spectre Spawner", 17, 315, coolDown: 9999999),
                            new InvisiToss("Beach Spectre Spawner", 17, 250, coolDown: 9999999),
                            new InvisiToss("Beach Spectre Spawner", 17, 110, coolDown: 9999999),
                            new InvisiToss("Beach Spectre Spawner", 17, 200, coolDown: 9999999),
                           new ConditionalEffect(ConditionEffectIndex.Invincible)
                     )

                            ))
                        .Init("Ghost Ship",
                    new State(
                        new DropPortalOnDeath("Davy Jones' Locker Portal", 21),
                        new OnDeathBehavior(
                            new RemoveEntity(100, "Tempest Cloud")
                            ),
                        new OnDeathBehavior(
                            new RemoveEntity(100, "Beach Spectre")
                            ),
                         new OnDeathBehavior(
                            new RemoveEntity(100, "Beach Spectre Spawner")
                            ),
                        new State("idle",
                            new SetAltTexture(1),
                            new Wander(0.1),
                            new DamageTakenTransition(2000, "pause")
                            ),
                        new State("pause",
                             new SetAltTexture(2),
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new TimedTransition(2000, "start")
                            ),
                          new State("start",
                               new SetAltTexture(0),
                          new Reproduce("Vengeful Spirit", densityMax: 2, spawnRadius: 1, coolDown: 1000),
                          new TimedTransition(15000, "midfight"),
                           new State("2",
                            new SetAltTexture(0),
                            new Prioritize(
                                 new Wander(0.45),
                                 new StayBack(0.3, 5)
                                ),
                            new Shoot(8.4, count: 1, projectileIndex: 0, coolDown: 450),
                            new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 20, coolDown: 1750),
                            new TimedTransition(3250, "1")
                            ),
                         new State("1",
                            new TossObject("Water Mine", 5, coolDown: 1500),
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new ReturnToSpawn(once: false, speed: 0.4),
                            new Shoot(8.4, count: 1, projectileIndex: 0, coolDown: 450),
                            new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 20, coolDown: 1750),
                            new TimedTransition(1500, "2")
                             )
                            ),


                           new State("midfight",
                         new Order(100, "Ghost Ship Anchor", "tempestcloud"),
                          new Reproduce("Vengeful Spirit", densityMax: 1, spawnRadius: 1, coolDown: 1000),
                          new TossObject("Water Mine", 5, coolDown: 2250),
                          new TimedTransition(10000, "countdown"),
                           new State("2",
                            new SetAltTexture(0),
                            new ReturnToSpawn(once: false, speed: 0.4),
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Shoot(10, count: 4, projectileIndex: 0, coolDownOffset: 1100, angleOffset: 270, coolDown: 1250),
                            new Shoot(10, count: 4, projectileIndex: 0, coolDownOffset: 1100, angleOffset: 90, coolDown: 1250),
                            new Shoot(8.4, count: 1, projectileIndex: 1, coolDown: 1250),
                            new TimedTransition(3000, "1")
                            ),
                         new State("1",
                            new Prioritize(
                                 new Follow(0.45, 8, 1),
                                 new Wander(0.3)
                                ),
                            new Taunt(1.00, "Fire at will!"),
                            new Shoot(8.4, count: 2, shootAngle: 25, projectileIndex: 1, coolDown: 3850),
                            new Shoot(8.4, count: 6, projectileIndex: 0, shootAngle: 10, coolDown: 2750),
                            new TimedTransition(4000, "2")
                             )
                            ),
                        new State("countdown",
                            new Wander(0.1),
                            new Timed(1000,
                                new Taunt(1.00, "Ready..")
                                ),
                             new Timed(2000,
                                new Taunt(1.00, "Aim..")
                                ),
                            new Shoot(8.4, count: 1, projectileIndex: 0, coolDown: 450),
                            new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 20, coolDown: 750),
                            new TimedTransition(2000, "fire")
                            ),
                        new State("fire",
                           new Prioritize(
                                 new Follow(0.36, 8, 1),
                                 new Wander(0.12)
                                ),
                             new Shoot(10, count: 4, projectileIndex: 1, coolDownOffset: 1100, angleOffset: 270, coolDown: 1250),
                            new Shoot(10, count: 4, projectileIndex: 1, coolDownOffset: 1100, angleOffset: 90, coolDown: 1250),
                            new Shoot(8.4, count: 10, projectileIndex: 0, coolDown: 3400),
                            new TimedTransition(3400, "midfight")
                            )

                   ),
                    new MostDamagers(3,
                        new ItemLoot("Greater Potion of Wisdom", 1.0)
                    ),
                    new ItemLoot("Ghost Pirate Rum", 1.0),
                    new Threshold(0.025,
                        new TierLoot(9, ItemType.Weapon, 0.1),
                        new ItemLoot("Ghastly Hammer", 0.01),
                        new TierLoot(4, ItemType.Ability, 0.1),
                        new TierLoot(5, ItemType.Ability, 0.05),
                        new TierLoot(9, ItemType.Armor, 0.1),
                        new TierLoot(3, ItemType.Ring, 0.05),
                        new TierLoot(10, ItemType.Armor, 0.05),
                        new TierLoot(11, ItemType.Armor, 0.04),
                        new TierLoot(10, ItemType.Weapon, 0.05),
                        new TierLoot(11, ItemType.Weapon, 0.04),
                        new TierLoot(4, ItemType.Ring, 0.025),
                        new TierLoot(5, ItemType.Ring, 0.02),
                        new EggLoot(EggRarity.Common, 0.05),
                        new EggLoot(EggRarity.Uncommon, 0.025),
                        new EggLoot(EggRarity.Rare, 0.02),
                        new EggLoot(EggRarity.Legendary, 0.005)
                    )
                )
            

        #endregion

        #region PENTARACT
        .Init("Pentaract",
                new State(
                    new State("Entry",
                        new PentaractStar(250),
                        new EntitiesNotExistsTransition(50, "Suicide", "Pentaract Tower"),
                        new State("EntryTimer",
                            new TimedTransition(15000, "RespawnTowers")
                        ),
                        new State("RespawnTowers",
                            new Order(50, "Pentaract Tower Corpse", "Respawn"),
                            new TimedTransition(0, "EntryTimer")
                        )
                    ),
                    new State("Suicide",
                        new Suicide()
                    )
                )
            )
            .Init("Pentaract Tower",
                new State(
                    new Spawn("Pentaract Eye", 5, 1),
                    new Grenade(4, 100, 8, coolDown: 2000),
                    new TransformOnDeath("Pentaract Tower Corpse"),
                    new CopyDamageOnDeath("Pentaract Tower Corpse", 2)
                )
            )
            .Init("Pentaract Eye",
                new State(
                    new Swirl(5, 20, targeted: false),
                    new Shoot(10, coolDown: 200)
                )
            )
            .Init("Pentaract Tower Corpse",
                new State(
                    new State("Entry",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new EntitiesNotExistsTransition(50, "Suicide", "Pentaract")
                    ),
                    new State("Respawn",
                        new Transform("Pentaract Tower")
                    ),
                    new State("Suicide",
                        new Suicide()
                    )
                ),
                new Threshold(0.3,
                    new ItemLoot("Greater Potion of Defense", 1)
                ),
                new Threshold(0.2,
                    new ItemLoot("Greater Potion of Speed", 0.5),
                    new ItemLoot("Greater Potion of Wisdom", 0.5)
                ),
                new Threshold(0.1,
                    new ItemLoot("Seal of Blasphemous Prayer", 0.02),

                    new TierLoot(11, ItemType.Weapon, 0.01),
                    new TierLoot(11, ItemType.Armor, 0.01),
                    new TierLoot(5, ItemType.Ring, 0.01),

                    new TierLoot(10, ItemType.Weapon, 0.01),
                    new TierLoot(10, ItemType.Armor, 0.01),

                    new TierLoot(9, ItemType.Weapon, 0.02),
                    new TierLoot(5, ItemType.Ability, 0.02),
                    new TierLoot(9, ItemType.Armor, 0.02),

                    new ItemLoot("Greater Potion of Attack", 0.1),
                    new ItemLoot("Greater Potion of Vitality", 0.1),
                    new ItemLoot("Greater Potion of Dexterity", 0.1),

                    new TierLoot(4, ItemType.Ability, 0.05),
                    new TierLoot(8, ItemType.Armor, 0.05),

                    new TierLoot(8, ItemType.Weapon, 0.1),
                    new TierLoot(7, ItemType.Armor, 0.1),
                    new TierLoot(3, ItemType.Ring, 0.1)
                )
            )

        #endregion

        #region ROCKDRAGON
        .Init("Dragon Head",
                new State(
                    new Reproduce("Rock Dragon Bat", 5, 5, coolDown: 10000),
                    //new DropPortalOnDeath("Lair of Draconis Portal", 75),
                    new State("default",
                        new PlayerWithinTransition(10, "spawnbody")
                        ),
                    new State("spawnbody",
                        new ChangeSize(60, 120),
                        new SetAltTexture(0),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Spawn("Body Segment A", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment B", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment C", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment D", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment E", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment F", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment G", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment H", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment I", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment Tail", 1, 1, coolDown: 99999),
                        new TimedTransition(400, "weirdmovement")
                        ),
                    new State("weirdmovement",

                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                       new StayAbove(2, 265),
                        new Sequence(
                            new Timed(1000,
                                new ReturnToSpawn(false, 0.51)
                                ),
                            new Timed(5000,
                            new Prioritize(
                                new Swirl(1.25, 8, targeted: false),
                                new Wander(0.2)
                                )
                                ),
                            new Timed(700,
                                new Prioritize(
                                    new Follow(0.9, 11, 1),
                                    new StayCloseToSpawn(0.5, 3)
                                    )
                                )
                            ),
                        new Shoot(4, count: 8, shootAngle: 8, projectileIndex: 1, coolDown: 1250),
                        new EntitiesNotExistsTransition(9999, "vulnerable", "Body Segment A", "Body Segment B", "Body Segment C", "Body Segment D", "Body Segment E", "Body Segment F", "Body Segment G", "Body Segment H", "Body Segment I")
                        ),
                    new State("vulnerable",
                        new ChangeSize(60, 165),
                        new SetAltTexture(1),
                        new RemoveEntity(9999, "Body Segment Tail"),
                        new Sequence(
                            new Timed(1250,
                                new ReturnToSpawn(false, 0.95)
                                ),
                            new Timed(1000,
                                new BackAndForth(0.96, 5)
                                ),
                            new Timed(2700,
                                new Prioritize(
                                    new Follow(1.1, 11, 1),
                                    new StayCloseToSpawn(0.95, 8)
                                    )
                                )
                            ),
                        new Shoot(8, count: 6, projectileIndex: 3, coolDown: 3000),
                        new Shoot(10, 1, projectileIndex: 2, coolDown: 4123),
                        new TimedTransition(11000, "spawnbody")
                        )
                    ),
                new MostDamagers(3,
                    LootTemplates.StatIncreasePotionsLoot()
                ),
                new Threshold(0.05,
                    new ItemLoot("Ray Katana", 0.05),
                    new ItemLoot("Zaarvox's Heart", 0.01),
                    new ItemLoot("Amulet of Drakefyre", 0.01),
                    new ItemLoot("Helm of Draconic Dominance", 0.01),
                    new ItemLoot("Indomptable", 0.01),
                    new TierLoot(8, ItemType.Weapon, 0.2),
                    new TierLoot(9, ItemType.Weapon, 0.175),
                    new TierLoot(10, ItemType.Weapon, 0.125),
                    new TierLoot(11, ItemType.Weapon, 0.05),
                    new TierLoot(8, ItemType.Armor, 0.2),
                    new TierLoot(9, ItemType.Armor, 0.175),
                    new TierLoot(10, ItemType.Armor, 0.15),
                    new TierLoot(11, ItemType.Armor, 0.1),
                    new TierLoot(12, ItemType.Armor, 0.05),
                    new TierLoot(4, ItemType.Ability, 0.15),
                    new TierLoot(5, ItemType.Ability, 0.1),
                    new TierLoot(5, ItemType.Ring, 0.05)
                )
            )
                .Init("Body Segment Bomb",
                    new State(
                      new State("BoutToBlow",
                      new TimedTransition(1620, "bom")
                        ),
                    new State("bom",
                       new Shoot(8.4, count: 13, projectileIndex: 0),
                       new Suicide()
                    )))
       .Init("Body Segment A",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment B",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment A", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment A")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )

                 )
            )
         .Init("Body Segment C",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment B", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment B")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment D",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment C", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment C")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment E",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment D", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment D")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment F",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment E", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment E")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment G",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment E", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment F")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment H",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment G", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment G")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment I",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment H", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment H")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
        .Init("Body Segment Tail",
             new State(
              new ConditionalEffect(ConditionEffectIndex.Invulnerable),
               new State("go",
                   new Protect(0.97, "Body Segment I", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment I")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
           .Init("Rock Dragon Bat",
                    new State(
                      new State("BoutToBlow",
                      new Prioritize(
                        new Follow(0.5, 8, 1),
                        new Wander(0.2)
                        ),
                      new Shoot(7, count: 3, shootAngle: 8, projectileIndex: 3, coolDown: 1300),
                      new HpLessTransition(0.11, "bom"),
                      new TimedTransition(5500, "bom")
                        ),
                    new State("bom",
                       new Shoot(8.4, count: 7, projectileIndex: 2),
                       new Suicide()
                    )
                )
            )
        #endregion

        #region LORDOFTHELOSTLANDS
            .Init("Lord of the Lost Lands",
                new State(
                    new HpLessTransition(0.15, "IMDONELIKESOOOODONE!"),
                    new State("timetogeticey",
                        new PlayerWithinTransition(8, "startupandfireup")
                        ),
                    new State("startupandfireup",
                        new SetAltTexture(0),
                        new Wander(0.3),
                        new Shoot(10, count: 7, shootAngle: 7, coolDownOffset: 1100, angleOffset: 270, coolDown: 2250),
                        new Shoot(10, count: 7, shootAngle: 7, coolDownOffset: 1100, angleOffset: 90, coolDown: 2250),

                        new Shoot(10, count: 7, shootAngle: 7, coolDown: 2250),
                        new Shoot(10, count: 7, shootAngle: 7, angleOffset: 180, coolDown: 2250),
                        new TimedTransition(8500, "GatherUp")
                        ),
                    new State("GatherUp",
                        new SetAltTexture(3),
                        new Taunt("GATHERING POWER!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(8.4, count: 6, shootAngle: 60, projectileIndex: 1, coolDown: 4550),
                        new Shoot(8.4, count: 6, shootAngle: 60, predictive: 2, projectileIndex: 1, coolDown: 2700),
                        new TimedTransition(5750, "protect")
                        ),
                    new State("protect",
                        //Minions spawn
                        new ConditionalEffect(ConditionEffectIndex.StunImmune),
                        new TossObject("Guardian of the Lost Lands", 5, 0, coolDown: 9999999, randomToss: false),
                        new TossObject("Guardian of the Lost Lands", 5, 90, coolDown: 9999999, randomToss: false),
                        new TossObject("Guardian of the Lost Lands", 5, 180, coolDown: 9999999, randomToss: false),
                        new TossObject("Guardian of the Lost Lands", 5, 270, coolDown: 9999999, randomToss: false),
                        new TimedTransition(1000, "crystals")
                        ),
                    new State("crystals",
                        new SetAltTexture(1),
                        new ConditionalEffect(ConditionEffectIndex.StunImmune),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TossObject("Protection Crystal", 4, 0, coolDown: 9999999, randomToss: false),
                        new TossObject("Protection Crystal", 4, 45, coolDown: 9999999, randomToss: false),
                        new TossObject("Protection Crystal", 4, 90, coolDown: 9999999, randomToss: false),
                        new TossObject("Protection Crystal", 4, 135, coolDown: 9999999, randomToss: false),
                        new TossObject("Protection Crystal", 4, 180, coolDown: 9999999, randomToss: false),
                        new TossObject("Protection Crystal", 4, 225, coolDown: 9999999, randomToss: false),
                        new TossObject("Protection Crystal", 4, 270, coolDown: 9999999, randomToss: false),
                        new TossObject("Protection Crystal", 4, 315, coolDown: 9999999, randomToss: false),
                        new TimedTransition(2100, "checkforcrystals")
                        ),
                    new State("checkforcrystals",
                        new SetAltTexture(1),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new EntitiesNotExistsTransition(9999, "startupandfireup", "Protection Crystal")
                        ),
                    new State("IMDONELIKESOOOODONE!",
                        new Taunt("NOOOOOOOOOOOOOOO!"),
                        new SetAltTexture(3),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new Flash(0xFF0000, 0.2, 3),
                        new TimedTransition(1000, "dead")
                        ),
                    new State("dead",
                        new DropPortalOnDeath("Ice Cave Portal", 35),
                        new Shoot(8.4, count: 6, shootAngle: 60, projectileIndex: 1),
                        new Suicide()
                        )
                    ),
                new MostDamagers(3,
                    LootTemplates.StatIncreasePotionsLoot()
                ),
                new Threshold(0.05,
                    new ItemLoot("Shield of Ogmur", 0.005),
                    new TierLoot(8, ItemType.Weapon, 0.2),
                    new TierLoot(9, ItemType.Weapon, 0.175),
                    new TierLoot(10, ItemType.Weapon, 0.125),
                    new TierLoot(11, ItemType.Weapon, 0.05),
                    new TierLoot(8, ItemType.Armor, 0.2),
                    new TierLoot(9, ItemType.Armor, 0.175),
                    new TierLoot(10, ItemType.Armor, 0.15),
                    new TierLoot(11, ItemType.Armor, 0.1),
                    new TierLoot(12, ItemType.Armor, 0.05),
                    new TierLoot(4, ItemType.Ability, 0.15),
                    new TierLoot(5, ItemType.Ability, 0.1),
                    new TierLoot(5, ItemType.Ring, 0.05)
                )
            )
            .Init("Protection Crystal",
                new State(
                    new State("PROTECT!",
                        new Orbit(0.3, 3, 20, "Lord of the Lost Lands"),
                        new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 10, coolDown: 100)
                        )
                    )
            )
            .Init("Guardian of the Lost Lands",
                new State(
                    new State("Tough",
                        new Follow(0.35, 8, 1),
                        new Spawn("Knight of the Lost Lands", initialSpawn: 1, maxChildren: 1),
                        new Shoot(8.4, count: 6, shootAngle: 60, projectileIndex: 1, coolDown: 2000),
                        new Shoot(8.4, count: 6, projectileIndex: 0, coolDown: 1300),
                        new HpLessTransition(0.35, "Scrub")
                        ),
                    new State("Scrub",
                        new StayBack(0.75, 5),
                        new Shoot(8.4, count: 6, shootAngle: 60, projectileIndex: 1, coolDown: 2000),
                        new Shoot(8.4, count: 5, projectileIndex: 0, coolDown: 1300)
                        )
                ),
                new ItemLoot("Health Potion", 0.07),
                new ItemLoot("Magic Potion", 0.07)
            )
            .Init("Knight of the Lost Lands",
                new State(
                    new State("Fighting",
                        new Follow(0.4, 8, 1),
                        new Shoot(8.4, count: 1, projectileIndex: 0, coolDown: 900)
                        )
                    ),
                new ItemLoot("Health Potion", 0.04),
                new ItemLoot("Magic Potion", 0.04)
            )


        #endregion

        #region GRANDSPHINX
        .Init("Grand Sphinx",
                    new State(
                        new HpLessOrder(50, 0.15, "Horrid Reaper", "Go away"),
                        new DropPortalOnDeath("Tomb of the Ancients Portal", 35),
                        new Spawn("Horrid Reaper", maxChildren: 8, initialSpawn: 1),
                        new State("BlindAttack",
                            new Wander(0.0005),
                            new StayCloseToSpawn(0.5, 8),
                            new Taunt("You hide like cowards... but you can't hide from this!"),
                            new State("1",
                                new Shoot(10, projectileIndex: 1, count: 10, shootAngle: 10, fixedAngle: 0),
                                new Shoot(10, projectileIndex: 1, count: 10, shootAngle: 10, fixedAngle: 180),
                                new TimedTransition(1500, "2")
                            ),
                            new State("2",
                                new Shoot(10, projectileIndex: 1, count: 10, shootAngle: 10, fixedAngle: 270),
                                new Shoot(10, projectileIndex: 1, count: 10, shootAngle: 10, fixedAngle: 90),
                                new TimedTransition(1500, "1")
                            ),
                            new TimedTransition(10000, "Ilde")
                        ),
                        new State("Ilde",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Flash(0xff00ff00, 0.01, 50),
                            new TimedTransition(2000, "ArmorBreakAttack")
                        ),
                        new State("ArmorBreakAttack",
                            new Wander(0.0005),
                            new StayCloseToSpawn(0.5, 8),
                            new Shoot(0, projectileIndex: 2, count: 8, shootAngle: 5, coolDown: 300),
                            new TimedTransition(10000, "Ilde2")
                        ),
                        new State("Ilde2",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Flash(0xff00ff00, 0.01, 50),
                            new TimedTransition(2000, "WeakenAttack")
                        ),
                        new State("WeakenAttack",
                            new Wander(0.0005),
                            new StayCloseToSpawn(0.5, 8),
                            new Shoot(10, projectileIndex: 3, count: 3, shootAngle: 120, coolDown: 900),
                            new Shoot(10, projectileIndex: 2, count: 3, shootAngle: 15, fixedAngle: 40, coolDown: 1600, coolDownOffset: 0),
                            new Shoot(10, projectileIndex: 2, count: 3, shootAngle: 15, fixedAngle: 220, coolDown: 1600, coolDownOffset: 0),
                            new Shoot(10, projectileIndex: 2, count: 3, shootAngle: 15, fixedAngle: 130, coolDown: 1600, coolDownOffset: 800),
                            new Shoot(10, projectileIndex: 2, count: 3, shootAngle: 15, fixedAngle: 310, coolDown: 1600, coolDownOffset: 800),
                            new TimedTransition(10000, "Ilde3")
                        ),
                        new State("Ilde3",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Flash(0xff00ff00, 0.01, 50),
                            new TimedTransition(2000, "BlindAttack")
                        )
                    ),
                    new MostDamagers(3,
                        new ItemLoot("Greater Potion of Vitality", 1),
                        new ItemLoot("Greater Potion of Wisdom", 1)
                    ),
                    new Threshold(0.05,
                        new ItemLoot("Helm of the Juggernaut", 0.005)
                    ),
                    new Threshold(0.1,
                        new OnlyOne(
                            LootTemplates.DefaultEggLoot(EggRarity.Legendary)
                        )
                    )
                )
                .Init("Horrid Reaper",
                    new State(
                        new Shoot(radius: 25, shootAngle: 10 * (float)Math.PI / 180, count: 1, projectileIndex: 0, coolDown: 1000),
                        new State("Idle",
                            new StayCloseToSpawn(0.5, 15),
                            new Wander(0.5),
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable, true)
                        ),
                        new State("Go away",
                            new TimedTransition(5000, "I AM OUT"),
                            new Taunt("OOaoaoAaAoaAAOOAoaaoooaa!!!")
                        ),
                        new State("I AM OUT",
                            new Decay(0)
                        )
                    )
                )

        #endregion

        #region CYCLOPSGOD
        .Init("Cyclops God",
                new State(
                    new DropPortalOnDeath("Spider Den Portal", 20, PortalDespawnTimeSec: 100),
                    new State("idle",
                        new PlayerWithinTransition(11, "blade_attack"),
                        new HpLessTransition(0.8, "blade_attack")
                        ),
                    new State("blade_attack",
                        new Prioritize(
                            new Follow(0.4, range: 7),
                            new Wander(0.4)
                            ),
                        new Shoot(10, projectileIndex: 4, count: 1, shootAngle: 15, predictive: 0.5, coolDown: 100000),
                        new Shoot(10, projectileIndex: 4, count: 2, shootAngle: 10, predictive: 0.5, coolDown: 100000,
                            coolDownOffset: 700),
                        new Shoot(10, projectileIndex: 4, count: 3, shootAngle: 8.5, predictive: 0.5, coolDown: 100000,
                            coolDownOffset: 1400),
                        new Shoot(10, projectileIndex: 4, count: 4, shootAngle: 7, predictive: 0.5, coolDown: 100000,
                            coolDownOffset: 2100),
                        new TimedTransition(4000, "if_cloaked1")
                        ),
                    new State("if_cloaked1",
                        new Shoot(10, projectileIndex: 4, count: 15, shootAngle: 24, fixedAngle: 8, coolDown: 1500,
                            coolDownOffset: 400),
                        new TimedTransition(10000, "wave_attack"),
                        new PlayerWithinTransition(10.5, "wave_attack")
                        ),
                    new State("wave_attack",
                        new Prioritize(
                            new Follow(0.6, range: 5),
                            new Wander(0.6)
                            ),
                        new Shoot(9, projectileIndex: 0, coolDown: 700, coolDownOffset: 700),
                        new Shoot(9, projectileIndex: 1, coolDown: 700, coolDownOffset: 700),
                        new Shoot(9, projectileIndex: 2, coolDown: 700, coolDownOffset: 700),
                        new Shoot(9, projectileIndex: 3, coolDown: 700, coolDownOffset: 700),
                        new TimedTransition(3800, "if_cloaked2")
                        ),
                    new State("if_cloaked2",
                        new Shoot(10, projectileIndex: 4, count: 15, shootAngle: 24, fixedAngle: 8, coolDown: 1500,
                            coolDownOffset: 400),
                        new TimedTransition(10000, "idle"),
                        new PlayerWithinTransition(10.5, "idle")
                        ),
                    new Taunt(0.7, 10000, "I will floss with your tendons!",
                        "I smell the blood of an Englishman!",
                        "I will suck the marrow from your bones!",
                        "You will be my food, {PLAYER}!",
                        "Blargh!!",
                        "Leave my castle!",
                        "More wine!"
                        ),
                    new StayCloseToSpawn(1.2, 5),
                    new Spawn("Cyclops", 5, coolDown: 10000),
                    new Spawn("Cyclops Warrior", 5, coolDown: 10000),
                    new Spawn("Cyclops Noble", 5, coolDown: 10000),
                    new Spawn("Cyclops Prince", 5, coolDown: 10000),
                    new Spawn("Cyclops King", 5, coolDown: 10000)
                    ),
                new ItemLoot("Golden Ring", 0.05)
            )
            .Init("Cyclops",
                new State(
                    new Prioritize(
                        new StayCloseToSpawn(1.2, 5),
                        new Follow(1.2, range: 1),
                        new Wander(0.4)
                        ),
                    new Shoot(3)
                    ),
                new ItemLoot("Golden Sword", 0.02),
                new ItemLoot("Studded Leather Armor", 0.02),
                new ItemLoot("Health Potion", 0.05)
            )
            .Init("Cyclops Warrior",
                new State(
                    new Prioritize(
                        new StayCloseToSpawn(1.2, 5),
                        new Follow(1.2, range: 1),
                        new Wander(0.4)
                        ),
                    new Shoot(3)
                    ),
                new ItemLoot("Golden Sword", 0.03),
                new ItemLoot("Golden Shield", 0.02),
                new ItemLoot("Health Potion", 0.05)
            )
            .Init("Cyclops Noble",
                new State(
                    new Prioritize(
                        new StayCloseToSpawn(1.2, 5),
                        new Follow(1.2, range: 1),
                        new Wander(0.4)
                        ),
                    new Shoot(3)
                    ),
                new ItemLoot("Golden Dagger", 0.02),
                new ItemLoot("Studded Leather Armor", 0.02),
                new ItemLoot("Health Potion", 0.05)
            )
            .Init("Cyclops Prince",
                new State(
                    new Prioritize(
                        new StayCloseToSpawn(1.2, 5),
                        new Follow(1.2, range: 1),
                        new Wander(0.4)
                        ),
                    new Shoot(3)
                    ),
                new ItemLoot("Mithril Dagger", 0.02),
                new ItemLoot("Plate Mail", 0.02),
                new ItemLoot("Seal of the Divine", 0.01),
                new ItemLoot("Health Potion", 0.05)
            )
            .Init("Cyclops King",
                new State(
                    new Prioritize(
                        new StayCloseToSpawn(1.2, 5),
                        new Follow(1.2, range: 1),
                        new Wander(0.4)
                        ),
                    new Shoot(3)
                    ),
                new ItemLoot("Golden Sword", 0.02),
                new ItemLoot("Mithril Armor", 0.02),
                new ItemLoot("Health Potion", 0.05)
        #endregion CYCLOPSGOD

            );


    }
}