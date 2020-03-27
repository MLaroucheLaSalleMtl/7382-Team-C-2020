using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingText
{
    #region //Lore
    private static string bridgeLoreB = "Ah, Ziae’l. Come, you need to fight." + "\nI know, you are not ready. Soon." + "\nYou must be.";
    private static string bridgeLoreA = "Ah, Ziae'l. I suppose, yes." + "You need practice." + "For now, we will have to make do" + "With you, as you are" + "A weak, little light";
    private static string chaosLoreB = "Ah, Ziae’l. Look, chaos." + "\nQuench the flames, weaken it." + "\nThere, those green patches. Stand on them." + "\nBe careful, it is only temporary.";
    private static string chaosLoreA = "Ah, Ziae’l…" + "\nNo, everything is alright." + "\nLook, that light. We must go." + "\nQuick, before…" + "\nNo, pay me no mind.";
    private static string lifeLoreB = "Ah, Ziae’l. Life.. is afraid?" + "\nIt will run away from you." + "\nOverwhelm it, wait in the light." + "\nBe careful, lest you fall. For eternity";
    private static string lifeLoreA = "Ah! Ziae’l. I did not see you." + "\nAre you done?" + "\nI see…" + "\nCome, we still have…" + "\nOne more." + "\n...?";
    private static string orderLoreB = ". . ." + "\nBe careful." + "\nThis one..." + "\nMeans something to me.";
    private static string orderLoreA = "...ah, Ziae’l. Leave me be." + "\n. . ." + "\nThere, go." + "\nThe last one.";
    private static string finalLoreB = "Ah, Ziae’l. Have you been paying attention?" + "\nI hope you have." + "\nEverything which was, and is." + "\nEnds here" + "\nGo." + "\nZiae'l";
    private static string finalLoreA = "Ah, Ziae’l." + "\nShh, quiet now." + "\nSpeak without sound." + "\nYou can rest now." + "\nClose your eyes." + "\n. . ." + "\nGoodbye" + "\nZiae'l";
    #endregion
    public static string[] loreTexts = { bridgeLoreB, bridgeLoreA, chaosLoreB, chaosLoreA, lifeLoreB, lifeLoreA, orderLoreB, orderLoreA, finalLoreB, finalLoreA };

    #region //LoreZ
    private static string bridgeLoreBZ = "EBH QAM" + "\nSM T" + "\nQB";
    private static string bridgeLoreAZ = "EBH G" + "\nA BL" + "\nSG" + "\nEBH AGT" + "\nOEBH";
    private static string chaosLoreBZ = "EBH K" + "\nHOK" + "\nDI BHML" + "\nOM";
    private static string chaosLoreAZ = "EBH" + "\nSM" + "\nML" + "\nK" + "\nS HA";
    private static string lifeLoreBZ = "EBH BH DLN" + "\nKT" + "\nBH STMI" + "\nBHI PMT";
    private static string lifeLoreAZ = "EBH L H" + "\nTBH" + "\nML" + "\nL BH O" + "\nE BH";
    private static string orderLoreBZ = "EBH" + "\nJL" + "\nBH" + "\nQR";
    private static string orderLoreAZ = "EBH" + "\nO" + "\nL" + "\nNBH";
    private static string finalLoreBZ = "EBH DCTQ" + "\nADL" + "\nSBH TBH" + "\nNLM" + "\nL" + "\nEBH";
    private static string finalLoreAZ = "EBH" + "\nAL ABH" + "\nO N" + "\nAPO" + "\nAD QSD" + "\nT BH QOS" + "\nOH" + "\nBH" + "\nQSBH" + "\nNOS" + "\nO" + "\nNL" + "\n EBH";
    #endregion
    public static string[] loreTextsZ = { bridgeLoreBZ, bridgeLoreAZ, chaosLoreBZ, chaosLoreAZ, lifeLoreBZ, lifeLoreAZ, orderLoreBZ, orderLoreAZ, finalLoreBZ, finalLoreAZ };

    #region //bridgeTips
    private static string bridgeTip1 = "Dashing gives great movement speed, but should not be abused.";
    private static string bridgeTipZ1 = "EICL NTLA";
    private static string bridgeTip2 = "Always keep moving, standing still is dangerous.";
    private static string bridgeTipZ2 = "LHP LHOA";
    private static string bridgeTip3 = "Git Gud Scrub";
    private static string bridgeTipZ3 = "DPBH";
    #endregion
    public static string[] bridgeTips = { bridgeTip1, bridgeTip2, bridgeTip3 };
    public static string[] bridgeTipsZ = { bridgeTipZ1, bridgeTipZ2, bridgeTipZ3 };

    #region //chaosTips
    private static string chaosTip1 = "The meteors will only damage when they are close to the ground, you can see their landing with the shadow they cast.";
    private static string chaosTip2 = "The three walls of fire have a hole inside of them that can be used to dodge the attack. Tanking the damage is a costly option.";
    private static string chaosTip3 = "Certain tiles on the ground will damage you, do your best at dodging them.";
    private static string chaosTip4 = "The boss is invincible until you step on all the green patches, be quick to attack him since he will regain his protection.";
    private static string chaosTip5 = "It's better to take damage from the tiles than the attacks.";
    private static string chaosTipsZ1 = "GLI NLO";
    private static string chaosTipsZ2 = "IK OLC JKOBL";
    private static string chaosTipsZ3 = "IA LM";
    private static string chaosTipsZ4 = "BHPJ MT";
    private static string chaosTipsZ5 = "AIEO BHQSP";
    #endregion
    public static string[] chaosTips = { chaosTip1, chaosTip2, chaosTip3, chaosTip4, chaosTip5 };
    public static string[] chaosTipsZ = { chaosTipsZ1, chaosTipsZ2, chaosTipsZ3, chaosTipsZ4, chaosTipsZ5 };

    #region//lifeTips
    private static string lifeTip1 = "The boss becomes temporarily stunned when either of his shields are depleted, make sure to attack him during that period of time.";
    private static string lifeTip2 = "Be careful, falling through the void deals a lot of damage. A well timed dash will let you clear the gap";
    private static string lifeTip3 = "The boss will teleport away from you if you stay close.";
    private static string lifeTip4 = "The boss' attacks become faster after breaking his shields, don't take your time.";
    private static string lifeTip5 = "Taking the shorter route across the arena is faster but more dangerous.";
    private static string lifeTip1Z = "IJA RT B";
    private static string lifeTip2Z = "AIO LMP";
    private static string lifeTip3Z = "EG FI L";
    private static string lifeTip4Z = "QCE";
    private static string lifeTip5Z = "JMN GF";
    #endregion
    public static string[] lifeTips = { lifeTip1, lifeTip2, lifeTip3, lifeTip4, lifeTip5 };
    public static string[] lifeTipsZ = { lifeTip1Z, lifeTip2Z, lifeTip3Z, lifeTip4Z, lifeTip5Z };


    #region//orderTips
    private static string orderTip1 = "The gargoyles glow before they attack.";
    private static string orderTip2 = "The purple and green balls always follow the same trajectory.";
    private static string orderTip3 = "The boss is more dangerous the lower his HP is.";
    private static string orderTip4 = "Control your movement carefully.";
    private static string orderTip1Z = "QN PI";
    private static string orderTip2Z = "MK MB MN";
    private static string orderTip3Z = "AB E BH";
    private static string orderTip4Z = "M DM";
    #endregion
    public static string[] orderTips = {orderTip1,orderTip2,orderTip3,orderTip4 };
    public static string[] orderTipsZ = { orderTip1Z, orderTip2Z, orderTip3Z, orderTip4Z };

    #region//finalTips

    #endregion
    public static string[] finalTips = { };
    public static string[] finalTipsZ = { };

    public static string congratsText = "Congratulations!\nおめでとう！";
    public static string congratsTextZ = "DPH";

    public static string GetTime(float timeInSeconds)
    {
        int minutes = ((int)timeInSeconds) / 60;
        int seconds = ((int)timeInSeconds) % 60;

        return minutes + ":" + ((seconds < 10) ? "0" + seconds : seconds.ToString());
    }

}
