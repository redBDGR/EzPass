﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzPass
{
    static class WordList
    {
        public static Version version = new Version(1, 2);

        private static bool forceUpdateWordList = true;

        public static void InitializeWordlist()
        {
            // Red Version key from registry
            object objVer = RegHandler.ReadKey("Version");
            string verStr = "";

            // Ensure key exists (null key does not exist in registry
            // Create with current app version if it doesn't exist
            if (objVer == null)
            {
                RegHandler.WriteKey("Version", version.ToString());
                verStr = version.ToString();    // Set current version string
            }
            else
            {
                verStr = objVer.ToString();
            }

            Version ver = new Version(verStr);

            // Check if current version number is newer than registry stored version from last execute
            // -1 is newer, 1 is older, 0 is same version
            if (ver.CompareTo(version) == -1)
            {
                // Re-write new version to registry
                RegHandler.WriteKey("Version", version.ToString());

                // Used for emergency re-writes only
                if (forceUpdateWordList)
                {
                    // Override registry key with the current stored wordlist below
                    RegHandler.WriteKey("Wordlist", RegHandler.WordlistHandler.ToRegistryValue(shortList));
                }
            }

            // Extract word list from registry
            shortList = RegHandler.WordlistHandler.FromRegistryValue(RegHandler.ReadKey("Wordlist"));
        }

        static public Dictionary<char, char> replacementChars = new Dictionary<char, char>()
        {
            { 'a', '@' },
            { 'o', '0' },
            //{ 'c', '(' },
            { 'i', '!' },
            { 'b', '8' },
            //{ 't', '7' },
            { 's', '$' },
            { 'e', '3' },
            { 'l', '1' },
        };

        public static Dictionary<char, string> phoneticAlphabet = new Dictionary<char, string>()
        {
            { 'a', "Alpha" },
            { 'b', "Bravo" },
            { 'c', "Charlie" },
            { 'd', "Delta" },
            { 'e', "Echo" },
            { 'f', "Foxtrot" },
            { 'g', "Golf" },
            { 'h', "Hotel" },
            { 'i', "India" },
            { 'j', "Juliett" },
            { 'k', "Kilo" },
            { 'l', "Lima" },
            { 'm', "Mike" },
            { 'n', "November" },
            { 'o', "Oscar" },
            { 'p', "Papa" },
            { 'q', "Quebec" },
            { 'r', "Romeo" },
            { 's', "Sierra" },
            { 't', "Tango" },
            { 'u', "Uniform" },
            { 'v', "Victor" },
            { 'w', "Whiskey" },
            { 'x', "X-Ray" },
            { 'y', "Yankee" },
            { 'z', "Zulu" }
        };

        static public List<string> shortList = new List<string>()
        {
            "acid",
            "acorn",
            "acre",
            "acts",
            "afar",
            "affix",
            "aged",
            "agent",
            "agile",
            "aging",
            "agony",
            "ahead",
            "aide",
            "aim",
            "ajar",
            "alarm",
            "alias",
            "alibi",
            "alien",
            "alike",
            "alive",
            "aloe",
            "aloft",
            "aloha",
            "alone",
            "amend",
            "amino",
            "ample",
            "amuse",
            "angel",
            "anger",
            "angle",
            "ankle",
            "apple",
            "april",
            "apple",
            "apron",
            "aqua",
            "area",
            "arena",
            "argue",
            "arise",
            "armed",
            "armor",
            "army",
            "aroma",
            "array",
            "art",
            "ashen",
            "atlas",
            "atom",
            "attic",
            "audio",
            "avert",
            "avoid",
            "awake",
            "award",
            "awoke",
            "axis",
            "bacon",
            "badge",
            "bagel",
            "baggy",
            "baked",
            "baker",
            "balmy",
            "banjo",
            "barge",
            "barn",
            "bash",
            "basil",
            "bask",
            "batch",
            "bath",
            "baton",
            "bats",
            "blade",
            "blank",
            "blast",
            "blaze",
            "bleak",
            "blend",
            "bless",
            "blimp",
            "blink",
            "bloat",
            "blob",
            "blog",
            "blot",
            "blunt",
            "blurt",
            "blush",
            "boast",
            "boat",
            "body",
            "boil",
            "bok",
            "bolt",
            "boned",
            "boney",
            "bonus",
            "book",
            "booth",
            "boots",
            "boss",
            "botch",
            "both",
            "boxer",
            "breed",
            "bribe",
            "brick",
            "bride",
            "brim",
            "bring",
            "brink",
            "brisk",
            "broad",
            "broil",
            "broke",
            "brook",
            "broom",
            "brush",
            "buck",
            "bud",
            "buggy",
            "bulge",
            "bulk",
            "bully",
            "bunch",
            "bunny",
            "bunt",
            "bush",
            "bust",
            "busy",
            "buzz",
            "cable",
            "cache",
            "cadet",
            "cage",
            "cake",
            "calm",
            "cameo",
            "canal",
            "candy",
            "cane",
            "canon",
            "cape",
            "card",
            "cargo",
            "carol",
            "carry",
            "carve",
            "case",
            "cash",
            "cause",
            "cedar",
            "chain",
            "chair",
            "chant",
            "chaos",
            "charm",
            "chase",
            "cheek",
            "cheer",
            "chef",
            "chess",
            "chest",
            "chew",
            "chief",
            "chili",
            "chill",
            "chip",
            "chomp",
            "chop",
            "chow",
            "chuck",
            "chump",
            "chunk",
            "churn",
            "chute",
            "cider",
            "cinch",
            "city",
            "civic",
            "civil",
            "clad",
            "claim",
            "clamp",
            "clap",
            "clash",
            "clasp",
            "class",
            "claw",
            "clay",
            "clean",
            "clear",
            "cleat",
            "cleft",
            "clerk",
            "click",
            "cling",
            "clink",
            "clip",
            "cloak",
            "clock",
            "clone",
            "cloth",
            "cloud",
            "clump",
            "coach",
            "coast",
            "coat",
            "cod",
            "coil",
            "cola",
            "cold",
            "colt",
            "comic",
            "comma",
            "clone",
            "cope",
            "copy",
            "coral",
            "cork",
            "cost",
            "couch",
            "cough",
            "cover",
            "cozy",
            "craft",
            "cramp",
            "crane",
            "crank",
            "crate",
            "crave",
            "crawl",
            "crazy",
            "creme",
            "crepe",
            "crept",
            "crib",
            "cried",
            "crisp",
            "crook",
            "crop",
            "cross",
            "crowd",
            "crown",
            "crumb",
            "crush",
            "crust",
            "cub",
            "cult",
            "cupid",
            "cure",
            "curl",
            "curry",
            "curse",
            "curve",
            "curvy",
            "cushy",
            "cut",
            "cycle",
            "daily",
            "dairy",
            "daisy",
            "dance",
            "dandy",
            "darn",
            "dart",
            "dash",
            "data",
            "date",
            "dawn",
            "deaf",
            "deal",
            "dean",
            "debit",
            "debt",
            "debug",
            "decaf",
            "decal",
            "decay",
            "deck",
            "decor",
            "decoy",
            "deed",
            "delay",
            "denim",
            "dense",
            "dent",
            "depth",
            "derby",
            "desk",
            "dial",
            "diary",
            "dice",
            "dig",
            "dill",
            "dime",
            "dimly",
            "diner",
            "dingy",
            "disco",
            "dish",
            "disk",
            "ditch",
            "ditzy",
            "dizzy",
            "dock",
            "dodge",
            "doll",
            "dome",
            "donor",
            "donut",
            "dose",
            "dot",
            "dove",
            "down",
            "dowry",
            "doze",
            "drab",
            "drama",
            "drank",
            "draw",
            "dress",
            "dried",
            "drift",
            "drill",
            "drive",
            "drone",
            "droop",
            "drove",
            "drown",
            "drum",
            "dry",
            "duck",
            "duct",
            "dude",
            "dug",
            "duke",
            "duo",
            "dusk",
            "dust",
            "duty",
            "dwarf",
            "dwell",
            "eagle",
            "early",
            "earth",
            "easel",
            "east",
            "eaten",
            "eats",
            "ebay",
            "ebony",
            "ebook",
            "echo",
            "edge",
            "eel",
            "eject",
            "elbow",
            "elder",
            "elf",
            "elk",
            "elm",
            "elope",
            "elude",
            "elves",
            "email",
            "emit",
            "empty",
            "emu",
            "enter",
            "entry",
            "envoy",
            "equal",
            "erase",
            "error",
            "erupt",
            "essay",
            "etch",
            "evade",
            "even",
            "evict",
            "evil",
            "evoke",
            "exact",
            "exit",
            "fable",
            "faced",
            "fact",
            "fade",
            "fall",
            "false",
            "fancy",
            "fang",
            "fax",
            "feast",
            "feed",
            "femur",
            "fence",
            "fend",
            "ferry",
            "fetal",
            "fetch",
            "fever",
            "fiber",
            "fifth",
            "fifty",
            "film",
            "filth",
            "final",
            "finch",
            "fit",
            "five",
            "flag",
            "flaky",
            "flame",
            "flap",
            "flask",
            "fled",
            "flick",
            "fling",
            "flint",
            "flip",
            "flirt",
            "float",
            "flock",
            "flop",
            "floss",
            "flyer",
            "foam",
            "foe",
            "fog",
            "foil",
            "folic",
            "folk",
            "food",
            "fool",
            "found",
            "fox",
            "foyer",
            "frail",
            "frame",
            "fray",
            "fresh",
            "fried",
            "frill",
            "frisk",
            "from",
            "front",
            "frost",
            "froth",
            "frown",
            "froze",
            "fruit",
            "gag",
            "gains",
            "gala",
            "game",
            "gap",
            "gave",
            "gear",
            "gecko",
            "geek",
            "gem",
            "genre",
            "gift",
            "gig",
            "gills",
            "given",
            "giver",
            "glad",
            "glass",
            "glide",
            "gloss",
            "glove",
            "glow",
            "glue",
            "goal",
            "going",
            "golf",
            "gong",
            "good",
            "gooey",
            "goofy",
            "gore",
            "gown",
            "grab",
            "grain",
            "grant",
            "grape",
            "graph",
            "grasp",
            "grass",
            "grave",
            "gravy",
            "gray",
            "green",
            "greet",
            "grew",
            "grid",
            "grief",
            "grill",
            "grip",
            "grit",
            "groom",
            "grope",
            "growl",
            "grub",
            "grunt",
            "guide",
            "gulf",
            "gulp",
            "gummy",
            "guru",
            "gush",
            "gut",
            "guy",
            "habit",
            "half",
            "halo",
            "halt",
            "happy",
            "harm",
            "hash",
            "hasty",
            "hatch",
            "hate",
            "haven",
            "hazel",
            "hazy",
            "heap",
            "heat",
            "heave",
            "hedge",
            "hefty",
            "help",
            "herbs",
            "hers",
            "hub",
            "hug",
            "hula",
            "hull",
            "human",
            "humid",
            "hung",
            "hunk",
            "hunt",
            "hurry",
            "hurt",
            "hush",
            "hut",
            "ice",
            "icing",
            "icon",
            "icy",
            "igloo",
            "image",
            "ion",
            "iron",
            "issue",
            "item",
            "ivory",
            "ivy",
            "jab",
            "jam",
            "jaws",
            "jazz",
            "jeep",
            "jelly",
            "jet",
            "jiffy",
            "job",
            "jog",
            "jolly",
            "jolt",
            "jot",
            "joy",
            "judge",
            "juice",
            "juicy",
            "july",
            "jumbo",
            "jump",
            "junky",
            "juror",
            "jury",
            "keep",
            "keg",
            "kept",
            "kick",
            "kilt",
            "king",
            "kite",
            "kitty",
            "kiwi",
            "knee",
            "knelt",
            "koala",
            "kung",
            "ladle",
            "lady",
            "lair",
            "lake",
            "lance",
            "land",
            "lapel",
            "large",
            "lash",
            "lasso",
            "last",
            "latch",
            "late",
            "lazy",
            "left",
            "legal",
            "lemon",
            "lend",
            "lens",
            "lent",
            "level",
            "lever",
            "lid",
            "life",
            "lift",
            "lilac",
            "lily",
            "limb",
            "limes",
            "line",
            "lint",
            "lion",
            "lip",
            "list",
            "lived",
            "liver",
            "lunar",
            "lunch",
            "lung",
            "lurch",
            "lure",
            "lurk",
            "lying",
            "lyric",
            "mace",
            "maker",
            "malt",
            "mama",
            "mango",
            "manor",
            "many",
            "map",
            "march",
            "mardi",
            "marry",
            "mash",
            "match",
            "mate",
            "math",
            "moan",
            "mocha",
            "moist",
            "mold",
            "mom",
            "moody",
            "mop",
            "morse",
            "most",
            "motor",
            "motto",
            "mount",
            "mouse",
            "mousy",
            "mouth",
            "move",
            "movie",
            "mower",
            "mud",
            "mug",
            "mulch",
            "mule",
            "mull",
            "mumbo",
            "mummy",
            "mural",
            "muse",
            "music",
            "musky",
            "mute",
            "nacho",
            "nag",
            "nail",
            "name",
            "nanny",
            "nap",
            "navy",
            "near",
            "neat",
            "neon",
            "nerd",
            "nest",
            "net",
            "next",
            "ninth",
            "nutty",
            "oak",
            "oasis",
            "oat",
            "ocean",
            "oil",
            "old",
            "olive",
            "omen",
            "onion",
            "only",
            "ooze",
            "opal",
            "open",
            "opera",
            "opt",
            "otter",
            "ouch",
            "ounce",
            "outer",
            "oval",
            "oven",
            "owl",
            "ozone",
            "pace",
            "pagan",
            "pager",
            "palm",
            "panda",
            "panic",
            "pants",
            "paper",
            "park",
            "party",
            "pasta",
            "patch",
            "path",
            "patio",
            "payer",
            "pecan",
            "penny",
            "pep",
            "perch",
            "perky",
            "perm",
            "pest",
            "petal",
            "petri",
            "petty",
            "photo",
            "plank",
            "plant",
            "plaza",
            "plead",
            "plot",
            "plow",
            "pluck",
            "plug",
            "plus",
            "poach",
            "pod",
            "poem",
            "poet",
            "pogo",
            "point",
            "poise",
            "poker",
            "polar",
            "polo",
            "pond",
            "pony",
            "poppy",
            "pork",
            "poser",
            "pouch",
            "pound",
            "pout",
            "power",
            "prank",
            "press",
            "print",
            "prior",
            "prism",
            "prize",
            "probe",
            "prong",
            "proof",
            "props",
            "prude",
            "prune",
            "pry",
            "pug",
            "pull",
            "pulp",
            "pulse",
            "puma",
            "punch",
            "punk",
            "pupil",
            "puppy",
            "purr",
            "purse",
            "push",
            "putt",
            "quack",
            "quake",
            "query",
            "quiet",
            "quill",
            "quilt",
            "quit",
            "quota",
            "quote",
            "rabid",
            "race",
            "rack",
            "radar",
            "radio",
            "raft",
            "rage",
            "raid",
            "rail",
            "rake",
            "rally",
            "ramp",
            "ranch",
            "range",
            "rank",
            "rant",
            "rash",
            "raven",
            "reach",
            "react",
            "ream",
            "rebel",
            "recap",
            "relax",
            "relay",
            "relic",
            "remix",
            "repay",
            "repel",
            "reply",
            "rerun",
            "reset",
            "rhyme",
            "rice",
            "rich",
            "ride",
            "rigid",
            "rigor",
            "rinse",
            "riot",
            "ripen",
            "rise",
            "risk",
            "ritzy",
            "rival",
            "river",
            "roast",
            "robe",
            "robin",
            "rock",
            "rogue",
            "roman",
            "rope",
            "rover",
            "royal",
            "ruby",
            "rug",
            "ruin",
            "rule",
            "runny",
            "rush",
            "rust",
            "rut",
            "sadly",
            "sage",
            "said",
            "saint",
            "salad",
            "salon",
            "salsa",
            "salt",
            "same",
            "sandy",
            "santa",
            "satin",
            "sauna",
            "saved",
            "savor",
            "sax",
            "say",
            "scale",
            "scam",
            "scan",
            "scare",
            "scarf",
            "scary",
            "scoff",
            "scold",
            "scoop",
            "scoot",
            "scope",
            "score",
            "scorn",
            "scout",
            "scowl",
            "scrap",
            "scrub",
            "scuba",
            "scuff",
            "sedan",
            "self",
            "send",
            "sepia",
            "serve",
            "set",
            "seven",
            "shack",
            "shade",
            "shady",
            "shaft",
            "shaky",
            "sham",
            "shape",
            "share",
            "sharp",
            "shed",
            "sheep",
            "sheet",
            "shelf",
            "shell",
            "shine",
            "shiny",
            "ship",
            "shirt",
            "shock",
            "shop",
            "shore",
            "shout",
            "shove",
            "shown",
            "showy",
            "shred",
            "shrug",
            "shun",
            "shush",
            "shut",
            "shy",
            "sift",
            "silk",
            "silky",
            "silly",
            "silo",
            "sip",
            "siren",
            "sixth",
            "size",
            "skate",
            "skew",
            "skid",
            "skier",
            "skies",
            "skip",
            "skirt",
            "skit",
            "sky",
            "slab",
            "slack",
            "slain",
            "slam",
            "slang",
            "slash",
            "slate",
            "slaw",
            "sled",
            "sleek",
            "sleep",
            "sleet",
            "slept",
            "slice",
            "slick",
            "slimy",
            "sling",
            "slip",
            "slit",
            "slob",
            "slot",
            "slug",
            "slum",
            "slurp",
            "slush",
            "small",
            "smash",
            "smell",
            "smile",
            "smirk",
            "smog",
            "snack",
            "snap",
            "snare",
            "snarl",
            "sneak",
            "sneer",
            "sniff",
            "snore",
            "snort",
            "snout",
            "snowy",
            "snub",
            "snuff",
            "speak",
            "speed",
            "spend",
            "spent",
            "spied",
            "spill",
            "spiny",
            "spoil",
            "spoke",
            "spoof",
            "spool",
            "spoon",
            "sport",
            "spot",
            "spout",
            "spray",
            "spree",
            "spur",
            "squad",
            "squat",
            "squid",
            "stack",
            "staff",
            "stage",
            "stain",
            "stall",
            "stamp",
            "stand",
            "stank",
            "stark",
            "start",
            "stash",
            "state",
            "stays",
            "steam",
            "steep",
            "stem",
            "step",
            "stew",
            "stick",
            "sting",
            "stir",
            "stock",
            "stole",
            "stomp",
            "stony",
            "stood",
            "stool",
            "stoop",
            "stop",
            "storm",
            "stout",
            "stove",
            "straw",
            "stray",
            "strut",
            "stuck",
            "stud",
            "stuff",
            "stump",
            "stung",
            "stunt",
            "suds",
            "sugar",
            "sulk",
            "surf",
            "sushi",
            "swab",
            "swan",
            "swarm",
            "sway",
            "swear",
            "sweat",
            "sweep",
            "swell",
            "swept",
            "swim",
            "swing",
            "swipe",
            "swirl",
            "swoop",
            "swore",
            "syrup",
            "tacky",
            "taco",
            "tag",
            "take",
            "tall",
            "talon",
            "tamer",
            "tank",
            "taper",
            "taps",
            "tarot",
            "tart",
            "task",
            "taste",
            "tasty",
            "taunt",
            "thank",
            "thaw",
            "theft",
            "theme",
            "thigh",
            "thing",
            "think",
            "thong",
            "thorn",
            "those",
            "throb",
            "thud",
            "thumb",
            "thump",
            "thus",
            "tiara",
            "tidal",
            "tidy",
            "tiger",
            "tile",
            "tilt",
            "tint",
            "tiny",
            "trace",
            "track",
            "trade",
            "train",
            "trait",
            "trap",
            "trash",
            "tray",
            "treat",
            "tree",
            "trek",
            "trend",
            "trial",
            "tribe",
            "trick",
            "trio",
            "trout",
            "truce",
            "truck",
            "trunk",
            "try",
            "tug",
            "tulip",
            "tummy",
            "turf",
            "tusk",
            "tutor",
            "tutu",
            "tux",
            "tweak",
            "tweet",
            "twice",
            "twine",
            "twins",
            "twirl",
            "twist",
            "uncle",
            "uncut",
            "undo",
            "unify",
            "union",
            "unit",
            "untie",
            "upon",
            "upper",
            "urban",
            "used",
            "user",
            "usher",
            "utter",
            "value",
            "vapor",
            "vegan",
            "venue",
            "verse",
            "vest",
            "veto",
            "vice",
            "video",
            "view",
            "viral",
            "virus",
            "visa",
            "visor",
            "vixen",
            "vocal",
            "voice",
            "void",
            "volt",
            "voter",
            "vowel",
            "wad",
            "wafer",
            "wager",
            "wages",
            "wagon",
            "wake",
            "walk",
            "wand",
            "wasp",
            "watch",
            "water",
            "wavy",
            "wheat",
            "whiff",
            "whole",
            "whoop",
            "wick",
            "widen",
            "widow",
            "width",
            "wife",
            "wifi",
            "wilt",
            "wind",
            "wing",
            "wink",
            "wipe",
            "wired",
            "wiry",
            "wise",
            "wish",
            "wispy",
            "wok",
            "wolf",
            "wool",
            "woozy",
            "word",
            "work",
            "worry",
            "wound",
            "woven",
            "wrath",
            "wreck",
            "wrist",
            "xerox",
            "yahoo",
            "yam",
            "yard",
            "year",
            "yelp",
            "yield",
            "yo-yo",
            "yodel",
            "yoga",
            "yoyo",
            "yummy",
            "zebra",
            "zero",
            "zesty",
            "zippy",
            "zone",
            "zoom",
            "banana",
            "orange",
            "parachute",
            "jumanji",
            "pazuzu",
            "bicycle",
            "animal",
            "annual",
            "appeal",
            "apple",
            "apply",
            "arrival",
            "aside",
            "asset",
            "assist",
            "century",
            "change",
            "chart",
            "chef",
            "chip",
            "circle",
            "index",
            "inform",
            "injury",
            "mixture",
            "modern",
            "morning",
            "mountain",
            "move",
            "nature",
            "nerve",
            "normal",
            "onion",
            "open",
            "palm",
            "part",
            "partly",
            "pet",
            "photo",
            "phrase",
            "pilot",
            "pitch",
            "present",
            "pretty",
            "price",
            "pride",
            "profit",
            "promise",
            "protect",
            "pure",
            "radio",
            "rain",
            "rapid",
            "recall",
            "recruit",
            "reform",
            "seek",
            "sense",
            "serve",
            "similar",
            "signal",
            "soft",
            "tail",
            "minute",
            "discovery",
            "jewel",
            "display",
            "excavate",
            "training",
            "village",
            "favourable",
            "sphere",
            "layout",
            "qualified",
            "gold",
            "blank",
            "narrow",
            "office",
            "soprano",
            "residence",
            "exclusive",
            "desire",
            "affinity",
            "tribute",
            "incredible",
            "theater",
            "minimum",
            "plastic",
            "apparatus",
            "button",
            "scene",
            "comment",
            "complete",
            "atmosphere",
            "install",
            "broccoli",
            "digital",
            "gallery",
            "beginning",
            "recognize",
            "premium",
            "privacy",
            "medieval",
            "mosaic",
            "protect",
            "density",
            "steam",
            "season",
            "extreme",
            "sword",
            "axe",
            "request",
            "home",
            "dinosaur",
            "jurassic",
            "elf",
            "canned",
            "tuna",
            "slice",
            "bread",
            "bard",
            "wizard",
            "warlock",
            "paladin",
            "cat",
            "fruitloop",
            "taccot",
            "charlie",
            "horse",
            "helgas",
            "doom",
            "santa",
            "caramel",
            "coconut",
            "alaska",
            "sundae",
            "herman",
            "zeus",
            "hades",
            "torta",
            "tottenham",
            "athena",
            "poseidon",
            "demeter",
            "ares",
            "artemis",
            "quelaag",
            "fortress",
            "capra",
            "gargoyle",
            "priscilla",
            "seath",
            "fence",
            "skier",
            "peacekeeper",
            "mechanic",
            "jaeger",
            "glukhar",
            "yhorm",
            "shturman",
            "sanitar",
            "lothric",
            "mantis",
            "nemesis",
            "ornstein",
            "smough",
            "vortex",
            "majora",
            "atheon",
            "wraith",
            "ghoul",
            "vampire",
            "daedra",
            "unrelenting",
            "force",
        };
    }
}
