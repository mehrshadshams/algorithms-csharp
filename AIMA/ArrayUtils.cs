namespace AIMA
{
    public class ArrayUtils
    {
        public static int Hash(int[] array)
        {
            if (array == null) return 0;
            int result = 1;
            foreach (var t in array)
            {
                result = 31 * result + t;
            }

            return result;
        }
    }
}