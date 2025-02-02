using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.SpaceArena.Scripts.Infrastructure.Localization.EnLocalizationService;

namespace Assets.SpaceArena.Scripts.Infrastructure.Localization
{
    public class RuLocalizationService : ILocalizationService
    {
        private Dictionary<string, ItemLocalization> _itemLoc = new Dictionary<string, ItemLocalization>
        {
            { "CommonGun", new ItemLocalization("Стандартный лазер","Базовый лазерный модуль для борьбы с мелкими угрозами.") },
            { "UNCOMMONGun", new ItemLocalization("Импульсный излучатель", "Улучшенный лазер с повышенной точностью и скорострельностью.") },
            { "RAREGun", new ItemLocalization("Плазменная пушка", "Мощный плазменный выстрел, пробивающий легкую броню.") },
            { "EPICGun", new ItemLocalization("Квантовый дезинтегратор", "Высокотехнологичное оружие, разрушающее цель на молекулярном уровне.") },
            { "LEGENDARYGun", new ItemLocalization("Гравитационная пушка", "Оружие, искажающее пространство, наносящее огромный урон.") },
            { "MYTHICALGun", new ItemLocalization("Звездный уничтожитель", "Легендарное орудие, способное уничтожить целый корабль одним выстрелом.") },

            { "CommonDrone", new ItemLocalization("Сторожевой дрон", "Простой дрон для поддержки в бою.") },
            { "UNCOMMONDrone", new ItemLocalization("Штурмовой дрон", "Дрон с улучшенной скорострельностью и точностью.") },
            { "RAREDrone", new ItemLocalization("Плазменный дрон", "Дрон, стреляющий плазменными зарядами, наносящими средний урон.") },
            { "EPICDrone", new ItemLocalization("Дрон-призрак", "Невидимый дрон, атакующий врагов с неожиданных направлений.") },
            { "LEGENDARYDrone", new ItemLocalization("Дрон-разрушитель", "Мощный дрон, способный уничтожать врагов залповым огнем.") },
            { "MYTHICALDrone", new ItemLocalization("Дрон-архангел", "Легендарный дрон, наносящий разрушительный урон и восстанавливающий щиты союзников.") },

            { "CommonTouret", new ItemLocalization("Стандартная турель", "Базовая турель для защиты корабля.") },
            { "UNCOMMONTouret", new ItemLocalization("Автоматическая турель", "Турель с улучшенной скорострельностью и точностью") },
            { "RARETouret", new ItemLocalization("Плазменная турель", "Турель, стреляющая плазменными зарядами, наносящими средний урон.") },
            { "EPICTouret", new ItemLocalization("Ионная турель", "Турель, парализующая врагов ионными разрядами.") },
            { "LEGENDARYTouret", new ItemLocalization("Громовержец", "Мощная турель, стреляющая разрядами энергии, уничтожающими врагов.") },
            { "MYTHICALTouret", new ItemLocalization("Судный день", "Легендарная турель, способная уничтожить целый флот за считанные секунды.") },
        };

        private Dictionary<string, string> _uiLoc = new Dictionary<string, string>
        {
            {"Play", "Играть" },
            {"Shop", "Магазин" },
            {"Settings", "Настройки" },
            {"Quit", "Выход" },
            {"Close", "Закрыть" },
            {"Get X2 reward?", "Получить X2 награду?" },
            {"Get X2", "Получить X2" },
            {"Equip", "Экипировать" },
            {"Upgrade", "Улучшить" },
            {"DMG X2", "УРОН X2" },
            {"$ X2", "$ X2" },
            {"Lvl", "Ур" },
            {"BOSS", "БОСС" },
            {"Boss RUSH!", "БОСС РАШ!" },
            {"Go to Arena!", "На Арену!" },
            {"Loading...", "Загрузка..." },
            {"added", "на борту" },
            {"Dead", "Мертв" },
        };

        public string GetItemName(string itemId)
        {
            return _itemLoc[itemId].Name;
        }

        public string GetItemDescription(string itemId)
        {
            return _itemLoc[itemId].Description;
        }

        public string GetUIByKey(string key)
        {
            if (_uiLoc.ContainsKey(key) == false) return "null";

            return _uiLoc[key];
        }
    }
}
