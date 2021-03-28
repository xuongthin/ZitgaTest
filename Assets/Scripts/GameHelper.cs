
public static class GameHelper
{
    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this T[] array)
    {
        int i = array.Length;
        while (i > 1)
        {
            int num = rng.Next(i--);
            T t = array[i];
            array[i] = array[num];
            array[num] = t;
        }
    }
}
