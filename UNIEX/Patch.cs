using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNIEXPatch
{
    enum Dll { SOULSTORM = 0 };
    enum PatchType { Jump = 0xE9, Call = 0xE8, NOP = 0x90, Push = 0x6A };

    struct Offsets
    {
        int _steam;
        int _120nosteam;
    };

    class Patch
    {
        private Dll dll;
        private PatchType type;
        private Offsets offsets;
        private int length, function;
        private byte[] oldCode;
        private bool injected;


    }
}
