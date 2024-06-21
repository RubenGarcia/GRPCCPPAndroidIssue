cmake_minimum_required(VERSION 3.19)
cmake_policy(SET CMP0077 NEW)
cmake_policy(SET CMP0091 NEW)

project(test CXX C)

include(CMakePrintHelpers)

set(CMAKE_VERBOSE_MAKEFILE ON)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

list(APPEND CMAKE_CXX_FLAGS_RELEASE "-fno-math-errno -ffast-math -O3")
string(REPLACE ";" " " CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_RELEASE}")
message("All Release Flags: " ${CMAKE_CXX_FLAGS_RELEASE})
set(TARGET_BUILD_PATH ${CMAKE_CURRENT_BINARY_DIR}/lib)
file(MAKE_DIRECTORY ${TARGET_BUILD_PATH})
cmake_print_variables(TARGET_BUILD_PATH)

set(CMAKE_LIBRARY_PATH ${TARGET_BUILD_PATH})
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${TARGET_BUILD_PATH})
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${TARGET_BUILD_PATH})
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${TARGET_BUILD_PATH})

option(STRIP "If set to ON, the symbols will be stripped of binaries" OFF)

string(TIMESTAMP BUILD_INFO "%Y-%m-%d_%H-%M-%S")
add_definitions(-DCMAKE_BUILD_INFO="${BUILD_INFO}")

## Define Build targets begin

set(TARGET_NAME "test")
set(TARGET_NAME_LIB ${TARGET_NAME}_Lib)

## Define Build targets end

set (project_SOURCES_CORE
    src/test/print.cpp
    )

# Group source files by filepath
function(assign_source_group_msvc)
    foreach (_source IN ITEMS ${ARGN})
        if (IS_ABSOLUTE "${_source}")
            file(RELATIVE_PATH _source_rel "${CMAKE_CURRENT_SOURCE_DIR}" "${_source}")
        else ()
            set(_source_rel "${_source}")
        endif ()
        get_filename_component(_source_path "${_source_rel}" PATH)

        string(REPLACE "/" "\\" _source_path_msvc "${_source_path}") # Windows slash handling
        source_group("${_source_path_msvc}" FILES "${_source}")
    endforeach ()
endfunction(assign_source_group_msvc)

set(project_HEADERS_CORE
        ${PROJECT_SOURCE_DIR}/include
        )

set(EXTRA_LIBS)
# # platform-specific settings
if (WIN32)
    add_definitions(
            -DCMAKE_GENERATOR_PLATFORM=x64
            -DWIN32_LEAN_AND_MEAN   # Fix "redefinition; different linkage" issues when including <windows.h> before <winsock2.h>
            -DNOMINMAX  # Fix <windows.h> macro's min/max() overlapping std::min/max
            -D_ENABLE_EXTENDED_ALIGNED_STORAGE  # Fix Eigen issue
            -D_ITERATOR_DEBUG_LEVEL=0
            -D_HAS_ITERATOR_DEBUGGING=0
    )
    assign_source_group_msvc(${project_SOURCES}) # MS Visual Studio cant map filesystem structure to project file structure
endif ()

if (UNIX AND NOT APPLE AND NOT ANDROID)
    set(THREADS_PREFER_PTHREAD_FLAG ON)
    find_package(Threads REQUIRED)
    list(APPEND EXTRA_LIBS Threads::Threads)        # Add Posix threads on Linux
endif ()

# end of platform-specific settings

cmake_print_variables(EXTRA_LIBS)

add_library(${TARGET_NAME_LIB} STATIC ${project_SOURCES_CORE})

target_include_directories(${TARGET_NAME_LIB} PRIVATE ${project_HEADERS_CORE})
target_link_libraries(${TARGET_NAME_LIB} LINK_PUBLIC ${EXTRA_LIBS})


set_target_properties(${TARGET_NAME_LIB} PROPERTIES
        # Hack to keep same name for both library targets
        OUTPUT_NAME ${TARGET_NAME}
        # MSVS sets RuntimeLibrary to dynamic (MDd) even if CMake set to type to static (MTd)
        MSVC_RUNTIME_LIBRARY "MultiThreaded$<$<CONFIG:Debug>:Debug>"
        )

#rgh disable
#target_include_directories(${TARGET_NAME_BIN} PRIVATE ${project_HEADERS_CORE} ${project_HEADERS_RUNTIME})
#target_link_libraries(${TARGET_NAME_BIN} LINK_PUBLIC ${EXTRA_LIBS})

set_target_properties(${TARGET_NAME_BIN} PROPERTIES
        # Hack to keep same name for both library and executable
        OUTPUT_NAME ${TARGET_NAME}
        # For MSVS Debugger to find config.json
        VS_DEBUGGER_WORKING_DIRECTORY $<TARGET_FILE_DIR:${TARGET_NAME}>
        )

file(MAKE_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}/include)

install(TARGETS ${TARGET_NAME_LIB} DESTINATION lib)

