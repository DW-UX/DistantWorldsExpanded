using ExpansionMod.Controls;
using ExpansionMod.HotKeyMapping;
using ExpansionMod.Objects;
using ExpansionMod.Objects.HotKeyMapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaconDistantWorlds
{
    public class BaconEntryPoint : IEntryPoint
    {
        public const string _ModKey = "BaconMod";
        private const string _ModRootFolder = "AdvMods\\BaconMod";
        private const string _HotKeyFileName = "BaconModHotKeysMappingFile.json";
        private IHotKeyManager _manager;

        public BaconEntryPoint()
        {
            BaconMain.EntryPointClass = this;
        }
        public IHotKeyManager GetHotKeyManager()
        {
            //подключить менеджер к експаншен моду, допилить поиск и загрузку модов. Допилить инициализацию мейн и старта
            if (_manager == null)
            {
                var keyMapper = new BaconKeyMapper(_HotKeyFileName);
                if (!keyMapper.MapKeys(Path.Combine(AppContext.BaseDirectory, _ModRootFolder)))
                {
                    throw new ApplicationException($"Failed to map keys, check {_HotKeyFileName} file");
                }
                _manager = new HotKeyManagerBacon(keyMapper, _ModRootFolder);
            }
            return _manager;
        }

        public string GetModKey()
        {
            return _ModKey;
        }
    }
}
