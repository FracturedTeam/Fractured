using UnityEngine;

public class Profil : ScriptableObject
{
    public  int act = 1;
    public  float transition = 0;

    public  Color act1_Color_A;
    public  Vector2 act1_Color_A_Location = new Vector2(0f,0.5f);
    public  Color act1_Color_B;
    public  Vector2 act1_Color_B_Location = new Vector2(0.5f, 1f);
    public  Color act1_Color_C;

    public  Color act2_Color_A;
    public  Vector2 act2_Color_A_Location = new Vector2(0f, 0.5f);
    public  Color act2_Color_B;
    public  Vector2 act2_Color_B_Location = new Vector2(0.5f, 1f);
    public  Color act2_Color_C;

    public  Color act3_Color_A;
    public  Vector2 act3_Color_A_Location = new Vector2(0f, 0.5f);
    public  Color act3_Color_B;
    public  Vector2 act3_Color_B_Location = new Vector2(0.5f, 1f);
    public  Color act3_Color_C;
}
