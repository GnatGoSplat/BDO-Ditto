using System.Collections.Generic;

namespace BDO_Ditto
{
    public static class StaticData
    {
        public static readonly List<uint> SuportedVersions = new List<uint> { 17, 18, 19 }; // 11, 12 and 13 hex

        #region Class id hex values
        public static readonly Dictionary<ulong, string> ClassIdLookup = new Dictionary<ulong, string>
        {
                                                           // Offset starts at 44 hex 8 bytes long
            { 1251758517271041305,      "Sorceress"     }, // 115F23BD46829519      19 95 82 46 BD 23 5F 11
            { 10777537339687380824,     "Valkyrie"      }, // 9591811BCD932758      58 27 93 CD 1B 81 91 95
            { 17145927421228022900,     "Ranger"        }, // EDF2922586ECFC74      74 FC EC 86 25 92 F2 ED
            { 10764718972524210919,     "Witch"         }, // 9563F6DF2036AAE7      E7 AA 36 20 DF F6 63 95
            { 15499404728391803384,     "Tamer"         }, // D718F27B29CDA1F8      F8 A1 CD 29 7B F2 18 D7
            { 17759858246325470518,     "Wizard"        }, // F677B0FAB1858536      36 85 85 B1 FA B0 77 F6
            { 4956354676860611428,      "Warrior"       }, // 44C88251971F6B64      64 6B 1F 97 51 82 C8 44
            { 9287506164331278002,      "Berserker"     }, // 80E3D9A62E376EB2      B2 6E 37 2E A6 D9 E3 80
            { 7011772489808301336,      "Musa"          }, // 614ED1EDF4E14D18      18 4D E1 F4 ED D1 4E 61
            { 10613727790916565293,     "Maehwa"        }, // 934B89312045F92D      2D F9 45 20 31 89 4B 93
            { 17453010291577773289,     "Kunoichi"      }, // F2358C63E2BADCE9      E9 DC BA E2 63 8C 35 F2
            { 10978699858950456037,     "Ninja"         }, // 985C2D5AA45F82E5      E5 82 5F A4 5A 2D 5C 98
            { 7534873226274538481,      "Dark Knight"   }, // 68913F471FB203F1      F1 03 B2 1F 47 3F 91 68
            { 1286660139353111900,      "Striker"       }, // 11DB229468CBFD5C      5C FD CB 68 94 22 DB 11
            { 13903222370355958151,     "Mystic"        }, // C0F22B155A54D187      87 D1 54 5A 15 2B F2 C0
            { 13298706715706332932,     "Lahn"          }, // B88E7F4C61C71704      04 17 C7 61 4C 7F 8E B8
            { 9027954148991039522,      "Archer"        }, // 22F81F6377BC497D      7D 49 BC 77 63 1F F8 22
            { 1993106510572782253,      "Shai"          }, // 1BA8EFCFBA3582AD      AD 82 35 BA CF EF A8 1B
            { 172777620496739550,       "Guardian"      }, // 0265D45496D86CDE      DE 6C D8 96 54 D4 65 02
            { 12445517493068544328,     "Hashashin"     }  // ACB75C388442B948      48 B9 42 84 38 5C B7 AC
        };
        #endregion

        public static BdoDataBlock GameVersion     = new BdoDataBlock(4, 12);
        public static BdoDataBlock ClassId         = new BdoDataBlock(68, 8);

        #region Offsets in the file for certain apperance data
        public static readonly Dictionary<string, BdoDataBlock> ApperanceSections = new Dictionary<string, BdoDataBlock>
        {
            { "HairAndFace",        new BdoDataBlock(76,   8)      },
            { "HairColors",         new BdoDataBlock(92,   8)      },
            { "Skin",               new BdoDataBlock(100,  8)      },
            { "EyeMakeup",          new BdoDataBlock(108,  24)     },
            { "EyeLine",            new BdoDataBlock(140,  8)      },
            { "Eyes",               new BdoDataBlock(148,  40)     },
            { "FaceShape",          new BdoDataBlock(220,  392)    },
            { "BodyShape",          new BdoDataBlock(604,  96)     },
            { "StandbyExpression",  new BdoDataBlock(884,  8)      },
            { "Voice",              new BdoDataBlock(892,  8)      }
        };
        #endregion
    }
}
