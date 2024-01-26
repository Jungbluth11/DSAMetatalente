using DSAUtils.HeldentoolInterop;

namespace Metatalente.Core
{
    public abstract class MetatalentBase
    {
#pragma warning disable CS8618 // is set in child classes
        protected Core core;
#pragma warning restore CS8618
        public int Duration { get; set; } = 60;
        public int MinDuration { get; protected set; } = 60;
        public int SkillPoints { get; set; }
        public bool IsSet { get; protected set; } = true;
        public Result LastResult { get; protected set; }

        public abstract void SetSkill();

        public abstract void Roll();

        protected void SetSkill(string[] baseSkills)
        {
            List<int> skillValues = new List<int>();
            int lowest = 0;
            int sum = 0;

            foreach (string baseSkill in baseSkills)
            {
                if (core.character != null)
                {
                    try
                    {
                        Ability ability = core.character.Talente.Single(a => a.Name == baseSkill);
                    }
                    catch
                    {
                        IsSet = false;
                        return;
                    }
                }
                switch (baseSkill)
                {
                    case "Wildnisleben":
                        skillValues.Add(core.SkillWildnisleben);
                        break;
                    case "Sinnenschärfe":
                        skillValues.Add(core.SkillSinnenschaerfe);
                        break;
                    case "Pflanzenkunde":
                        skillValues.Add(core.SkillPflanzenkunde);
                        break;
                    case "Tierkunde":
                        skillValues.Add(core.SkillTierkunde);
                        break;
                    case "Fährtensuchen":
                        skillValues.Add(core.SkillFaehrtensuchen);
                        break;
                    case "Schleichen":
                        skillValues.Add(core.SkillSchleichen);
                        break;
                    case "Sich Verstecken":
                        skillValues.Add(core.SkillSichVerstecken);
                        break;
                    default:
                        skillValues.Add(core.SkillWeapon);
                        break;
                }
            }
            foreach (int value in skillValues)
            {
                if (value < lowest || lowest == 0)
                {
                    lowest = value;
                }
                sum += value;
            }
            if (sum / skillValues.Count > lowest * 2)
            {
                SkillPoints = lowest * 2;
            }
            else
            {
                SkillPoints = sum / skillValues.Count;
            }
        }

        protected (int, string) Roll(int attribute1, int attribute2, int attribute3, int mod, int skillPoints = 0)
        {
            if (!IsSet)
            {
                throw new Exception("The loaded Character has not this Skill. Can't roll");
            }
            if (Duration < MinDuration)
            {
                throw new Exception("Duration can't be less than MinDuration");
            }
            if (skillPoints == 0)
            {
                skillPoints = SkillPoints;
            }
            if (core.IsKnownTerrain)
            {
                mod -= 3;
            }
            object[] result = DSA.TaP(attribute1, attribute2, attribute3, skillPoints, mod);
            object[] rolldata = (object[])result[1];
            string textdata = rolldata[0].ToString() + "/" + rolldata[1].ToString() + "/" + rolldata[2].ToString();
            if (!string.IsNullOrEmpty((string)rolldata[3]))
            {
                textdata += " (" + rolldata[3].ToString() + ")";
            }
            return ((int)result[0], textdata);
        }
    }
}