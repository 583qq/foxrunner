using UnityEngine;

public static class GameUtilities
{
    public static int GetLayerNumber(LayerMask layerMask)
    {
        int layerNumber = 0;
        int layer = layerMask.value;

        while(layer > 0)
        {
            layer = layer >> 1; // shift
            layerNumber++;
        }

        return layerNumber - 1;
    }
}