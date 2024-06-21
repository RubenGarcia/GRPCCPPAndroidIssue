   cmake \
	-G "Unix Makefiles" \
        -DCMAKE_TOOLCHAIN_FILE=$ANDROID_NDK_HOME/build/cmake/android.toolchain.cmake \
        -DANDROID_ABI=arm64-v8a \
	    -DANDROID_NDK=$ANDROID_NDK_HOME \
        -DANDROID_PLATFORM=android-29 \
        -DCMAKE_BUILD_TYPE=Release \
        -DSTRIP=ON \
        ..

