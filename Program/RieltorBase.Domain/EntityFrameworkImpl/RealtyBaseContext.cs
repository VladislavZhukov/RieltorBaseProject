namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System;
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
        private readonly VolgaInfoDBEntities context 
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
                RealtyObjectType newType = new RealtyObjectType
                {
                    TypeName = typeDescription.Name
                };

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
                Name = "Фирма \"У Васи\"",
                Address = "Улица Мира 52",
                Phone = "11-22-33fdsf"
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

            Password vasyaPassword = new Password()
            {
                Login = "vasya",
                Password1 = "12345v"
            };

            vasya.Passwords.Add(vasyaPassword);

            Agent petya = this.context.Agents.Add(new Agent()
            {
                Firm = newFirm,
                Name = "Петя",
                IsFirmAdmin = false,
                Addres = "Карла Маркса 25",
                LastName = "Сидоров",
                PhoneNumber = "2222-22"
            });

            Password petyaPassword = new Password()
            {
                Login = "petya",
                Password1 = "12345p"
            };

            petya.Passwords.Add(petyaPassword);

            // квартиры
            RealtyObject vasyasAppartment1 = this.context.RealtyObjects.Add(new RealtyObject()
            {
                RealtyObjectType = this.context.RealtyObjectTypes
                    .First(type => type.TypeName == "Квартиры"),
                Agent = vasya,
                Cost = 1000,
                Date = new DateTime(2016, 5, 6, 0, 0, 0, DateTimeKind.Local).ToUniversalTime(),
                AdditionalInfo = "Хорошая квартира"
            });

            new List<PropertyValue>(new[]
            {
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
            })
            .ForEach(value => vasyasAppartment1.PropertyValues.Add(value));

            RealtyObject vasyasAppartment2 = this.context.RealtyObjects.Add(new RealtyObject()
            {
                RealtyObjectType = this.context.RealtyObjectTypes
                    .FirstOrDefault(type => type.TypeName == "Квартиры"),
                Agent = vasya,
                Cost = 1100,
                Date = new DateTime(2016, 5, 16, 0, 0, 0, DateTimeKind.Local).ToUniversalTime(),
                AdditionalInfo = "Хорошая еще одна квартира"
            });

            new List<PropertyValue>(new[]
            {
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Улица"),
                    StringValue = "Ленина"
                }, 
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Дом"),
                    StringValue = "11"
                }
            })
            .ForEach(value => vasyasAppartment2.PropertyValues.Add(value));

            RealtyObject petyasAppartment1 = this.context.RealtyObjects.Add(new RealtyObject()
            {
                RealtyObjectType = this.context.RealtyObjectTypes
                    .FirstOrDefault(type => type.TypeName == "Квартиры"),
                Agent = petya,
                Cost = 2000,
                Date = new DateTime(2015, 5, 6, 0, 0, 0, DateTimeKind.Local).ToUniversalTime(),
                AdditionalInfo = "Так себе квартира",
                Note = "Кот остается"
            });

            new List<PropertyValue>(new[]
            {
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Улица"),
                    StringValue = "English Street"
                }, 
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Дом"),
                    StringValue = "111"
                }
            })
            .ForEach(value => petyasAppartment1.PropertyValues.Add(value));

            RealtyObject petyasAppartment2 = this.context.RealtyObjects.Add(new RealtyObject()
            {
                RealtyObjectType = this.context.RealtyObjectTypes
                    .FirstOrDefault(type => type.TypeName == "Квартиры"),
                Agent = petya,
                Cost = 3000,
                Date = new DateTime(2017, 5, 6, 0, 0, 0, DateTimeKind.Local).ToUniversalTime(),
                AdditionalInfo = "Уже лучше квартира",
                Note = "Мебель остается, балкон, ж/д"
            });

            new List<PropertyValue>(new[]
            {
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Улица"),
                    StringValue = "Московский проспект"
                }, 
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Дом"),
                    StringValue = "99"
                }
            })
            .ForEach(value => petyasAppartment2.PropertyValues.Add(value));

            RealtyObject petyasAppartment3 = this.context.RealtyObjects.Add(new RealtyObject()
            {
                RealtyObjectType = this.context.RealtyObjectTypes
                    .FirstOrDefault(type => type.TypeName == "Квартиры"),
                Agent = petya,
                Cost = 3500,
                Date = new DateTime(2015, 5, 6, 0, 0, 0, DateTimeKind.Local).ToUniversalTime(),
                AdditionalInfo = "Балкон, ж/д, пласт.окна",
                Note = "Вообще всё норм"
            });

            new List<PropertyValue>(new[]
            {
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Улица"),
                    StringValue = "Московский проспект"
                }, 
                new PropertyValue()
                {
                    PropertyType = this.context.PropertyTypes.First(prType => prType.PropertyName == "Дом"),
                    StringValue = "99"
                }
            })
            .ForEach(value => petyasAppartment3.PropertyValues.Add(value));
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
            this.context.Passwords.RemoveRange(this.context.Passwords);
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
