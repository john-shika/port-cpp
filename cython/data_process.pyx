from libc.stdlib cimport malloc, free
from libc.string cimport memcpy

cdef extern from "../data_process.h":
    ctypedef struct data_t:
        const char* data
        size_t size

    ctypedef void (*FuncNoReturn)(int status)

    data_t* dataProcess(FuncNoReturn fn, const char* data, size_t size)

cdef class DataT:
    cdef data_t* c_data

    def __cinit__(self, const char* data, size_t size):
        self.c_data = <data_t*>malloc(sizeof(data_t))
        self.c_data.data = <const char*>malloc(size)
        memcpy(<void*>self.c_data.data, data, size)
        self.c_data.size = size

    def __dealloc__(self):
        free(<void*>self.c_data.data)
        free(self.c_data)

    @property
    def data(self):
        return self.c_data.data

    @property
    def size(self):
        return self.c_data.size

cdef void callback(int status) noexcept:
    print(f"Callback called with status: {status}")

cdef FuncNoReturn cb = callback

cdef bytearray cast_data_to_bytes(data_t* data):
    if data == NULL:
        return bytearray(0)

    temp = bytearray(data.size)
    for i in range(data.size):
        temp[i] = data.data[i]

    return temp

def process_data(data: bytes):
    cdef DataT result
    result = DataT(data, len(data))
    cdef data_t* res = dataProcess(cb, result.data, result.size)
    return cast_data_to_bytes(res)
