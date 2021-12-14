using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClearManager
{
    //public static bool[] stageComplete = { false, false, false }; // 스테이지 완료

    //public static bool gameStarted = false;
    public static Vector3 playerPosition = new Vector3(-6.45f, 1.15f, 0f);

    public static bool[] stageClear = { false, false, false }; // 스테이지 클리어 조건 달성
}
