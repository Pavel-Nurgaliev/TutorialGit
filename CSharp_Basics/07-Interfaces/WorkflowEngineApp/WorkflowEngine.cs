internal class WorkflowEngine
{
    internal void Run(Workflow workflow)
    {
        if (workflow is null)
        {
            throw new NullReferenceException("Workflow cannot be null");
        }

        foreach (var activity in workflow.GetActivities())
        {
            activity.Execute();
        }

        Console.WriteLine("All activities are executed");
    }
}