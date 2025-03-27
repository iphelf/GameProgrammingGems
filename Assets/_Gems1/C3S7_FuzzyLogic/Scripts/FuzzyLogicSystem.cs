using Accord.Fuzzy;
using static Accord.Fuzzy.TrapezoidalFunction.EdgeType;

namespace _Gems1.C3S7_FuzzyLogic.Scripts
{
	internal class FuzzyLogicSystem
	{
		private InferenceSystem _InferenceSystem;

		internal FuzzyLogicSystem()
		{
			var fuzzyDb = _CreateDatabase();
			_InferenceSystem = _CreateInferenceSystem(fuzzyDb);
		}

		internal float InferSpeedScale(float distance, float distanceDelta)
		{
			_InferenceSystem.SetInput("Distance", distance);
			_InferenceSystem.SetInput("Delta", distanceDelta);
			var speedScale = _InferenceSystem.Evaluate("Action");
			return speedScale;
		}

		private Database _CreateDatabase()
		{
			var fuzzyDb = new Database();
			fuzzyDb.AddVariable(_CreateDistanceVariable());
			fuzzyDb.AddVariable(_CreateDistanceDeltaVariable());
			// fuzzyDb.AddVariable(_CreateActionVariable());
			fuzzyDb.AddVariable(_CreateMildActionVariable(1.05f));
			return fuzzyDb;
		}

		private InferenceSystem _CreateInferenceSystem(Database fuzzyDb)
		{
			var inf = new InferenceSystem(fuzzyDb, new CentroidDefuzzifier(1000));
			inf.NewRule("Rule1x1", "IF Distance IS VerySmall AND Delta IS ShrinkingFast THEN Action IS BrakeHard");
			inf.NewRule("Rule1x2", "IF Distance IS VerySmall AND Delta IS Shrinking THEN Action IS BrakeHard");
			inf.NewRule("Rule1x3", "IF Distance IS VerySmall AND Delta IS Stable THEN Action IS SlowDown");
			inf.NewRule("Rule1x4", "IF Distance IS VerySmall AND Delta IS Growing THEN Action IS SlowDown");
			inf.NewRule("Rule1x5", "IF Distance IS VerySmall AND Delta IS GrowingFast THEN Action IS MaintainSpeed");
			inf.NewRule("Rule2x1", "IF Distance IS Small AND Delta IS ShrinkingFast THEN Action IS BrakeHard");
			inf.NewRule("Rule2x2", "IF Distance IS Small AND Delta IS Shrinking THEN Action IS SlowDown");
			inf.NewRule("Rule2x3", "IF Distance IS Small AND Delta IS Stable THEN Action IS SlowDown");
			inf.NewRule("Rule2x4", "IF Distance IS Small AND Delta IS Growing THEN Action IS MaintainSpeed");
			inf.NewRule("Rule2x5", "IF Distance IS Small AND Delta IS GrowingFast THEN Action IS SpeedUp");
			inf.NewRule("Rule3x1", "IF Distance IS Perfect AND Delta IS ShrinkingFast THEN Action IS SlowDown");
			inf.NewRule("Rule3x2", "IF Distance IS Perfect AND Delta IS Shrinking THEN Action IS SlowDown");
			inf.NewRule("Rule3x3", "IF Distance IS Perfect AND Delta IS Stable THEN Action IS MaintainSpeed");
			inf.NewRule("Rule3x4", "IF Distance IS Perfect AND Delta IS Growing THEN Action IS SpeedUp");
			inf.NewRule("Rule3x5", "IF Distance IS Perfect AND Delta IS GrowingFast THEN Action IS SpeedUp");
			inf.NewRule("Rule4x1", "IF Distance IS Big AND Delta IS ShrinkingFast THEN Action IS SlowDown");
			inf.NewRule("Rule4x2", "IF Distance IS Big AND Delta IS Shrinking THEN Action IS MaintainSpeed");
			inf.NewRule("Rule4x3", "IF Distance IS Big AND Delta IS Stable THEN Action IS SpeedUp");
			inf.NewRule("Rule4x4", "IF Distance IS Big AND Delta IS Growing THEN Action IS SpeedUp");
			inf.NewRule("Rule4x5", "IF Distance IS Big AND Delta IS GrowingFast THEN Action IS FloorIt");
			inf.NewRule("Rule5x1", "IF Distance IS VeryBig AND Delta IS ShrinkingFast THEN Action IS MaintainSpeed");
			inf.NewRule("Rule5x2", "IF Distance IS VeryBig AND Delta IS Shrinking THEN Action IS SpeedUp");
			inf.NewRule("Rule5x3", "IF Distance IS VeryBig AND Delta IS Stable THEN Action IS SpeedUp");
			inf.NewRule("Rule5x4", "IF Distance IS VeryBig AND Delta IS Growing THEN Action IS FloorIt");
			inf.NewRule("Rule5x5", "IF Distance IS VeryBig AND Delta IS GrowingFast THEN Action IS FloorIt");
			return inf;
		}

