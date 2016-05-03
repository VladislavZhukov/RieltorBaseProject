namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// EF-реализация контекста базы данных недвижимости.
    /// </summary>
    public class RealtyBaseContext : IRealtyBaseContext
    {
        /// <summary>
        /// EF-контекст.
        /// </summary>
        VolgaInfoDBEntities context 
            = new VolgaInfoDBEntities();

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        /// <summary>
        /// Полная очистка базы данных.
        /// </summary>
        public void ClearDatabase()
        {
            this.DeleteLog();
            this.DeleteMainData();
            this.DeleteMetadata();
        }

        /// <summary>
        /// Создать стандартные метаданные.
        /// </summary>
        public void CreateStandardMetadata()
        {
            foreach (TypeMetadataDescription typeDescription
                in Metadata.GetInstance().TypeDescriptions)
            {
                RealtyObjectType newType =
                    new RealtyObjectType();

                newType.TypeName = typeDescription.Name;

                foreach (string propName
                    in typeDescription.PropertyNames)
                {
                    PropertyType propType =
                        this.FindOrCreatePropertyType(propName);
                    newType.PropertyTypes.Add(propType);
                }

                this.context.RealtyObjectTypes.Add(newType);
            }
        }

        /// <summary>
        /// Создать несколько тестовых объектов в БД.
        /// </summary>
        public void CreateFewObjects()
        {
            // фирмы и агенты
            Firm newFirm = this.context.Firms.Add(new Firm()
            {
                Name = "Фирма \"У Васи\""
            });

            Agent vasya = this.context.Agents.Add(new Agent()
            {
                Firm = newFirm,
                Name = "Вася",
                IsFirmAdmin = true,
                Addres = "Ленина 25",
                LastName = "Пупкин",
                PhoneNumber = "33 - 33-3(0)"
            });

            // квартиры
            RealtyObject vasyasAppartment1 = this.context.RealtyObjects.Add(new RealtyObject()
            {
                RealtyObjectType = this.context.RealtyObjectTypes
                    .First(type => type.TypeName == "Квартиры"),
                Agent = vasya
            });

            new List<PropertyValue>(new[]
            {
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Дата"),
                    StringValue = "11/05/2016"
                }, 
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Улица"),
                    StringValue = "Ленина"
                }, 
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Дом"),
                    StringValue = "11"
                }, 
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Цена тыс.руб."),
                    StringValue = "1100"
                }, 
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Дополнительная информация"),
                    StringValue = "Вся мебель остается"
                }
            })
            .ForEach(value => vasyasAppartment1.PropertyValues.Add(value));

            RealtyObject vasyasAppartment2 = this.context.RealtyObjects.Add(new RealtyObject()
            {
                RealtyObjectType = this.context.RealtyObjectTypes
                    .FirstOrDefault(type => type.TypeName == "Квартиры"),
                Agent = vasya
            });

            new List<PropertyValue>(new[]
            {
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Дата"),
                    StringValue = "11/05/2016"
                }, 
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Улица"),
                    StringValue = "Ленина"
                }, 
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Дом"),
                    StringValue = "11"
                }, 
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Цена тыс.руб."),
                    StringValue = "1100"
                }, 
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Дополнительная информация"),
                    StringValue = "Вся мебель остается"
                }
            })
            .ForEach(value => vasyasAppartment2.PropertyValues.Add(value));
        }

        /// <summary>
        /// Удалить записи журнала.
        /// </summary>
        private void DeleteLog()
        {
            this.context.Actions.RemoveRange(this.context.Actions);
            this.context.Changelogs.RemoveRange(this.context.Changelogs);
            this.context.ChangelogAgents.RemoveRange(this.context.ChangelogAgents);
        }

        /// <summary>
        /// Удалить основные данные: информацию об объектах недвижимости, фирмах и агентах.
        /// </summary>
        private void DeleteMainData()
        {
            this.context.Photos.RemoveRange(this.context.Photos);
            this.context.PropertyValues.RemoveRange(this.context.PropertyValues);
            this.context.RealtyObjects.RemoveRange(this.context.RealtyObjects);
            this.context.Agents.RemoveRange(this.context.Agents);
            this.context.Firms.RemoveRange(this.context.Firms);
        }

        /// <summary>
        /// Удалить информацию о типах и свойствах. 
        /// </summary>
        private void DeleteMetadata()
        {
            this.context.RealtyObjectTypes.RemoveRange(this.context.RealtyObjectTypes);
            this.context.PropertyTypes.RemoveRange(this.context.PropertyTypes);
        }

        /// <summary>
        /// Найти, или добавить новый тип свойства.
        /// </summary>
        /// <param name="propName">Имя свойства.</param>
        private PropertyType FindOrCreatePropertyType(string propName)
        {
            PropertyType existingType =
                this.context.PropertyTypes.Local.FirstOrDefault(
                    prType => prType.PropertyName == propName)
                ?? this.context.PropertyTypes.FirstOrDefault(
                    prType => prType.PropertyName == propName);

            if (existingType != null)
            {
                return existingType;
            }

            PropertyType newPropType = new PropertyType()
            {
                PropertyName = propName,
                PropertyValueType = "String"
            };

            this.context.PropertyTypes.Add(newPropType);
            return newPropType;
        }
    }
}
