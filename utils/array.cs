namespace Utils
{

    /*
        This is me adding functionality to an existing type. 
        I want to be abble to get a random ellement from an array without having to do the boilerplate every time. 
    */

    public static class ArrayExtensions
    {
        private static Random random = new Random();

        public static T RandomElement<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Array cannot be null or empty", nameof(array));
            }

            return array[random.Next(array.Length)];
        }
    }
}