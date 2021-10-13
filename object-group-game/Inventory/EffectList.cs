using object_group_game.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    class EffectList
    {
        private static EffectList _obj;
        public List<Effect> Effects { get; set; }
        private EffectList()
        {
            Effects = new List<Effect>();
            try
            {
                using (var context = new DataContext())
                {
                    Effects.AddRange(context.Effect.ToList());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static EffectList GetInstance()
        {
            if (_obj == null)
            {
                _obj = new EffectList();
            }
            return _obj;
        }
    }
}
