using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelController
{
    public static bool loadForest = true;
    public static bool skipTutorial = false;

    public static int exampleTreeNr = 1;
    static int numberOfTrees = 3;
    public static void DecreaseChosenTree()
    {
        exampleTreeNr--;
        exampleTreeNr = exampleTreeNr % (numberOfTrees+1);
        if (exampleTreeNr == 0)
        {
            exampleTreeNr++;

        }
    }
    public static void IncreaseChosenTree()
    {
        exampleTreeNr++;
        exampleTreeNr = exampleTreeNr % (numberOfTrees + 1);
        if (exampleTreeNr == 0)
        {
            exampleTreeNr=numberOfTrees;

        }
    }
}
