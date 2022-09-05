using System.Collections.Generic;

namespace BDO_Ditto
{
    public static class StaticData
    {
        public static readonly List<uint> SupportedVersions = new List<uint> { 17, 18, 19, 20 }; // 11, 12, 13, and 14 hex

        #region Class id hex values
        public static readonly Dictionary<ulong, string> ClassIdLookup = new Dictionary<ulong, string>
        {
                                                           // Offset starts at 44 hex 8 bytes long
            { 3357742161082951297,      "Corsair"       }, // 2E9918B496B3A681      81 A6 B3 96 B4 18 99 2E
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
            { 13074637124134913639,     "Hashashin"     }, // B572713127324667      67 46 32 27 31 71 72 B5

            // Version 20                                     Offset starts at 4C hex 8 bytes long
            { 7081595336976774109,      "Archer"        }, // 6246E170679033DD      DD 33 90 67 70 E1 46 62
            { 8412734655905554310,      "Corsair"       }, // 74C00998D2DE5F86      86 5F DE D2 98 09 C0 74
            { 9119805490502368545,      "Dark Knight"   }, // 7E900EC65A0CFD21      21 FD 0C 5A C6 0E 90 7E
            { 357830937251315772,       "Drakania"      }, // 04F745577082803C      3C 80 82 70 57 45 F7 04
            { 5290494074644712600,      "Kunoichi"      }, // 496B9C54A581CC98      98 CC 81 A5 54 9C 6B 49
            { 12438890563136803868,     "Lahn"          }, // AC9FD11027BA541C      1C 54 BA 27 10 D1 9F AC
            { 14040961963313825197,     "Maehwa"        }, // C2DB8483701785AD      AD 85 17 70 83 84 DB C2
            { 6802254817898671361,      "Mystic"        }, // 5E6676B78C8A2D01      01 2D 8A 8C B7 76 66 5E
            { 8013978285226004988,      "Nova"          }, // 6F375ECA609711FC      FC 11 97 60 CA 5E 37 6F
            { 9216515557679440964,      "Ranger"        }, // 7FE7A4118A582444      44 24 58 8A 11 A4 E7 7F
            { 617728548737276423,       "Shai"          }, // 08929CD79B1A5A07      07 5A 1A 9B D7 9C 92 08
            { 18271757708760598198,     "Sorceress"     }, // FD9252D404AE46B6      B6 46 AE 04 D4 52 92 FD
            { 13001645364074434640,     "Striker"       }, // B46F1F92AD42D050      50 D0 42 AD 92 1F 6F B4
            { 14205010153774206682,     "Tamer"         }, // C522557936874EDA      DA 4E 87 36 79 55 22 C5
            { 17328365105169856007,     "Witch"         }, // F07AB840E7EC7607      07 76 EC E7 40 B8 7A F0
            { 18331335825172845920,     "Valkyrie"      }  // FE65FCCF35D1D560      60 D5 D1 35 CF FC 65 FE

        };
        #endregion

        public static BdoDataBlock GameVersion     = new BdoDataBlock(4, 12);
        public static BdoDataBlock ClassIdOld      = new BdoDataBlock(68, 8);
        public static BdoDataBlock ClassId         = new BdoDataBlock(0x4C, 8);

        #region Offsets in the file for certain appearance data
        public static readonly Dictionary<string, BdoDataBlock> AppearanceSections = new Dictionary<string, BdoDataBlock>
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
