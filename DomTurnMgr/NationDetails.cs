﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  class NationDetails
  {
    public string Nation { get; }
    public string Epithet { get; }
    public string Filename { get; }

    NationDetails(string nation, string epithet, string filename)
    {
      Nation = nation;
      Epithet = epithet;
      Filename = filename;
    }

    private static Dictionary<int, NationDetails> lookup;
    public static NationDetails Get(int key)
    {
      if (lookup == null)
      {
        lookup = new Dictionary<int, NationDetails>();

        // Early ages
        lookup[5] = new NationDetails("Arcoscephale", "Golden Era", "early_arcoscephale");
        lookup[6] = new NationDetails("Ermor", "New Faith", "early_ermor");
        lookup[7] = new NationDetails("Ulm", "Enigma of Steel", "early_ulm");
        lookup[8] = new NationDetails("Marverni", "Time of Druids", "early_marverni");
        lookup[9] = new NationDetails("Sauromatia", "Amazon Queens", "early_sauromatia");
        lookup[10] = new NationDetails("T’ien Ch’i", "Spring and Autumn", "early_tienchi");
        lookup[11] = new NationDetails("Machaka", "Lion Kings", "early_machaka");
        lookup[12] = new NationDetails("Mictlan", "Reign of Blood", "early_mictlan");
        lookup[13] = new NationDetails("Abysia", "Children of Flame", "early_abysia");
        lookup[14] = new NationDetails("Caelum", "Eagle Kings", "early_caelum");
        lookup[15] = new NationDetails("C’tis", "Lizard Kings", "early_ctis");
        lookup[16] = new NationDetails("Pangaea", "Age of Revelry", "early_pangaea");
        lookup[17] = new NationDetails("Agartha", "Pale Ones", "early_agartha");
        lookup[18] = new NationDetails("Tir na n'Og", "Land of the Ever Young", "early_tirnanog");
        lookup[19] = new NationDetails("Fomoria", "The Cursed Ones", "early_fomoria");
        lookup[20] = new NationDetails("Vanheim", "Age of Vanir", "early_vanheim");
        lookup[21] = new NationDetails("Helheim", "Dusk and Death", "early_helheim");
        lookup[22] = new NationDetails("Niefelheim", "Sons of Winter", "early_niefelheim");
        lookup[24] = new NationDetails("Rus", "Sons of Heaven", "early_rus");
        lookup[25] = new NationDetails("Kailasa", "Rise of the Ape Kings", "early_kailasa");
        lookup[26] = new NationDetails("Lanka", "Land of Demons", "early_lanka");
        lookup[27] = new NationDetails("Yomi", "Oni Kings", "early_yomi");
        lookup[28] = new NationDetails("Hinnom", "Sons of the Fallen", "early_hinnom");
        lookup[29] = new NationDetails("Ur", "The First City", "early_ur");
        lookup[30] = new NationDetails("Berytos", "Phoenix Empire", "early_berytos");
        lookup[31] = new NationDetails("Xibalba", "Vigil of the Sun", "early_xibalba");
        lookup[32] = new NationDetails("Mekone", "Brazen Giants", "early_mekone");
        lookup[36] = new NationDetails("Atlantis", "Emergence of the Deep Ones", "early_atlantis");
        lookup[37] = new NationDetails("R’lyeh", "Time of Aboleths", "early_rlyeh");
        lookup[38] = new NationDetails("Pelagia", "Pearl Kings", "early_pelagia");
        lookup[39] = new NationDetails("Oceania", "Coming of the Capricorns", "early_oceania");
        lookup[40] = new NationDetails("Therodos", "Telkhine Spectre", "early_therodos");

        // MIddle Ages
        lookup[43] = new NationDetails("Arcoscephale", "The Old Kingdom", "middle_arcoscephale");
        lookup[44] = new NationDetails("Ermor", "Ashen Empire", "middle_ermor");
        lookup[45] = new NationDetails("Sceleria", "Reformed Empire", "middle_sceleria");
        lookup[46] = new NationDetails("Pythium", "Emerald Empire", "middle_pythium");
        lookup[47] = new NationDetails("Man", "Tower of Avalon", "middle_man");
        lookup[48] = new NationDetails("Eriu", "Last of the Tuatha", "middle_eriu");
        lookup[49] = new NationDetails("Ulm", "Forges of Ulm", "middle_ulm");
        lookup[50] = new NationDetails("Marignon", "Fiery Justice", "middle_marignon");
        lookup[51] = new NationDetails("Mictlan", "Reign of the Lawgiver", "middle_mictlan");
        lookup[52] = new NationDetails("T’ien Ch’i", "Imperial Bureaucracy", "middle_tienchi");
        lookup[53] = new NationDetails("Machaka", "Reign of Sorcerors", "middle_machaka");
        lookup[54] = new NationDetails("Agartha", "Golem Cult", "middle_agartha");
        lookup[55] = new NationDetails("Abysia", "Blood and Fire", "middle_abysia");
        lookup[56] = new NationDetails("Caelum", "Reign of the Seraphim", "middle_caelum");
        lookup[57] = new NationDetails("C’tis", "Miasma", "middle_ctis");
        lookup[58] = new NationDetails("Pangaea", "Age of Bronze", "middle_pangaea");
        lookup[59] = new NationDetails("Asphodel", "Carrion Woods", "middle_asphodel");
        lookup[60] = new NationDetails("Vanheim", "Arrival of Man", "middle_vanheim");
        lookup[61] = new NationDetails("Jotunheim", "Iron Woods", "middle_jotunheim");
        lookup[62] = new NationDetails("Vanarus", "Land of the Chuds", "middle_vanarus");
        lookup[63] = new NationDetails("Bandar Log", "Land of the Apes", "middle_bandarlog");
        lookup[64] = new NationDetails("Shinuyama", "Land of the Bakemono", "middle_shinuyama");
        lookup[65] = new NationDetails("Ashdod", "Reign of the Anakim", "middle_ashdod");
        lookup[66] = new NationDetails("Uruk", "City States", "middle_uruk");
        lookup[67] = new NationDetails("Nazca", "Kingdom of the Sun", "middle_nazca");
        lookup[68] = new NationDetails("Xibalba", "Flooded Caves", "middle_xibalba");
        lookup[73] = new NationDetails("Atlantis", "Kings of the Deep", "middle_atlantis");
        lookup[74] = new NationDetails("R’lyeh", "Fallen Star", "middle_rlyeh");
        lookup[75] = new NationDetails("Pelagia", "Triton Kings", "middle_pelagia");
        lookup[76] = new NationDetails("Oceania", "Mermidons", "middle_oceania");
        lookup[77] = new NationDetails("Ys", "Morgen Queens", "middle_ys");
        // Late Ages
        lookup[80] = new NationDetails("Arcoscephale", "Sibylline Guidance", "late_arcoscephale");
        lookup[81] = new NationDetails("Pythium", "Serpent Cult", "late_pythium");
        lookup[82] = new NationDetails("Lemur", "Soul Gate", "late_lemur");
        lookup[83] = new NationDetails("Man", "Towers of Chelms", "late_man");
        lookup[84] = new NationDetails("Ulm", "Black Forest", "late_ulm");
        lookup[85] = new NationDetails("Marignon", "Conquerors of the Sea", "late_marignon");
        lookup[86] = new NationDetails("Mictlan", "Blood and Rain", "late_mictlan");
        lookup[87] = new NationDetails("T’ien Ch’i", "Barbarian Kings", "late_tienchi");
        lookup[89] = new NationDetails("Jomon", "Human Daimyos", "late_jomon");
        lookup[90] = new NationDetails("Agartha", "Ktonian Dead", "late_agartha");
        lookup[91] = new NationDetails("Abysia", "Blood of Humans", "late_abysia");
        lookup[92] = new NationDetails("Caelum", "Return of the Raptors", "late_caelum");
        lookup[93] = new NationDetails("C’tis", "Desert Tombs", "late_ctis");
        lookup[94] = new NationDetails("Pangaea", "New Era", "late_pangaea");
        lookup[95] = new NationDetails("Midgård", "Age of Men", "late_midgård");
        lookup[96] = new NationDetails("Utgård", "Well of Urd", "late_utgard");
        lookup[97] = new NationDetails("Bogarus", "Age of Heroes", "late_bogarus");
        lookup[98] = new NationDetails("Patala", "Reign of the Nagas", "late_patala");
        lookup[99] = new NationDetails("Gath", "Last of the Giants", "late_gath");
        lookup[100] = new NationDetails("Ragha", "Dual Kingdom", "late_ragha");
        lookup[101] = new NationDetails("Xibalba", "Return of the Zotz", "late_xibalba");
        lookup[106] = new NationDetails("Atlantis", "Frozen Sea", "late_atlantis");
        lookup[107] = new NationDetails("R’lyeh", "Dreamlands", "late_rlyeh");
        lookup[108] = new NationDetails("Erytheia", "Kingdom of Two Worlds", "late_erytheia");

      }
      return lookup[key];
    }

  }
}