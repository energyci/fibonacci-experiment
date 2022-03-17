namespace UtilityLibraries;

public static class FibLibrary
{
    public static int GetFib(int n) {
            int number = n;
            int[] Fib = new int[number + 1];
            Fib[0] = 0;
            Fib[1] = 1;
            for(int i = 2; i <= number; i++){
                Fib[i] = Fib[i - 2] + Fib[i - 1];
            }
            return Fib[number];
        }

    public static int SlowGetFib(int n) {
        if(n == 1) 
            return 1;
        if(n <= 0)
            return 0;
        return SlowGetFib(n - 2) + SlowGetFib(n - 1);
    }
}