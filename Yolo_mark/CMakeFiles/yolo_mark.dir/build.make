# CMAKE generated file: DO NOT EDIT!
# Generated by "Unix Makefiles" Generator, CMake Version 3.17

# Delete rule output on recipe failure.
.DELETE_ON_ERROR:


#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:


# Disable VCS-based implicit rules.
% : %,v


# Disable VCS-based implicit rules.
% : RCS/%


# Disable VCS-based implicit rules.
% : RCS/%,v


# Disable VCS-based implicit rules.
% : SCCS/s.%


# Disable VCS-based implicit rules.
% : s.%


.SUFFIXES: .hpux_make_needs_suffix_list


# Command-line flag to silence nested $(MAKE).
$(VERBOSE)MAKESILENT = -s

# Suppress display of executed commands.
$(VERBOSE).SILENT:


# A target that is always out of date.
cmake_force:

.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

# The shell in which to execute make rules.
SHELL = /bin/sh

# The CMake executable.
CMAKE_COMMAND = /usr/local/Cellar/cmake/3.17.1/bin/cmake

# The command to remove a file.
RM = /usr/local/Cellar/cmake/3.17.1/bin/cmake -E rm -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = /Users/fong/YOLO/Yolo_mark

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = /Users/fong/YOLO/Yolo_mark

# Include any dependencies generated for this target.
include CMakeFiles/yolo_mark.dir/depend.make

# Include the progress variables for this target.
include CMakeFiles/yolo_mark.dir/progress.make

# Include the compile flags for this target's objects.
include CMakeFiles/yolo_mark.dir/flags.make

CMakeFiles/yolo_mark.dir/main.cpp.o: CMakeFiles/yolo_mark.dir/flags.make
CMakeFiles/yolo_mark.dir/main.cpp.o: main.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=/Users/fong/YOLO/Yolo_mark/CMakeFiles --progress-num=$(CMAKE_PROGRESS_1) "Building CXX object CMakeFiles/yolo_mark.dir/main.cpp.o"
	/Library/Developer/CommandLineTools/usr/bin/c++  $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -o CMakeFiles/yolo_mark.dir/main.cpp.o -c /Users/fong/YOLO/Yolo_mark/main.cpp

CMakeFiles/yolo_mark.dir/main.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/yolo_mark.dir/main.cpp.i"
	/Library/Developer/CommandLineTools/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E /Users/fong/YOLO/Yolo_mark/main.cpp > CMakeFiles/yolo_mark.dir/main.cpp.i

CMakeFiles/yolo_mark.dir/main.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/yolo_mark.dir/main.cpp.s"
	/Library/Developer/CommandLineTools/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S /Users/fong/YOLO/Yolo_mark/main.cpp -o CMakeFiles/yolo_mark.dir/main.cpp.s

# Object files for target yolo_mark
yolo_mark_OBJECTS = \
"CMakeFiles/yolo_mark.dir/main.cpp.o"

# External object files for target yolo_mark
yolo_mark_EXTERNAL_OBJECTS =

yolo_mark: CMakeFiles/yolo_mark.dir/main.cpp.o
yolo_mark: CMakeFiles/yolo_mark.dir/build.make
yolo_mark: /usr/local/lib/libopencv_gapi.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_stitching.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_alphamat.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_aruco.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_bgsegm.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_bioinspired.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_ccalib.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_dnn_objdetect.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_dnn_superres.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_dpm.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_face.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_freetype.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_fuzzy.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_hfs.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_img_hash.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_intensity_transform.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_line_descriptor.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_quality.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_rapid.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_reg.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_rgbd.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_saliency.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_sfm.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_stereo.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_structured_light.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_superres.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_surface_matching.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_tracking.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_videostab.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_xfeatures2d.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_xobjdetect.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_xphoto.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_highgui.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_shape.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_datasets.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_plot.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_text.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_dnn.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_ml.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_phase_unwrapping.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_optflow.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_ximgproc.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_video.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_videoio.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_imgcodecs.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_objdetect.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_calib3d.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_features2d.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_flann.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_photo.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_imgproc.4.3.0.dylib
yolo_mark: /usr/local/lib/libopencv_core.4.3.0.dylib
yolo_mark: CMakeFiles/yolo_mark.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --bold --progress-dir=/Users/fong/YOLO/Yolo_mark/CMakeFiles --progress-num=$(CMAKE_PROGRESS_2) "Linking CXX executable yolo_mark"
	$(CMAKE_COMMAND) -E cmake_link_script CMakeFiles/yolo_mark.dir/link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
CMakeFiles/yolo_mark.dir/build: yolo_mark

.PHONY : CMakeFiles/yolo_mark.dir/build

CMakeFiles/yolo_mark.dir/clean:
	$(CMAKE_COMMAND) -P CMakeFiles/yolo_mark.dir/cmake_clean.cmake
.PHONY : CMakeFiles/yolo_mark.dir/clean

CMakeFiles/yolo_mark.dir/depend:
	cd /Users/fong/YOLO/Yolo_mark && $(CMAKE_COMMAND) -E cmake_depends "Unix Makefiles" /Users/fong/YOLO/Yolo_mark /Users/fong/YOLO/Yolo_mark /Users/fong/YOLO/Yolo_mark /Users/fong/YOLO/Yolo_mark /Users/fong/YOLO/Yolo_mark/CMakeFiles/yolo_mark.dir/DependInfo.cmake --color=$(COLOR)
.PHONY : CMakeFiles/yolo_mark.dir/depend

