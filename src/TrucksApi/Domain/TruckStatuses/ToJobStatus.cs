﻿namespace TrucksApi.Domain.TruckStatuses;

public class ToJobStatus : TruckStatus
{
    public override string[] AllowedChangelist { get; } = { OutOfService, AtJob };
    public override string Status { get; } = ToJob;
}