		private LinguisticVariable _CreateDistanceVariable()
		{
			var distanceVariable = new LinguisticVariable("Distance", 0, 4);
			var distanceTermVerySmall = new FuzzySet("VerySmall", new TrapezoidalFunction(0, 1, Left));
			var distanceTermSmall = new FuzzySet("Small", new TrapezoidalFunction(0, 1, 2));
			var distanceTermPerfect = new FuzzySet("Perfect", new TrapezoidalFunction(1, 2, 3));
			var distanceTermBig = new FuzzySet("Big", new TrapezoidalFunction(2, 3, 4));
			var distanceTermVeryBig = new FuzzySet("VeryBig", new TrapezoidalFunction(3, 3, Right));
			distanceVariable.AddLabel(distanceTermVerySmall);
			distanceVariable.AddLabel(distanceTermSmall);
			distanceVariable.AddLabel(distanceTermPerfect);
			distanceVariable.AddLabel(distanceTermBig);
			distanceVariable.AddLabel(distanceTermVeryBig);
			return distanceVariable;
		}

		private LinguisticVariable _CreateDistanceDeltaVariable()
		{
			var deltaVariable = new LinguisticVariable("Delta", 0, 4);
			var deltaTermShrinkingFast = new FuzzySet("ShrinkingFast", new TrapezoidalFunction(0, 1, Left));
			var deltaTermShrinking = new FuzzySet("Shrinking", new TrapezoidalFunction(0, 1, 2));
			var deltaTermStable = new FuzzySet("Stable", new TrapezoidalFunction(1, 2, 3));
			var deltaTermGrowing = new FuzzySet("Growing", new TrapezoidalFunction(2, 3, 4));
			var deltaTermGrowingFast = new FuzzySet("GrowingFast", new TrapezoidalFunction(3, 3, Right));
			deltaVariable.AddLabel(deltaTermShrinkingFast);
			deltaVariable.AddLabel(deltaTermShrinking);
			deltaVariable.AddLabel(deltaTermStable);
			deltaVariable.AddLabel(deltaTermGrowing);
			deltaVariable.AddLabel(deltaTermGrowingFast);
			return deltaVariable;
		}

		private LinguisticVariable _CreateActionVariable()
		{
			var actionVariable = new LinguisticVariable("Action", 0.5f, 2);
			var actionTermBrakeHard = new FuzzySet("BrakeHard", new TrapezoidalFunction(0.5f, 0.75f, Left));
			var actionTermSlowDown = new FuzzySet("SlowDown", new TrapezoidalFunction(0.5f, 0.75f, 1.0f));
			var actionTermMaintainSpeed = new FuzzySet("MaintainSpeed", new TrapezoidalFunction(0.75f, 1.0f, 1.5f));
			var actionTermSpeedUp = new FuzzySet("SpeedUp", new TrapezoidalFunction(1.0f, 1.5f, 2.0f));
			var actionTermFloorIt = new FuzzySet("FloorIt", new TrapezoidalFunction(1.5f, 2.0f, Right));
			actionVariable.AddLabel(actionTermBrakeHard);
			actionVariable.AddLabel(actionTermSlowDown);
			actionVariable.AddLabel(actionTermMaintainSpeed);
			actionVariable.AddLabel(actionTermSpeedUp);
			actionVariable.AddLabel(actionTermFloorIt);
			return actionVariable;
		}

		private LinguisticVariable _CreateMildActionVariable(float step)
		{
			var s = new[] { 1 / step / step, 1 / step, 1, step, step * step };
			var actionVariable = new LinguisticVariable("Action", s[0], s[^1]);
			var actionTermBrakeHard = new FuzzySet("BrakeHard", new TrapezoidalFunction(s[0], s[1], Left));
			var actionTermSlowDown = new FuzzySet("SlowDown", new TrapezoidalFunction(s[0], s[1], s[2]));
			var actionTermMaintainSpeed = new FuzzySet("MaintainSpeed", new TrapezoidalFunction(s[1], s[2], s[3]));
			var actionTermSpeedUp = new FuzzySet("SpeedUp", new TrapezoidalFunction(s[2], s[3], s[4]));
			var actionTermFloorIt = new FuzzySet("FloorIt", new TrapezoidalFunction(s[3], s[4], Right));
			actionVariable.AddLabel(actionTermBrakeHard);
			actionVariable.AddLabel(actionTermSlowDown);
			actionVariable.AddLabel(actionTermMaintainSpeed);
			actionVariable.AddLabel(actionTermSpeedUp);
			actionVariable.AddLabel(actionTermFloorIt);
			return actionVariable;
		}
	}
}