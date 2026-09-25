using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace PerspectiveShift
{
    public partial class Avatar
    {
        public static bool IsAvatarLeftClick = false;
        private static Texture2D _reticleTex;
        public static Texture2D ReticleTex => _reticleTex ??= ContentFinder<Texture2D>.Get("UI/Reticle");
        private static Texture2D _reticleCooldownTex;
        public static Texture2D ReticleCooldownTex => _reticleCooldownTex ??= ContentFinder<Texture2D>.Get("UI/ReticleCooldown");
        private static Texture2D _reticleNoLOSTex;
        public static Texture2D ReticleNoLOSTex => _reticleNoLOSTex ??= ContentFinder<Texture2D>.Get("UI/ReticleNoLOS");

        private static Texture2D _dropCursorTex;
        public static Texture2D DropCursorTex => _dropCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Drop");

        private static Texture2D _mineCursorTex;
        public static Texture2D MineCursorTex => _mineCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Mine");

        private static Texture2D _buildCursorTex;
        public static Texture2D BuildCursorTex => _buildCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Build");

        private static Texture2D _chopCursorTex;
        public static Texture2D ChopCursorTex => _chopCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Chop");

        private static Texture2D _harvestCursorTex;
        public static Texture2D HarvestCursorTex => _harvestCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Harvest");

        private static Texture2D _cutCursorTex;
        public static Texture2D CutCursorTex => _cutCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Scythe");

        private static Texture2D _plantCursorTex;
        public static Texture2D PlantCursorTex => _plantCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Plant");

        private static Texture2D _plowCursorTex;
        public static Texture2D PlowCursorTex => _plowCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Plow");
        
        private static Texture2D _smoothCursorTex;
        public static Texture2D SmoothCursorTex => _smoothCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Smooth");
        
        private static Texture2D _cookCursorTex;
        public static Texture2D CookCursorTex => _cookCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Cook");
        
        private static Texture2D _butcherCursorTex;
        public static Texture2D ButcherCursorTex => _butcherCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Butcher");
        
        private static Texture2D _stonecuttingCursorTex;
        public static Texture2D StonecuttingCursorTex => _stonecuttingCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Stonecutting");

        private static Texture2D _brewCursorTex;
        public static Texture2D BrewCursorTex => _brewCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Brew");

        private static Texture2D _smeltCursorTex;
        public static Texture2D SmeltCursorTex => _smeltCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Smelt");

        private static Texture2D _tailorCursorTex;
        public static Texture2D TailorCursorTex => _tailorCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Tailor");
        
        private static Texture2D _tameCursorTex;
        public static Texture2D TameCursorTex => _tameCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Tame");
        
        private static Texture2D _slaughterCursorTex;
        public static Texture2D SlaughterCursorTex => _slaughterCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Slaughter");
        
        private static Texture2D _releaseToWildCursorTex;
        public static Texture2D ReleaseToWildCursorTex => _releaseToWildCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/ReleaseToWild");

        private static Texture2D _arrowsCursorTex;
        public static Texture2D ArrowsCursorTex => _arrowsCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Arrows");

        private static Texture2D _ammoCursorTex;
        public static Texture2D AmmoCursorTex => _ammoCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Ammo");

        private static Texture2D _chargeCursorTex;
        public static Texture2D ChargeCursorTex => _chargeCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Charge");

        private static Texture2D _traverseCursorTex;
        public static Texture2D TraverseCursorTex => _traverseCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Traverse");

        private static Texture2D _openCursorTex;
        public static Texture2D OpenCursorTex => _openCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Open");

        private static Texture2D _recreationCursorTex;
        public static Texture2D RecreationCursorTex => _recreationCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Recreation");

        private static Texture2D _sleepCursorTex;
        public static Texture2D SleepCursorTex => _sleepCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Sleep");

        private static Texture2D _researchCursorTex;
        public static Texture2D ResearchCursorTex => _researchCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Research");

        private static Texture2D _roofCursorTex;
        public static Texture2D RoofCursorTex => _roofCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Roof");

        private static Texture2D _launchCursorTex;
        public static Texture2D LaunchCursorTex => _launchCursorTex ??= ContentFinder<Texture2D>.Get("UI/CustomCursors/Launch");

        public bool HandleSelectorClick()
        {
            if (Find.Targeter.IsTargeting) return false;
            if (pawn.Downed) return false;
            if (pawn.InMentalState || passedOut) return false;

            if (ModCompatibility.IsPawnInVehicle(pawn, out Pawn veh, out bool isDriver, out bool isGunner))
            {
                if (isGunner && Event.current.type == EventType.MouseDown)
                {
                    if (Event.current.button == 0)
                    {
                        ModCompatibility.FireVehicleWeapons(veh, pawn, UI.MouseMapPosition());
                        Event.current.Use();
                        return true;
                    }
                    else if (Event.current.button == 1)
                    {
                        ModCompatibility.ClearVehicleWeapons(veh, pawn);
                        Event.current.Use();
                        return true;
                    }
                }
                return false;
            }

            if (Find.TickManager.Paused) return false;
            if (State.CameraLockPosition.HasValue) return false;

            if (IsMouseOverUI() || IsMouseOverColonistBar()) return false;

            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                if (pawn.Drafted)
                {
                    if (MouseIsOverPawn()) return false;

                    HandleFiring();
                    return true;
                }
                else
                {
                    return HandleLeftClick();
                }
            }

            if (Event.current.type == EventType.MouseDown && Event.current.button == 1)
            {
                if (pawn.carryTracker?.CarriedThing != null && pawn.inventory != null && pawn.carryTracker.CarriedThing is not (Pawn or Corpse))
                {
                    var carried = pawn.carryTracker.CarriedThing;
                    int count = carried.stackCount;

                    if (MassUtility.WillBeOverEncumberedAfterPickingUp(pawn, carried, count))
                    {
                        var maxCount = MassUtility.CountToPickUpUntilOverEncumbered(pawn, carried);
                        if (maxCount <= 0)
                        {
                            Messages.Message("PS_CannotCarryMoreWeight".Translate(), MessageTypeDefOf.RejectInput, false);
                            Event.current.Use();
                            return true;
                        }
                        count = maxCount;
                    }

                    var transferred = pawn.carryTracker.innerContainer.TryTransferToContainer(carried, pawn.inventory.innerContainer, count);
                    if (transferred > 0)
                    {
                        DefsOf.PS_PackInventory.PlayOneShotOnCamera();
                        Event.current.Use();
                        return true;
                    }
                }

                if (pawn.Drafted)
                {
                    var otherPawnsSelected = Find.Selector.SelectedObjects
                        .Any(o => o is Pawn p && p != pawn);
                    if (otherPawnsSelected)
                        return false;

                    if (pawn.jobs?.curJob != null && pawn.jobs.curJob.def.playerInterruptible)
                        pawn.jobs.EndCurrentJob(JobCondition.InterruptForced);

                    Event.current.Use();
                    return true;
                }

                if (pawn.jobs?.curJob != null && pawn.jobs.curJob.def.playerInterruptible)
                    pawn.jobs.EndCurrentJob(JobCondition.InterruptForced);
            }
            return false;
        }

        private void HandleFiring()
        {
            if (pawn.stances.curStance is Stance_Busy) return;
            if (IsAbilityCastJob()) return;
            var verb = GetActiveVerb();
            if (verb == null) return;

            if (pawn.WorkTagIsDisabled(WorkTags.Violent))
            {
                Messages.Message("IsIncapableOfViolence".Translate(pawn.LabelShort, pawn), MessageTypeDefOf.RejectInput);
                return;
            }

            if (!verb.verbProps.IsMeleeAttack && pawn.WorkTagIsDisabled(WorkTags.Shooting))
            {
                Messages.Message("IsIncapableOfShooting".Translate(pawn), MessageTypeDefOf.RejectInput);
                return;
            }

            var targetCell = UI.MouseCell();
            if (!targetCell.InBounds(pawn.Map)) return;

            var target = GetBestTarget(targetCell);
            Vector3 targetPos = target.Thing != null ? target.Thing.DrawPos : targetCell.ToVector3Shifted();
            Vector3 toTarget = targetPos - pawn.DrawPos;
            if (toTarget.sqrMagnitude > 0.01f)
                pawn.Rotation = Rot4.FromAngleFlat(NormAngle(Mathf.Atan2(toTarget.x, toTarget.z) * Mathf.Rad2Deg));

            if (pawn.Position.DistanceTo(targetCell) <= ShootTuning.MeleeRange)
            {
                if (target.Thing != null)
                    pawn.meleeVerbs.TryMeleeAttack(target.Thing);
            }
            else if (verb.CanHitTarget(target))
            {
                verb.TryStartCastOn(target, false, true);
            }
        }

        private void HandleCombatStance()
        {
            if (!pawn.Drafted || pawn.stances.curStance == null) return;

            bool isMoving = IsMoving;

            if (isMoving)
            {
                var rgActive = ModCompatibility.IsRunAndGunActiveFor(pawn, out string rgReason);
                if (rgActive)
                {
                    var stance = pawn.stances.curStance;
                    if (stance is Stance_Warmup warmup && stance.GetType() != ModCompatibility.stanceRunAndGunType)
                    {
                        ModCompatibility.ConvertToRunAndGunStance(pawn, warmup);
                    }
                    else if (stance is Stance_Cooldown cooldown && stance.GetType() != ModCompatibility.stanceRunAndGunCooldownType)
                    {
                        ModCompatibility.ConvertToRunAndGunCooldownStance(pawn, cooldown);
                    }
                    return;
                }

            }
            else
            {
                if (ModCompatibility.IsRunAndGunActiveFor(pawn))
                {
                    var stance = pawn.stances.curStance;
                    if (ModCompatibility.stanceRunAndGunType != null && stance.GetType() == ModCompatibility.stanceRunAndGunType && stance is Stance_Warmup warmup)
                    {
                        ModCompatibility.ConvertToVanillaWarmupStance(pawn, warmup);
                    }
                    else if (ModCompatibility.stanceRunAndGunCooldownType != null && stance.GetType() == ModCompatibility.stanceRunAndGunCooldownType && stance is Stance_Cooldown cooldown)
                    {
                        ModCompatibility.ConvertToVanillaCooldownStance(pawn, cooldown);
                    }
                }
            }
        }

        private void DrawReticle(Vector2 center)
        {
            if (Event.current.type != EventType.Repaint) return;

            if (pawn.stances.curStance is Stance_Busy)
            {
                bool isCooldown = pawn.stances.curStance is Stance_Cooldown;
                Color prev = GUI.color;
                GUI.color = isCooldown ? new Color(1f, 0.65f, 0f) : Color.white;
                float size = 32f;
                var rect = new Rect(center.x - size / 2f, center.y - size / 2f, size, size);
                var tex = isCooldown ? ReticleCooldownTex : ReticleTex;
                if (tex != null) GUI.DrawTexture(rect, tex);
                GUI.color = prev;
                return;
            }

            Color color = Color.green;
            Texture2D reticleTex = ReticleTex;

            var verb = GetActiveVerb();
            if (verb != null)
            {
                var targetCell = UI.MouseCell();
                if (!targetCell.InBounds(pawn.Map)) { LeanTarget = Vector3.zero; return; }

                var target = GetBestTarget(targetCell);
                if (!verb.CanHitTarget(target))
                {
                    color = Color.red;
                    reticleTex = ReticleNoLOSTex;
                    if (!IsMoving) LeanTarget = Vector3.zero;
                }
                else if (!IsMoving)
                {
                    UpdateLeanTarget(targetCell);
                }
            }

            Color prevColor = GUI.color;
            GUI.color = color;
            float sz = 32f;
            var r = new Rect(center.x - sz / 2f, center.y - sz / 2f, sz, sz);
            if (reticleTex != null) GUI.DrawTexture(r, reticleTex);
            GUI.color = prevColor;
        }

        private void UpdateLeanTarget(IntVec3 targetCell)
        {
            var leanSources = new List<IntVec3>();
            ShootLeanUtility.LeanShootingSourcesFromTo(pawn.Position, targetCell, pawn.Map, leanSources);
            var best = leanSources
                .Where(s => s != pawn.Position && s.IsValid && s != IntVec3.Zero
                            && GenSight.LineOfSight(s, targetCell, pawn.Map, skipFirstCell: true))
                .OrderBy(s => s.DistanceToSquared(targetCell))
                .FirstOrDefault();
            LeanTarget = (best != IntVec3.Zero && best != pawn.Position)
                ? (best - pawn.Position).ToVector3()
                : Vector3.zero;
        }

        private Verb GetActiveVerb()
        {
            var verb = pawn.equipment?.PrimaryEq?.PrimaryVerb;
            if (verb == null || verb.verbProps.IsMeleeAttack)
                verb = pawn.VerbTracker?.AllVerbs?.FirstOrDefault(v => v is Verb_MeleeAttack && v.Available());
            return verb;
        }

        private bool IsAbilityCastJob()
        {
            if (pawn.CurJob?.ability != null)
                return true;
            if (ModCompatibility.IsVEFAbilityCast(pawn))
                return true;
            return false;
        }

        private LocalTargetInfo GetBestTarget(IntVec3 targetCell)
        {
            var things = targetCell.GetThingList(pawn.Map);
            Thing best = things.FirstOrDefault(t => t is Pawn && t != pawn)
                ?? things.FirstOrDefault(t => t.def.category == ThingCategory.Building || t.def.category == ThingCategory.Item);
            return best != null ? new LocalTargetInfo(best) : new LocalTargetInfo(targetCell);
        }

        private void HandleHoldToFire(bool mouseOverGizmo, bool mouseOverUI)
        {

            if (Event.current.type == EventType.Repaint
                && pawn.Drafted
                && PerspectiveShiftMod.settings.holdToFire
                && Input.GetMouseButton(0)
                && !mouseOverUI && !mouseOverGizmo
                && !State.ControlsFrozen
                && !Find.Targeter.IsTargeting
                && !Find.TickManager.Paused)
            {
                HandleFiring();
            }
        }

        private void UpdateCursorAndReticle(bool mouseOverGizmo, bool mouseOverUI)
        {
            bool drafted = pawn.Drafted && !pawn.InMentalState;

            if (drafted && !Find.TickManager.Paused && Find.Selector.IsSelected(pawn) && !Find.Targeter.IsTargeting)
            {
                if (!PerspectiveShiftMod.settings.disableCustomGizmos)
                    Find.Selector.Deselect(pawn);
            }

            bool cursorBlocked = mouseOverUI || mouseOverGizmo || State.ControlsFrozen || Find.Targeter.IsTargeting
                || WorldRendererUtility.WorldSelected;

            bool customCursors = PerspectiveShiftMod.settings.customCursors;

            if (customCursors && !drafted && !cursorBlocked)
            {
                var hint = MouseOverJobTarget();
                if (hint != CursorJobHint.None)
                {
                    Cursor.visible = false;
                    DrawJobCursor(UI.MousePositionOnUIInverted, CursorTexFor(hint));
                    return;
                }
            }

            if (customCursors && PerspectiveShiftMod.settings.haulingCursor && CarriedThing != null && !pawn.InMentalState && !cursorBlocked)
            {
                if (drafted && !IsMoving) LeanTarget = Vector3.zero;
                Cursor.visible = false;
                DrawDropCursor(UI.MousePositionOnUIInverted);
                return;
            }

            if (drafted && !cursorBlocked && !Find.TickManager.Paused)
            {
                Cursor.visible = false;
                DrawReticle(UI.MousePositionOnUIInverted);
                return;
            }

            Cursor.visible = true;
        }

        private enum CursorJobHint
        {
            None,
            Mine,
            Build,
            Chop,
            Harvest,
            Cut,
            Plant,
            Plow,
            Smooth,
            Traverse,
            Open,
            ReloadArrow,
            ReloadAmmo,
            ReloadCharge,
            Sleep,
            Recreation,
            Research,
            Cook,
            Butcher,
            Stonecutting,
            Brew,
            Smelt,
            Tailor,
            Tame,
            Slaughter,
            ReleaseToWild,
            Roof,
            Launch,
        }

        private static Texture2D CursorTexFor(CursorJobHint hint)
        {
            switch (hint)
            {
                case CursorJobHint.Build: return BuildCursorTex;
                case CursorJobHint.Chop: return ChopCursorTex;
                case CursorJobHint.Harvest: return HarvestCursorTex;
                case CursorJobHint.Cut: return CutCursorTex;
                case CursorJobHint.Plant: return PlantCursorTex;
                case CursorJobHint.Plow: return PlowCursorTex;
                case CursorJobHint.Traverse: return TraverseCursorTex;
                case CursorJobHint.Open: return OpenCursorTex;
                case CursorJobHint.ReloadArrow: return ArrowsCursorTex;
                case CursorJobHint.ReloadAmmo: return AmmoCursorTex;
                case CursorJobHint.ReloadCharge: return ChargeCursorTex;
                case CursorJobHint.Sleep: return SleepCursorTex;
                case CursorJobHint.Recreation: return RecreationCursorTex;
                case CursorJobHint.Research: return ResearchCursorTex;
                case CursorJobHint.Smooth: return SmoothCursorTex;
                case CursorJobHint.Cook: return CookCursorTex;
                case CursorJobHint.Butcher: return ButcherCursorTex;
                case CursorJobHint.Stonecutting: return StonecuttingCursorTex;
                case CursorJobHint.Brew: return BrewCursorTex;
                case CursorJobHint.Smelt: return SmeltCursorTex;
                case CursorJobHint.Tailor: return TailorCursorTex;
                case CursorJobHint.Tame: return TameCursorTex;
                case CursorJobHint.Slaughter: return SlaughterCursorTex;
                case CursorJobHint.ReleaseToWild: return ReleaseToWildCursorTex;
                case CursorJobHint.Roof: return RoofCursorTex;
                case CursorJobHint.Launch: return LaunchCursorTex;
                default: return MineCursorTex;
            }
        }

        private const float JobCursorRefresh = 0.25f;
        private const float BillMemoCostFactor = 100f;
        private const float BillMemoMaxAge = 3f;

        private IntVec3 jobCursorCell = IntVec3.Invalid;
        private IntVec3 jobCursorPawnCell = IntVec3.Invalid;
        private CursorJobHint jobCursorHint;
        private Thing jobCursorTarget;
        private float jobCursorStaleAt;
        private Thing jobCursorCarried;
        private int jobCursorStamp;
        private Bill jobCursorBill;
        private bool jobCursorBillResume;
        private readonly List<Thing> jobCursorBillThings = new List<Thing>();
        private readonly List<int> jobCursorBillCounts = new List<int>();
        private Thing billMemoBench;
        private IntVec3 billMemoPawnCell;
        private Job billMemoJob;
        private int billMemoSignature;
        private bool billMemoDesignated;
        private float billMemoStaleAt;
        private bool billMemoResult;
        private Bill billMemoBill;
        private bool billMemoResume;
        private readonly List<Thing> billMemoThings = new List<Thing>();
        private readonly List<int> billMemoCounts = new List<int>();
        private static readonly Dictionary<System.Type, bool> overridesHasJobOnThing = new Dictionary<System.Type, bool>();

        private CursorJobHint MouseOverJobTarget()
        {
            if (pawn.InMentalState || pawn.Map == null) return NoJobTarget();
            var carried = CarriedThing;
            if (carried == null && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) return NoJobTarget();

            var cell = UI.MouseCell();
            bool targetGone = jobCursorTarget != null && (jobCursorTarget.Destroyed || !jobCursorTarget.Spawned);

            if (!targetGone
                && cell == jobCursorCell
                && pawn.Position == jobCursorPawnCell
                && carried == jobCursorCarried
                && Time.realtimeSinceStartup < jobCursorStaleAt)
            {
                return jobCursorHint;
            }

            jobCursorCell = cell;
            jobCursorPawnCell = pawn.Position;
            jobCursorCarried = carried;
            jobCursorStaleAt = Time.realtimeSinceStartup + JobCursorRefresh;
            jobCursorStamp++;
            jobCursorHint = EvaluateJobTarget(cell, carried, out jobCursorTarget);
            return jobCursorHint;
        }

        private CursorJobHint NoJobTarget()
        {
            if (jobCursorBill != null) jobCursorStamp++;
            ClearClickBill();
            jobCursorCell = IntVec3.Invalid;
            jobCursorHint = CursorJobHint.None;
            jobCursorTarget = null;
            return CursorJobHint.None;
        }

        private void ClearClickBill()
        {
            jobCursorBill = null;
            jobCursorBillResume = false;
            jobCursorBillThings.Clear();
            jobCursorBillCounts.Clear();
        }

        private void SetClickBill(Bill bill, bool resume)
        {
            ClearClickBill();
            jobCursorBill = bill;
            jobCursorBillResume = resume;
        }

        private void AddClickBillThing(Thing thing, int count)
        {
            if (thing == null) return;
            jobCursorBillThings.Add(thing);
            jobCursorBillCounts.Add(count);
            if (thing is UnfinishedThing) jobCursorBillResume = true;
        }

        private void CaptureClickBill(Job job)
        {
            SetClickBill(job.bill, job.bill is Bill_Autonomous { State: not FormingState.Gathering });
            var queue = job.targetQueueB;
            if (queue == null) return;
            for (int i = 0; i < queue.Count; i++)
            {
                AddClickBillThing(queue[i].Thing, job.countQueue != null && i < job.countQueue.Count ? job.countQueue[i] : 1);
            }
        }

        private CursorJobHint EvaluateJobTarget(IntVec3 cell, Thing carried, out Thing target)
        {
            ClearClickBill();
            var hint = carried != null ? EvaluateCarriedTarget(cell, carried, out target) : EvaluateJobTargetInt(cell, out target);
            JobFailReason.Clear();
            return hint;
        }

        private CursorJobHint EvaluateJobTargetInt(IntVec3 cell, out Thing target)
        {
            target = null;
            var things = cell.GetThingList(pawn.Map);
            var settings = PerspectiveShiftMod.settings;

            if (pawn.Position.DistanceTo(cell) > settings.grabRange)
            {
                if (!settings.recreationCursor) return CursorJobHint.None;

                for (int i = 0; i < things.Count; i++)
                {
                    if (!IsWithinInteractionRange(things[i]) || !CanRecreateAt(things[i])) continue;

                    target = things[i];
                    return CursorJobHint.Recreation;
                }
                return CursorJobHint.None;
            }

            if (settings.buildCursor && !pawn.WorkTypeIsDisabled(WorkTypeDefOf.Construction))
            {
                for (int i = 0; i < things.Count; i++)
                {
                    if (things[i] is not Frame frame || !frame.IsCompleted()) continue;
                    if (!GenConstruct.CanConstruct(frame, pawn, true, true)) continue;

                    target = frame;
                    return CursorJobHint.Build;
                }

                for (int i = 0; i < things.Count; i++)
                {
                    if (pawn.Map.designationManager.DesignationOn(things[i], DesignationDefOf.Deconstruct) == null) continue;
                    if (!pawn.CanReserve(things[i])) continue;

                    target = things[i];
                    return CursorJobHint.Build;
                }

                for (int i = 0; i < things.Count; i++)
                {
                    if (!CanRepair(things[i])) continue;

                    target = things[i];
                    return CursorJobHint.Build;
                }
            }

            if (settings.traverseCursor && ModCompatibility.AsAboveSoBelowAvailable)
            {
                for (int i = 0; i < things.Count; i++)
                {
                    if (!ModCompatibility.IsLevelLink(things[i])) continue;
                    if (things[i] is not Building_Door door || !door.PawnCanOpen(pawn)) continue;

                    target = things[i];
                    return CursorJobHint.Traverse;
                }
            }

            if (settings.reloadCursors && ModCompatibility.ProgressionAmmunitionAvailable)
            {
                for (int i = 0; i < things.Count; i++)
                {
                    if (!ModCompatibility.TryGetAmmoRechargerType(things[i], pawn, out string ammoType)) continue;
                    if (!pawn.CanReserve(things[i], 1, -1, null, true)) continue;

                    target = things[i];
                    return ReloadCursorHint(ammoType);
                }
            }

            if (settings.openCursor)
            {
                for (int i = 0; i < things.Count; i++)
                {
                    if (!CanOpenNow(things[i])) continue;

                    target = things[i];
                    return CursorJobHint.Open;
                }
            }

            if (settings.launchCursor)
            {
                for (int i = 0; i < things.Count; i++)
                {
                    if (!CanLaunchGravship(things[i])) continue;

                    target = things[i];
                    return CursorJobHint.Launch;
                }
            }

            if (settings.chopCursor || settings.harvestCursor || settings.cutCursor)
            {
                var plant = cell.GetPlant(pawn.Map);
                if (plant != null)
                {
                    if (settings.cutCursor && CanCutPlantNow(plant))
                    {
                        target = plant;
                        return CursorJobHint.Cut;
                    }

                    if (CanHarvestNow(plant))
                    {
                        bool isTree = plant.def.plant.IsTree;
                        if (isTree ? settings.chopCursor : settings.harvestCursor)
                        {
                            target = plant;
                            return isTree ? CursorJobHint.Chop : CursorJobHint.Harvest;
                        }
                    }
                }
            }

            if (settings.smoothCursor && CanSmoothWallAt(cell)) return CursorJobHint.Smooth;

            if (settings.mineCursor && !pawn.WorkTypeIsDisabled(WorkTypeDefOf.Mining) && !IsSmoothingWallAt(cell))
            {
                var mineable = cell.GetFirstMineable(pawn.Map);
                if (mineable != null && pawn.CanReserve(mineable))
                {
                    target = mineable;
                    return CursorJobHint.Mine;
                }
            }

            if (settings.roofCursor && CanBuildRoofAt(cell)) return CursorJobHint.Roof;

            if (settings.plantCursor && CanSowAt(cell)) return CursorJobHint.Plant;

            if (settings.plowCursor && CanPlowAt(cell)) return CursorJobHint.Plow;

            bool bills = (BillCursorsEnabled || settings.billTooltips) && !ClickPicksUpAt(cell);
            if (bills || settings.researchCursor || settings.sleepCursor || settings.recreationCursor)
            {
                for (int i = 0; i < things.Count; i++)
                {
                    if (bills && things[i] is Building and IBillGiver && FindClickBill(things[i], cell))
                    {
                        target = things[i];
                        return ClassifyBill(things[i], jobCursorBill.recipe);
                    }
                    if (CanResearchAt(things[i]))
                    {
                        target = things[i];
                        return CursorJobHint.Research;
                    }
                    if (CanSleepIn(things[i]))
                    {
                        target = things[i];
                        return CursorJobHint.Sleep;
                    }
                    if (CanRecreateAt(things[i]))
                    {
                        target = things[i];
                        return CursorJobHint.Recreation;
                    }
                }
            }

            if (settings.tameCursor || settings.slaughterCursor || settings.releaseToWildCursor)
            {
                for (int i = 0; i < things.Count; i++)
                {
                    if (things[i] is not Pawn animal) continue;

                    var animalHint = AnimalCursorHint(animal, cell);
                    if (animalHint == CursorJobHint.None) continue;

                    target = animal;
                    return animalHint;
                }
            }

            return CursorJobHint.None;
        }

        private static CursorJobHint ReloadCursorHint(string ammoType)
        {
            switch (ammoType)
            {
                case "Arrow": return CursorJobHint.ReloadArrow;
                case "Charge": return CursorJobHint.ReloadCharge;
                default: return CursorJobHint.ReloadAmmo;
            }
        }

        private bool CanCutPlantNow(Plant plant)
        {
            if (plant.def.plant.IsTree) return false;
            if (pawn.Map.designationManager.DesignationOn(plant, DesignationDefOf.CutPlant) == null) return false;
            if (pawn.WorkTypeIsDisabled(WorkTypeDefOf.PlantCutting)) return false;
            if (!PlantUtility.PawnWillingToCutPlant_Job(plant, pawn)) return false;

            return pawn.CanReserve(plant, 1, -1, null, true);
        }

        private bool CanOpenNow(Thing thing)
        {
            if (thing is not IOpenable { CanOpen: true }) return false;
            if (thing.def.category == ThingCategory.Item) return false;
            if (!pawn.health.capacities.CapableOf(PawnCapacityDefOf.Manipulation)) return false;
            if (!pawn.CanReach(thing, PathEndMode.OnCell, Danger.Deadly)) return false;

            return pawn.CanReserve(thing, 1, -1, null, true);
        }

        private bool CanLaunchGravship(Thing thing)
        {
            var console = thing.TryGetComp<CompPilotConsole>();
            if (console == null) return false;
            if (!console.CanUseNow().Accepted) return false;
            if (!pawn.CanReach(thing, PathEndMode.InteractionCell, Danger.Deadly)) return false;

            return (bool)console.ValidateNavigator(pawn);
        }

        private bool CanRepair(Thing thing)
        {
            if (thing is not Building building) return false;
            if (!RepairUtility.PawnCanRepairNow(pawn, building)) return false;
            if (pawn.Faction == Faction.OfPlayer && !pawn.Map.areaManager.Home[building.Position]) return false;
            if (building.IsBurning()) return false;

            var designations = pawn.Map.designationManager;
            if (designations.DesignationOn(building, DesignationDefOf.Deconstruct) != null) return false;
            if (building.def.mineable && designations.DesignationAt(building.Position, DesignationDefOf.Mine) != null) return false;
            if (building.def.mineable && designations.DesignationAt(building.Position, DesignationDefOf.MineVein) != null) return false;

            return pawn.CanReserve(building, 1, -1, null, true);
        }

        private bool CanBuildRoofAt(IntVec3 cell)
        {
            if (pawn.WorkTypeIsDisabled(WorkTypeDefOf.Construction)) return false;

            var map = pawn.Map;
            var buildRoof = map.areaManager.BuildRoof;
            if (buildRoof.TrueCount == 0 || !buildRoof[cell]) return false;
            if (cell.Roofed(map)) return false;
            if (!pawn.CanReserve(cell, 1, -1, ReservationLayerDefOf.Ceiling, true)) return false;
            if (!RoofCollapseUtility.WithinRangeOfRoofHolder(cell, map)) return false;
            if (!RoofCollapseUtility.ConnectedToRoofHolder(cell, map, true)) return false;

            return RoofUtility.FirstBlockingThing(cell, map) == null;
        }

        private static WorkGiverDef _growerSowDef;
        private static WorkGiverDef GrowerSowDef => _growerSowDef ??= DefDatabase<WorkGiverDef>.GetNamedSilentFail("GrowerSow");

        private bool CanSowAt(IntVec3 cell)
        {
            var sowDef = GrowerSowDef;
            if (sowDef?.Worker is not WorkGiver_GrowerSow scanner) return false;

            var settable = cell.GetPlantToGrowSettable(pawn.Map);
            if (settable == null) return false;

            if (!CanOrderCellWork(sowDef, scanner, cell)) return false;

            var savedPlantDef = WorkGiver_Grower.wantedPlantDef;
            try
            {
                WorkGiver_Grower.wantedPlantDef = null;
                if (!SowSettableAccepts(scanner, settable)) return false;
                if (scanner.ShouldSkip(pawn, true)) return false;

                return IsFreshJob(scanner.JobOnCell(pawn, cell, true));
            }
            finally
            {
                WorkGiver_Grower.wantedPlantDef = savedPlantDef;
            }
        }

        private bool SowSettableAccepts(WorkGiver_GrowerSow scanner, IPlantToGrowSettable settable)
        {
            var maxDanger = pawn.NormalMaxDanger();
            if (settable is Building_PlantGrower grower)
            {
                return grower.Faction == Faction.OfPlayer
                    && scanner.ExtraRequirements(grower, pawn)
                    && !grower.IsForbidden(pawn)
                    && pawn.CanReach(grower, PathEndMode.OnCell, maxDanger)
                    && !grower.IsBurning();
            }
            if (settable is Zone_Growing zone)
            {
                return zone.cells.Count > 0
                    && scanner.ExtraRequirements(zone, pawn)
                    && !zone.ContainsStaticFire
                    && pawn.CanReach(zone.Cells[0], PathEndMode.OnCell, maxDanger);
            }
            return false;
        }

        private static WorkGiverDef _clearSnowDef;
        private static WorkGiverDef ClearSnowDef => _clearSnowDef ??= DefDatabase<WorkGiverDef>.GetNamedSilentFail("CleanClearSnow");

        private static WorkGiverDef _smoothWallsDef;
        private static WorkGiverDef SmoothWallsDef => _smoothWallsDef ??= DefDatabase<WorkGiverDef>.GetNamedSilentFail("ConstructSmoothWalls");

        private bool CanPlowAt(IntVec3 cell)
        {
            if (!pawn.Map.areaManager.SnowOrSandClear[cell] || HasWorkDesignationAt(cell)) return false;
            return CanDoCellWork(ClearSnowDef, cell);
        }

        private bool CanSmoothWallAt(IntVec3 cell)
        {
            if (pawn.Map.designationManager.DesignationAt(cell, DesignationDefOf.SmoothWall) == null) return false;
            return IsSmoothingWallAt(cell) || CanDoCellWork(SmoothWallsDef, cell);
        }

        private bool HasWorkDesignationAt(IntVec3 cell)
        {
            if (!pawn.Map.designationManager.TryGetCellDesignations(cell, out var designations)) return false;
            for (int i = 0; i < designations.Count; i++)
            {
                if (designations[i].def != DesignationDefOf.Plan) return true;
            }
            return false;
        }

        private bool TakesWorkOrders => pawn.thinker?.TryGetMainTreeThinkNode<JobGiver_Work>() != null;

        private bool CanOrderWork(WorkGiverDef def, WorkGiver_Scanner scanner)
        {
            if (!def.directOrderable || pawn.workSettings == null || !pawn.workSettings.EverWork) return false;
            if (pawn.workSettings.GetPriority(def.workType) == 0) return false;
            return !pawn.WorkTagIsDisabled(def.workTags) && scanner.MissingRequiredCapacity(pawn) == null;
        }

        private bool CanOrderCellWork(WorkGiverDef def, WorkGiver_Scanner scanner, IntVec3 cell)
        {
            if (!CanOrderWork(def, scanner) || !TakesWorkOrders) return false;
            return !cell.IsForbidden(pawn) && pawn.CanReach(cell, PathEndMode.Touch, Danger.Deadly);
        }

        private bool IsFreshJob(Job job)
        {
            if (job == null) return false;
            bool alreadyDoing = pawn.jobs.curJob != null && pawn.jobs.curJob.JobIsSameAs(pawn, job);
            JobMaker.ReturnToPool(job);
            return !alreadyDoing;
        }

        private bool CanDoCellWork(WorkGiverDef def, IntVec3 cell)
        {
            if (def?.Worker is not WorkGiver_Scanner scanner) return false;
            if (scanner.ShouldSkip(pawn, true) || !scanner.HasJobOnCell(pawn, cell, true)) return false;
            if (!CanOrderCellWork(def, scanner, cell)) return false;
            return IsFreshJob(scanner.JobOnCell(pawn, cell, true));
        }

        private Job ThingWorkJob(WorkGiverDef def, Thing thing)
        {
            if (def.Worker is not WorkGiver_Scanner scanner || !CanOrderWork(def, scanner)) return null;
            if (FloatMenuOptionProvider_WorkGivers.ScannerShouldSkip(pawn, scanner, thing)) return null;
            bool overrides = OverridesHasJobOnThing(scanner);
            Job job = overrides ? null : scanner.JobOnThing(pawn, thing, true);
            if (overrides ? !scanner.HasJobOnThing(pawn, thing, true) : job == null) return null;
            if (thing.IsForbidden(pawn) || !pawn.CanReach(thing, scanner.PathEndMode, Danger.Deadly))
            {
                JobMaker.ReturnToPool(job);
                return null;
            }

            job ??= scanner.JobOnThing(pawn, thing, true);
            if (job == null || pawn.jobs.curJob == null || !pawn.jobs.curJob.JobIsSameAs(pawn, job)) return job;
            JobMaker.ReturnToPool(job);
            return null;
        }

        private Job FirstThingWorkJob(Thing thing, out WorkGiverDef giver)
        {
            giver = null;
            if (!TakesWorkOrders) return null;

            var workTypes = DefDatabase<WorkTypeDef>.AllDefsListForReading;
            for (int i = 0; i < workTypes.Count; i++)
            {
                var givers = workTypes[i].workGiversByPriority;
                for (int j = 0; j < givers.Count; j++)
                {
                    var def = givers[j];
                    if (def.equivalenceGroup != null) continue;
                    try
                    {
                        var job = ThingWorkJob(def, thing);
                        if (job == null) continue;

                        giver = def;
                        return job;
                    }
                    catch (System.Exception ex)
                    {
                        Log.ErrorOnce($"[PerspectiveShift] Cursor check failed for {def.defName}: {ex}", ("PSCursor" + def.defName).GetHashCode());
                    }
                }
            }
            return null;
        }

        private readonly struct ClickContext : System.IDisposable
        {
            private readonly Pawn savedMakingFor;
            private readonly bool savedLeftClick;

            public ClickContext(Pawn pawn)
            {
                savedMakingFor = FloatMenuMakerMap.makingFor;
                savedLeftClick = IsAvatarLeftClick;
                FloatMenuMakerMap.makingFor = pawn;
                IsAvatarLeftClick = true;
            }

            public void Dispose()
            {
                FloatMenuMakerMap.makingFor = savedMakingFor;
                IsAvatarLeftClick = savedLeftClick;
            }
        }

        private CursorJobHint AnimalCursorHint(Pawn animal, IntVec3 cell)
        {
            if (!animal.IsAnimal || animal.Downed || animal.IsSelfShutdown()) return CursorJobHint.None;

            var settings = PerspectiveShiftMod.settings;
            var designations = pawn.Map.designationManager;
            bool marked = (settings.tameCursor && designations.DesignationOn(animal, DesignationDefOf.Tame) != null)
                || (settings.slaughterCursor && animal.ShouldBeSlaughtered())
                || (settings.releaseToWildCursor && designations.DesignationOn(animal, DesignationDefOf.ReleaseAnimalToWild) != null);
            if (!marked || HasWorkDesignationAt(cell)) return CursorJobHint.None;

            WorkGiverDef giver;
            using (new ClickContext(pawn))
            {
                var job = FirstThingWorkJob(animal, out giver);
                if (job != null) JobMaker.ReturnToPool(job);
            }

            switch (giver?.Worker)
            {
                case WorkGiver_Tame when settings.tameCursor: return CursorJobHint.Tame;
                case WorkGiver_Slaughter when settings.slaughterCursor: return CursorJobHint.Slaughter;
                case WorkGiver_ReleaseAnimalsToWild when settings.releaseToWildCursor: return CursorJobHint.ReleaseToWild;
                default: return CursorJobHint.None;
            }
        }

        private static WorkGiverDef _brewBillsDef;
        private static WorkGiverDef BrewBillsDef => _brewBillsDef ??= DefDatabase<WorkGiverDef>.GetNamedSilentFail("DoBillsBrew");

        private static WorkGiverDef _tailorBillsDef;
        private static WorkGiverDef TailorBillsDef => _tailorBillsDef ??= DefDatabase<WorkGiverDef>.GetNamedSilentFail("DoBillsMakeApparel");

        private static bool MakesSpecial(RecipeDef recipe, SpecialProductType type) => recipe.specialProducts != null && recipe.specialProducts.Contains(type);

        private static bool BillCursorsEnabled
        {
            get
            {
                var settings = PerspectiveShiftMod.settings;
                return settings.cookCursor || settings.butcherCursor || settings.stonecuttingCursor
                    || settings.brewCursor || settings.smeltCursor || settings.tailorCursor;
            }
        }

        private bool FindClickBill(Thing bench, IntVec3 cell)
        {
            if (BenchClaimsClick(bench)) return false;

            bool designated = HasWorkDesignationAt(cell);
            int signature = BillStackSignature((IBillGiver)bench);
            if (bench == billMemoBench && pawn.Position == billMemoPawnCell && pawn.CurJob == billMemoJob
                && designated == billMemoDesignated && signature == billMemoSignature && Time.realtimeSinceStartup < billMemoStaleAt)
            {
                if (billMemoBill != null)
                {
                    SetClickBill(billMemoBill, billMemoResume);
                    jobCursorBillThings.AddRange(billMemoThings);
                    jobCursorBillCounts.AddRange(billMemoCounts);
                }
                return billMemoResult;
            }

            long start = System.Diagnostics.Stopwatch.GetTimestamp();
            bool result;
            using (new ClickContext(pawn)) result = ClickBill(bench, cell);
            float cost = (System.Diagnostics.Stopwatch.GetTimestamp() - start) / (float)System.Diagnostics.Stopwatch.Frequency;

            billMemoBench = bench;
            billMemoPawnCell = pawn.Position;
            billMemoJob = pawn.CurJob;
            billMemoDesignated = designated;
            billMemoSignature = signature;
            billMemoStaleAt = Time.realtimeSinceStartup + Mathf.Clamp(cost * BillMemoCostFactor, JobCursorRefresh, BillMemoMaxAge);
            billMemoResult = result;
            billMemoBill = jobCursorBill;
            billMemoResume = jobCursorBillResume;
            billMemoThings.Clear();
            billMemoThings.AddRange(jobCursorBillThings);
            billMemoCounts.Clear();
            billMemoCounts.AddRange(jobCursorBillCounts);
            return result;
        }

        private static int BillStackSignature(IBillGiver billGiver)
        {
            var bills = billGiver.BillStack;
            int signature = bills.Count;
            for (int i = 0; i < bills.Count; i++)
            {
                signature = signature * 31 + bills[i].loadID * 2 + (bills[i].suspended ? 1 : 0);
            }
            return signature;
        }

        private Job ScannerJobOnThing(WorkGiver_Scanner scanner, Thing thing, out bool hasJob)
        {
            if (!OverridesHasJobOnThing(scanner))
            {
                var job = scanner.JobOnThing(pawn, thing, true);
                hasJob = job != null;
                return job;
            }
            hasJob = scanner.HasJobOnThing(pawn, thing, true);
            return hasJob ? scanner.JobOnThing(pawn, thing, true) : null;
        }

        private static bool OverridesHasJobOnThing(WorkGiver_Scanner scanner)
        {
            var type = scanner.GetType();
            if (!overridesHasJobOnThing.TryGetValue(type, out bool overrides))
            {
                var method = type.GetMethod(nameof(WorkGiver_Scanner.HasJobOnThing), System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance,
                    null, new[] { typeof(Pawn), typeof(Thing), typeof(bool) }, null);
                overrides = method == null || method.DeclaringType != typeof(WorkGiver_Scanner);
                overridesHasJobOnThing[type] = overrides;
            }
            return overrides;
        }

        private bool BenchClaimsClick(Thing bench)
        {
            var designations = pawn.Map.designationManager;
            if (designations.DesignationOn(bench, DesignationDefOf.Deconstruct) != null) return true;
            if (!bench.def.Minifiable || (bench.Faction != pawn.Faction && !bench.def.building.alwaysUninstallable)) return false;
            return InstallBlueprintUtility.ExistingBlueprintFor(bench) != null || designations.DesignationOn(bench, DesignationDefOf.Uninstall) != null;
        }

        private bool ClickPicksUpAt(IntVec3 cell)
        {
            var item = cell.GetFirstItem(pawn.Map);
            if (item != null)
            {
                if (item is MinifiedThing && pawn.Map.designationManager.DesignationOn(item, DesignationDefOf.Deconstruct) != null) return false;
                return item is not Skyfaller && item is not ActiveTransporter && item.def.EverHaulable;
            }
            var other = cell.GetFirstPawn(pawn.Map);
            return other != null && other != pawn && (other.Downed || other.IsSelfShutdown());
        }

        private static CursorJobHint ClassifyBill(Thing bench, RecipeDef recipe)
        {
            var settings = PerspectiveShiftMod.settings;
            var category = BillCategory(bench, recipe);
            switch (category)
            {
                case CursorJobHint.Butcher: return settings.butcherCursor ? category : CursorJobHint.None;
                case CursorJobHint.Smelt: return settings.smeltCursor ? category : CursorJobHint.None;
                case CursorJobHint.Stonecutting: return settings.stonecuttingCursor ? category : CursorJobHint.None;
                case CursorJobHint.Brew: return settings.brewCursor ? category : CursorJobHint.None;
                case CursorJobHint.Tailor: return settings.tailorCursor ? category : CursorJobHint.None;
                case CursorJobHint.Cook: return settings.cookCursor ? category : CursorJobHint.None;
                default: return CursorJobHint.None;
            }
        }

        private static CursorJobHint BillCategory(Thing bench, RecipeDef recipe)
        {
            if (MakesSpecial(recipe, SpecialProductType.Butchery)) return CursorJobHint.Butcher;
            if (MakesSpecial(recipe, SpecialProductType.Smelted)) return CursorJobHint.Smelt;

            if (MakesSpecial(recipe, SpecialProductType.StoneBlocks) || recipe.ProducedThingDef?.IsWithinCategory(ThingCategoryDefOf.StoneBlocks) == true)
                return CursorJobHint.Stonecutting;

            if (BrewBillsDef?.fixedBillGiverDefs?.Contains(bench.def) == true || recipe.ProducedThingDef == ThingDefOf.Wort) return CursorJobHint.Brew;
            if (TailorBillsDef?.fixedBillGiverDefs?.Contains(bench.def) == true) return CursorJobHint.Tailor;

            bool cooking = bench.def.building?.isMealSource == true || recipe.ProducedThingDef?.ingestible?.IsMeal == true;
            return cooking ? CursorJobHint.Cook : CursorJobHint.None;
        }

        private bool ClickBill(Thing bench, IntVec3 cell)
        {
            if (pawn.workSettings == null) return false;

            var workGivers = pawn.workSettings.WorkGiversInOrderNormal;
            for (int i = 0; i < workGivers.Count; i++)
            {
                if (workGivers[i] is not WorkGiver_Scanner scanner || !scanner.PotentialWorkThingRequest.Accepts(bench)) continue;
                var job = ScannerJobOnThing(scanner, bench, out bool hasJob);
                if (!hasJob)
                {
                    if (pawn.WorkTypeIsDisabled(scanner.def.workType)) return false;
                    continue;
                }
                if (job == null) continue;

                bool clickable = job.def != JobDefOf.HaulToContainer && job.def != JobDefOf.Refuel && job.def != JobDefOf.RefuelAtomic
                    && JobTargetsInRange(job) && !job.def.HasModExtension<DisableLeftClickExtension>();
                bool isBill = clickable && job.def == JobDefOf.DoBill && job.bill != null;
                if (isBill) CaptureClickBill(job);
                bool fresh = IsFreshJob(job);
                if (!clickable) continue;
                if (isBill && !fresh) ClearClickBill();
                return isBill && fresh;
            }

            if (HasWorkDesignationAt(cell)) return false;

            var menuJob = FirstThingWorkJob(bench, out _);
            if (menuJob == null) return false;

            bool menuBill = menuJob.def == JobDefOf.DoBill && menuJob.bill != null;
            if (menuBill) CaptureClickBill(menuJob);
            JobMaker.ReturnToPool(menuJob);
            return menuBill;
        }

        private static readonly Dictionary<ThingDef, WorkGiverDef> depositGiverByBench = new Dictionary<ThingDef, WorkGiverDef>();
        private static readonly List<Thing> depositCandidates = new List<Thing>();
        private static readonly List<ThingCount> depositChosen = new List<ThingCount>();

        private CursorJobHint EvaluateCarriedTarget(IntVec3 cell, Thing carried, out Thing target)
        {
            target = null;
            if ((!BillCursorsEnabled && !PerspectiveShiftMod.settings.billTooltips) || carried is Pawn || !cell.InBounds(pawn.Map)) return CursorJobHint.None;
            if (pawn.Position.DistanceTo(cell) > PerspectiveShiftMod.settings.grabRange) return CursorJobHint.None;

            var things = cell.GetThingList(pawn.Map);
            for (int i = 0; i < things.Count; i++)
            {
                var thing = things[i];
                if (ClaimsCarriedClick(thing, carried)) return CursorJobHint.None;
                if (thing is not IBillGiver billGiver || !AcceptsDeposit(billGiver, carried)) continue;
                if (thing is not Building) return CursorJobHint.None;

                try
                {
                    using (new ClickContext(pawn)) DepositBill(thing, carried);
                }
                catch (System.Exception ex)
                {
                    ClearClickBill();
                    Log.ErrorOnce($"[PerspectiveShift] Carried cursor check failed for {thing.def.defName}: {ex}", ("PSCarriedCursor" + thing.def.defName).GetHashCode());
                }

                if (jobCursorBill == null) return CursorJobHint.None;
                target = thing;
                return ClassifyBill(thing, jobCursorBill.recipe);
            }
            return CursorJobHint.None;
        }

        private static bool ClaimsCarriedClick(Thing thing, Thing carried)
        {
            switch (thing)
            {
                case Blueprint_Install:
                    return true;
                case Blueprint_Build blueprint:
                    return blueprint.def.entityDefToBuild is not TerrainDef || blueprint.ThingCountNeeded(carried.def) > 0;
                case Frame frame:
                    return frame.def.entityDefToBuild is not TerrainDef || frame.ThingCountNeeded(carried.def) > 0;
                case Pawn patient:
                    return carried.def.IsMedicine && patient.health.HasHediffsNeedingTend();
            }

            var refuelable = thing.TryGetComp<CompRefuelable>();
            return refuelable != null && refuelable.Props.fuelFilter.Allows(carried) && refuelable.GetFuelCountToFullyRefuel() > 0;
        }

        private bool AcceptsDeposit(IBillGiver billGiver, Thing carried)
        {
            var bills = billGiver.BillStack;
            for (int i = 0; i < bills.Count; i++)
            {
                if (bills[i].ShouldDoNow() && BillWantsCarried(bills[i], carried)) return true;
            }
            return false;
        }

        private static WorkGiverDef DepositGiverDef(Thing bench)
        {
            if (depositGiverByBench.TryGetValue(bench.def, out var cached)) return cached;

            WorkGiverDef found = null;
            var defs = DefDatabase<WorkGiverDef>.AllDefsListForReading;
            for (int i = 0; i < defs.Count; i++)
            {
                if (defs[i].Worker is WorkGiver_DoBill doBill && doBill.ThingIsUsableBillGiver(bench))
                {
                    found = defs[i];
                    break;
                }
            }
            depositGiverByBench[bench.def] = found;
            return found;
        }

        private void DepositBill(Thing bench, Thing carried)
        {
            var giverDef = DepositGiverDef(bench);
            var billGiver = (IBillGiver)bench;
            if (giverDef == null || !bench.def.hasInteractionCell || bench.IsBurning()) return;
            if (!billGiver.BillStack.AnyShouldDoNow || !billGiver.UsableForBillsAfterFueling()) return;
            if (!pawn.CanReserve(bench, 1, -1, null, true) || !pawn.CanReserveSittableOrSpot(bench.InteractionCell, bench, true)) return;

            var refuelable = bench.TryGetComp<CompRefuelable>();
            if (refuelable != null && !refuelable.HasFuel) return;

            var bills = billGiver.BillStack;
            for (int i = 0; i < bills.Count; i++)
            {
                var bill = bills[i];
                if (!BillWantsCarried(bill, carried)) continue;
                if (!bill.CompletableEver || Find.TickManager.TicksGame <= bill.nextTickToSearchForIngredients) continue;
                if (bill.recipe.requiredGiverWorkType != null && bill.recipe.requiredGiverWorkType != giverDef.workType) continue;
                if (!bill.ShouldDoNow() || !bill.PawnAllowedToStartAnew(pawn) || bill.recipe.FirstSkillRequirementPawnDoesntSatisfy(pawn) != null) continue;

                if (bill is Bill_ProductionWithUft uftBill)
                {
                    var uft = uftBill.BoundUft;
                    if (uft != null && (uftBill.BoundWorker != pawn || uft.IsForbidden(pawn) || (uft != carried && !pawn.CanReserveAndReach(uft, PathEndMode.Touch, Danger.Deadly)))) continue;

                    uft ??= carried is UnfinishedThing carriedUft && !carriedUft.IsForbidden(pawn) ? carriedUft : WorkGiver_DoBill.ClosestUnfinishedThingForBill(pawn, uftBill);
                    if (uft != null)
                    {
                        if (BenchClear(billGiver, uft))
                        {
                            SetClickBill(bill, true);
                            AddClickBillThing(uft, 1);
                        }
                        return;
                    }
                }
                if (bill is Bill_Autonomous { State: not FormingState.Gathering })
                {
                    SetClickBill(bill, true);
                    return;
                }

                if (HasIngredientsWithCarried(bill, bench, carried))
                {
                    if (!BenchClear(billGiver)) ClearClickBill();
                    return;
                }
            }
        }

        private bool BenchClear(IBillGiver billGiver, Thing ignore = null)
        {
            foreach (var stackCell in billGiver.IngredientStackCells)
            {
                var item = pawn.Map.thingGrid.ThingAt(stackCell, ThingCategory.Item);
                if (item != null && item != ignore) return false;
            }
            return true;
        }

        private bool HasIngredientsWithCarried(Bill bill, Thing bench, Thing carried)
        {
            float radius = PerspectiveShiftMod.settings.grabRange + 1.5f;
            bool carriedCounts = !carried.IsForbidden(pawn) && WorkGiver_DoBill.IsUsableIngredient(carried, bill)
                && (pawn.Position - bench.Position).LengthHorizontalSquared < radius * radius;
            var rootCell = bench.InteractionCell;
            try
            {
                bool found = WorkGiver_DoBill.TryFindBestIngredientsHelper(
                    t => WorkGiver_DoBill.IsUsableIngredient(t, bill),
                    candidates =>
                    {
                        depositCandidates.Clear();
                        depositCandidates.AddRange(candidates);
                        if (carriedCounts) depositCandidates.Add(carried);
                        return WorkGiver_DoBill.TryFindBestBillIngredientsInSet(depositCandidates, bill, depositChosen, rootCell, false, null);
                    },
                    bill.recipe.ingredients, pawn, bench, depositChosen, radius);
                if (!found) return false;

                SetClickBill(bill, false);
                for (int i = 0; i < depositChosen.Count; i++) AddClickBillThing(depositChosen[i].Thing, depositChosen[i].Count);
                if (bill.xenogerm != null) AddClickBillThing(bill.xenogerm, 1);
                return true;
            }
            finally
            {
                depositCandidates.Clear();
                depositChosen.Clear();
            }
        }

        private bool CanResearchAt(Thing thing)
        {
            if (thing is not Building_ResearchBench bench) return false;
            if (!PerspectiveShiftMod.settings.researchCursor) return false;
            if (pawn.CurJobDef == JobDefOf.Research) return false;
            if (pawn.WorkTypeIsDisabled(WorkTypeDefOf.Research)) return false;

            var project = Find.ResearchManager.GetProject();
            if (project == null || !project.CanBeResearchedAt(bench, false)) return false;
            if (!pawn.CanReserve(bench, 1, -1, null, true)) return false;

            return !bench.def.hasInteractionCell || pawn.CanReserveSittableOrSpot(bench.InteractionCell, true);
        }

        private bool CanSleepIn(Thing thing)
        {
            if (thing is not Building_Bed bed) return false;
            if (!PerspectiveShiftMod.settings.sleepCursor) return false;
            if (!pawn.Awake() || pawn.InBed()) return false;
            if (bed.ForPrisoners || bed.Medical || pawn.needs?.rest == null) return false;
            if (!RestUtility.CanUseBedEver(pawn, bed.def)) return false;

            return pawn.CanReserveAndReach(bed, PathEndMode.OnCell, Danger.Deadly, bed.SleepingSlotsCount, 0);
        }

        private bool CanRecreateAt(Thing thing)
        {
            if (!PerspectiveShiftMod.settings.recreationCursor) return false;
            if (thing is not Building || pawn.needs?.joy == null) return false;
            if (pawn.CurJob != null && pawn.CurJob.targetA.Thing == thing && pawn.CurJobDef.joyKind != null) return false;
            if (thing.TryGetComp<CompPowerTrader>() is { PowerOn: false }) return false;

            var joyGivers = DefDatabase<JoyGiverDef>.AllDefsListForReading;
            for (int i = 0; i < joyGivers.Count; i++)
            {
                var joyGiver = joyGivers[i];
                if (joyGiver.thingDefs == null || !joyGiver.thingDefs.Contains(thing.def)) continue;

                Job job;
                if (joyGiver.Worker is JoyGiver_WatchBuilding)
                {
                    if (!WatchBuildingUtility.CalculateWatchCells(thing.def, thing.Position, thing.Rotation, pawn.Map).Contains(pawn.Position)) continue;
                    job = JobMaker.MakeJob(joyGiver.jobDef, thing, pawn.Position);
                }
                else if (joyGiver.Worker is JoyGiver_InteractBuilding worker)
                {
                    job = worker.TryGivePlayJob(pawn, thing);
                    if (job == null) continue;
                }
                else continue;

                bool startable = JobTargetsInRange(job) && !job.def.HasModExtension<DisableLeftClickExtension>();
                JobMaker.ReturnToPool(job);
                if (startable) return true;
            }
            return false;
        }

        private static void DrawJobCursor(Vector2 center, Texture2D tex)
        {
            if (Event.current.type != EventType.Repaint) return;
            if (tex == null) return;

            const float size = 22f;
            GUI.DrawTexture(new Rect(center.x - size / 2f, center.y - size / 2f, size, size), tex);
        }

        private void DrawDropCursor(Vector2 center)
        {
            if (Event.current.type != EventType.Repaint) return;

            var tex = DropCursorTex;
            if (tex == null) return;

            const float size = 22f;
            GUI.DrawTexture(new Rect(center.x - size / 2f, center.y - size / 2f, size, size), tex);
        }
    }
}
