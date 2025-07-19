# CMakeList.txt : Top-level CMake project file, do global configuration
# and include sub-projects here.
#
cmake_minimum_required (VERSION 3.8)

# Enable Hot Reload for MSVC compilers if supported.
if (POLICY CMP0141)
  cmake_policy(SET CMP0141 NEW)
  set(CMAKE_MSVC_DEBUG_INFORMATION_FORMAT "$<IF:$<AND:$<C_COMPILER_ID:MSVC>,$<CXX_COMPILER_ID:MSVC>>,$<$<CONFIG:Debug,RelWithDebInfo>:EditAndContinue>,$<$<CONFIG:Debug,RelWithDebInfo>:ProgramDatabase>>")
endif()

if(WIN32)
  set(OpenCV_DIR "C:/Users/sabotamas0/Downloads/opencv/build")
else()
  set(OpenCV_DIR "/usr/lib/opencv")
endif()
set(CMAKE_SUPPRESS_REGENERATION true)
project ("ImageProcessingCPP")
find_package(OpenCV REQUIRED COMPONENTS core imgproc highgui)
include_directories(${OpenCV_INCLUDE_DIRS})
add_executable(ImageProcessingCPP ${PROJECT_SOURCE_DIR}/ImageProcessingCPP/ImageProcessingCPP.cpp)
target_link_libraries( ImageProcessingCPP ${OpenCV_LIBS} )
