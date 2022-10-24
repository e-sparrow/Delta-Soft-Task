using System.Collections;
using System.Collections.Generic;

namespace Game.Services.Sound.Interfaces
{
    public interface ISoundSettings
    {
        IEnumerable<SoundSource> Sources
        {
            get;
        }
    }
}