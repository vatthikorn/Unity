/*
    Nathan Cruz

    This is just to handle values such:
    KnockBack values for different types of KnockBack
    Critical Hit multiplier
    Direction the player is facing (not a const but is determined by other scripts using this variable)

    Base Class for:
    PlayerAttack.cs
    PlayerRangedAttack.cs
*/

using UnityEngine;
using System.Collections;

public class AttackVars : MonoBehaviour {

    //KnockBack in vertical
    public const float knockBackY = 100f;

    //KnockBack in horizontal
    public const float smallestKnockBack = 100f;
    public const float smallerKnockBack = 150f;
    public const float smallKnockBack = 200f;
    public const float mediumKnockBack = 250f;
    public const float largeKnockBack = 300f;
    public const float largestKnockBack = 350f;

    //Critical Hit Multiplier
    public const float criticalHitMultiplier = 2.0f;

    public int direction;//Facing right = 1, Facing left = -1
    
}
