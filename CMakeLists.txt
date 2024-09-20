cmake_minimum_required(VERSION 3.20)

project(data_process VERSION 1.0)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED True)

add_library(data_process SHARED data_process.cpp)
target_include_directories(data_process PUBLIC "${PROJECT_SOURCE_DIR}")
target_compile_definitions(data_process PRIVATE DATA_PROCESS_EXPORTS)
