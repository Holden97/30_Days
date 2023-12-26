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
                        var bonus2 = stack.Pop();
                        var characterId2 = stack.Pop();
                        SetMaxHp((int)characterId2, (int)bonus2);
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
                    case "GET_DAMAGE_INCREASEMENT_PERSENT":
                        var characterId5 = stack.Pop();
                        stack.Push(GetDamageIncreasementPersent((int)characterId5));
                        break;
                    case "SET_DAMAGE_IMCRESEMENT_PERCENT":
                        var percent = stack.Pop();
                        var characterId6 = stack.Pop();
                        SetDamageImcresementPercent((int)characterId6, (float)percent);
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

        public static void SetMaxHp(int characterId, int maxHp)
        {
            Character character = GameManager.Instance.GetCharacter(characterId);
            character.health.maxHp += maxHp;
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
    }
}
