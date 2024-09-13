# Porting C++ Library To Python with Cython And C# using P/Invoke

## example main script for references

- C++ Build

```shell
cmake -S . -B build
cmake --build build --config Release
```

- Python Build And Run

```shell
python3 setup.py build_ext --inplace
python3 main.py
```

- C# Run

```shell
dotnet run
```
