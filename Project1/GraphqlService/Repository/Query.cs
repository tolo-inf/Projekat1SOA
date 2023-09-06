namespace GraphqlService.Repository
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<SensorValue> GetSensorValues([Service] SensorDbContext context) =>
            context.SensorValues;
    }
}
