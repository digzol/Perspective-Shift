using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace PerspectiveShift
{
    public partial class Avatar
    {
        private enum HintStatFormat
        {
            Number,
            Percent,
            Temperature,
            Nutrition,
        }

        private struct HintStat
        {
            public string label;
            public string value;
            public string delta;
            public float deltaWidth;
            public int sign;
            public Thing icon;
        }

        private readonly List<Apparel> replacedApparelBuffer = new List<Apparel>();

        private static StatDef _insulationColdStat;
        private static StatDef InsulationColdStat => _insulationColdStat ??= DefDatabase<StatDef>.GetNamedSilentFail("Insulation_Cold");

        private static StatDef _insulationHeatStat;
        private static StatDef InsulationHeatStat => _insulationHeatStat ??= DefDatabase<StatDef>.GetNamedSilentFail("Insulation_Heat");

        private void BuildGearHintStats(Thing gear)
        {
            equipHintTitle = null;
            equipHintQuality = null;
            equipHintStatCount = 0;
            if (gear == null) return;

            if (gear is Apparel apparel)
            {
                SetHintIdentity(gear);
                BuildApparelHintStats(apparel);
                return;
            }

            if (gear.def.IsWeapon)
            {
                SetHintIdentity(gear);
                BuildWeaponHintStats(gear);
            }
        }

        private void BuildFoodHintStats(Thing food)
        {
            equipHintTitle = null;
            equipHintQuality = null;
            equipHintStatCount = 0;
            if (food == null) return;

            SetHintIdentity(food);
            AddHintStat("PS_HintNutrition".Translate(), FoodUtility.NutritionForEater(pawn, food), 0f, HintStatFormat.Nutrition);
        }

        private void BuildDrugHintStats(Thing drug)
        {
            equipHintTitle = null;
            equipHintQuality = null;
            equipHintStatCount = 0;
            if (drug == null) return;

            SetHintIdentity(drug);

            var def = drug.def;
            if (def.IsNutritionGivingIngestible)
            {
                AddHintStat("PS_HintNutrition".Translate(), FoodUtility.NutritionForEater(pawn, drug), 0f, HintStatFormat.Nutrition);
            }

            float joy = def.ingestible?.joy ?? 0f;
            if (joy > 0f)
            {
                AddHintStat("PS_HintJoy".Translate(), joy, 0f, HintStatFormat.Number);
            }

            var drugProps = DrugStatsUtility.GetDrugComp(def);
            if (drugProps != null && drugProps.Addictive)
            {
                float tolerance = 0f;
                var toleranceDef = DrugStatsUtility.GetTolerance(def);
                if (toleranceDef != null)
                {
                    tolerance = pawn.health?.hediffSet?.GetFirstHediffOfDef(toleranceDef)?.Severity ?? 0f;
                }

                AddHintStat("PS_HintAddictiveness".Translate(), DrugStatsUtility.GetAddictivenessAtTolerance(def, tolerance), 0f, HintStatFormat.Percent);
            }
        }

        private void BuildBookHintStats(Book book)
        {
            equipHintTitle = null;
            equipHintQuality = null;
            equipHintStatCount = 0;
            if (book == null) return;

            SetHintIdentity(book);

            if (book.MentalBreakChancePerHour > 0f)
            {
                AddHintStatText("PS_HintMentalBreak".Translate(), "PS_HintPerHour".Translate(book.MentalBreakChancePerHour.ToStringPercent("0.0")));
            }

            foreach (var doer in book.BookComp.Doers)
            {
                if (doer is BookOutcomeDoerGainSkillExp skillDoer)
                {
                    AddBookSkillStats(skillDoer);
                }
                else if (doer is ReadingOutcomeDoerGainResearch researchDoer)
                {
                    AddBookResearchStats(researchDoer);
                }
                else if (doer is ReadingOutcomeDoerJoyFactorModifier)
                {
                    AddHintStatText("PS_HintJoy".Translate(), "x" + book.JoyFactor.ToStringPercent());
                }
            }
        }

        private void AddBookSkillStats(BookOutcomeDoerGainSkillExp doer)
        {
            foreach (var entry in doer.Values)
            {
                string value = "PS_HintNoGain".Translate();
                if (pawn.skills != null && BookOutcomeDoerGainSkillExp.CanProgressSkill(pawn, entry.Key, doer.Quality))
                {
                    float xpPerHour = entry.Value * pawn.skills.GetSkill(entry.Key).LearnRateFactor() * GenDate.TicksPerHour;
                    value = "PS_HintXpPerHour".Translate(xpPerHour.ToStringDecimalIfSmall());
                }
                AddHintStatText(entry.Key.LabelCap, value);
            }
        }

        private void AddBookResearchStats(ReadingOutcomeDoerGainResearch doer)
        {
            foreach (var entry in doer.values)
            {
                var project = entry.Key;
                string value = "PS_HintNoGain".Translate();
                if (!project.IsFinished && doer.IsProjectVisible(project))
                {
                    float perHour = entry.Value * GenDate.TicksPerHour;
                    if (doer.RoundTo != 0) perHour = Mathf.Round(perHour / doer.RoundTo) * doer.RoundTo;
                    value = "PS_HintPerHour".Translate(perHour.ToStringDecimalIfSmall());
                }
                AddHintStatText(project.LabelCap, value);
            }
        }

        private void BuildHarvestHintStats(Plant plant)
        {
            equipHintTitle = null;
            equipHintQuality = null;
            equipHintStatCount = 0;

            var yieldDef = plant?.def.plant?.harvestedThingDef;
            if (yieldDef == null) return;

            equipHintTitle = yieldDef.LabelCap;
            if (yieldDef.IsNutritionGivingIngestible)
            {
                AddHintStat("PS_HintNutrition".Translate(), yieldDef.GetStatValueAbstract(StatDefOf.Nutrition), 0f, HintStatFormat.Nutrition);
            }
        }

        private void BuildBillHintStats(Thing bench, Bill bill, bool carrying, out string label, out Texture2D icon)
        {
            var recipe = bill.recipe;
            var things = jobCursorBillThings;
            var counts = jobCursorBillCounts;

            equipHintBill = bill;
            equipHintTitle = bill.LabelCap;
            equipHintQuality = BillRepeatText(bill);
            equipHintStatCount = 0;
            SetBillTitleIcon(recipe, things, counts);

            var skill = recipe.workSkill != null ? pawn.skills?.GetSkill(recipe.workSkill) : null;
            int ingredientSlots = equipHintStats.Length - (skill != null ? 1 : 0);
            for (int i = 0; i < things.Count && equipHintStatCount < ingredientSlots; i++)
            {
                var thing = things[i];
                if (thing is UnfinishedThing uft)
                {
                    float work = bill.GetWorkAmount(uft);
                    float done = uft.Initialized && work > 0f ? Mathf.Clamp01(1f - uft.workLeft / work) : 0f;
                    AddHintIngredient(uft, InstanceLabel(uft), done.ToStringPercent());
                    continue;
                }

                if (IndexOfSameIngredient(things, thing) < i) continue;

                int total = 0;
                for (int k = i; k < things.Count; k++)
                {
                    if (things[k].def == thing.def && things[k].Stuff == thing.Stuff) total += counts[k];
                }
                AddHintIngredient(thing, GenLabel.ThingLabel(thing.def, thing.Stuff).CapitalizeFirst(thing.def), "x" + total);
            }

            if (skill != null) AddHintStatText(recipe.workSkill.LabelCap, skill.Level.ToString());

            bool resume = jobCursorBillResume;
            string key = carrying
                ? (resume ? "PS_ClickToDropAndResumeBill" : "PS_ClickToDropAndStartBill")
                : (resume ? "PS_ClickToResumeBill" : "PS_ClickToStartBill");
            label = key.Translate();

            var category = BillCategory(bench, recipe);
            icon = category != CursorJobHint.None ? CursorTexFor(category) : null;
        }

        private static string BillRepeatText(Bill bill)
        {
            if (bill is not Bill_Production production) return null;

            var mode = production.repeatMode;
            if (mode != BillRepeatModeDefOf.Forever && mode != BillRepeatModeDefOf.RepeatCount && mode != BillRepeatModeDefOf.TargetCount) return null;
            return production.RepeatInfoText;
        }

        private static int IndexOfSameIngredient(List<Thing> things, Thing thing)
        {
            for (int i = 0; i < things.Count; i++)
            {
                if (things[i].def == thing.def && things[i].Stuff == thing.Stuff) return i;
            }
            return -1;
        }

        private void SetBillTitleIcon(RecipeDef recipe, List<Thing> things, List<int> counts)
        {
            var product = recipe.UIIconThing;
            if (product != null)
            {
                equipHintTitleDef = product;
                if (product.MadeFromStuff) equipHintTitleStuff = BillStuff(recipe, things, counts);
                return;
            }

            equipHintTitleTex = recipe.UIIcon;
            if (equipHintTitleTex == null && things.Count > 0) equipHintTitleThing = things[0];
        }

        private static ThingDef BillStuff(RecipeDef recipe, List<Thing> things, List<int> counts)
        {
            if (things.Count == 0) return null;
            if (things[0] is UnfinishedThing uft) return uft.Stuff;
            if (recipe.productHasIngredientStuff) return things[0].def;

            ThingDef stuff = null;
            int best = 0;
            for (int i = 0; i < things.Count; i++)
            {
                var def = things[i].def;
                if (!def.IsStuff || def == stuff) continue;

                int total = 0;
                for (int k = i; k < things.Count; k++)
                {
                    if (things[k].def == def) total += counts[k];
                }
                if (total <= best) continue;

                stuff = def;
                best = total;
            }
            return stuff;
        }

        private bool CanHarvestNow(Plant plant)
        {
            if (!plant.HarvestableNow || !plant.CanYieldNow() || !pawn.CanReserve(plant)) return false;

            return plant.def.plant.IsTree
                ? (!pawn.WorkTypeIsDisabled(WorkTypeDefOf.PlantCutting) && PlantUtility.PawnWillingToCutPlant_Job(plant, pawn))
                : (plant.def.plant.harvestTag == "Standard" && !pawn.WorkTypeIsDisabled(WorkTypeDefOf.PlantCutting));
        }

        private static string InstanceLabel(Thing thing)
        {
            string label = thing.LabelNoParenthesis;
            if (thing is ThingWithComps withComps)
            {
                var comps = withComps.AllComps;
                for (int i = 0; i < comps.Count; i++)
                {
                    label = comps[i].TransformLabel(label);
                }
            }

            return label.CapitalizeFirst(thing.def);
        }

        private void SetHintIdentity(Thing gear)
        {
            equipHintTitle = InstanceLabel(gear);
            if (gear.TryGetQuality(out QualityCategory quality))
            {
                equipHintQuality = quality.GetLabel().CapitalizeFirst();
            }
        }

        private void BuildWeaponHintStats(Thing weapon)
        {
            var current = pawn.equipment?.Primary;
            bool compare = current != null && current != weapon;

            float dps = WeaponDps(weapon);
            float armorPen = WeaponArmorPenetration(weapon);

            AddHintStat("PS_HintDps".Translate(), dps, compare ? dps - WeaponDps(current) : 0f, HintStatFormat.Number);
            AddHintStat("PS_HintArmorPenetration".Translate(), armorPen, compare ? armorPen - WeaponArmorPenetration(current) : 0f, HintStatFormat.Percent);
        }

        private void BuildApparelHintStats(Apparel apparel)
        {
            var replaced = ReplacedWornApparel(apparel);

            AddApparelStat(apparel, replaced, StatDefOf.ArmorRating_Sharp, "PS_HintArmorSharp".Translate(), HintStatFormat.Percent);
            AddApparelStat(apparel, replaced, StatDefOf.ArmorRating_Blunt, "PS_HintArmorBlunt".Translate(), HintStatFormat.Percent);
            AddApparelStat(apparel, replaced, InsulationColdStat, "PS_HintInsulationCold".Translate(), HintStatFormat.Temperature);
            AddApparelStat(apparel, replaced, InsulationHeatStat, "PS_HintInsulationHeat".Translate(), HintStatFormat.Temperature);
        }

        private void AddApparelStat(Apparel apparel, List<Apparel> replaced, StatDef stat, string label, HintStatFormat format)
        {
            if (stat == null) return;

            float value = apparel.GetStatValue(stat);
            float replacedValue = 0f;
            for (int i = 0; i < replaced.Count; i++)
            {
                replacedValue += replaced[i].GetStatValue(stat);
            }

            if (format == HintStatFormat.Temperature && Mathf.Abs(value) < 1f && Mathf.Abs(replacedValue) < 1f) return;

            AddHintStat(label, value, replaced.Count > 0 ? value - replacedValue : 0f, format);
        }

        private List<Apparel> ReplacedWornApparel(Apparel apparel)
        {
            replacedApparelBuffer.Clear();

            var worn = pawn.apparel?.WornApparel;
            if (worn == null) return replacedApparelBuffer;

            for (int i = 0; i < worn.Count; i++)
            {
                if (worn[i] == apparel) continue;
                if (ApparelUtility.CanWearTogether(apparel.def, worn[i].def, pawn.RaceProps.body)) continue;
                replacedApparelBuffer.Add(worn[i]);
            }
            return replacedApparelBuffer;
        }

        private void AddHintStat(string label, float value, float delta, HintStatFormat format)
        {
            if (equipHintStatCount >= equipHintStats.Length) return;

            var stat = new HintStat { label = label, value = FormatHintStat(value, format) };
            if (Mathf.Abs(delta) >= HintStatEpsilon(format))
            {
                stat.sign = delta > 0f ? 1 : -1;
                stat.delta = FormatHintStat(Mathf.Abs(delta), format);
            }
            equipHintStats[equipHintStatCount++] = stat;
        }

        private void AddHintStatText(string label, string value)
        {
            if (equipHintStatCount >= equipHintStats.Length) return;

            equipHintStats[equipHintStatCount++] = new HintStat { label = label, value = value };
        }

        private void AddHintIngredient(Thing thing, string label, string value)
        {
            if (equipHintStatCount >= equipHintStats.Length) return;

            equipHintStats[equipHintStatCount++] = new HintStat { label = label, value = value, icon = thing };
        }

        private static string FormatHintStat(float value, HintStatFormat format)
        {
            switch (format)
            {
                case HintStatFormat.Percent: return value.ToStringPercent();
                case HintStatFormat.Temperature: return value.ToStringTemperatureOffset("F0");
                case HintStatFormat.Nutrition: return value.ToString("0.##");
                default: return value.ToString("F1");
            }
        }

        private static float HintStatEpsilon(HintStatFormat format)
        {
            switch (format)
            {
                case HintStatFormat.Percent: return 0.005f;
                case HintStatFormat.Temperature: return 0.5f;
                case HintStatFormat.Nutrition: return 0.005f;
                default: return 0.05f;
            }
        }

        private float WeaponDps(Thing weapon)
        {
            if (weapon == null) return 0f;

            if (weapon.def.IsRangedWeapon)
            {
                var verbProps = PrimaryRangedVerb(weapon.def);
                if (verbProps?.defaultProjectile?.projectile == null) return 0f;

                float cycleTime = verbProps.warmupTime
                    + weapon.GetStatValue(StatDefOf.RangedWeapon_Cooldown)
                    + ((verbProps.burstShotCount - 1) * verbProps.ticksBetweenBurstShots).TicksToSeconds();
                if (cycleTime <= 0f) return 0f;

                float damage = verbProps.defaultProjectile.projectile.GetDamageAmount(weapon);
                return damage * verbProps.burstShotCount / cycleTime;
            }

            if (weapon.def.IsMeleeWeapon)
            {
                return weapon.GetStatValue(StatDefOf.MeleeWeapon_AverageDPS);
            }

            return 0f;
        }

        private float WeaponArmorPenetration(Thing weapon)
        {
            if (weapon == null) return 0f;

            if (weapon.def.IsRangedWeapon)
            {
                var verbProps = PrimaryRangedVerb(weapon.def);
                if (verbProps?.defaultProjectile?.projectile == null) return 0f;
                return verbProps.defaultProjectile.projectile.GetArmorPenetration(weapon);
            }

            var tools = weapon.def.tools;
            if (tools == null || tools.Count == 0) return 0f;

            float weightTotal = 0f;
            float penetrationTotal = 0f;
            foreach (var entry in VerbUtility.GetAllVerbProperties(weapon.def.Verbs, tools))
            {
                if (!entry.verbProps.IsMeleeAttack) continue;

                float weight = entry.verbProps.AdjustedMeleeSelectionWeight(entry.tool, pawn, weapon, null, false);
                if (weight <= 0f) continue;

                weightTotal += weight;
                penetrationTotal += weight * entry.verbProps.AdjustedArmorPenetration(entry.tool, pawn, weapon, null);
            }

            return weightTotal > 0f ? penetrationTotal / weightTotal : 0f;
        }

        private static VerbProperties PrimaryRangedVerb(ThingDef def)
        {
            List<VerbProperties> verbs = def.Verbs;
            if (verbs == null) return null;

            VerbProperties fallback = null;
            for (int i = 0; i < verbs.Count; i++)
            {
                if (verbs[i].IsMeleeAttack) continue;
                if (verbs[i].isPrimary) return verbs[i];
                fallback ??= verbs[i];
            }
            return fallback;
        }
    }
}
