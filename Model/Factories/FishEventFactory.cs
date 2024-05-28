using Model.Abstactions;
using Model.Events;

namespace Model.Factories;

public static class FishEventFactory
{
    public static FishEvent CreateCustomFishEvent(long id, EventType type, string description, long fishId)
    {

        if (type != EventType.Кастомный)
        {
            throw new ArgumentException("Неправильно указан тип в методе создания кастомного события для рыб!");
        }

        return new FishEvent()
        {
            Id = id,
            Type = type,
            Description = description,
            FishId = fishId
        };
    }
    public static FishEvent CreateCustomFishEvent(EventType type, string description, long fishId)
    {

        if (type != EventType.Кастомный)
        {
            throw new ArgumentException("Неправильно указан тип в методе создания кастомного события для рыб!");
        }

        return new FishEvent()
        {
            Type = type,
            Description = description,
            FishId = fishId
        };
    }
    public static FishEvent CreateStandartFishEvent(long id, EventType type, long fishId)
    {
        return type switch
        {
            EventType.Создание => new FishEvent()
            {
                Id = id,
                Type = type,
                Description = $"Создана рыба с ID {fishId}",
                FishId = fishId
            },
            EventType.Редактирование => new FishEvent()
            {
                Id = id,
                Type = type,
                Description = $"Отредактирована рыба с ID {fishId}",
                FishId = fishId
            },
            EventType.Кормление => new FishEvent()
            {
                Id = id,
                Type = type,
                Description = $"Покормлена рыба с ID {fishId}",
                FishId = fishId
            },
            _ => throw new ArgumentException("Неправильно указан тип при создании FishEvent!"),
        };
    }
    public static FishEvent CreateStandartFishEvent(EventType type, long fishId)
    {
        return type switch
        {
            EventType.Создание => new FishEvent()
            {
                Type = type,
                Description = $"Создана рыба с ID {fishId}",
                FishId = fishId
            },
            EventType.Редактирование => new FishEvent()
            {
                Type = type,
                Description = $"Отредактирована рыба с ID {fishId}",
                FishId = fishId
            },
            EventType.Кормление => new FishEvent()
            {
                Type = type,
                Description = $"Покормлена рыба с ID {fishId}",
                FishId = fishId
            },
            _ => throw new ArgumentException("Неправильно указан тип при создании FishEvent!"),
        };
    }
}
