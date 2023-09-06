using Grpc.Core;
using GrpcService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Services
{
    public class SensorService : Sensor.SensorBase
    {
        private readonly SensorDbContext _dbContext;

        public SensorService(SensorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task GetAll(Empty em, IServerStreamWriter<SensorVal> responseStream, ServerCallContext context)
        {
            var sensorValues = await _dbContext.SensorValues.ToListAsync();
            foreach (var sv in sensorValues)
            {
                await responseStream.WriteAsync(new SensorVal
                {
                    Id = sv.Id,
                    Date = sv.Date.ToString(),
                    Temperature = sv.Temperature,
                    Humidity = sv.Humidity,
                    Light = sv.Light,
                    CO2 = sv.Co2
                });
            }
        }

        public override async Task<SensorVal> GetById(SensorValId sensorValid, ServerCallContext context)
        {
            var sv = await _dbContext.SensorValues.FindAsync(sensorValid.Id);

            return await Task.FromResult(new SensorVal
            {
                Id = sv.Id,
                Date = sv.Date.ToString(),
                Temperature = sv.Temperature,
                Humidity = sv.Humidity,
                Light = sv.Light,
                CO2 = sv.Co2
            });
        }

        public override async Task<SensorVal> AddValue(SensorVal sensorVal, ServerCallContext context)
        {
            var sv = new SensorValue()
            {
                Id = sensorVal.Id,
                Date = DateTime.Now,
                Temperature = sensorVal.Temperature,
                Humidity = sensorVal.Humidity,
                Light = sensorVal.Light,
                Co2 = sensorVal.CO2
            };

            await _dbContext.SensorValues.AddAsync(sv);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new SensorVal
            {
                Id = sv.Id,
                Date = sv.Date.ToString(),
                Temperature = sv.Temperature,
                Humidity = sv.Humidity,
                Light = sv.Light,
                CO2 = sv.Co2
            });
        }

        public override async Task<SensorVal> UpdateValue(SensorVal sensorVal, ServerCallContext context)
        {
            var sv = await _dbContext.SensorValues.FindAsync(sensorVal.Id);
            sv.Date = DateTime.Now;
            sv.Temperature = sensorVal.Temperature;
            sv.Humidity = sensorVal.Humidity;
            sv.Light = sensorVal.Light;
            sv.Co2 = sensorVal.CO2;

            _dbContext.SensorValues.Update(sv);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new SensorVal
            {
                Id = sv.Id,
                Date = sv.Date.ToString(),
                Temperature = sv.Temperature,
                Humidity = sv.Humidity,
                Light = sv.Light,
                CO2 = sv.Co2
            });
        }

        public override async Task<Empty> DeleteValue(SensorValId sensorValId, ServerCallContext context)
        {
            var sv = await _dbContext.SensorValues.FindAsync(sensorValId.Id);

            _dbContext.SensorValues.Remove(sv);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new Empty { });
        }
    }
}
