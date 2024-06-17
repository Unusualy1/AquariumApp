using Model.Enums;

namespace Model.Factories;

public static class EventFactory
{
    #region DecorationEvents

    public static DecorationEvent CreateCustomDecorationEvent(long id, EventType type, string description, long decorationId)
    {
        if (type != EventType.Кастомный)
        {
            throw new ArgumentException("Неправильно указан тип в методе создания кастомного события для рыб!");
        }

        return new DecorationEvent()
        {
            Id = id,
            Type = type,
            Description = description,
            DecorationId = decorationId
        };
    }

    public static DecorationEvent CreateCustomDecorationEvent(EventType type, string description, long decorationId)
    {
        if (type != EventType.Кастомный)
        {
            throw new ArgumentException("Неправильно указан тип в методе создания кастомного события для рыб!");
        }

        return new DecorationEvent()
        {
            Type = type,
            Description = description,
            DecorationId = decorationId
        };
    }

    public static DecorationEvent CreateStandartDecorationEvent(long id, EventType type, long decorationId)
    {
        return type switch
        {
            EventType.Создание => new DecorationEvent()
            {
                Id = id,
                Type = type,
                Description = $"Создана декорация с ID {decorationId}",
                DecorationId = decorationId
            },
            EventType.Редактирование => new DecorationEvent()
            {
                Id = id,
                Type = type,
                Description = $"Отредактирована декорация с ID {decorationId}",
                DecorationId = decorationId
            },
            _ => throw new ArgumentException("Неправильно указан тип при создании DecorationEvent!"),
        };
    }

    public static DecorationEvent CreateStandartDecorationEvent(EventType type, long decorationId)
    {
        return type switch
        {
            EventType.Создание => new DecorationEvent()
            {
                Type = type,
                Description = $"Создана декорация с ID {decorationId}",
                DecorationId = decorationId
            },
            EventType.Редактирование => new DecorationEvent()
            {
                Type = type,
                Description = $"Отредактирована декорация с ID {decorationId}",
                DecorationId = decorationId
            },
            _ => throw new ArgumentException("Неправильно указан тип при создании DecorationEvent!"),
        };
    }

    #endregion

    #region FishEvents

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

    #endregion

