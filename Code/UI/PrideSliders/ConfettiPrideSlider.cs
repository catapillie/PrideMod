using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celeste.Mod.PrideMod.UI {
    public class ConfettiPrideSlider : PrideSliderBase {
        public ConfettiPrideSlider(string label, Func<PrideTypes, string> values, PrideTypes value)
            : base(label, values, value) {

        }
    }
}
