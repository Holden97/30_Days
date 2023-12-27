using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.XR;

namespace OfficeWar
{
    /// <summary>
    /// 思路参见 https://gpp.tkchu.me/bytecode.html
    /// </summary>
    public class ShopParser
    {
        public static void Parse(string bytecode)
        {
            var stack = new Stack<object>();
            var instructions = bytecode.Split(';');
            foreach (var instruction in instructions)
            {

                var literals = instruction.Split(" ");
                switch (literals[0])
                {
                    case "ADD_FIXED_SPEED":
                        var bonus = stack.Pop();
                        var characterId = stack.Pop();
                        AddFixedSpeed((int)characterId, (int)bonus);
                        break;
                    case "ADD_MAX_HP":
                        stack.Push(LITERAL(literals[1]));
                        var bonus2 = stack.Pop();
                        var characterId2 = stack.Pop();
                        AddMaxHp((int)characterId2, bonus2);
                        break;
                    case "ADD_ATTACK_SPEED":
                        var bonus7 = stack.Pop();
                        var characterId7 = stack.Pop();
                        SET_ATTACK_SPEED((int)characterId7, (int)bonus7);
                        break;
                    case "ADD_SHIELD_PER_WAVE":
                        var bonus3 = stack.Pop();
                        var characterId3 = stack.Pop();
                        ADD_SHIELD_PER_WAVE((int)characterId3, (int)bonus3);
                        break;
                    case "GET_MAX_HP":
                        var characterId4 = stack.Pop();
                        stack.Push(GET_MAX_HP((int)characterId4));
                        break;
                    case "LITERAL":
                        stack.Push(LITERAL(literals[1]));
                        break;
                    case "MUL":
                        var multiplier1 = stack.Pop();
                        var multiplier2 = stack.Pop();
                        stack.Push(MUL((float)multiplier1, (float)multiplier2));
                        break;
                    case "ADD":
                        var add1 = stack.Pop();
                        var add2 = stack.Pop();
                        stack.Push(AddObjects(add1, add2));
                        break;
                    case "GET_DAMAGE_INCREASEMENT_PERSENT":
                        var characterId5 = stack.Pop();
                        stack.Push(GetDamageIncreasementPersent((int)characterId5));
                        break;
                    case "SET_DAMAGE_IMCRESEMENT_PERCENT":
                        var percent = stack.Pop();
                        float percentValue = 0;
                        if (percent is int)
                        {
                            percentValue = (int)percent;
                        }
                        if (percent is float)
                        {
                            percentValue = (float)percent;
                        }

                        var characterId6 = stack.Pop();
                        int characterId6Value = 0;
                        if (characterId6 is int)
                        {
                            characterId6Value = (int)characterId6;
                        }
                        if (characterId6 is float)
                        {
                            //unbox时要先转成对应的数据类型（float），再强转(int)，否则出错
                            characterId6Value = (int)(float)characterId6;
                        }
                        SetDamageImcresementPercent(characterId6Value, percentValue);
                        break;
                    case "GET_PLAYER_ID":
                        stack.Push(0);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void SET_ATTACK_SPEED(int characterId7, int bonus7)
        {
            Character character = GameManager.Instance.GetCharacter(characterId7);
            character.AttackSpeed += bonus7;
        }

        public static void AddMaxHp(int characterId, object bonus)
        {
            Character character = GameManager.Instance.GetCharacter(characterId);
            var value = ConvertObject(bonus);
            if (value is int)
            {
                character.health.maxHp += (int)value;
            }
            else if (value is float)
            {
                character.health.maxHp += (float)value;
            }
        }

        static object AddObjects(object a, object b)
        {
            if (a is int && b is int)
            {
                // 如果两个操作数都是 int，直接相加
                return (int)a + (int)b;
            }
            else if (a is float || b is float)
            {
                // 如果其中一个或两个操作数是 double，转换为 double 再相加
                return Convert.ToDouble(a) + Convert.ToDouble(b);
            }
            else if (a is string || b is string)
            {
                // 如果其中一个或两个操作数是 string，将它们转换为字符串并进行拼接
                return a.ToString() + b.ToString();
            }
            else
            {
                // 其他情况可能需要根据具体需求进行处理
                throw new ArgumentException("Unsupported data types for addition");
            }
        }

        public static void AddFixedSpeed(int characterId, float bonus)
        {
            Character character = GameManager.Instance.GetCharacter(characterId);
            character.speed.SetSpeed(character.speed.GetSpeed() * (bonus + 1));
        }

        public static void ADD_SHIELD_PER_WAVE(int characterId, int bonus)
        {
            Character character = GameManager.Instance.GetCharacter(characterId);
            character.shieldPerWaveBeforeStart += bonus;
        }

        public static void SET_SHIELD_PER_WAVE(int characterId, int layerCount)
        {
            Character character = GameManager.Instance.GetCharacter(characterId);
            character.shieldPerWaveBeforeStart = layerCount;
        }

        public static float GET_MAX_HP(int characterId)
        {
            Character character = GameManager.Instance.GetCharacter(characterId);
            return character.health.maxHp;
        }

        public static object LITERAL(object bonus)
        {
            //TODO:这里需要细化
            if (bonus is string s)
            {
                if (int.TryParse(s, out int intValue))
                {
                    return intValue;
                }
                else if (float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out float floatValue))
                {
                    return floatValue;
                }
            }
            return bonus;
        }

        public static float MUL(float multiplier1, float multiplier2)
        {
            return multiplier1 * multiplier2;
        }

        public static float ADD(float add1, float add2)
        {
            return add1 + add2;
        }

        public static float GetDamageIncreasementPersent(int characterId)
        {
            Character character = GameManager.Instance.GetCharacter(characterId);
            return character.damageEnhancedPercent;
        }

        public static void SetDamageImcresementPercent(int characterId, float bonus)
        {
            Character character = GameManager.Instance.GetCharacter(characterId);
            character.damageEnhancedPercent += bonus;
        }

        public void Push(Stack<object> stack, object value)
        {

            if (value is int)
            {
                int intValue = (int)value;
                stack.Push(intValue);
            }
            else if (value is float)
            {
                float floatValue = (float)value;
                stack.Push(floatValue);
            }
        }

        public static object ConvertObject(object value)
        {

            if (value is int)
            {
                int intValue = (int)value;
                return (intValue);
            }
            else if (value is float)
            {
                float floatValue = (float)value;
                return (floatValue);
            }
            return value;
        }
    }
}
