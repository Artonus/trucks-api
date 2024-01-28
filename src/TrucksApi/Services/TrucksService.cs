﻿using TrucksApi.Domain;
using TrucksApi.Repositories.Abstract;
using TrucksApi.Services.Abstract;
using TrucksApi.Mappings;

namespace TrucksApi.Services;

public class TrucksService : ITrucksService
{
    private readonly ITrucksRepository _trucksRepository;

    public TrucksService(ITrucksRepository trucksRepository)
    {
        _trucksRepository = trucksRepository;
    }

    public async Task<TruckResult> Add(TruckModel truck)
    {
        var exists = await _trucksRepository.GetById(truck.Id);
        if (exists is not null)
        {
            return new TruckResult("There is already a truck with specified id");
        }
        var res = await _trucksRepository.Add(truck.ToDto());

        if (res is null)
        {
            return new TruckResult("Could not create specified truck");
        }
        await _trucksRepository.CommitChanges();
        return new TruckResult(res.ToModel());

    }

    public async Task Delete(string id)
    {
        var truck = await _trucksRepository.GetById(id);

        if (truck is not null)
        {
            _trucksRepository.Delete(truck);
        }
        await _trucksRepository.CommitChanges();
    }

    public async Task<List<TruckModel>> GetAll()
    {
        var trucks = await _trucksRepository.GetAll();
        return trucks.ToModel();
    }

    public async Task<TruckModel?> GetById(string id)
    {
        var truck = await _trucksRepository.GetById(id);
        return truck?.ToModel();
    }

    public Task<List<TruckModel>> GetFiltered()
    {
        throw new NotImplementedException();
    }

    public async Task<TruckResult> SetStatus(string id, string status)
    {
        var exists = await _trucksRepository.GetById(id);
        if (exists is null)
        {
            return new TruckResult("Specified truck was not found", true);
        }
        var existingTruck = exists.ToModel();        

        var (isSet, errMsg) = existingTruck.SetStatus(status);

        if (isSet == false)
        {
            return new TruckResult(errMsg);
        }
        var updated = await _trucksRepository.Update(existingTruck.Id, existingTruck.ToDto());
        if (updated is null)
        {
            return new TruckResult("Could not update specified truck");
        }
        await _trucksRepository.CommitChanges();
        return new TruckResult(updated.ToModel());
    }

    public async Task<TruckResult> Update(TruckModel updatedTruck)
    {
        var exists = await _trucksRepository.GetById(updatedTruck.Id);
        if (exists is null)
        {
            return new TruckResult("Specified truck was not found", true);
        }
        var existingTruck = exists.ToModel();

        if (string.IsNullOrWhiteSpace(updatedTruck.Name) == false)
        {
            existingTruck.Name = updatedTruck.Name;
        }
        if (string.IsNullOrWhiteSpace(updatedTruck.Description) == false)
        {
            existingTruck.Description = updatedTruck.Description;
        }

        var (isSet, errMsg) = existingTruck.SetStatus(updatedTruck.Status);

        if (isSet == false)
        {
            return new TruckResult(errMsg);
        }
        var updated = await _trucksRepository.Update(existingTruck.Id, existingTruck.ToDto());
        if (updated is null)
        {
            return new TruckResult("Could not update specified truck");
        }
        await _trucksRepository.CommitChanges();
        return new TruckResult(updated.ToModel());
    }
}
