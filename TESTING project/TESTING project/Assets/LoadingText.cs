using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingText
{
    public static string[] loreText = { "Ah, Ziae’l. I see, you’ve awakened." + "\nOh, how magnificent. Inchoate as you may be." + "\nLook below, see? Vestigial, is it not?" + "\nRuins, before besmirch struck." + "\nHuh, inpatient. No time to palter, I see." + "\nZiae'l, you must egress. Go. Erstwhile." + "\nIt shall not be easy. You are, afterall, tenuous." + "\nBut, soon. They will stumble. Blind." + "\nYou are oh so effulgent." + "\nA speck, an elflock of fate." + "\nMake haste. Effect an appeal for clemency" + "\nLest your wings be burnt in anguish.", "Ah, Ziae’l. There, a monster? No" + "\nYou can feel it. Can you not?" + "\nAnger? No. Pain? No." + "\nIt is Chaos, but… alive?" + "\nHow? No matter." + "\nZiae’l. Go. Fight it." + "\nBe careful, the ground, it burns." + "\nThere! On the ground. See?" + "\nThat which is green. Stand on it." + "\nNow Now do not get restless." + "\nIt will not go without a fight.", "Ah Ziae’l, that, on your hand." + "\nWhere did you get it?" + "\nIt is dangerous, give it to me." + "\nOh Ziae’l do not be stubborn." + "\n. . ." + "\nVery well, you can keep this one." + "\nBut be careful with it." + "\nRhae'l'aen is not a toy." + "\nOver there? Can you see it?" + "\nThat light. Come." + "\nOur next destination awaits." };
    public static string[] loreTextZ = { "EBH PB" + "\nRBH ORB" + "\nGLI OAI" + "\nOAI MSK" + "\nMP MO" + "\nEBH MLT OBH" + "\nKOBH AOH" + "\nMTO" + "\nEBH RBH" + "\nOB OBLQ" + "\nMP PQHJA" + "\nKPT", "EBH ABH"+ "\nQB"+ "\nOB OB" + "\nK BH" + "\nNO" + "\nEBH KMKBH" + "\nAI K" + "\nCI" + "\nMCI" + "\nTBH" + "\nNBHKA", "EBH" + "\nLM" + "\nOH LBH" + "\nEBH OAL" + "\nOOO" + "\nEBH Q" + "\nCL" + "\nKBHBA" + "\nK" + "\nP KHL" + "\n L EBH T"};
    private static string bridgeTip1 = "Dashing gives great movement speed, but should not be abused.";
    private static string bridgeTipZ1 = "EICL NTLA";    
    private static string bridgeTip2 = "Always keep moving, standing still is dangerous.";
    private static string bridgeTipZ2 = "LHP LHOA";
    public static string[] bridgeTips = { "Git Gud Scrub" };
    public static string[] bridgeTipsZ = { "DPBH" };
    private static string chaosTip1 = "The meteors will only damage when they are close to the ground, you can see their landing with the shadow they cast.";
    private static string chaosTip2 = "The three walls of fire have a hole inside of them that can be used to dodge the attack. Tanking the damage is a costly option.";
    private static string chaosTip3 = "Certain tiles on the ground will damage you, do your best at dodging them.";
    private static string chaosTip4 = "The boss is invincible until you step on all the green patches, be quick to attack him since he will regain his protection.";
    private static string chaosTipsZ1 = "GLI NLO";
    private static string chaosTipsZ2 = "IK OLC JKOBL";
    private static string chaosTipsZ3 = "IA LM";
    private static string chaosTipsZ4 = "BHPJ MT";
    public static string[] chaosTips = { chaosTip1, chaosTip2, chaosTip3, chaosTip4 };
    public static string[] chaosTipsZ = { chaosTipsZ1, chaosTipsZ2, chaosTipsZ3, chaosTipsZ4 };

    public static string GetTime(float timeInSeconds)
    {
        int minutes = ((int)timeInSeconds) / 60;
        int seconds = ((int)timeInSeconds) % 60;

        return minutes + ":" + ((seconds < 10) ? "0" + seconds : seconds.ToString());
    }

}
