using ExpansionMod.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionMod.Controls
{
    public static class ViewConvert
    {
        public static List<KeyMappingTarget> ConvertKeyMappingTarget(List<ViewKeyMappingTarget> target)
        {
            return target.Select(x => ConvertKeyMappingTargetImpl(x)).ToList();
        }
        public static List<ViewKeyMappingTarget> ConvertKeyMappingTarget(List<KeyMappingTarget> target)
        {
            return target.Select(x => ConvertKeyMappingTargetImpl(x)).ToList();
        }
        private static KeyMappingTarget ConvertKeyMappingTargetImpl(ViewKeyMappingTarget target)
        {
            var res = new KeyMappingTarget()
            {
                FriendlyName = target.FriendlyName,
                TargetMethodId = target.TargetMethodId
            };
            res.MappedHotKeys = CoverntMappedHotKey(target.MappedHotKeys, res);
            return res;
        }

        private static List<MappedHotKey> CoverntMappedHotKey(List<ViewMappedHotKey> mappedHotKeys, KeyMappingTarget parent)
        {
            return mappedHotKeys.Select(x => new MappedHotKey(parent)
            {
                KeyCode = x.Key,
            }).ToList();
        }

        private static ViewKeyMappingTarget ConvertKeyMappingTargetImpl(KeyMappingTarget target)
        {
            return new ViewKeyMappingTarget()
            {
                FriendlyName = target.FriendlyName,
                MappedHotKeys = CoverntMappedHotKey(target.MappedHotKeys),
                TargetMethodId = target.TargetMethodId
            };
        }
        private static List<ViewMappedHotKey> CoverntMappedHotKey(List<MappedHotKey> mappedHotKeys)
        {
            return mappedHotKeys.Select(x => new ViewMappedHotKey()
            {
                Key = x.KeyCode,
            }).ToList();
        }
    }
}
