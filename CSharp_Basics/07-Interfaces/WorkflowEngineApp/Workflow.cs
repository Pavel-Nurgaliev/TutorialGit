internal class Workflow
{
    private List<IActivity> _activitiesList;
    
    internal List<IActivity> GetActivities()
    {
        return _activitiesList;
    }
    internal void AddActivity(IActivity activity)
    {
        if (_activitiesList is null)
        {
            _activitiesList = new List<IActivity>();
        }

        _activitiesList.Add(activity);
    }
}