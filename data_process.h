#pragma once

#include <cstdio>
#include <cstdlib>
#include <cstdbool>
#include <cstdint>

#if defined(_WIN32) || defined(_WIN64)
#ifdef DATA_PROCESS_EXPORTS
#define DATA_PROCESS_API __declspec(dllexport)
#else
#define DATA_PROCESS_API __declspec(dllimport)
#endif
#else
#define DATA_PROCESS_API
#endif

extern "C" {
    typedef void (*FuncNoReturn)(int status);

    typedef struct {
        const char* data;
        size_t size;
    } data_t;

    DATA_PROCESS_API data_t* dataProcess(FuncNoReturn fn, const char* data, size_t size);
}
