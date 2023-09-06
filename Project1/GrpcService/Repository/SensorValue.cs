using System;
using System.Collections.Generic;

namespace GrpcService.Repository;

public partial class SensorValue
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public double Temperature { get; set; }

    public double Humidity { get; set; }

    public double Light { get; set; }

    public double Co2 { get; set; }
}
