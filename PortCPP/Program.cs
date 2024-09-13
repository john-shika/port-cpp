using System;
using System.Runtime.InteropServices;

class Program
{
    // Define the data_t structure
    [StructLayout(LayoutKind.Sequential)]
    public struct DataT
    {
        public IntPtr data;
        public UIntPtr size;
    }

    // Define the delegate for the callback function
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FuncNoReturn(int status);

    // Import the dataProcess function from the DLL
    [DllImport("data_process.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr dataProcess(FuncNoReturn fn, string data, UIntPtr size);

    static void Main()
    {
        // Define the callback function
        void Callback(int status)
        {
            Console.WriteLine($"Callback called with status: {status}");
        }

        // Convert the callback to a delegate
        FuncNoReturn callback = new FuncNoReturn(Callback);

        // Call the dataProcess function
        string inputData = "Hello, World!";
        UIntPtr size = new UIntPtr((uint)inputData.Length);
        IntPtr resultPtr = dataProcess(callback, inputData, size);

        // Marshal the result back to managed code
        DataT result = Marshal.PtrToStructure<DataT>(resultPtr);

        // Convert the data pointer to a string
        string resultData = Marshal.PtrToStringAnsi(result.data, (int)result.size);

        Console.WriteLine($"Result data: {resultData}, size: {result.size}");

        // Clean up
        Marshal.FreeHGlobal(result.data);
        Marshal.FreeHGlobal(resultPtr);
    }
}
