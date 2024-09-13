#pragma once

#if defined(_WIN32) || defined(_WIN64)
#ifdef DATAPROCESS_EXPORTS
#define DATAPROCESS_API __declspec(dllexport)
#else
#define DATAPROCESS_API __declspec(dllimport)
#endif
#else
#define DATAPROCESS_API
#endif

extern "C" {
    typedef void (*FuncNoReturn)(int status);

    typedef struct {
        const char* data;
        size_t size;
    } data_t;

    DATAPROCESS_API data_t* dataProcess(FuncNoReturn fn, const char* data, size_t size);
}
