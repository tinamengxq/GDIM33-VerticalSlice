using Unity.VisualScripting;

public static class EventNames
{
    public static string IncreaseOxygen = "IncreaseOxygen";
}

[UnitTitle("On Increase O2")]
[UnitCategory("Events\\MyEvents")]
public class GraphLinkO2 : EventUnit<GameController>
{
    protected override bool register => true;

    public ValueOutput result {get; private set;}
    public override EventHook GetHook(GraphReference graphReference)
    {
        return new EventHook(EventNames.IncreaseOxygen);
    }

    protected override void Definition()
    {
        base.Definition();
        result = ValueOutput<GameController>("gameController");
    }

    protected override void AssignArguments(Flow flow, GameController data)
    {
        base.AssignArguments(flow, data);
        flow.SetValue(result , data);
    }
}