    #region FishSpeciesEvents

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
                Description = $"Отредактирован вид рыб с ID {fishSpeciesId}",
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
                Description = $"Создана вид рыбы с ID {fishSpeciesId}",
                FishSpeciesId = fishSpeciesId
            },
            EventType.Редактирование => new FishSpeciesEvent()
            {
                Type = type,
                Description = $"Отредактирован вид рыбы с ID {fishSpeciesId}",
                FishSpeciesId = fishSpeciesId
            },
            _ => throw new ArgumentException("Неправильно указан тип при создании FishSpeciesEvent!"),
        };
    }

    #endregion

    #region HabitatConditionsEvents

    public static HabitatConditionsEvent CreateCustomHabitatConditionsEvent(long id, EventType type, string description, long habitatConditionsId)
    {
        if (type != EventType.Кастомный)
        {
            throw new ArgumentException("Неправильно указан тип в методе создания кастомного события для рыб!");
        }

        return new HabitatConditionsEvent()
        {
            Id = id,
            Type = type,
            Description = description,
            HabitatConditionsId = habitatConditionsId
        };
    }

    public static HabitatConditionsEvent CreateCustomHabitatConditionsEvent(EventType type, string description, long habitatConditionsId)
    {
        if (type != EventType.Кастомный)
        {
            throw new ArgumentException("Неправильно указан тип в методе создания кастомного события для рыб!");
        }

        return new HabitatConditionsEvent()
        {
            Type = type,
            Description = description,
            HabitatConditionsId = habitatConditionsId
        };
    }

    public static HabitatConditionsEvent CreateStandartHabitatConditionsEvent(long id, EventType type, long habitatConditionsId)
    {
        return type switch
        {
            EventType.Создание => new HabitatConditionsEvent()
            {
                Id = id,
                Type = type,
                Description = $"Созданы условия обитания с ID {habitatConditionsId}",
                HabitatConditionsId = habitatConditionsId
            },
            EventType.Редактирование => new HabitatConditionsEvent()
            {
                Id = id,
                Type = type,
                Description = $"Отредактированы условия обитания с ID {habitatConditionsId}",
                HabitatConditionsId = habitatConditionsId
            },
            _ => throw new ArgumentException("Неправильно указан тип при создании HabitatConditionsEvent!"),
        };
    }

    public static HabitatConditionsEvent CreateStandartHabitatConditionsEvent(EventType type, long habitatConditionsId)
    {
        return type switch
        {
            EventType.Создание => new HabitatConditionsEvent()
            {
                Type = type,
                Description = $"Создана декорация с ID {habitatConditionsId}",
                HabitatConditionsId = habitatConditionsId
            },
            EventType.Редактирование => new HabitatConditionsEvent()
            {
                Type = type,
                Description = $"Отредактирована декорация с ID {habitatConditionsId}",
                HabitatConditionsId = habitatConditionsId
            },
            _ => throw new ArgumentException("Неправильно указан тип при создании HabitatConditionsEvent!"),
        };
    }

    #endregion

    #region PlantEvents

    public static PlantEvent CreateCustomPlantEvent(long id, EventType type, string description, long plantId) 
    {
        if (type != EventType.Кастомный)
        {
            throw new ArgumentException("Неправильно указан тип в методе создания кастомного события для рыб!");
        }

        return new PlantEvent()
        {
            Id = id,
            Type = type,
            Description = description,
            PlantId = plantId
        };
    }

    public static PlantEvent CreateCustomPlantEvent(EventType type, string description, long plantId) 
    {
        if (type != EventType.Кастомный)
        {
            throw new ArgumentException("Неправильно указан тип в методе создания кастомного события для рыб!");
        }

        return new PlantEvent()
        {
            Type = type,
            Description = description,
            PlantId = plantId
        };
    }

    public static PlantEvent CreateStandartPlantEvent(long id, EventType type, long plantId)
    {
        return type switch
        {
            EventType.Создание => new PlantEvent()
            {
                Id = id,
                Type = type,
                Description = $"Создано растение с ID {plantId}",
                PlantId = plantId
            },
            EventType.Редактирование => new PlantEvent()
            {
                Id = id,
                Type = type,
                Description = $"Отредактировано растение с ID {plantId}",
                PlantId = plantId
            },
            _ => throw new ArgumentException("Неправильно указан тип при создании PlantEvent!"),
        };
    }

    public static PlantEvent CreateStandartPlantEvent(EventType type, long plantId)
    {
        return type switch
        {
            EventType.Создание => new PlantEvent()
            {
                Type = type,
                Description = $"Создано растение с ID {plantId}",
                PlantId = plantId
            },
            EventType.Редактирование => new PlantEvent()
            {
                Type = type,
                Description = $"Отредактировано растение с ID {plantId}",
                PlantId = plantId
            },
            _ => throw new ArgumentException("Неправильно указан тип при создании PlantEvent!"),
        };
    }

    #endregion

    #region PlantSpeciesEvents

    public static PlantSpeciesEvent CreateCustomPlantSpeciesEvent(long id, EventType type, string description, long plantSpeciesId)
    {

        if (type != EventType.Кастомный)
        {
            throw new ArgumentException("Неправильно указан тип в методе создания кастомного события для вида растений!");
        }

        return new PlantSpeciesEvent()
        {
            Id = id,
            Type = type,
            Description = description,
            PlantSpeciesId = plantSpeciesId
        };
    }

    public static PlantSpeciesEvent CreateCustomPlantSpeciesEvent(EventType type, string description, long plantSpeciesId)
    {

        if (type != EventType.Кастомный)
        {
            throw new ArgumentException("Неправильно указан тип в методе создания кастомного события для вида растений!");
        }

        return new PlantSpeciesEvent()
        {
            Type = type,
            Description = description,
            PlantSpeciesId = plantSpeciesId
        };
    }

    public static PlantSpeciesEvent CreateStandartPlantSpeciesEvent(long id, EventType type, long plantSpeciesId)
    {
        return type switch
        {
            EventType.Создание => new PlantSpeciesEvent()
            {
                Id = id,
                Type = type,
                Description = $"Создан вид растенийа с ID {plantSpeciesId}",
                PlantSpeciesId = plantSpeciesId
            },
            EventType.Редактирование => new PlantSpeciesEvent()
            {
                Id = id,
                Type = type,
                Description = $"Отредактирован вида растенийа с ID {plantSpeciesId}",
                PlantSpeciesId = plantSpeciesId
            },
         
            _ => throw new ArgumentException("Неправильно указан тип при создании PlantSpeciesEvent!"),
        };
    }

    public static PlantSpeciesEvent CreateStandartPlantSpeciesEvent(EventType type, long plantSpeciesId)
    {
        return type switch
        {
            EventType.Создание => new PlantSpeciesEvent()
            {
                Type = type,
                Description = $"Создан вид растений с ID {plantSpeciesId}",
                PlantSpeciesId = plantSpeciesId
            },
            EventType.Редактирование => new PlantSpeciesEvent()
            {
                Type = type,
                Description = $"Отредактирован вид растений с ID {plantSpeciesId}",
                PlantSpeciesId = plantSpeciesId
            },
            _ => throw new ArgumentException("Неправильно указан тип при создании PlantSpeciesEvent!"),
        };
    }

    #endregion
}
