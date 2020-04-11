//using SpaceResortMurder.ObjectivesX;
using System;
using System.Collections.Generic;
//using SpaceResortMurder.Clues.CloningRoom;
//using SpaceResortMurder.Clues.DockingBay;
//using SpaceResortMurder.Clues.MeleenasSpaceCraft;
//using SpaceResortMurder.Clues.RaymondsSpaceCraft;
//using SpaceResortMurder.Deductions;
//using SpaceResortMurder.Deductions.CauseOfDeath;
//using SpaceResortMurder.Deductions.TheCulpritsMotive;
//using SpaceResortMurder.Deductions.TheMurdererWas;
//using SpaceResortMurder.Deductions.ZaidsResortForBetaTesting;
//using SpaceResortMurder.Dialogues.Meleena;
//using SpaceResortMurder.Dialogues.RaymondsClone;
//using SpaceResortMurder.Dialogues.Travis;
//using SpaceResortMurder.Dialogues.Warren;
//using SpaceResortMurder.Dialogues.Zaid;
//using SpaceResortMurder.DilemmasX;
//using SpaceResortMurder.DilemmasX.CoreDilemmas;
//using SpaceResortMurder.State;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
//using SpaceResortMurder.CharactersX;
//using SpaceResortMurder.Clues.PoliceSpaceCraft;
//using SpaceResortMurder.Deductions.ClonesDesign;
//using SpaceResortMurder.Deductions.LaunchedTheShip;
//using SpaceResortMurder.Deductions.TimeFrameForMurder;
//using SpaceResortMurder.Pathways;
//using MonoDragons.Core.Common;
//using SpaceResortMurder.Clues.VacantRoom;
//using SpaceResortMurder.Deductions.BruisesCameFrom;
//using SpaceResortMurder.Deductions.ElectricalBurnsComeFrom;
//using SpaceResortMurder.Dialogues;

    public static class GameResources
    {
        public const string MainMenuSceneName = "Main Menu";
        public const string OptionsSceneName = "Options";
        public const string CreditsSceneName = "Credits";
        public const string DilemmasSceneName = "Dilemmas";
        public const string DialogueMemoriesScene = "Dialogue Memories";
        public const string EndingSceneName = "Ending";
        public const string SaveLoadSceneName = "SaveLoad";
        public const string PickNameSceneName = "PickName";
        public const string LoadingSceneName = "Loading";

        public const string DefaultPlayerCharacterName = "ZX3-B";

        public const string WhatAreExclusiveChoices = nameof(WhatAreExclusiveChoices);

//        public static void TestAllSymbols()
//        {
//            _scanInfo.Values.ForEach(s => TestSymbols(s));
//            _clues.Values.ForEach(l => l.ForEach(s => TestSymbols(s)));
//            _dilemmaOrDeductionText.Values.ForEach(s => TestSymbols(s));
//            _objectiveTexts.Values.ForEach(s => TestSymbols(s));
//            _dialogues.Values.ForEach(d =>
//            {
//                TestSymbols(d.Opener);
//                d.Elements.ForEach(s => TestSymbols(s.Line));
//            });
//            _pathwayText.Values.ForEach(s => TestSymbols(s));
//        }
//
//        private static void TestSymbols(string stringWithSymbols)
//        {
//            if (stringWithSymbols.Where(c => c == '\\').Count() % 2 == 1)
//                throw new FormatException("A resource uses the reserved character '\\' improperly, you are suppose to use them in pairs with the text evaluation symbol between them");
//            var matches = Regex.Matches(stringWithSymbols, "\\\\([^\\\\]*)\\\\");
//            foreach (Match match in matches)
//                foreach (Capture capture in match.Captures)
//                    // The first capture is the whole value not a value caught by the capture zone
//                    if (!capture.Value.Contains("\\") && !_symbols.ContainsKey(capture.Value))
//                        throw new NotImplementedException("A dilemma or deduction uses unimplemented symbol \"" + capture + "\"");
//        }
//
//        public static string GetScanInfo(string character)
//        {
//            return ReplaceSymbols(_scanInfo[character]);
//        }
//
//        public static string GetCharacterName(string character)
//        {
//            return ReplaceSymbols(_characterNames[character]);
//        }
//
//        public static string GetCharacterImage(string character, Expression expression)
//        {
//            return _characterExpressions[character + " " + expression];
//        }
//
//        public static string[] GetClueLines(string dialogOrClue)
//        {
//            return _clues[dialogOrClue].Select(ReplaceSymbols).ToArray();
//        }
//
//        public static string GetPonderText(string dilemmaOrDeduction)
//        {
//            return ReplaceSymbols(_dilemmaOrDeductionText[dilemmaOrDeduction]);
//        }
//
//        public static string GetObjectiveText(string objective)
//        {
//            return ReplaceSymbols(_objectiveTexts[objective]);
//        }
//
//        public static DialogueSequence GetDialogueSequence(string dialog)
//        {
//            var sequence = _dialogues[dialog];
//            return new DialogueSequence(ReplaceSymbols(sequence.Opener), sequence.Elements.Select(e =>
//                new DialogueElement(e.IsCharacterTalking, e.Expression, ReplaceSymbols(e.Line))).ToArray());
//        }
//
//        public static string GetPathwayText(string pathway)
//        {
//            return ReplaceSymbols(_pathwayText[pathway]);
//        }
//
//        private static string ReplaceSymbols(string text)
//        {
//            return ReplaceSymbols(text.ToCharArray());
//        }
//
//        private static string ReplaceSymbols(char[] text)
//        {
//            var builder = new StringBuilder();
//            for (var i = 0; i < text.Length; i++)
//                if (text[i] == '\\')
//                {
//                    var symbol = new StringBuilder();
//                    for (i++; ; i++)
//                        if (text[i] == '\\')
//                        {
//                            builder.Append(_symbols[symbol.ToString()]());
//                            break;
//                        }
//                        else
//                            symbol.Append(text[i]);
//                }
//                else
//                    builder.Append(text[i]);
//            return builder.ToString();
//        }

//        private static DictionaryWithDefault<string, string> _scanInfo = new DictionaryWithDefault<string, string>("This scan is not implemented")
//        {
//            { nameof(OfficerWarren),
//                "Name: Warren Alexander \n \n" +
//                "Condition: Normal \n \n" +
//                "Augments: None \n \n" +
//                "Criminal Record: Found guilty of petty theft at the age of 12 \n \n" +
//                "Occupation: OPID (Outer Planet Investigative Department), 17 years"
//            },
//            { nameof(HackerMeleena),
//                "Name: Meleena Ka'lick \n \n" +
//                "Condition: Heightened pulse, Poor balance (May have recently exited cyberspace) \n \n" +
//                "Augments: CyberDeck, used to jack into cyberspace \n \n" +
//                "Criminal Record: Clean \n \n" +
//                "Occupation: Corporate Freelancer, 9 years \n \n" +
//                "Red Flags: Extremely limited records, possible alias or record cleaning"
//            },
//            { nameof(ResortManagerZaid),
//                "Name: Zaid Ahuji \n \n" +
//                "Condition: Normal \n \n" +
//                "Augments: None \n \n" +
//                "Criminal Record: Accused, but not convicted, of illegally selling customer data \n \n" +
//                "Occupation: ModeaJet Resort Manager, 2 years"
//            },
//            { nameof(ResearcherTravis),
//                "Name: Travis Falcon \n \n" +
//                "Condition: Recent Xetope use, an expensive legal smart drug that increases a user's focus \n \n" +
//                "Augments: Power Battery Arm, an expensive arm used as a portable power source \n \n" +
//                "Criminal Record: Human Perfect's Research Division is currently being investigated for human experimentation \n \n" +
//                "Occupation: Human Perfect Lead Researcher, 13 years"
//            },
//            { nameof(CEORaymondsClone),
//                "Name: Raymond Soule \n \n" +
//                "Condition: Recent bruising, a burn mark from an electric discharge \n \n" +
//                "Augments: Tracking Explosive Chip, embedded in the skull, both tracks and acts as a means to kill the person remotely \n \n" +
//                "Criminal Record: Currently under investigation for illegal human experimentation at his company Human First \n \n" +
//                "Occupation: CEO of Human Perfect, 21 years"
//            },
//        };
//
//        private static DictionaryWithDefault<string, string> _characterNames = new DictionaryWithDefault<string, string>("Unnamed Character") {
//            { nameof(OfficerWarren), "Warren, Officer" },
//            { nameof(HackerMeleena), "\\Meleena\\" },
//            { nameof(CEORaymondsClone), "Raymond Soule's Clone" },
//            { nameof(ResearcherTravis), "Travis Falcon, Clone Researcher" },
//            { nameof(ResortManagerZaid), "Zaid Ahuja, Resort Manager" },
//        };
//
//        private static DictionaryWithDefault<string, string> _characterExpressions = new DictionaryWithDefault<string, string>("characters/placeholder") {
//            #region Officer Warren
//            { nameof(OfficerWarren) + " " + Expression.Default, "characters/policeman" },
//            { nameof(OfficerWarren) + " " + Expression.Happy, "characters/policeman" },
//            { nameof(OfficerWarren) + " " + Expression.Angry, "characters/policeman" },
//            { nameof(OfficerWarren) + " " + Expression.Thinking, "characters/policeman" },
//            { nameof(OfficerWarren) + " " + Expression.Fear, "characters/policeman" },
//            { nameof(OfficerWarren) + " " + Expression.Sad, "characters/policeman" },
//            #endregion
//
//            #region Meleena
//            { nameof(HackerMeleena) + " " + Expression.Default, "characters/hacker_corporate_spy" },
//            { nameof(HackerMeleena) + " " + Expression.Happy, "characters/hacker_happy" },
//            { nameof(HackerMeleena) + " " + Expression.Angry, "characters/hacker_pissed_off" },
//            { nameof(HackerMeleena) + " " + Expression.Thinking, "characters/hacker_corporate_spy" },
//            { nameof(HackerMeleena) + " " + Expression.Fear, "characters/hacker_pissed_off" },
//            { nameof(HackerMeleena) + " " + Expression.Sad, "characters/hacker_pissed_off" },
//            #endregion
//
//            #region Zaid
//            { nameof(ResortManagerZaid) + " " + Expression.Default, "Characters/resort_manager_colored" },
//            { nameof(ResortManagerZaid) + " " + Expression.Happy, "Characters/resort_manager_colored" },
//            { nameof(ResortManagerZaid) + " " + Expression.Angry, "Characters/resort_manager_colored" },
//            { nameof(ResortManagerZaid) + " " + Expression.Thinking, "Characters/resort_manager_colored" },
//            { nameof(ResortManagerZaid) + " " + Expression.Fear, "Characters/resort_manager_colored" },
//            { nameof(ResortManagerZaid) + " " + Expression.Sad, "Characters/resort_manager_colored" },
//            #endregion
//
//            #region Travis
//            { nameof(ResearcherTravis) + " " + Expression.Default, "characters/scientist_guy" },
//            { nameof(ResearcherTravis) + " " + Expression.Happy, "characters/scientist_guy" },
//            { nameof(ResearcherTravis) + " " + Expression.Angry, "characters/scientist_guy" },
//            { nameof(ResearcherTravis) + " " + Expression.Thinking, "characters/scientist_guy" },
//            { nameof(ResearcherTravis) + " " + Expression.Fear, "characters/scientist_guy" },
//            { nameof(ResearcherTravis) + " " + Expression.Sad, "characters/scientist_guy" },
//            #endregion
//
//            #region Raymond's Clone
//            { nameof(CEORaymondsClone) + " " + Expression.Default, "characters/raymond_alive" },
//            { nameof(CEORaymondsClone) + " " + Expression.Happy, "characters/raymond_alive" },
//            { nameof(CEORaymondsClone) + " " + Expression.Angry, "characters/raymond_sad" },
//            { nameof(CEORaymondsClone) + " " + Expression.Thinking, "characters/raymond_alive" },
//            { nameof(CEORaymondsClone) + " " + Expression.Fear, "characters/raymond_sad" },
//            { nameof(CEORaymondsClone) + " " + Expression.Sad, "characters/raymond_sad" },
//            #endregion
//        };
//
//        private static DictionaryWithDefault<string, string[]> _clues = new DictionaryWithDefault<string, string[]>(new string[] { "This clue has not been implemented" }) {
//            #region Docking Bay
//            { nameof(RaymondsShip), new string[] {
//                "The ship is a Regal Glider, an expensive personal craft, registered to a Raymond Soule.",
//                "There is a T-71 Energy Blaster blast mark on the exterior. It is still fairly hot and must have been fired within the last 2 hours.",
//                "The door control has all its ICE disabled and has been jacked to remain unlocked."
//            } },
//            { nameof(MeleenasShip), new string[] {
//                "The ship is a heavily modded Corbin Cruiser. It's registered to a Meleena Ka'lick.",
//                "Meleena Ka'lick is a corp freelancer. Where does she get the money to purchase her own space craft?",
//            } },
//            { nameof(GarbageAirlock), new string[] {
//                "A garbage airlock that releases trash into space. It shows signs of recent use.",
//            } },
//            { nameof(PoliceCruiserShip), new string[] {
//                "My police cruiser is definitely a huge clue in this case. If only I could figure out what it means.",
//            } },
//            #endregion
//
//            #region Raymond's Ship Interior
//            { nameof(RaymondsCorpse), new string[] {
//                "Raymond died of asphyxiation.",
//                "His body is ballooned up to twice his normal size, his tongue and eyes have boiled. These injuries match the profile of being in space without a suit.",
//                "There is also bruising present around his neck from someone trying to choke him with their arm prior to his exposure to space.",
//                "He has 13 recent wide-needle punctures.",
//            } },
//            { nameof(ShipsLogs), new string[] {
//                "Ship's logs since after arriving this morning:",
//                "The ship launched at 7:05 PM \n" +
//                "The space hatch was opened at 7:10 PM \n" +
//                "The space hatch closed at 7:20 PM \n" +
//                "The ship landed at 7:25 PM",
//            } },
//            { nameof(RaymondsPad), new string[] {
//                "Raymond's personal pad",
//                "There is a list of resorts on here with ModeaJet Grand Resort being one of them, but it is among the crossed off ones.",
//                "This pad was used at 7:50 PM to send a message aproving ModeaJet Grand Resort to be the beta-tester for a new luxury clone.",
//            } },
//            #endregion
//
//            #region Meleena's Ship Interior
//            { nameof(EncryptedDataDrive), new string[] {
//                "I detect this data drive was used recently, but it's encrypted.",
//            } },
//            { nameof(UnencryptedDataDrive), new string[] {
//                "The data drive contains Raymond's files about a recent cloning experiment gone wrong.",
//                "The experiment was supposed to make perfect clones. It used needles to extract key matter for replication.",
//                "The experiment turned deadly when all the clones tried to kill their look-a-likes. It was a massacre. Bernard Falcon, Travis's brother, was overseeing the project and paid for it with his life.",
//                "Raymond Soule covered up the massacre by staging a terrorist attack that supposedly killed the people.",
//            } },
//            { nameof(SkeletonKey), new string[] {
//                "This is known as a skeleton key device. These special skeleton keys are used to disable ICE on most doors in a matter of nanoseconds.",
//            } },
//            { nameof(HackingRig), new string[] {
//                "This computer was recently used with a cyberdeck, using the alias \"Data Raven\".",
//                "\"Data Raven\" is an infamous decker that is responsible for numerous leaks of corp data exposing corruption.",
//            } },
//            #endregion
//
//            #region Travis's Cloning Room
//            { nameof(CloningChamber), new string[] {
//                "This machine is similiar to a known cloning device, but it seems to have a lot of strange modifications to it."
//            } },
//            #endregion
//
//            #region Police Cruiser
//            { nameof(Clock), new string[] {
//                "The time reads 8:02 PM"
//            } },
//            #endregion
//
//            #region Vacant Room
//            { nameof(T71EnergyBlaster), new string[] {
//                "A T-71 Energy Blaster. It is registered to Raymond Soule.",
//                "It possesses a security feature that ensures the wielder's DNA matches the owner before firing.",
//            } },
//            #endregion
//        };
//
//        private static DictionaryWithDefault<string, DialogueSequence> _dialogues = new DictionaryWithDefault<string, DialogueSequence>(
//            new DialogueSequence("This dialogue is not implemented", new DialogueElement(true, "This dialogue is not implemented"))) {
//            #region Warren
//            { nameof(WarrenIntroduction), new DialogueSequence(
//                "Who are you?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Thinking, "Who are you?"),
//                    new DialogueElement(true, Expression.Default, "Hello there, \\Player\\. This is your first time powering up, so I guess I better get you up to speed."),
//                    new DialogueElement(true, Expression.Default, "I'm Officer Warren, and YOU are a bioroid detective."),
//                    new DialogueElement(false, Expression.Thinking, "..."),
//                    new DialogueElement(true, Expression.Default, "Lookie here, you got the schnazy ability to scan people and spot those little details I can't."),
//                    new DialogueElement(true, Expression.Happy, "Go ahead and try it out on me."),
//            } ) },
//            { nameof(PettyTheftAt12), new DialogueSequence(
//                "You've committed petty theft at the age of 12.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "You've committed petty theft at the age of 12."),
//                    new DialogueElement(true, Expression.Happy, "What did I tell you? Isn't scanning pretty fancy? Alright, now to get to what's at hand."),
//                    new DialogueElement(true, Expression.Default, "Right before powering you up, I got pinged by the manager at ModeaJet Grand Resort about a corp exec murder."),
//                    new DialogueElement(true, Expression.Default, "The victim is Raymond Soule, the CEO of the cloning company Human Perfect."),
//                    new DialogueElement(true, Expression.Default, "Mr. Soule arrived intact at ModeaJet Grand Resort at around 7:00 AM this morning but was found dead this evening in his ship."),
//                    new DialogueElement(true, Expression.Happy, "Go look around the ship for clues, but don't go touching everything you see."),
//                    new DialogueElement(true, Expression.Happy, "Can't expect any clues to come searching for you..."),
//            } ) },
//            { nameof(AnytimeUpTilNow), new DialogueSequence(
//                "The murder took place anytime up until now.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "The murder took place anytime up until now."),
//                    new DialogueElement(true, Expression.Default, "You don't have that quite right. No worries though... I said that he arrived at the resort at 7 AM intact."),
//                }
//            ) },
//            { nameof(BetweenSevenAMToEightPM), new DialogueSequence(
//                "The murder must have taken place between 7 AM and 8 PM.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "The murder must have taken place between 7 AM and 8 PM."),
//                    new DialogueElement(true, Expression.Happy, "Exactly! Looks like we're ready to get started. Let's start investigating Raymond's craft."),
//            } ) },
//            { nameof(WeHaveUntilMidnight), new DialogueSequence(
//                "We have until midnight, so it's best if we hurry.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Happy, "We have until midnight, so it's best if we hurry."),
//                    new DialogueElement(true, Expression.Angry, "Man, that's not even close! Powering up the first time must be a hell of a ride."),
//            } ) },
//            { nameof(DetainedMeleena), new DialogueSequence(
//                "Where did she come from?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Where did she come from?"),
//                    new DialogueElement(true, Expression.Default, "She left that modded craft, and she is pretty close to the crime scene. I have detained her to stay with me, so you can question her."),
//            } ) },
//            { nameof(NeedASearchOrder), new DialogueSequence(
//                "I need a search order for Meleena's craft.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "I need a search order for Meleena's craft."),
//                    new DialogueElement(true, Expression.Happy, "I'll submit a request for Ms. Ka'lick's craft. Thankfully this is a high profile corp CEO case, so I'm sure we will get a reply soon."),
//                    new DialogueElement(true, Expression.Default, "In the meantime, you should make sure to get the statement from the hotel manager."),
//            } ) },
//            { nameof(IsTheSearchOrderReady), new DialogueSequence(
//                "Do we have the search order now?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Do we have the search order now?"),
//                    new DialogueElement(true, Expression.Happy, "Got it right here. I'm sure the case will be cracked wide open when we get in there."),
//                    new DialogueElement(false, Expression.Thinking, "That's very improbable."),
//                } ) },
//            #endregion
//
//            #region Meleena
//            { nameof(WhoAreYou), new DialogueSequence(
//                "Who are you?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Who are you?"),
//                    new DialogueElement(true, Expression.Angry, "It's fraggin' rude not to introduce yourself first, Bioroid!"),
//                    new DialogueElement(false, Expression.Default, "My name is \\Player\\."),
//                    new DialogueElement(true, Expression.Default, "Alright roid, I'm Meleena Ka'lick, a corp freelancer on vacation, satisfied?"),
//                    new DialogueElement(false, Expression.Default, "Yes."),
//            } ) },
//            { nameof(MeleenasAccount), new DialogueSequence(
//                "Give me your account from this morning until night.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Give me your account from this morning until night."),
//                    new DialogueElement(true, Expression.Default, "Did you leave your manners in the recharger, roid?"),
//                    new DialogueElement(false, Expression.Default, "No, but I don't think it's necessary to utilize them with you."),
//                    new DialogueElement(true, Expression.Angry, "Fraggin' roids. Alright since awaking in meat-space, I have been relaxing in my room and craft all day."),
//                    new DialogueElement(true, Expression.Default, "And before you ask, I have seen nothing out of the ordinary 'cept you."),
//            } ) },
//            { nameof(CorporateFreelancersCantNormallyAffordPersonalSpaceCrafts), new DialogueSequence(
//                "Corporate freelancers can't normally purchase their own craft.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Thinking, "Corporate freelancers can't normally purchase their own craft."),
//                    new DialogueElement(true, Expression.Happy, "Beats your pig-rig over there."),
//                    new DialogueElement(true, Expression.Default, "I'm a data specialist, tasked with gathering invaluable paydata. You'd be surprised how well those corp execs pay for my info."),
//            } ) },
//            { nameof(SearchYourCraftForEvidence), new DialogueSequence(
//                "Your craft is at the crime scene. I'm going to have to search it for evidence.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Your craft is at the crime scene. I'm going to have to search it for evidence."),
//                    new DialogueElement(true, Expression.Fear, "Drek! Gotta search order for that?"),
//            } ) },
//            { nameof(YouHaveARatherCleanRecord), new DialogueSequence(
//                "Your record is squeaky clean and rather small.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Thinking, "Your record is squeaky clean and rather small."),
//                    new DialogueElement(true, Expression.Happy, "Goody-two-shoes Meleena, that's me."),
//            } ) },
//            { nameof(ProveIt), new DialogueSequence(
//                "You are the closest. You must have hacked the door on Raymond's craft.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Thinking, "You are the closest. You must have hacked the door on Raymond's craft."),
//                    new DialogueElement(true, Expression.Angry, "Just 'cause I'm a data specialist doesn't mean I'm a decker."),
//            } ) },
//            { nameof(HereIsTheSearchOrder), new DialogueSequence(
//                "Here is the search order for your ship.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Here is the search order for your ship."),
//                    new DialogueElement(true, Expression.Angry, "This is jacked! The OPID don't give a frag about privacy."),
//            } ) },
//            { nameof(YouAreAHacker), new DialogueSequence(
//                "\"Data Raven\" you are rather infamous. What are you really doing here?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "\"Data Raven\" you are rather infamous. What are you really doing here?"),
//                    new DialogueElement(true, Expression.Fear, "Drek!"),
//                    new DialogueElement(true, Expression.Fear, "The chip-truth is I got word that Raymond Soule was visiting this small-time resort. CEO's don't take time off to small-time resorts."),
//                    new DialogueElement(true, Expression.Default, "I knew there was gonna be easy paydata for someone who has their own craft. I didn't De-rezz Raymond."),
//                    new DialogueElement(true, Expression.Default, "\\Player\\, I only use my decking to fight corp corruption, and don't you think it would be useful if you had a decker in your pocket for future investigations? Please don't include \"Data Raven\" in your report."),
//            } ) },
//            { nameof(IWontReportDataRaven), new DialogueSequence(
//                "Give me the encryption key and I won't report \"Data Raven\".",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Give me the encryption key and I won't report \"Data Raven\"."),
//                    new DialogueElement(true, Expression.Happy, "You won't regret this, roid! Anything for my new chummer. It's got some valuable paydata."),
//            } ) },
//            { nameof(DeckersMakeTheWorldWorse), new DialogueSequence(
//                "\"Data Raven\" needs to answer for her crimes.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Angry, "\"Data Raven\" needs to answer for her crimes."),
//                    new DialogueElement(true, Expression.Fear, "Drek!"),
//                    new DialogueElement(false, Expression.Angry, "If you don't want your charges to get even worse, you will give me the encryption key for your data drive."),
//                    new DialogueElement(true, Expression.Angry, "Fraggin' bioroid badges are incapable of being human! You can have the data drive! It's not even fraggin' useful to your case."),
//            } ) },
//            { nameof(YouBrokeIntoRaymondsShip), new DialogueSequence(
//                "You broke into Raymond's craft with your skeleton key.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "You broke into Raymond's craft with your skeleton key."),
//                    new DialogueElement(true, Expression.Fear, "Drek! Listen, roid, I'll give you the chip-truth."),
//                    new DialogueElement(true, Expression.Sad, "I fragged his ICE to find paydata in his craft. I was going to run, but I felt a blast on the side of his craft."),
//                    new DialogueElement(true, Expression.Default, "I hid myself and waited. Right when my patience was wearin' thin, Raymond comes in muttering to himself, launches the ship, opens the hatch and pops out."),
//                    new DialogueElement(true, Expression.Default, "I heard a loud thud, which in retrospect must have come from Raymond's fried body being dumped on the ground."),
//                    new DialogueElement(true, Expression.Default, "The meat in the ship wasn't muttering, so I don't know who it was that landed the ship, got out, and left."),
//                    new DialogueElement(true, Expression.Default, "As soon as he left, I floored it outta there."),
//            } ) },
//            { nameof(MeleenaYouShotTheShip), new DialogueSequence(
//                "You hacked Raymond's T-71 energy blaster and shot his craft.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "You hacked Raymond's T-71 energy blaster and shot his craft."),
//                    new DialogueElement(true, Expression.Angry, "What?! Why the frag would I do that? I like to think of myself as sneaky."),
//            } ) },
//            { nameof(YouDidntHearAVoiceYouLaunchedTheShip), new DialogueSequence(
//                "You lied about someone else entering Raymond's ship when you were there.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Angry, "You lied about someone else entering Raymond's ship when you were there."),
//                    new DialogueElement(false, Expression.Thinking, "You were the one who launched the ship."),
//                    new DialogueElement(true, Expression.Angry, "Drek! Your not pinning this on me, roid! I woulda zeroed the ship's logs."),
//            } ) },
//            #endregion
//
//            #region Zaid
//            { nameof(WhoAreYouZaid), new DialogueSequence(
//                "You manage this resort?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "You manage this resort?"),
//                    new DialogueElement(true, Expression.Default, "I'm Zaid Ahuji, manager of this grand resort. I assume you will be wanting a room while you stay here and investigate the er... incident."),
//                    new DialogueElement(true, Expression.Happy, "It comes with a state-of-the-art recharger, for one such as yourself."),
//                    new DialogueElement(false, Expression.Default, "I believe I can close this case before the night is over."),
//                    new DialogueElement(true, Expression.Default, "Of course! Given someone of your er... skill, I'm sure. Though, if it stumps you for the night, don't hesitate to ask for a room."),
//            } ) },
//            { nameof(ZaidsAccount), new DialogueSequence(
//                "What can you recall up until you pinged us?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "What can you recall up until you pinged us?"),
//                    new DialogueElement(true, Expression.Default, "Right away! I was staying here in the lobby er... when I saw a notification that Raymond's ship was leaving."),
//                    new DialogueElement(true, Expression.Default, "I didn't find that odd but 20 minutes later Raymond's ship returned."),
//                    new DialogueElement(true, Expression.Default, "I headed over to the docking bay. That's where I saw er... Raymond's ship was open."),
//                    new DialogueElement(true, Expression.Fear, "Upon coming over, I saw Raymond's er... figure... and then immediately called you."),
//            } ) },
//            { nameof(WhoIsStayingAtYourResort), new DialogueSequence(
//                "Tell me who is currently staying at your resort.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Tell me who is currently staying at your resort."),
//                    new DialogueElement(true, Expression.Default, "Right away! Meleena Ka'lick, who arrived in her own ship. Travis Falcon, a colleague of Raymond Soule. And the now deceased Raymond Soule."),
//            } ) },
//            { nameof(WhySoFewPeopleAtTheResort), new DialogueSequence(
//                "Your resort seems to be lacking in occupants?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Your resort seems to be lacking in occupants?"),
//                    new DialogueElement(true, Expression.Sad, "Ever since the reports of me allegedly selling customer data, it's been hard to rebuild my reputation."),
//            } ) },
//            { nameof(WhyWasRaymondHere), new DialogueSequence(
//                "Why was Raymond Soule visiting your resort?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Why was Raymond Soule visiting your resort?"),
//                    new DialogueElement(true, Expression.Happy, "Raymond was going to let me test a new line of luxury clones, which would greatly increase popularity."),
//            } ) },
//            { nameof(DoYouHaveAnyCamerasAtYourResort), new DialogueSequence(
//                "Do you have any surveillance that would be useful to my investigation?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Do you have any surveillance that would be useful to my investigation?"),
//                    new DialogueElement(true, Expression.Sad, "Well, after reports of me allegedly selling customer data, I had to um... remove all my cameras to put customers at ease."),
//            } ) },
//            { nameof(DidYouReleaseTheGarbage), new DialogueSequence(
//                "I noticed that the garbage airlock was recently used. Was that you?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Thinking, "I noticed that the garbage airlock was recently used. Was that you?"),
//                    new DialogueElement(true, Expression.Thinking, "No, I thought it was um... strange the garbage airlock was empty."),
//            } ) },
//            { nameof(DidRaymondApproveYourResort), new DialogueSequence(
//                "Did Raymond decide to let your resort test the luxury clones?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Did Raymond decide to let your resort test the luxury clones?"),
//                    new DialogueElement(true, Expression.Happy, "Yes. He said he quite liked the place."),
//            } ) },
//            { nameof(WillYourAcceptanceBePutIntoQuestion), new DialogueSequence(
//                "Now that Raymond is dead, will your acceptance for the luxury clones be revoked?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Now that Raymond is dead, will your acceptance for the luxury clones be revoked?"),
//                    new DialogueElement(true, Expression.Happy, "No, I'm quite confident they will still follow his decision."),
//            } ) },
//            { nameof(YouWereNotAcceptedForBetaTesting), new DialogueSequence(
//                "ModeaJet Grand Resort was not accepted for the luxury clone beta testing.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Angry, "ModeaJet Grand Resort was not accepted for the luxury clone beta testing."),
//                    new DialogueElement(false, Expression.Angry, "I found a list of resorts and yours was crossed off."),
//                    new DialogueElement(true, Expression.Fear, "But, what about the message Raymond sent?!"),
//                    new DialogueElement(false, Expression.Thinking, "How would you know about that, as it was sent at 7:50 PM?"),
//                    new DialogueElement(true, Expression.Fear, "..."),
//                    new DialogueElement(false, Expression.Default, "You should tell me what really happened."),
//                    new DialogueElement(true, Expression.Fear, "Alright... when I found Raymond's body, I er... sent the message on his pad."),
//                    new DialogueElement(true, Expression.Sad, "I was wrongly accused of illegally selling customer's data. It wasn't fair that my resort went from thriving to this. I'm in debt with the wrong people. I needed to pay ModeaJet."),
//                    new DialogueElement(true, Expression.Fear, "Please don't tell anyone that Raymond didn't send that message or I'll be doomed."),
//            } ) },
//            { nameof(IWontSealYourFate), new DialogueSequence(
//                "I won't be the one to seal your fate. It will stay off my report.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "I won't be the one to seal your fate. It will stay off my report."),
//                    new DialogueElement(true, Expression.Happy, "Thank you so much! God bless you!"),
//            } ) },
//            { nameof(YouBroughtThisOnYourself), new DialogueSequence(
//                "Your own poor decisions lead you to these consequences.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Your own poor decisions lead you to these consequences."),
//                    new DialogueElement(true, Expression.Sad, "No... no... what am I going to do?"),
//            } ) },
//            { nameof(ZaidYouHackedRaymondsDoor), new DialogueSequence(
//                "When you went to check out Raymond's ship, the door was still closed and you hacked it.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Angry, "When you went to check out Raymond's ship, the door was still closed and you hacked it."),
//                    new DialogueElement(true, Expression.Fear, "I didn't... I swear! I wouldn't be running a resort if I was a decker."),
//            } ) },
//            { nameof(ZaidLaunchedTheShipToGetHisPad), new DialogueSequence(
//                "You didn't just find his body. You launched his ship to retrieve his pad.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "You didn't just find his body. You launched his ship to retrieve his pad."),
//                    new DialogueElement(true, Expression.Default, "Sorry, Sir, I only came to check on the ship after it had landed."),
//            } ) },
//            #endregion
//
//            #region Travis
//            { nameof(DidYouWorkWithRaymond), new DialogueSequence(
//                "What is your relationship with Raymond Soule?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "What is your relationship with Raymond Soule?"),
//                    new DialogueElement(true, Expression.Thinking, "You are a brand new detective bioroid. I hear when your kind is new that you are more prone to error. It's caused by a lack of synaptic connections which are what allow humans to connect pieces of data together."),
//                    new DialogueElement(false, Expression.Default, "Are you claiming I have a wrong assumption?"),
//                    new DialogueElement(true, Expression.Thinking, "... No, just an observation. Given your prescence it is probable there was a death."),
//                    new DialogueElement(false, Expression.Default, "..."),
//                    new DialogueElement(true, Expression.Default, "My relationship with Raymond Soule is that of employer and employee."),
//            } ) },
//            { nameof(WhyIsTravisAtTheResort), new DialogueSequence(
//                "Why did Raymond Soule bring you to this resort?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Why did Raymond Soule bring you to this resort?"),
//                    new DialogueElement(true, Expression.Thinking, "..."),
//                    new DialogueElement(true, Expression.Default, "I was brought by Raymond for the purpose of secretly forming a clone of him. This resort was chosen as it has no surveillance."),
//            } ) },
//            { nameof(ExplainTheCloningMachine), new DialogueSequence(
//                "This is not a normal cloning device.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "This is not a normal cloning device."),
//                    new DialogueElement(true, Expression.Happy, "Of course! It wouldn't be located in any public database that you have access to."),
//                    new DialogueElement(true, Expression.Happy, "This is an experimental cloning device capable of making more accurate clones than ever before."),
//                    new DialogueElement(true, Expression.Default, "It uses 13 needles on the target to extract different kinds of material to synthesize."),
//                    new DialogueElement(true, Expression.Default, "Then we map out a target's brain, while the clone's body is being formed. The last step is forming the new brain and sealing up the clone."),
//                    new DialogueElement(true, Expression.Fear, "Now, I know it's illegal to use unapproved cloning devices on humans... but Raymond demanded it or I would be fired and my reputation dragged through the mud. I had no choice!"),
//                    new DialogueElement(true, Expression.Sad, "Will you spare me from the judgment of Raymond's wrong-doing?"),
//            } ) },
//            { nameof(WontTurnYouInForRaymondsAction), new DialogueSequence(
//                "You shouldn't pay for Raymond's crimes.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "You shouldn't pay for Raymond's crimes."),
//                    new DialogueElement(true, Expression.Happy, "I'm glad you you are a just bioroid."),
//                } ) },
//            { nameof(YouAreStillResposible), new DialogueSequence(
//                "You are still responsible for agreeing to do it.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Angry, "You are still responsible for agreeing to do it."),
//                    new DialogueElement(true, Expression.Thinking, "... how unfortunate."),
//                } ) },
//            { nameof(TravissAccount), new DialogueSequence(
//                "Give me your full account of today's events.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Give me your full account of today's events."),
//                    new DialogueElement(true, Expression.Thinking, "Constructing your timeline, Bioroid? Alright, I can assist you."),
//                    new DialogueElement(true, Expression.Default, "Raymond's craft arrived at 7 AM, I went directly to this room and worked 'til 4:30 setting up the cloning device."),
//                    new DialogueElement(true, Expression.Default, "Raymond Soule joined me to begin the cloning process at 5 PM. The process was finished at 6:30. Raymond and his clone then left for the night."),
//                    new DialogueElement(true, Expression.Default, "I have remained here, coordinating my research team, up until now."),
//            } ) },
//            { nameof(WhereIsYourClone), new DialogueSequence(
//                "Where is Raymond's clone?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Where is Raymond's clone?"),
//                    new DialogueElement(true, Expression.Default, "Let's have a look... I had a tracking device embedded in him upon creation."),
//                    new DialogueElement(true, Expression.Happy, "Interesting... he is chilling in Raymond's room."), 
//            } ) },
//            { nameof(CloningMalfunction), new DialogueSequence(
//                "Raymond's clone claims there was an electric discharge when cloning?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Raymond's clone claims there was an electric discharge when cloning?"),
//                    new DialogueElement(true, Expression.Thinking, "... Yes, there was an electrical discharge that hit the clone."),
//            } ) },
//            { nameof(PowerBatteryArm), new DialogueSequence(
//                "What do you use your Power Battery Arm for?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "What do you use your Power Battery Arm for?"),
//                    new DialogueElement(true, Expression.Default, "... I use it as a portable power source. In this paticular instance, I used some of the energy from it to power the cloning device."),
//            } ) },
//            { nameof(MatterRemovalBruises), new DialogueSequence(
//                "Raymond's clone claims that the needles removing matter causes bruises?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Thinking, "Raymond's clone claims that the needles removing matter causes bruises?"),
//                    new DialogueElement(true, Expression.Thinking, "Interesting... That's very suspicious."),
//                    new DialogueElement(true, Expression.Default, "Raymond's clone is lying. The process does not leave a mark on it's user. If he has bruises, they came from somewhere else."),
//            } ) },
//            { nameof(YourBrotherWasKilled), new DialogueSequence(
//                "Your brother died in a cloning experiment, and Raymond covered it up.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Your brother died in a cloning experiment, and Raymond covered it up."),
//                    new DialogueElement(true, Expression.Thinking, "... So you think this would be my motive for murder?"),
//                    new DialogueElement(false, Expression.Default, "It's a strong motive."),
//                    new DialogueElement(true, Expression.Angry, "Well, since I didn't commit the murder, I'll make that motive even stronger."),
//                    new DialogueElement(true, Expression.Angry, "My brother told Raymond the experiment wasn't ready, but Raymond wasn't willing to delay it. He rushed it, ignoring all safety precautions."),
//                }
//            ) },
//            { nameof(ViolentExperimentalResearch), new DialogueSequence(
//                "The technique used by this cloning chamber produces lethal clones.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "The technique used by this cloning chamber produces lethal clones."),
//                    new DialogueElement(false, Expression.Thinking, "It is the same technique used in the incident that killed your brother."), 
//                    new DialogueElement(true, Expression.Thinking, "Ah, so now you think I would make a clone that would attempt to kill Raymond?"),
//                    new DialogueElement(true, Expression.Default, "Well, sorry to disappoint, but even though the experiment resulted in a massacre, it did produce results and we were able to perfect the technique."),
//                    new DialogueElement(true, Expression.Default, "This is the same method, but without that fatal flaw."),
//            } ) },
//            #endregion
//
//            #region Raymond's Clone
//            { nameof(FoundYouRaymondsClone), new DialogueSequence(
//                "I have discoverd you, Clone.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "I have discoverd you, Clone."),
//                    new DialogueElement(true, Expression.Default, "I'm not a clone! I'll excuse your mistake this time. I sent my clone to sleep on my ship. That way, we don't run the risk of having both of us discovered at the same time."),
//            } ) },
//            { nameof(WhyKeepCloneSecret), new DialogueSequence(
//                "Why are you trying to keep your clone a secret?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Why are you trying to keep your clone a secret?"),
//                    new DialogueElement(true, Expression.Default, "I have become far too busy in my CEO responsibilities, but I'll be much more capable to handle them if I have 2 of me. But I'll lose influence if they find out I'm using a clone to do that."),
//            } ) },
//            { nameof(ElectricDischarge), new DialogueSequence(
//                "Where exactly does that recent electrical burn come from?",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Thinking, "Where exactly does that recent electrical burn come from?"),
//                    new DialogueElement(true, Expression.Thinking, "Ah yes..."),
//                    new DialogueElement(true, Expression.Thinking, "Well you see..."),
//                    new DialogueElement(true, Expression.Default, "The cloning process had a malfunction, and I was hit with an electrical discharge."),
//            } ) },
//            { nameof(Bruises), new DialogueSequence(
//                "You have quite a few recent bruises on you.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "You have quite a few recent bruises on you."),
//                    new DialogueElement(true, Expression.Thinking, "Yes, bruises..."),
//                    new DialogueElement(true, Expression.Default, "When the needles take matter from you in the new cloning process, it leaves bruises."),
//            } ) },
//            { nameof(RaymondSaysTheCloningDoesNotGiveBruises), new DialogueSequence(
//                "Travis said that cloning does not give bruises.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Travis said that cloning does not give bruises."),
//                    new DialogueElement(true, Expression.Fear, "Travis is lying."),
//            } ) },
//            { nameof(ItCouldOnlyHaveBeenUsedByYou), new DialogueSequence(
//                "The T-71 Energy Blaster requires Raymond's DNA. You must have been the one to shoot it.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "The T-71 Energy Blaster requires Raymond's DNA. You must have been the one to shoot it."),
//                    new DialogueElement(true, Expression.Thinking, "I didn't... I lent it to my clone right after he was cloned because he was worried about assassins on the way to the ship."),
//                    new DialogueElement(false, Expression.Default, "Then why is it in your room?"),
//                    new DialogueElement(true, Expression.Fear, "..."),
//                    new DialogueElement(true, Expression.Fear, "He returned it to me."),
//            } ) },
//            { nameof(YourCloneShotYourShip), new DialogueSequence(
//                "Your other half shot your ship with the T-71 energy blaster.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Your other half shot your ship with the T-71 energy blaster."),
//                    new DialogueElement(true, Expression.Happy, "Not a problem, I'll just pay for repairs and a new paint job when I get Earth-side."),
//            } ) },
//            { nameof(YouWereDesignedToKill), new DialogueSequence(
//                "You were designed to kill your look-a-like.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "You were designed to kill your look-a-like."),
//                    new DialogueElement(true, Expression.Angry, "I was not designed. I'm Raymond Soule! I'm going to report you for treating me with such discontempt!"),
//                    new DialogueElement(false, Expression.Thinking, "Then the clone was designed to kill."),
//                    new DialogueElement(true, Expression.Angry, "Well, it hasn't fragging tried to kill me yet."),
//            } ) },
//            { nameof(AnotherWitnessHeardYouOnTheShip), new DialogueSequence(
//                "A witness heard your voice aboard Raymond's craft.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "A witness heard your voice aboard Raymond's craft."),
//                    new DialogueElement(true, Expression.Default, "My clone has an identical voice. They probably just heard my clone's voice."),
//            } ) },
//            { nameof(YourBeingRidiculous), new DialogueSequence(
//                "I know you were designed to kill, and that the bruises came from a stuggle with Raymond.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Angry, "I know you were designed to kill, and that the bruises came from a stuggle with Raymond."),
//                    new DialogueElement(false, Expression.Angry, "If you don't start telling me the truth, I will be forced to accuse you."),
//                    new DialogueElement(true, Expression.Fear, "... you are right. I had a violent urge to kill him and tried to choke him. He managed to get away from me though."),
//                    new DialogueElement(true, Expression.Sad, "I didn't kill him, I tried to save him, and in a way you can save the part of him that lives on in me. If I am discovered as a clone, then I'll lose everything."),
//                    new DialogueElement(true, Expression.Happy, "Detective, leave out of your report the fact that I'm a clone. Human Perfect, under my leadership, will give you a handsome reward for your assistance in bringing the would-be assassin to justice."),
//            } ) },
//            { nameof(PostCloneYouShotTheShip), new DialogueSequence(
//                "Now that you're being honest... you shot the craft.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Now that you're being honest... you shot the craft."),
//                    new DialogueElement(true, Expression.Thinking, "... After our struggle, I chased after Raymond with his gun. But when we got to the docking bay, I came to my senses and willed myself not to shoot Raymond, so I shot the ship."),
//                    new DialogueElement(true, Expression.Default, "I lied about the electrical discharge. After shooting Raymond's ship, Raymond ran into the garbage airlock, and I was hit with a bolt of electricity. That knocked me out."),
//            } ) },
//            { nameof(PostCloneYouLaunchedTheShip), new DialogueSequence(
//                "Now tell me honestly. You were the one that launched the ship.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Default, "Now tell me honestly. You were the one that launched the ship."),
//                    new DialogueElement(true, Expression.Sad, "I did. I saw Raymond's body floating in space among garbage. I wanted to save him, so I flew my craft to him. By the time I brought him into the ship, however, he was dead."),
//            } ) },
//            { nameof(YouCanKeepYourLife), new DialogueSequence(
//                "I do deserve a reward, and I won't rob you of your life.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Happy, "I do deserve a reward, and I won't rob you of your life."),
//                    new DialogueElement(true, Expression.Happy, "Pleasure doing business with you."),
//            } ) },
//            { nameof(YouRanYourCompanyPoorly), new DialogueSequence(
//                "I won't help you continue running your evil company.",
//                new DialogueElement[] {
//                    new DialogueElement(false, Expression.Angry, "I won't help you continue running your evil company."),
//                    new DialogueElement(true, Expression.Fear, "... Maybe I can still convince people to let me run it. I'll just have to make some calls."),
//            } ) },
//            #endregion
//        };
//
//        private static DictionaryWithDefault<string, string> _dilemmaOrDeductionText = new DictionaryWithDefault<string, string>("This dilemma or deduction is not implemented") {
//            { nameof(WasZaidsResortAcceptedAsABetaTester), "Was ModeaJet Grand Resort accepted for the luxury clone testing?" },
//            { nameof(ZaidsResortAccepted), "The resort was accepted, because of the message on Raymond's pad" },
//            { nameof(ZaidsResortDeclined), "The resort was declined, ModeaJet Grand Resort was crossed off" },
//
//            { nameof(WhatWasTheTimeFrameForTheMurder), "When did the murder take place?" },
//            { nameof(FromAnytimeUntilEightPM), "It could have happened on any day or any time before 8 PM tonight" },
//            { nameof(SevenAMToEightPM), "It must have happened between 7 AM and 8 PM today" },
//            { nameof(EightPMtoMidnight), "It has to be 8 PM until midnight tonight, so we have to hurry" },
//
//            { nameof(WhoHackedTheDoor), "Who hacked the door on Raymond's craft?" },
//            { nameof(MeleenaHackedTheDoor), "Meleena" },
//            { nameof(TravisHackedTheDoor), "Travis" },
//            { nameof(ZaidHackedTheDoor), "Zaid" },
//
//            { nameof(WhereDidTheBruisesComeFrom), "How did Raymond's clone get bruised?" },
//            { nameof(BruisesCameFromMatterRemoval), "The cloning process bruised him" },
//            { nameof(BruisesCameFromAStruggle), "The bruises came from a struggle" },
//
//            { nameof(WhereDidRaymondsCloneGetAnElectricalBurn), "How did Raymond's clone get an electric burn?" },
//            { nameof(ACloningMalfunction), "He recieved it when being formed from the cloning chamber" },
//            { nameof(AStunGun), "He must have been hit with a stun gun" },
//            { nameof(SomethingElse), "It must have been from something unconventional" },
//
//            { nameof(WhoLaunchedTheShip), "Who launched Raymond's craft" },
//            { nameof(ZaidLaunchedTheShip), "Zaid" },
//            { nameof(MeleenaLaunchedTheShip), "Meleena" },
//            { nameof(TravisLaunchedTheShip), "Travis" },
//            { nameof(RaymondLaunchedTheShip), "Raymond Soule did before his death" },
//            { nameof(RaymondsCloneLaunchedTheShip), "Raymond Soule's Clone" },
//
//            { nameof(WhoShotRaymondsShip), "Who shot Raymond's ship?" },
//            { nameof(RaymondShotHisOwnShip), "Raymond" },
//            { nameof(ZaidShotRaymondsShip), "Zaid" },
//            { nameof(MeleenaShotRaymondsShip), "Meleena" },
//            { nameof(TravisShotRaymondsShip), "Travis" },
//            { nameof(RaymondsCloneShotRaymondsShip), "Raymond's clone" },
//
//            { nameof(WasTheCloneDesignedToKill), "Was the clone designed to kill his look-a-like?" },
//            { nameof(DesignedToKill), "Yes, Travis intentionally used a process that would create a killer clone" },
//            { nameof(PerfectedDesign), "No, the new cloning process was perfected" },
//
//            { nameof(WhatWasTheCauseOfDeath), "What was Raymond's cause of death?" },
//            { nameof(ChokedBySomeone), "The victim was choked to death prior to his exposure to space" },
//            { nameof(PushedOutOfHisShip), "The victim was pushed into space from his ship" },
//            { nameof(KilledInSpace), "The victim had his suit removed while in space" },
//            { nameof(LaunchedIntoSpaceFromTheGarbageAirlock), "The victim was launched into space from the garbage airlock" },
//
//            { nameof(WhoWasTheMurderer), "Who killed Raymond Soule?" },
//            { nameof(ZaidWasTheCulprit), "Zaid" },
//            { nameof(MeleenaWasTheCulprit), "Meleena" },
//            { nameof(TravisWasTheCulprit), "Travis" },
//            { nameof(RaymondsCloneWasTheCulprit), "Raymond Soule's Clone" },
//            { nameof(TravisAndRaymondsCloneAreTheCulprits), "Travis used Raymond's Clone" },
//
//            { nameof(WhatWasTheCulpritsMotive), "What was the culprit's motive?" },
//            { nameof(MeleenaGotCaught), "Meleena was discovered on Raymond's ship and chose to kill over prison" },
//            { nameof(MeleenaKilledHimBecauseHeIsEvil), "Meleena had discovered that he covered up a massacre within his company" },
//            { nameof(RaymondsCloneWasDesignedToKillForRevenge), "Travis blames Raymond for the death of his brother, so he made a murderous clone" },
//            { nameof(RaymondsCloneWasDesignedToKillAndThenControlHumanPerfectByProxy), "Travis wanted to control Human Perfect, so he made a clone to replace Raymond" },
//            { nameof(RevengeForHisBrothersDeath), "Travis blames Raymond for the death of his brother" },
//            { nameof(ToReplaceRaymondAsCEO), "Raymond's clone wasn't satisfied pretending to be the CEO. He decided he was going to replace Raymond" },
//            { nameof(ToSaveHisResort), "Zaid needed to pay off his debts, so he killed to become accepted for luxury clones" },
//        };
//
//        private static DictionaryWithDefault<string, string> _objectiveTexts = new DictionaryWithDefault<string, string>("This objective is not implemented") {
//            { nameof(LookAroundForClues), "Look around this ship for clues." },
//            { nameof(AnswerADilemma), "Answer a dilemma located in the top right corner." },
//            { nameof(InvestigateRaymondsShip), "Investigate the crime scene on Raymond's craft" },
//            { nameof(InvestigateMeleenasCraft), "Investigate Meleena's craft" },
//            { nameof(GoFindOutTheManagersAccount), "Get the resort manager's account" },
//            { nameof(QuestionTravis), "Meet Travis Falcon" },
//            { nameof(FindRaymondsClone), "Find Raymond's clone" },
//            { nameof(CheckWithWarrenForASearchOrder), "Check with Warren about the search order." },
//
//            { nameof(GetAnEncryptionKeyForMeleenasDataDrive), "Get Meleena's Data Encryption Key" },
//            { nameof(CheckWhatsOnMeleenasDataDrive), "Decrypt Meleena's Data Stick" },
//        };
//
//        private static DictionaryWithDefault<string, string> _pathwayText = new DictionaryWithDefault<string, string>("This pathway should not be stopping you"){
//            { nameof(CloningRoomToLobby), "I really should inspect the cloning chamber before I leave." },
//            { nameof(DockingBayToLobby), "\\DockingBayToLobby\\" },
//            { nameof(DockingBayToMeleenasShip), "I will need the owner of this craft to unlock it for me." },
//            { nameof(DockingBayToPoliceCruiser), "Who needs to investigate crime scenes when I can joyride in my cruiser?" },
//            { nameof(LobbyToCloningRoom), "I love barging into random rooms for absolutely no reason." },
//            { nameof(LobbyToVacantRoom), "I'm sure to find a clue if I just wander aimlessly around for hours." },
//            { nameof(MeleenasShipToDockingBay), "Now that I did all that work to get in here, I am just gonna ignore everything in here." },
//            { nameof(PoliceCruiserToDockingBay), "\\PoliceCruiserToDockingBay\\" },
//            { nameof(RaymondsShipToDockingBay), "Look at all these obvious clues I'm trying to ignore." },
//            { nameof(VacantRoomToLobby), "I feel like I'm missing a smoking gun." },
//        };
//
//        private static Dictionary<string, Func<string>> _symbols = new Dictionary<string, Func<string>> {
//            { "Player",
//                () => CurrentGameState.PlayerName
//            },
//            { "Meleena",
//                () =>
//                {
//                    if (CurrentGameState.IsThinking(nameof(HackingRig)))
//                        return "Meleena Ka'lick, Decker";
//                    if (CurrentGameState.IsThinking(nameof(HackerMeleena)) || CurrentGameState.IsThinking(nameof(WhoAreYou)))
//                        return "Meleena Ka'lick, Corp Freelancer";
//                    return "???";
//                }
//            },
//            { "DockingBayToLobby",
//                () => CurrentGameState.IsThinking(nameof(RaymondsCorpse)) ? "Let's pretend Warren doesn't have a female with him." : "Why investigate murders when you can chill in the lobby?"
//            },
//            { "PoliceCruiserToDockingBay",
//                () => CurrentGameState.IsThinking(nameof(Clock)) ? "Maybe try talking to Warren again once you have a working theory setup for the murder timeframe." : "I should talk to Officer Warren before leaving."
//            }
//        };
}

