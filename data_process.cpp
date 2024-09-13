#include "./data_process.h"
#include <cstring>

data_t* dataProcess(FuncNoReturn fn, const char* data, size_t size) {
    data_t* result = new data_t;
    result->data = new char[size];
    std::memcpy((void*)result->data, data, size);
    result->size = size;

    fn(0); // Example status

    return result;
}
