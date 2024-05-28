using Model;
using Model.Abstactions;
using Model.Events;

namespace Model.Factories;

public static class FishSpeciesEventFactory
{
    public static FishSpeciesEvent CreateCustomFishSpeciesEvent(long id, EventType type, string description, long fishSpeciesId)
    {

        if (type != EventType.Кастомный)
        {
            throw new ArgumentException("Неправильно указан тип в методе создания кастомного события для рыб!");
        }

        return new FishSpeciesEvent()
        {
            Id = id,
            Type = type,
            Description = description,
            FishSpeciesId = fishSpeciesId
        };
    }
    public static FishSpeciesEvent CreateCustomFishSpeciesEvent(EventType type, string description, long fishSpeciesId)
    {

        if (type != EventType.Кастомный)
        {
            throw new ArgumentException("Неправильно указан тип в методе создания кастомного события для рыб!");
        }

        return new FishSpeciesEvent()
        {
            Type = type,
            Description = description,
            FishSpeciesId = fishSpeciesId
        };
    }
    public static FishSpeciesEvent CreateStandartFishSpeciesEvent(long id, EventType type, long fishSpeciesId)
    {
        return type switch
        {
            EventType.Создание => new FishSpeciesEvent()
            {
                Id = id,
                Type = type,
                Description = $"Создана рыба с ID {fishSpeciesId}",
                FishSpeciesId = fishSpeciesId
            },
            EventType.Редактирование => new FishSpeciesEvent()
            {
                Id = id,
                Type = type,
                Description = $"Отредактирована рыба с ID {fishSpeciesId}",
                FishSpeciesId = fishSpeciesId
            },
            EventType.Кормление => new FishSpeciesEvent()
            {
                Id = id,
                Type = type,
                Description = $"Покормлена рыба с ID {fishSpeciesId}",
                FishSpeciesId = fishSpeciesId
            },
            _ => throw new ArgumentException("Неправильно указан тип при создании FishSpeciesEvent!"),
        };
    }
    public static FishSpeciesEvent CreateStandartFishSpeciesEvent(EventType type, long fishSpeciesId)
    {
        return type switch
        {
            EventType.Создание => new FishSpeciesEvent()
            {
                Type = type,
                Description = $"Создана рыба с ID {fishSpeciesId}",
                FishSpeciesId = fishSpeciesId
            },
            EventType.Редактирование => new FishSpeciesEvent()
            {
                Type = type,
                Description = $"Отредактирована рыба с ID {fishSpeciesId}",
                FishSpeciesId = fishSpeciesId
            },
            EventType.Кормление => new FishSpeciesEvent()
            {
                Type = type,
                Description = $"Покормлена рыба с ID {fishSpeciesId}",
                FishSpeciesId = fishSpeciesId
            },
            _ => throw new ArgumentException("Неправильно указан тип при создании FishSpeciesEvent!"),
        };
    }
}
